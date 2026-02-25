using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Polls.Commands.DeletePoll;

public record DeletePollCommand(Guid Id) : IRequest<bool>;

public class DeletePollCommandHandler(IApplicationDbContext db) : IRequestHandler<DeletePollCommand, bool>
{
    public async Task<bool> Handle(DeletePollCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.Polls.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.Polls.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
