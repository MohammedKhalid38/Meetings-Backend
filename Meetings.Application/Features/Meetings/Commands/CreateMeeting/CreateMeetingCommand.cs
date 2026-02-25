using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Meetings.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.Meetings.Commands.CreateMeeting;

public record CreateMeetingCommand(Guid BoardId, string Title, string? Description, DateTime ScheduledAt, string? Location, string? MeetingUrl, Guid CreatedBy) : IRequest<MeetingDto>;

public class CreateMeetingCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateMeetingCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = new Meeting { Id = Guid.NewGuid(), BoardId = request.BoardId, Title = request.Title, Description = request.Description, ScheduledAt = request.ScheduledAt, Status = "Scheduled", Location = request.Location, MeetingUrl = request.MeetingUrl, CreatedBy = request.CreatedBy, CreatedAt = DateTime.UtcNow };
        db.Meetings.Add(meeting);
        await db.SaveChangesAsync(cancellationToken);
        return new MeetingDto(meeting.Id, meeting.BoardId, meeting.Title, meeting.Description, meeting.ScheduledAt, meeting.StartedAt, meeting.EndedAt, meeting.Status, meeting.Location, meeting.MeetingUrl, meeting.CreatedBy, meeting.CreatedAt, meeting.UpdatedAt);
    }
}
