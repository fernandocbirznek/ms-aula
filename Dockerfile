# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar todos os arquivos do projeto
COPY . .

# Restaurar dependências e buildar a aplicação
RUN dotnet restore ms-aula.sln
RUN dotnet publish ms-aula.sln -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copiar os arquivos gerados no build
COPY --from=build /app/out .

# Expor a porta usada pela aplicação
EXPOSE 5000
EXPOSE 5001

# Comando de inicialização
ENTRYPOINT ["dotnet", "ms-aula.dll"]