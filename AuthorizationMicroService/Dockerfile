#Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /AuthorizationMicroService

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

#Service
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /AuthorizationMicroService

COPY --from=build-env /AuthorizationMicroService/out .

ENTRYPOINT ["dotnet", "AuthorizationMicroService.dll"]
