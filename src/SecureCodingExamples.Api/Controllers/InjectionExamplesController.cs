using Microsoft.AspNetCore.Mvc;
using SecureCodingExamples.Api.Services;

namespace SecureCodingExamples.Api.Controllers;

[ApiController]
[Route("api/examples/injection")]
public class InjectionExamplesController : ControllerBase
{
    private readonly SqlInjectionExampleService _service;

    public InjectionExamplesController(SqlInjectionExampleService service)
    {
        _service = service;
    }

    [HttpGet("insecure-search")]
    public async Task<IActionResult> InsecureSearch([FromQuery] string email)
    {
        var users = await _service.InsecureSearchByEmailAsync(email);
        return Ok(new
        {
            warning = "Educational anti-pattern: vulnerable query construction.",
            users
        });
    }

    [HttpGet("secure-search")]
    public async Task<IActionResult> SecureSearch([FromQuery] string email)
    {
        var users = await _service.SecureSearchByEmailAsync(email);
        return Ok(users);
    }
}
