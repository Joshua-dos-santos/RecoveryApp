#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Recovery_FrontEnd_User/Recovery_FrontEnd_User.csproj", "Recovery_FrontEnd_User/"]
RUN dotnet restore "Recovery_FrontEnd_User/Recovery_FrontEnd_User.csproj"
COPY . .
WORKDIR "/src/Recovery_FrontEnd_User"
RUN dotnet build "Recovery_FrontEnd_User.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recovery_FrontEnd_User.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recovery_FrontEnd_User.dll"]
