using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinutes.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinutes.Queries.GetMeetingMinute;

public record GetMeetingMinuteQuery(Guid Id) : IRequest<MeetingMinuteDto?>;

public class GetMeetingMinuteQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMeetingMinuteQuery, MeetingMinuteDto?>
{
    public async Task<MeetingMinuteDto?> Handle(GetMeetingMinuteQuery request, CancellationToken cancellationToken)
        => await db.MeetingMinutes.Where(m => m.Id == request.Id).Select(m => new MeetingMinuteDto(m.Id, m.MeetingId, m.Content, m.Status, m.CreatedBy, m.CreatedAt, m.UpdatedAt)).FirstOrDefaultAsync(cancellationToken);
}
