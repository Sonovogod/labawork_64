using MyChat.Models;
using MyChat.Services.ViewModels.Chats;

namespace MyChat.Services.Abstracts;

public interface IChatService
{
    ChatViewModel GetAll();
    bool AddMessage(CreateMessageViewModel model, User currentUser);
}