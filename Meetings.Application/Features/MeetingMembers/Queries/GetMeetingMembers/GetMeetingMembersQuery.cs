using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMembers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMembers.Queries.GetMeetingMembers;

public record GetMeetingMembersQuery(Guid? MeetingId = null) : IRequest<List<MeetingMemberDto>>;

public class GetMeetingMembersQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMeetingMembersQuery, List<MeetingMemberDto>>
{
    public async Task<List<MeetingMemberDto>> Handle(GetMeetingMembersQuery request, CancellationToken cancellationToken)
    {
        var query = db.MeetingMembers.AsQueryable();
        if (request.MeetingId.HasValue) query = query.Where(m => m.MeetingId == request.MeetingId.Value);
        return await query.Select(m => new MeetingMemberDto(m.Id, m.MeetingId, m.UserId, m.Role, m.Status, m.JoinedAt, m.CreatedAt)).ToListAsync(cancellationToken);
    }
}
