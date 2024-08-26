# Use uma imagem base do .NET 8 SDK para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Defina o diretório de trabalho no container
WORKDIR /app

# Copie os arquivos de projeto para o container
COPY *.csproj ./
RUN dotnet restore

# Copie o restante do código para o container
COPY . ./

# Compile o aplicativo
RUN dotnet publish -c Release -o out

# Use uma imagem base do .NET 8 Runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Defina o diretório de trabalho no container
WORKDIR /app

# Copie os arquivos publicados para o diretório de trabalho
COPY --from=build /app/out ./

# Defina a porta que o aplicativo vai usar
EXPOSE 80

# Defina o comando de entrada para rodar a aplicação
ENTRYPOINT ["dotnet", "YourAppName.dll"]
