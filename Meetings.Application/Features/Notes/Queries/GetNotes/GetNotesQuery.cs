using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Notes.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Notes.Queries.GetNotes;

public record GetNotesQuery(Guid? MeetingId = null) : IRequest<List<NoteDto>>;

public class GetNotesQueryHandler(IApplicationDbContext db) : IRequestHandler<GetNotesQuery, List<NoteDto>>
{
    public async Task<List<NoteDto>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        var query = db.Notes.AsQueryable();
        if (request.MeetingId.HasValue) query = query.Where(n => n.MeetingId == request.MeetingId.Value);
        return await query.Select(n => new NoteDto(n.Id, n.MeetingId, n.Content, n.UserId, n.IsPrivate, n.CreatedAt, n.UpdatedAt)).ToListAsync(cancellationToken);
    }
}
