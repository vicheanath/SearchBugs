import React, { useState, useEffect } from "react";
import { useNotifications } from "../hooks/useNotifications";
import {
  notificationService,
  NotificationData,
} from "../services/notificationService";
import { apiClient } from "../lib/api";

export const NotificationTester: React.FC = () => {
  const [isConnected, setIsConnected] = useState(false);
  const [connectionStatus, setConnectionStatus] = useState("Disconnected");
  const [userId, setUserId] = useState("test-user-123");
  const [customMessage, setCustomMessage] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [testResult, setTestResult] = useState<string>("");
  const [testResultType, setTestResultType] = useState<
    "success" | "error" | "info"
  >("info");

  const { notifications, unreadCount, clearNotifications } = useNotifications();

  // Check connection status periodically
  useEffect(() => {
    const checkConnection = () => {
      const connected = notificationService.isConnected();
      const status = notificationService.getConnectionState();
      setIsConnected(connected);
      setConnectionStatus(status);
    };

    checkConnection();
    const interval = setInterval(checkConnection, 1000);
    return () => clearInterval(interval);
  }, []);

  // Set up notification listeners
  useEffect(() => {
    const handleNotification = (data: NotificationData) => {
      console.log("Test component received notification:", data);
      setTestResult(`Received: ${data.message}`);
      setTestResultType("success");
    };

    const handleBugNotification = (data: NotificationData) => {
      console.log("Test component received bug notification:", data);
      setTestResult(`Bug notification: ${data.message}`);
      setTestResultType("success");
    };

    notificationService.onNotification("notification", handleNotification);
    notificationService.onNotification(
      "bugNotification",
      handleBugNotification
    );

    return () => {
      notificationService.offNotification("notification", handleNotification);
      notificationService.offNotification(
        "bugNotification",
        handleBugNotification
      );
    };
  }, []);

  const handleConnect = async () => {
    try {
      setIsLoading(true);
      setTestResult("Connecting to SignalR...");
      setTestResultType("info");

      await notificationService.startConnection();
      await notificationService.joinUserGroup(userId);

      setTestResult("Connected successfully and joined user group!");
      setTestResultType("success");
    } catch (error) {
      setTestResult(`Connection failed: ${error}`);
      setTestResultType("error");
      console.error("Connection error:", error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleDisconnect = async () => {
    try {
      setIsLoading(true);
      setTestResult("Disconnecting...");
      setTestResultType("info");

      await notificationService.leaveUserGroup(userId);
      await notificationService.stopConnection();

      setTestResult("Disconnected successfully!");
      setTestResultType("info");
    } catch (error) {
      setTestResult(`Disconnect failed: ${error}`);
      setTestResultType("error");
      console.error("Disconnect error:", error);
    } finally {
      setIsLoading(false);
    }
  };

  const sendTestNotification = async () => {
    try {
      setIsLoading(true);
      setTestResult("Sending test notification...");
      setTestResultType("info");

      const payload = {
        userId: userId,
        message: customMessage || undefined,
      };

      await apiClient.testNotifications.sendTestNotification(payload);
      setTestResult(`Test notification sent successfully!`);
      setTestResultType("success");
    } catch (error: unknown) {
      const errorMessage =
        error instanceof Error ? error.message : "Unknown error";
      setTestResult(`Request failed: ${errorMessage}`);
      setTestResultType("error");
      console.error("Send notification error:", error);
    } finally {
      setIsLoading(false);
    }
  };

  const sendBugCreatedTest = async () => {
    try {
      setIsLoading(true);
      const payload = {
        userId: userId,
        message:
          "🐛 New bug created: Test Bug #" + Math.floor(Math.random() * 1000),
      };

      await apiClient.testNotifications.sendTestNotification(payload);
      setTestResult("Bug created notification sent!");
      setTestResultType("success");
    } catch (error: unknown) {
      const errorMessage =
        error instanceof Error ? error.message : "Unknown error";
      setTestResult(`Failed to send bug notification: ${errorMessage}`);
      setTestResultType("error");
    } finally {
      setIsLoading(false);
    }
  };

  const sendBugUpdatedTest = async () => {
    try {
      setIsLoading(true);
      const payload = {
        userId: userId,
        message: "📝 Bug updated: Status changed to In Progress",
      };

      await apiClient.testNotifications.sendTestNotification(payload);
      setTestResult("Bug updated notification sent!");
      setTestResultType("success");
    } catch (error: unknown) {
      const errorMessage =
        error instanceof Error ? error.message : "Unknown error";
      setTestResult(`Failed to send update notification: ${errorMessage}`);
      setTestResultType("error");
    } finally {
      setIsLoading(false);
    }
  };

  const getStatusColor = () => {
    if (connectionStatus === "Connected") return "text-green-600 bg-green-100";
    if (
      connectionStatus === "Connecting" ||
      connectionStatus === "Reconnecting"
    )
      return "text-yellow-600 bg-yellow-100";
    return "text-red-600 bg-red-100";
  };

  const getResultColor = () => {
    switch (testResultType) {
      case "success":
        return "text-green-800 bg-green-100 border-green-300";
      case "error":
        return "text-red-800 bg-red-100 border-red-300";
      default:
        return "text-blue-800 bg-blue-100 border-blue-300";
    }
  };

  return (
    <div className="max-w-4xl mx-auto p-6 bg-white shadow-lg rounded-lg">
      <div className="mb-6">
        <h2 className="text-2xl font-bold text-gray-900 mb-2">
          🧪 SignalR Notification Tester
        </h2>
        <p className="text-gray-600">
          Test real-time notifications using SignalR connection
        </p>
      </div>

      {/* Connection Status */}
      <div className="mb-6 p-4 rounded-lg border">
        <div className="flex items-center justify-between mb-4">
          <h3 className="text-lg font-semibold">Connection Status</h3>
          <span
            className={`px-3 py-1 rounded-full text-sm font-medium ${getStatusColor()}`}
          >
            {connectionStatus}
          </span>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label
              htmlFor="userId"
              className="block text-sm font-medium text-gray-700 mb-1"
            >
              User ID
            </label>
            <input
              type="text"
              id="userId"
              value={userId}
              onChange={(e) => setUserId(e.target.value)}
              className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-hidden focus:ring-2 focus:ring-blue-500"
              placeholder="Enter user ID for testing"
              disabled={isConnected}
            />
          </div>

          <div className="flex items-end gap-2">
            <button
              onClick={handleConnect}
              disabled={isConnected || isLoading}
              className="px-4 py-2 bg-green-600 text-white rounded-md hover:bg-green-700 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {isLoading && connectionStatus !== "Connected"
                ? "Connecting..."
                : "Connect"}
            </button>

            <button
              onClick={handleDisconnect}
              disabled={!isConnected || isLoading}
              className="px-4 py-2 bg-red-600 text-white rounded-md hover:bg-red-700 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {isLoading && connectionStatus === "Connected"
                ? "Disconnecting..."
                : "Disconnect"}
            </button>
          </div>
        </div>
      </div>

      {/* Test Notification Sending */}
      <div className="mb-6 p-4 rounded-lg border">
        <h3 className="text-lg font-semibold mb-4">Send Test Notifications</h3>

        <div className="space-y-4">
          {/* Custom Message */}
          <div>
            <label
              htmlFor="customMessage"
              className="block text-sm font-medium text-gray-700 mb-1"
            >
              Custom Message (optional)
            </label>
            <input
              type="text"
              id="customMessage"
              value={customMessage}
              onChange={(e) => setCustomMessage(e.target.value)}
              className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-hidden focus:ring-2 focus:ring-blue-500"
              placeholder="Enter custom notification message"
            />
          </div>

          {/* Action Buttons */}
          <div className="flex flex-wrap gap-2">
            <button
              onClick={sendTestNotification}
              disabled={!isConnected || isLoading}
              className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              📢 Send Custom Notification
            </button>

            <button
              onClick={sendBugCreatedTest}
              disabled={!isConnected || isLoading}
              className="px-4 py-2 bg-purple-600 text-white rounded-md hover:bg-purple-700 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              🐛 Send Bug Created
            </button>

            <button
              onClick={sendBugUpdatedTest}
              disabled={!isConnected || isLoading}
              className="px-4 py-2 bg-orange-600 text-white rounded-md hover:bg-orange-700 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              📝 Send Bug Updated
            </button>
          </div>
        </div>
      </div>

      {/* Test Results */}
      {testResult && (
        <div className={`mb-6 p-4 rounded-lg border ${getResultColor()}`}>
          <h3 className="text-lg font-semibold mb-2">Test Result</h3>
          <p className="text-sm">{testResult}</p>
        </div>
      )}

      {/* Notifications Summary */}
      <div className="mb-6 p-4 rounded-lg border">
        <div className="flex items-center justify-between mb-4">
          <h3 className="text-lg font-semibold">
            Notifications Summary ({notifications.length} total, {unreadCount}{" "}
            unread)
          </h3>
          {notifications.length > 0 && (
            <button
              onClick={clearNotifications}
              className="px-3 py-1 text-sm bg-gray-500 text-white rounded hover:bg-gray-600"
            >
              Clear All
            </button>
          )}
        </div>

        <div className="space-y-2 max-h-64 overflow-y-auto">
          {notifications.length === 0 ? (
            <p className="text-gray-500 text-center py-4">
              No notifications received yet
            </p>
          ) : (
            notifications.slice(0, 10).map((notification, index) => (
              <div
                key={notification.id || index}
                className={`p-3 rounded border ${
                  !notification.isRead
                    ? "bg-blue-50 border-blue-200"
                    : "bg-gray-50 border-gray-200"
                }`}
              >
                <div className="flex justify-between items-start">
                  <div className="flex-1">
                    <p
                      className={`text-sm ${
                        !notification.isRead ? "font-semibold" : ""
                      }`}
                    >
                      {notification.message}
                    </p>
                    {notification.data && (
                      <p className="text-xs text-gray-600 mt-1">
                        {notification.data}
                      </p>
                    )}
                    <p className="text-xs text-gray-400 mt-1">
                      {new Date(notification.timestamp).toLocaleString()}
                    </p>
                  </div>
                  {!notification.isRead && (
                    <span className="inline-block w-2 h-2 bg-blue-500 rounded-full ml-2 mt-1"></span>
                  )}
                </div>
              </div>
            ))
          )}
          {notifications.length > 10 && (
            <p className="text-center text-sm text-gray-500">
              ... and {notifications.length - 10} more notifications
            </p>
          )}
        </div>
      </div>

      {/* Instructions */}
      <div className="p-4 bg-gray-50 rounded-lg">
        <h3 className="text-lg font-semibold mb-2">📋 How to Test</h3>
        <ol className="list-decimal list-inside space-y-1 text-sm text-gray-700">
          <li>Make sure the API server is running on port 5026</li>
          <li>Enter a User ID and click "Connect"</li>
          <li>Wait for the connection status to show "Connected"</li>
          <li>Click any of the "Send" buttons to test notifications</li>
          <li>
            Check the notifications bell in the header for real-time updates
          </li>
          <li>View received notifications in the summary below</li>
        </ol>
      </div>
    </div>
  );
};

export default NotificationTester;
