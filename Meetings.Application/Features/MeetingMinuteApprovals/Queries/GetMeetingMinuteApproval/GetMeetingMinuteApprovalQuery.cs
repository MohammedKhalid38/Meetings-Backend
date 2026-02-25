using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinuteApprovals.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinuteApprovals.Queries.GetMeetingMinuteApproval;

public record GetMeetingMinuteApprovalQuery(Guid Id) : IRequest<MeetingMinuteApprovalDto?>;

public class GetMeetingMinuteApprovalQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMeetingMinuteApprovalQuery, MeetingMinuteApprovalDto?>
{
    public async Task<MeetingMinuteApprovalDto?> Handle(GetMeetingMinuteApprovalQuery request, CancellationToken cancellationToken)
        => await db.MeetingMinuteApprovals.Where(a => a.Id == request.Id).Select(a => new MeetingMinuteApprovalDto(a.Id, a.MeetingMinuteId, a.ApproverId, a.Status, a.Comments, a.CreatedAt, a.UpdatedAt)).FirstOrDefaultAsync(cancellationToken);
}
