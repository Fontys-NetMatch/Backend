using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.API.Response.Success.User;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.API.Controllers;

public class UserController : Controller
{
    private readonly IUserContainer _container;

    public UserController(IUserContainer container)
    {
        _container = container;
    }

    public static void Register(WebApplication app)
    {
        app.MapGet("/user/{id:int}", (
                [FromRoute] int id,
                [FromServices] UserController controller
            ) => controller.GetUser(id))
            .WithName("GetUserById")
            .WithDescription("Get a user by Id")
            .Produces<UserResponse>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .RequiresJwtToken()
            .WithTags("User")
            .WithOrder(1)
            .WithOpenApi();

        app.MapGet("/user", (
                [FromServices] UserController controller
            ) => controller.GetAllActiveUsers())
            .WithName("GetAllActiveUsers")
            .WithDescription("Get all active users")
            .Produces<UsersResponse>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .RequiresJwtToken()
            .WithTags("User")
            .WithOrder(2)
            .WithOpenApi();

        app.MapPost("/user", (
                [FromBody] User user,
                [FromServices] UserController controller
            ) => controller.CreateUser(user))
            .WithName("CreateUser")
            .WithDescription("Create a new user")
            .Produces<SuccessResponse>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .RequiresJwtToken()
            .WithTags("User")
            .WithOrder(3)
            .WithOpenApi();

        app.MapPut("/user/{id:int}", (
                [FromRoute] int id,
                [FromBody] User user,
                [FromServices] UserController controller
            ) => controller.UpdateUser(user))
            .WithName("UpdateUser")
            .WithDescription("Update an existing user")
            .Produces<SuccessResponse>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .RequiresJwtToken()
            .WithTags("User")
            .WithOrder(4)
            .WithOpenApi();

        app.MapDelete("/user/{id:int}", (
                [FromRoute] int id,
                [FromServices] UserController controller
            ) => controller.SoftDeleteUser(id))
            .WithName("SoftDeleteUser")
            .WithDescription("Soft delete a user by Id")
            .Produces<SuccessResponse>()
            .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            .RequiresJwtToken()
            .WithTags("User")
            .WithOrder(5)
            .WithOpenApi();
    }

    private BaseResponse GetUser(int id)
    {
        try
        {
            var user = _container.GetUserByIdAsync(id).Result;
            if (user == null)
                return new NotFoundResponse("User not found");

            return new UserResponse(
                user.Id,
                user.Firstname,
                user.Surname,
                user.Email,
                user.ProfileImagePath,
                user.IsActive
            );

        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }
    }

    private BaseResponse GetAllActiveUsers()
    {
        try
        {
            var users = _container.GetAllActiveUsersAsync().Result;
            if (users.Count == 0)
                return new NoContentResponse();

            var responses = users.Select(user => new UserResponse(
                user.Id,
                user.Firstname,
                user.Surname,
                user.Email,
                user.ProfileImagePath,
                user.IsActive
            )).ToList();

            return new UsersResponse(responses);
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }
    }

    private BaseResponse CreateUser(User user)
    {
        try
        {
            _container.CreateUser(user);
            return new SuccessResponse("User created successfully");
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }
    }

    private BaseResponse UpdateUser(User user)
    {
        try
        {
            _container.UpdateUser(user).Wait();
            return new SuccessResponse("User updated successfully");
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }
    }

    private BaseResponse SoftDeleteUser(int id)
    {
        try
        {
            _container.SoftDeleteUser(id).Wait();
            return new SuccessResponse("User soft-deleted successfully");
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }
    }
}
