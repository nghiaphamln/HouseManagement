using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Models.Account;
using CorrelationId.Abstractions;
using HouseManagement.Base;
using Logics.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Models.Base;

namespace HouseManagement.Controllers;

[AllowAnonymous]
public class AccountController(
    ICorrelationContextAccessor correlationContextAccessor,
    ILogicAccount logicAccount
) : BaseController
{
    private readonly string _trackId = correlationContextAccessor.CorrelationContext.CorrelationId;

    [PreventLoginRedirectAttribute]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [PreventLoginRedirectAttribute]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        request.TrackId = _trackId;
        return await ExecuteFunctionWithTrackId(() => logicAccount.Register(request), _trackId);
    }

    [PreventLoginRedirectAttribute]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [PreventLoginRedirectAttribute]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromBody] RegisterRequest request)
    {
        request.TrackId = _trackId;

        var validUser = await logicAccount.Login(request);
        if (validUser.IsError)
        {
            return await ToIntegrationResponse(validUser, _trackId);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, validUser.Value.FullName),
            new(ClaimTypes.Email, validUser.Value.Email),
            new(ClaimTypes.Role, validUser.Value.Role),
            new(ClaimTypes.Thumbprint, validUser.Value.Avatar)
        };

        var identity = new ClaimsIdentity(claims, "cookie");
        var principal = new ClaimsPrincipal(identity);
        
        await HttpContext.SignInAsync(scheme: "HouseManagementSecurityScheme", principal: principal);

        return Ok(new IntegrationResponse<string>
        {
            Status = HttpStatusCode.OK,
            TrackId = _trackId,
            Message = "Đăng nhập thành công",
            Data = request.RequestPath ?? "/"
        });
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(scheme: "HouseManagementSecurityScheme");
        return RedirectToAction("Login");
    }
}