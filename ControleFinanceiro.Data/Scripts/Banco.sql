CREATE DATABASE ControleFinanceiro;

USE ControleFinanceiro;
GO

CREATE TABLE LancamentoFinanceiro(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Descricao VARCHAR(500) NOT NULL,
	Tipo VARCHAR(10) NOT NULL, -- 'Crédito' ou 'Débito'
	ValorOriginal DECIMAL(15, 2) NOT NULL,
	PercentualTaxa DECIMAL(5, 2) NULL, -- Taxa (aplicável a Débito)
	PercentualDesconto DECIMAL(5, 2) NULL, -- Desconto (aplicável a Crédito)
	ValorCalculado DECIMAL(15, 2) NOT NULL,
	DataLancamento DATETIME NOT NULL,
	DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
	DataPagamento DATETIME NULL,
	DataCancelamento DATETIME NULL,
	Competencia VARCHAR(7) NOT NULL, -- Formato: MM/YYYY
	Status VARCHAR(15) NOT NULL DEFAULT 'Aberto' -- 'Aberto', 'Pago', 'Cancelado'
);
