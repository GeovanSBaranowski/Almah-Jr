using System.Configuration;
using System.Data.SqlClient;


namespace ControleFinanceiro.Data.Connection
{
    public class DbConnectionFactory
    {
        public SqlConnection CreateConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ControleFinanceiroConnection"].ConnectionString;

            return new SqlConnection(connectionString);
        }
    }
}
