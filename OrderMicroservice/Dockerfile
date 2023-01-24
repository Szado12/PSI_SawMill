#Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /OrderMicroservice

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

#Service
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /OrderMicroservice

COPY --from=build-env /OrderMicroservice/out .

ENTRYPOINT ["dotnet", "OrderMicroservice.dll"]
