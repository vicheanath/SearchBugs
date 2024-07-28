
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Users;


namespace SearchBugs.Persistence.Configurations;

internal sealed class RolePermissionConfiguration
    : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
            GuestPermissionsSeed().ToArray(),
            ReporterPermissionsSeed().ToArray(),
            DeveloperPermissionsSeed().ToArray(),
            ProjectManagerPermissionsSeed().ToArray(),
            AdminPermissionsSeed().ToArray()
        );
    }

    private static RolePermission Create(
        Role role, Permission permission)
    {
        return new RolePermission
        {
            RoleId = role.Id,
            PermissionId = permission.Id
        };
    }
    private static List<RolePermission> GuestPermissionsSeed()
    {
        var permissions = new List<RolePermission>
        {
            // Bug View Bug Details, List All Bugs, View Bug Comments, View Bug Attachments, View Bug History, View Bug Time Tracking, View Bug Custom Fields
            Create(Role.Guest, Permission.ViewBugDetails),
            Create(Role.Guest, Permission.ListAllBugs),
            Create(Role.Guest, Permission.ViewBugComments),
            Create(Role.Guest, Permission.ViewBugAttachments),
            Create(Role.Guest, Permission.ViewBugHistory),
            Create(Role.Guest, Permission.ViewCustomFieldOnBug),
        };
        return permissions;
    }

    private static List<RolePermission> ReporterPermissionsSeed()
    {
        var permissions = new List<RolePermission>
        {
            // Bug View Bug Details, List All Bugs, Add Comment to Bug, View Bug Comments, Add Attachment to Bug, View Bug Attachments, View Bug History, View Bug Time Tracking, View Bug Custom Fields
            Create(Role.Reporter, Permission.CreateBug),
            Create(Role.Reporter, Permission.ViewBugDetails),
            Create(Role.Reporter, Permission.ListAllBugs),
            Create(Role.Reporter, Permission.AddCommentToBug),
            Create(Role.Reporter, Permission.ViewBugComments),
            Create(Role.Reporter, Permission.AddAttachmentToBug),
            Create(Role.Reporter, Permission.ViewBugAttachments),
            Create(Role.Reporter, Permission.ViewBugHistory),
            Create(Role.Reporter, Permission.ViewTimeSpentOnBug),
            Create(Role.Reporter, Permission.ViewCustomFieldOnBug),
            // Notification View User Notifications, Mark Notification as Read
            Create(Role.Reporter, Permission.ViewNotification),
            Create(Role.Reporter, Permission.MarkNotificationAsRead)
        };
        return permissions;
    }


    private static List<RolePermission> DeveloperPermissionsSeed()
    {
        var permissions = new List<RolePermission>
        {
            // Bug Create Bug, View Bug Details, Update Bug, List All Bugs, Add Comment to Bug, View Bug Comments, Add Attachment to Bug, View Bug Attachments, View Bug History, Track Time for Bug, View Bug Time Tracking, Add Custom Field to Bug, View Bug Custom Fields
            Create(Role.Developer, Permission.CreateBug),
            Create(Role.Developer, Permission.ViewBugDetails),
            Create(Role.Developer, Permission.UpdateBug),
            Create(Role.Developer, Permission.ListAllBugs),
            Create(Role.Developer, Permission.AddCommentToBug),
            Create(Role.Developer, Permission.ViewBugComments),
            Create(Role.Developer, Permission.AddAttachmentToBug),
            Create(Role.Developer, Permission.ViewBugAttachments),
            Create(Role.Developer, Permission.ViewBugHistory),
            Create(Role.Developer, Permission.TrackTimeSpentOnBug),
            Create(Role.Developer, Permission.ViewTimeSpentOnBug),
            Create(Role.Developer, Permission.AddCustomFieldToBug),
            Create(Role.Developer, Permission.ViewCustomFieldOnBug),
            // Notification View User Notifications, Mark Notification as Read, Delete Notification
            Create(Role.Developer, Permission.ViewNotification),
            Create(Role.Developer, Permission.MarkNotificationAsRead),

            // Repository 
            Create(Role.Developer, Permission.ViewRepositoryDetails),
            Create(Role.Developer, Permission.LinkBugToRepository),
            Create(Role.Developer, Permission.ViewBugRepository)
        };
        return permissions;
    }

    private static List<RolePermission> ProjectManagerPermissionsSeed()
    {
        var permissions = new List<RolePermission>
        {
            // Project
            Create(Role.ProjectManager, Permission.ListAllUsers),
            Create(Role.ProjectManager, Permission.CreateProject),
            Create(Role.ProjectManager, Permission.ViewProjectDetails),
            Create(Role.ProjectManager, Permission.UpdateProject),
            Create(Role.ProjectManager, Permission.DeleteProject),
            Create(Role.ProjectManager, Permission.ListAllProjects),
            // Bug  Create Bug, View Bug Details, Update Bug, Delete Bug, List All Bugs, Add Comment to Bug, View Bug Comments, Add Attachment to Bug, View Bug Attachments, View Bug History, Track Time for Bug, View Bug Time Tracking, Add Custom Field to Bug, View Bug Custom Fields
            Create(Role.ProjectManager, Permission.CreateBug),
            Create(Role.ProjectManager, Permission.ViewBugDetails),
            Create(Role.ProjectManager, Permission.UpdateBug),
            Create(Role.ProjectManager, Permission.DeleteBug),
            Create(Role.ProjectManager, Permission.ListAllBugs),
            Create(Role.ProjectManager, Permission.AddCommentToBug),
            Create(Role.ProjectManager, Permission.ViewBugComments),
            Create(Role.ProjectManager, Permission.AddAttachmentToBug),
            Create(Role.ProjectManager, Permission.ViewBugAttachments),
            Create(Role.ProjectManager, Permission.ViewBugHistory),
            Create(Role.ProjectManager, Permission.TrackTimeSpentOnBug),
            Create(Role.ProjectManager, Permission.ViewTimeSpentOnBug),
            Create(Role.ProjectManager, Permission.AddCustomFieldToBug),
            Create(Role.ProjectManager, Permission.ViewCustomFieldOnBug),
            // Notification View User Notifications, Mark Notification as Read, Delete Notification
            Create(Role.ProjectManager, Permission.ViewNotification),
            Create(Role.ProjectManager, Permission.MarkNotificationAsRead),
            Create(Role.ProjectManager, Permission.DeleteNotification),
            // Repository Create Repository, View Repository Details, Update Repository, Delete Repository, List All Repositories, Link Bug to Repository, View Bug Repository
            Create(Role.ProjectManager, Permission.CreateRepository),
            Create(Role.ProjectManager, Permission.ViewRepositoryDetails),
            Create(Role.ProjectManager, Permission.UpdateRepository),
            Create(Role.ProjectManager, Permission.DeleteRepository),
            Create(Role.ProjectManager, Permission.ListAllRepositories),
            Create(Role.ProjectManager, Permission.LinkBugToRepository),
            Create(Role.ProjectManager, Permission.ViewBugRepository)

        };
        return permissions;
    }

    private static List<RolePermission> AdminPermissionsSeed()
    {
        var permissions = new List<RolePermission>
        {
            Create(Role.Admin, Permission.CreateUser),
            Create(Role.Admin, Permission.ViewUserDetails),
            Create(Role.Admin, Permission.UpdateUser),
            Create(Role.Admin, Permission.DeleteUser),
            Create(Role.Admin, Permission.AuthenticateUser),
            Create(Role.Admin, Permission.ChangeUserPassword),
            Create(Role.Admin, Permission.ListAllUsers),
            Create(Role.Admin, Permission.CreateProject),
            Create(Role.Admin, Permission.ViewProjectDetails),
            Create(Role.Admin, Permission.UpdateProject),
            Create(Role.Admin, Permission.DeleteProject),
            Create(Role.Admin, Permission.ListAllProjects),
            Create(Role.Admin, Permission.CreateBug),
            Create(Role.Admin, Permission.ViewBugDetails),
            Create(Role.Admin, Permission.UpdateBug),
            Create(Role.Admin, Permission.DeleteBug),
            Create(Role.Admin, Permission.ListAllBugs),
            Create(Role.Admin, Permission.AddCommentToBug),
            Create(Role.Admin, Permission.ViewBugComments),
            Create(Role.Admin, Permission.AddAttachmentToBug),
            Create(Role.Admin, Permission.ViewBugAttachments),
            Create(Role.Admin, Permission.ViewBugHistory),
            Create(Role.Admin, Permission.TrackTimeSpentOnBug),
            Create(Role.Admin, Permission.ViewTimeSpentOnBug),
            Create(Role.Admin, Permission.AddCustomFieldToBug),
            Create(Role.Admin, Permission.ViewCustomFieldOnBug),
            Create(Role.Admin, Permission.ViewNotification),
            Create(Role.Admin, Permission.DeleteNotification),
            Create(Role.Admin, Permission.MarkNotificationAsRead),
            Create(Role.Admin, Permission.CreateRepository),
            Create(Role.Admin, Permission.ViewRepositoryDetails),
            Create(Role.Admin, Permission.UpdateRepository),
            Create(Role.Admin, Permission.DeleteRepository),
            Create(Role.Admin, Permission.ListAllRepositories),
            Create(Role.Admin, Permission.LinkBugToRepository),
            Create(Role.Admin, Permission.ViewBugRepository)
        };
        return permissions;
    }

}
