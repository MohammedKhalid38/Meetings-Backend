namespace Meetings.Application.Features.PollAnswers.DTOs;

public record PollAnswerDto(
    Guid Id,
    Guid PollId,
    string AnswerText,
    Guid UserId,
    DateTime CreatedAt
);
