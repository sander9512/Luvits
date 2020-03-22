# Base SDK Image From Microsoft
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy CSProj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy project files and build release
COPY . ./
RUN dotnet publish -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Luvits.MultimediaManagementAPI.dll"]