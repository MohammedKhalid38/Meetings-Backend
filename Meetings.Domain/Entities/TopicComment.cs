namespace Meetings.Domain.Entities;

public class TopicComment
{
    public Guid Id { get; set; }
    public Guid TopicId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Topic? Topic { get; set; }
    public User? User { get; set; }
}
