using API.Common.JSend;
using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Common;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    protected IActionResult HandleResult<T>(Result<T> result, bool created = false)
    {
        if (!result.IsSuccess)
            return new JSendResponse(result.StatusCode, new JSendBody
            {
                Status = result.StatusCode < 500 ? "fail" : "error",
                Errors = result.Errors
            });

        return created
            ? JSendHelper.Created(data: result.Value)
            : JSendHelper.Success(data: result.Value);
    }

    protected IActionResult HandleResult(Result result)
    {
        if (!result.IsSuccess)
            return new JSendResponse(result.StatusCode, new JSendBody
            {
                Status = result.StatusCode < 500 ? "fail" : "error",
                Errors = result.Errors
            });

        return JSendHelper.Deleted();
    }
    
}