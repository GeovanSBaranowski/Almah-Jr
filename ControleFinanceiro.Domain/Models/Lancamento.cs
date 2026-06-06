using ControleFinanceiro.Domain.Enums;
using System;


namespace ControleFinanceiro.Domain.Models
{
    public class Lancamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TipoLancamento Tipo { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal? PercentualTaxa { get; set; }
        public decimal? PercentualDesconto { get; set; }
        public decimal ValorCalculado { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public string Competencia { get; set; }
        public StatusLancamento Status { get; set; }
    }
}
