using MyChat.Services.ChatServices;

namespace MyChat.Services.ViewModels.Chats;

public class ChatViewModel
{
    public CreateMessageViewModel? CreateMessage { get; set; }
    public List<MessageViewModel>? Messages { get; set; }
}