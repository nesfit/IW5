FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY Directory.Build.props .
COPY ["CookBook.Api.App/CookBook.Api.App.csproj", "CookBook.Api.App/"]
COPY ["CookBook.Api.BL/CookBook.Api.BL.csproj", "CookBook.Api.BL/"]
COPY ["CookBook.Api.DAL.Common/CookBook.Api.DAL.Common.csproj", "CookBook.Api.DAL.Common/"]
COPY ["CookBook.Common/CookBook.Common.csproj", "CookBook.Common/"]
COPY ["CookBook.Common.BL/CookBook.Common.BL.csproj", "CookBook.Common.BL/"]
COPY ["CookBook.Common.Models/CookBook.Common.Models.csproj", "CookBook.Common.Models/"]
COPY ["CookBook.Api.DAL.EF/CookBook.Api.DAL.EF.csproj", "CookBook.Api.DAL.EF/"]
COPY ["CookBook.Api.DAL.Memory/CookBook.Api.DAL.Memory.csproj", "CookBook.Api.DAL.Memory/"]
COPY ["CookBook.Hosting.ServiceDefaults/CookBook.Hosting.ServiceDefaults.csproj", "CookBook.Hosting.ServiceDefaults/"]
RUN dotnet restore "CookBook.Api.App/CookBook.Api.App.csproj"
COPY . .
WORKDIR "/src/CookBook.Api.App"
RUN dotnet build "CookBook.Api.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CookBook.Api.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CookBook.Api.App.dll"]
