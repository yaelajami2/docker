# Base image for build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["./WebApplication1.csproj", "WebApplication1/"]
WORKDIR "/src/WebApplication1"
RUN dotnet restore

# Copy the remaining source code and build
COPY . .
RUN dotnet build "WebApplication1.csproj" -c Release -o /app/build
RUN dotnet publish "WebApplication1.csproj" -c Release -o /app/publish

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebApplication1.dll"]






