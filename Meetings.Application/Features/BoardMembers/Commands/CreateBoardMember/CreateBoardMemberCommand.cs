using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.BoardMembers.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.BoardMembers.Commands.CreateBoardMember;

public record CreateBoardMemberCommand(Guid BoardId, Guid UserId, string Role) : IRequest<BoardMemberDto>;

public class CreateBoardMemberCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateBoardMemberCommand, BoardMemberDto>
{
    public async Task<BoardMemberDto> Handle(CreateBoardMemberCommand request, CancellationToken cancellationToken)
    {
        var member = new BoardMember { Id = Guid.NewGuid(), BoardId = request.BoardId, UserId = request.UserId, Role = request.Role, JoinedAt = DateTime.UtcNow, IsActive = true };
        db.BoardMembers.Add(member);
        await db.SaveChangesAsync(cancellationToken);
        return new BoardMemberDto(member.Id, member.BoardId, member.UserId, member.Role, member.JoinedAt, member.IsActive);
    }
}
