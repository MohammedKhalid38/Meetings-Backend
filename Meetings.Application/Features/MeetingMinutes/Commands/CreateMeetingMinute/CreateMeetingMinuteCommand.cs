using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinutes.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.MeetingMinutes.Commands.CreateMeetingMinute;

public record CreateMeetingMinuteCommand(Guid MeetingId, string Content, Guid CreatedBy) : IRequest<MeetingMinuteDto>;

public class CreateMeetingMinuteCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateMeetingMinuteCommand, MeetingMinuteDto>
{
    public async Task<MeetingMinuteDto> Handle(CreateMeetingMinuteCommand request, CancellationToken cancellationToken)
    {
        var entity = new MeetingMinute { Id = Guid.NewGuid(), MeetingId = request.MeetingId, Content = request.Content, Status = "Draft", CreatedBy = request.CreatedBy, CreatedAt = DateTime.UtcNow };
        db.MeetingMinutes.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return new MeetingMinuteDto(entity.Id, entity.MeetingId, entity.Content, entity.Status, entity.CreatedBy, entity.CreatedAt, entity.UpdatedAt);
    }
}
