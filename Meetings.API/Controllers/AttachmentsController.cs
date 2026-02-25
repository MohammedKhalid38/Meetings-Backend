using MediatR;
using Meetings.Application.Features.Attachments.Commands.CreateAttachment;
using Meetings.Application.Features.Attachments.Commands.DeleteAttachment;
using Meetings.Application.Features.Attachments.Queries.GetAttachment;
using Meetings.Application.Features.Attachments.Queries.GetAttachments;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttachmentsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetAttachmentsQuery(), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var attachment = await mediator.Send(new GetAttachmentQuery(id), cancellationToken);
        return attachment is null ? NotFound() : Ok(attachment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAttachmentCommand command, CancellationToken cancellationToken)
    {
        var attachment = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = attachment.Id }, attachment);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteAttachmentCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
