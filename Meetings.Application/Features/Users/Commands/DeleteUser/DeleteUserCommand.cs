using MediatR;
using Meetings.Application.Common.Interfaces;

namespace Meetings.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;

public class DeleteUserCommandHandler(IIdentityService identityService) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        => await identityService.DeleteUserAsync(request.Id, cancellationToken);
}
