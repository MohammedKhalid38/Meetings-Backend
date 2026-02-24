using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Boards.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Boards.Commands.UpdateBoard;

public record UpdateBoardCommand(Guid Id, string Name, string? Description, bool IsActive) : IRequest<BoardDto?>;

public class UpdateBoardCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateBoardCommand, BoardDto?>
{
    public async Task<BoardDto?> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await db.Boards.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (board is null) return null;

        board.Name = request.Name;
        board.Description = request.Description;
        board.IsActive = request.IsActive;
        board.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync(cancellationToken);

        return new BoardDto(board.Id, board.Name, board.Description, board.IsActive, board.CreatedAt, board.UpdatedAt);
    }
}
