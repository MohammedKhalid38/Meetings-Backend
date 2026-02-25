namespace Meetings.Application.Features.Attachments.DTOs;

public record AttachmentDto(
    Guid Id,
    string FileName,
    string FilePath,
    long FileSize,
    string ContentType,
    Guid UploadedBy,
    DateTime CreatedAt
);
