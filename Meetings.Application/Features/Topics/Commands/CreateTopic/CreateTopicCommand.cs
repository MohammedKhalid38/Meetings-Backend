using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Topics.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.Topics.Commands.CreateTopic;

public record CreateTopicCommand(Guid MeetingId, string Title, string? Description, int OrderIndex, Guid CreatedBy) : IRequest<TopicDto>;

public class CreateTopicCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateTopicCommand, TopicDto>
{
    public async Task<TopicDto> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        var topic = new Topic { Id = Guid.NewGuid(), MeetingId = request.MeetingId, Title = request.Title, Description = request.Description, OrderIndex = request.OrderIndex, Status = "Open", CreatedBy = request.CreatedBy, CreatedAt = DateTime.UtcNow };
        db.Topics.Add(topic);
        await db.SaveChangesAsync(cancellationToken);
        return new TopicDto(topic.Id, topic.MeetingId, topic.Title, topic.Description, topic.OrderIndex, topic.Status, topic.CreatedBy, topic.CreatedAt, topic.UpdatedAt);
    }
}
