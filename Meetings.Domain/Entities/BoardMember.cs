namespace Meetings.Domain.Entities;

public class BoardMember
{
    public Guid Id { get; set; }
    public Guid BoardId { get; set; }
    public Guid UserId { get; set; }
    public string Role { get; set; } = string.Empty;
    public DateTime JoinedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public Board? Board { get; set; }
    public User? User { get; set; }
}
