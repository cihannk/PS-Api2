#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PS-Api2/PS-Api2.csproj", "PS-Api2/"]
RUN dotnet restore "PS-Api2/PS-Api2.csproj"
COPY . .
WORKDIR "/src/PS-Api2"
RUN dotnet build "PS-Api2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PS-Api2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

LABEL io.k8s.display-name="ps.api2.openshift" \
      io.k8s.description="ps.api2.openshift" \
      io.openshift.expose-services="8080:http"

ENV DockerFileVersion=10

EXPOSE 8080

ENV ASPNETCORE_URLS=http://*:8080

ENTRYPOINT ["dotnet", "PS-Api2.dll"]
