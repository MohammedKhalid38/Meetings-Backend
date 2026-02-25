using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.PollAnswers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.PollAnswers.Queries.GetPollAnswer;

public record GetPollAnswerQuery(Guid Id) : IRequest<PollAnswerDto?>;

public class GetPollAnswerQueryHandler(IApplicationDbContext db) : IRequestHandler<GetPollAnswerQuery, PollAnswerDto?>
{
    public async Task<PollAnswerDto?> Handle(GetPollAnswerQuery request, CancellationToken cancellationToken)
        => await db.PollAnswers.Where(a => a.Id == request.Id).Select(a => new PollAnswerDto(a.Id, a.PollId, a.AnswerText, a.UserId, a.CreatedAt)).FirstOrDefaultAsync(cancellationToken);
}
