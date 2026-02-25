using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinuteApprovals.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.MeetingMinuteApprovals.Commands.CreateMeetingMinuteApproval;

public record CreateMeetingMinuteApprovalCommand(Guid MeetingMinuteId, Guid ApproverId) : IRequest<MeetingMinuteApprovalDto>;

public class CreateMeetingMinuteApprovalCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateMeetingMinuteApprovalCommand, MeetingMinuteApprovalDto>
{
    public async Task<MeetingMinuteApprovalDto> Handle(CreateMeetingMinuteApprovalCommand request, CancellationToken cancellationToken)
    {
        var entity = new MeetingMinuteApproval { Id = Guid.NewGuid(), MeetingMinuteId = request.MeetingMinuteId, ApproverId = request.ApproverId, Status = "Pending", CreatedAt = DateTime.UtcNow };
        db.MeetingMinuteApprovals.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return new MeetingMinuteApprovalDto(entity.Id, entity.MeetingMinuteId, entity.ApproverId, entity.Status, entity.Comments, entity.CreatedAt, entity.UpdatedAt);
    }
}
