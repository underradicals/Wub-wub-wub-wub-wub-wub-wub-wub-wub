using Application.IdentityProvider.WebApi.Common;
using Application.IdentityProvider.WebApi.DbContexts;   
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.IdentityProvider.WebApi.Features.Register;

internal interface IRegisterUserRepository
{
    public Task<ApplicationUser> GetUserAsync(string email, string password);
    public Task<Result<RegisterUserResponse, ProblemDetails>> CreateUserAsync(RegisterUserDto user);
}

internal class RegisterUserRepository : IRegisterUserRepository
{
    private readonly PostgresDbContext _dbContext;

    public RegisterUserRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ApplicationUser> GetUserAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<RegisterUserResponse, ProblemDetails>> CreateUserAsync(RegisterUserDto user)
    {
        if (await _dbContext.ApplicationUsers.AnyAsync(x => x.NormalizedEmail == user.Email.ToUpper()))
        {
            return new ProblemDetails
            {
                Detail = "User was not created",
                Type = "Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Instance = "/register",
                Extensions = new Dictionary<string, object?>
                {
                    ["Error"] = "Email already exists",
                }
            };
        }

        var newUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            Email = user.Email,
            NormalizedEmail = user.Email.ToUpper(),
            Password = user.Password,
        };

        await _dbContext.ApplicationUsers.AddAsync(newUser);

        var changes = await _dbContext.SaveChangesAsync();

        if (changes == 0)
        {
            return new ProblemDetails
            {
                Detail = "User was not created",
                Type = "Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Instance = "/register",
                Extensions = new Dictionary<string, object?>
                {
                    ["Error"] = "This could be anything, our bad"
                }
            };
        }

        return new RegisterUserResponse
        {
            Id = newUser.Id,
            Email = newUser.Email,
            IsSuccessful = true
        };
    }
}