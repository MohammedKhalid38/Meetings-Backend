using MediatR;
using Meetings.Application.Features.Polls.Commands.CreatePoll;
using Meetings.Application.Features.Polls.Commands.DeletePoll;
using Meetings.Application.Features.Polls.Commands.UpdatePoll;
using Meetings.Application.Features.Polls.Queries.GetPoll;
using Meetings.Application.Features.Polls.Queries.GetPolls;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PollsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? topicId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetPollsQuery(topicId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var poll = await mediator.Send(new GetPollQuery(id), cancellationToken);
        return poll is null ? NotFound() : Ok(poll);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePollCommand command, CancellationToken cancellationToken)
    {
        var poll = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = poll.Id }, poll);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePollRequest request, CancellationToken cancellationToken)
    {
        var poll = await mediator.Send(new UpdatePollCommand(id, request.Question, request.IsMultipleChoice, request.Status), cancellationToken);
        return poll is null ? NotFound() : Ok(poll);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeletePollCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdatePollRequest(string Question, bool IsMultipleChoice, string Status);
