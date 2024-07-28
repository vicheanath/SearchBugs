using Shared.Primitives;

namespace SearchBugs.Domain.Roles;

public class Permission : Enumeration<Permission>
{
    #region User Permmisions
    public static readonly Permission CreateUser = new(1, nameof(CreateUser), "Can create a new user");
    public static readonly Permission ViewUserDetails = new(2, nameof(ViewUserDetails), "Can view user details");
    public static readonly Permission UpdateUser = new(3, nameof(UpdateUser), "Can update user details");
    public static readonly Permission DeleteUser = new(4, nameof(DeleteUser), "Can delete a user");
    public static readonly Permission AuthenticateUser = new(5, nameof(AuthenticateUser), "Can authenticate a user");
    public static readonly Permission ChangeUserPassword = new(6, nameof(ChangeUserPassword), "Can change user password");
    public static readonly Permission ListAllUsers = new(7, nameof(ListAllUsers), "Can list all users");
    #endregion

    #region Project 
    public static readonly Permission CreateProject = new(8, nameof(CreateProject), "Can Create Project");
    public static readonly Permission ViewProjectDetails = new(9, nameof(ViewUserDetails), "Can View Project Details");
    public static readonly Permission UpdateProject = new(10, nameof(UpdateProject), "Can Update Project");
    public static readonly Permission DeleteProject = new(11, nameof(DeleteProject), "Can Delete Project");
    public static readonly Permission ListAllProjects = new(12, nameof(ListAllProjects), "Can List All Projects");

    #endregion
    #region Bug 
    public static readonly Permission CreateBug = new(13, nameof(CreateBug), "Can Create Bug");
    public static readonly Permission ViewBugDetails = new(14, nameof(ViewBugDetails), "Can View Bug Details");
    public static readonly Permission UpdateBug = new(15, nameof(UpdateBug), "Can Update Bug");
    public static readonly Permission DeleteBug = new(16, nameof(DeleteBug), "Can Delete Bug");
    public static readonly Permission ListAllBugs = new(17, nameof(ListAllBugs), "Can List All Bugs");
    public static readonly Permission AddCommentToBug = new(18, nameof(AddCommentToBug), "Can Add Comment To Bug");
    public static readonly Permission ViewBugComments = new(19, nameof(ViewBugComments), "Can View Bug Comments");
    public static readonly Permission AddAttachmentToBug = new(20, nameof(AddAttachmentToBug), "Can Add Attachment To Bug");
    public static readonly Permission ViewBugAttachments = new(21, nameof(ViewBugAttachments), "Can View Bug Attachments");
    public static readonly Permission ViewBugHistory = new(22, nameof(ViewBugHistory), "Can View Bug History");
    public static readonly Permission TrackTimeSpentOnBug = new(23, nameof(TrackTimeSpentOnBug), "Can Track Time Spent On Bug");
    public static readonly Permission ViewTimeSpentOnBug = new(24, nameof(ViewTimeSpentOnBug), "Can View Time Spent On Bug");
    public static readonly Permission AddCustomFieldToBug = new(25, nameof(AddCustomFieldToBug), "Can Add Custom Field To Bug");
    public static readonly Permission ViewCustomFieldOnBug = new(26, nameof(ViewCustomFieldOnBug), "Can View Custom Field On Bug");


    #endregion

    #region Notification 
    public static readonly Permission ViewNotification = new(28, nameof(ViewNotification), "Can View Notification");
    public static readonly Permission DeleteNotification = new(29, nameof(DeleteNotification), "Can Delete Notification");
    public static readonly Permission MarkNotificationAsRead = new(30, nameof(MarkNotificationAsRead), "Can Mark Notification As Read");
    #endregion

    #region Repository 
    public static readonly Permission CreateRepository = new(31, nameof(CreateRepository), "Can Create Repository");
    public static readonly Permission ViewRepositoryDetails = new(32, nameof(ViewRepositoryDetails), "Can View Repository Details");
    public static readonly Permission UpdateRepository = new(33, nameof(UpdateRepository), "Can Update Repository");
    public static readonly Permission DeleteRepository = new(34, nameof(DeleteRepository), "Can Delete Repository");
    public static readonly Permission ListAllRepositories = new(35, nameof(ListAllRepositories), "Can List All Repositories");
    public static readonly Permission LinkBugToRepository = new(36, nameof(LinkBugToRepository), "Can Link Bug To Repository");
    public static readonly Permission ViewBugRepository = new(37, nameof(ViewBugRepository), "Can View Bug Repository");
    #endregion

    private Permission(int id, string name, string description)
       : base(id, name) =>
       Description = description;

    /// <summary>
    /// Initializes a new instance of the <see cref="Permission"/> class.
    /// </summary>
    private Permission() => Description = string.Empty;

    /// <summary>
    /// Gets the description.
    /// </summary>
    public string Description { get; private init; }


}