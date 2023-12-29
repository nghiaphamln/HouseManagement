using System.Net;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Models.Base;

namespace HouseManagement.Base;

public class BaseController : Controller
{
    protected async Task<IActionResult> ExecuteFunctionWithTrackId<T>(Func<Task<ErrorOr<T>>> func, string trackId)
    {
        try
        {
            var response = await func();
            return response.MatchFirst(Ok1, Func);

            IActionResult Ok1(T result) =>
                Ok(new IntegrationResponse<T>
                {
                    Data = result,
                    Status = HttpStatusCode.OK,
                    Message = "OK",
                    TrackId = trackId,
                });

            IActionResult Func(Error firstError)
            {
                var result = firstError.Type switch
                {
                    ErrorType.Validation => Ok(new IntegrationResponse
                    {
                        Message = firstError.Description,
                        Status = HttpStatusCode.BadRequest,
                        TrackId = trackId,
                    }),
                    ErrorType.NotFound => Ok(new IntegrationResponse
                    {
                        Message = firstError.Description,
                        Status = HttpStatusCode.NotFound,
                        TrackId = trackId,
                    }),
                    _ => Ok(new IntegrationResponse
                    {
                        Message = firstError.Description, Status = HttpStatusCode.InternalServerError,
                        TrackId = trackId,
                    })
                };
                return result;
            }
        }
        catch (Exception e)
        {
            return Ok(new IntegrationResponse<object>
            {
                Data = null,
                Status = HttpStatusCode.InternalServerError,
                Message = e.Message
            });
        }
    }

    protected Task<IActionResult> ToIntegrationResponse<T>(ErrorOr<T> response, string trackId)
    {
        try
        {
            return Task.FromResult(response.MatchFirst(Ok1, Func));

            IActionResult Ok1(T result) =>
                Ok(new IntegrationResponse<T>
                {
                    Data = result,
                    Status = HttpStatusCode.OK,
                    Message = "OK",
                    TrackId = trackId,
                });

            IActionResult Func(Error firstError)
            {
                var result = firstError.Type switch
                {
                    ErrorType.Validation => Ok(new IntegrationResponse
                    {
                        Message = firstError.Description,
                        Status = HttpStatusCode.BadRequest,
                        TrackId = trackId,
                    }),
                    ErrorType.NotFound => Ok(new IntegrationResponse
                    {
                        Message = firstError.Description,
                        Status = HttpStatusCode.NotFound,
                        TrackId = trackId,
                    }),
                    _ => Ok(new IntegrationResponse
                    {
                        Message = firstError.Description, Status = HttpStatusCode.InternalServerError,
                        TrackId = trackId,
                    })
                };
                return result;
            }
        }
        catch (Exception e)
        {
            return Task.FromResult<IActionResult>(Ok(new IntegrationResponse<object>
            {
                Data = null,
                Status = HttpStatusCode.InternalServerError,
                Message = e.Message
            }));
        }
    }
}