using MediatR;
using Meetings.Application.Features.TopicFiles.Commands.CreateTopicFile;
using Meetings.Application.Features.TopicFiles.Commands.DeleteTopicFile;
using Meetings.Application.Features.TopicFiles.Queries.GetTopicFile;
using Meetings.Application.Features.TopicFiles.Queries.GetTopicFiles;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicFilesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? topicId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetTopicFilesQuery(topicId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var file = await mediator.Send(new GetTopicFileQuery(id), cancellationToken);
        return file is null ? NotFound() : Ok(file);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTopicFileCommand command, CancellationToken cancellationToken)
    {
        var file = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = file.Id }, file);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteTopicFileCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
