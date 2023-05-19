using MyChat.Models;

namespace MyChat.Services.Abstracts;

public interface IAccountService
{
    public bool UserNameUnique(string userName);
    public bool UserEmailUnique(string email);
    public Task<User?> FindByEmailOrLoginAsync(string key);
    
}