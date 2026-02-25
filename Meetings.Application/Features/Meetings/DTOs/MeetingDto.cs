namespace Meetings.Application.Features.Meetings.DTOs;

public record MeetingDto(
    Guid Id,
    Guid BoardId,
    string Title,
    string? Description,
    DateTime ScheduledAt,
    DateTime? StartedAt,
    DateTime? EndedAt,
    string Status,
    string? Location,
    string? MeetingUrl,
    Guid CreatedBy,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
