using ControleFinanceiro.Data.Repository;
using ControleFinanceiro.Domain.Enums;
using ControleFinanceiro.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            if (!Regex.IsMatch(lancamento.Competencia, @"^(0[1-9]|1[0-2])\/\d{4}$"))
            {
                throw new Exception(
                    "Competência deve estar no formato MM/YYYY.");
            }

            if (string.IsNullOrWhiteSpace(lancamento.Descricao))
            {
                throw new Exception("Descricao e obrigatorio");
            }

            if(lancamento.ValorOriginal <= 0)
            {
                throw new Exception("Valor deve ser maior que zero!");
            }

            if (!Enum.IsDefined(typeof(TipoLancamento), lancamento.Tipo))
            {
                throw new Exception("Tipo de lançamento inválido.");
            }

            if (lancamento.Tipo != TipoLancamento.Credito && lancamento.Tipo != TipoLancamento.Debito)
            {
                throw new Exception("Tipo de lancamento invalido");
            }

            if (lancamento.PercentualDesconto.HasValue &&(lancamento.PercentualDesconto < 0 || lancamento.PercentualDesconto > 100))
            { 
                throw new Exception("Desconto deve estar entre 0 e 100.");
            }

            if (lancamento.PercentualTaxa.HasValue && (lancamento.PercentualTaxa < 0 || lancamento.PercentualTaxa > 100))
            {
                throw new Exception("Taxa deve estar entre 0 e 100.");
            }

            if (lancamento.Tipo == TipoLancamento.Credito)
            {
                if (!lancamento.PercentualDesconto.HasValue)
                    throw new Exception("Desconto é obrigatório para crédito.");

                if (lancamento.PercentualTaxa.HasValue)
                    throw new Exception("Taxa não deve ser informada para crédito.");
            }

            if (lancamento.Tipo == TipoLancamento.Debito)
            {
                if (!lancamento.PercentualTaxa.HasValue)
                    throw new Exception("Taxa é obrigatória para débito.");

                if (lancamento.PercentualDesconto.HasValue)
                    throw new Exception("Desconto não deve ser informado para débito.");
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
            if (!Regex.IsMatch(competencia, @"^(0[1-9]|1[0-2])\/\d{4}$"))
            {
                throw new Exception(
                    "Competência deve estar no formato MM/YYYY.");
            }

            if (string.IsNullOrWhiteSpace(competencia)){
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
