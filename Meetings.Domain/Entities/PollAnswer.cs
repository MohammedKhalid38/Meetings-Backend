namespace Meetings.Domain.Entities;

public class PollAnswer
{
    public Guid Id { get; set; }
    public Guid PollId { get; set; }
    public string AnswerText { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Poll? Poll { get; set; }
    public User? User { get; set; }
}
