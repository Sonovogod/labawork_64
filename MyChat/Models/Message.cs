namespace MyChat.Models;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime DateOfSend { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}