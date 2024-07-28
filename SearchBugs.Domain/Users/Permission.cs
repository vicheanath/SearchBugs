using Shared.Primitives;

namespace SearchBugs.Domain.Users;

public class Permission : Enumeration<Permission>
{
    #region User Permmisions
    public static readonly Permission CreateUser = new(1, "Create User");
    public static readonly Permission ViewUserDetails = new(2, "View User Details");
    public static readonly Permission UpdateUser = new(3, "Update User");
    public static readonly Permission DeleteUser = new(4, "Delete User");
    public static readonly Permission AuthenticateUser = new(5, "Authenticate User");
    public static readonly Permission ChangeUserPassword = new(6, "Change User Password");
    public static readonly Permission ListAllUsers = new(7, "List All Users");
    #endregion

    #region Project Permissions
    public static readonly Permission CreateProject = new(8, "Create Project");
    public static readonly Permission ViewProjectDetails = new(9, "View Project Details");
    public static readonly Permission UpdateProject = new(10, "Update Project");
    public static readonly Permission DeleteProject = new(11, "Delete Project");
    public static readonly Permission ListAllProjects = new(12, "List All Projects");

    #endregion
    #region Bug Permissions
    public static readonly Permission CreateBug = new(13, "Create Bug");
    public static readonly Permission ViewBugDetails = new(14, "View Bug Details");
    public static readonly Permission UpdateBug = new(15, "Update Bug");
    public static readonly Permission DeleteBug = new(16, "Delete Bug");
    public static readonly Permission ListAllBugs = new(17, "List All Bugs");
    public static readonly Permission AddCommentToBug = new(18, "Add Comment To Bug");
    public static readonly Permission ViewBugComments = new(19, "View Bug Comments");
    public static readonly Permission AddAttachmentToBug = new(20, "Add Attachment To Bug");
    public static readonly Permission ViewBugAttachments = new(21, "View Bug Attachments");
    public static readonly Permission ViewBugHistory = new(22, "View Bug History");
    public static readonly Permission TrackTimeSpentOnBug = new(23, "Track Time Spent On Bug");
    public static readonly Permission ViewTimeSpentOnBug = new(24, "View Time Spent On Bug");
    public static readonly Permission AddCustomFieldToBug = new(25, "Add Custom Field To Bug");
    public static readonly Permission ViewCustomFieldOnBug = new(26, "View Custom Field On Bug");


    #endregion

    #region Notification Permissions
    public static readonly Permission ViewNotification = new(28, "View Notification");
    public static readonly Permission DeleteNotification = new(29, "Delete Notification");
    public static readonly Permission MarkNotificationAsRead = new(30, "Mark Notification As Read");
    #endregion

    #region Repository Permissions
    public static readonly Permission CreateRepository = new(31, "Create Repository");
    public static readonly Permission ViewRepositoryDetails = new(32, "View Repository Details");
    public static readonly Permission UpdateRepository = new(33, "Update Repository");
    public static readonly Permission DeleteRepository = new(34, "Delete Repository");
    public static readonly Permission ListAllRepositories = new(35, "List All Repositories");
    public static readonly Permission LinkBugToRepository = new(36, "Link Bug To Repository");
    public static readonly Permission ViewBugRepository = new(37, "View Bug Repository");
    #endregion

    public int Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public ICollection<Role> Roles { get; set; } = new List<Role>();

    public Permission(int id, string name)
        : base(id, name)
    {
    }
}
