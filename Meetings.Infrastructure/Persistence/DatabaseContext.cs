using Meetings.Application.Common.Interfaces;
using Meetings.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Infrastructure.Persistence;

public class DatabaseContext(DbContextOptions<DatabaseContext> options)
    : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>(options), IApplicationDbContext
{
    public DbSet<Board> Boards => Set<Board>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<BoardMember> BoardMembers => Set<BoardMember>();
    public DbSet<Meeting> Meetings => Set<Meeting>();
    public DbSet<MeetingMember> MeetingMembers => Set<MeetingMember>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<TopicFile> TopicFiles => Set<TopicFile>();
    public DbSet<TopicComment> TopicComments => Set<TopicComment>();
    public DbSet<Decision> Decisions => Set<Decision>();
    public DbSet<Poll> Polls => Set<Poll>();
    public DbSet<PollAnswer> PollAnswers => Set<PollAnswer>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<MeetingMinute> MeetingMinutes => Set<MeetingMinute>();
    public DbSet<MeetingMinuteApproval> MeetingMinuteApprovals => Set<MeetingMinuteApproval>();
    public DbSet<MeetingMinuteSignature> MeetingMinuteSignatures => Set<MeetingMinuteSignature>();

    DbSet<User> IApplicationDbContext.Users => Set<User>();
    DbSet<Role> IApplicationDbContext.Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Friendly Identity table names
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

        modelBuilder.Entity<Board>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<Attachment>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.FileName).IsRequired().HasMaxLength(500);
            e.Property(x => x.FilePath).IsRequired().HasMaxLength(2000);
            e.Property(x => x.ContentType).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<BoardMember>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Role).IsRequired().HasMaxLength(50);
            e.HasOne(x => x.Board).WithMany().HasForeignKey(x => x.BoardId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Meeting>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).IsRequired().HasMaxLength(300);
            e.Property(x => x.Status).IsRequired().HasMaxLength(50);
            e.HasOne(x => x.Board).WithMany().HasForeignKey(x => x.BoardId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MeetingMember>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Role).IsRequired().HasMaxLength(50);
            e.Property(x => x.Status).IsRequired().HasMaxLength(50);
            e.HasOne(x => x.Meeting).WithMany().HasForeignKey(x => x.MeetingId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Topic>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).IsRequired().HasMaxLength(300);
            e.Property(x => x.Status).IsRequired().HasMaxLength(50);
            e.HasOne(x => x.Meeting).WithMany().HasForeignKey(x => x.MeetingId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TopicFile>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Topic).WithMany().HasForeignKey(x => x.TopicId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Attachment).WithMany().HasForeignKey(x => x.AttachmentId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TopicComment>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Content).IsRequired().HasMaxLength(5000);
            e.HasOne(x => x.Topic).WithMany().HasForeignKey(x => x.TopicId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Decision>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Content).IsRequired().HasMaxLength(5000);
            e.Property(x => x.Status).IsRequired().HasMaxLength(50);
            e.HasOne(x => x.Topic).WithMany().HasForeignKey(x => x.TopicId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Poll>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Question).IsRequired().HasMaxLength(1000);
            e.Property(x => x.Status).IsRequired().HasMaxLength(50);
            e.HasOne(x => x.Topic).WithMany().HasForeignKey(x => x.TopicId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PollAnswer>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.AnswerText).IsRequired().HasMaxLength(1000);
            e.HasOne(x => x.Poll).WithMany().HasForeignKey(x => x.PollId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Note>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Content).IsRequired().HasMaxLength(10000);
            e.HasOne(x => x.Meeting).WithMany().HasForeignKey(x => x.MeetingId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<MeetingMinute>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Status).IsRequired().HasMaxLength(50);
            e.HasOne(x => x.Meeting).WithMany().HasForeignKey(x => x.MeetingId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MeetingMinuteApproval>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Status).IsRequired().HasMaxLength(50);
            e.HasOne(x => x.MeetingMinute).WithMany().HasForeignKey(x => x.MeetingMinuteId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Approver).WithMany().HasForeignKey(x => x.ApproverId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<MeetingMinuteSignature>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.MeetingMinute).WithMany().HasForeignKey(x => x.MeetingMinuteId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Signer).WithMany().HasForeignKey(x => x.SignedBy).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
