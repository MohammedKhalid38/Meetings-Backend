using MediatR;
using Meetings.Application.Features.Boards.Commands.CreateBoard;
using Meetings.Application.Features.Boards.Commands.DeleteBoard;
using Meetings.Application.Features.Boards.Commands.UpdateBoard;
using Meetings.Application.Features.Boards.Queries.GetBoard;
using Meetings.Application.Features.Boards.Queries.GetBoards;
using Microsoft.AspNetCore.Mvc;

namespace Meetings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var boards = await mediator.Send(new GetBoardsQuery(), cancellationToken);
        return Ok(boards);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var board = await mediator.Send(new GetBoardQuery(id), cancellationToken);
        return board is null ? NotFound() : Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBoardCommand command, CancellationToken cancellationToken)
    {
        var board = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = board.Id }, board);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBoardRequest request, CancellationToken cancellationToken)
    {
        var board = await mediator.Send(new UpdateBoardCommand(id, request.Name, request.Description, request.IsActive), cancellationToken);
        return board is null ? NotFound() : Ok(board);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await mediator.Send(new DeleteBoardCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}

public record UpdateBoardRequest(string Name, string? Description, bool IsActive);
