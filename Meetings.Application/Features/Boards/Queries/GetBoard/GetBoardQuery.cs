using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Boards.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Boards.Queries.GetBoard;

public record GetBoardQuery(Guid Id) : IRequest<BoardDto?>;

public class GetBoardQueryHandler(IApplicationDbContext db) : IRequestHandler<GetBoardQuery, BoardDto?>
{
    public async Task<BoardDto?> Handle(GetBoardQuery request, CancellationToken cancellationToken)
    {
        return await db.Boards
            .Where(b => b.Id == request.Id)
            .Select(b => new BoardDto(b.Id, b.Name, b.Description, b.IsActive, b.CreatedAt, b.UpdatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}