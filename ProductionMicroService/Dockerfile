#Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /ProductionMicroService

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

#Service
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /ProductionMicroService

COPY --from=build-env /ProductionMicroService/out .

ENTRYPOINT ["dotnet", "ProductionMicroService.dll"]
