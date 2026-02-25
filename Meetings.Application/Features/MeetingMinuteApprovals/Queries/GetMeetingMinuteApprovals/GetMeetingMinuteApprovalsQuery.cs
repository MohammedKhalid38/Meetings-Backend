using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinuteApprovals.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinuteApprovals.Queries.GetMeetingMinuteApprovals;

public record GetMeetingMinuteApprovalsQuery(Guid? MeetingMinuteId = null) : IRequest<List<MeetingMinuteApprovalDto>>;

public class GetMeetingMinuteApprovalsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMeetingMinuteApprovalsQuery, List<MeetingMinuteApprovalDto>>
{
    public async Task<List<MeetingMinuteApprovalDto>> Handle(GetMeetingMinuteApprovalsQuery request, CancellationToken cancellationToken)
    {
        var query = db.MeetingMinuteApprovals.AsQueryable();
        if (request.MeetingMinuteId.HasValue) query = query.Where(a => a.MeetingMinuteId == request.MeetingMinuteId.Value);
        return await query.Select(a => new MeetingMinuteApprovalDto(a.Id, a.MeetingMinuteId, a.ApproverId, a.Status, a.Comments, a.CreatedAt, a.UpdatedAt)).ToListAsync(cancellationToken);
    }
}
