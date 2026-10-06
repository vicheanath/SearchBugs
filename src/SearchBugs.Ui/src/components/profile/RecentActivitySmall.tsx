import React from "react";
import { useQuery } from "@tanstack/react-query";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import {
  Activity,
  Clock,
  CheckCircle,
  XCircle,
  ArrowRight,
  RefreshCw,
  BookOpen,
  Bug,
  Users,
  Star,
  GitFork,
  Edit,
} from "lucide-react";
import { formatDistanceToNow } from "date-fns";
import { apiClient } from "@/lib/api";

interface RecentActivitySmallProps {
  maxItems?: number;
  showViewAll?: boolean;
  onViewAll?: () => void;
  className?: string;
}

const getActivityIcon = (iconName: string) => {
  const iconMap: Record<string, React.ComponentType<{ className?: string }>> = {
    Bug: Bug,
    FolderOpen: BookOpen,
    User: Users,
    Shield: Star,
    LogIn: Activity,
    UserPlus: Users,
    Lock: Star,
    GitBranch: GitFork,
    Bell: Activity,
    Plus: Activity,
    Edit: Edit,
    Trash2: Activity,
    Activity: Activity,
  };

  return iconMap[iconName] || Activity;
};

export const RecentActivitySmall: React.FC<RecentActivitySmallProps> = ({
  maxItems = 5,
  showViewAll = true,
  onViewAll,
  className,
}) => {
  const {
    data: activityData,
    isLoading,
    error,
    refetch,
    isRefetching,
  } = useQuery({
    queryKey: ["user-activity-small"],
    queryFn: () =>
      apiClient.profile.getActivity({ pageNumber: 1, pageSize: maxItems }),
    refetchInterval: 30000, // Refresh every 30 seconds
  });

  if (isLoading) {
    return (
      <Card className={className}>
        <CardHeader className="pb-3">
          <CardTitle className="text-base flex items-center gap-2">
            <Activity className="h-4 w-4" />
            Recent Activity
          </CardTitle>
        </CardHeader>
        <CardContent>
          <div className="space-y-3">
            {[...Array(3)].map((_, i) => (
              <div key={i} className="flex items-start gap-2">
                <div className="w-6 h-6 bg-muted rounded-full animate-pulse" />
                <div className="flex-1 space-y-1">
                  <div className="h-3 bg-muted rounded animate-pulse" />
                  <div className="h-2 bg-muted rounded w-2/3 animate-pulse" />
                </div>
              </div>
            ))}
          </div>
        </CardContent>
      </Card>
    );
  }

  if (error || !activityData?.data.isSuccess) {
    return (
      <Card className={className}>
        <CardHeader className="pb-3">
          <CardTitle className="text-base flex items-center gap-2">
            <Activity className="h-4 w-4" />
            Recent Activity
          </CardTitle>
        </CardHeader>
        <CardContent>
          <div className="text-center py-4 text-muted-foreground">
            <Activity className="h-8 w-8 mx-auto mb-2 opacity-50" />
            <p className="text-sm">Failed to load</p>
            <Button
              variant="ghost"
              size="sm"
              className="mt-2 h-7 px-2"
              onClick={() => refetch()}
            >
              <RefreshCw className="h-3 w-3 mr-1" />
              Retry
            </Button>
          </div>
        </CardContent>
      </Card>
    );
  }

  const activities = activityData.data.value.activities;

  return (
    <Card className={className}>
      <CardHeader className="pb-3">
        <div className="flex items-center justify-between">
          <CardTitle className="text-base flex items-center gap-2">
            <Activity className="h-4 w-4" />
            Recent Activity
          </CardTitle>
          <Button
            variant="ghost"
            size="sm"
            onClick={() => refetch()}
            disabled={isRefetching}
            className="h-7 w-7 p-0"
          >
            <RefreshCw
              className={`h-3 w-3 ${isRefetching ? "animate-spin" : ""}`}
            />
          </Button>
        </div>
      </CardHeader>
      <CardContent>
        {activities.length === 0 ? (
          <div className="text-center py-4 text-muted-foreground">
            <Activity className="h-8 w-8 mx-auto mb-2 opacity-50" />
            <p className="text-sm">No recent activity</p>
          </div>
        ) : (
          <div className="space-y-3">
            {activities.slice(0, maxItems).map((activity) => {
              const IconComponent = getActivityIcon(activity.icon);
              return (
                <div
                  key={activity.id}
                  className="flex items-start gap-2 group hover:bg-muted/50 -mx-2 px-2 py-1 rounded-md transition-colors"
                >
                  <div
                    className={`p-1 rounded-full shrink-0 ${
                      activity.isSuccess
                        ? "bg-green-100 text-green-600"
                        : "bg-red-100 text-red-600"
                    }`}
                  >
                    <IconComponent className="h-3 w-3" />
                  </div>
                  <div className="flex-1 min-w-0">
                    <p className="text-xs font-medium line-clamp-2 leading-tight">
                      {activity.description}
                    </p>
                    <div className="flex items-center gap-1 mt-1">
                      <p className="text-xs text-muted-foreground flex items-center gap-1">
                        <Clock className="h-2 w-2" />
                        {formatDistanceToNow(new Date(activity.timestamp), {
                          addSuffix: true,
                        })}
                      </p>
                      {activity.isSuccess ? (
                        <CheckCircle className="h-2 w-2 text-green-600" />
                      ) : (
                        <XCircle className="h-2 w-2 text-red-600" />
                      )}
                    </div>
                    <Badge
                      variant="outline"
                      className="text-xs px-1 py-0 h-4 mt-1"
                    >
                      {activity.type}
                    </Badge>
                  </div>
                </div>
              );
            })}

            {showViewAll && onViewAll && activities.length > 0 && (
              <div className="pt-2 border-t">
                <Button
                  variant="ghost"
                  size="sm"
                  className="w-full h-7 text-xs"
                  onClick={onViewAll}
                >
                  View all activity
                  <ArrowRight className="h-3 w-3 ml-1" />
                </Button>
              </div>
            )}
          </div>
        )}
      </CardContent>
    </Card>
  );
};

export default RecentActivitySmall;
