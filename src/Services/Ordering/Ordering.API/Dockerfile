#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Ordering/Ordering.API/Ordering.API.csproj", "Services/Ordering/Ordering.API/"]
COPY ["Services/Ordering.Infrastructure/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Ordering.Infrastructure/Ordering.Infrastructure/"]
COPY ["Services/Ordering.Application/Ordering.Application/Ordering.Application.csproj", "Services/Ordering.Application/Ordering.Application/"]
COPY ["Services/Ordering.Domian/Ordering.Domian/Ordering.Domian.csproj", "Services/Ordering.Domian/Ordering.Domian/"]
RUN dotnet restore "Services/Ordering/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]