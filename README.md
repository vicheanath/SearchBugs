## Summary of Endpoints:
### User Management Domain
- POST /api/users
- GET /api/users/{user_id}
- PUT /api/users/{user_id}
- DELETE /api/users/{user_id}
- POST /api/auth/login
- POST /api/auth/logout
- GET /api/users
### Project Management Domain
- POST /api/projects
- GET /api/projects/{project_id}
- PUT /api/projects/{project_id}
- DELETE /api/projects/{project_id}
- GET /api/projects
### Bug Tracking Domain
- POST /api/bugs
- GET /api/bugs/{bug_id}
- PUT /api/bugs/{bug_id}
- DELETE /api/bugs/{bug_id}
- GET /api/bugs
- POST /api/bugs/{bug_id}/comments
- GET /api/bugs/{bug_id}/comments
- POST /api/bugs/{bug_id}/attachments
- GET /api/bugs/{bug_id}/attachments
- GET /api/bugs/{bug_id}/history
- POST /api/bugs/{bug_id}/time-tracking
- GET /api/bugs/{bug_id}/time-tracking
- POST /api/bugs/{bug_id}/custom-fields
- GET /api/bugs/{bug_id}/custom-fields
### Notification Domain
- GET /api/users/{user_id}/notifications
- PUT /api/notifications/{notification_id}/read
- DELETE /api/notifications/{notification_id}
### Repository Management Domain
- POST /api/repositories
- GET /api/repositories/{repo_id}
- PUT /api/repositories/{repo_id}
- DELETE /api/repositories/{repo_id}
- GET /api/repositories
- POST /api/bugs/{bug_id}/repositories
- GET /api/bugs/{bug_id}/repositories
