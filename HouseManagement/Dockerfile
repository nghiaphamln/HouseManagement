FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

EXPOSE 80

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
CMD ["dotnet", "HouseManagement.dll", "run"]