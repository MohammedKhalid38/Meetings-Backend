using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMembers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMembers.Queries.GetMeetingMember;

public record GetMeetingMemberQuery(Guid Id) : IRequest<MeetingMemberDto?>;

public class GetMeetingMemberQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMeetingMemberQuery, MeetingMemberDto?>
{
    public async Task<MeetingMemberDto?> Handle(GetMeetingMemberQuery request, CancellationToken cancellationToken)
        => await db.MeetingMembers.Where(m => m.Id == request.Id)
            .Select(m => new MeetingMemberDto(m.Id, m.MeetingId, m.UserId, m.Role, m.Status, m.JoinedAt, m.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
}
