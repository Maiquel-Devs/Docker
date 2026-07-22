# 1ª ETAPA: COMPILAÇÃO (Build)

# Baixa a imagem completa do SDK do .NET 10 (contém o compilador e ferramentas)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Define a pasta de trabalho dentro do container onde o código será processado
WORKDIR /src

# Copia todos os arquivos do seu computador (projeto C#) para dentro do container
COPY . .

# Baixa os pacotes NuGet necessários para o projeto
RUN dotnet restore

# Compila o projeto em modo Release e joga os arquivos gerados na pasta /app/publish
RUN dotnet publish -c Release -o /app/publish


# 2ª ETAPA: EXECUÇÃO (Runtime)


# Baixa apenas o Runtime do .NET 10 (imagem muito mais leve, só para rodar a app)
FROM mcr.microsoft.com/dotnet/aspnet:10.0

# Define a pasta de trabalho onde a aplicação final vai rodar
WORKDIR /app

# Copia APENAS os arquivos compilados lá da 1ª etapa (build) para esta etapa leve
COPY --from=build /app/publish .

# Define a variável para o servidor C# (Kestrel) escutar na porta 8080
ENV ASPNETCORE_URLS=http://+:8080

# Documenta que o container vai disponibilizar a porta 8080
EXPOSE 8080

# Define o comando principal que liga a sua aplicação quando o container inicia
ENTRYPOINT ["dotnet", "DockerComposeApp.dll"]