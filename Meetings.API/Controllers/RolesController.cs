using MediatR;
using Meetings.Application.Features.Roles.Commands.AssignRoleToUser;
using Meetings.Application.Features.Roles.Commands.CreateRole;
using Meetings.Application.Features.Roles.Commands.DeleteRole;
using Meetings.Application.Features.Roles.Commands.RemoveRoleFromUser;
using Meetings.Application.Features.Roles.Commands.UpdateRole;
using Meetings.Application.Features.Roles.Queries.GetRole;
using Meetings.Application.Features.Roles.Queries.GetRoles;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetRolesQuery(), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var role = await mediator.Send(new GetRoleQuery(id), cancellationToken);
        return role is null ? NotFound() : Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var (succeeded, role, errors) = await mediator.Send(command, cancellationToken);
        if (!succeeded) return BadRequest(new { errors });
        return CreatedAtAction(nameof(GetById), new { id = role!.Id }, role);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        var (succeeded, errors) = await mediator.Send(new UpdateRoleCommand(id, request.Name, request.Description), cancellationToken);
        return succeeded ? NoContent() : BadRequest(new { errors });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteRoleCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    [HttpPost("assign")]
    public async Task<IActionResult> Assign([FromBody] AssignRoleToUserCommand command, CancellationToken cancellationToken)
    {
        var (succeeded, errors) = await mediator.Send(command, cancellationToken);
        return succeeded ? NoContent() : BadRequest(new { errors });
    }

    [HttpPost("remove")]
    public async Task<IActionResult> Remove([FromBody] RemoveRoleFromUserCommand command, CancellationToken cancellationToken)
    {
        var (succeeded, errors) = await mediator.Send(command, cancellationToken);
        return succeeded ? NoContent() : BadRequest(new { errors });
    }
}

public record UpdateRoleRequest(string Name, string? Description);
