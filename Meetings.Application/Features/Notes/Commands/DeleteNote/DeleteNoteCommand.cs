using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Notes.Commands.DeleteNote;

public record DeleteNoteCommand(Guid Id) : IRequest<bool>;

public class DeleteNoteCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteNoteCommand, bool>
{
    public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.Notes.FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.Notes.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
