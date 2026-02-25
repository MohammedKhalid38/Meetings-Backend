using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Attachments.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Attachments.Queries.GetAttachments;

public record GetAttachmentsQuery : IRequest<List<AttachmentDto>>;

public class GetAttachmentsQueryHandler(IApplicationDbContext db) : IRequestHandler<GetAttachmentsQuery, List<AttachmentDto>>
{
    public async Task<List<AttachmentDto>> Handle(GetAttachmentsQuery request, CancellationToken cancellationToken)
        => await db.Attachments
            .Select(a => new AttachmentDto(a.Id, a.FileName, a.FilePath, a.FileSize, a.ContentType, a.UploadedBy, a.CreatedAt))
            .ToListAsync(cancellationToken);
}
