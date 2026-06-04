using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Data.Repository;
using System;

namespace ControleFinanceiro.Business.Services
{
    public class LancamentoService
    {
        private readonly LancamentoRepository _repository;

        public LancamentoService()
        {
            _repository = new LancamentoRepository();   
        }

        private decimal CacularValorFinal(Lancamento lancamento)
        {
            if(lancamento.Tipo == "Credito")
            {
                return lancamento.ValorOriginal - (lancamento.ValorOriginal * (lancamento.PercentualDesconto ?? 0) / 100);
            }

            else if(lancamento.Tipo == "Debito")
            {
                return lancamento.ValorOriginal + (lancamento.ValorOriginal * (lancamento.PercentualTaxa ?? 0) / 100);
            }

            throw new Exception("Tipo de lancamento invalido");
        }

        private void Validar(Lancamento lancamento)
        {
            if(string.IsNullOrEmpty(lancamento.Descricao))
            {
                throw new Exception("Descricao e obrigatorio");
            }

            if(lancamento.ValorOriginal <= 0)
            {
                throw new Exception("Valor deve ser maior que zero!");
            }

            if(lancamento.Tipo != "Credito" && lancamento.Tipo != "Debito")
            {
                throw new Exception("Tipo de lancamento invalido");
            }

            if(lancamento.Tipo == "Credito" && !lancamento.PercentualDesconto.HasValue)
            {
                throw new Exception("Desconto e obrigatorio para credito");
            }

            if(lancamento.Tipo == "Debito" && !lancamento.PercentualTaxa.HasValue)
            {
                throw new Exception("Taxa e obrigatorio para debito");
            }
        }

        public void Salvar(Lancamento lancamento)
        {
            Validar(lancamento);

            lancamento.ValorCalculado = CacularValorFinal(lancamento);
            lancamento.DataCriacao = DateTime.Now;
            lancamento.Status = "Aberto";

            _repository.Inserir(lancamento);

        }
    }
}
