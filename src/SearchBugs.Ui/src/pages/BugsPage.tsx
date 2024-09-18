import { ListFilter, MoreHorizontal, PlusCircle } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardFooter,
} from "@/components/ui/card";
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
import { useNavigate } from "react-router-dom";
import { useApi } from "@/hooks/useApi";

interface Bug{
  id: string;
  title: string;
  description: string;
  status: string;
  reporter: string;
  assignee: string;
}

export const BugsPage = () => {
  const { data } = useApi<Bug>("bugs");
  const navigate = useNavigate();
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
            onClick={() => navigate("/add-bug")}
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
                <TableHead className="hidden md:table-cell">Status</TableHead>
                <TableHead className="hidden md:table-cell">Reporter</TableHead>
                <TableHead className="hidden md:table-cell">Assignee</TableHead>
                <TableHead>
                  <span className="sr-only">Actions</span>
                </TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {data?.value?.map((project: Bug) => (
                <TableRow key={project.id}>
                  <TableCell>{project.title}</TableCell>
                  <TableCell className="hidden md:table-cell">{project.description}</TableCell>
                  <TableCell >
                    {project.status}
                  </TableCell>
                  <TableCell className="hidden md:table-cell">
                    {project.reporter}
                  </TableCell>
                  <TableCell className="hidden md:table-cell">
                    {project.assignee}
                  </TableCell>
                  <TableCell>
                    <DropdownMenu>
                      <DropdownMenuTrigger asChild>
                        <Button
                          aria-haspopup="true"
                          size="icon"
                          variant="ghost"
                        >
                          <MoreHorizontal className="h-4 w-4" />
                          <span className="sr-only">Toggle menu</span>
                        </Button>
                      </DropdownMenuTrigger>
                      <DropdownMenuContent align="end">
                        <DropdownMenuLabel>Actions</DropdownMenuLabel>
                        <DropdownMenuItem>Edit</DropdownMenuItem>
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
            {data?.value?.length} bugs
          </span>
        </CardFooter>
      </Card>
    </div>
  );
};
