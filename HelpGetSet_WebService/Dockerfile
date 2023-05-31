FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5001
ENV DOTNET_URLS=http://+:5001
    
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "PL_API/PL_API.csproj"
WORKDIR "/src/PL_API"
RUN dotnet build "PL_API.csproj" -c Release -o /app
    
FROM build AS publish
WORKDIR "/src/PL_API"
RUN dotnet publish "PL_API.csproj" -c Release -o /app
    
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PL_API.dll"]
