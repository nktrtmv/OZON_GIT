FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "TRAM.csproj" .
RUN dotnet restore "TRAM.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "TRAM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TRAM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TRAM.dll"]
