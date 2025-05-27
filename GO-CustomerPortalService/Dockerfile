# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY nuget.config ./
COPY nuget-packages ./nuget-packages
COPY GO-CustomerPortalService/GO-CustomerPortalService.csproj GO-CustomerPortalService/

RUN dotnet restore "GO-CustomerPortalService/GO-CustomerPortalService.csproj" --configfile ./nuget.config

COPY . .

WORKDIR /src/GO-CustomerPortalService
RUN dotnet publish GO-CustomerPortalService.csproj -c Release -o /app/publish

# Final stage - nginx server
FROM nginx:alpine AS final

COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY GO-CustomerPortalService/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

ENTRYPOINT ["nginx", "-g", "daemon off;"]
