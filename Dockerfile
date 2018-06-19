FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src/StaticFileServer
COPY src/StaticFileServer.csproj .
RUN dotnet restore StaticFileServer.csproj
COPY src/ .
RUN dotnet build StaticFileServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish StaticFileServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StaticFileServer.dll"]
