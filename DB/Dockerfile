FROM mcr.microsoft.com/mssql/server:2022-latest
WORKDIR /database
COPY . ./

ENV SA_PASSWORD Password!1
ENV ACCEPT_EULA Y

RUN /opt/mssql/bin/sqlservr & (sleep 90 && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P Password!1 -d master -i script.sql)