using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinuteSignatures.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.MeetingMinuteSignatures.Commands.CreateMeetingMinuteSignature;

public record CreateMeetingMinuteSignatureCommand(Guid MeetingMinuteId, Guid SignedBy, string? SignatureData) : IRequest<MeetingMinuteSignatureDto>;

public class CreateMeetingMinuteSignatureCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateMeetingMinuteSignatureCommand, MeetingMinuteSignatureDto>
{
    public async Task<MeetingMinuteSignatureDto> Handle(CreateMeetingMinuteSignatureCommand request, CancellationToken cancellationToken)
    {
        var entity = new MeetingMinuteSignature { Id = Guid.NewGuid(), MeetingMinuteId = request.MeetingMinuteId, SignedBy = request.SignedBy, SignatureData = request.SignatureData, SignedAt = DateTime.UtcNow };
        db.MeetingMinuteSignatures.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return new MeetingMinuteSignatureDto(entity.Id, entity.MeetingMinuteId, entity.SignedBy, entity.SignatureData, entity.SignedAt);
    }
}
