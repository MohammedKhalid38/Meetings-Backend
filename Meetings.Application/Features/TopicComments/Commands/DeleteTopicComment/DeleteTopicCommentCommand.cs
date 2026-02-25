using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.TopicComments.Commands.DeleteTopicComment;

public record DeleteTopicCommentCommand(Guid Id) : IRequest<bool>;

public class DeleteTopicCommentCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteTopicCommentCommand, bool>
{
    public async Task<bool> Handle(DeleteTopicCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.TopicComments.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.TopicComments.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
