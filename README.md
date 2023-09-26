
# pré-requisitos
  - .NET 7 instalado (https://dotnet.microsoft.com/en-us/download/dotnet)
  - Visual Studio ( https://visualstudio.microsoft.com/pt-br/vs/community/ )

# instalação
  - abrir terminal do diretorio onde o Projeto esta.
  - se o dotnet ef não tiver instalado no projeto rodar o comando : `dotnet tool install --global dotnet-ef`
  - Rodar as migrations para criar as tabelas do banco de dados `dotnet ef database update`
  - Compilar o projeto : `dotnet build .`
  - iniciar o projeto : `dotnet run`


# custom
  - caso queria adicionar uma nova migration : `dotnet ef migrations add NomeMigrations`
  - Para enviar ao banco de dados essa nova migration : `dotnet ef database update`