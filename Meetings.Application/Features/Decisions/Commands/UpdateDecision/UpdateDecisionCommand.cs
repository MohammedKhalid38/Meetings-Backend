using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Decisions.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Decisions.Commands.UpdateDecision;

public record UpdateDecisionCommand(Guid Id, string Content, string Status) : IRequest<DecisionDto?>;

public class UpdateDecisionCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateDecisionCommand, DecisionDto?>
{
    public async Task<DecisionDto?> Handle(UpdateDecisionCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.Decisions.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
        if (entity is null) return null;
        entity.Content = request.Content; entity.Status = request.Status; entity.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return new DecisionDto(entity.Id, entity.TopicId, entity.Content, entity.MadeBy, entity.Status, entity.CreatedAt, entity.UpdatedAt);
    }
}
