namespace Meetings.Application.Features.Boards.DTOs;

public record BoardDto(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
