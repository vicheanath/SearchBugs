using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SearchBugs.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPullRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pull_request",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    repo_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    source_branch = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    target_branch = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    merged_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    merged_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pull_request", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pull_request");
        }
    }
}
