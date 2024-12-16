FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NBPTask.API/NBPTask.API.csproj", "NBPTask.API/"]
COPY ["NBPTask.Infrastructure/NBPTask.Infrastructure.csproj", "NBPTask.Infrastructure/"]
COPY ["NBPTask.Domain/NBPTask.Domain.csproj", "NBPTask.Domain/"]
COPY ["NBPTask.Shared/NBPTask.Shared.csproj", "NBPTask.Shared/"]
COPY ["NBPTask.Application/NBPTask.Application.csproj", "NBPTask.Application/"]
RUN dotnet restore "NBPTask.API/NBPTask.API.csproj"
COPY . .
WORKDIR "/src/NBPTask.API"
RUN dotnet build "NBPTask.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NBPTask.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NBPTask.API.dll"]
