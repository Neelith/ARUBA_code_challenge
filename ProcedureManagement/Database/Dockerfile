# db/Dockerfile
FROM mcr.microsoft.com/mssql/server:2022-latest

# Set environment variables
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=SuperLong@ndSecurePassw0rd

# Copy SQL scripts to the container
COPY Database/Migrations /tmp/Migrations

# Start SQL Server, run migrations, and keep the server running
CMD /bin/bash -c '/opt/mssql/bin/sqlservr & sleep 30s && \
    for f in /tmp/Migrations/*.sql; do \
        /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "$SA_PASSWORD" -i "$f"; \
    done && tail -f /dev/null'
