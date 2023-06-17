#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Product/Products.Api/ProductApp.Api.csproj", "src/Services/Product/Products.Api/"]
COPY ["src/Services/Product/Product.Application/ProductApp.Application.csproj", "src/Services/Product/Product.Application/"]
COPY ["src/Services/Product/Product.Domain/ProductApp.Domain.csproj", "src/Services/Product/Product.Domain/"]
COPY ["src/Services/Product/Product.Infrastructure/ProductApp.Infrastructure.csproj", "src/Services/Product/Product.Infrastructure/"]
RUN dotnet restore "src/Services/Product/Products.Api/ProductApp.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Product/Products.Api"
RUN dotnet build "ProductApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductApp.Api.dll"]