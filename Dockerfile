# --- Base image for runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiér kun det nødvendige først
COPY GO-CustomerPortalService.csproj ./
COPY nuget.config ./
COPY nuget-packages ./nuget-packages

# Restore med lokal nuget-mappe
RUN dotnet restore "GO-CustomerPortalService.csproj" --configfile ./nuget.config

# Kopiér resten af projektet
COPY . .

# Byg og publicer
WORKDIR /src
RUN dotnet build GO-CustomerPortalService.csproj -c Release -o /app/build
RUN dotnet publish GO-CustomerPortalService.csproj -c Release -o /app/publish

# --- Final image ---
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GO-CustomerPortalService.dll"]
