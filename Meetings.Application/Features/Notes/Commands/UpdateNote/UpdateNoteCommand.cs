using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Notes.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Notes.Commands.UpdateNote;

public record UpdateNoteCommand(Guid Id, string Content, bool IsPrivate) : IRequest<NoteDto?>;

public class UpdateNoteCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateNoteCommand, NoteDto?>
{
    public async Task<NoteDto?> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.Notes.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        if (entity is null) return null;
        entity.Content = request.Content; entity.IsPrivate = request.IsPrivate; entity.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return new NoteDto(entity.Id, entity.MeetingId, entity.Content, entity.UserId, entity.IsPrivate, entity.CreatedAt, entity.UpdatedAt);
    }
}
