# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["Services/ProductService/Product.API/ProductCategory.API.csproj", "Services/ProductService/Product.API/"]
COPY ["GeneralUsing/GeneralUsing/GeneralUsing.csproj", "GeneralUsing/GeneralUsing/"]

# Restore dependencies for ProductCategory.API
WORKDIR /src/Services/ProductService/Product.API
RUN dotnet restore ProductCategory.API.csproj  # Corrected: Specify the project file

# Restore dependencies for GeneralUsing
WORKDIR /src/GeneralUsing/GeneralUsing
RUN dotnet restore GeneralUsing.csproj # Corrected: Specify the project file

# Copy the rest of the source code
WORKDIR /src
COPY . .

# Build the application
WORKDIR /src/Services/ProductService/Product.API
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Services/ProductService/Product.API
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductCategory.API.dll"]