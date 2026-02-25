using MediatR;
using Meetings.Application.Features.MeetingMinuteApprovals.Commands.CreateMeetingMinuteApproval;
using Meetings.Application.Features.MeetingMinuteApprovals.Commands.DeleteMeetingMinuteApproval;
using Meetings.Application.Features.MeetingMinuteApprovals.Commands.UpdateMeetingMinuteApproval;
using Meetings.Application.Features.MeetingMinuteApprovals.Queries.GetMeetingMinuteApproval;
using Meetings.Application.Features.MeetingMinuteApprovals.Queries.GetMeetingMinuteApprovals;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingMinuteApprovalsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? meetingMinuteId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetMeetingMinuteApprovalsQuery(meetingMinuteId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var approval = await mediator.Send(new GetMeetingMinuteApprovalQuery(id), cancellationToken);
        return approval is null ? NotFound() : Ok(approval);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMeetingMinuteApprovalCommand command, CancellationToken cancellationToken)
    {
        var approval = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = approval.Id }, approval);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMeetingMinuteApprovalRequest request, CancellationToken cancellationToken)
    {
        var approval = await mediator.Send(new UpdateMeetingMinuteApprovalCommand(id, request.Status, request.Comments), cancellationToken);
        return approval is null ? NotFound() : Ok(approval);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteMeetingMinuteApprovalCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateMeetingMinuteApprovalRequest(string Status, string? Comments);
