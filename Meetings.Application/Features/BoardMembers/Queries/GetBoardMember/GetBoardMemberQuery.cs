using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.BoardMembers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.BoardMembers.Queries.GetBoardMember;

public record GetBoardMemberQuery(Guid Id) : IRequest<BoardMemberDto?>;

public class GetBoardMemberQueryHandler(IApplicationDbContext db) : IRequestHandler<GetBoardMemberQuery, BoardMemberDto?>
{
    public async Task<BoardMemberDto?> Handle(GetBoardMemberQuery request, CancellationToken cancellationToken)
        => await db.BoardMembers
            .Where(m => m.Id == request.Id)
            .Select(m => new BoardMemberDto(m.Id, m.BoardId, m.UserId, m.Role, m.JoinedAt, m.IsActive))
            .FirstOrDefaultAsync(cancellationToken);
}
