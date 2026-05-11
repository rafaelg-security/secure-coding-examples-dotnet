namespace SecureCodingExamples.Api.Dtos;

// Educational anti-pattern: exposes Role as client-controlled input.
// Safer code should use CreateUserRequest instead.
public class InsecureCreateUserRequest
{
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
}
