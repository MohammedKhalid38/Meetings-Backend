namespace Meetings.Application.Features.MeetingMinutes.DTOs;

public record MeetingMinuteDto(
    Guid Id,
    Guid MeetingId,
    string Content,
    string Status,
    Guid CreatedBy,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
