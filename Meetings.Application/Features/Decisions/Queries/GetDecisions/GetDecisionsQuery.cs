using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Decisions.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Decisions.Queries.GetDecisions;

public record GetDecisionsQuery(Guid? TopicId = null) : IRequest<List<DecisionDto>>;

public class GetDecisionsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetDecisionsQuery, List<DecisionDto>>
{
    public async Task<List<DecisionDto>> Handle(GetDecisionsQuery request, CancellationToken cancellationToken)
    {
        var query = db.Decisions.AsQueryable();
        if (request.TopicId.HasValue) query = query.Where(d => d.TopicId == request.TopicId.Value);
        return await query.Select(d => new DecisionDto(d.Id, d.TopicId, d.Content, d.MadeBy, d.Status, d.CreatedAt, d.UpdatedAt)).ToListAsync(cancellationToken);
    }
}
