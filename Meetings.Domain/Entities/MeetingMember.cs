namespace Meetings.Domain.Entities;

public class MeetingMember
{
    public Guid Id { get; set; }
    public Guid MeetingId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = "Participant";
    public string Status { get; set; } = "Invited";
    public DateTime? JoinedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public Meeting? Meeting { get; set; }
    public User? User { get; set; }
}
