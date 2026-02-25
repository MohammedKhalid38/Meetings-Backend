using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinuteApprovals.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinuteApprovals.Commands.UpdateMeetingMinuteApproval;

public record UpdateMeetingMinuteApprovalCommand(Guid Id, string Status, string? Comments) : IRequest<MeetingMinuteApprovalDto?>;

public class UpdateMeetingMinuteApprovalCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateMeetingMinuteApprovalCommand, MeetingMinuteApprovalDto?>
{
    public async Task<MeetingMinuteApprovalDto?> Handle(UpdateMeetingMinuteApprovalCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.MeetingMinuteApprovals.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (entity is null) return null;
        entity.Status = request.Status; entity.Comments = request.Comments; entity.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return new MeetingMinuteApprovalDto(entity.Id, entity.MeetingMinuteId, entity.ApproverId, entity.Status, entity.Comments, entity.CreatedAt, entity.UpdatedAt);
    }
}
