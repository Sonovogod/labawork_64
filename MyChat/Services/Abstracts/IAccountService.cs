using Microsoft.AspNetCore.Identity;
using MyChat.Models;
using MyChat.Services.ViewModels.Users;

namespace MyChat.Services.Abstracts;

public interface IAccountService
{
    public bool UserNameUnique(string userName, string id);
    public bool UserEmailUnique(string email, string id);
    public Task<User?> FindByEmailOrLoginAsync(string key);
    public Task<IdentityResult> Add(UserRegisterViewModel model);
    public Task<IdentityResult> UpdateInfo(UserEditViewModel model, string userName);
} 