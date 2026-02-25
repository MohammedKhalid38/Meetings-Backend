using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Polls.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.Polls.Commands.CreatePoll;

public record CreatePollCommand(Guid TopicId, string Question, bool IsMultipleChoice, Guid CreatedBy) : IRequest<PollDto>;

public class CreatePollCommandHandler(IApplicationDbContext db) : IRequestHandler<CreatePollCommand, PollDto>
{
    public async Task<PollDto> Handle(CreatePollCommand request, CancellationToken cancellationToken)
    {
        var entity = new Poll { Id = Guid.NewGuid(), TopicId = request.TopicId, Question = request.Question, IsMultipleChoice = request.IsMultipleChoice, Status = "Active", CreatedBy = request.CreatedBy, CreatedAt = DateTime.UtcNow };
        db.Polls.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return new PollDto(entity.Id, entity.TopicId, entity.Question, entity.IsMultipleChoice, entity.Status, entity.CreatedBy, entity.CreatedAt, entity.UpdatedAt);
    }
}
