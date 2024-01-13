using System.Security.Claims;
using CorrelationId.Abstractions;
using HouseManagement.Base;
using Logics.Group;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Group;

namespace HouseManagement.Controllers;

[Authorize]
public class GroupController(
    ICorrelationContextAccessor correlationContextAccessor,
    ILogicGroup logicGroup
) : BaseController
{
    private readonly string _trackId = correlationContextAccessor.CorrelationContext.CorrelationId;
    
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequest request)
    {
        request.TrackId = _trackId;
        request.CreatedUser = User.FindFirstValue(ClaimTypes.Name)!;
        return await ExecuteFunctionWithTrackId(() => logicGroup.Create(request), _trackId);
    }
}