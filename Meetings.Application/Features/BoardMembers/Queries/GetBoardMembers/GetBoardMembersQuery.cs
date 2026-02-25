using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.BoardMembers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.BoardMembers.Queries.GetBoardMembers;

public record GetBoardMembersQuery(Guid? BoardId = null) : IRequest<List<BoardMemberDto>>;

public class GetBoardMembersQueryHandler(IApplicationDbContext db) : IRequestHandler<GetBoardMembersQuery, List<BoardMemberDto>>
{
    public async Task<List<BoardMemberDto>> Handle(GetBoardMembersQuery request, CancellationToken cancellationToken)
    {
        var query = db.BoardMembers.Where(m => m.IsActive);
        if (request.BoardId.HasValue) query = query.Where(m => m.BoardId == request.BoardId.Value);
        return await query.Select(m => new BoardMemberDto(m.Id, m.BoardId, m.UserId, m.Role, m.JoinedAt, m.IsActive)).ToListAsync(cancellationToken);
    }
}
