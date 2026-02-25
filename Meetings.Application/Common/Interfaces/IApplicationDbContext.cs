using Meetings.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Board> Boards { get; }
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Attachment> Attachments { get; }
    DbSet<BoardMember> BoardMembers { get; }
    DbSet<Meeting> Meetings { get; }
    DbSet<MeetingMember> MeetingMembers { get; }
    DbSet<Topic> Topics { get; }
    DbSet<TopicFile> TopicFiles { get; }
    DbSet<TopicComment> TopicComments { get; }
    DbSet<Decision> Decisions { get; }
    DbSet<Poll> Polls { get; }
    DbSet<PollAnswer> PollAnswers { get; }
    DbSet<Note> Notes { get; }
    DbSet<MeetingMinute> MeetingMinutes { get; }
    DbSet<MeetingMinuteApproval> MeetingMinuteApprovals { get; }
    DbSet<MeetingMinuteSignature> MeetingMinuteSignatures { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
