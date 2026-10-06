import React, { useState, useEffect } from "react";
import { useAuth } from "@/hooks/useAuth";
import { Button } from "@/components/ui/button";
import { Card, CardContent } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Badge } from "@/components/ui/badge";
import {
  Crown,
  UserCog,
  Users,
  Settings,
  AlertTriangle,
  Search,
  Activity,
  EyeOff,
  Shield,
  Clock,
} from "lucide-react";
import { useToast } from "@/components/ui/use-toast";
import { useQuery } from "@tanstack/react-query";
import { apiClient, User, AuditLog } from "@/lib/api";
import { format } from "date-fns";

interface AdminBannerProps {
  onQuickImpersonate?: (userId: string) => void;
}

export const AdminBanner: React.FC<AdminBannerProps> = ({
  onQuickImpersonate,
}) => {
  const { user: currentUser } = useAuth();
  const [searchTerm, setSearchTerm] = useState("");
  const [showQuickActions, setShowQuickActions] = useState(false);
  const [showAuditLogs, setShowAuditLogs] = useState(false);
  const [secretClickCount, setSecretClickCount] = useState(0);
  const { toast } = useToast();

  // Check if current user is admin
  const isAdmin = currentUser?.roles?.some((role) => role.name === "Admin");

  // Reset secret click count after 5 seconds
  useEffect(() => {
    if (secretClickCount > 0) {
      const timer = setTimeout(() => setSecretClickCount(0), 5000);
      return () => clearTimeout(timer);
    }
  }, [secretClickCount]);

  // Secret audit log access (click crown 5 times quickly)
  const handleSecretAccess = () => {
    const newCount = secretClickCount + 1;
    setSecretClickCount(newCount);

    if (newCount >= 5) {
      setShowAuditLogs(!showAuditLogs);
      setSecretClickCount(0);
      toast({
        title: showAuditLogs ? "Audit Logs Hidden" : "Audit Logs Revealed",
        description: showAuditLogs
          ? "Audit logs panel is now hidden"
          : "Secret audit logs panel is now visible",
      });
    }
  };

  // Quick search for users when typing
  const { data: searchResults } = useQuery({
    queryKey: ["users-search", searchTerm],
    queryFn: async () => {
      if (searchTerm.length < 2) return [];
      const response = await apiClient.users.getAll({
        searchTerm,
        pageSize: 5,
      });
      return response.data.value;
    },
    enabled: searchTerm.length >= 2 && showQuickActions,
  });

  // Recent audit logs query
  const { data: auditLogs } = useQuery({
    queryKey: ["audit-logs-recent"],
    queryFn: async () => {
      const response = await apiClient.auditLogs.getAll({
        pageSize: 10,
      });
      return response.data.value;
    },
    enabled: showAuditLogs,
    refetchInterval: showAuditLogs ? 10000 : false, // Refresh every 10 seconds when visible
  });

  if (!isAdmin || currentUser?.isImpersonating) {
    return null;
  }

  const handleQuickImpersonate = async (userId: string, userName: string) => {
    if (onQuickImpersonate) {
      onQuickImpersonate(userId);
    }
    setSearchTerm("");
    setShowQuickActions(false);

    toast({
      title: "Quick Impersonation",
      description: `Attempting to impersonate ${userName}...`,
    });
  };

  return (
    <Card className="mb-4 bg-linear-to-r from-slate-50 to-blue-50 dark:from-slate-900 dark:to-blue-950 border-slate-300 dark:border-slate-700 mt-4">
      <CardContent className="py-3">
        <div className="flex items-center justify-between">
          <div className="flex items-center">
            <div
              className="cursor-pointer hover:scale-110 transition-transform"
              onClick={handleSecretAccess}
              title={`Click ${
                5 - secretClickCount
              } more times for secret access`}
            >
              <Crown className="h-5 w-5 text-slate-700 dark:text-slate-300 mr-3 hover:text-blue-600 dark:hover:text-blue-400 transition-colors" />
            </div>
            <div>
              <span className="text-slate-800 dark:text-slate-200 font-medium">
                Administrator Panel
              </span>
              <p className="text-sm text-slate-600 dark:text-slate-400">
                You have administrative privileges. Use them responsibly.
              </p>
            </div>
          </div>

          <div className="flex items-center gap-2">
            {!showQuickActions ? (
              <>
                <Button
                  variant="outline"
                  size="sm"
                  onClick={() => setShowQuickActions(!showQuickActions)}
                  className="border-slate-400 dark:border-slate-500 text-slate-700 dark:text-slate-300 hover:bg-slate-100 dark:hover:bg-slate-700"
                >
                  <UserCog className="h-4 w-4 mr-1" />
                  Quick Impersonate
                </Button>

                <Button
                  variant="outline"
                  size="sm"
                  onClick={() => (window.location.href = "/users")}
                  className="border-slate-400 dark:border-slate-500 text-slate-700 dark:text-slate-300 hover:bg-slate-100 dark:hover:bg-slate-700"
                >
                  <Users className="h-4 w-4 mr-1" />
                  Manage Users
                </Button>

                <Button
                  variant="outline"
                  size="sm"
                  onClick={() => (window.location.href = "/settings")}
                  className="border-slate-400 dark:border-slate-500 text-slate-700 dark:text-slate-300 hover:bg-slate-100 dark:hover:bg-slate-700"
                >
                  <Settings className="h-4 w-4 mr-1" />
                  System Settings
                </Button>

                {showAuditLogs && (
                  <Button
                    variant="outline"
                    size="sm"
                    onClick={() => setShowAuditLogs(false)}
                    className="border-red-400 dark:border-red-500 text-red-700 dark:text-red-400 hover:bg-red-50 dark:hover:bg-red-950/50"
                  >
                    <EyeOff className="h-4 w-4 mr-1" />
                    Hide Audit
                  </Button>
                )}
              </>
            ) : (
              <div className="flex items-center gap-2">
                <div className="relative">
                  <Search className="absolute left-2 top-2 h-4 w-4 text-slate-500 dark:text-slate-400" />
                  <Input
                    placeholder="Search users to impersonate..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    className="pl-8 w-64 border-slate-400 dark:border-slate-500 focus:ring-blue-500 dark:focus:ring-blue-400"
                  />

                  {/* Quick search results */}
                  {searchResults && searchResults.length > 0 && (
                    <div className="absolute top-full left-0 right-0 mt-1 bg-white dark:bg-slate-800 border border-slate-300 dark:border-slate-600 rounded-md shadow-lg z-50 max-h-40 overflow-y-auto">
                      {searchResults.map((user: User) => (
                        <div
                          key={user.id}
                          className="px-3 py-2 hover:bg-blue-50 dark:hover:bg-blue-950/30 cursor-pointer flex items-center justify-between"
                          onClick={() =>
                            handleQuickImpersonate(
                              user.id,
                              `${user.firstName} ${user.lastName}`
                            )
                          }
                        >
                          <div>
                            <div className="font-medium text-sm text-slate-900 dark:text-slate-100">
                              {user.firstName} {user.lastName}
                            </div>
                            <div className="text-xs text-gray-500 dark:text-gray-400">
                              {user.email}
                            </div>
                          </div>
                          <UserCog className="h-4 w-4 text-blue-600 dark:text-blue-400" />
                        </div>
                      ))}
                    </div>
                  )}
                </div>

                <Button
                  variant="ghost"
                  size="sm"
                  onClick={() => {
                    setShowQuickActions(false);
                    setSearchTerm("");
                  }}
                  className="text-slate-700 dark:text-slate-300 hover:bg-slate-100 dark:hover:bg-slate-700"
                >
                  Cancel
                </Button>
              </div>
            )}
          </div>
        </div>

        {/* Hidden Audit Logs Panel */}
        {showAuditLogs && (
          <div className="mt-4 pt-4 border-t border-slate-300 dark:border-slate-600">
            <div className="flex items-center justify-between mb-3">
              <div className="flex items-center">
                <Shield className="h-4 w-4 text-orange-600 dark:text-orange-400 mr-2" />
                <span className="text-sm font-medium text-slate-800 dark:text-slate-200">
                  Recent System Activity (Secret View)
                </span>
              </div>
              <Button
                variant="ghost"
                size="sm"
                onClick={() => window.open("/audit-logs", "_blank")}
                className="text-xs text-blue-600 dark:text-blue-400 hover:text-blue-800 dark:hover:text-blue-300"
              >
                <Activity className="h-3 w-3 mr-1" />
                View Full Logs
              </Button>
            </div>

            <div className="bg-slate-100 dark:bg-slate-800 rounded-md p-3 max-h-48 overflow-y-auto">
              {auditLogs && auditLogs.length > 0 ? (
                <div className="space-y-2">
                  {auditLogs.slice(0, 5).map((log: AuditLog) => (
                    <div
                      key={log.id}
                      className="flex items-center justify-between text-xs bg-white dark:bg-slate-700 rounded px-2 py-1"
                    >
                      <div className="flex items-center gap-2">
                        <Badge
                          variant={log.isSuccess ? "default" : "destructive"}
                          className="text-xs"
                        >
                          {log.isSuccess ? "✓" : "✗"}
                        </Badge>
                        <span className="font-mono text-slate-700 dark:text-slate-300 truncate max-w-40">
                          {log.requestName}
                        </span>
                        <span className="text-slate-500 dark:text-slate-400">
                          by {log.userName || "System"}
                        </span>
                      </div>
                      <div className="flex items-center gap-1 text-slate-500 dark:text-slate-400">
                        <Clock className="h-3 w-3" />
                        <span>
                          {format(new Date(log.createdOnUtc), "HH:mm:ss")}
                        </span>
                      </div>
                    </div>
                  ))}
                </div>
              ) : (
                <div className="text-center text-slate-500 dark:text-slate-400 text-xs">
                  <Activity className="h-8 w-8 mx-auto mb-2 opacity-50" />
                  No recent activity found
                </div>
              )}
            </div>
          </div>
        )}

        {/* Warning message */}
        <div className="flex items-center mt-3 pt-3 border-t border-slate-300 dark:border-slate-600">
          <AlertTriangle className="h-4 w-4 text-amber-500 dark:text-amber-400 mr-2" />
          <span className="text-xs text-amber-700 dark:text-amber-300">
            Admin actions are logged and monitored. Impersonation should only be
            used for legitimate support purposes.
          </span>
        </div>
      </CardContent>
    </Card>
  );
};
