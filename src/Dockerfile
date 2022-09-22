FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
    
# Copy csproj and restore as distinct layers
COPY "OpenFibu.API/*.csproj" "./src/OpenFibu.API"
COPY "OpenFibu.Application/*.csproj" "./src/OpenFibu.Application"
COPY "OpenFibu.Data/*.csproj" "./src/OpenFibu.Data"
COPY "OpenFibu.Domain/*.csproj" "./src/OpenFibu.Domain"
COPY "OpenFibu.Shared/*.csproj" "./src/OpenFibu.Shared"
#COPY "../../OpenFibu.sln" "./"

RUN dotnet restore "./src/OpenFibu.API"
COPY . ./src/

RUN dotnet publish ./src/OpenFibu.API/OpenFibu.API.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "OpenFibu.API.dll"]