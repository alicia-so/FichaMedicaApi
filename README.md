# Tecnologias Utilizadas

 - Backend: Desenvolvido em ASP.NET Core MVC com EF Core.
 - Frontend: Construído com React (Link do repositório: https://github.com/alicia-so/UsuarioApiFront)
 - Testes Unitários: Feito com xUnit para garantir a qualidade do código.

# Sistema de Autenticação
- Implementado com um sistema de autenticação robusto que permite o login e registro de usuários. Os padrões de autenticação JWT (JSON Web Tokens) foram utilizados para garantir a segurança das contas de usuário. Cada usuário possui um Role que determina se ele é um Paciente ou Médico.

# Controle de Acessos
- Pacientes: Têm permissão apenas para visualizar suas próprias fichas médicas, garantindo a privacidade e a confidencialidade dos dados médicos.
- Médicos: Têm a capacidade de criar, editar e apagar fichas médicas, facilitando o gerenciamento dos registros de pacientes.

# Atributos das Fichas Médicas
  As fichas médicas contêm os seguintes atributos essenciais:

- Nome Completo do Paciente: Armazena o nome completo do paciente para identificação precisa.
- Usuário Vinculado: Associa cada ficha a um usuário específico, permitindo o acesso controlado.
- Número de Celular do Paciente: Armazenado no formato E.164 para garantir a consistência e facilitar a comunicação.

# Pré-requisitos
  - .NET 7 instalado (https://dotnet.microsoft.com/en-us/download/dotnet)
  - Visual Studio ( https://visualstudio.microsoft.com/pt-br/vs/community/ )
  - MySQL: Esta aplicação utiliza o banco de dados MySQL para armazenar as informações. Certifique-se de ter o MySQL instalado e configurado em seu sistema. Você pode baixar o MySQL em https://dev.mysql.com/downloads/ e seguir as instruções de instalação.

# Configuração do Banco de Dados
- Antes de executar a aplicação, você precisará criar um banco de dados MySQL e configurar as informações de conexão no arquivo de configuração do ASP.NET Core (geralmente appsettings.json). Certifique-se de que as credenciais do banco de dados, como o nome do banco de dados, nome de usuário e senha, estejam corretamente configuradas.

# Instalação
  - abrir terminal do diretorio onde o Projeto esta.
  - se o dotnet ef não tiver instalado no projeto rodar o comando : `dotnet tool install --global dotnet-ef`
  - Rodar as migrations para criar as tabelas do banco de dados `dotnet ef database update`
  - Compilar o projeto : `dotnet build .`
  - iniciar o projeto : `dotnet run`


# Custom
  - caso queria adicionar uma nova migration : `dotnet ef migrations add NomeMigrations`
  - Para enviar ao banco de dados essa nova migration : `dotnet ef database update`