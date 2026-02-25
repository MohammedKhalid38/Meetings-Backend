using MediatR;
using Meetings.Application.Common.Interfaces;

namespace Meetings.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid Id, string FirstName, string LastName, string? ProfilePicture, bool IsActive)
    : IRequest<(bool Succeeded, IEnumerable<string> Errors)>;

public class UpdateUserCommandHandler(IIdentityService identityService)
    : IRequestHandler<UpdateUserCommand, (bool Succeeded, IEnumerable<string> Errors)>
{
    public async Task<(bool Succeeded, IEnumerable<string> Errors)> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        => await identityService.UpdateUserAsync(request.Id, request.FirstName, request.LastName, request.ProfilePicture, request.IsActive, cancellationToken);
}
