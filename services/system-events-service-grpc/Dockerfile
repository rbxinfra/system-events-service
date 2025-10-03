# Base Image: net8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0.1-jammy

WORKDIR /app
COPY . /app

RUN openssl req -new -x509 -nodes -days 365 -newkey rsa:4096 -subj "/CN=system-events-service" \ 
        -keyout ./grpc.key -out ./grpc.crt && \
    openssl pkcs12 -export -nodes -out grpc.pfx -inkey grpc.key \
        -in grpc.crt \
        -passout pass:

CMD ["dotnet", "/app/Roblox.SystemEvents.Service.dll"]