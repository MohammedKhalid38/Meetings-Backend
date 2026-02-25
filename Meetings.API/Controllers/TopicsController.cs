using MediatR;
using Meetings.Application.Features.Topics.Commands.CreateTopic;
using Meetings.Application.Features.Topics.Commands.DeleteTopic;
using Meetings.Application.Features.Topics.Commands.UpdateTopic;
using Meetings.Application.Features.Topics.Queries.GetTopic;
using Meetings.Application.Features.Topics.Queries.GetTopics;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? meetingId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetTopicsQuery(meetingId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var topic = await mediator.Send(new GetTopicQuery(id), cancellationToken);
        return topic is null ? NotFound() : Ok(topic);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTopicCommand command, CancellationToken cancellationToken)
    {
        var topic = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = topic.Id }, topic);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTopicRequest request, CancellationToken cancellationToken)
    {
        var topic = await mediator.Send(new UpdateTopicCommand(id, request.Title, request.Description, request.OrderIndex, request.Status), cancellationToken);
        return topic is null ? NotFound() : Ok(topic);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteTopicCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateTopicRequest(string Title, string? Description, int OrderIndex, string Status);
