FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IS.Order.Api/IS.Order.Api.csproj", "IS.Order.Api/"]
COPY ["IS.Order.Application/IS.Order.Application.csproj", "IS.Order.Application/"]
COPY ["IS.Order.Domain/IS.Order.Domain.csproj", "IS.Order.Domain/"]
COPY ["IS.Order.Persistence/IS.Order.Persistence.csproj", "IS.Order.Persistence/"]
RUN dotnet restore "IS.Order.Api/IS.Order.Api.csproj"
COPY . .
WORKDIR "/src/IS.Order.Api"
RUN dotnet build "IS.Order.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IS.Order.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IS.Order.Api.dll"]
