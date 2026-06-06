using System;
using ControleFinanceiro.Domain.Enums;
using ControleFinanceiro.Business.Services;
using ControleFinanceiro.Domain.Models;

namespace ControleFinanceiro
{
    public partial class LancamentoEditar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarLancamento();
            }
        }

        private void CarregarLancamento()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);

            LancamentoService service = new LancamentoService();
            Lancamento lancamento = service.BuscarPorId(id);

            hdnId.Value = lancamento.Id.ToString();
            txtDescricao.Text = lancamento.Descricao;
            ddlTipo.SelectedValue = lancamento.Tipo.ToString();
            txtValorOriginal.Text = lancamento.ValorOriginal.ToString();

            txtPercentualTaxa.Text = lancamento.PercentualTaxa.HasValue ? lancamento.PercentualTaxa.Value.ToString() : string.Empty;
            txtPercentualDesconto.Text = lancamento.PercentualDesconto.HasValue ? lancamento.PercentualDesconto.Value.ToString() : string.Empty;

            txtDataLancamento.Text = lancamento.DataLancamento.ToString("yyyy/MM/dd");

            txtCompetencia.Text = lancamento.Competencia;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Lancamento lancamento = new Lancamento();

                lancamento.Id = Convert.ToInt32(hdnId.Value);
                lancamento.Descricao = txtDescricao.Text.Trim();

                lancamento.Tipo = (TipoLancamento)Enum.Parse(typeof(TipoLancamento), ddlTipo.SelectedValue);

                lancamento.ValorOriginal = decimal.Parse(txtValorOriginal.Text);

                lancamento.PercentualTaxa = string.IsNullOrWhiteSpace(txtPercentualTaxa.Text) ? (decimal?)null : decimal.Parse(txtPercentualTaxa.Text);
                lancamento.PercentualDesconto = string.IsNullOrWhiteSpace(txtPercentualDesconto.Text) ? (decimal?)null : decimal.Parse(txtPercentualDesconto.Text);

                lancamento.DataLancamento = DateTime.Parse(txtDataLancamento.Text);
                lancamento.Competencia = txtCompetencia.Text.Trim();

                LancamentoService service = new LancamentoService();

                service.Atualizar(lancamento);

                lblMensagem.Text = "Atualizado com sucesso";
            } 
            catch(Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
    }
}