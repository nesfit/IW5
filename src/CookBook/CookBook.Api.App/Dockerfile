FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-base

# ref: https://blog.nimbleways.com/docker-build-caching-for-dotnet-applications-done-right-with-dotnet-subset/
FROM build-base AS prepare-restore-files
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global --no-cache dotnet-subset --version 0.3.2
WORKDIR /src
COPY . .
RUN dotnet subset restore "CookBook.Api.App/CookBook.Api.App.csproj" --root-directory /src --output restore_subset/

FROM build-base AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY --from=prepare-restore-files /src/restore_subset .
RUN dotnet restore "CookBook.Api.App/CookBook.Api.App.csproj"

COPY . .
WORKDIR "/src/CookBook.Api.App"
RUN dotnet build "CookBook.Api.App.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CookBook.Api.App.csproj" --no-restore -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CookBook.Api.App.dll"]
