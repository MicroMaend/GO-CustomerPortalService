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

# Copy the built app
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# Remove default nginx config to ensure template is used
RUN rm -f /etc/nginx/conf.d/default.conf

# Copy nginx template
COPY GO-CustomerPortalService/nginx.conf.template /etc/nginx/templates/default.conf.template

# Install envsubst if not already available (usually included in nginx:alpine)
RUN apk add --no-cache gettext

# Create startup script for debugging
COPY <<EOF /docker-entrypoint.d/40-substitute-env.sh
#!/bin/sh
set -e

echo "=== Environment Variables ==="
echo "AUCTION_SERVICE_URL: \${AUCTION_SERVICE_URL}"
echo "USER_SERVICE_URL: \${USER_SERVICE_URL}"
echo "BIDDING_SERVICE_URL: \${BIDDING_SERVICE_URL}"
echo "AUTH_SERVICE_URL: \${AUTH_SERVICE_URL}"
echo "CATALOG_SERVICE_URL: \${CATALOG_SERVICE_URL}"
echo "================================"

# Check if template exists
if [ -f "/etc/nginx/templates/default.conf.template" ]; then
    echo "Template file found: /etc/nginx/templates/default.conf.template"
else
    echo "ERROR: Template file not found!"
    exit 1
fi

# The nginx:alpine image automatically processes templates in /etc/nginx/templates/
# and creates config files in /etc/nginx/conf.d/ with envsubst
EOF

RUN chmod +x /docker-entrypoint.d/40-substitute-env.sh

EXPOSE 80

# Use the default nginx entrypoint which handles template processing
CMD ["nginx", "-g", "daemon off;"]