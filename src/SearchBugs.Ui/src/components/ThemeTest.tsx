import { useUIStore } from "../stores/global/uiStore";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Sun, Moon, Monitor } from "lucide-react";

/**
 * Simple theme tester component to verify Vite theme integration
 */
export const ThemeTest = () => {
  const { theme, setTheme } = useUIStore();

  const themes = [
    { id: "light", label: "Light", icon: Sun },
    { id: "dark", label: "Dark", icon: Moon },
    { id: "system", label: "System", icon: Monitor },
  ] as const;

  return (
    <Card className="w-full max-w-md">
      <CardHeader>
        <CardTitle>Theme Test</CardTitle>
      </CardHeader>
      <CardContent className="space-y-4">
        <div className="text-sm text-muted-foreground">
          Current theme: <span className="font-medium">{theme}</span>
        </div>

        <div className="grid grid-cols-3 gap-2">
          {themes.map(({ id, label, icon: Icon }) => (
            <Button
              key={id}
              variant={theme === id ? "default" : "outline-solid"}
              size="sm"
              onClick={() => setTheme(id)}
              className="flex items-center gap-2"
            >
              <Icon className="h-3 w-3" />
              {label}
            </Button>
          ))}
        </div>

        <div className="text-xs text-muted-foreground">
          <div>
            HTML class:{" "}
            {document.documentElement.classList.contains("dark")
              ? "dark"
              : "light"}
          </div>
          <div>
            Data theme:{" "}
            {document.documentElement.getAttribute("data-theme") || "none"}
          </div>
        </div>
      </CardContent>
    </Card>
  );
};
