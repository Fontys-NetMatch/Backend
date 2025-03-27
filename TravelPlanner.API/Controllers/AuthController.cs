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
    // Declare container to interact with the auth logic layer & IAppconfig interface
    private readonly IAuthContainer _container;
    private readonly IAppConfig _config;

    // Constructor to initialize the controller with the Auth container & IAppconfig interface
    public AuthController(IAuthContainer container, IAppConfig config)
    {
        _container = container;
        _config = config;
    }

    //Registration of a user
    public static void Register(WebApplication app)
    {
        app.MapPost("/auth/login", (
                HttpContext context, //HTTP context, handels data return and how it is sent back
                [FromBody] LoginData data, //Gets data from api request, moves data to LoginData object
                [FromServices] AuthController controller //Regelt dependancy injection voor controller
            ) => controller.LoginRequest(context, data)) //Setsup LoginRequest method while spreading out Data
            .WithName("Login")// Name the endpoint for reference
            .WithDescription("Login using your credentials")
            .Produces<LoginResponse>() //Return of succesfull Data
            .Produces<InvalidCredentialsResponse>(StatusCodes.Status400BadRequest) //Error handeling, for invalid data
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError) //Error handeling, for internal server problems
            .WithOpenApi(); //Adds documentation to Swagger
        app.MapPost("/auth/register", (
                HttpContext context, //HTTP context, handels data return and how it is sent back
                [FromBody] RegisterData data, //Gets data from api request, moves data to LoginData object
                [FromServices] AuthController controller //Regelt dependancy injection voor controller
            ) => controller.RegisterRequest(context, data)) //Setup Registerrequest method while spreading out Data
            .WithName("Register")// Name the endpoint for reference
            .WithDescription("Register a new user")
            .Produces<SuccessResponse>() //Return of succesfull Data
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError) //Error handeling, for internal server problems
            .WithOpenApi(); //Adds documentation to Swagger
    }

    private BaseResponse LoginRequest(HttpContext? context, LoginData data)
    {
        try
        {
            var user = _container.LoginUser(data); // Call loginUser from IAuthContainer
            var jwtToken = GenerateJwtToken(user, data.Remember); //Generate JWT Autehtication Token

            var response = new LoginResponse( //Generate response
                "User logged in successfully",
                new LoginDataObj(jwtToken, user)
            );
            return response;
        }
        catch (InvalidCredentialsException) // Error handeling, Autethication token
        {
            return new InvalidCredentialsResponse();
        }
        catch (BllException e) // Error handeling from BLL
        {
            return new ErrorResponse(e.Message);
        }
    }

    private BaseResponse RegisterRequest(HttpContext? context, RegisterData data)
    {
        try
        {
            _container.RegisterUser(data); // Call RegisterUser form IAuthContainer
            return new SuccessResponse("User registered successfully"); // Return SuccesResponse to api caller
        }
        catch (BllException e) //Error handeling form BLL
        {
            return new ErrorResponse(e.Message);
        }
    }

    private string GenerateJwtToken(User user, bool remember = false)
    {
        var claims = new[] //Generatie van een unieke gebruiker
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.ID.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Firstname),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.Surname),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var secretBytes = Encoding.UTF8.GetBytes(_config.GetJwtConfig().Secret); //Genereerd Bytes voor key
        var key = new SymmetricSecurityKey(secretBytes); //Generates Key with bytes
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Hashed key

        var token = new JwtSecurityToken( //Generates JwtToken
            issuer: _config.GetJwtConfig().Issuer, //Backend used as sender
            audience: _config.GetJwtConfig().Audience, //API caller as audience
            claims: claims, //Jwttoken user information insert
            expires: remember ? DateTime.Now.AddMonths(1) : DateTime.Now.AddHours(1), //JWtToken duration
            signingCredentials: credentials); //JwtToken key insert

        return new JwtSecurityTokenHandler().WriteToken(token); //Return Token
    }

}