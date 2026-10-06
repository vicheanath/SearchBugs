import {
  ListFilter,
  MoreHorizontal,
  PlusCircle,
  Trash2,
  Pencil,
  Eye,
} from "lucide-react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardFooter } from "@/components/ui/card";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger,
  DropdownMenuCheckboxItem,
  DropdownMenuSeparator,
} from "@/components/ui/dropdown-menu";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useApi } from "@/hooks/useApi";
import { useToast } from "@/components/ui/use-toast";
import { useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { Badge } from "@/components/ui/badge";
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Bug } from "lucide-react";
import { ListLoadingSkeleton } from "@/components/ui/loading";

interface Bug {
  id: string;
  title: string;
  description: string;
  status: string;
  priority: string;
  severity: string;
  reporter: string;
  assignee: string;
  projectId: string;
}

const statuses = ["Open", "In Progress", "Resolved", "Closed"];

export const BugsPage = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  const projectId = searchParams.get("projectId");
  const status = searchParams.get("status");
  const { data, isLoading } = useApi<Bug[]>(
    `bugs${projectId ? `?projectId=${projectId}` : ""}`
  );
  const { mutate: mutateDelete } = useApi<Bug>(`bugs`);
  const navigate = useNavigate();
  const { toast } = useToast();
  const [selectedBug, setSelectedBug] = useState<Bug | null>(null);
  const [isDeleteDialogOpen, setIsDeleteDialogOpen] = useState(false);

  const handleStatusFilter = (newStatus: string | null) => {
    if (newStatus === status) {
      searchParams.delete("status");
    } else {
      searchParams.set("status", newStatus!);
    }
    setSearchParams(searchParams);
  };

  const handleDeleteBug = async (bugId: string) => {
    try {
      const result = await mutateDelete.mutateAsync({ id: bugId });
      if (result.isSuccess) {
        toast({
          title: "Bug deleted",
          description: "The bug has been deleted successfully.",
        });
        setIsDeleteDialogOpen(false);
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
        description: "Failed to delete bug. Please try again.",
        variant: "destructive",
      });
    }
  };

  const filteredBugs = data?.value?.filter((bug) => {
    if (status && bug.status !== status) return false;
    return true;
  });

  if (isLoading) {
    return (
      <div className="container mx-auto py-6 space-y-6 max-w-7xl">
        <div className="flex items-center justify-between bg-card p-4 rounded-lg shadow-xs">
          <div>
            <h1 className="text-2xl font-bold tracking-tight">Bugs</h1>
            <p className="text-sm text-muted-foreground mt-1">
              Manage and track bugs across your projects
            </p>
          </div>
          <div className="flex items-center gap-2">
            <Button variant="outline" size="sm" className="h-9 gap-1" disabled>
              <ListFilter className="h-4 w-4" />
              <span className="sr-only sm:not-sr-only sm:whitespace-nowrap">
                Filter
              </span>
            </Button>
            <Button size="sm" className="h-9 gap-1" disabled>
              <PlusCircle className="h-4 w-4" />
              <span className="sr-only sm:not-sr-only sm:whitespace-nowrap">
                Add Bug
              </span>
            </Button>
          </div>
        </div>

        <Card className="border-none shadow-xs">
          <CardContent className="p-6">
            <ListLoadingSkeleton items={8} />
          </CardContent>
        </Card>
      </div>
    );
  }

  return (
    <div className="container mx-auto py-6 space-y-6 max-w-7xl">
      <div className="flex items-center justify-between bg-card p-4 rounded-lg shadow-xs">
        <div>
          <h1 className="text-2xl font-bold tracking-tight">Bugs</h1>
          <p className="text-sm text-muted-foreground mt-1">
            Manage and track bugs across your projects
          </p>
        </div>
        <div className="flex items-center gap-2">
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline" size="sm" className="h-9 gap-1">
                <ListFilter className="h-4 w-4" />
                <span className="sr-only sm:not-sr-only sm:whitespace-nowrap">
                  Filter
                </span>
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end" className="w-[200px]">
              <DropdownMenuLabel>Filter by status</DropdownMenuLabel>
              <DropdownMenuSeparator />
              {statuses.map((s) => (
                <DropdownMenuCheckboxItem
                  key={s}
                  checked={status === s}
                  onCheckedChange={() => handleStatusFilter(s)}
                  className="cursor-pointer"
                >
                  {s}
                </DropdownMenuCheckboxItem>
              ))}
            </DropdownMenuContent>
          </DropdownMenu>
          <Button
            size="sm"
            className="h-9 gap-1"
            onClick={() => navigate("/add-bug")}
          >
            <PlusCircle className="h-4 w-4" />
            <span className="sr-only sm:not-sr-only sm:whitespace-nowrap">
              Add Bug
            </span>
          </Button>
        </div>
      </div>

      <Card className="border-none shadow-xs">
        <CardContent className="p-0">
          <div className="relative w-full overflow-auto">
            <Table>
              <TableHeader>
                <TableRow className="hover:bg-muted/50">
                  <TableHead className="w-[300px]">Title</TableHead>
                  <TableHead className="hidden md:table-cell">
                    Description
                  </TableHead>
                  <TableHead className="w-[100px]">Status</TableHead>
                  <TableHead className="hidden md:table-cell w-[100px]">
                    Priority
                  </TableHead>
                  <TableHead className="hidden md:table-cell w-[100px]">
                    Severity
                  </TableHead>
                  <TableHead className="hidden md:table-cell w-[150px]">
                    Reporter
                  </TableHead>
                  <TableHead className="hidden md:table-cell w-[150px]">
                    Assignee
                  </TableHead>
                  <TableHead className="w-[50px]">
                    <span className="sr-only">Actions</span>
                  </TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {filteredBugs?.map((bug) => (
                  <TableRow key={bug.id} className="hover:bg-muted/50">
                    <TableCell className="font-medium">
                      <div className="flex items-center gap-2">
                        <span className="truncate max-w-[250px]">
                          {bug.title}
                        </span>
                      </div>
                    </TableCell>
                    <TableCell className="hidden md:table-cell">
                      <span className="text-muted-foreground truncate block max-w-[300px]">
                        {bug.description}
                      </span>
                    </TableCell>
                    <TableCell>
                      <Badge
                        variant={
                          bug.status === "Open"
                            ? "default"
                            : bug.status === "In Progress"
                            ? "secondary"
                            : "outline-solid"
                        }
                      >
                        {bug.status}
                      </Badge>
                    </TableCell>
                    <TableCell className="hidden md:table-cell">
                      <Badge
                        variant={
                          bug.priority === "Critical"
                            ? "destructive"
                            : bug.priority === "High"
                            ? "default"
                            : "outline-solid"
                        }
                      >
                        {bug.priority}
                      </Badge>
                    </TableCell>
                    <TableCell className="hidden md:table-cell">
                      <Badge
                        variant={
                          bug.severity === "Critical"
                            ? "destructive"
                            : bug.severity === "High"
                            ? "default"
                            : "outline-solid"
                        }
                      >
                        {bug.severity}
                      </Badge>
                    </TableCell>
                    <TableCell className="hidden md:table-cell">
                      <div className="flex items-center gap-2">
                        <Avatar className="h-6 w-6">
                          <AvatarFallback className="bg-primary/10 text-primary text-xs">
                            {bug.reporter[0]}
                          </AvatarFallback>
                        </Avatar>
                        <span className="text-sm">{bug.reporter}</span>
                      </div>
                    </TableCell>
                    <TableCell className="hidden md:table-cell">
                      <div className="flex items-center gap-2">
                        <Avatar className="h-6 w-6">
                          <AvatarFallback className="bg-primary/10 text-primary text-xs">
                            {bug.assignee[0]}
                          </AvatarFallback>
                        </Avatar>
                        <span className="text-sm">{bug.assignee}</span>
                      </div>
                    </TableCell>
                    <TableCell>
                      <DropdownMenu>
                        <DropdownMenuTrigger asChild>
                          <Button
                            aria-haspopup="true"
                            size="icon"
                            variant="ghost"
                            className="h-8 w-8"
                          >
                            <MoreHorizontal className="h-4 w-4" />
                            <span className="sr-only">Toggle menu</span>
                          </Button>
                        </DropdownMenuTrigger>
                        <DropdownMenuContent align="end" className="w-[160px]">
                          <DropdownMenuLabel>Actions</DropdownMenuLabel>
                          <DropdownMenuItem
                            onClick={() => navigate(`/bugs/${bug.id}`)}
                            className="cursor-pointer"
                          >
                            <Eye className="h-4 w-4 mr-2" />
                            View Details
                          </DropdownMenuItem>
                          <DropdownMenuItem
                            onClick={() => {
                              setSelectedBug(bug);
                              // TODO: Implement edit functionality
                            }}
                            className="cursor-pointer"
                          >
                            <Pencil className="h-4 w-4 mr-2" />
                            Edit
                          </DropdownMenuItem>
                          <DropdownMenuItem
                            className="text-destructive cursor-pointer"
                            onClick={() => {
                              setSelectedBug(bug);
                              setIsDeleteDialogOpen(true);
                            }}
                          >
                            <Trash2 className="h-4 w-4 mr-2" />
                            Delete
                          </DropdownMenuItem>
                        </DropdownMenuContent>
                      </DropdownMenu>
                    </TableCell>
                  </TableRow>
                ))}
                {filteredBugs?.length === 0 && (
                  <TableRow>
                    <TableCell colSpan={8} className="h-24 text-center">
                      <div className="flex flex-col items-center justify-center gap-1">
                        <Bug className="h-8 w-8 text-muted-foreground" />
                        <p className="text-sm font-medium">No bugs found</p>
                        <p className="text-sm text-muted-foreground">
                          Get started by creating a new bug
                        </p>
                      </div>
                    </TableCell>
                  </TableRow>
                )}
              </TableBody>
            </Table>
          </div>
        </CardContent>
        <CardFooter className="border-t bg-muted/50 px-6 py-3">
          <div className="flex items-center justify-between w-full">
            <span className="text-sm text-muted-foreground">
              {filteredBugs?.length}{" "}
              {filteredBugs?.length === 1 ? "bug" : "bugs"}
            </span>
            {status && (
              <Button
                variant="ghost"
                size="sm"
                onClick={() => handleStatusFilter(null)}
                className="h-8"
              >
                Clear filter
              </Button>
            )}
          </div>
        </CardFooter>
      </Card>

      <Dialog open={isDeleteDialogOpen} onOpenChange={setIsDeleteDialogOpen}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Delete Bug</DialogTitle>
            <DialogDescription>
              Are you sure you want to delete this bug? This action cannot be
              undone.
            </DialogDescription>
          </DialogHeader>
          <DialogFooter>
            <Button
              variant="outline"
              onClick={() => setIsDeleteDialogOpen(false)}
            >
              Cancel
            </Button>
            <Button
              variant="destructive"
              onClick={() => selectedBug && handleDeleteBug(selectedBug.id)}
              disabled={mutateDelete.isPending}
            >
              {mutateDelete.isPending ? "Deleting..." : "Delete"}
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
};
