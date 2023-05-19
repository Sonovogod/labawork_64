using Microsoft.AspNetCore.Identity;
using MyChat.Models;
using MyChat.Services.ViewModels.Users;

namespace MyChat.Services.Abstracts;

public interface IAccountService
{
    public bool UserNameUnique(string userName);
    public bool UserEmailUnique(string email);
    public Task<User?> FindByEmailOrLoginAsync(string key);
    public Task<IdentityResult> Add(UserRegisterViewModel model);
}