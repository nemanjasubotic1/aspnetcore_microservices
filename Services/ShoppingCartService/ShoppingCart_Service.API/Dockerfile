# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/ShoppingCartService/ShoppingCart_Service.API/ShoppingCart_Service.API.csproj", "Services/ShoppingCartService/ShoppingCart_Service.API/"]
COPY ["GeneralUsing/GeneralUsing/GeneralUsing.csproj", "GeneralUsing/GeneralUsing/"]
COPY ["Integration/Integration.csproj", "Integration/"]
RUN dotnet restore "./Services/ShoppingCartService/ShoppingCart_Service.API/ShoppingCart_Service.API.csproj"
COPY . .
WORKDIR "/src/Services/ShoppingCartService/ShoppingCart_Service.API"
RUN dotnet build "./ShoppingCart_Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShoppingCart_Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCart_Service.API.dll"]