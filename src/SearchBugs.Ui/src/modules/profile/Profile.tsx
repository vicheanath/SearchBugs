import { Card, CardContent } from "@/components/ui/card";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { MapPin, Users } from "lucide-react";
import { Button } from "@/components/ui/button";

interface ProfileProps {
  displayName: string;
  username: string;
  profileImage: string;
  bio: string;
  followers: number;
  following: number;
  location: string;
}

export const Profile: React.FC<ProfileProps> = ({
  displayName,
  username,
  bio,
  followers,
  following,
  location,
    profileImage,
}) => {
  return (
    <Card>
      <CardContent className="pt-6">
        <div className="mb-4 text-center">
          <Avatar className="mx-auto h-40 w-40">
            <AvatarImage alt={username} src={profileImage} />
            <AvatarFallback>JD</AvatarFallback>
          </Avatar>
          <h1 className="mt-4 text-2xl font-bold">{displayName}</h1>
          <p className="text-gray-600">@{username}</p>
        </div>
        <div className="space-y-4">
          <p>{bio}</p>
          <div className="flex items-center space-x-2">
            <Users className="h-4 w-4 text-gray-500" />
            <span>
              {followers} followers · {following} following
            </span>
          </div>
          <div className="flex items-center space-x-2">
            <MapPin className="h-4 w-4 text-gray-500" />
            <span>{location}</span>
          </div>
          <Button className="w-full">Edit profile</Button>
        </div>
      </CardContent>
    </Card>
  );
};
