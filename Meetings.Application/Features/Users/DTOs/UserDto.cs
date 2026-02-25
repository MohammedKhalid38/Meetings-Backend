namespace Meetings.Application.Features.Users.DTOs;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string? ProfilePicture,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
