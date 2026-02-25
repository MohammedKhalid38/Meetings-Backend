using MediatR;
using Meetings.Application.Features.MeetingMembers.Commands.CreateMeetingMember;
using Meetings.Application.Features.MeetingMembers.Commands.DeleteMeetingMember;
using Meetings.Application.Features.MeetingMembers.Commands.UpdateMeetingMember;
using Meetings.Application.Features.MeetingMembers.Queries.GetMeetingMember;
using Meetings.Application.Features.MeetingMembers.Queries.GetMeetingMembers;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingMembersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? meetingId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetMeetingMembersQuery(meetingId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var member = await mediator.Send(new GetMeetingMemberQuery(id), cancellationToken);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMeetingMemberCommand command, CancellationToken cancellationToken)
    {
        var member = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMeetingMemberRequest request, CancellationToken cancellationToken)
    {
        var member = await mediator.Send(new UpdateMeetingMemberCommand(id, request.Role, request.Status), cancellationToken);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteMeetingMemberCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateMeetingMemberRequest(string Role, string Status);
