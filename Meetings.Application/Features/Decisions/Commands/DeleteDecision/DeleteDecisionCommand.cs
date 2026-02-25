using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Decisions.Commands.DeleteDecision;

public record DeleteDecisionCommand(Guid Id) : IRequest<bool>;

public class DeleteDecisionCommandHandler(IApplicationDbContext db) : IRequestHandler<DeleteDecisionCommand, bool>
{
    public async Task<bool> Handle(DeleteDecisionCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.Decisions.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.Decisions.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
