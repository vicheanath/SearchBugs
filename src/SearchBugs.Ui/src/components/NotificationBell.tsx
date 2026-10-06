import React, { useState, useEffect, useRef } from "react";
import { useNotifications } from "../hooks/useNotifications";
import {
  useUnreadCountQuery,
  useNotificationMutations,
} from "../hooks/useNotificationQueries";
import { NotificationData } from "../services/notificationService";
import {
  Bell,
  BellRing,
  Check,
  CheckCheck,
  Trash2,
  ExternalLink,
  Bug,
  MessageSquare,
  UserCheck,
  AlertCircle,
  Info,
  X,
  Wifi,
  WifiOff,
} from "lucide-react";
import { useNavigate } from "react-router-dom";
import { cn } from "@/lib/utils";

interface NotificationBellProps {}

export const NotificationBell: React.FC<NotificationBellProps> = () => {
  const navigate = useNavigate();
  const { notifications, clearNotifications, isConnected, loadNotifications } =
    useNotifications();

  // Use React Query for unread count to prevent undefined errors
  const { data: unreadCountData, refetch: refetchUnreadCount } =
    useUnreadCountQuery();
  const { markAsReadMutation, markAllAsReadMutation } =
    useNotificationMutations();

  const [isOpen, setIsOpen] = useState(false);
  const [hasNewNotifications, setHasNewNotifications] = useState(false);
  const dropdownRef = useRef<HTMLDivElement>(null);
  const previousCountRef = useRef<number>(0);

  // Get combined unread count from both API and local state
  const apiUnreadCount = unreadCountData?.count || 0;
  const localUnreadCount = notifications.filter((n) => !n.isRead).length;
  const totalUnreadCount = Math.max(apiUnreadCount, localUnreadCount);

  // Track new notifications for animation
  useEffect(() => {
    if (totalUnreadCount > previousCountRef.current) {
      setHasNewNotifications(true);
      // Reset animation after 3 seconds
      setTimeout(() => setHasNewNotifications(false), 3000);
    }
    previousCountRef.current = totalUnreadCount;
  }, [totalUnreadCount]);

  // Load notifications when component mounts or when opening
  useEffect(() => {
    if (isOpen) {
      loadNotifications();
      refetchUnreadCount();
    }
  }, [isOpen, loadNotifications, refetchUnreadCount]);

  // Close dropdown when clicking outside
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (
        dropdownRef.current &&
        !dropdownRef.current.contains(event.target as Node)
      ) {
        setIsOpen(false);
      }
    };

    if (isOpen) {
      document.addEventListener("mousedown", handleClickOutside);
      return () =>
        document.removeEventListener("mousedown", handleClickOutside);
    }
  }, [isOpen]);

  const handleNotificationClick = async (notification: NotificationData) => {
    if (notification.id && !notification.isRead) {
      await markAsReadMutation.mutateAsync(notification.id);
    }

    // Navigate to bug if it's a bug notification
    if (notification.bugId) {
      navigate(`/bugs/${notification.bugId}`);
      setIsOpen(false);
    }
  };

  const handleViewAllNotifications = () => {
    navigate("/notifications");
    setIsOpen(false);
  };

  const handleMarkAllAsRead = async () => {
    await markAllAsReadMutation.mutateAsync();
    refetchUnreadCount();
  };

  const formatTime = (timestamp: string) => {
    const date = new Date(timestamp);
    const now = new Date();
    const diff = now.getTime() - date.getTime();
    const minutes = Math.floor(diff / 60000);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);

    if (minutes < 1) return "Just now";
    if (minutes < 60) return `${minutes}m ago`;
    if (hours < 24) return `${hours}h ago`;
    if (days === 1) return "Yesterday";
    return `${days}d ago`;
  };

  const getNotificationIcon = (type: string) => {
    switch (type.toLowerCase()) {
      case "bug_created":
      case "bugcreated":
        return <Bug className="h-4 w-4 text-red-500" />;
      case "bug_updated":
      case "bugupdated":
        return <MessageSquare className="h-4 w-4 text-blue-500" />;
      case "bug_assigned":
      case "bugassigned":
        return <UserCheck className="h-4 w-4 text-green-500" />;
      case "system":
        return <AlertCircle className="h-4 w-4 text-orange-500" />;
      default:
        return <Info className="h-4 w-4 text-gray-500" />;
    }
  };

  const ConnectionStatus = () => (
    <div className="flex items-center gap-1 text-xs text-muted-foreground">
      {isConnected ? (
        <>
          <Wifi className="h-3 w-3 text-green-500" />
          <span>Live</span>
        </>
      ) : (
        <>
          <WifiOff className="h-3 w-3 text-orange-500" />
          <span>Offline</span>
        </>
      )}
    </div>
  );

  return (
    <div className="relative" ref={dropdownRef}>
      {/* Bell Icon */}
      <button
        onClick={() => setIsOpen(!isOpen)}
        className={cn(
          "relative p-2 text-gray-600 hover:text-gray-800 dark:text-gray-300 dark:hover:text-gray-100 transition-all duration-200 focus:outline-hidden focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 rounded-lg",
          hasNewNotifications && "animate-pulse"
        )}
        aria-label={`Notifications ${
          totalUnreadCount > 0 ? `(${totalUnreadCount} unread)` : ""
        }`}
      >
        {totalUnreadCount > 0 ? (
          <BellRing
            className={cn(
              "h-5 w-5 sm:h-6 sm:w-6 text-blue-600",
              hasNewNotifications && "animate-bounce"
            )}
          />
        ) : (
          <Bell className="h-5 w-5 sm:h-6 sm:w-6" />
        )}
        {/* Notification Badge */}
        {totalUnreadCount > 0 && (
          <span
            className={cn(
              "absolute -top-0.5 -right-0.5 bg-red-500 text-white text-xs rounded-full min-w-[18px] h-[18px] flex items-center justify-center font-medium shadow-lg transition-transform duration-200",
              hasNewNotifications && "scale-110"
            )}
          >
            {totalUnreadCount > 99 ? "99+" : totalUnreadCount}
          </span>
        )}
        {/* Connection indicator dot */}
        <div
          className={cn(
            "absolute bottom-0 right-0 w-2 h-2 rounded-full border border-background",
            isConnected ? "bg-green-500" : "bg-orange-500"
          )}
        />
      </button>

      {/* Dropdown */}
      {isOpen && (
        <div className="absolute right-0 mt-2 w-80 sm:w-96 bg-white dark:bg-gray-800 rounded-xl shadow-2xl border border-gray-200 dark:border-gray-700 z-50 overflow-hidden">
          {/* Header */}
          <div className="px-4 sm:px-6 py-3 sm:py-4 border-b border-gray-100 dark:border-gray-700 bg-linear-to-r from-blue-50 to-indigo-50 dark:from-gray-700 dark:to-gray-600">
            <div className="flex items-center justify-between">
              <div className="flex items-center gap-2">
                <h3 className="text-base sm:text-lg font-semibold text-gray-900 dark:text-gray-100 flex items-center gap-2">
                  <Bell className="h-4 w-4 sm:h-5 sm:w-5 text-blue-600" />
                  <span className="hidden sm:inline">Notifications</span>
                  <span className="sm:hidden">Alerts</span>
                  {totalUnreadCount > 0 && (
                    <span className="bg-blue-100 dark:bg-blue-900 text-blue-800 dark:text-blue-200 text-xs px-2 py-1 rounded-full">
                      {totalUnreadCount} new
                    </span>
                  )}
                </h3>
              </div>
              <div className="flex items-center space-x-2">
                <ConnectionStatus />
                {totalUnreadCount > 0 && (
                  <button
                    onClick={handleMarkAllAsRead}
                    disabled={markAllAsReadMutation.isPending}
                    className="text-xs sm:text-sm text-blue-600 hover:text-blue-800 font-medium disabled:opacity-50 flex items-center gap-1"
                    title="Mark all as read"
                  >
                    <CheckCheck className="h-3 w-3" />
                    <span className="hidden sm:inline">Mark all read</span>
                  </button>
                )}
                <button
                  onClick={() => setIsOpen(false)}
                  className="text-gray-400 hover:text-gray-600 dark:text-gray-500 dark:hover:text-gray-300 p-1 rounded-md hover:bg-gray-100 dark:hover:bg-gray-700"
                  title="Close notifications"
                >
                  <X className="h-4 w-4" />
                </button>
              </div>
            </div>
          </div>

          {/* Notifications List */}
          <div className="max-h-80 sm:max-h-96 overflow-y-auto">
            {notifications.length === 0 ? (
              <div className="px-4 sm:px-6 py-8 sm:py-12 text-center text-gray-500 dark:text-gray-400">
                <div className="mx-auto h-12 w-12 sm:h-16 sm:w-16 text-gray-300 dark:text-gray-600 mb-3 sm:mb-4">
                  <Bell className="h-full w-full" />
                </div>
                <h4 className="text-base sm:text-lg font-medium text-gray-900 dark:text-gray-100 mb-1 sm:mb-2">
                  No notifications yet
                </h4>
                <p className="text-xs sm:text-sm">
                  When you get notifications, they'll show up here.
                </p>
              </div>
            ) : (
              <div className="divide-y divide-gray-50 dark:divide-gray-700">
                {notifications.slice(0, 10).map((notification, index) => (
                  <div
                    key={notification.id || index}
                    onClick={() => handleNotificationClick(notification)}
                    className={cn(
                      "px-4 sm:px-6 py-3 sm:py-4 cursor-pointer hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors relative",
                      !notification.isRead &&
                        "bg-blue-50/50 dark:bg-blue-950/50 border-l-4 border-l-blue-500"
                    )}
                  >
                    <div className="flex items-start space-x-3">
                      <div className="shrink-0 mt-1">
                        {getNotificationIcon(notification.type)}
                      </div>

                      <div className="flex-1 min-w-0">
                        <p
                          className={cn(
                            "text-sm",
                            !notification.isRead
                              ? "font-semibold text-gray-900 dark:text-gray-100"
                              : "font-normal text-gray-700 dark:text-gray-300"
                          )}
                        >
                          {notification.message}
                        </p>

                        {notification.data && (
                          <p className="text-xs text-gray-500 dark:text-gray-400 mt-1 line-clamp-2">
                            {notification.data}
                          </p>
                        )}

                        <div className="flex items-center justify-between mt-2">
                          <p className="text-xs text-gray-400 dark:text-gray-500">
                            {formatTime(notification.timestamp)}
                          </p>
                          {notification.bugId && (
                            <div className="flex items-center gap-1 text-xs text-blue-600 dark:text-blue-400">
                              <ExternalLink className="h-3 w-3" />
                              <span className="hidden sm:inline">View Bug</span>
                            </div>
                          )}
                        </div>
                      </div>

                      {!notification.isRead && (
                        <div className="shrink-0 flex items-center gap-1 sm:gap-2">
                          <div className="w-2 h-2 bg-blue-500 rounded-full" />
                          <button
                            onClick={(e) => {
                              e.stopPropagation();
                              if (notification.id) {
                                markAsReadMutation.mutate(notification.id);
                              }
                            }}
                            disabled={markAsReadMutation.isPending}
                            className="text-gray-400 hover:text-blue-600 disabled:opacity-50 p-1 rounded-md hover:bg-gray-100 dark:hover:bg-gray-600"
                            title="Mark as read"
                          >
                            <Check className="h-3 w-3 sm:h-4 sm:w-4" />
                          </button>
                        </div>
                      )}
                    </div>
                  </div>
                ))}

                {notifications.length > 10 && (
                  <div className="px-4 sm:px-6 py-3 text-center text-xs sm:text-sm text-gray-500 dark:text-gray-400 bg-gray-50 dark:bg-gray-700">
                    ... and {notifications.length - 10} more notifications
                  </div>
                )}
              </div>
            )}
          </div>

          {/* Footer */}
          {notifications.length > 0 && (
            <div className="px-4 sm:px-6 py-3 sm:py-4 border-t border-gray-100 dark:border-gray-700 bg-gray-50 dark:bg-gray-800">
              <div className="flex items-center justify-between">
                <button
                  onClick={handleViewAllNotifications}
                  className="text-xs sm:text-sm font-medium text-blue-600 hover:text-blue-800 dark:text-blue-400 dark:hover:text-blue-300 flex items-center gap-1 sm:gap-2 transition-colors"
                >
                  <ExternalLink className="h-3 w-3 sm:h-4 sm:w-4" />
                  <span className="hidden sm:inline">
                    View all notifications
                  </span>
                  <span className="sm:hidden">View all</span>
                </button>
                <button
                  onClick={() => clearNotifications()}
                  className="text-xs sm:text-sm text-gray-500 hover:text-red-600 dark:text-gray-400 dark:hover:text-red-400 flex items-center gap-1 transition-colors"
                  title="Clear all notifications"
                >
                  <Trash2 className="h-3 w-3" />
                  <span className="hidden sm:inline">Clear all</span>
                </button>
              </div>
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default NotificationBell;
