using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyChat.Services.ViewModels.Users;

public class UserEditViewModel
{
    public string Id { get; set; }
    
    [Display(Name = "Логин")]
    [StringLength(30, MinimumLength = 1, ErrorMessage = "Минимальное количество знаков: 1, Максимальное - 30")]
    [Required(ErrorMessage = "Поле не можен быть пустым")]
    [RegularExpression(@"^[^@А-Яа-я]*$", ErrorMessage = "Не допускается использование кириллицы и знака '@' в логине")]
    [Remote("CheckUniqueName", "AccountValidation", ErrorMessage = "Такое имя уже есть", AdditionalFields = "UserName, Id")]
    public string UserName { get; set; }
    
    [Display(Name = "Эл. почта")]
    [Required(ErrorMessage = "Поле не можен быть пустым")]
    [EmailAddress (ErrorMessage = "Некорректный адрес")]
    [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Неверный формат ввода")]
    [Remote("CheckUniqueEmail", "AccountValidation", ErrorMessage = "Такая почта уже зарегистрирована", AdditionalFields = "Email, Id")]
    public string Email { get; set; }
    
    [Display(Name = "Дата рождения")]
    [Required(ErrorMessage = "Поле не может быть пустым")]
    [Remote("CheckDateOfBirthday", "AccountValidation", ErrorMessage = "Неверная дата ,введи дату рождения, вход строго 18+", AdditionalFields = "DateOfBirthday")]
    public DateOnly DateOfBirthday { get; set; }
    
    [Display(Name = "Фото профиля")]
    public string? Avatar { get; set; }

    public DateTime DateOfCreate { get; set; }
}