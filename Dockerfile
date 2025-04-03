FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MedApi.API/MedApi.API.csproj", "MedApi.API/"]
COPY ["MedApi.Application/MedApi.Application.csproj", "MedApi.Application/"]
COPY ["MedApi.Domain/MedApi.Domain.csproj", "MedApi.Domain/"]
COPY ["MedApi.Infrastructure/MedApi.Infrastructure.csproj", "MedApi.Infrastructure/"]
RUN dotnet restore "MedApi.API/MedApi.API.csproj"

COPY . .
WORKDIR "/src/MedApi.API"
RUN dotnet build "MedApi.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MedApi.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedApi.API.dll"]