using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Roles.DTOs;

namespace Meetings.Application.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand(string Name, string? Description)
    : IRequest<(bool Succeeded, RoleDto? Role, IEnumerable<string> Errors)>;

public class CreateRoleCommandHandler(IRoleService roleService)
    : IRequestHandler<CreateRoleCommand, (bool Succeeded, RoleDto? Role, IEnumerable<string> Errors)>
{
    public async Task<(bool Succeeded, RoleDto? Role, IEnumerable<string> Errors)> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        => await roleService.CreateRoleAsync(request.Name, request.Description, cancellationToken);
}
