using Dapper;
using Microsoft.Data.Sqlite;

namespace SecureCodingExamples.Api.Security;

public class DatabaseInitializer
{
    private readonly SqliteConnection _connection;

    public DatabaseInitializer(SqliteConnection connection)
    {
        _connection = connection;
    }

    public void Initialize()
    {
        _connection.Execute("""
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Email TEXT NOT NULL UNIQUE,
                DisplayName TEXT NOT NULL,
                PasswordHash TEXT NOT NULL,
                Role TEXT NOT NULL
            );
        """);

        var existing = _connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Users;");
        if (existing > 0)
        {
            return;
        }

        _connection.Execute("""
            INSERT INTO Users (Email, DisplayName, PasswordHash, Role)
            VALUES (@Email, @DisplayName, @PasswordHash, @Role);
        """, new
        {
            Email = "admin@example.com",
            DisplayName = "Admin User",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            Role = "Admin"
        });

        _connection.Execute("""
            INSERT INTO Users (Email, DisplayName, PasswordHash, Role)
            VALUES (@Email, @DisplayName, @PasswordHash, @Role);
        """, new
        {
            Email = "user@example.com",
            DisplayName = "Standard User",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
            Role = "User"
        });
    }
}
