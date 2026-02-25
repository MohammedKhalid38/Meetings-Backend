using MediatR;
using Meetings.Application.Common.Interfaces;

namespace Meetings.Application.Features.Users.Commands.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword)
    : IRequest<(bool Succeeded, IEnumerable<string> Errors)>;

public class ChangePasswordCommandHandler(IIdentityService identityService)
    : IRequestHandler<ChangePasswordCommand, (bool Succeeded, IEnumerable<string> Errors)>
{
    public async Task<(bool Succeeded, IEnumerable<string> Errors)> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        => await identityService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword, cancellationToken);
}
