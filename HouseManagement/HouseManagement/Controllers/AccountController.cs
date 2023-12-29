using Microsoft.AspNetCore.Mvc;
using Models.Account;
using CorrelationId.Abstractions;
using HouseManagement.Base;
using Logics.Account;
using Microsoft.AspNetCore.Authorization;

namespace HouseManagement.Controllers;

[AllowAnonymous]
public class AccountController(
    ICorrelationContextAccessor correlationContextAccessor,
    ILogicAccount logicAccount
) : BaseController
{
    private readonly string _trackId = correlationContextAccessor.CorrelationContext.CorrelationId;

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        request.TrackId = _trackId;
        return await ExecuteFunctionWithTrackId(() => logicAccount.Register(request), _trackId);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([FromBody] RegisterRequest request)
    {
        request.TrackId = _trackId;
        return await ExecuteFunctionWithTrackId(() => logicAccount.Login(request), _trackId);
    }
}