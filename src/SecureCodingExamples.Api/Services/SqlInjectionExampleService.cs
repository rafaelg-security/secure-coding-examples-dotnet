using Dapper;
using Microsoft.Data.Sqlite;
using SecureCodingExamples.Api.Dtos;

namespace SecureCodingExamples.Api.Services;

public class SqlInjectionExampleService
{
    private readonly SqliteConnection _connection;

    public SqlInjectionExampleService(SqliteConnection connection)
    {
        _connection = connection;
    }

    // Educational anti-pattern only:
    // string concatenation allows attacker-controlled input to change the query meaning.
    public async Task<IEnumerable<SafeUserResponse>> InsecureSearchByEmailAsync(string email)
    {
        var sql = $"SELECT Id, Email, DisplayName, Role FROM Users WHERE Email = '{email}'";
        return await _connection.QueryAsync<SafeUserResponse>(sql);
    }

    // Secure pattern:
    // parameterized queries keep data separate from SQL instructions.
    public async Task<IEnumerable<SafeUserResponse>> SecureSearchByEmailAsync(string email)
    {
        const string sql = """
            SELECT Id, Email, DisplayName, Role
            FROM Users
            WHERE Email = @Email;
        """;

        return await _connection.QueryAsync<SafeUserResponse>(sql, new { Email = email });
    }
}
