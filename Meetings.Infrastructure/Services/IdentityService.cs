using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Users.DTOs;
using Meetings.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Meetings.Infrastructure.Services;

public class IdentityService(UserManager<User> userManager) : IIdentityService
{
    public async Task<(bool Succeeded, UserDto? User, IEnumerable<string> Errors)> CreateUserAsync(
        string firstName, string lastName, string email, string userName, string password, CancellationToken cancellationToken)
    {
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            UserName = userName,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return (false, null, result.Errors.Select(e => e.Description));

        var dto = new UserDto(user.Id, user.FirstName, user.LastName, user.Email!, user.UserName!, user.ProfilePicture, user.IsActive, user.CreatedAt, user.UpdatedAt);
        return (true, dto, []);
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(
        Guid userId, string firstName, string lastName, string? profilePicture, bool isActive, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null) return (false, ["User not found"]);

        user.FirstName = firstName;
        user.LastName = lastName;
        user.ProfilePicture = profilePicture;
        user.IsActive = isActive;
        user.UpdatedAt = DateTime.UtcNow;

        var result = await userManager.UpdateAsync(user);
        return result.Succeeded ? (true, []) : (false, result.Errors.Select(e => e.Description));
    }

    public async Task<bool> DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null) return false;
        var result = await userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> ChangePasswordAsync(
        Guid userId, string currentPassword, string newPassword, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null) return (false, ["User not found"]);
        var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.Succeeded ? (true, []) : (false, result.Errors.Select(e => e.Description));
    }
}
