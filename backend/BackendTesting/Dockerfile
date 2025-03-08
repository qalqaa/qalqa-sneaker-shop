# Базовый образ для рантайма
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

# Аргумент для сертификата
ARG CERT_PATH

# Проверяем, существует ли сертификат, и копируем его в контейнер
RUN if [ -f "$CERT_PATH" ]; then cp "$CERT_PATH" /app/aspnetcore-localhost.pfx; fi

EXPOSE 8080
EXPOSE 8081

# Образ для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BackendTesting/BackendTesting.csproj", "BackendTesting/"]
RUN dotnet restore "./BackendTesting/BackendTesting.csproj"
COPY . .
WORKDIR "/src/BackendTesting"
RUN dotnet build "./BackendTesting.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикация приложения
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BackendTesting.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный контейнер
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendTesting.dll"]
