using MediatR;
using Meetings.Application.Common.Interfaces;

namespace Meetings.Application.Features.Roles.Commands.AssignRoleToUser;

public record AssignRoleToUserCommand(Guid UserId, string RoleName) : IRequest<(bool Succeeded, IEnumerable<string> Errors)>;

public class AssignRoleToUserCommandHandler(IRoleService roleService)
    : IRequestHandler<AssignRoleToUserCommand, (bool Succeeded, IEnumerable<string> Errors)>
{
    public async Task<(bool Succeeded, IEnumerable<string> Errors)> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        => await roleService.AssignRoleToUserAsync(request.UserId, request.RoleName, cancellationToken);
}
