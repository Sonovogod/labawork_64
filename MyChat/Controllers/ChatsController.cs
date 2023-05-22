using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyChat.Extensions;
using MyChat.Services.Abstracts;
using MyChat.Models;
using MyChat.Services.ChatServices;
using MyChat.Services.ViewModels.Chats;

namespace MyChat.Controllers;

public class ChatsController : Controller
{
    private readonly IChatService _chatService;
    private readonly IAccountService _accountService;

    public ChatsController(IChatService chatService, IAccountService accountService)
    {
        _chatService = chatService;
        _accountService = accountService;
    }

    [HttpGet]
    [Authorize]
    public IActionResult CommonChat()
    {
        ChatViewModel model = _chatService.GetAll();
        return View(model);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult CommonChat(CreateMessageViewModel model)
    {
        string userName = User.Identity.Name;
        User currentUser = _accountService.FindByUserName(userName);
        if (ModelState.IsValid)
        {
            bool result = _chatService.AddMessage(model, currentUser);
            if (result)
            {
                MessageViewModel response = model.MapToMessageViewModel(currentUser);
                return Ok(response);
            }
            ModelState.AddModelError("saveMessageError", "Не удалось отправить сообщение");
        }
        ModelState.AddModelError("sendMessageError", "Ошибка отправки сообщения");
        ChatViewModel newModel = _chatService.GetAll();
        return View(newModel);
    }
}