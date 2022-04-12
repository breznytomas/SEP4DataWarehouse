

#Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

#THIS IS FOR LOCAL DOCKERIZATION
# Run the app on container startup
# Use your project name for the second parameter
#ENTRYPOINT [ "dotnet", "SEP4DataWarehouse.dll" ]

#THIS IS FOR HEROKU
CMD ASPNETCORE_URLS=http://*:$PORT dotnet SEP4DataWarehouse.dll

