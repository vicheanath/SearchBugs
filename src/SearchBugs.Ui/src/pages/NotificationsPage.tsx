import { useState } from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { apiClient } from "../lib/api";
import {
  useUnreadCountQuery,
  useNotificationsQuery,
} from "../hooks/useNotificationQueries";
import { Button } from "@/components/ui/button";
import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import {
  Bell,
  Check,
  CheckCheck,
  Trash2,
  Filter,
  RefreshCw,
  BugIcon,
  MessageSquare,
  AlertTriangle,
  Info,
  UserCheck,
  ArrowLeft,
} from "lucide-react";
import { cn } from "@/lib/utils";
import { useNavigate } from "react-router-dom";

interface NotificationFilters {
  isRead?: boolean;
  pageSize: number;
  pageNumber: number;
}

export const NotificationsPage = () => {
  const navigate = useNavigate();
  const [filters, setFilters] = useState<NotificationFilters>({
    pageSize: 20,
    pageNumber: 1,
  });

  const queryClient = useQueryClient();

  // Use the new hooks with proper error handling
  const {
    data: notificationsResponse,
    isLoading,
    error,
    refetch,
    isFetching,
  } = useNotificationsQuery(
    filters.pageNumber,
    filters.pageSize,
    filters.isRead
  );

  // Get unread count with proper error handling
  const { data: unreadCountData } = useUnreadCountQuery();

  // Mark as read mutation
  const markAsReadMutation = useMutation({
    mutationFn: async (id: string) => {
      const response = await apiClient.notifications.markAsRead(id);
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["notifications"] });
    },
    onError: (error) => {
      console.error("Failed to mark notification as read:", error);
    },
  });

  // Mark all as read mutation
  const markAllAsReadMutation = useMutation({
    mutationFn: async () => {
      const response = await apiClient.notifications.markAllAsRead();
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["notifications"] });
    },
    onError: (error) => {
      console.error("Failed to mark all notifications as read:", error);
    },
  });

  // Delete notification mutation
  const deleteNotificationMutation = useMutation({
    mutationFn: async (id: string) => {
      const response = await apiClient.notifications.delete(id);
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["notifications"] });
    },
    onError: (error) => {
      console.error("Failed to delete notification:", error);
    },
  });

  // Clear all notifications mutation
  const clearAllMutation = useMutation({
    mutationFn: async () => {
      const response = await apiClient.notifications.clearAll();
      return response.data;
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["notifications"] });
    },
    onError: (error) => {
      console.error("Failed to clear all notifications:", error);
    },
  });

  const handleFilterChange = (newFilters: Partial<NotificationFilters>) => {
    setFilters((prev) => ({ ...prev, ...newFilters, pageNumber: 1 }));
  };

  const handlePageChange = (pageNumber: number) => {
    setFilters((prev) => ({ ...prev, pageNumber }));
  };

  const getNotificationIcon = (type: string) => {
    switch (type) {
      case "bug_created":
        return <BugIcon className="h-4 w-4" />;
      case "bug_updated":
        return <MessageSquare className="h-4 w-4" />;
      case "bug_assigned":
        return <UserCheck className="h-4 w-4" />;
      case "system":
        return <AlertTriangle className="h-4 w-4" />;
      default:
        return <Info className="h-4 w-4" />;
    }
  };

  const getNotificationIconColor = (type: string) => {
    switch (type) {
      case "bug_created":
        return "text-red-500";
      case "bug_updated":
        return "text-blue-500";
      case "bug_assigned":
        return "text-green-500";
      case "system":
        return "text-orange-500";
      default:
        return "text-gray-500";
    }
  };

  const formatTimeAgo = (createdAt: string) => {
    const date = new Date(createdAt);
    const now = new Date();
    const diff = now.getTime() - date.getTime();
    const minutes = Math.floor(diff / 60000);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);

    if (minutes < 1) return "Just now";
    if (minutes < 60) return `${minutes}m ago`;
    if (hours < 24) return `${hours}h ago`;
    return `${days}d ago`;
  };

  const notifications = notificationsResponse?.notifications || [];
  const unreadCount = unreadCountData?.count || 0;

  return (
    <div className="container mx-auto py-6 px-4">
      <div className="mb-6">
        <div className="flex items-center justify-between mb-4">
          <div className="flex items-center gap-3">
            <Button
              variant="ghost"
              size="sm"
              onClick={() => navigate(-1)}
              className="mr-2"
            >
              <ArrowLeft className="h-4 w-4 mr-1" />
              Back
            </Button>
            <Bell className="h-6 w-6" />
            <h1 className="text-2xl font-bold">Notifications</h1>
            {unreadCount > 0 && (
              <Badge variant="destructive" className="ml-2">
                {unreadCount} unread
              </Badge>
            )}
          </div>

          <div className="flex gap-2">
            <Button
              variant="outline"
              size="sm"
              onClick={() => refetch()}
              disabled={isFetching}
            >
              <RefreshCw
                className={cn("h-4 w-4 mr-2", isFetching && "animate-spin")}
              />
              Refresh
            </Button>

            {unreadCount > 0 && (
              <Button
                variant="outline"
                size="sm"
                onClick={() => markAllAsReadMutation.mutate()}
                disabled={markAllAsReadMutation.isPending}
              >
                <CheckCheck className="h-4 w-4 mr-2" />
                Mark All Read
              </Button>
            )}

            {notifications.length > 0 && (
              <Button
                variant="outline"
                size="sm"
                onClick={() => {
                  if (
                    confirm(
                      "Are you sure you want to clear all notifications? This action cannot be undone."
                    )
                  ) {
                    clearAllMutation.mutate();
                  }
                }}
                disabled={clearAllMutation.isPending}
              >
                <Trash2 className="h-4 w-4 mr-2" />
                Clear All
              </Button>
            )}
          </div>
        </div>

        {/* Filters */}
        <div className="flex gap-2 mb-4">
          <Button
            variant={filters.isRead === undefined ? "default" : "outline-solid"}
            size="sm"
            onClick={() => handleFilterChange({ isRead: undefined })}
          >
            <Filter className="h-4 w-4 mr-2" />
            All
          </Button>

          <Button
            variant={filters.isRead === false ? "default" : "outline-solid"}
            size="sm"
            onClick={() => handleFilterChange({ isRead: false })}
          >
            Unread ({unreadCount})
          </Button>

          <Button
            variant={filters.isRead === true ? "default" : "outline-solid"}
            size="sm"
            onClick={() => handleFilterChange({ isRead: true })}
          >
            Read
          </Button>
        </div>
      </div>

      {/* Loading state */}
      {isLoading && (
        <div className="flex justify-center items-center py-12">
          <RefreshCw className="h-6 w-6 animate-spin mr-2" />
          <span>Loading notifications...</span>
        </div>
      )}

      {/* Error state */}
      {error && (
        <Card className="border-red-200 bg-red-50">
          <CardContent className="pt-6">
            <div className="flex items-center gap-2 text-red-600">
              <AlertTriangle className="h-5 w-5" />
              <span>Failed to load notifications. Please try again.</span>
              <Button variant="outline" size="sm" onClick={() => refetch()}>
                Retry
              </Button>
            </div>
          </CardContent>
        </Card>
      )}

      {/* Empty state */}
      {!isLoading && !error && notifications.length === 0 && (
        <Card>
          <CardContent className="pt-6">
            <div className="text-center py-12">
              <Bell className="h-12 w-12 mx-auto text-gray-400 mb-4" />
              <h3 className="text-lg font-medium text-gray-900 mb-2">
                No notifications
              </h3>
              <p className="text-gray-500">
                {filters.isRead === false
                  ? "You have no unread notifications"
                  : filters.isRead === true
                  ? "You have no read notifications"
                  : "You don't have any notifications yet"}
              </p>
            </div>
          </CardContent>
        </Card>
      )}

      {/* Notifications list */}
      {!isLoading && notifications.length > 0 && (
        <div className="space-y-3">
          {notifications.map((notification) => (
            <Card
              key={notification.id}
              className={cn(
                "transition-all hover:shadow-md",
                !notification.isRead && "border-blue-200 bg-blue-50/50"
              )}
            >
              <CardContent className="p-4">
                <div className="flex items-start justify-between">
                  <div className="flex items-start gap-3 flex-1">
                    <div
                      className={cn(
                        "mt-1",
                        getNotificationIconColor(notification.type)
                      )}
                    >
                      {getNotificationIcon(notification.type)}
                    </div>

                    <div className="flex-1 min-w-0">
                      <div className="flex items-center gap-2 mb-1">
                        <p
                          className={cn(
                            "text-sm font-medium text-gray-900",
                            !notification.isRead && "font-semibold"
                          )}
                        >
                          {notification.message}
                        </p>
                        {!notification.isRead && (
                          <div className="w-2 h-2 bg-blue-500 rounded-full shrink-0" />
                        )}
                      </div>

                      {notification.data && (
                        <p className="text-xs text-gray-600 mb-2">
                          {notification.data}
                        </p>
                      )}

                      <div className="flex items-center gap-2 text-xs text-gray-500">
                        <span>{formatTimeAgo(notification.createdAt)}</span>
                        {notification.bugId && (
                          <>
                            <span>•</span>
                            <span>Bug #{notification.bugId.slice(0, 8)}</span>
                          </>
                        )}
                      </div>
                    </div>
                  </div>

                  <div className="flex gap-1 ml-4">
                    {!notification.isRead && (
                      <Button
                        variant="ghost"
                        size="sm"
                        onClick={() =>
                          markAsReadMutation.mutate(notification.id)
                        }
                        disabled={markAsReadMutation.isPending}
                        className="h-8 w-8 p-0"
                      >
                        <Check className="h-3 w-3" />
                      </Button>
                    )}

                    <Button
                      variant="ghost"
                      size="sm"
                      onClick={() => {
                        if (
                          confirm(
                            "Are you sure you want to delete this notification?"
                          )
                        ) {
                          deleteNotificationMutation.mutate(notification.id);
                        }
                      }}
                      disabled={deleteNotificationMutation.isPending}
                      className="h-8 w-8 p-0 text-red-500 hover:text-red-700"
                    >
                      <Trash2 className="h-3 w-3" />
                    </Button>
                  </div>
                </div>
              </CardContent>
            </Card>
          ))}
        </div>
      )}

      {/* Pagination */}
      {notificationsResponse && notificationsResponse.totalPages > 1 && (
        <div className="mt-6 flex justify-center items-center gap-2">
          <Button
            variant="outline"
            size="sm"
            onClick={() => handlePageChange(filters.pageNumber - 1)}
            disabled={filters.pageNumber <= 1}
          >
            Previous
          </Button>

          <span className="text-sm text-gray-600">
            Page {filters.pageNumber} of {notificationsResponse.totalPages}
          </span>

          <Button
            variant="outline"
            size="sm"
            onClick={() => handlePageChange(filters.pageNumber + 1)}
            disabled={filters.pageNumber >= notificationsResponse.totalPages}
          >
            Next
          </Button>
        </div>
      )}
    </div>
  );
};
