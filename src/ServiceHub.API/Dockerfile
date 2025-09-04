# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ServiceHub.API/ServiceHub.API.csproj", "src/ServiceHub.API/"]
COPY ["src/ServiceHub.Application/ServiceHub.Application.csproj", "src/ServiceHub.Application/"]
COPY ["src/ServiceHub.Domain/ServiceHub.Domain.csproj", "src/ServiceHub.Domain/"]
COPY ["src/ServiceHub.Shared/ServiceHub.Shared.csproj", "src/ServiceHub.Shared/"]
COPY ["src/ServiceHub.Infrastructure/ServiceHub.Infrastructure.csproj", "src/ServiceHub.Infrastructure/"]
RUN dotnet restore "./src/ServiceHub.API/ServiceHub.API.csproj"
COPY . .
WORKDIR "/src/src/ServiceHub.API"
RUN dotnet build "./ServiceHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ServiceHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceHub.API.dll"]