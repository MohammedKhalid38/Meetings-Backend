namespace Meetings.Domain.Entities;

public class TopicFile
{
    public Guid Id { get; set; }
    public Guid TopicId { get; set; }
    public Guid AttachmentId { get; set; }
    public Guid UploadedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public Topic? Topic { get; set; }
    public Attachment? Attachment { get; set; }
}
