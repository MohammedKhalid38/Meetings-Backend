namespace Meetings.Application.Features.BoardMembers.DTOs;

public record BoardMemberDto(
    Guid Id,
    Guid BoardId,
    Guid UserId,
    string Role,
    DateTime JoinedAt,
    bool IsActive
);
