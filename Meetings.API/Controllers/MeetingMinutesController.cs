using MediatR;
using Meetings.Application.Features.MeetingMinutes.Commands.CreateMeetingMinute;
using Meetings.Application.Features.MeetingMinutes.Commands.DeleteMeetingMinute;
using Meetings.Application.Features.MeetingMinutes.Commands.UpdateMeetingMinute;
using Meetings.Application.Features.MeetingMinutes.Queries.GetMeetingMinute;
using Meetings.Application.Features.MeetingMinutes.Queries.GetMeetingMinutes;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingMinutesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? meetingId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetMeetingMinutesQuery(meetingId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var minute = await mediator.Send(new GetMeetingMinuteQuery(id), cancellationToken);
        return minute is null ? NotFound() : Ok(minute);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMeetingMinuteCommand command, CancellationToken cancellationToken)
    {
        var minute = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = minute.Id }, minute);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMeetingMinuteRequest request, CancellationToken cancellationToken)
    {
        var minute = await mediator.Send(new UpdateMeetingMinuteCommand(id, request.Content, request.Status), cancellationToken);
        return minute is null ? NotFound() : Ok(minute);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteMeetingMinuteCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateMeetingMinuteRequest(string Content, string Status);
