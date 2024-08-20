# Use the official .NET image from the Docker Hub
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the code
COPY . ./
RUN dotnet build -c Release -o /app/build

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WebApplication1.dll"]


EXPOSE 80
EXPOSE 443


