using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.BoardMembers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.BoardMembers.Commands.UpdateBoardMember;

public record UpdateBoardMemberCommand(Guid Id, string Role, bool IsActive) : IRequest<BoardMemberDto?>;

public class UpdateBoardMemberCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateBoardMemberCommand, BoardMemberDto?>
{
    public async Task<BoardMemberDto?> Handle(UpdateBoardMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await db.BoardMembers.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (member is null) return null;
        member.Role = request.Role;
        member.IsActive = request.IsActive;
        await db.SaveChangesAsync(cancellationToken);
        return new BoardMemberDto(member.Id, member.BoardId, member.UserId, member.Role, member.JoinedAt, member.IsActive);
    }
}
