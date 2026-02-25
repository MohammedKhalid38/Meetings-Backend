using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Decisions.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.Decisions.Commands.CreateDecision;

public record CreateDecisionCommand(Guid TopicId, string Content, Guid MadeBy) : IRequest<DecisionDto>;

public class CreateDecisionCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateDecisionCommand, DecisionDto>
{
    public async Task<DecisionDto> Handle(CreateDecisionCommand request, CancellationToken cancellationToken)
    {
        var entity = new Decision { Id = Guid.NewGuid(), TopicId = request.TopicId, Content = request.Content, MadeBy = request.MadeBy, Status = "Pending", CreatedAt = DateTime.UtcNow };
        db.Decisions.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return new DecisionDto(entity.Id, entity.TopicId, entity.Content, entity.MadeBy, entity.Status, entity.CreatedAt, entity.UpdatedAt);
    }
}
