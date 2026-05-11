using Microsoft.AspNetCore.Mvc;
using SecureCodingExamples.Api.Dtos;
using SecureCodingExamples.Api.Services;

namespace SecureCodingExamples.Api.Controllers;

[ApiController]
[Route("api/examples/mass-assignment")]
public class MassAssignmentExamplesController : ControllerBase
{
    private readonly MassAssignmentService _service;

    public MassAssignmentExamplesController(MassAssignmentService service)
    {
        _service = service;
    }

    [HttpPost("insecure-create-user")]
    public async Task<IActionResult> InsecureCreateUser(InsecureCreateUserRequest request)
    {
        var id = await _service.InsecureCreateUserAsync(request);

        return Ok(new
        {
            warning = "Educational anti-pattern: client can control Role.",
            id
        });
    }

    [HttpPost("secure-create-user")]
    public async Task<IActionResult> SecureCreateUser(CreateUserRequest request)
    {
        var id = await _service.SecureCreateUserAsync(request);

        return Ok(new
        {
            id,
            roleAssignedByServer = "User"
        });
    }
}
