#Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /EmployeeMicroservice

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

#Service
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /EmployeeMicroservice

COPY --from=build-env /EmployeeMicroservice/out .

ENTRYPOINT ["dotnet", "EmployeeMicroservice.dll"]
