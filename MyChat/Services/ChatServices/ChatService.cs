using Microsoft.EntityFrameworkCore;
using MyChat.Extensions;
using MyChat.Models;
using MyChat.Services.Abstracts;
using MyChat.Services.ViewModels.Chats;

namespace MyChat.Services.ChatServices;

public class ChatService : IChatService
{
    private readonly ChatContext _db;

    public ChatService(ChatContext db)
    {
        _db = db;
    }

    public ChatViewModel GetAll()
    {
        var messages = _db.Messages
            .Include(x => x.User)
            .ToList();
        return messages.MapToChatViewModels();
    }

    public bool AddMessage(CreateMessageViewModel model, User currentUser)
    {
        try
        {
            _db.Messages.Add(model.MapToMessageModel(currentUser));
            _db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}