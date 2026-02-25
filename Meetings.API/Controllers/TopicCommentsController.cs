using MediatR;
using Meetings.Application.Features.TopicComments.Commands.CreateTopicComment;
using Meetings.Application.Features.TopicComments.Commands.DeleteTopicComment;
using Meetings.Application.Features.TopicComments.Commands.UpdateTopicComment;
using Meetings.Application.Features.TopicComments.Queries.GetTopicComment;
using Meetings.Application.Features.TopicComments.Queries.GetTopicComments;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicCommentsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? topicId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetTopicCommentsQuery(topicId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var comment = await mediator.Send(new GetTopicCommentQuery(id), cancellationToken);
        return comment is null ? NotFound() : Ok(comment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTopicCommentCommand command, CancellationToken cancellationToken)
    {
        var comment = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTopicCommentRequest request, CancellationToken cancellationToken)
    {
        var comment = await mediator.Send(new UpdateTopicCommentCommand(id, request.Content), cancellationToken);
        return comment is null ? NotFound() : Ok(comment);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteTopicCommentCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateTopicCommentRequest(string Content);
