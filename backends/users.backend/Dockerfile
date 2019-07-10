FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["users.backend.csproj", "users.backend/"]
RUN dotnet restore "users.backend/users.backend.csproj"
COPY . .
RUN dotnet build "users.backend.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "users.backend.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "users.backend.dll"]