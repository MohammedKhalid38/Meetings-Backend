namespace Meetings.Application.Features.Roles.DTOs;

public record RoleDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt
);
