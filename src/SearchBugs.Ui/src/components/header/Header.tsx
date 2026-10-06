import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Plus,
  Search,
  LogOut,
  Sun,
  Moon,
  Monitor,
  Menu,
  X,
} from "lucide-react";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import {
  Sheet,
  SheetContent,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
  SheetClose,
} from "@/components/ui/sheet";
import { useNavigate } from "react-router-dom";
import { useAuth } from "@/hooks/useAuth";
import { useToast } from "@/components/ui/use-toast";
import { NotificationBell } from "@/components/NotificationBell";
import { ImpersonationDialog } from "@/components/ImpersonationDialog";
import { TimezoneIndicator } from "@/components/ui/timezone-indicator";
import { useUIStore } from "../../stores/global/uiStore";
import { useState } from "react";

export const Header = () => {
  const navigate = useNavigate();
  const { user, logout } = useAuth();
  const { toast } = useToast();
  const { theme, setTheme } = useUIStore();
  const [isSearchVisible, setIsSearchVisible] = useState(false);

  const handleLogout = () => {
    logout();
    toast({
      title: "Logged out",
      description: "You have been successfully logged out.",
    });
    navigate("/login");
  };

  const toggleTheme = () => {
    if (theme === "light") {
      setTheme("dark");
    } else if (theme === "dark") {
      setTheme("system");
    } else {
      setTheme("light");
    }
  };

  const getThemeIcon = () => {
    if (theme === "dark") return <Moon className="h-4 w-4" />;
    if (theme === "light") return <Sun className="h-4 w-4" />;
    return <Monitor className="h-4 w-4" />; // System theme
  };

  const MobileNavigation = () => (
    <Sheet>
      <SheetTrigger asChild>
        <Button
          variant="ghost"
          size="icon"
          className="lg:hidden"
          aria-label="Open menu"
        >
          <Menu className="h-5 w-5" />
        </Button>
      </SheetTrigger>
      <SheetContent side="right" className="w-[300px] sm:w-[350px]">
        <SheetHeader>
          <SheetTitle>Navigation</SheetTitle>
        </SheetHeader>
        <div className="mt-6 flex flex-col space-y-3">
          <SheetClose asChild>
            <Button
              variant="ghost"
              className="w-full justify-start"
              onClick={() => navigate("/projects")}
            >
              Projects
            </Button>
          </SheetClose>
          <SheetClose asChild>
            <Button
              variant="ghost"
              className="w-full justify-start"
              onClick={() => navigate("/repositories")}
            >
              Repositories
            </Button>
          </SheetClose>
          <SheetClose asChild>
            <Button
              variant="ghost"
              className="w-full justify-start"
              onClick={() => navigate("/bugs")}
            >
              Bugs
            </Button>
          </SheetClose>
          <SheetClose asChild>
            <Button
              variant="ghost"
              className="w-full justify-start"
              onClick={() => navigate("/users")}
            >
              Users
            </Button>
          </SheetClose>

          <div className="h-px bg-border my-4" />

          <SheetClose asChild>
            <Button
              variant="ghost"
              className="w-full justify-start"
              onClick={() => navigate("/projects/create")}
            >
              <Plus className="mr-2 h-4 w-4" />
              New Project
            </Button>
          </SheetClose>
          <SheetClose asChild>
            <Button
              variant="ghost"
              className="w-full justify-start"
              onClick={() => navigate("/repositories/create")}
            >
              <Plus className="mr-2 h-4 w-4" />
              New Repository
            </Button>
          </SheetClose>
          <SheetClose asChild>
            <Button
              variant="ghost"
              className="w-full justify-start"
              onClick={() => navigate("/add-bug")}
            >
              <Plus className="mr-2 h-4 w-4" />
              Report Bug
            </Button>
          </SheetClose>
        </div>
      </SheetContent>
    </Sheet>
  );

  return (
    <header className="bg-card border-b shadow-xs sticky top-0 z-40">
      <div className="container mx-auto flex items-center justify-between p-3 sm:p-4">
        {/* Logo and Search - Left Section */}
        <div className="flex items-center space-x-3 sm:space-x-4 flex-1 min-w-0">
          <div
            className="flex items-center space-x-2 cursor-pointer shrink-0"
            onClick={() => navigate("/")}
          >
            <div className="h-7 w-7 sm:h-8 sm:w-8 bg-primary rounded-lg flex items-center justify-center">
              <span className="text-primary-foreground font-bold text-xs sm:text-sm">
                SB
              </span>
            </div>
            <span className="font-semibold text-base sm:text-lg hidden sm:block">
              SearchBugs
            </span>
          </div>

          {/* Desktop Search */}
          <div className="relative hidden md:flex flex-1 max-w-md">
            <Search className="absolute left-3 top-2.5 h-4 w-4 text-muted-foreground" />
            <Input
              className="pl-10 pr-4"
              placeholder="Search bugs, projects..."
            />
          </div>

          {/* Mobile Search Toggle */}
          <Button
            variant="ghost"
            size="icon"
            className="md:hidden shrink-0"
            onClick={() => setIsSearchVisible(!isSearchVisible)}
            aria-label="Toggle search"
          >
            <Search className="h-4 w-4" />
          </Button>
        </div>

        {/* Desktop Navigation - Center Section */}
        <nav className="hidden lg:flex lg:items-center lg:space-x-1 xl:space-x-2">
          <Button
            variant="ghost"
            size="sm"
            onClick={() => navigate("/projects")}
          >
            Projects
          </Button>
          <Button
            variant="ghost"
            size="sm"
            onClick={() => navigate("/repositories")}
          >
            Repositories
          </Button>
          <Button variant="ghost" size="sm" onClick={() => navigate("/bugs")}>
            Bugs
          </Button>
          <Button variant="ghost" size="sm" onClick={() => navigate("/users")}>
            Users
          </Button>
        </nav>

        {/* User Actions - Right Section */}
        <div className="flex items-center space-x-1 sm:space-x-2 shrink-0">
          {/* Hide timezone on small screens */}
          <div className="hidden sm:block">
            <TimezoneIndicator />
          </div>

          <NotificationBell />

          {/* Theme Toggle */}
          <Button
            variant="ghost"
            size="icon"
            onClick={toggleTheme}
            className="hidden sm:flex"
            title={`Switch to ${
              theme === "light" ? "dark" : theme === "dark" ? "system" : "light"
            } theme`}
          >
            {getThemeIcon()}
          </Button>

          {/* Show impersonation dialog for admin users - TODO: Add proper permission check */}
          {user?.roles?.some((role) => role.name === "Admin") && (
            <div className="hidden sm:block">
              <ImpersonationDialog />
            </div>
          )}

          {/* Desktop Create Menu */}
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button size="icon" variant="ghost" className="hidden sm:flex">
                <Plus className="h-4 w-4" />
                <span className="sr-only">Create</span>
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuItem onClick={() => navigate("/projects/create")}>
                New Project
              </DropdownMenuItem>
              <DropdownMenuItem
                onClick={() => navigate("/repositories/create")}
              >
                New Repository
              </DropdownMenuItem>
              <DropdownMenuItem onClick={() => navigate("/add-bug")}>
                Report Bug
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>

          {/* Mobile Navigation Menu */}
          <MobileNavigation />

          {/* User Menu */}
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Avatar className="h-7 w-7 sm:h-8 sm:w-8 cursor-pointer">
                <AvatarFallback className="bg-primary text-primary-foreground text-xs sm:text-sm">
                  {user?.firstName?.[0] || user?.email?.[0] || "U"}
                </AvatarFallback>
              </Avatar>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end" className="w-48">
              <DropdownMenuLabel>
                <div className="flex flex-col space-y-1">
                  <p className="text-sm font-medium line-clamp-1">
                    {user?.firstName} {user?.lastName}
                  </p>
                  <p className="text-xs text-muted-foreground line-clamp-1">
                    {user?.email}
                  </p>
                </div>
              </DropdownMenuLabel>
              <DropdownMenuSeparator />
              <DropdownMenuItem onClick={() => navigate("/profile")}>
                Profile
              </DropdownMenuItem>
              <DropdownMenuItem onClick={() => navigate("/settings")}>
                Settings
              </DropdownMenuItem>

              {/* Mobile-only menu items */}
              <div className="sm:hidden">
                <DropdownMenuSeparator />
                <DropdownMenuItem onClick={toggleTheme}>
                  <div className="flex items-center w-full">
                    {getThemeIcon()}
                    <span className="ml-2">
                      {theme === "light"
                        ? "Dark"
                        : theme === "dark"
                        ? "System"
                        : "Light"}{" "}
                      Theme
                    </span>
                  </div>
                </DropdownMenuItem>
                {user?.roles?.some((role) => role.name === "Admin") && (
                  <DropdownMenuItem>
                    <ImpersonationDialog />
                  </DropdownMenuItem>
                )}
              </div>

              <DropdownMenuSeparator />
              <DropdownMenuItem
                onClick={handleLogout}
                className="text-destructive"
              >
                <LogOut className="mr-2 h-4 w-4" />
                Log out
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      </div>

      {/* Mobile Search Bar */}
      {isSearchVisible && (
        <div className="border-t bg-background/95 backdrop-blur-sm supports-backdrop-filter:bg-background/60 md:hidden">
          <div className="container mx-auto p-3">
            <div className="relative">
              <Search className="absolute left-3 top-2.5 h-4 w-4 text-muted-foreground" />
              <Input
                className="pl-10 pr-10"
                placeholder="Search bugs, projects..."
                autoFocus
              />
              <Button
                variant="ghost"
                size="icon"
                className="absolute right-1 top-1 h-7 w-7"
                onClick={() => setIsSearchVisible(false)}
                aria-label="Close search"
              >
                <X className="h-4 w-4" />
              </Button>
            </div>
          </div>
        </div>
      )}
    </header>
  );
};
