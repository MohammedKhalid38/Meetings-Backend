using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Users.DTOs;

namespace Meetings.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(string FirstName, string LastName, string Email, string UserName, string Password)
    : IRequest<(bool Succeeded, UserDto? User, IEnumerable<string> Errors)>;

public class CreateUserCommandHandler(IIdentityService identityService)
    : IRequestHandler<CreateUserCommand, (bool Succeeded, UserDto? User, IEnumerable<string> Errors)>
{
    public async Task<(bool Succeeded, UserDto? User, IEnumerable<string> Errors)> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        => await identityService.CreateUserAsync(request.FirstName, request.LastName, request.Email, request.UserName, request.Password, cancellationToken);
}
