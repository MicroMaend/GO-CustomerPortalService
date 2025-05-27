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

# Copy nginx template instead of direct config
COPY GO-CustomerPortalService/nginx.conf.template /etc/nginx/templates/default.conf.template

# Install envsubst if not already available (usually included in nginx:alpine)
RUN apk add --no-cache gettext

# Create startup script
COPY <<EOF /docker-entrypoint.d/40-substitute-env.sh
#!/bin/sh
set -e

# Set default values for environment variables if not provided
export AUCTION_SERVICE_URL=\${AUCTION_SERVICE_URL:-http://localhost:8080}
export USER_SERVICE_URL=\${USER_SERVICE_URL:-http://localhost:8080}
export BIDDING_SERVICE_URL=\${BIDDING_SERVICE_URL:-http://localhost:8080}
export AUTH_SERVICE_URL=\${AUTH_SERVICE_URL:-http://localhost:8080}
export CATALOG_SERVICE_URL=\${CATALOG_SERVICE_URL:-http://localhost:8080}

echo "Substituting environment variables in nginx config..."
echo "AUCTION_SERVICE_URL: \$AUCTION_SERVICE_URL"
echo "USER_SERVICE_URL: \$USER_SERVICE_URL"
echo "BIDDING_SERVICE_URL: \$BIDDING_SERVICE_URL"
echo "AUTH_SERVICE_URL: \$AUTH_SERVICE_URL"
echo "CATALOG_SERVICE_URL: \$CATALOG_SERVICE_URL"

# The nginx:alpine image automatically processes templates in /etc/nginx/templates/
# and creates config files in /etc/nginx/conf.d/ with envsubst
EOF

RUN chmod +x /docker-entrypoint.d/40-substitute-env.sh

EXPOSE 80

ENTRYPOINT ["nginx", "-g", "daemon off;"]