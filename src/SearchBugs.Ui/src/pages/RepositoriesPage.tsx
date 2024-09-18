import { ListFilter, MoreHorizontal, PlusCircle } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardFooter } from "@/components/ui/card";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";

import {
  DropdownMenuCheckboxItem,
  DropdownMenuSeparator,
} from "@/components/ui/dropdown-menu";

import { useApi } from "@/hooks/useApi";
import { useNavigate } from "react-router-dom";
interface Repository {
  id: string;
  name: string;
  url: string;
  description: string;
  createdOnUtc: string;
}

export const RepositoriesPage = () => {
  const navigate = useNavigate();
  const { data } = useApi<Repository>("repo");
  return (
    <div className="flex flex-col gap-3 md:gap-8">
      <div className="flex items-center">
        <h5 className="text-lg font-semibold">Bugs</h5>
        <div className="ml-auto flex items-center gap-2">
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline" size="sm" className="h-7 gap-1">
                <ListFilter className="h-3.5 w-3.5" />
                <span className="sr-only sm:not-sr-only sm:whitespace-nowrap">
                  Filter
                </span>
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuLabel>Filter by</DropdownMenuLabel>
              <DropdownMenuSeparator />
              <DropdownMenuCheckboxItem checked>Open</DropdownMenuCheckboxItem>
              <DropdownMenuCheckboxItem>In Progress</DropdownMenuCheckboxItem>
              <DropdownMenuCheckboxItem>Closed</DropdownMenuCheckboxItem>
            </DropdownMenuContent>
          </DropdownMenu>
          <Button
            size="sm"
            className="h-7 gap-1"
            onClick={() => navigate("/repositories/add")}
          >
            <PlusCircle className="h-3.5 w-3.5" />
            <span className="sr-only sm:not-sr-only sm:whitespace-nowrap">
              Add Bug
            </span>
          </Button>
        </div>
      </div>
      <Card x-chunk="dashboard-06-chunk-0">
        <CardContent>
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Name</TableHead>
                <TableHead>Description</TableHead>
                <TableHead>
                  <span className="sr-only">Actions</span>
                </TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {data?.value.map((repo) => (
                <TableRow key={repo.id}>
                  <TableCell>{repo.name}</TableCell>
                  <TableCell>{repo.description}</TableCell>
                  <TableCell>
                    <DropdownMenu>
                      <DropdownMenuTrigger asChild>
                        <Button variant="outline" size="sm" className="h-7 w-7">
                          <MoreHorizontal className="h-3.5 w-3.5" />
                          <span className="sr-only">Actions</span>
                        </Button>
                      </DropdownMenuTrigger>
                      <DropdownMenuContent align="end">
                        <DropdownMenuLabel>Actions</DropdownMenuLabel>
                        <DropdownMenuSeparator />
                        <DropdownMenuItem
                          onClick={() => navigate(`/repositories/${repo.id}`)}
                        >
                          View
                        </DropdownMenuItem>
                        <DropdownMenuItem
                          onClick={() =>
                            navigate(`/repositories/${repo.id}/edit`)
                          }
                        >
                          Edit
                        </DropdownMenuItem>
                        <DropdownMenuItem>Delete</DropdownMenuItem>
                      </DropdownMenuContent>
                    </DropdownMenu>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </CardContent>
        <CardFooter>
            <span className="text-sm text-gray-500">
                {data?.value.length} repositories
            </span>
        </CardFooter>
      </Card>
    </div>
  );
};
