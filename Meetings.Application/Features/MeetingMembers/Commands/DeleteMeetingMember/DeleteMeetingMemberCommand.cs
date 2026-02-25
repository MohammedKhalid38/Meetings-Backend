using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMembers.Commands.DeleteMeetingMember;

public record DeleteMeetingMemberCommand(Guid Id) : IRequest<bool>;

public class DeleteMeetingMemberCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteMeetingMemberCommand, bool>
{
    public async Task<bool> Handle(DeleteMeetingMemberCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.MeetingMembers.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.MeetingMembers.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
