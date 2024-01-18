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
    private const int DefaultPageSize = 5;

    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        var searchData = (await logicGroup.GetForPaging(new GroupGetForPagingRequest
        {
            PageSize = DefaultPageSize,
            PageNumber = pageNumber
        }, _trackId)).Value;
        var pager = new PagerSearch<GroupEntity>(searchData.TotalRecord, DefaultPageSize, pageNumber)
        {
            Results = searchData.Data,
            Page = pageNumber
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