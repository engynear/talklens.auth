FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TalkLens.Auth.API/TalkLens.Auth.API.csproj", "TalkLens.Auth.API/"]
COPY ["TalkLens.Auth.Core/TalkLens.Auth.Core.csproj", "TalkLens.Auth.Core/"]
COPY ["TalkLens.Auth.Infrastructure/TalkLens.Auth.Infrastructure.csproj", "TalkLens.Auth.Infrastructure/"]
RUN dotnet restore "TalkLens.Auth.API/TalkLens.Auth.API.csproj"

COPY . .
RUN dotnet build "TalkLens.Auth.API/TalkLens.Auth.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TalkLens.Auth.API/TalkLens.Auth.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalkLens.Auth.API.dll"] 