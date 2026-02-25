using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinutes.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinutes.Commands.UpdateMeetingMinute;

public record UpdateMeetingMinuteCommand(Guid Id, string Content, string Status) : IRequest<MeetingMinuteDto?>;

public class UpdateMeetingMinuteCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateMeetingMinuteCommand, MeetingMinuteDto?>
{
    public async Task<MeetingMinuteDto?> Handle(UpdateMeetingMinuteCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.MeetingMinutes.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (entity is null) return null;
        entity.Content = request.Content; entity.Status = request.Status; entity.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return new MeetingMinuteDto(entity.Id, entity.MeetingId, entity.Content, entity.Status, entity.CreatedBy, entity.CreatedAt, entity.UpdatedAt);
    }
}
