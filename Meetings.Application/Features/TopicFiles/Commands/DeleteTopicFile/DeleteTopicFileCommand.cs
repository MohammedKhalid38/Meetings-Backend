using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.TopicFiles.Commands.DeleteTopicFile;

public record DeleteTopicFileCommand(Guid Id) : IRequest<bool>;

public class DeleteTopicFileCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteTopicFileCommand, bool>
{
    public async Task<bool> Handle(DeleteTopicFileCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.TopicFiles.FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.TopicFiles.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
