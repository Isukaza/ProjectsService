FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ProjectsService/ProjectsService.csproj ProjectsService/
COPY DAL/DAL.csproj DAL/
RUN dotnet restore "ProjectsService/ProjectsService.csproj"

COPY . .
WORKDIR /src/ProjectsService
RUN dotnet publish "ProjectsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV DOTNET_RUNNING_IN_CONTAINER=true \
    ASPNETCORE_URLS="https://+:9443;http://+:9080" \
    ASPNETCORE_Kestrel__Certificates__Default__Path=/app/certs/cert.pfx

EXPOSE 9080
EXPOSE 9443

ENTRYPOINT ["dotnet", "ProjectsService.dll"]