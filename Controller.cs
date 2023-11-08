using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleApp1;

[ApiController]
public class MyController : ControllerBase
{
    private readonly IAuthorizationService _service;

    public MyController(IAuthorizationService service)
    {
        _service = service;
    }

    [Authorize("MyPolicy", AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [HttpPost("/route")]
    public string MyMethod()
    {
        return "Hello World";
    }

    public async Task<IActionResult> MethodWithService()
    {
        var result = await _service.AuthorizeAsync(User, null,
            new AssertionRequirement(context => context.User.Identity?.Name == "rtm0x143"));

        if (!result.Succeeded) return new StatusCodeResult(StatusCodes.Status403Forbidden);

        return Ok("Hello World");
    }
}