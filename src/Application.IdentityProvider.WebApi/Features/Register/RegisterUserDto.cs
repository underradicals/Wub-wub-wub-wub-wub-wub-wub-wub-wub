﻿namespace Application.IdentityProvider.WebApi.Features.Register;

public class RegisterUserDto
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}