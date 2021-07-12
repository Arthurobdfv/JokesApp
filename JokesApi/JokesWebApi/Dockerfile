#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["JokesWebApi/JokesWebApi.csproj", "JokesWebApi/"]
COPY ["JokesWebApp.Data/JokesWebApp.Data.csproj", "JokesWebApp.Data/"]
COPY ["JokesWebApp.DAL/JokesWebApp.DAL.csproj", "JokesWebApp.DAL/"]
RUN dotnet restore "JokesWebApi/JokesWebApi.csproj"
COPY . .
WORKDIR "/src/JokesWebApi"
RUN dotnet build "JokesWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JokesWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JokesWebApi.dll"]