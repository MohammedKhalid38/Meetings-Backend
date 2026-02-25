using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Polls.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Polls.Commands.UpdatePoll;

public record UpdatePollCommand(Guid Id, string Question, bool IsMultipleChoice, string Status) : IRequest<PollDto?>;

public class UpdatePollCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdatePollCommand, PollDto?>
{
    public async Task<PollDto?> Handle(UpdatePollCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.Polls.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (entity is null) return null;
        entity.Question = request.Question; entity.IsMultipleChoice = request.IsMultipleChoice; entity.Status = request.Status; entity.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);
        return new PollDto(entity.Id, entity.TopicId, entity.Question, entity.IsMultipleChoice, entity.Status, entity.CreatedBy, entity.CreatedAt, entity.UpdatedAt);
    }
}
