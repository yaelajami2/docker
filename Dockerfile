
# Base image for build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApplication1.csproj", "WebApplication1/"]
RUN dotnet restore
COPY . .
RUN dotnet build "WebApplication1.csproj" -c Release -o /app/build
# ללא שלב פרסום
#RUN dotnet publish "WebApplication1.csproj" -c Release -o /app/publish

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/build .
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebApplication1.dll"]





