import { useParams, useNavigate } from "react-router-dom";
import { useApi } from "@/hooks/useApi";
import { apiClient } from "@/lib/api";
import type { UpdateBugDto } from "@/lib/api";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Badge } from "@/components/ui/badge";
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Textarea } from "@/components/ui/textarea";
import { RichTextEditor } from "@/components/ui/rich-text-editor";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useState } from "react";
import ReactMarkdown from "react-markdown";
import remarkGfm from "remark-gfm";
import {
  Clock,
  MessageSquare,
  Paperclip,
  History,
  Settings,
  ArrowLeft,
  Download,
  Edit,
  Check,
  X,
} from "lucide-react";
import { useTimezone } from "@/hooks/useTimezone";
import { useToast } from "@/components/ui/use-toast";
import { PageLoadingState } from "@/components/ui/loading";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { queryClient } from "@/lib/queryClient";

interface Bug {
  id: string;
  title: string;
  description: string;
  status: string;
  priority: string;
  severity: string;
  projectId: string;
  assigneeId: string;
  reporterId: string;
  createdAt: string;
  updatedAt: string;
}

interface Comment {
  id: string;
  commentText: string;
  userId: string;
  createdOnUtc: string;
  modifiedOnUtc?: string;
  userName?: string; // This might come from a join or separate call
}

interface Attachment {
  id: string;
  fileName: string;
  fileSize: number;
  uploadedAt: string;
  uploadedBy: string;
}

interface TimeEntry {
  id: string;
  duration: string;
  description: string;
  loggedAt: string;
  loggedBy: string;
}

interface HistoryEntry {
  id: string;
  fieldChanged: string;
  oldValue: string;
  newValue: string;
  changedById: string;
  changedAtUtc: string;
  userName: string;
}

interface CustomField {
  id: string;
  name: string;
  value: string;
}

export const BugDetailsPage = () => {
  const { bugId } = useParams<{ bugId: string }>();
  const navigate = useNavigate();
  const { toast } = useToast();
  const { formatDistanceToNow } = useTimezone();
  const [newComment, setNewComment] = useState("");
  const [isAddingComment, setIsAddingComment] = useState(false);
  const [timeEntry, setTimeEntry] = useState({ duration: "", description: "" });
  const [customField, setCustomField] = useState({ name: "", value: "" });

  // Inline editing states
  const [editingField, setEditingField] = useState<string | null>(null);
  const [editedTitle, setEditedTitle] = useState("");
  const [editedDescription, setEditedDescription] = useState("");
  const [editedStatus, setEditedStatus] = useState("");
  const [editedPriority, setEditedPriority] = useState("");
  const [editedSeverity, setEditedSeverity] = useState("");
  const [isUpdating, setIsUpdating] = useState(false);

  const { data: bug, isLoading: isLoadingBug } = useApi<Bug>(`bugs/${bugId}`);
  const { data: comments, mutate: mutateComments } = useApi<Comment[]>(
    `bugs/${bugId}/comments`
  );
  const {
    data: attachments,
    mutate: { mutateAsync: mutateAttachments },
  } = useApi<Attachment[]>(`bugs/${bugId}/attachments`);
  const { data: timeEntries, mutate: mutateTimeEntries } = useApi<TimeEntry[]>(
    `bugs/${bugId}/time-tracking`
  );
  const { data: history } = useApi<HistoryEntry[]>(`bugs/${bugId}/history`);
  const { data: customFields, mutate: mutateCustomFields } = useApi<
    CustomField[]
  >(`bugs/${bugId}/custom-fields`);

  // Available options for dropdowns
  const statusOptions = ["Open", "In Progress", "Resolved", "Closed"];
  const priorityOptions = ["Low", "Medium", "High"];
  const severityOptions = ["Low", "Medium", "High"];

  // Function to render markdown in the preview
  const renderMarkdown = (text: string) => {
    return (
      <div className="prose prose-sm max-w-none">
        <ReactMarkdown remarkPlugins={[remarkGfm]}>{text}</ReactMarkdown>
      </div>
    );
  };

  // Handle starting inline edit
  const startEdit = (field: string, currentValue: string) => {
    setEditingField(field);
    switch (field) {
      case "title":
        setEditedTitle(currentValue);
        break;
      case "description":
        setEditedDescription(currentValue);
        break;
      case "status":
        setEditedStatus(currentValue);
        break;
      case "priority":
        setEditedPriority(currentValue);
        break;
      case "severity":
        setEditedSeverity(currentValue);
        break;
    }
  };

  // Handle canceling edit
  const cancelEdit = () => {
    setEditingField(null);
    setEditedTitle("");
    setEditedDescription("");
    setEditedStatus("");
    setEditedPriority("");
    setEditedSeverity("");
  };

  // Handle saving field update
  const saveFieldUpdate = async () => {
    if (!bugId || !bug?.value) return;

    setIsUpdating(true);
    try {
      const updateData = {
        title: editingField === "title" ? editedTitle : bug.value.title,
        description:
          editingField === "description"
            ? editedDescription
            : bug.value.description,
        status: editingField === "status" ? editedStatus : bug.value.status,
        priority:
          editingField === "priority" ? editedPriority : bug.value.priority,
        severity:
          editingField === "severity" ? editedSeverity : bug.value.severity,
        assigneeId: bug.value.assigneeId,
      } as UpdateBugDto;

      const response = await apiClient.bugs.update(bugId, updateData);

      if (response.data.isSuccess) {
        queryClient.invalidateQueries({ queryKey: [`bugs/${bugId}`] });
        setEditingField(null);
        toast({
          title: "Success",
          description: "Bug updated successfully",
        });
      } else {
        toast({
          title: "Error",
          description: response.data.error?.message || "Failed to update bug",
          variant: "destructive",
        });
      }
    } catch (error) {
      console.error("Error updating bug:", error);
      toast({
        title: "Error",
        description: "Failed to update bug. Please try again.",
        variant: "destructive",
      });
    } finally {
      setIsUpdating(false);
    }
  };

  const handleAddComment = async () => {
    if (!newComment.trim() || !bugId) return;

    if (newComment.length > 10000) {
      toast({
        title: "Comment too long",
        description: "Comment must be less than 10000 characters.",
        variant: "destructive",
      });
      return;
    }

    setIsAddingComment(true);
    try {
      console.log("Adding comment with data:", { bugId, content: newComment });
      const response = await apiClient.comments.create({
        bugId: bugId,
        content: newComment,
      });
      console.log("API response:", response);

      if (response.data.isSuccess) {
        setNewComment("");
        // Refetch comments
        if (mutateComments.mutate) {
          mutateComments.mutate({});
        }
        toast({
          title: "Comment added",
          description: "Your comment has been added successfully.",
        });
      } else {
        // Handle validation errors specifically
        if (response.data.error?.code === "ValidationError") {
          const errorMessage =
            response.data.error?.message || "Validation failed";
          toast({
            title: "Validation Error",
            description: errorMessage,
            variant: "destructive",
          });
        } else {
          toast({
            title: "Error",
            description:
              response.data.error?.message || "Failed to add comment",
            variant: "destructive",
          });
        }
      }
    } catch (error) {
      console.error("Error adding comment:", error);
      toast({
        title: "Error",
        description: "Failed to add comment. Please try again.",
        variant: "destructive",
      });
    } finally {
      setIsAddingComment(false);
    }
  };

  const handleTrackTime = async () => {
    if (!timeEntry.duration || !timeEntry.description) return;
    try {
      const result = await mutateTimeEntries.mutateAsync(timeEntry);
      if (result.isSuccess) {
        setTimeEntry({ duration: "", description: "" });
        toast({
          title: "Time logged",
          description: "Time has been logged successfully.",
        });
      } else {
        toast({
          title: "Error",
          description: result.error.message,
          variant: "destructive",
        });
      }
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to log time. Please try again.",
        variant: "destructive",
      });
    }
  };

  const handleAddCustomField = async () => {
    if (!customField.name || !customField.value) return;
    try {
      const result = await mutateCustomFields.mutateAsync(customField);
      if (result.isSuccess) {
        setCustomField({ name: "", value: "" });
        toast({
          title: "Custom field added",
          description: "Custom field has been added successfully.",
        });
      } else {
        toast({
          title: "Error",
          description: result.error.message,
          variant: "destructive",
        });
      }
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to add custom field. Please try again.",
        variant: "destructive",
      });
    }
  };

  const handleFileUpload = async (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const file = event.target.files?.[0];
    if (!file) return;

    const formData = new FormData();
    formData.append("file", file);

    try {
      const response = await fetch(`/api/bugs/${bugId}/attachments`, {
        method: "POST",
        body: formData,
      });
      const result = await response.json();
      if (result.isSuccess) {
        await mutateAttachments({});
        toast({
          title: "File uploaded",
          description: "File has been uploaded successfully.",
        });
      } else {
        toast({
          title: "Error",
          description: result.error.message,
          variant: "destructive",
        });
      }
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to upload file. Please try again.",
        variant: "destructive",
      });
    }
  };

  if (isLoadingBug) return <PageLoadingState text="Loading bug details..." />;
  if (!bug?.value) return <div>Bug not found</div>;

  const bugData = bug.value;

  return (
    <div className="container mx-auto py-6 space-y-6 max-w-7xl">
      <div className="flex items-center justify-between bg-card p-4 rounded-lg shadow-xs">
        <div className="flex-1">
          {/* Editable Title */}
          {editingField === "title" ? (
            <div className="flex items-center gap-2 mb-2">
              <Input
                value={editedTitle}
                onChange={(e) => setEditedTitle(e.target.value)}
                className="text-xl font-bold"
                autoFocus
                onKeyDown={(e) => {
                  if (e.key === "Enter") saveFieldUpdate();
                  if (e.key === "Escape") cancelEdit();
                }}
              />
              <Button
                size="sm"
                onClick={saveFieldUpdate}
                disabled={isUpdating}
                className="h-8 w-8 p-0"
              >
                <Check className="h-4 w-4" />
              </Button>
              <Button
                size="sm"
                variant="outline"
                onClick={cancelEdit}
                className="h-8 w-8 p-0"
              >
                <X className="h-4 w-4" />
              </Button>
            </div>
          ) : (
            <div
              className="group cursor-pointer"
              onClick={() => startEdit("title", bugData.title)}
            >
              <div className="flex items-center gap-2 mb-2">
                <h1 className="text-2xl font-bold tracking-tight group-hover:bg-muted/50 rounded px-1 -mx-1 transition-colors">
                  {bugData.title}
                </h1>
                <Edit className="h-4 w-4 opacity-0 group-hover:opacity-50 transition-opacity" />
              </div>
            </div>
          )}

          {/* Editable Status, Priority, Severity Badges */}
          <div className="flex gap-2">
            {/* Status Badge */}
            {editingField === "status" ? (
              <div className="flex items-center gap-1">
                <Select value={editedStatus} onValueChange={setEditedStatus}>
                  <SelectTrigger className="h-8 w-auto min-w-20">
                    <SelectValue />
                  </SelectTrigger>
                  <SelectContent>
                    {statusOptions.map((status) => (
                      <SelectItem key={status} value={status}>
                        {status}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
                <Button
                  size="sm"
                  onClick={saveFieldUpdate}
                  disabled={isUpdating}
                  className="h-6 w-6 p-0"
                >
                  <Check className="h-3 w-3" />
                </Button>
                <Button
                  size="sm"
                  variant="outline"
                  onClick={cancelEdit}
                  className="h-6 w-6 p-0"
                >
                  <X className="h-3 w-3" />
                </Button>
              </div>
            ) : (
              <div
                className="group cursor-pointer"
                onClick={() => startEdit("status", bugData.status)}
              >
                <Badge
                  variant={
                    bugData.status === "Open"
                      ? "default"
                      : bugData.status === "In Progress"
                      ? "secondary"
                      : "outline-solid"
                  }
                  className="group-hover:ring-1 group-hover:ring-muted-foreground/20 transition-all"
                >
                  {bugData.status}
                  <Edit className="h-3 w-3 ml-1 opacity-0 group-hover:opacity-50 transition-opacity" />
                </Badge>
              </div>
            )}

            {/* Priority Badge */}
            {editingField === "priority" ? (
              <div className="flex items-center gap-1">
                <Select
                  value={editedPriority}
                  onValueChange={setEditedPriority}
                >
                  <SelectTrigger className="h-8 w-auto min-w-20">
                    <SelectValue />
                  </SelectTrigger>
                  <SelectContent>
                    {priorityOptions.map((priority) => (
                      <SelectItem key={priority} value={priority}>
                        {priority}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
                <Button
                  size="sm"
                  onClick={saveFieldUpdate}
                  disabled={isUpdating}
                  className="h-6 w-6 p-0"
                >
                  <Check className="h-3 w-3" />
                </Button>
                <Button
                  size="sm"
                  variant="outline"
                  onClick={cancelEdit}
                  className="h-6 w-6 p-0"
                >
                  <X className="h-3 w-3" />
                </Button>
              </div>
            ) : (
              <div
                className="group cursor-pointer"
                onClick={() => startEdit("priority", bugData.priority)}
              >
                <Badge
                  variant={
                    bugData.priority === "Critical"
                      ? "destructive"
                      : bugData.priority === "High"
                      ? "default"
                      : "outline-solid"
                  }
                  className="group-hover:ring-1 group-hover:ring-muted-foreground/20 transition-all"
                >
                  {bugData.priority}
                  <Edit className="h-3 w-3 ml-1 opacity-0 group-hover:opacity-50 transition-opacity" />
                </Badge>
              </div>
            )}

            {/* Severity Badge */}
            {editingField === "severity" ? (
              <div className="flex items-center gap-1">
                <Select
                  value={editedSeverity}
                  onValueChange={setEditedSeverity}
                >
                  <SelectTrigger className="h-8 w-auto min-w-20">
                    <SelectValue />
                  </SelectTrigger>
                  <SelectContent>
                    {severityOptions.map((severity) => (
                      <SelectItem key={severity} value={severity}>
                        {severity}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
                <Button
                  size="sm"
                  onClick={saveFieldUpdate}
                  disabled={isUpdating}
                  className="h-6 w-6 p-0"
                >
                  <Check className="h-3 w-3" />
                </Button>
                <Button
                  size="sm"
                  variant="outline"
                  onClick={cancelEdit}
                  className="h-6 w-6 p-0"
                >
                  <X className="h-3 w-3" />
                </Button>
              </div>
            ) : (
              <div
                className="group cursor-pointer"
                onClick={() => startEdit("severity", bugData.severity)}
              >
                <Badge
                  variant={
                    bugData.severity === "Critical"
                      ? "destructive"
                      : bugData.severity === "High"
                      ? "default"
                      : "outline-solid"
                  }
                  className="group-hover:ring-1 group-hover:ring-muted-foreground/20 transition-all"
                >
                  {bugData.severity}
                  <Edit className="h-3 w-3 ml-1 opacity-0 group-hover:opacity-50 transition-opacity" />
                </Badge>
              </div>
            )}
          </div>
        </div>
        <Button
          variant="outline"
          onClick={() => navigate("/bugs")}
          className="gap-2"
        >
          <ArrowLeft className="h-4 w-4" />
          Back to Bugs
        </Button>
      </div>

      <Tabs defaultValue="details" className="space-y-4">
        <TabsList className="w-full justify-start h-auto p-1 bg-muted/50">
          <TabsTrigger
            value="details"
            className="data-[state=active]:bg-background"
          >
            Details
          </TabsTrigger>
          <TabsTrigger
            value="comments"
            className="data-[state=active]:bg-background"
          >
            <MessageSquare className="w-4 h-4 mr-2" />
            Comments
          </TabsTrigger>
          <TabsTrigger
            value="attachments"
            className="data-[state=active]:bg-background"
          >
            <Paperclip className="w-4 h-4 mr-2" />
            Attachments
          </TabsTrigger>
          <TabsTrigger
            value="time"
            className="data-[state=active]:bg-background"
          >
            <Clock className="w-4 h-4 mr-2" />
            Time Tracking
          </TabsTrigger>
          <TabsTrigger
            value="history"
            className="data-[state=active]:bg-background"
          >
            <History className="w-4 h-4 mr-2" />
            History
          </TabsTrigger>
          <TabsTrigger
            value="custom-fields"
            className="data-[state=active]:bg-background"
          >
            <Settings className="w-4 h-4 mr-2" />
            Custom Fields
          </TabsTrigger>
        </TabsList>

        <TabsContent value="details" className="space-y-4">
          <Card className="border-none shadow-xs">
            <CardHeader>
              <CardTitle className="text-lg">Description</CardTitle>
            </CardHeader>
            <CardContent>
              {/* Editable Description */}
              {editingField === "description" ? (
                <div className="space-y-2">
                  <RichTextEditor
                    value={editedDescription}
                    onChange={setEditedDescription}
                    placeholder="Describe the bug in detail..."
                    maxLength={10000}
                    className="min-h-[300px]"
                  />
                  <div className="flex items-center gap-2">
                    <Button
                      size="sm"
                      onClick={saveFieldUpdate}
                      disabled={isUpdating}
                    >
                      <Check className="h-4 w-4 mr-2" />
                      Save
                    </Button>
                    <Button size="sm" variant="outline" onClick={cancelEdit}>
                      <X className="h-4 w-4 mr-2" />
                      Cancel
                    </Button>
                  </div>
                </div>
              ) : (
                <div
                  className="group cursor-pointer p-2 -m-2 rounded-md hover:bg-muted/50 transition-colors"
                  onClick={() => startEdit("description", bugData.description)}
                >
                  <div className="flex items-start gap-2">
                    <div className="whitespace-pre-wrap text-muted-foreground flex-1">
                      {bugData.description ? (
                        renderMarkdown(bugData.description)
                      ) : (
                        <span className="text-muted-foreground">
                          Click to add a description...
                        </span>
                      )}
                    </div>
                    <Edit className="h-4 w-4 opacity-0 group-hover:opacity-50 transition-opacity mt-0.5" />
                  </div>
                </div>
              )}
            </CardContent>
          </Card>
        </TabsContent>

        <TabsContent value="comments" className="space-y-4">
          <Card className="border-none shadow-xs">
            <CardContent className="pt-6">
              <div className="space-y-6">
                {comments?.value?.map((comment) => (
                  <div key={comment.id} className="flex gap-4">
                    <Avatar className="h-8 w-8">
                      <AvatarFallback className="bg-primary/10 text-primary">
                        {comment.userName?.[0] || "U"}
                      </AvatarFallback>
                    </Avatar>
                    <div className="flex-1 space-y-1">
                      <div className="flex items-center gap-2">
                        <span className="font-medium">
                          {comment.userName || "Unknown User"}
                        </span>
                        <span className="text-sm text-muted-foreground">
                          {formatDistanceToNow(comment.createdOnUtc)}
                        </span>
                      </div>
                      <div className="text-sm text-muted-foreground">
                        {renderMarkdown(comment.commentText)}
                      </div>
                    </div>
                  </div>
                ))}
              </div>
              <div className="mt-6 space-y-4">
                <RichTextEditor
                  value={newComment}
                  onChange={setNewComment}
                  placeholder="Add a comment... Supports **bold**, _italic_, `code`, lists, and more!"
                  maxLength={10000}
                  className="min-h-[150px]"
                />
                <div className="flex justify-between items-center">
                  <div className="text-sm text-muted-foreground">
                    💡 Use Markdown formatting for rich text
                  </div>
                  <Button
                    onClick={handleAddComment}
                    disabled={
                      isAddingComment ||
                      !newComment.trim() ||
                      newComment.length > 10000
                    }
                    className="w-auto"
                  >
                    {isAddingComment ? "Adding..." : "Add Comment"}
                  </Button>
                </div>
              </div>
            </CardContent>
          </Card>
        </TabsContent>

        <TabsContent value="attachments" className="space-y-4">
          <Card className="border-none shadow-xs">
            <CardContent className="pt-6">
              <div className="space-y-4">
                {attachments?.value?.map((attachment) => (
                  <div
                    key={attachment.id}
                    className="flex items-center gap-4 p-4 rounded-lg bg-muted/50"
                  >
                    <Paperclip className="w-4 h-4 text-muted-foreground" />
                    <div className="flex-1">
                      <div className="font-medium">{attachment.fileName}</div>
                      <div className="text-sm text-muted-foreground">
                        {formatDistanceToNow(attachment.uploadedAt)} by{" "}
                        {attachment.uploadedBy}
                      </div>
                    </div>
                    <Button variant="ghost" size="sm" className="gap-2">
                      <Download className="h-4 w-4" />
                      Download
                    </Button>
                  </div>
                ))}
              </div>
              <div className="mt-6">
                <Label htmlFor="file" className="text-sm font-medium">
                  Upload Attachment
                </Label>
                <div className="mt-2 flex items-center gap-4">
                  <Input
                    id="file"
                    type="file"
                    onChange={handleFileUpload}
                    className="max-w-sm"
                  />
                </div>
              </div>
            </CardContent>
          </Card>
        </TabsContent>

        <TabsContent value="time" className="space-y-4">
          <Card className="border-none shadow-xs">
            <CardContent className="pt-6">
              <div className="space-y-4">
                {timeEntries?.value?.map((entry) => (
                  <div
                    key={entry.id}
                    className="flex items-center gap-4 p-4 rounded-lg bg-muted/50"
                  >
                    <Clock className="w-4 h-4 text-muted-foreground" />
                    <div className="flex-1">
                      <div className="font-medium">{entry.duration}</div>
                      <div className="text-sm text-muted-foreground">
                        {entry.description}
                      </div>
                      <div className="text-sm text-muted-foreground">
                        {formatDistanceToNow(entry.loggedAt)} by{" "}
                        {entry.loggedBy}
                      </div>
                    </div>
                  </div>
                ))}
              </div>
              <div className="mt-6 space-y-4">
                <div className="grid gap-2">
                  <Label htmlFor="duration" className="text-sm font-medium">
                    Duration (e.g., 2h 30m)
                  </Label>
                  <Input
                    id="duration"
                    value={timeEntry.duration}
                    onChange={(e) =>
                      setTimeEntry({ ...timeEntry, duration: e.target.value })
                    }
                    placeholder="Enter duration"
                    className="max-w-sm"
                  />
                </div>
                <div className="grid gap-2">
                  <Label
                    htmlFor="timeDescription"
                    className="text-sm font-medium"
                  >
                    Description
                  </Label>
                  <Textarea
                    id="timeDescription"
                    value={timeEntry.description}
                    onChange={(e) =>
                      setTimeEntry({
                        ...timeEntry,
                        description: e.target.value,
                      })
                    }
                    placeholder="What did you work on?"
                    className="min-h-[100px]"
                  />
                </div>
                <Button
                  onClick={handleTrackTime}
                  disabled={mutateTimeEntries.isPending}
                  className="w-full sm:w-auto"
                >
                  {mutateTimeEntries.isPending ? "Logging..." : "Log Time"}
                </Button>
              </div>
            </CardContent>
          </Card>
        </TabsContent>

        <TabsContent value="history" className="space-y-4">
          <Card className="border-none shadow-xs">
            <CardContent className="pt-6">
              <div className="space-y-4">
                {history?.value?.map((entry) => (
                  <div
                    key={entry.id}
                    className="flex gap-4 p-4 rounded-lg bg-muted/50"
                  >
                    <Avatar className="h-8 w-8">
                      <AvatarFallback className="bg-primary/10 text-primary">
                        {entry.userName?.[0] || "U"}
                      </AvatarFallback>
                    </Avatar>
                    <div className="flex-1">
                      <div className="font-medium">
                        {entry.fieldChanged} changed
                      </div>
                      <div className="text-sm text-muted-foreground">
                        {entry.oldValue && entry.oldValue !== "" ? (
                          <>
                            From{" "}
                            <span className="font-mono bg-red-100 px-1 rounded">
                              {entry.oldValue}
                            </span>{" "}
                            to{" "}
                            <span className="font-mono bg-green-100 px-1 rounded">
                              {entry.newValue}
                            </span>
                          </>
                        ) : (
                          <>
                            Set to{" "}
                            <span className="font-mono bg-green-100 px-1 rounded">
                              {entry.newValue}
                            </span>
                          </>
                        )}
                      </div>
                      <div className="text-sm text-muted-foreground">
                        {formatDistanceToNow(entry.changedAtUtc)} by{" "}
                        {entry.userName}
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>
        </TabsContent>

        <TabsContent value="custom-fields" className="space-y-4">
          <Card className="border-none shadow-xs">
            <CardContent className="pt-6">
              <div className="space-y-4">
                {customFields?.value?.map((field) => (
                  <div
                    key={field.id}
                    className="grid grid-cols-2 gap-4 p-4 rounded-lg bg-muted/50"
                  >
                    <div className="font-medium">{field.name}</div>
                    <div className="text-muted-foreground">{field.value}</div>
                  </div>
                ))}
              </div>
              <div className="mt-6 space-y-4">
                <div className="grid gap-2">
                  <Label htmlFor="fieldName" className="text-sm font-medium">
                    Field Name
                  </Label>
                  <Input
                    id="fieldName"
                    value={customField.name}
                    onChange={(e) =>
                      setCustomField({ ...customField, name: e.target.value })
                    }
                    placeholder="Enter field name"
                    className="max-w-sm"
                  />
                </div>
                <div className="grid gap-2">
                  <Label htmlFor="fieldValue" className="text-sm font-medium">
                    Value
                  </Label>
                  <Input
                    id="fieldValue"
                    value={customField.value}
                    onChange={(e) =>
                      setCustomField({ ...customField, value: e.target.value })
                    }
                    placeholder="Enter value"
                    className="max-w-sm"
                  />
                </div>
                <Button
                  onClick={handleAddCustomField}
                  disabled={mutateCustomFields.isPending}
                  className="w-full sm:w-auto"
                >
                  {mutateCustomFields.isPending
                    ? "Adding..."
                    : "Add Custom Field"}
                </Button>
              </div>
            </CardContent>
          </Card>
        </TabsContent>
      </Tabs>
    </div>
  );
};
