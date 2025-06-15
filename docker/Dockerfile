FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY src/*.sln ./
COPY src/OrderManagement.Api/*.csproj ./OrderManagement.Api/
COPY src/OrderManagement.Domain/*.csproj ./OrderManagement.Domain/
COPY src/OrderManagement.Infra/*.csproj ./OrderManagement.Infra/
COPY src/OrderManagement.Application/*.csproj ./OrderManagement.Application/

RUN dotnet restore OrderManagement.Api/OrderManagement.Api.csproj

COPY src/ ./

RUN dotnet build OrderManagement.Api/OrderManagement.Api.csproj -c Release -o /app/build

RUN dotnet publish OrderManagement.Api/OrderManagement.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "OrderManagement.Api.dll"]