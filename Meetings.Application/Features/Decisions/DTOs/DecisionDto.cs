namespace Meetings.Application.Features.Decisions.DTOs;

public record DecisionDto(
    Guid Id,
    Guid TopicId,
    string Content,
    Guid MadeBy,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
