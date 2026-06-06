using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Data.Repository;
using System;
using System.Collections.Generic;
using ControleFinanceiro.Domain.Enums;

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
            if(lancamento.Tipo == TipoLancamento.Credito)
            {
                return lancamento.ValorOriginal - (lancamento.ValorOriginal * (lancamento.PercentualDesconto ?? 0) / 100);
            }

            else if(lancamento.Tipo == TipoLancamento.Debito)
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

            if(lancamento.Tipo != TipoLancamento.Credito && lancamento.Tipo != TipoLancamento.Debito)
            {
                throw new Exception("Tipo de lancamento invalido");
            }

            if(lancamento.Tipo == TipoLancamento.Credito && !lancamento.PercentualDesconto.HasValue)
            {
                throw new Exception("Desconto e obrigatorio para credito");
            }

            if(lancamento.Tipo == TipoLancamento.Debito && !lancamento.PercentualTaxa.HasValue)
            {
                throw new Exception("Taxa e obrigatorio para debito");
            }
        }

        public void Salvar(Lancamento lancamento)
        {
            Validar(lancamento);

            if (_repository.Duplicado(lancamento.Competencia, lancamento.Descricao, lancamento.Tipo))
            {
                throw new Exception("Lancamento ja existe");
            }

            lancamento.ValorCalculado = CacularValorFinal(lancamento);
            lancamento.DataCriacao = DateTime.Now;
            lancamento.Status = StatusLancamento.Aberto;

            _repository.Inserir(lancamento);

        }

        public List<Lancamento> BuscarTodos()
        {
            return _repository.BuscarTodos();
        }

        public Lancamento BuscarPorId(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Lancamento nao existe");
            }

            Lancamento lancamento = _repository.BuscarPorId(id);

            if (lancamento == null)
            {
                throw new Exception("Lancamento nao existe");
            }

            return lancamento;
        }

        public List<Lancamento> BuscarPorCompetencia(string competencia)
        {
            if(string.IsNullOrWhiteSpace(competencia)){
                throw new Exception("Informe a competencia");
            }

            return _repository.BuscarPorCompetencia(competencia.Trim());
        }

        public decimal ObterSaldo()
        {
            return _repository.ObterSaldo();
        }

        public void Pagar(int id)
        {
            if(id <= 0)
            {
                throw new Exception("Lancamento nao existe");
            }

            _repository.Pagar(id);
        }

        public void Cancelar(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Lancamento nao existe");
            }

            _repository.Cancelar(id);
        }

        public void Atualizar(Lancamento lancamento)
        {
            Validar(lancamento);

            Lancamento lancamentoBanco = _repository.BuscarPorId(lancamento.Id);

            if(lancamentoBanco == null)
            {
                throw new Exception("Lancamento nao encontrado");
            }

            if(lancamentoBanco.Status != StatusLancamento.Aberto)
            {
                throw new Exception("O titulo precisa estar aberto para ser editado");
            }

            if(_repository.DuplicadoEditar(lancamento.Id, lancamento.Competencia, lancamento.Descricao, lancamento.Tipo))
            {
                throw new Exception("Esse lancamento ja existe");
            }

            lancamento.ValorCalculado = CacularValorFinal(lancamento);

            _repository.Atualizar(lancamento);
        }
    }
}
