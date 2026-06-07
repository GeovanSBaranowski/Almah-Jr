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

                if (!Enum.TryParse(ddlTipo.SelectedValue, out TipoLancamento tipo))
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

                if (!DateTime.TryParse(txtDataLancamento.Text, out DateTime dataLancamento))
                {
                    throw new Exception("Informe uma data de lançamento válida.");
                }

                lancamento.DataLancamento = dataLancamento;

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