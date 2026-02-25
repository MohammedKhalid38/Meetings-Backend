namespace Meetings.Application.Features.Topics.DTOs;

public record TopicDto(
    Guid Id,
    Guid MeetingId,
    string Title,
    string? Description,
    int OrderIndex,
    string Status,
    Guid CreatedBy,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
