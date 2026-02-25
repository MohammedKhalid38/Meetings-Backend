using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Decisions.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Decisions.Queries.GetDecision;

public record GetDecisionQuery(Guid Id) : IRequest<DecisionDto?>;

public class GetDecisionQueryHandler(IApplicationDbContext db) : IRequestHandler<GetDecisionQuery, DecisionDto?>
{
    public async Task<DecisionDto?> Handle(GetDecisionQuery request, CancellationToken cancellationToken)
        => await db.Decisions.Where(d => d.Id == request.Id).Select(d => new DecisionDto(d.Id, d.TopicId, d.Content, d.MadeBy, d.Status, d.CreatedAt, d.UpdatedAt)).FirstOrDefaultAsync(cancellationToken);
}
