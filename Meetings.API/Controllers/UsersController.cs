using MediatR;
using Meetings.Application.Features.Users.Commands.ChangePassword;
using Meetings.Application.Features.Users.Commands.CreateUser;
using Meetings.Application.Features.Users.Commands.DeleteUser;
using Meetings.Application.Features.Users.Commands.UpdateUser;
using Meetings.Application.Features.Users.Queries.GetUser;
using Meetings.Application.Features.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetUsersQuery(), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await mediator.Send(new GetUserQuery(id), cancellationToken);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var (succeeded, user, errors) = await mediator.Send(command, cancellationToken);
        if (!succeeded) return BadRequest(new { errors });
        return CreatedAtAction(nameof(GetById), new { id = user!.Id }, user);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var (succeeded, errors) = await mediator.Send(new UpdateUserCommand(id, request.FirstName, request.LastName, request.ProfilePicture, request.IsActive), cancellationToken);
        return succeeded ? NoContent() : BadRequest(new { errors });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteUserCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    [HttpPost("{id:guid}/change-password")]
    public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var (succeeded, errors) = await mediator.Send(new ChangePasswordCommand(id, request.CurrentPassword, request.NewPassword), cancellationToken);
        return succeeded ? NoContent() : BadRequest(new { errors });
    }
}

public record UpdateUserRequest(string FirstName, string LastName, string? ProfilePicture, bool IsActive);
public record ChangePasswordRequest(string CurrentPassword, string NewPassword);
