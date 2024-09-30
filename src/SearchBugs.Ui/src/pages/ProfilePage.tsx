import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";

import { Profile } from "@/modules/profile/Profile";
import { Overview } from "@/modules/profile/Overview";
import { Repositories } from "@/modules/profile/Repositories";
import { Repository } from "@/modules/profile/Repository";

const mockRepositories: Repository[] = [];
mockRepositories.push({
  name: "repo-name-1",
  description: "Short description of the repository",
  stars: 100,
  forks: 10,
  isPublic: true,
});
mockRepositories.push({
  name: "repo-name-2",
  description: "Short description of the repository",
  stars: 200,
  forks: 20,
  isPublic: true,
});
mockRepositories.push({
  name: "repo-name-3",
  description: "Short description of the repository",
  stars: 300,
  forks: 30,
  isPublic: true,
});
mockRepositories.push({
  name: "repo-name-4",
  description: "Short description of the repository",
  stars: 400,
  forks: 40,
  isPublic: true,
});
mockRepositories.push({
  name: "repo-name-5",
  description: "Short description of the repository",
  stars: 500,
  forks: 50,
  isPublic: true,
});

export const ProfilePage: React.FC = () => {
  return (
    <main className="container mx-auto p-4">
      <div className="grid gap-6 md:grid-cols-4">
        <div className="md:col-span-1">
          <Profile
            displayName="Vichea Nath"
            username="vicheanath"
            bio="Software Engineer"
            followers={100}
            following={50}
            location="New York, USA"
            profileImage="https://avatars.githubusercontent.com/u/48352653"
          />
        </div>
        <div className="md:col-span-3 space-y-6">
          <Tabs defaultValue="overview">
            <TabsList>
              <TabsTrigger value="overview">Overview</TabsTrigger>
              <TabsTrigger value="repositories">Repositories</TabsTrigger>
              <TabsTrigger value="projects">Projects</TabsTrigger>
              <TabsTrigger value="packages">Packages</TabsTrigger>
            </TabsList>
            <TabsContent value="overview" className="space-y-6">
              <Overview />
            </TabsContent>
            <TabsContent value="repositories">
              <Repositories repositories={mockRepositories} />
            </TabsContent>
            <TabsContent value="projects">Projects</TabsContent>
            <TabsContent value="packages">Packages</TabsContent>
          </Tabs>
        </div>
      </div>
    </main>
  );
};

export default ProfilePage;
