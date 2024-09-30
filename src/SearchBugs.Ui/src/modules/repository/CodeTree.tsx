import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { GitBranch } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";


export const CodeTree: React.FC = () => {
  return (
    <>
      <div className="mb-4">
        <div className="flex items-center justify-between">
          <div className="flex items-center space-x-2">
            <GitBranch className="h-5 w-5" />
            <span className="font-semibold">main</span>
          </div>

          <div className="flex items-center space-x-2">
            <Select>
              <SelectTrigger>
                <Button variant="outline" size="sm">
                  <span className="sr-only">Branch</span>
                  <span>Branch: main</span>
                </Button>
              </SelectTrigger>
              <SelectContent align="end">
                <SelectItem value="main">main</SelectItem>
                <SelectItem value="develop">develop</SelectItem>
                <SelectItem value="feature">feature</SelectItem>
              </SelectContent>
            </Select>
          </div>
        </div>
      </div>
      <Card>
        <CardHeader></CardHeader>
        <CardContent>
          <div className="space-y-2">
            <div className="flex items-center justify-between rounded-md border p-2">
              <div className="flex items-center space-x-2">
                <svg
                  className=" h-5 w-5 text-yellow-400"
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
                  <path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z" />
                  <polyline points="14 2 14 8 20 8" />
                </svg>
                <span>README.md</span>
              </div>
              <span className="text-sm text-gray-500">Updated 3 days ago</span>
            </div>
            <div className="flex items-center justify-between rounded-md border p-2">
              <div className="flex items-center space-x-2">
                <svg
                  className=" h-5 w-5 text-blue-500"
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
                  <path d="m18 16 4-4-4-4" />
                  <path d="m6 8-4 4 4 4" />
                  <path d="m14.5 4-5 16" />
                </svg>
                <span>index.js</span>
              </div>
              <span className="text-sm text-gray-500">Updated 1 week ago</span>
            </div>
            <div className="flex items-center justify-between rounded-md border p-2">
              <div className="flex items-center space-x-2">
                <svg
                  className=" h-5 w-5 text-orange-500"
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
                  <path d="M21 12V7H5a2 2 0 0 1 0-4h14v4" />
                  <path d="M3 5v14a2 2 0 0 0 2 2h16" />
                  <path d="M18 12h-2" />
                  <path d="M15 9h-2" />
                  <path d="M15 15h-2" />
                </svg>
                <span>package.json</span>
              </div>
              <span className="text-sm text-gray-500">Updated 2 weeks ago</span>
            </div>
          </div>
        </CardContent>
      </Card>
      <Card className="mt-4">
        <CardHeader className="border-b">
          <CardTitle>README.md</CardTitle>
        </CardHeader>
        <CardContent>
          The README.md file is a markdown file that contains information about
          the repository.
        </CardContent>
      </Card>
    </>
  );
};
