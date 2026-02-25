namespace Meetings.Domain.Entities;

public class MeetingMinute
{
    public Guid Id { get; set; }
    public Guid MeetingId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Status { get; set; } = "Draft";
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Meeting? Meeting { get; set; }
}
