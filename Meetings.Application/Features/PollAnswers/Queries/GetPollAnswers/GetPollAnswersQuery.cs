using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.PollAnswers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.PollAnswers.Queries.GetPollAnswers;

public record GetPollAnswersQuery(Guid? PollId = null) : IRequest<List<PollAnswerDto>>;

public class GetPollAnswersQueryHandler(IApplicationDbContext db) : IRequestHandler<GetPollAnswersQuery, List<PollAnswerDto>>
{
    public async Task<List<PollAnswerDto>> Handle(GetPollAnswersQuery request, CancellationToken cancellationToken)
    {
        var query = db.PollAnswers.AsQueryable();
        if (request.PollId.HasValue) query = query.Where(a => a.PollId == request.PollId.Value);
        return await query.Select(a => new PollAnswerDto(a.Id, a.PollId, a.AnswerText, a.UserId, a.CreatedAt)).ToListAsync(cancellationToken);
    }
}
