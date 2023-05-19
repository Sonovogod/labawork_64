using MyChat.Models;
using MyChat.Services.ViewModels.Users;

namespace instagram.Services.ViewModels.Users;

public class UserProfileViewModel
{
    public string Avatar { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirthday { get; set; }
    public List<Message> Messages { get; set; } = new List<Message>();
    public UserRegisterViewModel Edit { get; set; } = new UserRegisterViewModel();
}