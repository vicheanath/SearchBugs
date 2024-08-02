import { QueryKey, QueryOptions } from "react-query";
import { accessTokenKey } from "./api";
import { apiBaseUrl } from "./constants";

export const defaultQueryFn = async (
  q: QueryOptions<unknown, unknown, unknown, QueryKey>
) => {
  const accessToken = localStorage.getItem(accessTokenKey);
  const r = await fetch(`${apiBaseUrl}${q.queryKey}`, {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });
  if (r.status !== 200) {
    throw new Error(await r.text());
  }
  return await r.json();
};
