import { Bug, FolderGit, GitGraph, Home } from "lucide-react";

import { Badge } from "@/components/ui/badge";
import { NavLink } from "react-router-dom";

class SideBarModel {
  name: string;
  icon: React.ReactNode;
  url: string;
  badge: number;

  constructor(name: string, icon: React.ReactNode, url: string, badge: number = 0) {
    this.name = name;
    this.icon = icon;
    this.badge = badge;
    this.url = url;
  }
}

export const Sidebar = () => {
  const sidebar: SideBarModel[] = [
    new SideBarModel("Dashboard", <Home className="h-4 w-4" />, "/", 0),
    new SideBarModel("Projects", <FolderGit className="h-4 w-4" />, "/projects", 0),
    new SideBarModel("Bugs", <Bug className="h-4 w-4" />, "/bugs", 0),
    new SideBarModel(
      "Repository",
      <GitGraph className="h-4 w-4" />,
      "/repositories",
      0
    ),
  ];

  return (
    <nav className="grid items-start px-2 text-sm font-medium lg:px-4">
      {sidebar.map((item, index) => (
        <NavLink
          key={index}
          style={({ isActive }) => ({
            color: isActive ? "hsl(var(--primary))" : "hsl(var(--muted-foreground))",
            backgroundColor: isActive ? "hsl(var(--muted))" : "transparent",
          })}
          to={item.url}
          className="flex items-center gap-3 rounded-lg  px-3 py-2 transition-all hover:text-primary"
        >
          {item.icon}
          {item.name}
          {item.badge > 0 && (
            <Badge className="ml-auto flex h-6 w-6 shrink-0 items-center justify-center rounded-full">
              {item.badge}
            </Badge>
          )}
        </NavLink>
      ))}
    </nav>
  );
};
