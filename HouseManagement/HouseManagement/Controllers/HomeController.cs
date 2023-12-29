using HouseManagement.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseManagement.Controllers;

[Authorize]
public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}