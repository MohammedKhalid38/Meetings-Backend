namespace Meetings.Domain.Entities;

public class Decision
{
    public Guid Id { get; set; }
    public Guid TopicId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid MadeBy { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Topic? Topic { get; set; }
}
