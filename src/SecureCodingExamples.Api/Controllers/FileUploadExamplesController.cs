using Microsoft.AspNetCore.Mvc;
using SecureCodingExamples.Api.Services;

namespace SecureCodingExamples.Api.Controllers;

[ApiController]
[Route("api/examples/file-upload")]
public class FileUploadExamplesController : ControllerBase
{
    private readonly FileUploadSecurityService _service;

    public FileUploadExamplesController(FileUploadSecurityService service)
    {
        _service = service;
    }

    [HttpPost("validate")]
    public IActionResult ValidateUpload(IFormFile file)
    {
        if (!_service.IsAllowedFile(file))
        {
            return BadRequest(new
            {
                error = "File type or size not allowed."
            });
        }

        return Ok(new
        {
            accepted = true,
            safeStorageName = _service.CreateSafeStorageName(file)
        });
    }
}
