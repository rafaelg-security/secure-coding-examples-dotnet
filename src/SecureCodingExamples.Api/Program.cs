using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Data.Sqlite;
using SecureCodingExamples.Api.Middleware;
using SecureCodingExamples.Api.Security;
using SecureCodingExamples.Api.Services;
using SecureCodingExamples.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();

builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddScoped<SqlInjectionExampleService>();
builder.Services.AddScoped<PasswordSecurityService>();
builder.Services.AddScoped<MassAssignmentService>();
builder.Services.AddScoped<FileUploadSecurityService>();

builder.Services.AddScoped(_ =>
{
    var connection = new SqliteConnection("Data Source=secure-coding-examples.db");
    connection.Open();
    return connection;
});

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<SafeExceptionMiddleware>();

app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["Referrer-Policy"] = "no-referrer";
    await next();
});

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<DatabaseInitializer>().Initialize();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class Program { }
