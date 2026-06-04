//using ControleFinanceiro.Data.Repository;
using System;
using System.Web.UI;

namespace ControleFinanceiro
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var repository = new LancamentoRepository();

            //var lancamentos = repository.BuscarTodos();

            Response.Write(
                "Quantidade encontrada: {lancamentos.Count}");
        }
    }
}