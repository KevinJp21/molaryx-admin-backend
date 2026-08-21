FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore "molaryx-admin.csproj"

RUN dotnet publish "molaryx-admin.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

EXPOSE 3000

ENV ASPNETCORE_URLS=http://+:3000

ENTRYPOINT ["dotnet", "molaryx-admin.dll"]