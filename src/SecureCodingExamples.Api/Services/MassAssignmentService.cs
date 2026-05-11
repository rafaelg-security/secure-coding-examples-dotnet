using Dapper;
using Microsoft.Data.Sqlite;
using SecureCodingExamples.Api.Dtos;

namespace SecureCodingExamples.Api.Services;

public class MassAssignmentService
{
    private readonly SqliteConnection _connection;

    public MassAssignmentService(SqliteConnection connection)
    {
        _connection = connection;
    }

    // Educational anti-pattern only:
    // allows the client to choose Role, which could create privilege escalation.
    public async Task<int> InsecureCreateUserAsync(InsecureCreateUserRequest request)
    {
        const string sql = """
            INSERT INTO Users (Email, DisplayName, PasswordHash, Role)
            VALUES (@Email, @DisplayName, @PasswordHash, @Role);
            SELECT last_insert_rowid();
        """;

        return await _connection.ExecuteScalarAsync<int>(sql, new
        {
            request.Email,
            request.DisplayName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            request.Role
        });
    }

    // Secure pattern:
    // use allow-listed DTOs and assign privileged fields server-side.
    public async Task<int> SecureCreateUserAsync(CreateUserRequest request)
    {
        const string sql = """
            INSERT INTO Users (Email, DisplayName, PasswordHash, Role)
            VALUES (@Email, @DisplayName, @PasswordHash, @Role);
            SELECT last_insert_rowid();
        """;

        return await _connection.ExecuteScalarAsync<int>(sql, new
        {
            request.Email,
            request.DisplayName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "User"
        });
    }
}
