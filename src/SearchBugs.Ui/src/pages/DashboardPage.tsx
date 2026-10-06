import React from "react";
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import {
  FolderKanban,
  Bug,
  AlertTriangle,
  Activity,
  TrendingUp,
} from "lucide-react";
import { useApi } from "@/hooks/useApi";
import { useAuth } from "@/hooks/useAuth";
import { useNavigate } from "react-router-dom";
import { safeParseDateForSort } from "@/lib/date-utils";
import { useTimezone } from "@/hooks/useTimezone";

interface Project {
  id: string;
  name: string;
  description: string;
  createdOnUtc: string;
  updatedOnUtc: string;
}

interface Bug {
  id: string;
  title: string;
  status: string;
  priority: string;
  severity: string;
  createdOnUtc: string;
}

export const DashboardPage: React.FC = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const { formatDistanceToNow } = useTimezone();
  const { data: projects, isLoading: projectsLoading } =
    useApi<Project[]>("projects");
  const { data: bugs, isLoading: bugsLoading } = useApi<Bug[]>("bugs");

  const projectsList = projects?.value || [];
  const bugsList = bugs?.value || [];

  // Calculate stats
  const totalProjects = projectsList.length;
  const totalBugs = bugsList.length;
  const openBugs = bugsList.filter(
    (bug) => bug.status === "New" || bug.status === "InProgress"
  ).length;
  const criticalBugs = bugsList.filter(
    (bug) => bug.priority === "Critical" || bug.severity === "Critical"
  ).length;

  // Recent activity (last 5 bugs)
  const recentBugs = bugsList
    .sort(
      (a, b) =>
        safeParseDateForSort(b.createdOnUtc) -
        safeParseDateForSort(a.createdOnUtc)
    )
    .slice(0, 5);

  return (
    <main className="container mx-auto py-6 space-y-6 max-w-7xl">
      {/* Header */}
      <div className="flex items-center justify-between bg-card p-6 rounded-lg shadow-xs">
        <div className="flex items-center space-x-4">
          <Avatar className="h-16 w-16">
            <AvatarFallback className="bg-primary text-primary-foreground text-lg">
              {user?.firstName?.[0] || user?.email?.[0] || "U"}
            </AvatarFallback>
          </Avatar>
          <div>
            <h1 className="text-2xl font-bold">
              Welcome back, {user?.firstName || "User"}!
            </h1>
            <p className="text-muted-foreground">
              Here's what's happening with your projects
            </p>
          </div>
        </div>
        <Button onClick={() => navigate("/add-bug")} className="gap-2">
          <Bug className="h-4 w-4" />
          Report Bug
        </Button>
      </div>

      {/* Stats Cards */}
      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
        <Card className="hover:shadow-md transition-shadow">
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">
              Total Projects
            </CardTitle>
            <FolderKanban className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">{totalProjects}</div>
            <p className="text-xs text-muted-foreground">Active projects</p>
          </CardContent>
        </Card>

        <Card className="hover:shadow-md transition-shadow">
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Total Bugs</CardTitle>
            <Bug className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">{totalBugs}</div>
            <p className="text-xs text-muted-foreground">All reported bugs</p>
          </CardContent>
        </Card>

        <Card className="hover:shadow-md transition-shadow">
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Open Bugs</CardTitle>
            <Activity className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">{openBugs}</div>
            <p className="text-xs text-muted-foreground">Need attention</p>
          </CardContent>
        </Card>

        <Card className="hover:shadow-md transition-shadow">
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">
              Critical Issues
            </CardTitle>
            <AlertTriangle className="h-4 w-4 text-destructive" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold text-destructive">
              {criticalBugs}
            </div>
            <p className="text-xs text-muted-foreground">High priority</p>
          </CardContent>
        </Card>
      </div>

      <div className="grid gap-6 md:grid-cols-2">
        {/* Recent Activity */}
        <Card className="col-span-1">
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Activity className="h-5 w-5" />
              Recent Bugs
            </CardTitle>
          </CardHeader>
          <CardContent>
            {bugsLoading ? (
              <div className="space-y-3">
                {[...Array(5)].map((_, i) => (
                  <div key={i} className="animate-pulse flex space-x-4">
                    <div className="rounded-full bg-muted h-8 w-8"></div>
                    <div className="flex-1 space-y-2">
                      <div className="h-4 bg-muted rounded w-3/4"></div>
                      <div className="h-3 bg-muted rounded w-1/2"></div>
                    </div>
                  </div>
                ))}
              </div>
            ) : recentBugs.length === 0 ? (
              <div className="text-center py-8">
                <Bug className="h-12 w-12 text-muted-foreground mx-auto mb-2" />
                <p className="text-muted-foreground">No bugs reported yet</p>
                <Button
                  variant="outline"
                  size="sm"
                  className="mt-2"
                  onClick={() => navigate("/add-bug")}
                >
                  Report First Bug
                </Button>
              </div>
            ) : (
              <ul className="space-y-4">
                {recentBugs.map((bug) => (
                  <li
                    key={bug.id}
                    className="flex items-start space-x-3 p-3 hover:bg-muted/50 rounded-lg transition-colors cursor-pointer"
                    onClick={() => navigate(`/bugs/${bug.id}`)}
                  >
                    <div className="mt-1">
                      <Bug className="h-4 w-4 text-muted-foreground" />
                    </div>
                    <div className="flex-1 min-w-0">
                      <p className="text-sm font-medium truncate">
                        {bug.title}
                      </p>
                      <div className="flex items-center gap-2 mt-1">
                        <Badge
                          variant={
                            bug.status === "New"
                              ? "default"
                              : bug.status === "InProgress"
                              ? "secondary"
                              : "outline-solid"
                          }
                          className="text-xs"
                        >
                          {bug.status}
                        </Badge>
                        <Badge
                          variant={
                            bug.priority === "Critical"
                              ? "destructive"
                              : bug.priority === "High"
                              ? "default"
                              : "outline-solid"
                          }
                          className="text-xs"
                        >
                          {bug.priority}
                        </Badge>
                      </div>
                      <p className="text-xs text-muted-foreground mt-1">
                        {formatDistanceToNow(bug.createdOnUtc)}
                      </p>
                    </div>
                  </li>
                ))}
              </ul>
            )}
          </CardContent>
        </Card>

        {/* Recent Projects */}
        <Card className="col-span-1">
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <FolderKanban className="h-5 w-5" />
              Recent Projects
            </CardTitle>
          </CardHeader>
          <CardContent>
            {projectsLoading ? (
              <div className="space-y-3">
                {[...Array(5)].map((_, i) => (
                  <div key={i} className="animate-pulse space-y-2">
                    <div className="h-4 bg-muted rounded w-3/4"></div>
                    <div className="h-3 bg-muted rounded w-full"></div>
                    <div className="h-3 bg-muted rounded w-1/2"></div>
                  </div>
                ))}
              </div>
            ) : projectsList.length === 0 ? (
              <div className="text-center py-8">
                <FolderKanban className="h-12 w-12 text-muted-foreground mx-auto mb-2" />
                <p className="text-muted-foreground">No projects created yet</p>
                <Button
                  variant="outline"
                  size="sm"
                  className="mt-2"
                  onClick={() => navigate("/projects/create")}
                >
                  Create First Project
                </Button>
              </div>
            ) : (
              <ul className="space-y-4">
                {projectsList.slice(0, 5).map((project) => (
                  <li
                    key={project.id}
                    className="p-3 hover:bg-muted/50 rounded-lg transition-colors cursor-pointer"
                    onClick={() => navigate(`/projects/${project.id}`)}
                  >
                    <div className="flex items-start justify-between">
                      <div className="flex items-center gap-2">
                        <FolderKanban className="h-4 w-4 text-muted-foreground" />
                        <div>
                          <h3 className="text-sm font-medium">
                            {project.name}
                          </h3>
                          <p className="text-xs text-muted-foreground line-clamp-2">
                            {project.description || "No description"}
                          </p>
                        </div>
                      </div>
                      <span className="text-xs text-muted-foreground">
                        {formatDistanceToNow(project.updatedOnUtc)}
                      </span>
                    </div>
                  </li>
                ))}
              </ul>
            )}
          </CardContent>
        </Card>
      </div>

      {/* Quick Actions */}
      <Card>
        <CardHeader>
          <CardTitle className="flex items-center gap-2">
            <TrendingUp className="h-5 w-5" />
            Quick Actions
          </CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid gap-4 md:grid-cols-3">
            <Button
              variant="outline"
              className="h-auto p-4 flex flex-col gap-2"
              onClick={() => navigate("/projects/create")}
            >
              <FolderKanban className="h-6 w-6" />
              <span>Create Project</span>
            </Button>
            <Button
              variant="outline"
              className="h-auto p-4 flex flex-col gap-2"
              onClick={() => navigate("/add-bug")}
            >
              <Bug className="h-6 w-6" />
              <span>Report Bug</span>
            </Button>
            <Button
              variant="outline"
              className="h-auto p-4 flex flex-col gap-2"
              onClick={() => navigate("/bugs")}
            >
              <Activity className="h-6 w-6" />
              <span>View All Bugs</span>
            </Button>
          </div>
        </CardContent>
      </Card>
    </main>
  );
};
