namespace Meetings.Domain.Entities;

public class Note
{
    public Guid Id { get; set; }
    public Guid MeetingId { get; set; }
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Meeting? Meeting { get; set; }
    public User? User { get; set; }
}
