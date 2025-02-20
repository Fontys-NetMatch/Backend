using LinqToDB;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Database;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.API.Controllers;

public class AuthController
{

    private readonly DbManager _db;

    public AuthController(DbManager db)
    {
        _db = db;
    }

    public static void Register(WebApplication app)
    {
        app.MapPost("/auth/login", (
                HttpContext context,
                [FromBody] LoginRequest request,
                [FromServices] AuthController controller
            ) => controller.LoginRequest(context, request))
            .WithName("Login")
            .WithDescription("Login using your credentials")
            .WithOpenApi();
        app.MapPost("/auth/register", (
                HttpContext context,
                [FromBody] RegisterRequest request,
                [FromServices] AuthController controller
            ) => controller.RegisterRequest(context, request))
            .WithName("Register")
            .WithDescription("Register a new user")
            .WithOpenApi();
    }

    private object LoginRequest(HttpContext? context, LoginRequest request)
    {
        var user = _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password).Result;

        if (user == null)
        {
            return new
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        return new
        {
            Success = true,
            Message = "Login successful",
            Token = "",
        };
    }

    private object RegisterRequest(HttpContext? context, RegisterRequest request)
    {
        var user = _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email).Result;

        if (user != null)
        {
            return new
            {
                Success = false,
                Message = "Email already used by another user"
            };
        }

        _db.Insert(new User
        {
            Firstname = "",
            Lastname = "",
            Email = request.Email,
            Password = request.Password
        });

        return new
        {
            Success = true,
            Message = "User registered successfully"
        };
    }

}