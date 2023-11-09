using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSample;

[ApiController]
public class MyController : ControllerBase
{
    private readonly IAuthorizationService _service;

    public MyController(IAuthorizationService service)
    {
        _service = service;
    }

    [Authorize("MyPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("/method-with-attribute")]
    public string MyMethod()
    {
        return "Hello World";
    }

    [HttpGet("method-with-service")]
    public async Task<IActionResult> MethodWithService()
    {
        var result = await _service.AuthorizeAsync(User, null, "MyPolicy");

        if (!result.Succeeded) return new StatusCodeResult(StatusCodes.Status403Forbidden);

        return Ok("Hello World");
    }
}