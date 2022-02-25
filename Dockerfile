# Docker is like a virtual OS and like our CI/CD build agent, we have to specify what it needs to be able to run our application
# From docker instructions grabs images from image registry and use it as our environment for this virutal OS
from mcr.microsoft.com/dotnet/sdk:latest as build

# workdir docker instructions sets our working dicrectory for this virtual OS
workdir /app

# Copy docker instructions lets us copy files from our computer and paste it on the virtual OS
copy *.sln ./
copy APIPortal/*.csproj APIPortal/
copy BL/*.csproj BL/
copy DL/*.csproj DL/
copy Model/*.csproj Model/
copy Test/*.csproj Test/
run dotnet restore

# Copy the rest of our source codes from our projects
copy . ./

#Creating our publish folder by running CLI command
run dotnet publish -c Release -o publish

# After building and publishing our application, we need to set our environment to runtime
from mcr.microsoft.com/dotnet/aspnet:latest as runtime

workdir /app
copy --from=build app/publish ./

# CMD to set that APIPortal.dll assembly will be our entrypoint
cmd ["dotnet", "APIPortal.dll"]

#Expose to port 80
expose 80