using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinutes.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinutes.Queries.GetMeetingMinutes;

public record GetMeetingMinutesQuery(Guid? MeetingId = null) : IRequest<List<MeetingMinuteDto>>;

public class GetMeetingMinutesQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMeetingMinutesQuery, List<MeetingMinuteDto>>
{
    public async Task<List<MeetingMinuteDto>> Handle(GetMeetingMinutesQuery request, CancellationToken cancellationToken)
    {
        var query = db.MeetingMinutes.AsQueryable();
        if (request.MeetingId.HasValue) query = query.Where(m => m.MeetingId == request.MeetingId.Value);
        return await query.Select(m => new MeetingMinuteDto(m.Id, m.MeetingId, m.Content, m.Status, m.CreatedBy, m.CreatedAt, m.UpdatedAt)).ToListAsync(cancellationToken);
    }
}
