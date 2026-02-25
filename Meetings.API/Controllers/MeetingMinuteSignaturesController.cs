using MediatR;
using Meetings.Application.Features.MeetingMinuteSignatures.Commands.CreateMeetingMinuteSignature;
using Meetings.Application.Features.MeetingMinuteSignatures.Commands.DeleteMeetingMinuteSignature;
using Meetings.Application.Features.MeetingMinuteSignatures.Queries.GetMeetingMinuteSignature;
using Meetings.Application.Features.MeetingMinuteSignatures.Queries.GetMeetingMinuteSignatures;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingMinuteSignaturesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? meetingMinuteId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetMeetingMinuteSignaturesQuery(meetingMinuteId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var signature = await mediator.Send(new GetMeetingMinuteSignatureQuery(id), cancellationToken);
        return signature is null ? NotFound() : Ok(signature);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMeetingMinuteSignatureCommand command, CancellationToken cancellationToken)
    {
        var signature = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = signature.Id }, signature);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteMeetingMinuteSignatureCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
