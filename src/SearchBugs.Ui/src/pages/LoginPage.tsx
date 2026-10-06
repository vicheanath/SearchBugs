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
import { loginSchema, type LoginFormData } from "@/lib/validations";

export const LoginPage = () => {
  const { login } = useAuth();
  const navigate = useNavigate();
  const { toast } = useToast();

  const {
    control,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const onSubmit = async (data: LoginFormData) => {
    try {
      await login(data.email, data.password);
      toast({
        title: "Welcome back!",
        description: "You have successfully logged in.",
      });
      navigate("/");
    } catch (error) {
      toast({
        title: "Login failed",
        description: "Please check your credentials and try again.",
        variant: "destructive",
      });
    }
  };

  return (
    <div className="absolute inset-0 flex items-center justify-center bg-linear-to-br from-background to-muted">
      <Card className="mx-auto max-w-sm w-full m-4 shadow-lg">
        <CardHeader className="text-center">
          <CardTitle className="text-2xl">Welcome Back</CardTitle>
          <CardDescription>
            Enter your email below to login to your account
          </CardDescription>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit(onSubmit)} className="grid gap-4">
            <FormInput
              control={control}
              name="email"
              label="Email"
              type="email"
              placeholder="m@example.com"
              required
              disabled={isSubmitting}
              error={errors.email}
            />
            <div className="grid gap-2">
              <div className="flex items-center justify-between">
                <span className="text-sm font-medium">Password *</span>
                <NavLink
                  to={"/forgot-password"}
                  className="text-sm underline hover:no-underline"
                >
                  Forgot your password?
                </NavLink>
              </div>
              <FormInput
                control={control}
                name="password"
                type="password"
                required
                disabled={isSubmitting}
                error={errors.password}
              />
            </div>
            <Button type="submit" className="w-full" disabled={isSubmitting}>
              {isSubmitting && (
                <LoaderIcon className="mr-2 h-4 w-4 animate-spin" />
              )}
              Login
            </Button>
            <Button
              variant="outline"
              className="w-full"
              type="button"
              disabled={isSubmitting}
            >
              Login with Google
            </Button>
          </form>
          <div className="mt-4 text-center text-sm">
            Don&apos;t have an account?{" "}
            <NavLink to={"/register"} className="underline hover:no-underline">
              Sign up
            </NavLink>
          </div>
        </CardContent>
      </Card>
    </div>
  );
};
