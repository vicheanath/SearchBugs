import { useQuery } from "react-query";
interface ErrorResult {
    code : string;
    message : string;
}

interface ApiResult<T> {
    value: T[];
    error: ErrorResult[];
    isFailure : boolean;
    isSuccess : boolean;
}

export const useApi = <T>(url: string) => {
    const q = useQuery<ApiResult<T>>(url);
    return q;
};