using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Meetings.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Meetings.Commands.UpdateMeeting;

public record UpdateMeetingCommand(Guid Id, string Title, string? Description, DateTime ScheduledAt, DateTime? StartedAt, DateTime? EndedAt, string Status, string? Location, string? MeetingUrl) : IRequest<MeetingDto?>;

public class UpdateMeetingCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateMeetingCommand, MeetingDto?>
{
    public async Task<MeetingDto?> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await db.Meetings.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (meeting is null) return null;
        meeting.Title = request.Title; meeting.Description = request.Description; meeting.ScheduledAt = request.ScheduledAt;
        meeting.StartedAt = request.StartedAt; meeting.EndedAt = request.EndedAt; meeting.Status = request.Status;
        meeting.Location = request.Location; meeting.MeetingUrl = request.MeetingUrl; meeting.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return new MeetingDto(meeting.Id, meeting.BoardId, meeting.Title, meeting.Description, meeting.ScheduledAt, meeting.StartedAt, meeting.EndedAt, meeting.Status, meeting.Location, meeting.MeetingUrl, meeting.CreatedBy, meeting.CreatedAt, meeting.UpdatedAt);
    }
}
