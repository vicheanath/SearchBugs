import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { FormInput } from "@/components/ui/form";
import { NavLink, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useAuth } from "@/hooks/useAuth";
import { useToast } from "@/components/ui/use-toast";
import { LoaderIcon } from "lucide-react";
import { registerSchema, type RegisterFormData } from "@/lib/validations";

export const RegisterPage = () => {
  const { register } = useAuth();
  const navigate = useNavigate();
  const { toast } = useToast();

  const {
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<RegisterFormData>({
    resolver: zodResolver(registerSchema),
    defaultValues: {
      firstName: "",
      lastName: "",
      email: "",
      password: "",
    },
  });

  const onSubmit = async (data: RegisterFormData) => {
    try {
      await register(data.firstName, data.lastName, data.email, data.password);
      toast({
        title: "Account created successfully!",
        description: "Welcome to SearchBugs. You can now login.",
      });
      navigate("/login");
    } catch (error: unknown) {
      console.error("Registration error:", error);
      const errorMessage =
        error instanceof Error
          ? error.message
          : "Registration failed. Please try again.";
      toast({
        title: "Registration failed",
        description: errorMessage,
        variant: "destructive",
      });
    }
  };

  return (
    <div className="absolute inset-0 flex items-center justify-center bg-linear-to-br from-background to-muted">
      <Card className="mx-auto max-w-sm w-full m-4 shadow-lg">
        <CardHeader className="text-center">
          <CardTitle className="text-xl">Sign Up</CardTitle>
          <CardDescription>
            Enter your information to create an account
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit(onSubmit)} className="grid gap-4">
            <div className="grid grid-cols-2 gap-4">
              <FormInput
                control={control}
                name="firstName"
                label="First name"
                placeholder="Search"
                required
                disabled={isSubmitting}
                error={errors.firstName}
              />
              <FormInput
                control={control}
                name="lastName"
                label="Last name"
                placeholder="Bugs"
                required
                disabled={isSubmitting}
                error={errors.lastName}
              />
            </div>
            <FormInput
              control={control}
              name="email"
              label="Email"
              type="email"
              placeholder="example@searchbugs.com"
              required
              disabled={isSubmitting}
              error={errors.email}
            />
            <FormInput
              control={control}
              name="password"
              label="Password"
              type="password"
              required
              disabled={isSubmitting}
              error={errors.password}
            />
            <Button type="submit" className="w-full" disabled={isSubmitting}>
              {isSubmitting && (
                <LoaderIcon className="mr-2 h-4 w-4 animate-spin" />
              )}
              Create an account
            </Button>
            <Button
              variant="outline"
              className="w-full"
              type="button"
              disabled={isSubmitting}
            >
              Sign up with GitHub
            </Button>
          </form>
          <div className="mt-4 text-center text-sm">
            Already have an account?{" "}
            <NavLink to={"/login"} className="underline hover:no-underline">
              Sign in
            </NavLink>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};
