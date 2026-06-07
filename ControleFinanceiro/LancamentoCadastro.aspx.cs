using ControleFinanceiro.Business.Services;
using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Domain.Enums;
using System;


namespace ControleFinanceiro
{
    public partial class LancamentoCadastro : System.Web.UI.Page
    {
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Lancamento lancamento = new Lancamento();

                lancamento.Descricao = txtDescricao.Text.Trim();

                if (!Enum.TryParse(ddlTipo.SelectedValue,out TipoLancamento tipo))
                {
                    throw new Exception("Selecione um tipo de lançamento.");
                }

                lancamento.Tipo = tipo;

                decimal valorOriginal;

                if (!decimal.TryParse(txtValorOriginal.Text, out valorOriginal))
                {
                    throw new Exception("O valor Original precisa ser em números.");
                }

                lancamento.ValorOriginal = valorOriginal;

                lancamento.PercentualTaxa = null;

                if (!string.IsNullOrWhiteSpace(txtPercentualTaxa.Text))
                {
                    decimal taxa;

                    if (!decimal.TryParse(txtPercentualTaxa.Text, out taxa))
                    {
                        throw new Exception("O valor da Taxa precisa ser em números.");
                    }

                    lancamento.PercentualTaxa = taxa;
                }

                lancamento.PercentualDesconto = null;

                if (!string.IsNullOrWhiteSpace(txtPercentualDesconto.Text))
                {
                    decimal desconto;

                    if (!decimal.TryParse(txtPercentualDesconto.Text, out desconto))
                    {
                        throw new Exception("O valor do Desconto precisa ser em números.");
                    }

                    lancamento.PercentualDesconto = desconto;
                }

                lancamento.DataLancamento = DateTime.Parse(txtDataLancamento.Text);

                lancamento.Competencia = txtCompetencia.Text.Trim();

                LancamentoService service = new LancamentoService();

                service.Salvar(lancamento);

                lblMensagem.Text = "Lancamento realizado com sucesso";
            }
            catch (Exception ex)
            {                
                lblMensagem.Text = ex.Message;               
            }
        }
    }
}