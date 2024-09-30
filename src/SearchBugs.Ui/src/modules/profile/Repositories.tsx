import {
    Card,
    CardContent,
    CardHeader,
  } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { GitFork, Star } from "lucide-react";

import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
  } from "@/components/ui/select";
import { Repository } from "./Repository";



interface RepositoriesProps {
    repositories: Repository[];
}

export const Repositories : React.FC<RepositoriesProps> = ({repositories}) => {
    return (
        <Card>
        <CardHeader>
            {/* 
                feltering repositories by name , select type of repository (public or private), sort by stars, forks, and date created, language, and topics
            */}
            <div className="flex items-center space-x-4">
                <Input placeholder="Find a repository..." />
                <Button>Filter</Button>
                <Select>
                    <SelectTrigger>
                        <SelectValue placeholder="Sort by" />
                    </SelectTrigger>
                    <SelectContent>
                        <SelectItem value="stars">Stars</SelectItem>
                        <SelectItem value="forks">Forks</SelectItem>
                        <SelectItem value="date">Date created</SelectItem>
                    </SelectContent>
                </Select>
                <Select>
                    <SelectTrigger>
                        <SelectValue placeholder="Type" />
                    </SelectTrigger>
                    <SelectContent>
                        <SelectItem value="public">Public</SelectItem>
                        <SelectItem value="private">Private</SelectItem>
                    </SelectContent>
                </Select>
                <Select>
                    <SelectTrigger>
                        <SelectValue placeholder="Language" />
                    </SelectTrigger>
                    <SelectContent>
                        <SelectItem value="javascript">JavaScript</SelectItem>
                        <SelectItem value="typescript">TypeScript</SelectItem>
                        <SelectItem value="python">Python</SelectItem>
                    </SelectContent>
                </Select>

            </div>

            
        </CardHeader>
        <CardContent>
          <ul className="space-y-4">
            {repositories.map((item, index) => (
                <div key={index} className="flex items-start space-x-4">
                    <div>
                        <h1 className="text-lg font-semibold"></h1>
                            <a href="#" className="hover:underline">
                               {item.name}
                            </a>
                        <p className="text-sm">
                            {item.description}
                        </p>
                    </div>
                    <div className="flex space-x-4 text-sm text-gray-500">
                        <span className="flex items-center">
                            <Star className="mr-1 h-4 w-4" />
                            {item.stars}
                        </span>
                        <span className="flex items-center">
                            <GitFork className="mr-1 h-4 w-4" />
                            {item.forks}
                        </span>
                        <Badge>
                            {item.isPublic ? "Public" : "Private"}
                        </Badge>
                    </div>
                </div>
            ))}
          </ul>
        </CardContent>
      </Card>
    );
}