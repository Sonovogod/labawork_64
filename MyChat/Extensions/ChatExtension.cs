using MyChat.Models;
using MyChat.Services.ChatServices;
using MyChat.Services.ViewModels.Chats;

namespace MyChat.Extensions;

public static class ChatExtension
{
    public static ChatViewModel MapToChatViewModels(this IEnumerable<Message> model)
    {
        return new ChatViewModel
        {
            CreateMessage = new CreateMessageViewModel(),
            Messages = model.MapToMessageViewModel()
        };
    }

    public static List<MessageViewModel> MapToMessageViewModel(this IEnumerable<Message> model)
    {
        return model.Select(x => new MessageViewModel
        {
            Id = x.Id,
            Content = x.Content,
            DateOfSend = x.DateOfSend,
            User = x.User.MapToUserViewModel(),
            UserId = x.UserId
        }).ToList();
    }
    
    public static Message MapToMessageModel(this CreateMessageViewModel model, User user)
    {
        return new Message
        {
            Content = model.Content,
            DateOfSend = DateTime.Now,
            UserId = user.Id
        };
    }
    
    public static MessageViewModel MapToMessageViewModel(this CreateMessageViewModel model, User user)
    {
        return new MessageViewModel
        {
            Content = model.Content,
            DateOfSend = DateTime.Now,
            UserId = user.Id,
            User = user.MapToUserViewModel()
        };
    }
}