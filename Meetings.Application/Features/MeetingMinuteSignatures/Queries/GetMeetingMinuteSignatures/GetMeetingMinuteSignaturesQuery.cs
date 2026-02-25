using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMinuteSignatures.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMinuteSignatures.Queries.GetMeetingMinuteSignatures;

public record GetMeetingMinuteSignaturesQuery(Guid? MeetingMinuteId = null) : IRequest<List<MeetingMinuteSignatureDto>>;

public class GetMeetingMinuteSignaturesQueryHandler(IApplicationDbContext db) : IRequestHandler<GetMeetingMinuteSignaturesQuery, List<MeetingMinuteSignatureDto>>
{
    public async Task<List<MeetingMinuteSignatureDto>> Handle(GetMeetingMinuteSignaturesQuery request, CancellationToken cancellationToken)
    {
        var query = db.MeetingMinuteSignatures.AsQueryable();
        if (request.MeetingMinuteId.HasValue) query = query.Where(s => s.MeetingMinuteId == request.MeetingMinuteId.Value);
        return await query.Select(s => new MeetingMinuteSignatureDto(s.Id, s.MeetingMinuteId, s.SignedBy, s.SignatureData, s.SignedAt)).ToListAsync(cancellationToken);
    }
}
