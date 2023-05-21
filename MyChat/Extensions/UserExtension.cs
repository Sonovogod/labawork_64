using instagram.Services.ViewModels.Users;
using MyChat.Models;
using MyChat.Services.ViewModels.Users;

namespace MyChat.Extensions;

public static class UserExtension
{
    public static User MapToUserModel(this UserRegisterViewModel model)
    {
        return new User()
        {
            UserName = model.UserName,
            Email = model.Email,
            Avatar = model.Avatar,
            DateOfBirthday = model.DateOfBirthday
        };
    }
    
    public static UserProfileViewModel MapToUserProfileViewModel(this User model)
    {
        return new UserProfileViewModel()
        {
            UserName = model.UserName,
            Email = model.Email,
            Avatar = model.Avatar,
            DateOfBirthday = model.DateOfBirthday,
            Messages = model.Messages
        };
    }
    public static UserEditViewModel MapToUserEditViewModel(this User model)
    {
        return new UserEditViewModel()
        {
            UserName = model.UserName,
            Email = model.Email,
            Avatar = model.Avatar,
            DateOfBirthday = model.DateOfBirthday,
            Id = model.Id,
            DateOfCreate = model.DateOfCreate
        };
    }
    
    public static void MapToUpdateUserModel(this User changedModel, UserEditViewModel oldUser)
    {
        changedModel.Id = oldUser.Id;
        changedModel.UserName = oldUser.UserName;
        changedModel.Email = oldUser.Email;
        changedModel.Avatar = oldUser.Avatar;
        changedModel.DateOfBirthday = oldUser.DateOfBirthday;
        changedModel.DateOfCreate = oldUser.DateOfCreate;
    }
}