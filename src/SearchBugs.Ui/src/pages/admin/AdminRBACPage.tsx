import React, { useState } from "react";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Badge } from "@/components/ui/badge";
import { Shield, Users, Grid3X3, Settings } from "lucide-react";
import { RoleManagement } from "./components/RoleManagement";
import { UserRoleManagement } from "./components/UserRoleManagement";
import { PermissionMatrix } from "./components/PermissionMatrix";

export const AdminRBACPage: React.FC = () => {
  const [activeTab, setActiveTab] = useState("roles");

  return (
    <div className="container mx-auto px-4 py-6 max-w-7xl">
      {/* Page Header */}
      <div className="mb-8">
        <div className="flex items-center gap-3 mb-2">
          <div className="p-2 bg-blue-100 dark:bg-blue-900 rounded-lg">
            <Shield className="h-6 w-6 text-blue-600 dark:text-blue-400" />
          </div>
          <div>
            <h1 className="text-3xl font-bold text-gray-900 dark:text-gray-100">
              Role-Based Access Control
            </h1>
            <p className="text-gray-600 dark:text-gray-400 mt-1">
              Manage roles, permissions, and user access across the system
            </p>
          </div>
        </div>

        {/* Quick Stats */}
        <div className="flex flex-wrap gap-3 mt-4">
          <Badge variant="outline" className="flex items-center gap-1">
            <Shield className="h-3 w-3" />
            Admin Access Required
          </Badge>
          <Badge variant="secondary" className="flex items-center gap-1">
            <Settings className="h-3 w-3" />
            System Management
          </Badge>
        </div>
      </div>

      {/* Main Content */}
      <Card className="border-0 shadow-xs">
        <CardHeader className="border-b bg-gray-50 dark:bg-gray-900">
          <CardTitle className="text-xl">Access Control Management</CardTitle>
          <CardDescription>
            Configure roles, assign permissions, and manage user access levels
          </CardDescription>
        </CardHeader>
        <CardContent className="p-0">
          <Tabs
            value={activeTab}
            onValueChange={setActiveTab}
            className="w-full"
          >
            <TabsList className="grid w-full grid-cols-3 bg-gray-100 dark:bg-gray-800 m-6 mb-0">
              <TabsTrigger
                value="roles"
                className="flex items-center gap-2 data-[state=active]:bg-white dark:data-[state=active]:bg-gray-700"
              >
                <Shield className="h-4 w-4" />
                <span className="hidden sm:inline">Roles & Permissions</span>
                <span className="sm:hidden">Roles</span>
              </TabsTrigger>
              <TabsTrigger
                value="users"
                className="flex items-center gap-2 data-[state=active]:bg-white dark:data-[state=active]:bg-gray-700"
              >
                <Users className="h-4 w-4" />
                <span className="hidden sm:inline">User Roles</span>
                <span className="sm:hidden">Users</span>
              </TabsTrigger>
              <TabsTrigger
                value="matrix"
                className="flex items-center gap-2 data-[state=active]:bg-white dark:data-[state=active]:bg-gray-700"
              >
                <Grid3X3 className="h-4 w-4" />
                <span className="hidden sm:inline">Permission Matrix</span>
                <span className="sm:hidden">Matrix</span>
              </TabsTrigger>
            </TabsList>

            <div className="p-6">
              <TabsContent value="roles" className="mt-0">
                <div className="space-y-4">
                  <div>
                    <h3 className="text-lg font-semibold mb-2">
                      Role & Permission Management
                    </h3>
                    <p className="text-sm text-muted-foreground mb-6">
                      Create and manage roles, then assign specific permissions
                      to each role. This defines what actions users with each
                      role can perform.
                    </p>
                  </div>
                  <RoleManagement />
                </div>
              </TabsContent>

              <TabsContent value="users" className="mt-0">
                <div className="space-y-4">
                  <div>
                    <h3 className="text-lg font-semibold mb-2">
                      User Role Assignment
                    </h3>
                    <p className="text-sm text-muted-foreground mb-6">
                      Assign roles to users to grant them specific permissions.
                      Users can have multiple roles, and their effective
                      permissions will be the union of all assigned roles.
                    </p>
                  </div>
                  <UserRoleManagement />
                </div>
              </TabsContent>

              <TabsContent value="matrix" className="mt-0">
                <div className="space-y-4">
                  <div>
                    <h3 className="text-lg font-semibold mb-2">
                      Permission Matrix Overview
                    </h3>
                    <p className="text-sm text-muted-foreground mb-6">
                      View a comprehensive matrix showing all permissions across
                      all roles. This provides a quick overview of the entire
                      permission structure.
                    </p>
                  </div>
                  <PermissionMatrix />
                </div>
              </TabsContent>
            </div>
          </Tabs>
        </CardContent>
      </Card>
    </div>
  );
};
