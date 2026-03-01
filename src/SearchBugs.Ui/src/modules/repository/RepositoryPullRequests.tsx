import React, { useState } from "react";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { apiClient, PullRequestListItemDto, PullRequestDetailDto } from "@/lib/api";
import { DiffViewer } from "@/components/repository/DiffViewer";
import { useToast } from "@/hooks/use-toast";
import { format } from "date-fns";
import { GitPullRequest, Plus } from "lucide-react";

interface RepositoryPullRequestsProps {
  repoUrl: string;
  branches: string[];
  currentUserId?: string;
}

export const RepositoryPullRequests: React.FC<RepositoryPullRequestsProps> = ({
  repoUrl,
  branches,
  currentUserId = "",
}) => {
  const [showCreate, setShowCreate] = useState(false);
  const [selectedPrId, setSelectedPrId] = useState<string | null>(null);
  const [createSource, setCreateSource] = useState("");
  const [createTarget, setCreateTarget] = useState("");
  const [createTitle, setCreateTitle] = useState("");
  const [createDescription, setCreateDescription] = useState("");
  const { toast } = useToast();
  const queryClient = useQueryClient();

  const { data: prList = [], isLoading } = useQuery({
    queryKey: ["pull-requests", repoUrl],
    queryFn: async () => {
      const res = await apiClient.repositories.getPullRequests(repoUrl);
      const data = res.data as { isSuccess?: boolean; value?: PullRequestListItemDto[] };
      if (data?.isSuccess && Array.isArray(data.value)) return data.value;
      return [];
    },
  });

  const { data: prDetail, isLoading: detailLoading } = useQuery({
    queryKey: ["pull-request", repoUrl, selectedPrId],
    queryFn: async () => {
      if (!selectedPrId) return null;
      const res = await apiClient.repositories.getPullRequest(repoUrl, selectedPrId);
      const data = res.data as { isSuccess?: boolean; value?: PullRequestDetailDto };
      if (data?.isSuccess && data.value) return data.value;
      return null;
    },
    enabled: !!selectedPrId,
  });

  const createMutation = useMutation({
    mutationFn: async () =>
      apiClient.repositories.createPullRequest(repoUrl, {
        sourceBranch: createSource,
        targetBranch: createTarget,
        title: createTitle,
        description: createDescription,
        createdByUserId: currentUserId,
      }),
    onSuccess: () => {
      toast({ title: "Pull request created" });
      setShowCreate(false);
      setCreateSource("");
      setCreateTarget("");
      setCreateTitle("");
      setCreateDescription("");
      queryClient.invalidateQueries({ queryKey: ["pull-requests", repoUrl] });
    },
    onError: (e: Error) => toast({ title: "Failed to create PR", description: e.message, variant: "destructive" }),
  });

  const mergeMutation = useMutation({
    mutationFn: async (prId: string) =>
      apiClient.repositories.mergePullRequest(repoUrl, prId, {
        authorName: "Web User",
        authorEmail: "web@local",
        mergedByUserId: currentUserId,
      }),
    onSuccess: () => {
      toast({ title: "Pull request merged" });
      setSelectedPrId(null);
      queryClient.invalidateQueries({ queryKey: ["pull-requests", repoUrl] });
    },
    onError: (e: Error) => toast({ title: "Merge failed", description: e.message, variant: "destructive" }),
  });

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h3 className="font-semibold">Pull requests</h3>
        <Button onClick={() => setShowCreate(true)} size="sm">
          <Plus className="h-4 w-4 mr-1" />
          New pull request
        </Button>
      </div>

      {isLoading ? (
        <div className="text-sm text-muted-foreground">Loading...</div>
      ) : (
        <Card>
          <CardContent className="p-0">
            <ul className="divide-y">
              {prList.map((pr) => (
                <li key={pr.id}>
                  <button
                    type="button"
                    onClick={() => setSelectedPrId(pr.id)}
                    className="w-full text-left px-3 py-2 hover:bg-muted/50 flex items-center gap-2"
                  >
                    <GitPullRequest className="h-4 w-4 shrink-0" />
                    <span className="font-medium truncate flex-1">{pr.title}</span>
                    <span className="text-xs text-muted-foreground">
                      {pr.sourceBranch} → {pr.targetBranch}
                    </span>
                    <span className="text-xs text-muted-foreground">{pr.status}</span>
                  </button>
                </li>
              ))}
            </ul>
            {prList.length === 0 && (
              <div className="p-6 text-center text-muted-foreground text-sm">No pull requests yet.</div>
            )}
          </CardContent>
        </Card>
      )}

      <Dialog open={showCreate} onOpenChange={setShowCreate}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>New pull request</DialogTitle>
          </DialogHeader>
          <div className="space-y-4">
            <div>
              <label className="text-sm font-medium">Source branch</label>
              <Select value={createSource} onValueChange={setCreateSource}>
                <SelectTrigger className="mt-1">
                  <SelectValue placeholder="Select" />
                </SelectTrigger>
                <SelectContent>
                  {branches.map((b) => (
                    <SelectItem key={b} value={b}>{b}</SelectItem>
                  ))}
                </SelectContent>
              </Select>
            </div>
            <div>
              <label className="text-sm font-medium">Target branch</label>
              <Select value={createTarget} onValueChange={setCreateTarget}>
                <SelectTrigger className="mt-1">
                  <SelectValue placeholder="Select" />
                </SelectTrigger>
                <SelectContent>
                  {branches.map((b) => (
                    <SelectItem key={b} value={b}>{b}</SelectItem>
                  ))}
                </SelectContent>
              </Select>
            </div>
            <div>
              <label className="text-sm font-medium">Title</label>
              <Input value={createTitle} onChange={(e) => setCreateTitle(e.target.value)} className="mt-1" placeholder="Title" />
            </div>
            <div>
              <label className="text-sm font-medium">Description</label>
              <Input value={createDescription} onChange={(e) => setCreateDescription(e.target.value)} className="mt-1" placeholder="Description" />
            </div>
            <Button
              onClick={() => createMutation.mutate()}
              disabled={!createSource || !createTarget || !createTitle.trim() || createMutation.isPending}
              className="w-full"
            >
              {createMutation.isPending ? "Creating..." : "Create"}
            </Button>
          </div>
        </DialogContent>
      </Dialog>

      <Dialog open={!!selectedPrId} onOpenChange={(open) => !open && setSelectedPrId(null)}>
        <DialogContent className="max-w-4xl max-h-[80vh] overflow-y-auto">
          <DialogHeader>
            <DialogTitle>{prDetail?.title ?? "Pull request"}</DialogTitle>
          </DialogHeader>
          {detailLoading ? (
            <div className="text-sm text-muted-foreground">Loading...</div>
          ) : prDetail ? (
            <div className="space-y-4">
              <p className="text-sm text-muted-foreground">
                {prDetail.sourceBranch} → {prDetail.targetBranch} · {prDetail.status}
              </p>
              {prDetail.description && <p className="text-sm">{prDetail.description}</p>}
              <DiffViewer diffs={prDetail.diff ?? []} />
              {prDetail.status === "Open" && selectedPrId && (
                <Button
                  onClick={() => mergeMutation.mutate(selectedPrId)}
                  disabled={mergeMutation.isPending}
                >
                  {mergeMutation.isPending ? "Merging..." : "Merge"}
                </Button>
              )}
            </div>
          ) : null}
        </DialogContent>
      </Dialog>
    </div>
  );
};
