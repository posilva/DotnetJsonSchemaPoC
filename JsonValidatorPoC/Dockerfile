FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JsonValidatorPoC/JsonValidatorPoC.csproj", "JsonValidatorPoC/"]
RUN dotnet restore "JsonValidatorPoC/JsonValidatorPoC.csproj"
COPY . .
WORKDIR "/src/JsonValidatorPoC"
RUN dotnet build "JsonValidatorPoC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JsonValidatorPoC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JsonValidatorPoC.dll"]
