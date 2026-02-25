using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Topics.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Topics.Commands.UpdateTopic;

public record UpdateTopicCommand(Guid Id, string Title, string? Description, int OrderIndex, string Status) : IRequest<TopicDto?>;

public class UpdateTopicCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateTopicCommand, TopicDto?>
{
    public async Task<TopicDto?> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        var topic = await db.Topics.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        if (topic is null) return null;
        topic.Title = request.Title; topic.Description = request.Description; topic.OrderIndex = request.OrderIndex; topic.Status = request.Status; topic.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return new TopicDto(topic.Id, topic.MeetingId, topic.Title, topic.Description, topic.OrderIndex, topic.Status, topic.CreatedBy, topic.CreatedAt, topic.UpdatedAt);
    }
}
