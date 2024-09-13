
import { QueryClient } from "react-query";
import { defaultQueryFn } from "./defaultQueryFn";

export const queryClient = new QueryClient({
  defaultOptions: {
    mutations: {
      onError: async (e) => {
        console.log(e);
      },
    },
    queries: {
      staleTime: 60 * 1000 * 1, // 5 minutes
      onError: (e) => {
        console.log(e);
      },
      queryFn: defaultQueryFn,
    },
  },
});