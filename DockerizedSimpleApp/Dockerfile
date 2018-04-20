FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /src
COPY DockerizedSimpleApp.sln ./
COPY DockerizedSimpleApp/DockerizedSimpleApp.csproj DockerizedSimpleApp/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/DockerizedSimpleApp
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:2.0-runtime AS base
#FROM build as base
WORKDIR /app
#FROM base AS final
#WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DockerizedSimpleApp.dll"]
#ENTRYPOINT ls 
