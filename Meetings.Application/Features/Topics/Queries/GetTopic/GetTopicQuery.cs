using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Topics.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Topics.Queries.GetTopic;

public record GetTopicQuery(Guid Id) : IRequest<TopicDto?>;

public class GetTopicQueryHandler(IApplicationDbContext db) : IRequestHandler<GetTopicQuery, TopicDto?>
{
    public async Task<TopicDto?> Handle(GetTopicQuery request, CancellationToken cancellationToken)
        => await db.Topics.Where(t => t.Id == request.Id)
            .Select(t => new TopicDto(t.Id, t.MeetingId, t.Title, t.Description, t.OrderIndex, t.Status, t.CreatedBy, t.CreatedAt, t.UpdatedAt))
            .FirstOrDefaultAsync(cancellationToken);
}
