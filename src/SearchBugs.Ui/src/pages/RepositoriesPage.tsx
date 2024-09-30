import React from "react";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Badge } from "@/components/ui/badge";
import {
  AlertCircle,
  Book,
  Eye,
  Code,
  GitFork,
  GitPullRequest,
  Play,
  Star,
} from "lucide-react";
import { CodeTree } from "@/modules/repository/CodeTree";

export const RepositoryPage: React.FC = () => {

  return (
    <div className="container mx-auto p-4">
      <div className="mb-4 flex items-center justify-between">
        <div className="flex items-center space-x-2">
          <Book className="h-5 w-5" />
          <h1 className="text-xl font-semibold">
            <a href="#" className="text-blue-600 hover:underline">
              username
            </a>{" "}
            /{" "}
            <a href="#" className="text-blue-600 hover:underline">
              repository-name
            </a>
          </h1>
          <Badge variant="outline">Public</Badge>
        </div>
        <div className="flex space-x-2">
          <Button variant="outline" size="sm">
            <Eye className="mr-2 h-4 w-4" />
            Watch
          </Button>
          <Button variant="outline" size="sm">
            <GitFork className="mr-2 h-4 w-4" />
            Fork
          </Button>
          <Button variant="outline" size="sm">
            <Star className="mr-2 h-4 w-4" />
            Star
          </Button>
        </div>
      </div>
      <Tabs defaultValue="code" className="mb-4">
        <TabsList>
          <TabsTrigger value="code">
            <Code className="mr-2 h-4 w-4" />
            Code
          </TabsTrigger>
          <TabsTrigger value="issues">
            <AlertCircle className="mr-2 h-4 w-4" />
            Issues
          </TabsTrigger>
          <TabsTrigger value="pull-requests">
            <GitPullRequest className="mr-2 h-4 w-4" />
            Pull requests
          </TabsTrigger>
          <TabsTrigger value="actions">
            <Play className="mr-2 h-4 w-4" />
            Actions
          </TabsTrigger>
          <TabsTrigger value="projects">
            <svg
              className="mr-2 h-4 w-4"
              fill="none"
              height="24"
              stroke="currentColor"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="2"
              viewBox="0 0 24 24"
              width="24"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path d="M8 6h10a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h2" />
              <path d="M12 4v4" />
              <path d="M16 4v4" />
              <path d="M8 4v4" />
            </svg>
            Projects
          </TabsTrigger>
        </TabsList>
        <TabsContent value="code" className="mt-4">
            <CodeTree />
        </TabsContent>
      </Tabs>
    </div>
  );
};
