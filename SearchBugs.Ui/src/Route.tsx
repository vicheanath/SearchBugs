import { createBrowserRouter, RouterProvider } from "react-router-dom";
import {
  BugAddPage,
  BugDetailsPage,
  BugsPage,
  CreateProjectPage,
  DashboardPage,
  LoginPage,
  NotificationsPage,
  ProjectDetailsPage,
  ProjectsPage,
  RegisterPage,
  RepositoriesPage,
  RepositoryDetailsPage,
  SettingPage,
  UserDetailsPage,
  UsersPage,
} from "./pages";
import MainLayout from "./layouts/Main";

const router = createBrowserRouter([
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/register",
    element: <RegisterPage />,
  },
  {
    element: <MainLayout />,
    children: [
      {
        path: "/",
        element: <DashboardPage />,
      },
      {
        path: "/projects",
        element: <ProjectsPage />,
      },
      {
        path: "/projects/create",
        element: <CreateProjectPage />,
      },
      {
        path: "/projects/:projectId",
        element: <ProjectDetailsPage />,
      },
      {
        path: "/users",
        element: <UsersPage />,
      },
      {
        path: "/users/:userId",
        element: <UserDetailsPage />,
      },
      {
        path: "/repositories",
        element: <RepositoriesPage />,
      },
      {
        path: "/repositories/:url",
        element: <RepositoryDetailsPage />,
      },
      {
        path: "/bugs",
        element: <BugsPage />,
      },
      {
        path: "/add-bug",
        element: <BugAddPage />,
      },

      {
        path: "/bugs/:bugId",
        element: <BugDetailsPage />,
      },
      {
        path: "/notifications",
        element: <NotificationsPage />,
      },
      {
        path: "/settings",
        element: <SettingPage />,
      }
    ],
  },
]);

const Route = () => {
  return <RouterProvider router={router} />;
};

export default Route;
