# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Set the working directory
WORKDIR /app

# Copy the project file and restore dependencies
COPY ./api.csproj ./
RUN dotnet restore ./api.csproj

# Copy the rest of the application code
COPY . ./

# Publish the application
RUN dotnet publish ./api.csproj -c Release -o /app/publish

# Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the build output
COPY --from=build /app/publish .

# Run the application
ENTRYPOINT ["dotnet", "api.dll"]
