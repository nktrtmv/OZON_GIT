FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "TGM.csproj" .
RUN dotnet restore "TGM.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "TGM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TGM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TGM.dll"]
