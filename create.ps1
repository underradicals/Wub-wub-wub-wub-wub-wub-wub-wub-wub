dotnet new webapi -n Application.Client.WebApi -o ./src/Application.Client.WebApi
dotnet new webapi -n Application.IdentityProvider.WebApi -o ./src/Application.SecretsManager.WebApi
dotnet new webapi -n Application.SecretsManager.WebApi -o ./src/Application.SecretsManager.WebApi


dotnet sln add .\src\Application.Client.WebApi\Application.Client.WebApi.csproj
dotnet sln add .\src\Application.SecretsManager.WebApi\Application.SecretsManager.WebApi.csproj
dotnet sln add .\src\Application.IdentityProvider.WebApi\Application.IdentityProvider.WebApi.csproj

npm create vite@latest ./src/Application.Client.ClientApp
npm create vite@latest ./src/Application.SecretsManager.ClientApp
npm create vite@latest ./src/Application.IdentityProvider.ClientApp