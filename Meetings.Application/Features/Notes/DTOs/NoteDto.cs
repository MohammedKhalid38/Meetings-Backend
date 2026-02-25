namespace Meetings.Application.Features.Notes.DTOs;

public record NoteDto(
    Guid Id,
    Guid MeetingId,
    string Content,
    Guid UserId,
    bool IsPrivate,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
