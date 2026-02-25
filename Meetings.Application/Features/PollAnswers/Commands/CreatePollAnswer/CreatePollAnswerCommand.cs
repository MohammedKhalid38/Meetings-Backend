using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.PollAnswers.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.PollAnswers.Commands.CreatePollAnswer;

public record CreatePollAnswerCommand(Guid PollId, string AnswerText, Guid UserId) : IRequest<PollAnswerDto>;

public class CreatePollAnswerCommandHandler(IApplicationDbContext db) : IRequestHandler<CreatePollAnswerCommand, PollAnswerDto>
{
    public async Task<PollAnswerDto> Handle(CreatePollAnswerCommand request, CancellationToken cancellationToken)
    {
        var entity = new PollAnswer { Id = Guid.NewGuid(), PollId = request.PollId, AnswerText = request.AnswerText, UserId = request.UserId, CreatedAt = DateTime.UtcNow };
        db.PollAnswers.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return new PollAnswerDto(entity.Id, entity.PollId, entity.AnswerText, entity.UserId, entity.CreatedAt);
    }
}
