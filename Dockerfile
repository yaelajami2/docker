# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore
# Copy the rest of the application code
COPY . ./

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the build output
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=https://+:443;http://+:80
# Run the application
ENTRYPOINT ["dotnet", "api.dll"]
EXPOSE 80
EXPOSE 443

