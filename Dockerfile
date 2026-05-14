FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["FactoryDataApi.csproj", "./"]
RUN dotnet restore "FactoryDataApi.csproj"

COPY . .
RUN dotnet build "FactoryDataApi.csproj" -c Release -o /app/build
RUN dotnet publish "FactoryDataApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FactoryDataApi.dll"]
