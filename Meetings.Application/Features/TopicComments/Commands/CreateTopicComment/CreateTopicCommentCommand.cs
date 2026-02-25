using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.TopicComments.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.TopicComments.Commands.CreateTopicComment;

public record CreateTopicCommentCommand(Guid TopicId, string Content, Guid UserId) : IRequest<TopicCommentDto>;

public class CreateTopicCommentCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateTopicCommentCommand, TopicCommentDto>
{
    public async Task<TopicCommentDto> Handle(CreateTopicCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = new TopicComment { Id = Guid.NewGuid(), TopicId = request.TopicId, Content = request.Content, UserId = request.UserId, CreatedAt = DateTime.UtcNow };
        db.TopicComments.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return new TopicCommentDto(entity.Id, entity.TopicId, entity.Content, entity.UserId, entity.CreatedAt, entity.UpdatedAt);
    }
}
