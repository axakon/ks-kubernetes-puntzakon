FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app
EXPOSE 80

COPY . .

RUN dotnet restore ./puntman/puntman.csproj

RUN dotnet publish ./puntman/puntman.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "./puntman.dll"]