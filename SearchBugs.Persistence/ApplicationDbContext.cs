using MediatR;
using Microsoft.EntityFrameworkCore;
using SearchBugs.Domain;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Notifications;
using SearchBugs.Domain.Projects;
using SearchBugs.Domain.Roles;
using SearchBugs.Domain.Users;
using Shared.Primitives;
namespace SearchBugs.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<Project> Projects { get; private set; }
    public DbSet<User> Users { get; private set; }
    public DbSet<Permission> Permissions { get; private set; }
    public DbSet<Role> Roles { get; private set; }
    public DbSet<Bug> Bugs { get; private set; }
    public DbSet<BugStatus> BugStatuses { get; private set; }
    public DbSet<BugPriority> BugPriorities { get; private set; }
    public DbSet<Comment> Comments { get; private set; }
    public DbSet<Attachment> Attachments { get; private set; }
    public DbSet<BugCustomField> BugCustomFields { get; private set; }
    public DbSet<CustomField> CustomFields { get; private set; }
    public DbSet<Notification> Notifications { get; private set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker
            .Entries<IEntity>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany(e =>
            {
                var domainEvents = e.GetDomainEvents();

                e.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
