using System.ComponentModel.DataAnnotations;

namespace MyChat.Services.ViewModels.Chats;

public class CreateMessageViewModel
{
    [Display(Name = "Комментарий")]
    [StringLength(300, MinimumLength = 1, ErrorMessage = "Минимальное количество знаков: 1, Максимальное - 300")]
    [Required(ErrorMessage = "Поле не можен быть пустым")]
    
    public string Content { get; set; }
}