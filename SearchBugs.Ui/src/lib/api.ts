import axios from "axios";
import { apiBaseUrl } from "./constants";
export const accessTokenKey = "access";
export const refreshTokenKey = "refresh";

const accessToken = localStorage.getItem(accessTokenKey);
const refreshToken = localStorage.getItem(refreshTokenKey);
export const api = axios.create({
  baseURL: apiBaseUrl,
  headers: {
    Authorization: `Bearer ${accessToken}` || "",
  },
});
api.defaults.headers.common["Content-Type"] = "application/json";

export const refreshAccessTokenFn = async () => {
  fetch(`${apiBaseUrl}token/refresh`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${refreshToken}` || "",
    },
  })
    .then((res) => res.json())
    .then((data) => {
      const { accessToken } = data;
      api.defaults.headers.common["Authorization"] = `Bearer ${accessToken}`;
      localStorage.setItem(accessTokenKey, accessToken);
    });
};

api.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    const originalRequest = error.config;
    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      await refreshAccessTokenFn();
      return api(originalRequest);
    }
    return Promise.reject(error);
  }
);