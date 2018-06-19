FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY src/paprikon.StaticFileServer.csproj paprikon.StaticFileServer/
RUN dotnet restore paprikon.StaticFileServer/paprikon.StaticFileServer.csproj
COPY . .
WORKDIR /src/paprikon.StaticFileServer
RUN dotnet build paprikon.StaticFileServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish paprikon.StaticFileServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "paprikon.StaticFileServer.dll"]
