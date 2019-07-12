FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ocelot.api-gateway.csproj", "ocelot.api-gateway/"]
RUN dotnet restore "ocelot.api-gateway/ocelot.api-gateway.csproj"
COPY . .
RUN dotnet build "ocelot.api-gateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ocelot.api-gateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ocelot.api-gateway.dll"]