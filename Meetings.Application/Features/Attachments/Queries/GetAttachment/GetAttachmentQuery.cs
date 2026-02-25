using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Attachments.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Attachments.Queries.GetAttachment;

public record GetAttachmentQuery(Guid Id) : IRequest<AttachmentDto?>;

public class GetAttachmentQueryHandler(IApplicationDbContext db) : IRequestHandler<GetAttachmentQuery, AttachmentDto?>
{
    public async Task<AttachmentDto?> Handle(GetAttachmentQuery request, CancellationToken cancellationToken)
        => await db.Attachments
            .Where(a => a.Id == request.Id)
            .Select(a => new AttachmentDto(a.Id, a.FileName, a.FilePath, a.FileSize, a.ContentType, a.UploadedBy, a.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
}
