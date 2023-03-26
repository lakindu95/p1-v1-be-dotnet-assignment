FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /build/

COPY API/API.csproj API/API.csproj
COPY Domain/Domain.csproj Domain/Domain.csproj
COPY Infrastructure/Infrastructure.csproj Infrastructure/Infrastructure.csproj
           
RUN dotnet restore API/API.csproj
COPY . .
ARG VERSION
RUN dotnet build API/API.csproj -c Release /p:Version=$(if [ $VERSION ]; then echo $VERSION; else echo "0.0.0"; fi) -o /app

FROM build AS publish
ARG VERSION
RUN dotnet publish API/API.csproj -c Release /p:Version=$(if [ $VERSION ]; then echo $VERSION; else echo "0.0.0"; fi)\
 -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API.dll"]
