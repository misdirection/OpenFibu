FROM mcr.microsoft.com/dotnet/sdk:6.0.302-alpine3.16 AS build-env
WORKDIR /app
    
# Copy csproj and restore as distinct layers
COPY [ "OpenFibu.API/*.csproj", "OpenFibu.API/" ]
COPY [ "OpenFibu.Application/*.csproj", "OpenFibu.Application/" ]
COPY [ "OpenFibu.Data/*.csproj", "OpenFibu.Data/" ]
COPY [ "OpenFibu.Domain/*.csproj", "OpenFibu.Domain/" ]
COPY [ "OpenFibu.Shared/*.csproj", "OpenFibu.Shared/" ]

RUN dotnet restore "OpenFibu.API/OpenFibu.API.csproj"
COPY . .

RUN dotnet publish "OpenFibu.API/OpenFibu.API.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0.7-alpine3.16

WORKDIR /app
COPY --from=build-env /app/out .

# Install cultures
RUN apk add --no-cache icu-libs
# Disable the invariant mode
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

EXPOSE 80

ENTRYPOINT ["dotnet", "OpenFibu.API.dll"]