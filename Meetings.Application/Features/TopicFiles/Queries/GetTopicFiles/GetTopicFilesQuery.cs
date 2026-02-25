using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.TopicFiles.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.TopicFiles.Queries.GetTopicFiles;

public record GetTopicFilesQuery(Guid? TopicId = null) : IRequest<List<TopicFileDto>>;

public class GetTopicFilesQueryHandler(IApplicationDbContext db) : IRequestHandler<GetTopicFilesQuery, List<TopicFileDto>>
{
    public async Task<List<TopicFileDto>> Handle(GetTopicFilesQuery request, CancellationToken cancellationToken)
    {
        var query = db.TopicFiles.AsQueryable();
        if (request.TopicId.HasValue) query = query.Where(f => f.TopicId == request.TopicId.Value);
        return await query.Select(f => new TopicFileDto(f.Id, f.TopicId, f.AttachmentId, f.UploadedBy, f.CreatedAt)).ToListAsync(cancellationToken);
    }
}
