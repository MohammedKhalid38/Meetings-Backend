using MediatR;
using Meetings.Application.Features.BoardMembers.Commands.CreateBoardMember;
using Meetings.Application.Features.BoardMembers.Commands.DeleteBoardMember;
using Meetings.Application.Features.BoardMembers.Commands.UpdateBoardMember;
using Meetings.Application.Features.BoardMembers.Queries.GetBoardMember;
using Meetings.Application.Features.BoardMembers.Queries.GetBoardMembers;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardMembersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? boardId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetBoardMembersQuery(boardId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var member = await mediator.Send(new GetBoardMemberQuery(id), cancellationToken);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBoardMemberCommand command, CancellationToken cancellationToken)
    {
        var member = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBoardMemberRequest request, CancellationToken cancellationToken)
    {
        var member = await mediator.Send(new UpdateBoardMemberCommand(id, request.Role, request.IsActive), cancellationToken);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteBoardMemberCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateBoardMemberRequest(string Role, bool IsActive);
