using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Meetings.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Meetings.Queries.GetMeeting;

public record GetMeetingQuery(Guid Id) : IRequest<MeetingDto?>;

public class GetMeetingQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMeetingQuery, MeetingDto?>
{
    public async Task<MeetingDto?> Handle(GetMeetingQuery request, CancellationToken cancellationToken)
        => await db.Meetings.Where(m => m.Id == request.Id)
            .Select(m => new MeetingDto(m.Id, m.BoardId, m.Title, m.Description, m.ScheduledAt, m.StartedAt, m.EndedAt, m.Status, m.Location, m.MeetingUrl, m.CreatedBy, m.CreatedAt, m.UpdatedAt))
            .FirstOrDefaultAsync(cancellationToken);
}
