# Use the SDK image to build the app


# FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
# FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS final
# WORKDIR /app
# COPY ["libs/", "./"]

# ENTRYPOINT ["dotnet", "AttendanceCapture.dll"]


FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
WORKDIR /source
COPY ["AttendanceCapture/AttendanceCapture.csproj", "./AttendanceCapture/"]
RUN dotnet restore "AttendanceCapture/AttendanceCapture.csproj"
COPY ["AttendanceCapture/", "./AttendanceCapture/"]
WORKDIR "/source/AttendanceCapture"
RUN dotnet publish -c Release -o /app --no-restore

# The final image uses an ASP.NET runtime image suitable for Windows.
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS final
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080


ENTRYPOINT ["dotnet", "AttendanceCapture.dll"]












## Use the official image as a parent image
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
## Set the environment to stop producing the duplicate 'System.Runtime.Extensions' assembly warnings.
#ENV NUGET_XMLDOC_MODE skip
#
## Install system dependencies for Emgu CV
#RUN apt-get update && apt-get install -y \
    #libgdiplus \
    #libc6-dev \
    #libgomp1 \
    #libicu-dev \
    #ffmpeg \
    ## Install the available OpenCV libraries (adjust versions as needed)
    #libopencv-core-dev \
    #libopencv-imgproc-dev \
    #libopencv-highgui-dev \
    ## Add additional dependencies as needed
    #&& rm -rf /var/lib/apt/lists/*
#
## Use the SDK image to build the app
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#WORKDIR /src
#COPY ["AttendanceCapture.csproj", "./AttendanceCapture/"]
#RUN dotnet restore "AttendanceCapture/AttendanceCapture.csproj"
#COPY . .
#WORKDIR "/src/AttendanceCapture"
#RUN dotnet build "AttendanceCapture.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "AttendanceCapture.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "AttendanceCapture.dll"]
#
#