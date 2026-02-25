namespace Meetings.Domain.Entities;

public class Poll
{
    public Guid Id { get; set; }
    public Guid TopicId { get; set; }
    public string Question { get; set; } = string.Empty;
    public bool IsMultipleChoice { get; set; }
    public string Status { get; set; } = "Active";
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Topic? Topic { get; set; }
}
