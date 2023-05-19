using instagram.Services.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyChat.Enums.File;
using MyChat.Extensions;
using MyChat.Models;
using MyChat.Services.Abstracts;
using MyChat.Services.ViewModels.Users;

namespace MyChat.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IFileService _fileService;
    private readonly SignInManager<User> _signInManager;

    public AccountController(SignInManager<User> signInManager, IAccountService accountService, IFileService fileService)
    {
        _signInManager = signInManager;
        _accountService = accountService;
        _fileService = fileService;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl)
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Profile", new { userName = User.Identity.Name });
        return View(new UserLoginViewModel {ReturnUrl = returnUrl});
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginViewModel model)
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Profile", "Account");
        if (ModelState.IsValid)
        {
            User? user = await _accountService.FindByEmailOrLoginAsync(model.EmailOrLogin);
            if (user is not null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Profile", new { userName = User.Identity.Name });
                }

            }

            ModelState.AddModelError("incorrectAuthentication", "Некорректный логин и/или пароль");
        }

        return View(model);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        UserRegisterViewModel model = new UserRegisterViewModel();
        return View(model);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserRegisterViewModel model, IFormFile? uploadedFile)
    {
        if (ModelState.IsValid)
        {
            if (uploadedFile is null)
            {
                string filePath = _fileService.GetPrimalImgPath();
                model.Avatar = filePath;
            }
            
            bool fileValid = _fileService.FileValid(uploadedFile, ImageType.Logo);
            if (fileValid && uploadedFile.Length != 0)
            {
                string filePath = _fileService.SaveImage(uploadedFile, ImageType.Logo);
                model.Avatar = filePath;
            }
            ModelState.AddModelError("incorrectLogo", "Ошибка загрузки, фото не соответсвует требованиям");
            
            var result = await _accountService.Add(model);
            if (result.Succeeded)
            {
                User? user = await _accountService.FindByEmailOrLoginAsync(model.Email);
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction("Profile", "Account", new {userName = user.UserName});
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            ModelState.AddModelError("CreateUserError", "Ошибка при создании пользователя");
        }
        ModelState.AddModelError("incorrectRegistration", "Ошибка регистрации!");

        return View(model);
    }
    
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile(string userName)
    {
        User? user = await _accountService.FindByEmailOrLoginAsync(userName);
        var totalUserId = _accountService.FindByEmailOrLoginAsync(User.Identity.Name).Result.Id;
        if (user is not null)
        {
            UserProfileViewModel userView = user.MapToUserProfileViewModel();
            ViewData.Add("totalUser", totalUserId);
            
            return View(userView);
        }
        
        return NotFound();
    }

    public IActionResult EditProfile()
    {
        throw new NotImplementedException();
    }
}
