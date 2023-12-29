using Microsoft.AspNetCore.Mvc;
using Models.Account;
using CorrelationId.Abstractions;
using HouseManagement.Base;
using Logics.Account;

namespace HouseManagement.Controllers;

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
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        request.TrackId = _trackId;
        return await ExecuteFunctionWithTrackId(() => logicAccount.Register(request), _trackId);
    }
}