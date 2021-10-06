#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Recovery-FrontEnd/Recovery-FrontEnd.csproj", "Recovery-FrontEnd/"]
RUN dotnet restore "Recovery-FrontEnd/Recovery-FrontEnd.csproj"
COPY . .
WORKDIR "/src/Recovery-FrontEnd"
RUN dotnet build "Recovery-FrontEnd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recovery-FrontEnd.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recovery-FrontEnd.dll"]