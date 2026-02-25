namespace Meetings.Application.Features.TopicFiles.DTOs;

public record TopicFileDto(
    Guid Id,
    Guid TopicId,
    Guid AttachmentId,
    Guid UploadedBy,
    DateTime CreatedAt
);
