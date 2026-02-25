using MediatR;
using Meetings.Application.Features.Meetings.Commands.CreateMeeting;
using Meetings.Application.Features.Meetings.Commands.DeleteMeeting;
using Meetings.Application.Features.Meetings.Commands.UpdateMeeting;
using Meetings.Application.Features.Meetings.Queries.GetMeeting;
using Meetings.Application.Features.Meetings.Queries.GetMeetings;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? boardId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetMeetingsQuery(boardId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var meeting = await mediator.Send(new GetMeetingQuery(id), cancellationToken);
        return meeting is null ? NotFound() : Ok(meeting);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMeetingCommand command, CancellationToken cancellationToken)
    {
        var meeting = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = meeting.Id }, meeting);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMeetingRequest request, CancellationToken cancellationToken)
    {
        var meeting = await mediator.Send(new UpdateMeetingCommand(id, request.Title, request.Description, request.ScheduledAt, request.StartedAt, request.EndedAt, request.Status, request.Location, request.MeetingUrl), cancellationToken);
        return meeting is null ? NotFound() : Ok(meeting);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteMeetingCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateMeetingRequest(string Title, string? Description, DateTime ScheduledAt, DateTime? StartedAt, DateTime? EndedAt, string Status, string? Location, string? MeetingUrl);
