import axios from "axios";
import { apiBaseUrl } from "./constants";
export const accessTokenKey = "access";

// Create api instance
export const api = axios.create({
  baseURL: apiBaseUrl,
});

// Set default content type
api.defaults.headers.common["Content-Type"] = "application/json";

// Add request interceptor to include token in each request
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem(accessTokenKey);
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Response interceptor for handling 401 errors
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      // Token is invalid or expired, remove it and redirect to login
      localStorage.removeItem(accessTokenKey);
      // You might want to redirect to login page here
      // window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

// Response wrapper type for API responses
export interface ApiResponse<T> {
  value: T;
  isSuccess: boolean;
  isFailure: boolean;
  error: {
    code: string;
    message: string;
  };
}

// Authentication response types
export interface LoginResponse {
  token: string;
}

export interface RegisterResponse {
  token: string;
}

export interface ImpersonateResponse {
  token: string;
  impersonatedUserEmail: string;
}

export interface StopImpersonateResponse {
  token: string;
}

// Types
export interface Project {
  id: string;
  name: string;
  description: string;
  createdOnUtc: string;
  updatedOnUtc: string;
}

export interface Bug {
  id: string;
  projectId: string;
  title: string;
  description: string;
  status: BugStatus;
  priority: BugPriority;
  severity: BugSeverity;
  reporterId: string;
  assigneeId?: string;
  createdOnUtc: string;
  updatedOnUtc: string;
}

export interface Comment {
  id: string;
  commentText: string;
  userId: string;
  createdOnUtc: string;
  modifiedOnUtc?: string;
  userName?: string;
  userEmail?: string;
}

export interface Attachment {
  id: string;
  bugId: string;
  fileName: string;
  fileSize: number;
  contentType: string;
  uploadedBy: string;
  uploadedOnUtc: string;
}

export interface TimeEntry {
  id: string;
  bugId: string;
  userId: string;
  hours: number;
  description: string;
  date: string;
  createdOnUtc: string;
  updatedOnUtc: string;
}

export interface HistoryEntry {
  id: string;
  fieldChanged: string;
  oldValue: string;
  newValue: string;
  changedById: string;
  changedAtUtc: string;
  userName: string;
}

export interface CustomField {
  id: string;
  projectId: string;
  name: string;
  type: CustomFieldType;
  isRequired: boolean;
  defaultValue?: string;
  createdOnUtc: string;
  updatedOnUtc: string;
}

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  roles?: Role[];
  createdOnUtc: string;
  modifiedOnUtc?: string;
}

export interface UserProfile {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  roles?: Role[];
  createdOnUtc?: string;
  modifiedOnUtc?: string;
  // Real profile fields from domain model
  bio?: string;
  location?: string;
  website?: string;
  avatarUrl?: string;
  company?: string;
  jobTitle?: string;
  twitterHandle?: string;
  linkedInProfile?: string;
  gitHubProfile?: string;
  isPublic: boolean;
  dateOfBirth?: string;
  phoneNumber?: string;
  timeZone?: string;
  preferredLanguage?: string;
  recentActivity: ProfileActivity[];
}

export interface ProfileActivity {
  id: string;
  type: string;
  action: string;
  target: string;
  timestamp: string;
  icon: string;
  isSuccess: boolean;
  duration: string; // TimeSpan as string
}

export interface UserActivityItem {
  id: string;
  type: string;
  action: string;
  target: string;
  description: string;
  timestamp: string;
  icon: string;
  isSuccess: boolean;
  duration: string; // TimeSpan as string
  errorMessage?: string;
}

export interface UserActivityResponse {
  activities: UserActivityItem[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export interface UpdateProfileRequest {
  firstName: string;
  lastName: string;
  bio?: string;
  location?: string;
  website?: string;
  gitHubUsername?: string;
  linkedInUrl?: string;
  twitterHandle?: string;
  company?: string;
  jobTitle?: string;
  phoneNumber?: string;
  timeZone?: string;
  avatarUrl?: string;
  dateOfBirth?: string; // ISO string format
}

export interface UpdateProfileResponse {
  success: boolean;
  message: string;
}

export interface Role {
  id: number;
  name: string;
  permissions: string[];
}

export interface RoleWithPermissions {
  id: number;
  name: string;
  permissions: Permission[];
}

export interface Permission {
  id: number;
  name: string;
  description: string;
}

export interface AuditLog {
  id: string;
  requestName: string;
  requestData: string;
  responseData?: string;
  isSuccess: boolean;
  errorMessage?: string;
  duration: string; // TimeSpan as string
  userId?: string;
  userName?: string;
  ipAddress: string;
  userAgent: string;
  createdOnUtc: string;
}

export interface NotificationDto {
  id: string;
  type: string;
  message: string;
  data?: string;
  bugId?: string;
  isRead: boolean;
  createdAt: string;
}

export interface PagedNotificationResponse {
  notifications: NotificationDto[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export interface UnreadCountResponse {
  count: number;
}

export interface Repository {
  id: string;
  projectId: string;
  name: string;
  url: string;
  type: RepositoryType;
  createdOnUtc: string;
  updatedOnUtc: string;
}

export interface GitTreeItem {
  path: string;
  name: string;
  type: string; // "Tree" or "Blob"
}

export interface FileDiff {
  filePath: string;
  oldPath: string;
  status: string;
  patch: string;
}

export interface CommitInfoDto {
  sha: string;
  message: string;
  authorName: string;
  authorEmail: string;
  when: string;
}

export interface PullRequestListItemDto {
  id: string;
  title: string;
  sourceBranch: string;
  targetBranch: string;
  status: string;
  createdBy: string;
  createdAtUtc: string;
}

export interface PullRequestDetailDto {
  id: string;
  repoUrl: string;
  sourceBranch: string;
  targetBranch: string;
  title: string;
  description: string;
  status: string;
  createdBy: string;
  createdAtUtc: string;
  mergedAtUtc?: string;
  mergedBy?: string;
  diff: FileDiff[];
}

export interface PullRequestDto {
  id: string;
  repoUrl: string;
  sourceBranch: string;
  targetBranch: string;
  title: string;
  description: string;
  status: string;
  createdBy: string;
  createdAtUtc: string;
  mergedAtUtc?: string;
  mergedBy?: string;
}

// Enums
export enum BugStatus {
  New = "New",
  InProgress = "InProgress",
  Resolved = "Resolved",
  Closed = "Closed",
  Reopened = "Reopened",
}

export enum BugPriority {
  Low = "Low",
  Medium = "Medium",
  High = "High",
  Critical = "Critical",
}

export enum BugSeverity {
  Low = "Low",
  Medium = "Medium",
  High = "High",
  Critical = "Critical",
}

export enum CustomFieldType {
  Text = "Text",
  Number = "Number",
  Date = "Date",
  Boolean = "Boolean",
  Select = "Select",
}

export enum UserRole {
  User = "User",
  Admin = "Admin",
}

export enum RepositoryType {
  Git = "Git",
  Svn = "Svn",
  Mercurial = "Mercurial",
}

// DTOs
export interface CreateProjectDto {
  name: string;
  description: string;
}

export interface CreateBugDto {
  projectId: string;
  title: string;
  description: string;
  status: string;
  priority: string;
  severity: string;
  reporterId: string;
  assigneeId?: string;
}

export interface UpdateBugDto {
  title?: string;
  description?: string;
  status?: BugStatus;
  priority?: BugPriority;
  severity?: BugSeverity;
  assigneeId?: string;
}

export interface CreateCommentDto {
  bugId: string;
  content: string;
}

export interface CreateTimeEntryDto {
  bugId: string;
  userId: string;
  hours: number;
  description: string;
  date: string;
}

export interface CreateCustomFieldDto {
  projectId: string;
  name: string;
  type: CustomFieldType;
  isRequired: boolean;
  defaultValue?: string;
}

export interface UpdateCustomFieldDto {
  name?: string;
  type?: CustomFieldType;
  isRequired?: boolean;
  defaultValue?: string;
}

export interface CreateRepositoryDto {
  projectId: string;
  name: string;
  url: string;
  type: RepositoryType;
}

export interface UpdateRepositoryDto {
  name?: string;
  url?: string;
  type?: RepositoryType;
}

// API functions
export const apiClient = {
  // Authentication
  auth: {
    login: (data: { email: string; password: string }) =>
      api.post<ApiResponse<LoginResponse>>("/auth/login", data),
    register: (data: {
      email: string;
      password: string;
      firstName: string;
      lastName: string;
    }) => api.post<ApiResponse<RegisterResponse>>("/auth/register", data),
    impersonate: (data: { userIdToImpersonate: string }) =>
      api.post<ApiResponse<ImpersonateResponse>>("/auth/impersonate", data),
    stopImpersonate: () =>
      api.post<ApiResponse<StopImpersonateResponse>>("/auth/stop-impersonate"),
  },

  // Projects
  projects: {
    getAll: () => api.get<ApiResponse<Project[]>>("/projects"),
    getById: (id: string) => api.get<ApiResponse<Project>>(`/projects/${id}`),
    create: (data: CreateProjectDto) =>
      api.post<ApiResponse<Project>>("/projects", data),
    delete: (id: string) => api.delete<ApiResponse<void>>(`/projects/${id}`),
  },

  // Comments
  comments: {
    getByBugId: (bugId: string) =>
      api.get<ApiResponse<Comment[]>>(`/bugs/${bugId}/comments`),
    create: (data: CreateCommentDto) => {
      console.log("API Client - Creating comment with data:", data);
      const payload = { Content: data.content };
      console.log("API Client - Sending payload:", payload);
      return api.post<ApiResponse<Comment>>(
        `/bugs/${data.bugId}/comments`,
        payload
      );
    },
  },

  // Bugs
  bugs: {
    getAll: (projectId?: string) =>
      api.get<ApiResponse<Bug[]>>(
        projectId ? `/bugs?projectId=${projectId}` : "/bugs"
      ),
    getById: (id: string) => api.get<ApiResponse<Bug>>(`/bugs/${id}`),
    create: (data: CreateBugDto) => api.post<ApiResponse<Bug>>("/bugs", data),
    update: (id: string, data: UpdateBugDto) =>
      api.put<ApiResponse<Bug>>(`/bugs/${id}`, data),
    delete: (id: string) => api.delete<ApiResponse<void>>(`/bugs/${id}`),

    // Comments
    getComments: (bugId: string) =>
      api.get<ApiResponse<Comment[]>>(`/bugs/${bugId}/comments`),
    addComment: (data: CreateCommentDto) =>
      api.post<ApiResponse<Comment>>(`/bugs/${data.bugId}/comments`, {
        Content: data.content,
      }),

    // Attachments
    getAttachments: (bugId: string) =>
      api.get<ApiResponse<Attachment[]>>(`/bugs/${bugId}/attachments`),
    addAttachment: (bugId: string, file: File) => {
      const formData = new FormData();
      formData.append("file", file);
      return api.post<ApiResponse<Attachment>>(
        `/bugs/${bugId}/attachments`,
        formData,
        {
          headers: { "Content-Type": "multipart/form-data" },
        }
      );
    },

    // History
    getHistory: (bugId: string) =>
      api.get<ApiResponse<HistoryEntry[]>>(`/bugs/${bugId}/history`),

    // Time tracking
    getTimeTracking: (bugId: string) =>
      api.get<ApiResponse<TimeEntry[]>>(`/bugs/${bugId}/time-tracking`),
    trackTime: (
      bugId: string,
      data: { duration: string; description: string }
    ) => api.post<ApiResponse<TimeEntry>>(`/bugs/${bugId}/time-tracking`, data),

    // Custom fields
    getCustomFields: (bugId: string) =>
      api.get<ApiResponse<CustomField[]>>(`/bugs/${bugId}/custom-fields`),
    addCustomField: (bugId: string, data: { name: string; value: string }) =>
      api.post<ApiResponse<CustomField>>(`/bugs/${bugId}/custom-fields`, data),
  },

  // Users
  users: {
    getAll: (params?: {
      searchTerm?: string;
      roleFilter?: string;
      pageNumber?: number;
      pageSize?: number;
    }) => {
      const queryParams = new URLSearchParams();
      if (params?.searchTerm)
        queryParams.append("searchTerm", params.searchTerm);
      if (params?.roleFilter && params.roleFilter !== "all")
        queryParams.append("roleFilter", params.roleFilter);
      if (params?.pageNumber)
        queryParams.append("pageNumber", params.pageNumber.toString());
      if (params?.pageSize)
        queryParams.append("pageSize", params.pageSize.toString());

      const queryString = queryParams.toString();
      return api.get<ApiResponse<User[]>>(
        `/users${queryString ? `?${queryString}` : ""}`
      );
    },
    getById: (id: string) => api.get<ApiResponse<User>>(`/users/${id}`),
    create: (data: {
      firstName: string;
      lastName: string;
      email: string;
      password: string;
      roles?: string[];
    }) => api.post<ApiResponse<User>>("/users", data),
    update: (id: string, data: { firstName: string; lastName: string }) =>
      api.put<ApiResponse<User>>(`/users/${id}`, data),
    delete: (id: string) => api.delete<ApiResponse<void>>(`/users/${id}`),
    assignRole: (userId: string, role: string) =>
      api.post<ApiResponse<void>>(`/users/${userId}/assign-role`, {
        userId,
        role,
      }),
    removeRole: (userId: string, role: string) =>
      api.delete<ApiResponse<void>>(`/users/${userId}/remove-role`, {
        data: { userId, role },
      }),
    changePassword: (
      userId: string,
      currentPassword: string,
      newPassword: string
    ) =>
      api.post<ApiResponse<void>>(`/users/${userId}/change-password`, {
        currentPassword,
        newPassword,
      }),
  },

  // Profile
  profile: {
    getCurrent: () => api.get<ApiResponse<UserProfile>>("/profile"),
    update: (data: UpdateProfileRequest) =>
      api.put<ApiResponse<UpdateProfileResponse>>("/profile", data),
    getActivity: (params?: { pageNumber?: number; pageSize?: number }) => {
      const queryParams = new URLSearchParams();
      if (params?.pageNumber)
        queryParams.append("pageNumber", params.pageNumber.toString());
      if (params?.pageSize)
        queryParams.append("pageSize", params.pageSize.toString());

      const queryString = queryParams.toString();
      return api.get<ApiResponse<UserActivityResponse>>(
        `/profile/activity${queryString ? `?${queryString}` : ""}`
      );
    },
  },

  // Roles
  roles: {
    getAll: () => api.get<ApiResponse<Role[]>>("/roles"),
    getPermissions: () =>
      api.get<ApiResponse<Permission[]>>("/roles/permissions"),
    getRoleWithPermissions: (roleId: number) =>
      api.get<ApiResponse<RoleWithPermissions>>(`/roles/${roleId}`),
    assignPermissionToRole: (roleId: number, permissionId: number) =>
      api.post(`/roles/${roleId}/permissions/${permissionId}`),
    removePermissionFromRole: (roleId: number, permissionId: number) =>
      api.delete(`/roles/${roleId}/permissions/${permissionId}`),
  },

  // Repositories
  repositories: {
    getAll: () => api.get<ApiResponse<Repository[]>>("/repo"),
    getDetails: (url: string, path: string) =>
      api.get<ApiResponse<any>>(`/repo/${encodeURIComponent(url)}/${path}`),
    create: (data: {
      name: string;
      description: string;
      url: string;
      projectId: string;
    }) => api.post<ApiResponse<Repository>>("/repo", data),
    delete: (url: string) => api.delete<ApiResponse<void>>(`/repo/${encodeURIComponent(url)}`),
    getCommitDiff: (url: string, commitSha: string) =>
      api.get(`/repo/${encodeURIComponent(url)}/commit/${commitSha}`),
    commitChanges: (
      url: string,
      commitSha: string,
      data: {
        author: string;
        email: string;
        message: string;
        content: string;
      }
    ) => api.post(`/repo/${encodeURIComponent(url)}/commit/${commitSha}`, data),
    getTree: (url: string, commitSha: string) =>
      api.get<ApiResponse<GitTreeItem[]>>(
        `/repo/${encodeURIComponent(url)}/tree/${commitSha}`
      ),
    getFileContent: (url: string, commitSha: string, filePath: string) =>
      api.get<ApiResponse<string>>(
        `/repo/${encodeURIComponent(url)}/file/${commitSha}/${filePath}`
      ),
    getBranches: (url: string) =>
      api.get<ApiResponse<string[]>>(
        `/repo/${encodeURIComponent(url)}/branches`
      ),
    clone: (url: string, targetPath: string) =>
      api.post(`/repo/${encodeURIComponent(url)}/clone`, { targetPath }),
    merge: (url: string, data: { sourceBranch: string; targetBranch: string; authorName: string; authorEmail: string }) =>
      api.post(`/repo/${encodeURIComponent(url)}/merge`, data),
    push: (url: string, data: { branchName: string; remoteName?: string }) =>
      api.post(`/repo/${encodeURIComponent(url)}/push`, data),
    pull: (url: string, data: { branchName: string; authorName: string; authorEmail: string; remoteName?: string }) =>
      api.post(`/repo/${encodeURIComponent(url)}/pull`, data),
    getCommits: (url: string, params?: { branch?: string; skip?: number; take?: number }) => {
      const search = new URLSearchParams();
      if (params?.branch) search.set("branch", params.branch);
      if (params?.skip != null) search.set("skip", String(params.skip));
      if (params?.take != null) search.set("take", String(params.take));
      return api.get<ApiResponse<CommitInfoDto[]>>(`/repo/${encodeURIComponent(url)}/commits?${search}`);
    },
    getCompare: (url: string, baseSha: string, compareSha: string) =>
      api.get<ApiResponse<FileDiff[]>>(`/repo/${encodeURIComponent(url)}/compare?baseSha=${encodeURIComponent(baseSha)}&compare=${encodeURIComponent(compareSha)}`),
    getPullRequests: (url: string, status?: string) =>
      api.get<ApiResponse<PullRequestListItemDto[]>>(`/repo/${encodeURIComponent(url)}/pull-requests${status ? `?status=${status}` : ""}`),
    getPullRequest: (url: string, id: string) =>
      api.get<ApiResponse<PullRequestDetailDto>>(`/repo/${encodeURIComponent(url)}/pull-requests/${id}`),
    createPullRequest: (url: string, data: { sourceBranch: string; targetBranch: string; title: string; description: string; createdByUserId: string }) =>
      api.post<ApiResponse<PullRequestDto>>(`/repo/${encodeURIComponent(url)}/pull-requests`, data),
    mergePullRequest: (url: string, id: string, data: { authorName: string; authorEmail: string; mergedByUserId: string }) =>
      api.post(`/repo/${encodeURIComponent(url)}/pull-requests/${id}/merge`, data),
  },

  // Test Notifications
  testNotifications: {
    sendTestNotification: (data: { userId: string; message?: string }) =>
      api.post<{ message: string }>(
        "/test-notifications/send-test-notification",
        data
      ),
  },

  // Audit Logs
  auditLogs: {
    getAll: (params?: {
      userId?: string;
      startDate?: string;
      endDate?: string;
      pageNumber?: number;
      pageSize?: number;
    }) => {
      const queryParams = new URLSearchParams();
      if (params?.userId) queryParams.append("userId", params.userId);
      if (params?.startDate) queryParams.append("startDate", params.startDate);
      if (params?.endDate) queryParams.append("endDate", params.endDate);
      if (params?.pageNumber)
        queryParams.append("pageNumber", params.pageNumber.toString());
      if (params?.pageSize)
        queryParams.append("pageSize", params.pageSize.toString());

      const queryString = queryParams.toString();
      return api.get<ApiResponse<AuditLog[]>>(
        `/audit-logs${queryString ? `?${queryString}` : ""}`
      );
    },
  },

  // Notifications
  notifications: {
    getAll: (params?: {
      pageNumber?: number;
      pageSize?: number;
      isRead?: boolean;
    }) => {
      const queryParams = new URLSearchParams();
      if (params?.pageNumber)
        queryParams.append("pageNumber", params.pageNumber.toString());
      if (params?.pageSize)
        queryParams.append("pageSize", params.pageSize.toString());
      if (params?.isRead !== undefined)
        queryParams.append("isRead", params.isRead.toString());

      const queryString = queryParams.toString();
      return api.get<ApiResponse<PagedNotificationResponse>>(
        `/notifications${queryString ? `?${queryString}` : ""}`
      );
    },
    getUnread: () =>
      api.get<ApiResponse<NotificationDto[]>>("/notifications/unread"),
    getUnreadCount: () =>
      api.get<ApiResponse<UnreadCountResponse>>("/notifications/unread-count"),
    markAsRead: (id: string) =>
      api.put<ApiResponse<void>>(`/notifications/${id}/read`),
    markAllAsRead: () =>
      api.put<ApiResponse<void>>("/notifications/mark-all-read"),
    delete: (id: string) =>
      api.delete<ApiResponse<void>>(`/notifications/${id}`),
    clearAll: () => api.delete<ApiResponse<void>>("/notifications/clear-all"),
  },
};
