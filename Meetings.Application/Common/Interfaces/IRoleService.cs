using Meetings.Application.Features.Roles.DTOs;

namespace Meetings.Application.Common.Interfaces;

public interface IRoleService
{
    Task<(bool Succeeded, RoleDto? Role, IEnumerable<string> Errors)> CreateRoleAsync(string name, string? description, CancellationToken cancellationToken);
    Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateRoleAsync(Guid roleId, string name, string? description, CancellationToken cancellationToken);
    Task<bool> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken);
    Task<(bool Succeeded, IEnumerable<string> Errors)> AssignRoleToUserAsync(Guid userId, string roleName, CancellationToken cancellationToken);
    Task<(bool Succeeded, IEnumerable<string> Errors)> RemoveRoleFromUserAsync(Guid userId, string roleName, CancellationToken cancellationToken);
}
