# syntax=docker/dockerfile:1

# Comments are provided throughout this file to help you get started.
# If you need more help, visit the Dockerfile reference guide at
# https://docs.docker.com/engine/reference/builder/

# Create a stage for building the application.
FROM mcr.microsoft.com/dotnet/sdk:6.0-windowsservercore AS build

COPY . /source

WORKDIR /source/MVCStudentsApp

# Build the application.
RUN dotnet publish -c Release -o C:\app

# Create a new stage for running the application that contains the minimal
# runtime dependencies for the application.
FROM mcr.microsoft.com/dotnet/aspnet:6.0-windowsservercore AS final
WORKDIR C:\app
COPY --from=build C:\app .

# Create a non-privileged user that the app will run under.
USER ContainerUser
ENTRYPOINT ["dotnet", "MVCStudentsApp.dll"]
