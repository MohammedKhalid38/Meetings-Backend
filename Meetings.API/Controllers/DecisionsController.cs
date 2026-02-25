using MediatR;
using Meetings.Application.Features.Decisions.Commands.CreateDecision;
using Meetings.Application.Features.Decisions.Commands.DeleteDecision;
using Meetings.Application.Features.Decisions.Commands.UpdateDecision;
using Meetings.Application.Features.Decisions.Queries.GetDecision;
using Meetings.Application.Features.Decisions.Queries.GetDecisions;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DecisionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? topicId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetDecisionsQuery(topicId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var decision = await mediator.Send(new GetDecisionQuery(id), cancellationToken);
        return decision is null ? NotFound() : Ok(decision);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDecisionCommand command, CancellationToken cancellationToken)
    {
        var decision = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = decision.Id }, decision);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDecisionRequest request, CancellationToken cancellationToken)
    {
        var decision = await mediator.Send(new UpdateDecisionCommand(id, request.Content, request.Status), cancellationToken);
        return decision is null ? NotFound() : Ok(decision);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteDecisionCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateDecisionRequest(string Content, string Status);
