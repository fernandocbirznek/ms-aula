# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar todos os arquivos do projeto
COPY . .

# Restaurar depend�ncias e buildar a aplica��o
RUN dotnet restore ms-aula.sln
RUN dotnet publish ms-aula.sln -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copiar os arquivos gerados no build
COPY --from=build /app/out .

# Expor a porta usada pela aplica��o
EXPOSE 5000
EXPOSE 5001

# Comando de inicializa��o
ENTRYPOINT ["dotnet", "ms-aula.dll"]