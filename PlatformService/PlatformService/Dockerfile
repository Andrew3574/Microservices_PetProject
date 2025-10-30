FROM mcr.microsoft.com/dotnet/sdk:9.0 as publish
WORKDIR /src
COPY ["PlatformService.csproj","."]
RUN dotnet restore "PlatformService.csproj"
COPY . .
RUN dotnet publish "PlatformService.csproj" -o /src/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 as final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=publish /src/publish .

ENTRYPOINT ["dotnet","PlatformService.dll"]