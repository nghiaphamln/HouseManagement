using System.Security.Claims;
using CorrelationId.Abstractions;
using HouseManagement.Base;
using Logics.Group;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Base;
using Models.Entities;
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
        var searchData = (await logicGroup.GetForPaging(new GroupGetForPagingRequest(), _trackId)).Value;
        var pager = new PagerSearch<GroupEntity>(searchData.TotalRecord, 5, 1)
        {
            Results = searchData.Data,
            Page = 1
        };
        return View(pager);
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