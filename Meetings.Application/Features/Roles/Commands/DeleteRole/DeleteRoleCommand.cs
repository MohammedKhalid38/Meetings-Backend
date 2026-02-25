using MediatR;
using Meetings.Application.Common.Interfaces;

namespace Meetings.Application.Features.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(Guid Id) : IRequest<bool>;

public class DeleteRoleCommandHandler(IRoleService roleService) : IRequestHandler<DeleteRoleCommand, bool>
{
    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        => await roleService.DeleteRoleAsync(request.Id, cancellationToken);
}
