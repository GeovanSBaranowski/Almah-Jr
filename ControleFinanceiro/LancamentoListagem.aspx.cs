using System;
using ControleFinanceiro.Business.Services;

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
    }
}