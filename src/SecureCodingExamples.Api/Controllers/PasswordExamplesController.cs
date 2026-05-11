using Microsoft.AspNetCore.Mvc;
using SecureCodingExamples.Api.Dtos;
using SecureCodingExamples.Api.Services;

namespace SecureCodingExamples.Api.Controllers;

[ApiController]
[Route("api/examples/passwords")]
public class PasswordExamplesController : ControllerBase
{
    private readonly PasswordSecurityService _service;

    public PasswordExamplesController(PasswordSecurityService service)
    {
        _service = service;
    }

    [HttpPost("secure-login-check")]
    public async Task<IActionResult> SecureLoginCheck(LoginRequest request)
    {
        var isValid = await _service.SecureVerifyPasswordAsync(request);

        return Ok(new
        {
            authenticated = isValid,
            note = "Uses BCrypt verification and does not expose password hashes."
        });
    }
}
