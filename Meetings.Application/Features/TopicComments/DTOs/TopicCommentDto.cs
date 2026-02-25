namespace Meetings.Application.Features.TopicComments.DTOs;

public record TopicCommentDto(
    Guid Id,
    Guid TopicId,
    string Content,
    Guid UserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
