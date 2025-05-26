# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopier nuget konfiguration og pakker først
COPY nuget.config ./
COPY nuget-packages ./nuget-packages

# Kopier HELE GO-CustomerPortalService projektmappen
# Dette inkluderer .csproj, Program.cs, Pages, og din Authentication mappe
COPY GO-CustomerPortalService/ GO-CustomerPortalService/

# Skift arbejdsmappe til Blazor projektet
WORKDIR /src/GO-CustomerPortalService

# Gendan afhængigheder for Blazor projektet
RUN dotnet restore "GO-CustomerPortalService.csproj" --configfile /src/nuget.config

# Publicer Blazor applikationen
RUN dotnet publish GO-CustomerPortalService.csproj -c Release -o /app/publish

# Final stage - nginx server
FROM nginx:alpine AS final

# Kopier det publicerede wwwroot indhold fra build stadiet
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# Kopier nginx.conf fra roden af build konteksten
# Antager at nginx.conf er i roden af dit repo (samme niveau som GO-CustomerPortalService mappen)
COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

ENTRYPOINT ["nginx", "-g", "daemon off;"]