using Dapper;
using Microsoft.Data.Sqlite;
using SecureCodingExamples.Api.Dtos;
using SecureCodingExamples.Api.Models;

namespace SecureCodingExamples.Api.Services;

public class PasswordSecurityService
{
    private readonly SqliteConnection _connection;

    public PasswordSecurityService(SqliteConnection connection)
    {
        _connection = connection;
    }

    // Educational anti-pattern only:
    // this simulates insecure thinking and must not be used for real authentication.
    public bool InsecurePasswordComparison(string suppliedPassword, string storedPassword)
    {
        return suppliedPassword == storedPassword;
    }

    public async Task<bool> SecureVerifyPasswordAsync(LoginRequest request)
    {
        const string sql = """
            SELECT Id, Email, DisplayName, PasswordHash, Role
            FROM Users
            WHERE Email = @Email;
        """;

        var user = await _connection.QuerySingleOrDefaultAsync<UserRecord>(sql, new { request.Email });

        if (user is null)
        {
            return false;
        }

        return BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
    }
}
