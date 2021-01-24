#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["London.Api/London.Api.csproj", "London.Api/"]
RUN dotnet restore "London.Api/London.Api.csproj"
COPY . .
WORKDIR "/src/London.Api"
RUN dotnet build "London.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "London.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "London.Api.dll"]