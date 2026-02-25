using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinutes.Commands.DeleteMeetingMinute;

public record DeleteMeetingMinuteCommand(Guid Id) : IRequest<bool>;

public class DeleteMeetingMinuteCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteMeetingMinuteCommand, bool>
{
    public async Task<bool> Handle(DeleteMeetingMinuteCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.MeetingMinutes.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.MeetingMinutes.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
