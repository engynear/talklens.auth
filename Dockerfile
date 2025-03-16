FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["TalkLens.Auth.API/TalkLens.Auth.API.csproj", "TalkLens.Auth.API/"]
COPY ["TalkLens.Auth.Core/TalkLens.Auth.Core.csproj", "TalkLens.Auth.Core/"]
COPY ["TalkLens.Auth.Application/TalkLens.Auth.Application.csproj", "TalkLens.Auth.Application/"]
COPY ["TalkLens.Auth.Infrastructure/TalkLens.Auth.Infrastructure.csproj", "TalkLens.Auth.Infrastructure/"]
RUN dotnet restore "TalkLens.Auth.API/TalkLens.Auth.API.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "TalkLens.Auth.API/TalkLens.Auth.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "TalkLens.Auth.API/TalkLens.Auth.API.csproj" -c Release -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalkLens.Auth.API.dll"] 