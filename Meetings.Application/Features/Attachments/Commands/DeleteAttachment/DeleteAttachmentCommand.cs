using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Attachments.Commands.DeleteAttachment;

public record DeleteAttachmentCommand(Guid Id) : IRequest<bool>;

public class DeleteAttachmentCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteAttachmentCommand, bool>
{
    public async Task<bool> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.Attachments.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.Attachments.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
