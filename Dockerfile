FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
# Copy project files
COPY Trek.sln ./
COPY Trek/Trek.csproj ./Trek/
COPY Graphium/Graphium/Graphium.csproj ./Graphium/Graphium/
# Restore dependencies
RUN dotnet restore
# Copy everything
COPY Trek/ ./Trek/
COPY Graphium/Graphium/ ./Graphium/Graphium/
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Trek.dll"]
