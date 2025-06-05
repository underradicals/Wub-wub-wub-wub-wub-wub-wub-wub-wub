using Application.IdentityProvider.WebApi.Common;
using Application.IdentityProvider.WebApi.Common.EmailVerification;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.IdentityProvider.WebApi.Features.Register;

internal interface IRequestHandler<in TRequest, TResponse> where TResponse : IResult
{
    Task<IResult> Handle(TRequest request, CancellationToken cancellationToken);
}

internal class RegisterUserHandler : IRequestHandler<RegisterUserRequest, IResult>
{
    private readonly IValidator<RegisterUserRequest> _validator;
    private readonly IEmailVerificationService _emailVerificationService;
    private readonly IRegisterUserRepository _repository;

    public RegisterUserHandler(
        IValidator<RegisterUserRequest> validator, 
        IEmailVerificationService emailVerificationService, 
        IRegisterUserRepository repository)
    {
        _validator = validator;
        _emailVerificationService = emailVerificationService;
        _repository = repository;
    }

    public async Task<IResult> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        
        var validationRequest = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationRequest.IsValid)
        {
            var errors = validationRequest.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return Results.ValidationProblem(errors);
        }
        
        // Check to see if Email is Valid (without sending an email)
        // if (await _emailVerificationService.HasValidMxRecords(request.Email) == false)
        // {
        //     // return ProblemDetails
        // }
        
        var hasher = new PasswordHasher<ApplicationUser>();
        var user = new ApplicationUser();
        var hashedPassword = hasher.HashPassword(user, request.Password);
        
        var userResponse = await _repository.CreateUserAsync(new RegisterUserDto
        {
            Email = request.Email,
            Password = hashedPassword,
        });

        return userResponse.Match(
            x => Results.CreatedAtRoute("RegisterUser", new { Id = x.Id, Email = x.Email }, x),
            Results.BadRequest
        );
    }
}