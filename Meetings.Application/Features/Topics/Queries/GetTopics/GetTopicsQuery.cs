using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Topics.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Topics.Queries.GetTopics;

public record GetTopicsQuery(Guid? MeetingId = null) : IRequest<List<TopicDto>>;

public class GetTopicsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetTopicsQuery, List<TopicDto>>
{
    public async Task<List<TopicDto>> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
    {
        var query = db.Topics.AsQueryable();
        if (request.MeetingId.HasValue) query = query.Where(t => t.MeetingId == request.MeetingId.Value);
        return await query.OrderBy(t => t.OrderIndex).Select(t => new TopicDto(t.Id, t.MeetingId, t.Title, t.Description, t.OrderIndex, t.Status, t.CreatedBy, t.CreatedAt, t.UpdatedAt)).ToListAsync(cancellationToken);
    }
}
