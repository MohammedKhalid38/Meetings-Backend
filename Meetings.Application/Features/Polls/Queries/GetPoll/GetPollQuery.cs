using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Polls.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Polls.Queries.GetPoll;

public record GetPollQuery(Guid Id) : IRequest<PollDto?>;

public class GetPollQueryHandler(IApplicationDbContext db) : IRequestHandler<GetPollQuery, PollDto?>
{
    public async Task<PollDto?> Handle(GetPollQuery request, CancellationToken cancellationToken)
        => await db.Polls.Where(p => p.Id == request.Id).Select(p => new PollDto(p.Id, p.TopicId, p.Question, p.IsMultipleChoice, p.Status, p.CreatedBy, p.CreatedAt, p.UpdatedAt)).FirstOrDefaultAsync(cancellationToken);
}
