using Application.IdentityProvider.WebApi;

try
{
    var builder = IdentityProviderExtensions.CreateApplicationBuilder(args);
    var app = builder.BuildApplication();
    app.Run();
}
catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
{
    Console.WriteLine(ex.Message);
    throw;
}
finally
{
    Console.WriteLine("End of application");
}
