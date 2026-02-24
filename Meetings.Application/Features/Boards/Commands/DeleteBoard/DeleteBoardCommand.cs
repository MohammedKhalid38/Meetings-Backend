using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Boards.Commands.DeleteBoard;

public record DeleteBoardCommand(Guid Id) : IRequest<bool>;

public class DeleteBoardCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteBoardCommand, bool>
{
    public async Task<bool> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await db.Boards.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (board is null) return false;

        db.Boards.Remove(board);
        await db.SaveChangesAsync(cancellationToken);

        return true;
    }
}
