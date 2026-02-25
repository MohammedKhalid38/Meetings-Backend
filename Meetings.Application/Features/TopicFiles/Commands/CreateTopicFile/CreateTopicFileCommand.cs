using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.TopicFiles.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.TopicFiles.Commands.CreateTopicFile;

public record CreateTopicFileCommand(Guid TopicId, Guid AttachmentId, Guid UploadedBy) : IRequest<TopicFileDto>;

public class CreateTopicFileCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateTopicFileCommand, TopicFileDto>
{
    public async Task<TopicFileDto> Handle(CreateTopicFileCommand request, CancellationToken cancellationToken)
    {
        var entity = new TopicFile { Id = Guid.NewGuid(), TopicId = request.TopicId, AttachmentId = request.AttachmentId, UploadedBy = request.UploadedBy, CreatedAt = DateTime.UtcNow };
        db.TopicFiles.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return new TopicFileDto(entity.Id, entity.TopicId, entity.AttachmentId, entity.UploadedBy, entity.CreatedAt);
    }
}
