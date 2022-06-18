#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AgendaEscolarApi/AgendaEscolarApi.csproj", "AgendaEscolarApi/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "AgendaEscolarApi/AgendaEscolarApi.csproj"
COPY . .
WORKDIR "/src/AgendaEscolarApi"
RUN dotnet build "AgendaEscolarApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AgendaEscolarApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet AgendaEscolarApi.dll