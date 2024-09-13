import { Button } from "@/components/ui/button";
import { Card, CardContent } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import { FieldValues, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { useMutation } from "react-query";
import { api } from "@/lib/api";
import { useToast } from "@/components/ui/use-toast";
import { useNavigate } from "react-router-dom";

export const CreateProjectPage = () => {
  const CreateProjectSchema = z.object({
    name: z
      .string()
      .min(3, "Name must be at least 3 characters long")
      .max(50, "Name must be at most 50 characters long"),
    description: z
      .string()
      .max(500, "Description must be at most 500 characters long"),
  });
  const { toast } = useToast();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(CreateProjectSchema),
  });

  const createProjectMutation = useMutation((data: FieldValues) => {
    return api.post("/projects", data).then(() =>
      toast({
        title: "Project created",
        description: "The project has been created successfully",
      })
    );
  });

  const onSubmit = (data: FieldValues) => {
    createProjectMutation.mutate(data);
    navigate("/projects");
  };

  return (
    <main className="flex items-start gap-4 p-4 ">
      <div className="mx-auto grid flex-1 auto-rows-max gap-4">
        <div className="flex items-center gap-4">
          <h1 className="flex-1 shrink-0 whitespace-nowrap text-xl font-semibold tracking-tight sm:grow-0">
            Create Project
          </h1>
        </div>
        <Card className="flex-1 pt-5">
          <CardContent>
            <form
              onSubmit={handleSubmit(onSubmit)}
              className="flex flex-col gap-4"
            >
              <div className="flex flex-col gap-4">
                <div className="flex flex-col gap-1">
                  <Label htmlFor="name">Name</Label>
                  <Input id="name" type="text" {...register("name")} />
                  {errors.name?.message && (
                    <p className="text-sm text-red-500">
                      {errors.name?.message.toString()}
                    </p>
                  )}
                </div>
                <div>
                  <Label htmlFor="description">Description</Label>
                  <Textarea id="description" {...register("description")} />
                  {errors.description?.message && (
                    <p className="text-sm text-red-500">
                      {errors.description?.message.toString()}
                    </p>
                  )}
                </div>
              </div>
              <div className="mt-4">
                <Button type="submit">Create</Button>
              </div>
            </form>
          </CardContent>
        </Card>
      </div>
    </main>
  );
};
