import { QueryClientProvider } from "react-query";
import Route from "./Route";
import { queryClient } from "./lib/queryClient";

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Route />
    </QueryClientProvider>
  );
}

export default App;
