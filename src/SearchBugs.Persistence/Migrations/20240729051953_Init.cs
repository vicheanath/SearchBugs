using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SearchBugs.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bug_priority",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bug_priority", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bug_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bug_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "project_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name_first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name_last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email_value = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: false),
                    password = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "custom_field",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    field_type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_custom_field", x => x.id);
                    table.ForeignKey(
                        name: "fk_custom_field_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "repository",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_repository", x => x.id);
                    table.ForeignKey(
                        name: "fk_repository_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permission", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "fk_role_permission_permission_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permission_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bug",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    priority_id = table.Column<int>(type: "integer", nullable: false),
                    severity = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    assignee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reporter_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bug", x => x.id);
                    table.ForeignKey(
                        name: "fk_bug_bug_priorities_priority_id",
                        column: x => x.priority_id,
                        principalTable: "bug_priority",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bug_bug_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "bug_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bug_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bug_user_assignee_id",
                        column: x => x.assignee_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bug_user_reporter_id",
                        column: x => x.reporter_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_role_user",
                columns: table => new
                {
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    project_role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_role_user", x => new { x.project_id, x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_project_role_user_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_role_user_project_role_project_role_id",
                        column: x => x.project_role_id,
                        principalTable: "project_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_role_user_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_user_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_role_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attachment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    content_type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    content = table.Column<byte[]>(type: "bytea", nullable: false),
                    bug_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attachment", x => x.id);
                    table.ForeignKey(
                        name: "fk_attachment_bug_bug_id",
                        column: x => x.bug_id,
                        principalTable: "bug",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bug_custom_field",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bug_id = table.Column<Guid>(type: "uuid", nullable: false),
                    custom_field_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bug_custom_field", x => x.id);
                    table.ForeignKey(
                        name: "fk_bug_custom_field_bug_bug_id",
                        column: x => x.bug_id,
                        principalTable: "bug",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bug_custom_field_custom_field_custom_field_id",
                        column: x => x.custom_field_id,
                        principalTable: "custom_field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bug_history",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bug_id = table.Column<Guid>(type: "uuid", nullable: false),
                    changed_by = table.Column<Guid>(type: "uuid", nullable: false),
                    field_changed = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    old_value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    new_value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    changed_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bug_history", x => x.id);
                    table.ForeignKey(
                        name: "fk_bug_history_bug_bug_id",
                        column: x => x.bug_id,
                        principalTable: "bug",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bug_history_user_changed_by",
                        column: x => x.changed_by,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bug_repository",
                columns: table => new
                {
                    bug_id = table.Column<Guid>(type: "uuid", nullable: false),
                    repository_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bug_repository", x => new { x.bug_id, x.repository_id });
                    table.ForeignKey(
                        name: "fk_bug_repository_bug_bug_id",
                        column: x => x.bug_id,
                        principalTable: "bug",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bug_repository_repository_repository_id",
                        column: x => x.repository_id,
                        principalTable: "repository",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bug_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comment_text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment", x => x.id);
                    table.ForeignKey(
                        name: "fk_comment_bug_bug_id",
                        column: x => x.bug_id,
                        principalTable: "bug",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comment_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    message = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    bug_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notification", x => x.id);
                    table.ForeignKey(
                        name: "fk_notification_bug_bug_id",
                        column: x => x.bug_id,
                        principalTable: "bug",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notification_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "time_track",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    bug_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    time_spent = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    logged_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_time_track", x => x.id);
                    table.ForeignKey(
                        name: "fk_time_track_bug_bug_id",
                        column: x => x.bug_id,
                        principalTable: "bug",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_time_track_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "bug_priority",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Low" },
                    { 2, "Medium" },
                    { 3, "High" }
                });

            migrationBuilder.InsertData(
                table: "bug_status",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Resolved" },
                    { 4, "Closed" }
                });

            migrationBuilder.InsertData(
                table: "permission",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Can create a new user", "CreateUser" },
                    { 2, "Can view user details", "ViewUserDetails" },
                    { 3, "Can update user details", "UpdateUser" },
                    { 4, "Can delete a user", "DeleteUser" },
                    { 5, "Can authenticate a user", "AuthenticateUser" },
                    { 6, "Can change user password", "ChangeUserPassword" },
                    { 7, "Can list all users", "ListAllUsers" },
                    { 8, "Can Create Project", "CreateProject" },
                    { 9, "Can View Project Details", "ViewUserDetails" },
                    { 10, "Can Update Project", "UpdateProject" },
                    { 11, "Can Delete Project", "DeleteProject" },
                    { 12, "Can List All Projects", "ListAllProjects" },
                    { 13, "Can Create Bug", "CreateBug" },
                    { 14, "Can View Bug Details", "ViewBugDetails" },
                    { 15, "Can Update Bug", "UpdateBug" },
                    { 16, "Can Delete Bug", "DeleteBug" },
                    { 17, "Can List All Bugs", "ListAllBugs" },
                    { 18, "Can Add Comment To Bug", "AddCommentToBug" },
                    { 19, "Can View Bug Comments", "ViewBugComments" },
                    { 20, "Can Add Attachment To Bug", "AddAttachmentToBug" },
                    { 21, "Can View Bug Attachments", "ViewBugAttachments" },
                    { 22, "Can View Bug History", "ViewBugHistory" },
                    { 23, "Can Track Time Spent On Bug", "TrackTimeSpentOnBug" },
                    { 24, "Can View Time Spent On Bug", "ViewTimeSpentOnBug" },
                    { 25, "Can Add Custom Field To Bug", "AddCustomFieldToBug" },
                    { 26, "Can View Custom Field On Bug", "ViewCustomFieldOnBug" },
                    { 28, "Can View Notification", "ViewNotification" },
                    { 29, "Can Delete Notification", "DeleteNotification" },
                    { 30, "Can Mark Notification As Read", "MarkNotificationAsRead" },
                    { 31, "Can Create Repository", "CreateRepository" },
                    { 32, "Can View Repository Details", "ViewRepositoryDetails" },
                    { 33, "Can Update Repository", "UpdateRepository" },
                    { 34, "Can Delete Repository", "DeleteRepository" },
                    { 35, "Can List All Repositories", "ListAllRepositories" },
                    { 36, "Can Link Bug To Repository", "LinkBugToRepository" },
                    { 37, "Can View Bug Repository", "ViewBugRepository" }
                });

            migrationBuilder.InsertData(
                table: "project_role",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Owner" },
                    { 2, "Admin" },
                    { 3, "Developer" },
                    { 4, "Tester" }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Project Manager" },
                    { 3, "Developer" },
                    { 4, "Reporter" },
                    { 5, "Guest" }
                });

            migrationBuilder.InsertData(
                table: "role_permission",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 1 },
                    { 18, 1 },
                    { 19, 1 },
                    { 20, 1 },
                    { 21, 1 },
                    { 22, 1 },
                    { 23, 1 },
                    { 24, 1 },
                    { 25, 1 },
                    { 26, 1 },
                    { 28, 1 },
                    { 29, 1 },
                    { 30, 1 },
                    { 31, 1 },
                    { 32, 1 },
                    { 33, 1 },
                    { 34, 1 },
                    { 35, 1 },
                    { 36, 1 },
                    { 37, 1 },
                    { 7, 2 },
                    { 8, 2 },
                    { 9, 2 },
                    { 10, 2 },
                    { 11, 2 },
                    { 12, 2 },
                    { 13, 2 },
                    { 14, 2 },
                    { 15, 2 },
                    { 16, 2 },
                    { 17, 2 },
                    { 18, 2 },
                    { 19, 2 },
                    { 20, 2 },
                    { 21, 2 },
                    { 22, 2 },
                    { 23, 2 },
                    { 24, 2 },
                    { 25, 2 },
                    { 26, 2 },
                    { 28, 2 },
                    { 29, 2 },
                    { 30, 2 },
                    { 31, 2 },
                    { 32, 2 },
                    { 33, 2 },
                    { 34, 2 },
                    { 35, 2 },
                    { 36, 2 },
                    { 37, 2 },
                    { 13, 3 },
                    { 14, 3 },
                    { 15, 3 },
                    { 17, 3 },
                    { 18, 3 },
                    { 19, 3 },
                    { 20, 3 },
                    { 21, 3 },
                    { 22, 3 },
                    { 23, 3 },
                    { 24, 3 },
                    { 25, 3 },
                    { 26, 3 },
                    { 28, 3 },
                    { 30, 3 },
                    { 32, 3 },
                    { 36, 3 },
                    { 37, 3 },
                    { 13, 4 },
                    { 14, 4 },
                    { 17, 4 },
                    { 18, 4 },
                    { 19, 4 },
                    { 20, 4 },
                    { 21, 4 },
                    { 22, 4 },
                    { 24, 4 },
                    { 26, 4 },
                    { 28, 4 },
                    { 30, 4 },
                    { 14, 5 },
                    { 17, 5 },
                    { 19, 5 },
                    { 21, 5 },
                    { 22, 5 },
                    { 26, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_attachment_bug_id",
                table: "attachment",
                column: "bug_id");

            migrationBuilder.CreateIndex(
                name: "ix_bug_assignee_id",
                table: "bug",
                column: "assignee_id");

            migrationBuilder.CreateIndex(
                name: "ix_bug_priority_id",
                table: "bug",
                column: "priority_id");

            migrationBuilder.CreateIndex(
                name: "ix_bug_project_id",
                table: "bug",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_bug_reporter_id",
                table: "bug",
                column: "reporter_id");

            migrationBuilder.CreateIndex(
                name: "ix_bug_status_id",
                table: "bug",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_bug_title",
                table: "bug",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_bug_custom_field_bug_id",
                table: "bug_custom_field",
                column: "bug_id");

            migrationBuilder.CreateIndex(
                name: "ix_bug_custom_field_custom_field_id",
                table: "bug_custom_field",
                column: "custom_field_id");

            migrationBuilder.CreateIndex(
                name: "ix_bug_history_bug_id",
                table: "bug_history",
                column: "bug_id");

            migrationBuilder.CreateIndex(
                name: "ix_bug_history_changed_by",
                table: "bug_history",
                column: "changed_by");

            migrationBuilder.CreateIndex(
                name: "ix_bug_repository_bug_id",
                table: "bug_repository",
                column: "bug_id");

            migrationBuilder.CreateIndex(
                name: "ix_bug_repository_repository_id",
                table: "bug_repository",
                column: "repository_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_bug_id",
                table: "comment",
                column: "bug_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_user_id",
                table: "comment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_custom_field_name",
                table: "custom_field",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_custom_field_project_id",
                table: "custom_field",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_notification_bug_id",
                table: "notification",
                column: "bug_id");

            migrationBuilder.CreateIndex(
                name: "ix_notification_user_id",
                table: "notification",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_role_user_project_role_id",
                table: "project_role_user",
                column: "project_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_project_role_user_user_id",
                table: "project_role_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_repository_project_id",
                table: "repository",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_repository_url",
                table: "repository",
                column: "url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_permission_id",
                table: "role_permission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_role_id",
                table: "role_permission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_time_track_bug_id",
                table: "time_track",
                column: "bug_id");

            migrationBuilder.CreateIndex(
                name: "ix_time_track_user_id",
                table: "time_track",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_role_id",
                table: "user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_user_id",
                table: "user_role",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attachment");

            migrationBuilder.DropTable(
                name: "bug_custom_field");

            migrationBuilder.DropTable(
                name: "bug_history");

            migrationBuilder.DropTable(
                name: "bug_repository");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "project_role_user");

            migrationBuilder.DropTable(
                name: "role_permission");

            migrationBuilder.DropTable(
                name: "time_track");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "custom_field");

            migrationBuilder.DropTable(
                name: "repository");

            migrationBuilder.DropTable(
                name: "project_role");

            migrationBuilder.DropTable(
                name: "permission");

            migrationBuilder.DropTable(
                name: "bug");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "bug_priority");

            migrationBuilder.DropTable(
                name: "bug_status");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
