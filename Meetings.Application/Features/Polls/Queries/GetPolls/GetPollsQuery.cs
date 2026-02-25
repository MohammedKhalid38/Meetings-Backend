using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Polls.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Polls.Queries.GetPolls;

public record GetPollsQuery(Guid? TopicId = null) : IRequest<List<PollDto>>;

public class GetPollsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetPollsQuery, List<PollDto>>
{
    public async Task<List<PollDto>> Handle(GetPollsQuery request, CancellationToken cancellationToken)
    {
        var query = db.Polls.AsQueryable();
        if (request.TopicId.HasValue) query = query.Where(p => p.TopicId == request.TopicId.Value);
        return await query.Select(p => new PollDto(p.Id, p.TopicId, p.Question, p.IsMultipleChoice, p.Status, p.CreatedBy, p.CreatedAt, p.UpdatedAt)).ToListAsync(cancellationToken);
    }
}
