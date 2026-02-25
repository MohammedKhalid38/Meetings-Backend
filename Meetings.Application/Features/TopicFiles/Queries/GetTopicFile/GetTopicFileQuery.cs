using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.TopicFiles.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.TopicFiles.Queries.GetTopicFile;

public record GetTopicFileQuery(Guid Id) : IRequest<TopicFileDto?>;

public class GetTopicFileQueryHandler(IApplicationDbContext db) : IRequestHandler<GetTopicFileQuery, TopicFileDto?>
{
    public async Task<TopicFileDto?> Handle(GetTopicFileQuery request, CancellationToken cancellationToken)
        => await db.TopicFiles.Where(f => f.Id == request.Id).Select(f => new TopicFileDto(f.Id, f.TopicId, f.AttachmentId, f.UploadedBy, f.CreatedAt)).FirstOrDefaultAsync(cancellationToken);
}
