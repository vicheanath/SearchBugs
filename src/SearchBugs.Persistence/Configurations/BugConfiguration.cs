﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchBugs.Domain.Bugs;
using SearchBugs.Domain.Projects;
using SearchBugs.Domain.Users;
using SearchBugs.Persistence.Constants;
using Shared.Extensions;

namespace SearchBugs.Persistence.Configurations;

internal sealed class BugConfiguration : IEntityTypeConfiguration<Bug>
{
    public void Configure(EntityTypeBuilder<Bug> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<Bug> builder)
    {
        builder.ToTable(TableNames.Bugs);
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .HasConversion(id => id.Value, value => new BugId(value))
            .ValueGeneratedNever();
        builder.Property(b => b.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(b => b.Description)
            .HasMaxLength(2000);
        builder.Property(b => b.Severity)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(b => b.CreatedOnUtc)
            .IsRequired();
        builder.Property(b => b.ModifiedOnUtc)
            .IsRequired(false);
        builder.Property(b => b.ProjectId)
            .HasConversion(id => id.Value, value => new ProjectId(value))
            .IsRequired();
        builder.Property(b => b.StatusId);

        builder.Property(b => b.PriorityId);
        builder.Property(b => b.AssigneeId)
            .HasConversion(id => id.Value, value => new UserId(value));
        builder.Property(b => b.ReporterId)
            .HasConversion(id => id.Value, value => new UserId(value));


    }

    private static void ConfigureRelationships(EntityTypeBuilder<Bug> builder)
    {
        builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(b => b.ProjectId);
        builder.HasOne(b => b.Status)
            .WithMany()
            .HasForeignKey(b => b.StatusId);
        builder.HasOne(b => b.Priority)
            .WithMany()
            .HasForeignKey(b => b.PriorityId);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(b => b.AssigneeId);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(b => b.ReporterId);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Bug> builder)
    {
        builder.HasIndex(b => b.Title)
            .IsUnique();
    }
}
