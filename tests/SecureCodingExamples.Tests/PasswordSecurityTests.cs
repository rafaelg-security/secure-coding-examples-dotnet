using SecureCodingExamples.Api.Services;

namespace SecureCodingExamples.Tests;

public class PasswordSecurityTests
{
    [Fact]
    public void InsecurePasswordComparison_ShouldOnlyBeUsedAsEducationalAntiPattern()
    {
        var service = new PasswordSecurityService(null!);

        var result = service.InsecurePasswordComparison("password", "password");

        Assert.True(result);
    }

    [Fact]
    public void BCryptHash_ShouldVerifyOriginalPassword()
    {
        var password = "StrongPassword123!";
        var hash = BCrypt.Net.BCrypt.HashPassword(password);

        Assert.True(BCrypt.Net.BCrypt.Verify(password, hash));
        Assert.False(BCrypt.Net.BCrypt.Verify("WrongPassword123!", hash));
    }
}
