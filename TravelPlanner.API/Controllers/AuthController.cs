using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.Domain.Exceptions;
using TravelPlanner.Domain.Interfaces;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Request.Auth;
using InvalidCredentialsException = TravelPlanner.Domain.Exceptions.InvalidCredentialsException;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.DataObjects;

namespace TravelPlanner.API.Controllers;

public class AuthController
{

    private readonly IAuthContainer _container;
    private readonly IAppConfig _config;

    public AuthController(IAuthContainer container, IAppConfig config)
    {
        _container = container;
        _config = config;
    }

    public static void Register(WebApplication app)
    {
        app.MapPost("/auth/login", (
                HttpContext context,
                [FromBody] LoginData data,
                [FromServices] AuthController controller
            ) => controller.LoginRequest(context, data))
            .WithName("Login")
            .WithDescription("Login using your credentials")
            .Produces<LoginResponse>()
            .Produces<InvalidCredentialsResponse>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();
        app.MapPost("/auth/register", (
                HttpContext context,
                [FromBody] RegisterData data,
                [FromServices] AuthController controller
            ) => controller.RegisterRequest(context, data))
            .WithName("Register")
            .WithDescription("Register a new user")
            .Produces<SuccessResponse>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .WithOpenApi();
    }

    private BaseResponse LoginRequest(HttpContext? context, LoginData data)
    {
        try
        {
            var user = _container.LoginUser(data);
            var jwtToken = GenerateJwtToken(user, data.Remember);

            var response = new LoginResponse(
                "User logged in successfully",
                new LoginDataObj(jwtToken, user)
            );
            return response;
        }
        catch (InvalidCredentialsException)
        {
            return new InvalidCredentialsResponse();
        }
        catch (BllException e)
        {
            return new ErrorResponse(e.Message);
        }
    }

    private BaseResponse RegisterRequest(HttpContext? context, RegisterData data)
    {
        try
        {
            _container.RegisterUser(data);
            return new SuccessResponse("User registered successfully");
        }
        catch (BllException e)
        {
            return new ErrorResponse(e.Message);
        }
    }

    private string GenerateJwtToken(User user, bool remember = false)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Firstname),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.Lastname),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var secretBytes = Encoding.UTF8.GetBytes(_config.GetJwtConfig().Secret);
        var key = new SymmetricSecurityKey(secretBytes);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config.GetJwtConfig().Issuer,
            audience: _config.GetJwtConfig().Audience,
            claims: claims,
            expires: remember ? DateTime.Now.AddMonths(1) : DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}