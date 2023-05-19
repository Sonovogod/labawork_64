using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            return RedirectToAction("", "");
        return View(new UserLoginViewModel {ReturnUrl = returnUrl});
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginViewModel model)
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("", "");
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

                    return RedirectToAction("", new { userName = user.UserName });
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
}