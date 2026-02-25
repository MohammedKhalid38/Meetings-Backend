using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.BoardMembers.Commands.DeleteBoardMember;

public record DeleteBoardMemberCommand(Guid Id) : IRequest<bool>;

public class DeleteBoardMemberCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteBoardMemberCommand, bool>
{
    public async Task<bool> Handle(DeleteBoardMemberCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.BoardMembers.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.BoardMembers.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
