import React, { useState } from "react";
import { ContributionActivity } from "@/modules/profile/ContibutionActivity";
import { Activity, ActivityType, RecentActivity } from "./RecentActivity";
import { PopularRepository } from "./PopularRepository";

export const Overview: React.FC = () => {
  const [selectedYear, setSelectedYear] = useState(
    new Date().getFullYear().toString()
  );
  const years = Array.from({ length: 5 }, (_, i) =>
    (new Date().getFullYear() - i).toString()
  );

  const activities : Activity[] = [
    {
      type: ActivityType.Push,
      actor: "vicheanath",
      repository: "search-bugs",
      createdAt: "2021-10-10",
      title: "pushed to",
      description: "Add new feature",
      avatar: "https://avatars.githubusercontent.com/u/48352653",
    },
    {
      type: ActivityType.PullRequest,
      actor: "vicheanath",
      repository: "search-bugs",
      createdAt: "2021-10-10",
      title: "opened a pull request in",
      description: "Add new feature",
      avatar: "https://avatars.githubusercontent.com/u/48352653",
    },
    {
      type: ActivityType.Issue,
      actor: "vicheanath",
      repository: "search-bugs",
      createdAt: "2021-10-10",
      title: "opened an issue in",
      description: "Add new feature",
      avatar: "https://avatars.githubusercontent.com/u/48352653",
    },
    {
      type: ActivityType.Create,
      actor: "vicheanath",
      repository: "search-bugs",
      createdAt: "2021-10-10",
      title: "created a repository",
      description: "You have created this repository",
      avatar: "https://avatars.githubusercontent.com/u/48352653",
    },
    {
      type: ActivityType.Fork,
      actor: "vicheanath",
      repository: "search-bugs",
      createdAt: "2021-10-10",
      title: "forked",
      description: "You have forked this repository",
      avatar: "https://avatars.githubusercontent.com/u/48352653",
    },
    {
      type: ActivityType.Watch,
      actor: "vicheanath",
      repository: "search-bugs",
      createdAt: "2021-10-10",
      title: "starred",
      description: "You have starred this repository",
      avatar: "https://avatars.githubusercontent.com/u/48352653",
    },
  ];

  return (
    <>
      <ContributionActivity
        years={years}
        selectedYear={selectedYear}
        setSelectedYear={setSelectedYear}
      />
      <PopularRepository />
      
      <RecentActivity activities={activities} />
    </>
  );
};
