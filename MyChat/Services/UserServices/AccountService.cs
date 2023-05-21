using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyChat.Extensions;
using MyChat.Models;
using MyChat.Services.Abstracts;
using MyChat.Services.ViewModels.Users;

namespace MyChat.Services.UserServices;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _db;
    private readonly ChatContext _dbChat;

    public AccountService(UserManager<User> db, ChatContext dbChat)
    {
        _db = db;
        _dbChat = dbChat;
    }

    public bool UserNameUnique(string userName, string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            bool nameIsExist = _dbChat.Users.Any(x => x.UserName != null && x.Id != id && x.UserName.ToLower().Equals(userName.ToLower()));
            if (nameIsExist)
                return false;
            return true;
        }

        return !_dbChat.Users.Any(x => x.UserName != null && x.UserName.ToLower().Equals(userName.ToLower()));
    }

    public bool UserEmailUnique(string email, string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            bool emailIsExist = _dbChat.Users.Any(x => x.Email != null && x.Id != id && x.Email.ToLower().Equals(email.ToLower()));
            if (emailIsExist)
                return false;
            return true;
        }
        return !_dbChat.Users.Any(x => x.Email != null && x.Email.ToLower().Equals(email.ToLower()));
    }

    public async Task<User?> FindByEmailOrLoginAsync(string? key)
    {
        User? user = new User();
        if (key is null)
            return null;

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        bool isMail = Regex.IsMatch(key, pattern);

        if (isMail)
            user = await _dbChat.Users.Include(x=> x.Messages)
                .FirstOrDefaultAsync(x => x.NormalizedEmail != null && x.NormalizedEmail.Equals(key.ToUpper()));
        else
            user = await _dbChat.Users
                .Include(x=> x.Messages)
                .FirstOrDefaultAsync(x => x.NormalizedUserName != null && x.NormalizedUserName.Equals(key.ToUpper()));
        return user;
    }
    
    public async Task<IdentityResult> Add(UserRegisterViewModel model)
    {
        User user = model.MapToUserModel();
        user.UserName = model.UserName.ToLower();
        user.DateOfCreate = DateTime.Now;
        IdentityResult result = await _db.CreateAsync(user, model.Password);
        return result;
    }

    public async Task<IdentityResult> UpdateInfo(UserEditViewModel model, string userName)
    {
        User user = _db.Users.FirstOrDefault(x => x.UserName.ToLower().Equals(userName.ToLower()));
        user.MapToUpdateUserModel(model);
        IdentityResult result = await _db.UpdateAsync(user);
        return result;
    }
}