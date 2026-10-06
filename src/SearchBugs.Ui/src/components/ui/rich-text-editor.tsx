import * as React from "react";
import { cn } from "@/lib/utils";
import { Button } from "./button";
import ReactMarkdown from "react-markdown";
import remarkGfm from "remark-gfm";
import {
  Bold,
  Italic,
  Underline,
  List,
  ListOrdered,
  Code,
  Code2,
  Link2,
  Quote,
  Undo,
  Redo,
  Eye,
  Edit3,
} from "lucide-react";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "./tabs";

export interface RichTextEditorProps {
  value?: string;
  onChange?: (value: string) => void;
  placeholder?: string;
  className?: string;
  disabled?: boolean;
  maxLength?: number;
}

interface FormatAction {
  before: string;
  after: string;
  placeholder?: string;
}

const RichTextEditor = React.forwardRef<
  HTMLTextAreaElement,
  RichTextEditorProps
>(
  (
    {
      className,
      value = "",
      onChange,
      placeholder,
      disabled,
      maxLength = 10000,
      ...props
    },
    ref
  ) => {
    const textareaRef = React.useRef<HTMLTextAreaElement>(null);
    const [activeTab, setActiveTab] = React.useState<"write" | "preview">(
      "write"
    );
    const [undoStack, setUndoStack] = React.useState<string[]>([]);
    const [redoStack, setRedoStack] = React.useState<string[]>([]);

    React.useImperativeHandle(ref, () => textareaRef.current!);

    const handleChange = (newValue: string) => {
      if (maxLength && newValue.length > maxLength) return;

      // Add to undo stack
      if (value !== newValue) {
        setUndoStack((prev) => [...prev.slice(-19), value]); // Keep last 20 states
        setRedoStack([]); // Clear redo stack on new change
      }

      onChange?.(newValue);
    };

    const undo = () => {
      if (undoStack.length > 0) {
        const previousValue = undoStack[undoStack.length - 1];
        setRedoStack((prev) => [value, ...prev.slice(0, 19)]);
        setUndoStack((prev) => prev.slice(0, -1));
        onChange?.(previousValue);
      }
    };

    const redo = () => {
      if (redoStack.length > 0) {
        const nextValue = redoStack[0];
        setUndoStack((prev) => [...prev.slice(-19), value]);
        setRedoStack((prev) => prev.slice(1));
        onChange?.(nextValue);
      }
    };

    const insertFormat = ({
      before,
      after,
      placeholder: ph = "text",
    }: FormatAction) => {
      const textarea = textareaRef.current;
      if (!textarea) return;

      const start = textarea.selectionStart;
      const end = textarea.selectionEnd;
      const selectedText = value.substring(start, end);
      const textToInsert = selectedText || ph;

      const newText =
        value.substring(0, start) +
        before +
        textToInsert +
        after +
        value.substring(end);

      handleChange(newText);

      // Set cursor position after formatting
      setTimeout(() => {
        const newStart = start + before.length;
        const newEnd = newStart + textToInsert.length;
        textarea.setSelectionRange(newStart, newEnd);
        textarea.focus();
      }, 0);
    };

    const insertListItem = (ordered = false) => {
      const textarea = textareaRef.current;
      if (!textarea) return;

      const start = textarea.selectionStart;

      const prefix = ordered ? "1. " : "- ";
      const newText =
        value.substring(0, start) + "\n" + prefix + value.substring(start);

      handleChange(newText);

      setTimeout(() => {
        textarea.setSelectionRange(
          start + prefix.length + 1,
          start + prefix.length + 1
        );
        textarea.focus();
      }, 0);
    };

    const formatActions = [
      {
        icon: Bold,
        label: "Bold (Ctrl+B)",
        action: () =>
          insertFormat({ before: "**", after: "**", placeholder: "bold text" }),
      },
      {
        icon: Italic,
        label: "Italic (Ctrl+I)",
        action: () =>
          insertFormat({ before: "*", after: "*", placeholder: "italic text" }),
      },
      {
        icon: Underline,
        label: "Strikethrough",
        action: () =>
          insertFormat({
            before: "~~",
            after: "~~",
            placeholder: "strikethrough text",
          }),
      },
      {
        icon: Code,
        label: "Inline Code",
        action: () =>
          insertFormat({ before: "`", after: "`", placeholder: "code" }),
      },
      {
        icon: Code2,
        label: "Code Block",
        action: () =>
          insertFormat({
            before: "```\n",
            after: "\n```",
            placeholder: "// Your code here",
          }),
      },
      {
        icon: List,
        label: "Bullet List",
        action: () => insertListItem(false),
      },
      {
        icon: ListOrdered,
        label: "Numbered List",
        action: () => insertListItem(true),
      },
      {
        icon: Quote,
        label: "Blockquote",
        action: () =>
          insertFormat({ before: "> ", after: "", placeholder: "quote" }),
      },
      {
        icon: Link2,
        label: "Link",
        action: () =>
          insertFormat({
            before: "[",
            after: "](https://)",
            placeholder: "link text",
          }),
      },
    ];

    return (
      <div className={cn("border border-input rounded-md", className)}>
        <div className="border-b border-input">
          <Tabs
            value={activeTab}
            onValueChange={(v) => setActiveTab(v as "write" | "preview")}
          >
            <div className="flex items-center justify-between px-3 py-2">
              <TabsList className="grid w-fit grid-cols-2 h-8">
                <TabsTrigger
                  value="write"
                  className="flex items-center gap-1 text-xs"
                >
                  <Edit3 className="h-3 w-3" />
                  Write
                </TabsTrigger>
                <TabsTrigger
                  value="preview"
                  className="flex items-center gap-1 text-xs"
                >
                  <Eye className="h-3 w-3" />
                  Preview
                </TabsTrigger>
              </TabsList>

              <div className="flex items-center gap-1">
                <Button
                  variant="ghost"
                  size="sm"
                  className="h-7 w-7 p-0"
                  onClick={undo}
                  disabled={undoStack.length === 0 || disabled}
                  title="Undo (Ctrl+Z)"
                >
                  <Undo className="h-3 w-3" />
                </Button>
                <Button
                  variant="ghost"
                  size="sm"
                  className="h-7 w-7 p-0"
                  onClick={redo}
                  disabled={redoStack.length === 0 || disabled}
                  title="Redo (Ctrl+Y)"
                >
                  <Redo className="h-3 w-3" />
                </Button>
              </div>
            </div>

            <TabsContent value="write" className="m-0">
              <div className="border-b border-input p-2">
                <div className="flex items-center gap-1 flex-wrap">
                  {formatActions.map((action, index) => (
                    <React.Fragment key={action.label}>
                      <Button
                        variant="ghost"
                        size="sm"
                        className="h-7 w-7 p-0"
                        onClick={action.action}
                        disabled={disabled}
                        title={action.label}
                      >
                        <action.icon className="h-3 w-3" />
                      </Button>
                      {(index === 2 || index === 4 || index === 6) && (
                        <div className="w-px h-4 bg-border mx-1" />
                      )}
                    </React.Fragment>
                  ))}
                </div>
              </div>

              <textarea
                ref={textareaRef}
                className={cn(
                  "w-full min-h-[200px] p-3 bg-transparent text-sm resize-none outline-hidden placeholder:text-muted-foreground",
                  "focus:ring-0 focus:outline-hidden"
                )}
                value={value}
                onChange={(e) => handleChange(e.target.value)}
                placeholder={placeholder}
                disabled={disabled}
                onKeyDown={(e) => {
                  if (e.ctrlKey || e.metaKey) {
                    if (e.key === "z" && !e.shiftKey) {
                      e.preventDefault();
                      undo();
                    } else if (e.key === "y" || (e.key === "z" && e.shiftKey)) {
                      e.preventDefault();
                      redo();
                    } else if (e.key === "b") {
                      e.preventDefault();
                      insertFormat({
                        before: "**",
                        after: "**",
                        placeholder: "bold text",
                      });
                    } else if (e.key === "i") {
                      e.preventDefault();
                      insertFormat({
                        before: "*",
                        after: "*",
                        placeholder: "italic text",
                      });
                    }
                  }
                }}
                {...props}
              />
            </TabsContent>

            <TabsContent value="preview" className="m-0">
              <div className="min-h-[200px] p-3 text-sm prose prose-sm max-w-none">
                {value.trim() ? (
                  <ReactMarkdown remarkPlugins={[remarkGfm]}>
                    {value}
                  </ReactMarkdown>
                ) : (
                  <span className="text-muted-foreground">
                    Nothing to preview
                  </span>
                )}
              </div>
            </TabsContent>
          </Tabs>
        </div>

        {maxLength && (
          <div className="px-3 py-2 text-xs text-muted-foreground flex justify-between items-center bg-muted/20">
            <span>
              Markdown supported: **bold**, *italic*, `code`, [links](url),{" "}
              {">"}quotes
            </span>
            <span
              className={cn(
                value.length > maxLength * 0.9 ? "text-orange-600" : "",
                value.length >= maxLength ? "text-red-600" : ""
              )}
            >
              {value.length}/{maxLength}
            </span>
          </div>
        )}
      </div>
    );
  }
);

RichTextEditor.displayName = "RichTextEditor";

export { RichTextEditor };
