import { createBrowserRouter, RouterProvider } from "react-router-dom";
import {
  BugDetailsPage,
  BugsPage,
  DashboardPage,
  NotificationsPage,
  ProjectDetailsPage,
  ProjectsPage,
  RepositoriesPage,
  RepositoryDetailsPage,
  UserDetailsPage,
  UsersPage,
} from "./pages";
import Layout from "./layouts/Main";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        path: "/dashboard",
        element: <DashboardPage />,
      },
      {
        path: "/projects",
        element: <ProjectsPage />,
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
        path: "/repositories/:repoId",
        element: <RepositoryDetailsPage />,
      },
      {
        path: "/bugs",
        element: <BugsPage />,
      },

      {
        path: "/bugs/:bugId",
        element: <BugDetailsPage />,
      },
      {
        path: "/notifications",
        element: <NotificationsPage />,
      },
    ],
  },
]);

const Route = () => {
  
  return <RouterProvider router={router} />;
}

export default Route;