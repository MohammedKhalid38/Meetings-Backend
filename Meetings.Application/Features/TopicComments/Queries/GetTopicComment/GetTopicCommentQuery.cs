using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.TopicComments.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.TopicComments.Queries.GetTopicComment;

public record GetTopicCommentQuery(Guid Id) : IRequest<TopicCommentDto?>;

public class GetTopicCommentQueryHandler(IApplicationDbContext db) : IRequestHandler<GetTopicCommentQuery, TopicCommentDto?>
{
    public async Task<TopicCommentDto?> Handle(GetTopicCommentQuery request, CancellationToken cancellationToken)
        => await db.TopicComments.Where(c => c.Id == request.Id).Select(c => new TopicCommentDto(c.Id, c.TopicId, c.Content, c.UserId, c.CreatedAt, c.UpdatedAt)).FirstOrDefaultAsync(cancellationToken);
}
