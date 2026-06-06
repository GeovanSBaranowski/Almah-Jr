using System;
using ControleFinanceiro.Business.Services;
using ControleFinanceiro.Domain.Enums;
using ControleFinanceiro.Domain.Models;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro
{
    public partial class LancamentoListagem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarLancamentos();
            }
        }

        private void CarregarLancamentos()
        {
            LancamentoService service = new LancamentoService();

            gvLancamentos.DataSource = service.BuscarTodos();
            gvLancamentos.DataBind();

            decimal saldo = service.ObterSaldo();

            lblSaldo.Text = saldo.ToString("C");
        }

        protected void gvLancamentos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            LancamentoService service = new LancamentoService();

            if (e.CommandName == "Editar")
            {
                Lancamento lancamento = service.BuscarPorId(id);

                if(lancamento.Status != StatusLancamento.Aberto)
                {
                    lblMensagem.Text = "O titulo precisa estar aberto para edita-lo";
                    return;
                }

                Response.Redirect($"LancamentoEditar.aspx?id={id}");
            }
            else if (e.CommandName == "Pagar")
            {
                service.Pagar(id);
            }
            else if(e.CommandName == "Cancelar")
            {
                service.Cancelar(id);
            }
            CarregarLancamentos();
        }

        protected void btnExportarCsv_Click(object sender, EventArgs e)
        {
            try
            {
                LancamentoService service = new LancamentoService();

                List<Lancamento> lancamentos = service.BuscarPorCompetencia(txtCompetenciaExportacao.Text);

                StringBuilder csv = new StringBuilder();
                csv.AppendLine("Descricao;Tipo;ValorOriginal;ValorCalculado;DataLancamento;Competencia;Status");

                foreach(Lancamento lancamento in lancamentos)
                {
                    csv.AppendLine($"{lancamento.Descricao}" +
                        $"{lancamento.Tipo}" +
                        $"{lancamento.ValorOriginal}" +
                        $"{lancamento.ValorCalculado}" +
                        $"{lancamento.DataLancamento: dd/MM/yyyy}" +
                        $"{lancamento.Competencia}" +
                        $"{lancamento.Status}");
                }

                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment; filename=lancamentos.csv");
                Response.Write(csv.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
    }
}