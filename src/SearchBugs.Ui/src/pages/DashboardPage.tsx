import React from "react";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Book, GitFork, Star } from "lucide-react";

export const DashboardPage: React.FC = () => {
  return (
    <main className="container mx-auto p-4">
    <div className="mb-8 flex items-center justify-between">
      <div className="flex items-center space-x-4">
        <Avatar className="h-16 w-16">
          <AvatarImage alt="@username" src="/placeholder-user.jpg" />
          <AvatarFallback>UN</AvatarFallback>
        </Avatar>
        <div>
          <h1 className="text-2xl font-bold">John Doe</h1>
          <p className="text-gray-600">@johndoe</p>
        </div>
      </div>
      <Button>Edit profile</Button>
    </div>
    <div className="grid gap-6 md:grid-cols-3">
      <Card className="col-span-2">
        <CardHeader>
          <CardTitle>Recent Activity</CardTitle>
        </CardHeader>
        <CardContent>
          <ul className="space-y-4">
            {[1, 2, 3].map((_, index) => (
              <li key={index} className="flex items-start space-x-4">
                <Avatar className="mt-1">
                  <AvatarImage
                    alt="@username"
                    src="/placeholder-user.jpg"
                  />
                  <AvatarFallback>UN</AvatarFallback>
                </Avatar>
                <div>
                  <p className="text-sm">
                    <span className="font-semibold">johndoe</span> created a
                    pull request in{" "}
                    <span className="font-semibold">organization/repo</span>
                  </p>
                  <p className="text-xs text-gray-500">2 hours ago</p>
                </div>
              </li>
            ))}
          </ul>
        </CardContent>
      </Card>
      <Card>
        <CardHeader>
          <CardTitle>Popular Repositories</CardTitle>
        </CardHeader>
        <CardContent>
          <ul className="space-y-4">
            {[1, 2, 3].map((_, index) => (
              <li key={index}>
                <h3 className="text-lg font-semibold">
                  <Book className="mr-2 inline-block h-4 w-4" />
                  <a href="#" className="hover:underline">
                    repo-name-{index + 1}
                  </a>
                </h3>
                <p className="text-sm text-gray-600">
                  Short description of the repository
                </p>
                <div className="mt-2 flex space-x-4 text-sm text-gray-500">
                  <span className="flex items-center">
                    <Star className="mr-1 h-4 w-4" />
                    {Math.floor(Math.random() * 1000)}
                  </span>
                  <span className="flex items-center">
                    <GitFork className="mr-1 h-4 w-4" />
                    {Math.floor(Math.random() * 100)}
                  </span>
                </div>
              </li>
            ))}
          </ul>
        </CardContent>
      </Card>
    </div>
    <Card className="mt-6">
      <CardHeader>
        <CardTitle>Contribution Activity</CardTitle>
      </CardHeader>
      <CardContent>
        <div className="h-32 w-full bg-gray-200">
          {/* Placeholder for contribution graph */}
          <div className="flex h-full items-end justify-between p-2">
            {[...Array(52)].map((_, index) => (
              <div
                key={index}
                className="w-2"
                style={{
                  height: `${Math.floor(Math.random() * 100)}%`,
                  backgroundColor: `rgb(${Math.floor(
                    Math.random() * 200
                  )}, 230, ${Math.floor(Math.random() * 200)})`,
                }}
              />
            ))}
          </div>
        </div>
      </CardContent>
    </Card>
    <div className="mt-6 grid gap-6 md:grid-cols-2">
      <Card>
        <CardHeader>
          <CardTitle>Organizations</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="flex space-x-4">
            {[1, 2, 3, 4].map((_, index) => (
              <Avatar key={index} className="h-12 w-12">
                <AvatarImage
                  alt={`Organization ${index + 1}`}
                  src={`/placeholder-org-${index + 1}.jpg`}
                />
                <AvatarFallback>O{index + 1}</AvatarFallback>
              </Avatar>
            ))}
          </div>
        </CardContent>
      </Card>
      <Card>
        <CardHeader>
          <CardTitle>Projects</CardTitle>
        </CardHeader>
        <CardContent>
          <ul className="space-y-2">
            {[1, 2, 3].map((_, index) => (
              <li key={index} className="flex items-center justify-between">
                <span className="font-medium">Project {index + 1}</span>
                <span className="text-sm text-gray-500">
                  Updated 2 days ago
                </span>
              </li>
            ))}
          </ul>
        </CardContent>
      </Card>
    </div>
  </main>
  );
};
