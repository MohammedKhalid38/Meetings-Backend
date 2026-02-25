using MediatR;
using Meetings.Application.Common.Interfaces;

namespace Meetings.Application.Features.Roles.Commands.UpdateRole;

public record UpdateRoleCommand(Guid Id, string Name, string? Description)
    : IRequest<(bool Succeeded, IEnumerable<string> Errors)>;

public class UpdateRoleCommandHandler(IRoleService roleService)
    : IRequestHandler<UpdateRoleCommand, (bool Succeeded, IEnumerable<string> Errors)>
{
    public async Task<(bool Succeeded, IEnumerable<string> Errors)> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        => await roleService.UpdateRoleAsync(request.Id, request.Name, request.Description, cancellationToken);
}
