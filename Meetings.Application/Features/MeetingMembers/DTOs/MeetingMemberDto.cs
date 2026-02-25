namespace Meetings.Application.Features.MeetingMembers.DTOs;

public record MeetingMemberDto(
    Guid Id,
    Guid MeetingId,
    Guid UserId,
    string Role,
    string Status,
    DateTime? JoinedAt,
    DateTime CreatedAt
);
