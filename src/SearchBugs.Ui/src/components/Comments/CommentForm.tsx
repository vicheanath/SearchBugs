import React from "react";
import { Button } from "@/components/ui/button";
import { FormTextarea } from "@/components/ui/form";
import { Card, CardContent } from "@/components/ui/card";
import { Send } from "lucide-react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { commentSchema, type CommentFormData } from "@/lib/validations";

interface CommentFormProps {
  onSubmit: (content: string) => Promise<void>;
  isSubmitting?: boolean;
  placeholder?: string;
  maxLength?: number;
  className?: string;
}

export const CommentForm: React.FC<CommentFormProps> = ({
  onSubmit,
  isSubmitting = false,
  placeholder = "Add a comment...",
  maxLength = 2000,
  className = "",
}) => {
  const {
    control,
    handleSubmit,
    reset,
    watch,
    formState: { errors },
  } = useForm<CommentFormData>({
    resolver: zodResolver(commentSchema),
    defaultValues: {
      content: "",
    },
  });

  const content = watch("content");

  const onSubmitForm = async (data: CommentFormData) => {
    if (isSubmitting) return;

    try {
      await onSubmit(data.content.trim());
      reset(); // Clear form on success
    } catch (error) {
      // Error handling is done by the parent component
      console.error("Error submitting comment:", error);
    }
  };

  const isValid = content.trim().length > 0 && content.length <= maxLength;
  const isNearLimit = content.length > maxLength * 0.8;
  const isOverLimit = content.length > maxLength;

  return (
    <Card className={`border-none shadow-xs ${className}`}>
      <CardContent className="pt-6">
        <form onSubmit={handleSubmit(onSubmitForm)} className="space-y-4">
          <FormTextarea
            control={control}
            name="content"
            placeholder={placeholder}
            className="min-h-[100px] resize-none"
            disabled={isSubmitting}
            error={errors.content}
          />

          <div className="flex justify-between items-center">
            <div className="flex items-center gap-2">
              <span
                className={`text-sm ${
                  isOverLimit
                    ? "text-destructive"
                    : isNearLimit
                    ? "text-yellow-600"
                    : "text-muted-foreground"
                }`}
              >
                {content.length}/{maxLength} characters
              </span>
              {isOverLimit && (
                <span className="text-xs text-destructive">
                  Comment is too long
                </span>
              )}
            </div>

            <Button
              type="submit"
              size="sm"
              disabled={!isValid || isSubmitting}
              className="gap-2"
            >
              {isSubmitting ? (
                <>
                  <div className="h-4 w-4 animate-spin rounded-full border-2 border-current border-t-transparent" />
                  Adding...
                </>
              ) : (
                <>
                  <Send className="h-4 w-4" />
                  Add Comment
                </>
              )}
            </Button>
          </div>
        </form>
      </CardContent>
    </Card>
  );
};
