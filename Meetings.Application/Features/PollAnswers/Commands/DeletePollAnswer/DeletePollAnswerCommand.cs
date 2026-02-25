using MediatR;
using Meetings.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.PollAnswers.Commands.DeletePollAnswer;

public record DeletePollAnswerCommand(Guid Id) : IRequest<bool>;

public class DeletePollAnswerCommandHandler(IApplicationDbContext db) : IRequestHandler<DeletePollAnswerCommand, bool>
{
    public async Task<bool> Handle(DeletePollAnswerCommand request, CancellationToken cancellationToken)
    {
        var entity = await db.PollAnswers.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (entity is null) return false;
        db.PollAnswers.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
