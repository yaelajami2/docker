# Base image for build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApplication1.csproj", "."]
RUN dotnet restore "WebApplication1.csproj"
COPY . .
RUN dotnet build "WebApplication1.csproj" -c Release -o /app/build
RUN dotnet publish "WebApplication1.csproj" -c Release -o /app/publish

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebApplication1.dll"]


