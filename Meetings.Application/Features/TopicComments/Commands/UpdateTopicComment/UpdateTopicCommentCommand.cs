using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.TopicComments.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.TopicComments.Commands.UpdateTopicComment;

public record UpdateTopicCommentCommand(Guid Id, string Content) : IRequest<TopicCommentDto?>;

public class UpdateTopicCommentCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateTopicCommentCommand, TopicCommentDto?>
{
    public async Task<TopicCommentDto?> Handle(UpdateTopicCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.TopicComments.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (entity is null) return null;
        entity.Content = request.Content; entity.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return new TopicCommentDto(entity.Id, entity.TopicId, entity.Content, entity.UserId, entity.CreatedAt, entity.UpdatedAt);
    }
}
