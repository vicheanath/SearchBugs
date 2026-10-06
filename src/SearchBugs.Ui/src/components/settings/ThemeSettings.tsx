import { useState, useEffect } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { useUIStore } from "../../stores/global/uiStore";
import { getSystemTheme, type Theme } from "../../lib/theme";
import { Monitor, Moon, Sun, Palette, Check } from "lucide-react";

export const ThemeSettings = () => {
  const { theme, setTheme } = useUIStore();
  const [systemTheme, setSystemTheme] = useState<"light" | "dark">("light");

  // Listen to system theme changes
  useEffect(() => {
    const systemTheme = getSystemTheme();
    setSystemTheme(systemTheme);

    const mediaQuery = window.matchMedia("(prefers-color-scheme: dark)");
    const handleChange = (e: MediaQueryListEvent) => {
      const newSystemTheme = e.matches ? "dark" : "light";
      setSystemTheme(newSystemTheme);
      // If user is using system theme, the theme utilities will handle the update
    };

    mediaQuery.addEventListener("change", handleChange);
    return () => mediaQuery.removeEventListener("change", handleChange);
  }, []);

  const themeOptions = [
    {
      id: "light" as Theme,
      name: "Light",
      description: "Always use light mode",
      icon: Sun,
      preview: "bg-white border-gray-200",
    },
    {
      id: "dark" as Theme,
      name: "Dark",
      description: "Always use dark mode",
      icon: Moon,
      preview: "bg-gray-900 border-gray-700",
    },
    {
      id: "system" as Theme,
      name: "System",
      description: "Use system preference",
      icon: Monitor,
      preview:
        systemTheme === "dark"
          ? "bg-gray-900 border-gray-700"
          : "bg-white border-gray-200",
    },
  ];

  const handleThemeChange = (newTheme: Theme) => {
    setTheme(newTheme); // This will use the new Vite-friendly utilities
  };

  const getCurrentThemeDisplay = () => {
    if (theme === "system") {
      return `System (${systemTheme})`;
    }
    return theme.charAt(0).toUpperCase() + theme.slice(1);
  };

  return (
    <Card>
      <CardHeader>
        <CardTitle className="flex items-center gap-2">
          <Palette className="h-5 w-5" />
          Appearance
        </CardTitle>
      </CardHeader>
      <CardContent className="space-y-6">
        {/* Current Theme Status */}
        <div className="flex items-center justify-between p-3 border rounded-lg bg-muted/50">
          <div>
            <p className="font-medium">Current Theme</p>
            <p className="text-sm text-muted-foreground">
              {getCurrentThemeDisplay()}
            </p>
          </div>
          <Badge variant="secondary" className="flex items-center gap-1">
            {theme === "light" && <Sun className="h-3 w-3" />}
            {theme === "dark" && <Moon className="h-3 w-3" />}
            {theme === "system" && <Monitor className="h-3 w-3" />}
            Active
          </Badge>
        </div>

        {/* Theme Options */}
        <div>
          <h4 className="text-sm font-medium mb-3">Choose Theme</h4>
          <div className="grid gap-3">
            {themeOptions.map((option) => {
              const IconComponent = option.icon;
              const isSelected = theme === option.id;

              return (
                <button
                  key={option.id}
                  onClick={() => handleThemeChange(option.id)}
                  className={`flex items-center gap-4 p-4 rounded-lg border transition-all hover:bg-muted/50 ${
                    isSelected
                      ? "border-primary bg-primary/5"
                      : "border-border hover:border-primary/50"
                  }`}
                >
                  {/* Theme Preview */}
                  <div
                    className={`w-12 h-8 rounded border-2 ${option.preview} shrink-0`}
                  >
                    <div className="w-full h-full rounded flex items-center justify-center">
                      <div
                        className={`w-2 h-2 rounded-full ${
                          option.id === "light"
                            ? "bg-yellow-400"
                            : option.id === "dark"
                            ? "bg-blue-400"
                            : "bg-linear-to-r from-yellow-400 to-blue-400"
                        }`}
                      />
                    </div>
                  </div>

                  {/* Theme Info */}
                  <div className="flex-1 text-left">
                    <div className="flex items-center gap-2">
                      <IconComponent className="h-4 w-4" />
                      <span className="font-medium">{option.name}</span>
                    </div>
                    <p className="text-sm text-muted-foreground mt-1">
                      {option.description}
                    </p>
                  </div>

                  {/* Selection Indicator */}
                  {isSelected && (
                    <div className="shrink-0">
                      <div className="w-5 h-5 bg-primary rounded-full flex items-center justify-center">
                        <Check className="h-3 w-3 text-primary-foreground" />
                      </div>
                    </div>
                  )}
                </button>
              );
            })}
          </div>
        </div>

        {/* Additional Options */}
        <div>
          <h4 className="text-sm font-medium mb-3">Additional Options</h4>
          <div className="space-y-3">
            <div className="flex items-center justify-between p-3 border rounded-lg">
              <div>
                <p className="font-medium">Reduce motion</p>
                <p className="text-sm text-muted-foreground">
                  Minimize animations and transitions
                </p>
              </div>
              <Button variant="outline" size="sm">
                Configure
              </Button>
            </div>
            <div className="flex items-center justify-between p-3 border rounded-lg">
              <div>
                <p className="font-medium">High contrast</p>
                <p className="text-sm text-muted-foreground">
                  Increase color contrast for better accessibility
                </p>
              </div>
              <Button variant="outline" size="sm">
                Configure
              </Button>
            </div>
          </div>
        </div>

        {/* System Theme Info */}
        {theme === "system" && (
          <div className="p-3 bg-blue-50 dark:bg-blue-950/30 border border-blue-200 dark:border-blue-800 rounded-lg">
            <div className="flex items-start gap-2">
              <Monitor className="h-4 w-4 text-blue-600 dark:text-blue-400 mt-0.5" />
              <div>
                <p className="text-sm font-medium text-blue-800 dark:text-blue-200">
                  System theme active
                </p>
                <p className="text-xs text-blue-600 dark:text-blue-300 mt-1">
                  Your theme will automatically switch based on your system's
                  appearance settings. Currently showing:{" "}
                  <span className="font-medium">{systemTheme} mode</span>
                </p>
              </div>
            </div>
          </div>
        )}
      </CardContent>
    </Card>
  );
};
