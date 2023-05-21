using Microsoft.AspNetCore.Mvc;
using MyChat.Services.Abstracts;

namespace MyChat.Controllers;

public class AccountValidationController
{
    private readonly IAccountService _service;

    public AccountValidationController(IAccountService service)
    {
        _service = service;
    }

    [AcceptVerbs("GET", "POST")]
    public bool CheckUniqueName(string userName, string id)
        => _service.UserNameUnique(userName, id);


    [AcceptVerbs("GET", "POST")]
    public bool CheckUniqueEmail(string email, string id)
        => _service.UserEmailUnique(email, id);

    [AcceptVerbs("GET", "POST")]
    public bool CheckDateOfBirthday(DateOnly dateOfBirthday)
    {
        TimeOnly time = new TimeOnly(00, 00, 00);
        DateTime now = DateTime.Now;
        DateTime eighteenYearsAgo = now.AddYears(-18);
        DateTime overYears = now.AddYears(-110);
        DateTime inputDate = dateOfBirthday.ToDateTime(time);


        if (inputDate > eighteenYearsAgo || inputDate <  overYears)
            return false;

        return true;
    }
}
