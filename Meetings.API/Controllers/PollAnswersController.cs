using MediatR;
using Meetings.Application.Features.PollAnswers.Commands.CreatePollAnswer;
using Meetings.Application.Features.PollAnswers.Commands.DeletePollAnswer;
using Meetings.Application.Features.PollAnswers.Queries.GetPollAnswer;
using Meetings.Application.Features.PollAnswers.Queries.GetPollAnswers;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PollAnswersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? pollId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetPollAnswersQuery(pollId), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var answer = await mediator.Send(new GetPollAnswerQuery(id), cancellationToken);
        return answer is null ? NotFound() : Ok(answer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePollAnswerCommand command, CancellationToken cancellationToken)
    {
        var answer = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = answer.Id }, answer);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeletePollAnswerCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
