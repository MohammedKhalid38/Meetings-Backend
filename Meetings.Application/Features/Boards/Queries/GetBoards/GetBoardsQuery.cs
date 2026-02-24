using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Boards.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Boards.Queries.GetBoards;

public record GetBoardsQuery : IRequest<List<BoardDto>>;

public class GetBoardsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetBoardsQuery, List<BoardDto>>
{
    public async Task<List<BoardDto>> Handle(GetBoardsQuery request, CancellationToken cancellationToken)
    {
        return await db.Boards
            .Where(b => b.IsActive)
            .Select(b => new BoardDto(b.Id, b.Name, b.Description, b.IsActive, b.CreatedAt, b.UpdatedAt))
            .ToListAsync(cancellationToken);
    }
}
