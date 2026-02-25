using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinuteSignatures.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinuteSignatures.Queries.GetMeetingMinuteSignature;

public record GetMeetingMinuteSignatureQuery(Guid Id) : IRequest<MeetingMinuteSignatureDto?>;

public class GetMeetingMinuteSignatureQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMeetingMinuteSignatureQuery, MeetingMinuteSignatureDto?>
{
    public async Task<MeetingMinuteSignatureDto?> Handle(GetMeetingMinuteSignatureQuery request, CancellationToken cancellationToken)
        => await db.MeetingMinuteSignatures.Where(s => s.Id == request.Id).Select(s => new MeetingMinuteSignatureDto(s.Id, s.MeetingMinuteId, s.SignedBy, s.SignatureData, s.SignedAt)).FirstOrDefaultAsync(cancellationToken);
}
