using MediatR;
using Meetings.Application.Features.Notes.Commands.CreateNote;
using Meetings.Application.Features.Notes.Commands.DeleteNote;
using Meetings.Application.Features.Notes.Commands.UpdateNote;
using Meetings.Application.Features.Notes.Queries.GetNote;
using Meetings.Application.Features.Notes.Queries.GetNotes;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? meetingId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetNotesQuery(meetingId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var note = await mediator.Send(new GetNoteQuery(id), cancellationToken);
        return note is null ? NotFound() : Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteRequest request, CancellationToken cancellationToken)
    {
        var note = await mediator.Send(new UpdateNoteCommand(id, request.Content, request.IsPrivate), cancellationToken);
        return note is null ? NotFound() : Ok(note);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteNoteCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateNoteRequest(string Content, bool IsPrivate);
