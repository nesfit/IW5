FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY Directory.Build.props .
COPY ["CookBook.Web.App/CookBook.Web.App.csproj", "CookBook.Web.App/"]
COPY ["CookBook.Web.BL/CookBook.Web.BL.csproj", "CookBook.Web.BL/"]
COPY ["CookBook.Common.BL/CookBook.Common.BL.csproj", "CookBook.Common.BL/"]
COPY ["CookBook.Common.Models/CookBook.Common.Models.csproj", "CookBook.Common.Models/"]
COPY ["CookBook.Common/CookBook.Common.csproj", "CookBook.Common/"]
COPY ["CookBook.Web.DAL/CookBook.Web.DAL.csproj", "CookBook.Web.DAL/"]
RUN dotnet restore "CookBook.Web.App/CookBook.Web.App.csproj"
COPY . .
WORKDIR "/src/CookBook.Web.App"
RUN dotnet build "CookBook.Web.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CookBook.Web.App.csproj" -c $BUILD_CONFIGURATION --self-contained -o /app/publish /p:UseAppHost=false

FROM docker.io/library/busybox:1.36 AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["httpd", "-f", "-v", "-p", "8081", "-h", "./wwwroot"]
