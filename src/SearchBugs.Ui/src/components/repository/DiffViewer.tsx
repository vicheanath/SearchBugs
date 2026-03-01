import React from "react";
import { FileDiff } from "@/lib/api";
import { cn } from "@/lib/utils";

interface DiffViewerProps {
  diffs: FileDiff[];
  className?: string;
}

export const DiffViewer: React.FC<DiffViewerProps> = ({ diffs, className }) => {
  if (!diffs?.length) {
    return (
      <div className={cn("rounded border bg-muted/30 p-4 text-sm text-muted-foreground", className)}>
        No changes to show.
      </div>
    );
  }

  return (
    <div className={cn("space-y-4", className)}>
      {diffs.map((file, idx) => (
        <div key={idx} className="rounded border overflow-hidden">
          <div className="bg-muted px-3 py-2 text-sm font-medium">
            {file.filePath}
            {file.oldPath !== file.filePath && file.oldPath && (
              <span className="text-muted-foreground ml-2">(was {file.oldPath})</span>
            )}
            <span className="ml-2 text-muted-foreground">{file.status}</span>
          </div>
          <pre className="overflow-x-auto p-0 text-xs bg-background border-t">
            <code>
              {file.patch
                ? file.patch.split("\n").map((line, i) => (
                    <div
                      key={i}
                      className={cn(
                        "px-3 py-0.5 font-mono whitespace-pre",
                        line.startsWith("+") && "bg-green-500/10 text-green-800 dark:text-green-300",
                        line.startsWith("-") && "bg-red-500/10 text-red-800 dark:text-red-300"
                      )}
                    >
                      {line || " "}
                    </div>
                  ))
                : "(binary or no patch)"}
            </code>
          </pre>
        </div>
      ))}
    </div>
  );
};
