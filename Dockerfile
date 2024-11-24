#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 5000
#EXPOSE 5001
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["ms-aula.csproj", "."]
#RUN dotnet restore "./ms-aula.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "ms-aula.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "ms-aula.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ms-aula.dll"]

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