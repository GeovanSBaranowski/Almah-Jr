using ControleFinanceiro.Business.Services;
using ControleFinanceiro.Domain.Models;
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
                lancamento.Tipo = ddlTipo.SelectedValue;
                lancamento.ValorOriginal = decimal.Parse(txtValorOriginal.Text);

                lancamento.PercentualTaxa = string.IsNullOrWhiteSpace(txtPercentualTaxa.Text) ? (decimal?)null : decimal.Parse(txtPercentualTaxa.Text);

                lancamento.PercentualDesconto = string.IsNullOrWhiteSpace(txtPercentualDesconto.Text) ? (decimal?)null : decimal.Parse(txtPercentualDesconto.Text);

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