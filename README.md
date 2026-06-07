Como executar o projeto

Pré-requisitos

- Visual Studio 2022 Community ou superior
- .NET Framework 4.8
- SQL Server Express

Configuração do Banco de Dados

1. Execute o script "Banco.sql".
2. Verifique a string de conexão no arquivo "Web.config".

Executando o Projeto

1. Abra a solução `ControleFinanceiro.sln`.
2. No Gerenciador de Soluções, clique com o botão direito em `Home.aspx`.
3. Selecione **Definir como Página Inicial**.
4. Pressione **F5** para executar.

Funcionalidades Implementadas

- Cadastro de lançamentos
- Edição de lançamentos em aberto
- Pagamento de lançamentos
- Cancelamento de lançamentos
- Controle de saldo
- Validação de duplicidade
- Exportação CSV por competência
- Validação de regras de negócio
- Persistência em SQL Server utilizando ADO.NET

Decisões de implementação

-Adicionado outra biblioteca, chamada de Domain, para armazenar a Model do Objeto Lancamento, pois anteriormente a mesma estava na camada Business e acabei enfrentando
problemas com referencia circular, foi dessa forma que eu consegui resolver, além disso, aproveitei ela para acrescentar Enums e assim deixar o codigo mais assertivo.

-As chamadas seguem o seguinte formato:
	Web -> Business -> Data -> Domain.

	Sendo suas reposabilidades:
		- ControleFinanceiro (Web Forms)
		- ControleFinanceiro.Business (Regras de negócio)
		- ControleFinanceiro.Data (Persistência ADO.NET)
		- ControleFinanceiro.Domain (Entidades e Enums)

-As validações ficaram em suma maioria no Business, onde mesmo as consultas da Data que não retornavam nada apenas apresentam valores falsos para que assim o business lide
com o tratamento, não foi implementado nenhuma regra no banco também(além é lógico dos campos que permitem valores nulos).

-Foram realizados poucos Metodos em JS, sendo os existem mais necessários para um funcionamento didealizado.

-Optei por não dar atenção a aparecencia e responsividade das telas pois esse não era um item solicitado no teste.

Ferramentas

-Visual Studio Professional 2022(foi o unico que consegui utilizar):
-SQL Server Comunnity 2022;
-Chat GPT

