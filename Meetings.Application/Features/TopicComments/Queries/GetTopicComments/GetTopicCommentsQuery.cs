using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.TopicComments.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.TopicComments.Queries.GetTopicComments;

public record GetTopicCommentsQuery(Guid? TopicId = null) : IRequest<List<TopicCommentDto>>;

public class GetTopicCommentsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetTopicCommentsQuery, List<TopicCommentDto>>
{
    public async Task<List<TopicCommentDto>> Handle(GetTopicCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = db.TopicComments.AsQueryable();
        if (request.TopicId.HasValue) query = query.Where(c => c.TopicId == request.TopicId.Value);
        return await query.OrderBy(c => c.CreatedAt).Select(c => new TopicCommentDto(c.Id, c.TopicId, c.Content, c.UserId, c.CreatedAt, c.UpdatedAt)).ToListAsync(cancellationToken);
    }
}
