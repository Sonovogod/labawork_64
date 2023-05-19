using Microsoft.AspNetCore.Identity;

namespace MyChat.Models;

public class User: IdentityUser
{
    public DateOnly DateOfBirthday { get; set; }
    public string Avatar { get; set; }
    public List<Message> Messages { get; set; }
}