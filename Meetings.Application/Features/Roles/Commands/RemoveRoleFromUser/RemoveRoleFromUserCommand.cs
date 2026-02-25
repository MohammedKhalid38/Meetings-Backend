using MediatR;
using Meetings.Application.Common.Interfaces;

namespace Meetings.Application.Features.Roles.Commands.RemoveRoleFromUser;

public record RemoveRoleFromUserCommand(Guid UserId, string RoleName) : IRequest<(bool Succeeded, IEnumerable<string> Errors)>;

public class RemoveRoleFromUserCommandHandler(IRoleService roleService)
    : IRequestHandler<RemoveRoleFromUserCommand, (bool Succeeded, IEnumerable<string> Errors)>
{
    public async Task<(bool Succeeded, IEnumerable<string> Errors)> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        => await roleService.RemoveRoleFromUserAsync(request.UserId, request.RoleName, cancellationToken);
}
