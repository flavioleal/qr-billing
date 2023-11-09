
## QRBilling - Aplicativo de Cobrança

## Introdução

Este é o QRBilling, um aplicativo de cobrança destinado a fins de teste.

## Tecnologias

- Visual Studio 2022
- Angular 16
- .NET 7
- MongoDB
- SEQ/datalust

## Começando

### Banco de dados / Serviço de Logs

Você precisa do Docker para executar o MongoDB e o SEQ/datalust. Certifique-se de tê-lo instalado: [Link para a instalação do Docker](https://docs.docker.com/get-docker/)

1. Clone o repositório e navegue até a pasta `/docker`.
2. Execute o comando abaixo com o Docker em execução:
    ```bash
    docker-compose up -d 
    ```
3. Após a execução, verifique se os containers estão rodando.
#
### Backend 

Certifique-se de ter o SDK .NET 7 instalado na máquina. Você pode instalá-lo aqui: [Link para instalação do .NET 7](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0)
#
### Frontend

Certifique-se de ter o Node instalado na máquina. Você pode instalá-lo aqui: [Link para instalação do Node.JS](https://nodejs.org/en/download)

Além disso, é necessário o AngularCLI na máquina. Você pode instalá-lo assim: [Link para instalação do AngularCLI](https://github.com/angular/angular-cli) versão 16.2.8

Para rodar o projeto, navegue até a pasta `/frontend` e execute os seguintes comandos:

1. Execute o comando abaixo para instalar as bibliotecas:
    ```bash
    npm install
    ```
    Certifique-se de que este comando instala as dependências listadas no arquivo `package.json`.

2. Execute o comando abaixo para rodar o servidor de desenvolvimento:
    ```bash
    npm run start
    ```
    Este comando inicia o servidor de desenvolvimento do Angular.

## Usuários de teste
| Usuário | Senha |
|--|--|
| admin | admin |
| lojista1| lojista1|
| lojista2| lojista2|
| lojista3| lojista3|
