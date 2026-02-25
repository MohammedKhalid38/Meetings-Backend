namespace Meetings.Domain.Entities;

public class MeetingMinuteApproval
{
    public Guid Id { get; set; }
    public Guid MeetingMinuteId { get; set; }
    public Guid ApproverId { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public MeetingMinute? MeetingMinute { get; set; }
    public User? Approver { get; set; }
}
