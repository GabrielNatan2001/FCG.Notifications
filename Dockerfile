FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/FCG.Notifications.Domain/FCG.Notifications.Domain.csproj", "src/FCG.Notifications.Domain/"]
COPY ["src/FCG.Notifications.Application/FCG.Notifications.Application.csproj", "src/FCG.Notifications.Application/"]
COPY ["src/FCG.Notifications.Infrastructure/FCG.Notifications.Infrastructure.csproj", "src/FCG.Notifications.Infrastructure/"]
COPY ["src/FCG.Notifications.Worker/FCG.Notifications.Worker.csproj", "src/FCG.Notifications.Worker/"]

RUN dotnet restore "src/FCG.Notifications.Worker/FCG.Notifications.Worker.csproj"

COPY src/ .
WORKDIR /src/FCG.Notifications.Worker
RUN dotnet publish "FCG.Notifications.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FCG.Notifications.Worker.dll"]
