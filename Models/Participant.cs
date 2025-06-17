namespace AICalendar.Models;

public class Participant
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int EventId { get; set; }
    public User User { get; set; } = new User();
    public Event Event { get; set; } = new Event();
}