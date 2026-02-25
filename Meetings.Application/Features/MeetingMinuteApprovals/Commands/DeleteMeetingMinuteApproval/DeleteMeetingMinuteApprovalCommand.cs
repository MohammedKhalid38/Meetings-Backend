using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinuteApprovals.Commands.DeleteMeetingMinuteApproval;

public record DeleteMeetingMinuteApprovalCommand(Guid Id) : IRequest<bool>;

public class DeleteMeetingMinuteApprovalCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteMeetingMinuteApprovalCommand, bool>
{
    public async Task<bool> Handle(DeleteMeetingMinuteApprovalCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.MeetingMinuteApprovals.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.MeetingMinuteApprovals.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
