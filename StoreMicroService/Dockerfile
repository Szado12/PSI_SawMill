#Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /StoreMicroService

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

#Service
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /StoreMicroService

COPY --from=build-env /StoreMicroService/out .

ENTRYPOINT ["dotnet", "StoreMicroService.dll"]
