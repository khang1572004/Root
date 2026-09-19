ARG DOTNET_SDK_IMAGE=mcr.microsoft.com/dotnet/sdk:10.0
ARG DOTNET_RUNTIME_IMAGE=mcr.microsoft.com/dotnet/aspnet:10.0

FROM ${DOTNET_SDK_IMAGE} AS build
WORKDIR /src

COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["src/Service.Api/Service.Api.csproj", "src/Service.Api/"]
COPY ["src/Service.Application/Service.Application.csproj", "src/Service.Application/"]
COPY ["src/Service.Domain/Service.Domain.csproj", "src/Service.Domain/"]
COPY ["src/Service.Infrastructure/Service.Infrastructure.csproj", "src/Service.Infrastructure/"]
RUN dotnet restore "src/Service.Api/Service.Api.csproj"

COPY . .
RUN dotnet publish "src/Service.Api/Service.Api.csproj" --configuration Release --no-restore --output /app/publish /p:UseAppHost=false

FROM build AS migration-bundle
ENV ConnectionStrings__Postgres="Host=localhost;Port=5432;Database=service;Username=service;Password=design-time-only"
ENV Jwt__Authority="https://issuer.example.invalid/"
ENV Jwt__Audience="service-api"
RUN dotnet tool install --tool-path /tools dotnet-ef --version 10.* \
    && /tools/dotnet-ef migrations bundle --configuration Release --no-build --project src/Service.Infrastructure/Service.Infrastructure.csproj --startup-project src/Service.Api/Service.Api.csproj --output /app/migrate

FROM ${DOTNET_RUNTIME_IMAGE} AS final
WORKDIR /app
RUN apt-get update \
    && apt-get install --no-install-recommends --yes curl \
    && rm -rf /var/lib/apt/lists/* \
    && addgroup --system --gid 10001 app \
    && adduser --system --uid 10001 --ingroup app --home /app --shell /usr/sbin/nologin app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build --chown=app:app /app/publish .
USER app
HEALTHCHECK --interval=30s --timeout=5s --start-period=15s --retries=3 CMD curl --fail --silent http://127.0.0.1:8080/health/live || exit 1
ENTRYPOINT ["dotnet", "Service.Api.dll"]
