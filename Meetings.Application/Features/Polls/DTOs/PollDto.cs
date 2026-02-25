namespace Meetings.Application.Features.Polls.DTOs;

public record PollDto(
    Guid Id,
    Guid TopicId,
    string Question,
    bool IsMultipleChoice,
    string Status,
    Guid CreatedBy,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
