import React, { useState } from "react";
import { useQuery } from "@tanstack/react-query";
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { apiClient, CommitInfoDto, FileDiff } from "@/lib/api";
import { DiffViewer } from "@/components/repository/DiffViewer";
import { RefreshCw } from "lucide-react";
import { format } from "date-fns";

interface RepositoryCommitsProps {
  repoUrl: string;
  branch?: string;
}

export const RepositoryCommits: React.FC<RepositoryCommitsProps> = ({ repoUrl, branch = "main" }) => {
  const [selectedCommit, setSelectedCommit] = useState<CommitInfoDto | null>(null);
  const [commitDiff, setCommitDiff] = useState<FileDiff[]>([]);

  const { data: commits = [], isLoading, refetch } = useQuery({
    queryKey: ["commits", repoUrl, branch],
    queryFn: async () => {
      const res = await apiClient.repositories.getCommits(repoUrl, { branch, take: 50 });
      const data = res.data as { isSuccess?: boolean; value?: CommitInfoDto[] };
      if (data?.isSuccess && Array.isArray(data.value)) return data.value;
      return [];
    },
  });

  const fetchDiff = async (sha: string) => {
    try {
      const res = await apiClient.repositories.getCommitDiff(repoUrl, sha);
      const data = res.data as { isSuccess?: boolean; value?: FileDiff[] };
      if (data?.isSuccess && Array.isArray(data.value)) setCommitDiff(data.value);
      else setCommitDiff([]);
    } catch {
      setCommitDiff([]);
    }
  };

  const handleSelectCommit = (c: CommitInfoDto) => {
    setSelectedCommit(c);
    fetchDiff(c.sha);
  };

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between">
        <h3 className="font-semibold">Commits</h3>
        <Button variant="outline" size="sm" onClick={() => refetch()} disabled={isLoading}>
          <RefreshCw className={`h-4 w-4 mr-1 ${isLoading ? "animate-spin" : ""}`} />
          Refresh
        </Button>
      </div>
      {isLoading ? (
        <div className="text-sm text-muted-foreground">Loading commits...</div>
      ) : (
        <div className="grid gap-4 md:grid-cols-2">
          <Card>
            <CardContent className="p-0">
              <ul className="divide-y max-h-[400px] overflow-y-auto">
                {commits.map((c) => (
                  <li key={c.sha}>
                    <button
                      type="button"
                      onClick={() => handleSelectCommit(c)}
                      className={`w-full text-left px-3 py-2 hover:bg-muted/50 transition-colors ${selectedCommit?.sha === c.sha ? "bg-muted" : ""}`}
                    >
                      <div className="font-mono text-xs text-muted-foreground truncate">{c.sha.slice(0, 7)}</div>
                      <div className="text-sm font-medium truncate">{c.message.split("\n")[0]}</div>
                      <div className="text-xs text-muted-foreground">
                        {c.authorName} · {format(new Date(c.when), "MMM d, yyyy")}
                      </div>
                    </button>
                  </li>
                ))}
              </ul>
            </CardContent>
          </Card>
          <Card>
            <CardContent className="p-3">
              {selectedCommit ? (
                <>
                  <div className="text-sm text-muted-foreground mb-2">
                    {selectedCommit.sha} · {selectedCommit.authorName}
                  </div>
                  <DiffViewer diffs={commitDiff} className="max-h-[360px] overflow-y-auto" />
                </>
              ) : (
                <div className="text-sm text-muted-foreground py-8 text-center">Select a commit to view diff.</div>
              )}
            </CardContent>
          </Card>
        </div>
      )}
    </div>
  );
};
