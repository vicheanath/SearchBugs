import {
    Card,
    CardContent,
    CardDescription,
    CardHeader,
    CardTitle,
  } from "@/components/ui/card";
  import { Badge } from "@/components/ui/badge";
  import { Book, GitFork, Star } from "lucide-react";

export const PopularRepository: React.FC = () => {

    return (
        <Card>
        <CardHeader>
          <CardTitle className="text-lg">Popular repositories</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid gap-4 sm:grid-cols-2">
            {[1, 2, 3, 4].map((_, index) => (
              <Card key={index}>
                <CardHeader>
                  <CardTitle className="text-lg">
                    <Book className="mr-2 inline-block h-4 w-4" />
                    <a href="#" className="hover:underline">
                      repo-name-{index + 1}
                    </a>
                  </CardTitle>
                  <CardDescription>
                    Short description of the repository
                  </CardDescription>
                </CardHeader>
                <CardContent>
                  <div className="flex space-x-4 text-sm text-gray-500">
                    <span className="flex items-center">
                      <Star className="mr-1 h-4 w-4" />
                      {Math.floor(Math.random() * 1000)}
                    </span>
                    <span className="flex items-center">
                      <GitFork className="mr-1 h-4 w-4" />
                      {Math.floor(Math.random() * 100)}
                    </span>
                    <Badge>Public</Badge>
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>
        </CardContent>
      </Card>
    );

}