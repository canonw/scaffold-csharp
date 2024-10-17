FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5067

ENV ASPNETCORE_URLS=http://+:5067

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/MyWebApi/MyWebApi.csproj", "src/MyWebApi/"]
RUN dotnet restore "src/MyWebApi/MyWebApi.csproj"
COPY . .
WORKDIR "/src/src/MyWebApi"
RUN dotnet build "MyWebApi.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "MyWebApi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyWebApi.dll"]
