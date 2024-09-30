import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import {
  GitPullRequest,
  Star,
  GitBranchPlus,
  GitFork,
  BugIcon,
  Eye as Watch,
} from 'lucide-react';
import React from "react";

export enum ActivityType {
  Push = "PushEvent",
  PullRequest = "PullRequestEvent",
  Issue = "IssueEvent",
  Create = "CreateEvent",
  Fork = "ForkEvent",
  Watch = "WatchEvent",
}

export interface Activity {
  type: ActivityType;
  actor: string;
  repository: string;
  createdAt: string;
  title: string;
  description: string;
  avatar: string;
}

interface RecentActivityProps {
  activities: Activity[];
}

export const RecentActivity: React.FC<RecentActivityProps> = ({ activities }) => {
  const icons = {
    [ActivityType.Push]: <GitBranchPlus className="h-6 w-6" />,
    [ActivityType.PullRequest]: <GitPullRequest className="h-6 w-6" />,
    [ActivityType.Issue]: <BugIcon className="h-6 w-6" />,
    [ActivityType.Create]: <Star className="h-6 w-6" />,
    [ActivityType.Fork]: <GitFork className="h-6 w-6" />,
    [ActivityType.Watch]: <Watch className="h-6 w-6" />,
  };
  return (
    <Card>
      <CardHeader>
        <CardTitle>Recent Activity</CardTitle>
      </CardHeader>
      <CardContent>
        <ul className="space-y-4">
          {activities.map((activity, index) => (
            <li key={index} className="flex items-center space-x-4">
              <div className="flex items-center">
                {icons[activity.type]}
              </div>
              <div>
                <p>
                  <span className="font-bold">{activity.actor}</span> {activity.title}{" "}
                  <a href="#" className="font-bold hover:underline">
                    {activity.repository}
                  </a>
                </p>
                <p>{activity.description}</p>
              </div>
            </li>
          ))}
        </ul>
      </CardContent>
    </Card>
  );
};
