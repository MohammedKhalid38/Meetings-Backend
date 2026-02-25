using Meetings.Application.Features.Users.DTOs;

namespace Meetings.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<(bool Succeeded, UserDto? User, IEnumerable<string> Errors)> CreateUserAsync(string firstName, string lastName, string email, string userName, string password, CancellationToken cancellationToken);
    Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(Guid userId, string firstName, string lastName, string? profilePicture, bool isActive, CancellationToken cancellationToken);
    Task<bool> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<(bool Succeeded, IEnumerable<string> Errors)> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken);
}
