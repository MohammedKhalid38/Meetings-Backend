using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Notes.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Notes.Queries.GetNote;

public record GetNoteQuery(Guid Id) : IRequest<NoteDto?>;

public class GetNoteQueryHandler(IApplicationDbContext db) : IRequestHandler<GetNoteQuery, NoteDto?>
{
    public async Task<NoteDto?> Handle(GetNoteQuery request, CancellationToken cancellationToken)
        => await db.Notes.Where(n => n.Id == request.Id).Select(n => new NoteDto(n.Id, n.MeetingId, n.Content, n.UserId, n.IsPrivate, n.CreatedAt, n.UpdatedAt)).FirstOrDefaultAsync(cancellationToken);
}
