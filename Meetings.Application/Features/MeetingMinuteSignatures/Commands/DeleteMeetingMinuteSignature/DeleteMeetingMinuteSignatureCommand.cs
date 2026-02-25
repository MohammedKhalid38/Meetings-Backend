using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinuteSignatures.Commands.DeleteMeetingMinuteSignature;

public record DeleteMeetingMinuteSignatureCommand(Guid Id) : IRequest<bool>;

public class DeleteMeetingMinuteSignatureCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteMeetingMinuteSignatureCommand, bool>
{
    public async Task<bool> Handle(DeleteMeetingMinuteSignatureCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.MeetingMinuteSignatures.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.MeetingMinuteSignatures.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
