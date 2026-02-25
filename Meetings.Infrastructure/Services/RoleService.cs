using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Roles.DTOs;
using Meetings.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Meetings.Infrastructure.Services;

public class RoleService(RoleManager<Role> roleManager, UserManager<User> userManager) : IRoleService
{
    public async Task<(bool Succeeded, RoleDto? Role, IEnumerable<string> Errors)> CreateRoleAsync(
        string name, string? description, CancellationToken cancellationToken)
    {
        var role = new Role { Name = name, Description = description, CreatedAt = DateTime.UtcNow };
        var result = await roleManager.CreateAsync(role);
        if (!result.Succeeded) return (false, null, result.Errors.Select(e => e.Description));
        return (true, new RoleDto(role.Id, role.Name!, role.Description, role.CreatedAt), []);
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateRoleAsync(
        Guid roleId, string name, string? description, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role is null) return (false, ["Role not found"]);
        role.Name = name;
        role.Description = description;
        var result = await roleManager.UpdateAsync(role);
        return result.Succeeded ? (true, []) : (false, result.Errors.Select(e => e.Description));
    }

    public async Task<bool> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        if (role is null) return false;
        var result = await roleManager.DeleteAsync(role);
        return result.Succeeded;
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> AssignRoleToUserAsync(
        Guid userId, string roleName, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null) return (false, ["User not found"]);
        var result = await userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded ? (true, []) : (false, result.Errors.Select(e => e.Description));
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> RemoveRoleFromUserAsync(
        Guid userId, string roleName, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null) return (false, ["User not found"]);
        var result = await userManager.RemoveFromRoleAsync(user, roleName);
        return result.Succeeded ? (true, []) : (false, result.Errors.Select(e => e.Description));
    }
}
