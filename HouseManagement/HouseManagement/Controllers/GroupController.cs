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
    
    public async Task<IActionResult> Index()
    {
        var defaultData = (await logicGroup.GetForPaging(new GroupGetForPagingRequest(), _trackId)).Value;
        return View(defaultData);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequest request)
    {
        request.TrackId = _trackId;
        request.CreatedUser = User.FindFirstValue(ClaimTypes.Email)!;
        return await ExecuteFunctionWithTrackId(() => logicGroup.Create(request), _trackId);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetForPaging([FromBody] GroupGetForPagingRequest request)
    {
        return await ExecuteFunctionWithTrackId(() => logicGroup.GetForPaging(request, _trackId), _trackId);
    }
}