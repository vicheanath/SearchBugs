import React, { useState } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { FormInput } from "@/components/ui/form";
import { Badge } from "@/components/ui/badge";
import { Switch } from "@/components/ui/switch";
import { Label } from "@/components/ui/label";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import {
  Shield,
  Lock,
  Smartphone,
  Eye,
  Key,
  AlertTriangle,
  RefreshCw,
  Trash2,
  Bell,
  Database,
  Globe,
  ExternalLink,
  Plus,
} from "lucide-react";
import {
  useSecuritySettings,
  useNotificationSettings,
  useIntegrationSettings,
  useDataExport,
  useAccountDeletion,
} from "@/hooks/useSettings";

// Security Settings Component
const passwordSchema = z
  .object({
    currentPassword: z.string().min(1, "Current password is required"),
    newPassword: z.string().min(8, "Password must be at least 8 characters"),
    confirmPassword: z.string(),
  })
  .refine((data) => data.newPassword === data.confirmPassword, {
    message: "Passwords don't match",
    path: ["confirmPassword"],
  });

type PasswordFormData = z.infer<typeof passwordSchema>;

export const SecuritySettingsPanel: React.FC = () => {
  const {
    securitySettings,
    isLoading,
    updatePassword,
    isUpdatingPassword,
    toggle2FA,
    isToggling2FA,
  } = useSecuritySettings();
  const [showPasswordForm, setShowPasswordForm] = useState(false);

  const {
    control,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<PasswordFormData>({
    resolver: zodResolver(passwordSchema),
  });

  const onPasswordSubmit = (data: PasswordFormData) => {
    updatePassword({
      currentPassword: data.currentPassword,
      newPassword: data.newPassword,
    });
    reset();
    setShowPasswordForm(false);
  };

  if (isLoading) {
    return <div>Loading security settings...</div>;
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle className="text-lg flex items-center gap-2">
          <Shield className="h-5 w-5" />
          Security Settings
        </CardTitle>
      </CardHeader>
      <CardContent className="space-y-6">
        {/* Password Section */}
        <div className="space-y-4">
          <div className="flex items-center justify-between p-4 border rounded-lg">
            <div className="flex items-center gap-3">
              <Lock className="h-5 w-5 text-muted-foreground" />
              <div>
                <h4 className="font-medium">Password</h4>
                <p className="text-sm text-muted-foreground">
                  Last changed{" "}
                  {new Date(
                    securitySettings?.lastPasswordChange || ""
                  ).toLocaleDateString()}
                </p>
              </div>
            </div>
            <Button
              variant="outline"
              size="sm"
              onClick={() => setShowPasswordForm(!showPasswordForm)}
            >
              Change Password
            </Button>
          </div>

          {/* Password Change Form */}
          {showPasswordForm && (
            <Card className="ml-8">
              <CardContent className="pt-4">
                <form
                  onSubmit={handleSubmit(onPasswordSubmit)}
                  className="space-y-4"
                >
                  <FormInput
                    control={control}
                    name="currentPassword"
                    label="Current Password"
                    type="password"
                    required
                    error={errors.currentPassword}
                  />
                  <FormInput
                    control={control}
                    name="newPassword"
                    label="New Password"
                    type="password"
                    required
                    error={errors.newPassword}
                  />
                  <FormInput
                    control={control}
                    name="confirmPassword"
                    label="Confirm New Password"
                    type="password"
                    required
                    error={errors.confirmPassword}
                  />
                  <div className="flex gap-2">
                    <Button
                      type="submit"
                      size="sm"
                      disabled={isUpdatingPassword}
                    >
                      {isUpdatingPassword ? (
                        <>
                          <RefreshCw className="h-4 w-4 mr-2 animate-spin" />
                          Updating...
                        </>
                      ) : (
                        "Update Password"
                      )}
                    </Button>
                    <Button
                      type="button"
                      variant="outline"
                      size="sm"
                      onClick={() => setShowPasswordForm(false)}
                    >
                      Cancel
                    </Button>
                  </div>
                </form>
              </CardContent>
            </Card>
          )}
        </div>

        {/* Two-Factor Authentication */}
        <div className="flex items-center justify-between p-4 border rounded-lg">
          <div className="flex items-center gap-3">
            <Smartphone className="h-5 w-5 text-muted-foreground" />
            <div>
              <h4 className="font-medium">Two-Factor Authentication</h4>
              <p className="text-sm text-muted-foreground">
                {securitySettings?.twoFactorEnabled
                  ? "Two-factor authentication is enabled"
                  : "Add an extra layer of security"}
              </p>
            </div>
          </div>
          <div className="flex items-center gap-2">
            {securitySettings?.twoFactorEnabled && (
              <Badge variant="secondary">Enabled</Badge>
            )}
            <Button
              variant={
                securitySettings?.twoFactorEnabled ? "destructive" : "default"
              }
              size="sm"
              onClick={() => toggle2FA(!securitySettings?.twoFactorEnabled)}
              disabled={isToggling2FA}
            >
              {isToggling2FA ? (
                <RefreshCw className="h-4 w-4 animate-spin" />
              ) : securitySettings?.twoFactorEnabled ? (
                "Disable 2FA"
              ) : (
                "Enable 2FA"
              )}
            </Button>
          </div>
        </div>

        {/* API Keys */}
        <div className="flex items-center justify-between p-4 border rounded-lg">
          <div className="flex items-center gap-3">
            <Key className="h-5 w-5 text-muted-foreground" />
            <div>
              <h4 className="font-medium">API Keys</h4>
              <p className="text-sm text-muted-foreground">
                {securitySettings?.activeTokens || 0} active tokens
              </p>
            </div>
          </div>
          <Button variant="outline" size="sm">
            Manage Keys
          </Button>
        </div>

        {/* Login History */}
        <div className="space-y-4">
          <h4 className="font-medium flex items-center gap-2">
            <Eye className="h-4 w-4" />
            Recent Login Activity
          </h4>
          <div className="space-y-2">
            {securitySettings?.loginHistory?.map((login) => (
              <div
                key={login.id}
                className="flex items-center justify-between p-3 border rounded-lg text-sm"
              >
                <div>
                  <p className="font-medium">{login.location}</p>
                  <p className="text-muted-foreground">{login.device}</p>
                </div>
                <div className="text-right">
                  <p>{new Date(login.timestamp).toLocaleDateString()}</p>
                  <p className="text-muted-foreground">{login.ipAddress}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </CardContent>
    </Card>
  );
};

// Notification Settings Component
export const NotificationSettingsPanel: React.FC = () => {
  const { notificationSettings, isLoading, updateNotifications, isUpdating } =
    useNotificationSettings();
  const [settings, setSettings] = useState(notificationSettings);

  React.useEffect(() => {
    if (notificationSettings) {
      setSettings(notificationSettings);
    }
  }, [notificationSettings]);

  const handleToggle = (
    section: "emailNotifications" | "pushNotifications",
    key: string
  ) => {
    if (!settings) return;

    const newSettings = {
      ...settings,
      [section]: {
        ...settings[section],
        [key]:
          !settings[section][key as keyof (typeof settings)[typeof section]],
      },
    };
    setSettings(newSettings);
  };

  const handleSave = () => {
    if (settings) {
      updateNotifications(settings);
    }
  };

  if (isLoading) {
    return <div>Loading notification settings...</div>;
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle className="text-lg flex items-center gap-2">
          <Bell className="h-5 w-5" />
          Notification Preferences
        </CardTitle>
      </CardHeader>
      <CardContent className="space-y-6">
        {/* Email Notifications */}
        <div className="space-y-4">
          <h4 className="font-medium">Email Notifications</h4>
          <div className="space-y-3">
            {Object.entries(settings?.emailNotifications || {}).map(
              ([key, value]) => (
                <div key={key} className="flex items-center justify-between">
                  <Label
                    htmlFor={`email-${key}`}
                    className="flex-1 cursor-pointer"
                  >
                    {key
                      .replace(/([A-Z])/g, " $1")
                      .replace(/^./, (str) => str.toUpperCase())}
                  </Label>
                  <Switch
                    id={`email-${key}`}
                    checked={value}
                    onCheckedChange={() =>
                      handleToggle("emailNotifications", key)
                    }
                  />
                </div>
              )
            )}
          </div>
        </div>

        {/* Push Notifications */}
        <div className="space-y-4">
          <h4 className="font-medium">Push Notifications</h4>
          <div className="space-y-3">
            {Object.entries(settings?.pushNotifications || {}).map(
              ([key, value]) => (
                <div key={key} className="flex items-center justify-between">
                  <Label
                    htmlFor={`push-${key}`}
                    className="flex-1 cursor-pointer"
                  >
                    {key
                      .replace(/([A-Z])/g, " $1")
                      .replace(/^./, (str) => str.toUpperCase())}
                  </Label>
                  <Switch
                    id={`push-${key}`}
                    checked={value}
                    onCheckedChange={() =>
                      handleToggle("pushNotifications", key)
                    }
                  />
                </div>
              )
            )}
          </div>
        </div>

        {/* Save Button */}
        <div className="flex justify-end">
          <Button onClick={handleSave} disabled={isUpdating}>
            {isUpdating ? (
              <>
                <RefreshCw className="h-4 w-4 mr-2 animate-spin" />
                Saving...
              </>
            ) : (
              "Save Preferences"
            )}
          </Button>
        </div>
      </CardContent>
    </Card>
  );
};

// Integration Settings Component
export const IntegrationSettingsPanel: React.FC = () => {
  const {
    integrationSettings,
    isLoading,
    connectIntegration,
    disconnectIntegration,
    isConnecting,
  } = useIntegrationSettings();

  const integrations = [
    {
      key: "github",
      name: "GitHub",
      icon: "🐙",
      description: "Sync repositories and issues",
    },
    {
      key: "slack",
      name: "Slack",
      icon: "💬",
      description: "Get notifications in your workspace",
    },
    {
      key: "discord",
      name: "Discord",
      icon: "🎮",
      description: "Team communication and alerts",
    },
    {
      key: "jira",
      name: "Jira",
      icon: "🔷",
      description: "Project management integration",
    },
  ];

  if (isLoading) {
    return <div>Loading integration settings...</div>;
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle className="text-lg flex items-center gap-2">
          <Globe className="h-5 w-5" />
          Integrations
        </CardTitle>
      </CardHeader>
      <CardContent className="space-y-4">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          {integrations.map((integration) => {
            const setting =
              integrationSettings?.[
                integration.key as keyof typeof integrationSettings
              ];
            const isConnected = setting?.connected;

            return (
              <div key={integration.key} className="p-4 border rounded-lg">
                <div className="flex items-center justify-between mb-3">
                  <div className="flex items-center gap-3">
                    <span className="text-2xl">{integration.icon}</span>
                    <div>
                      <h4 className="font-medium">{integration.name}</h4>
                      <p className="text-sm text-muted-foreground">
                        {integration.description}
                      </p>
                    </div>
                  </div>
                  {isConnected && <Badge variant="secondary">Connected</Badge>}
                </div>

                {isConnected && (
                  <div className="mb-3 text-sm text-muted-foreground">
                    {integration.key === "github" &&
                      "username" in setting &&
                      setting.username && <p>@{setting.username}</p>}
                    {integration.key === "slack" &&
                      "workspaceName" in setting &&
                      setting.workspaceName && <p>{setting.workspaceName}</p>}
                    {integration.key === "jira" &&
                      "instance" in setting &&
                      setting.instance && <p>{setting.instance}</p>}
                  </div>
                )}

                <div className="flex gap-2">
                  {isConnected ? (
                    <Button
                      variant="outline"
                      size="sm"
                      onClick={() => disconnectIntegration(integration.key)}
                      disabled={isConnecting}
                    >
                      Disconnect
                    </Button>
                  ) : (
                    <Button
                      variant="default"
                      size="sm"
                      onClick={() =>
                        connectIntegration({
                          service: integration.key,
                          config: {},
                        })
                      }
                      disabled={isConnecting}
                    >
                      {isConnecting ? (
                        <RefreshCw className="h-4 w-4 mr-2 animate-spin" />
                      ) : (
                        <Plus className="h-4 w-4 mr-2" />
                      )}
                      Connect
                    </Button>
                  )}
                  {isConnected && (
                    <Button variant="ghost" size="sm">
                      <ExternalLink className="h-4 w-4" />
                    </Button>
                  )}
                </div>
              </div>
            );
          })}
        </div>
      </CardContent>
    </Card>
  );
};

// Advanced Settings Component
export const AdvancedSettingsPanel: React.FC = () => {
  const { exportData, isExporting } = useDataExport();
  const { deleteAccount, isDeletingAccount } = useAccountDeletion();
  const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);
  const [deleteConfirmation, setDeleteConfirmation] = useState({
    password: "",
    confirmText: "",
  });

  const handleDeleteAccount = () => {
    deleteAccount(deleteConfirmation);
  };

  return (
    <Card>
      <CardHeader>
        <CardTitle className="text-lg flex items-center gap-2">
          <Globe className="h-5 w-5" />
          Advanced Settings
        </CardTitle>
      </CardHeader>
      <CardContent className="space-y-6">
        {/* Data Export */}
        <div className="flex items-center justify-between p-4 border rounded-lg">
          <div className="flex items-center gap-3">
            <Database className="h-5 w-5 text-muted-foreground" />
            <div>
              <h4 className="font-medium">Export Data</h4>
              <p className="text-sm text-muted-foreground">
                Download your account data in JSON format
              </p>
            </div>
          </div>
          <Button
            variant="outline"
            size="sm"
            onClick={() => exportData("json")}
            disabled={isExporting}
          >
            {isExporting ? (
              <>
                <RefreshCw className="h-4 w-4 mr-2 animate-spin" />
                Exporting...
              </>
            ) : (
              "Export Data"
            )}
          </Button>
        </div>

        {/* Danger Zone */}
        <div className="border border-red-200 rounded-lg p-4 bg-red-50/50">
          <div className="flex items-center justify-between mb-4">
            <div className="flex items-center gap-3">
              <AlertTriangle className="h-5 w-5 text-red-500" />
              <div>
                <h4 className="font-medium text-red-700">Danger Zone</h4>
                <p className="text-sm text-red-600">
                  These actions cannot be undone
                </p>
              </div>
            </div>
          </div>

          <div className="space-y-4">
            <div className="flex items-center justify-between">
              <div>
                <h5 className="font-medium text-red-700">Delete Account</h5>
                <p className="text-sm text-red-600">
                  Permanently delete your account and all data
                </p>
              </div>
              <Button
                variant="destructive"
                size="sm"
                onClick={() => setShowDeleteConfirmation(true)}
              >
                <Trash2 className="h-4 w-4 mr-2" />
                Delete Account
              </Button>
            </div>

            {/* Delete Confirmation Form */}
            {showDeleteConfirmation && (
              <Card className="bg-card border-destructive/20 dark:border-destructive/30">
                <CardContent className="pt-4">
                  <div className="space-y-4">
                    <p className="text-sm text-red-600">
                      This will permanently delete your account and all
                      associated data. This action cannot be undone.
                    </p>

                    <div className="space-y-2">
                      <Label>Type "DELETE MY ACCOUNT" to confirm:</Label>
                      <input
                        className="w-full px-3 py-2 border border-input rounded-md focus:outline-hidden focus:ring-2 focus:ring-primary/20"
                        value={deleteConfirmation.confirmText}
                        onChange={(e) =>
                          setDeleteConfirmation({
                            ...deleteConfirmation,
                            confirmText: e.target.value,
                          })
                        }
                        placeholder="DELETE MY ACCOUNT"
                      />
                    </div>

                    <div className="space-y-2">
                      <Label>Enter your password:</Label>
                      <input
                        type="password"
                        className="w-full px-3 py-2 border border-input rounded-md focus:outline-hidden focus:ring-2 focus:ring-primary/20"
                        value={deleteConfirmation.password}
                        onChange={(e) =>
                          setDeleteConfirmation({
                            ...deleteConfirmation,
                            password: e.target.value,
                          })
                        }
                        placeholder="Your password"
                      />
                    </div>

                    <div className="flex gap-2">
                      <Button
                        variant="destructive"
                        size="sm"
                        onClick={handleDeleteAccount}
                        disabled={
                          isDeletingAccount ||
                          deleteConfirmation.confirmText !==
                            "DELETE MY ACCOUNT" ||
                          !deleteConfirmation.password
                        }
                      >
                        {isDeletingAccount ? (
                          <>
                            <RefreshCw className="h-4 w-4 mr-2 animate-spin" />
                            Deleting...
                          </>
                        ) : (
                          <>
                            <Trash2 className="h-4 w-4 mr-2" />
                            Delete Account
                          </>
                        )}
                      </Button>
                      <Button
                        variant="outline"
                        size="sm"
                        onClick={() => {
                          setShowDeleteConfirmation(false);
                          setDeleteConfirmation({
                            password: "",
                            confirmText: "",
                          });
                        }}
                      >
                        Cancel
                      </Button>
                    </div>
                  </div>
                </CardContent>
              </Card>
            )}
          </div>
        </div>
      </CardContent>
    </Card>
  );
};
