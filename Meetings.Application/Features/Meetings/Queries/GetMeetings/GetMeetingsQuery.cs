using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Meetings.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Meetings.Queries.GetMeetings;

public record GetMeetingsQuery(Guid? BoardId = null) : IRequest<List<MeetingDto>>;

public class GetMeetingsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMeetingsQuery, List<MeetingDto>>
{
    public async Task<List<MeetingDto>> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
    {
        var query = db.Meetings.AsQueryable();
        if (request.BoardId.HasValue) query = query.Where(m => m.BoardId == request.BoardId.Value);
        return await query.Select(m => new MeetingDto(m.Id, m.BoardId, m.Title, m.Description, m.ScheduledAt, m.StartedAt, m.EndedAt, m.Status, m.Location, m.MeetingUrl, m.CreatedBy, m.CreatedAt, m.UpdatedAt)).ToListAsync(cancellationToken);
    }
}
