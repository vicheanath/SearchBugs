import React from "react";
import { RecentActivitySmall } from "@/components/profile/RecentActivitySmall";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import {
  TrendingUp,
  Users,
  Bug,
  CheckCircle2,
  Activity,
  Clock,
} from "lucide-react";

interface DashboardProps {
  user?: {
    firstName: string;
    lastName: string;
    email: string;
    avatarUrl?: string;
  };
}

export const Dashboard: React.FC<DashboardProps> = ({ user }) => {
  const handleViewAllActivity = () => {
    // Navigate to full activity page
    console.log("Navigate to activity page");
  };

  return (
    <div className="container mx-auto p-6 space-y-6">
      {/* Welcome Header */}
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold">
            Welcome back{user ? `, ${user.firstName}` : ""}!
          </h1>
          <p className="text-muted-foreground">
            Here's what's happening with your projects today.
          </p>
        </div>
        {user && (
          <Avatar className="h-10 w-10">
            <AvatarImage src={user.avatarUrl} alt={user.firstName} />
            <AvatarFallback>
              {user.firstName[0]}
              {user.lastName[0]}
            </AvatarFallback>
          </Avatar>
        )}
      </div>

      {/* Stats Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Active Bugs</CardTitle>
            <Bug className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">23</div>
            <p className="text-xs text-muted-foreground">+2 from last week</p>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Resolved</CardTitle>
            <CheckCircle2 className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">156</div>
            <p className="text-xs text-muted-foreground">+12 from last week</p>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Team Members</CardTitle>
            <Users className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">8</div>
            <p className="text-xs text-muted-foreground">Active contributors</p>
          </CardContent>
        </Card>

        <Card>
          <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
            <CardTitle className="text-sm font-medium">Performance</CardTitle>
            <TrendingUp className="h-4 w-4 text-muted-foreground" />
          </CardHeader>
          <CardContent>
            <div className="text-2xl font-bold">94%</div>
            <p className="text-xs text-muted-foreground">Resolution rate</p>
          </CardContent>
        </Card>
      </div>

      {/* Main Content Grid */}
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
        {/* Main Content Area */}
        <div className="lg:col-span-2 space-y-6">
          <Card>
            <CardHeader>
              <CardTitle className="text-lg">Quick Actions</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="grid grid-cols-2 gap-4">
                <div className="flex items-center p-3 border rounded-lg hover:bg-muted/50 cursor-pointer transition-colors">
                  <Bug className="h-5 w-5 mr-3 text-red-500" />
                  <div>
                    <p className="font-medium">Report Bug</p>
                    <p className="text-sm text-muted-foreground">
                      Create new issue
                    </p>
                  </div>
                </div>
                <div className="flex items-center p-3 border rounded-lg hover:bg-muted/50 cursor-pointer transition-colors">
                  <Users className="h-5 w-5 mr-3 text-blue-500" />
                  <div>
                    <p className="font-medium">Team Review</p>
                    <p className="text-sm text-muted-foreground">
                      Check assignments
                    </p>
                  </div>
                </div>
              </div>
            </CardContent>
          </Card>

          <Card>
            <CardHeader>
              <CardTitle className="text-lg">Recent Projects</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-4">
                {[
                  {
                    name: "Web Application",
                    status: "Active",
                    issues: 5,
                    lastUpdate: "2 hours ago",
                  },
                  {
                    name: "Mobile App",
                    status: "In Review",
                    issues: 2,
                    lastUpdate: "1 day ago",
                  },
                  {
                    name: "API Service",
                    status: "Completed",
                    issues: 0,
                    lastUpdate: "3 days ago",
                  },
                ].map((project, index) => (
                  <div
                    key={index}
                    className="flex items-center justify-between p-3 border rounded-lg"
                  >
                    <div className="flex-1">
                      <p className="font-medium">{project.name}</p>
                      <div className="flex items-center gap-2 mt-1">
                        <Badge
                          variant={
                            project.status === "Active"
                              ? "default"
                              : project.status === "In Review"
                              ? "secondary"
                              : "outline-solid"
                          }
                        >
                          {project.status}
                        </Badge>
                        <span className="text-sm text-muted-foreground">
                          {project.issues} issues
                        </span>
                      </div>
                    </div>
                    <div className="text-right">
                      <p className="text-sm text-muted-foreground flex items-center gap-1">
                        <Clock className="h-3 w-3" />
                        {project.lastUpdate}
                      </p>
                    </div>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>
        </div>

        {/* Sidebar */}
        <div className="space-y-6">
          {/* Recent Activity Small Component */}
          <RecentActivitySmall
            maxItems={4}
            showViewAll={true}
            onViewAll={handleViewAllActivity}
            className="h-fit"
          />

          {/* Team Status */}
          <Card>
            <CardHeader className="pb-3">
              <CardTitle className="text-base flex items-center gap-2">
                <Users className="h-4 w-4" />
                Team Status
              </CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-3">
                {[
                  {
                    name: "Alice Johnson",
                    status: "online",
                    avatar: "",
                    role: "Developer",
                  },
                  {
                    name: "Bob Smith",
                    status: "busy",
                    avatar: "",
                    role: "Designer",
                  },
                  {
                    name: "Carol Brown",
                    status: "away",
                    avatar: "",
                    role: "QA Engineer",
                  },
                ].map((member, index) => (
                  <div key={index} className="flex items-center gap-3">
                    <div className="relative">
                      <Avatar className="h-8 w-8">
                        <AvatarFallback className="text-xs">
                          {member.name
                            .split(" ")
                            .map((n) => n[0])
                            .join("")}
                        </AvatarFallback>
                      </Avatar>
                      <div
                        className={`absolute -bottom-0.5 -right-0.5 h-3 w-3 rounded-full border-2 border-background ${
                          member.status === "online"
                            ? "bg-green-500"
                            : member.status === "busy"
                            ? "bg-red-500"
                            : "bg-yellow-500"
                        }`}
                      />
                    </div>
                    <div className="flex-1 min-w-0">
                      <p className="text-sm font-medium truncate">
                        {member.name}
                      </p>
                      <p className="text-xs text-muted-foreground">
                        {member.role}
                      </p>
                    </div>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>

          {/* Priority Issues */}
          <Card>
            <CardHeader className="pb-3">
              <CardTitle className="text-base flex items-center gap-2">
                <Activity className="h-4 w-4" />
                Priority Issues
              </CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-3">
                {[
                  {
                    title: "Login authentication fails",
                    priority: "High",
                    assignee: "Alice",
                  },
                  {
                    title: "Mobile layout broken on iOS",
                    priority: "Medium",
                    assignee: "Bob",
                  },
                  {
                    title: "Database connection timeout",
                    priority: "Critical",
                    assignee: "Carol",
                  },
                ].map((issue, index) => (
                  <div key={index} className="flex items-start gap-2 text-xs">
                    <div
                      className={`w-2 h-2 rounded-full mt-1.5 shrink-0 ${
                        issue.priority === "Critical"
                          ? "bg-red-500"
                          : issue.priority === "High"
                          ? "bg-orange-500"
                          : "bg-yellow-500"
                      }`}
                    />
                    <div className="flex-1 min-w-0">
                      <p className="font-medium line-clamp-2 leading-tight">
                        {issue.title}
                      </p>
                      <p className="text-muted-foreground">
                        Assigned to {issue.assignee}
                      </p>
                    </div>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
