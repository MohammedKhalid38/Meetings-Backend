using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Notes.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.Notes.Commands.CreateNote;

public record CreateNoteCommand(Guid MeetingId, string Content, Guid UserId, bool IsPrivate) : IRequest<NoteDto>;

public class CreateNoteCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateNoteCommand, NoteDto>
{
    public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = new Note { Id = Guid.NewGuid(), MeetingId = request.MeetingId, Content = request.Content, UserId = request.UserId, IsPrivate = request.IsPrivate, CreatedAt = DateTime.UtcNow };
        db.Notes.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return new NoteDto(entity.Id, entity.MeetingId, entity.Content, entity.UserId, entity.IsPrivate, entity.CreatedAt, entity.UpdatedAt);
    }
}
