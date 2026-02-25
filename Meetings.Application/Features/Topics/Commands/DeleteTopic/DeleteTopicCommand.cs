using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Topics.Commands.DeleteTopic;

public record DeleteTopicCommand(Guid Id) : IRequest<bool>;

public class DeleteTopicCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteTopicCommand, bool>
{
    public async Task<bool> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.Topics.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.Topics.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
