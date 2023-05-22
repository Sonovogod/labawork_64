using MyChat.Services.ViewModels.Users;

namespace MyChat.Services.ChatServices;

public class MessageViewModel
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime DateOfSend { get; set; }
    public string UserId { get; set; }
    public UserViewModel User { get; set; }
}