using Microsoft.AspNetCore.Mvc;

namespace HouseManagement.Controllers;

public class AccountController : Controller
{
    public IActionResult Register()
    {
        return View();
    }
}