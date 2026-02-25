namespace Meetings.Domain.Entities;

public class MeetingMinuteSignature
{
    public Guid Id { get; set; }
    public Guid MeetingMinuteId { get; set; }
    public Guid SignedBy { get; set; }
    public string? SignatureData { get; set; }
    public DateTime SignedAt { get; set; }

    public MeetingMinute? MeetingMinute { get; set; }
    public User? Signer { get; set; }
}
