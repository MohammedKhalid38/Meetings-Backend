using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Meetings.Commands.DeleteMeeting;

public record DeleteMeetingCommand(Guid Id) : IRequest<bool>;

public class DeleteMeetingCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteMeetingCommand, bool>
{
    public async Task<bool> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.Meetings.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.Meetings.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
