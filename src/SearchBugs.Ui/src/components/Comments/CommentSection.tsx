import React from "react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { CommentItem } from "./CommentItem";
import { CommentForm } from "./CommentForm";
import { MessageSquare } from "lucide-react";
import { LoadingSkeleton } from "@/components/ui/loading";
import { Comment } from "@/lib/api";

interface CommentSectionProps {
  comments?: Comment[];
  isLoading?: boolean;
  onAddComment: (content: string) => Promise<void>;
  isAddingComment?: boolean;
  title?: string;
  className?: string;
  showCommentCount?: boolean;
  maxHeight?: number;
}

export const CommentSection: React.FC<CommentSectionProps> = ({
  comments = [],
  isLoading = false,
  onAddComment,
  isAddingComment = false,
  title = "Comments",
  className = "",
  showCommentCount = true,
  maxHeight = 400,
}) => {
  const commentCount = comments.length;

  return (
    <div className={`space-y-4 ${className}`}>
      {/* Comments Display */}
      <Card className="border-none shadow-xs">
        <CardHeader className="pb-3">
          <CardTitle className="text-lg flex items-center gap-2">
            <MessageSquare className="w-5 h-5" />
            {title}
            {showCommentCount && (
              <span className="text-sm font-normal text-muted-foreground">
                ({commentCount})
              </span>
            )}
          </CardTitle>
        </CardHeader>
        <CardContent>
          {isLoading ? (
            <div className="space-y-4">
              {[...Array(3)].map((_, i) => (
                <div key={i} className="flex gap-4">
                  <LoadingSkeleton variant="avatar" />
                  <div className="flex-1 space-y-2">
                    <LoadingSkeleton variant="text" className="w-1/4" />
                    <LoadingSkeleton variant="text" />
                    <LoadingSkeleton variant="text" className="w-3/4" />
                  </div>
                </div>
              ))}
            </div>
          ) : comments.length === 0 ? (
            <div className="text-center py-8">
              <MessageSquare className="w-12 h-12 text-muted-foreground mx-auto mb-4" />
              <p className="text-muted-foreground">
                No comments yet. Be the first to comment!
              </p>
            </div>
          ) : (
            <div
              className="space-y-4 overflow-y-auto pr-2"
              style={{ maxHeight: `${maxHeight}px` }}
            >
              {comments.map((comment) => (
                <CommentItem key={comment.id} comment={comment} />
              ))}
            </div>
          )}
        </CardContent>
      </Card>

      {/* Comment Form */}
      <CommentForm
        onSubmit={onAddComment}
        isSubmitting={isAddingComment}
        placeholder={`Add a comment for this bug...`}
      />
    </div>
  );
};
