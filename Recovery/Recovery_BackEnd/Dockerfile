#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Recovery/Recovery_BackEnd/Recovery_BackEnd.csproj", "Recovery_BackEnd/"]
COPY ["Recovery/Recovery_Models/Recovery_Models.csproj", "Recovery_Models/"]
COPY ["Recovery/Recovery_Backend_Data/Recovery_Backend_Data.csproj", "Recovery_Backend_Data/"]
RUN dotnet restore "Recovery_BackEnd/Recovery_BackEnd.csproj"
COPY . .
WORKDIR "/src/Recovery/Recovery_BackEnd"
RUN dotnet build "Recovery_BackEnd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recovery_BackEnd.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recovery_BackEnd.dll"]
