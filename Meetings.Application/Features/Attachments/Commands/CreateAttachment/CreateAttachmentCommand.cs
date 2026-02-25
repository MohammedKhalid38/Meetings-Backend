using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Attachments.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.Attachments.Commands.CreateAttachment;

public record CreateAttachmentCommand(string FileName, string FilePath, long FileSize, string ContentType, Guid UploadedBy) : IRequest<AttachmentDto>;

public class CreateAttachmentCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateAttachmentCommand, AttachmentDto>
{
    public async Task<AttachmentDto> Handle(CreateAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = new Attachment
        {
            Id = Guid.NewGuid(),
            FileName = request.FileName,
            FilePath = request.FilePath,
            FileSize = request.FileSize,
            ContentType = request.ContentType,
            UploadedBy = request.UploadedBy,
            CreatedAt = DateTime.UtcNow
        };
        db.Attachments.Add(attachment);
        await db.SaveChangesAsync(cancellationToken);
        return new AttachmentDto(attachment.Id, attachment.FileName, attachment.FilePath, attachment.FileSize, attachment.ContentType, attachment.UploadedBy, attachment.CreatedAt);
    }
}
