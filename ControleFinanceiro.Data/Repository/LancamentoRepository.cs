using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleFinanceiro.Data.Repository
{
    public class LancamentoRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public LancamentoRepository()
        {
            _connectionFactory = new DbConnectionFactory();
        }

        public List<Lancamento> BuscarTodos()
        {
            List<Lancamento> lancamentos = new List<Lancamento>();

            string sql = @"SELECT * FROM LancamentoFinanceiro";

            using(SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lancamento lancamento = new Lancamento();

                            lancamento.Id = Convert.ToInt32(reader["Id"]);
                            lancamento.Descricao = reader["Descricao"].ToString();
                            lancamento.Tipo = reader["Tipo"].ToString();
                            lancamento.ValorOriginal = Convert.ToDecimal(reader["ValorOriginal"]);
                            lancamento.ValorCalculado = Convert.ToDecimal(reader["ValorCalculado"]);
                            lancamento.DataLancamento = Convert.ToDateTime(reader["DataLancamento"]);
                            lancamento.DataCriacao = Convert.ToDateTime(reader["DataCriacao"]);

                            lancamento.DataPagamento = reader["DataPagamento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataPagamento"]);
                            lancamento.DataCancelamento = reader["DataCancelamento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataCancelamento"]);

                            lancamento.Competencia = reader["Competencia"].ToString();
                            lancamento.Status = reader["Status"].ToString();
                            
                            lancamentos.Add(lancamento);
                        }
                    }
                }
            }
            return lancamentos;
        }

        public void Inserir(Lancamento lancamento)
        {
            string sql = @"
                            INSERT INTO LancamentoFinanceiro
                            (
                                Descricao,
                                Tipo,
                                ValorOriginal,
                                PercentualTaxa,
                                PercentualDesconto,
                                ValorCalculado,
                                DataLancamento,
                                DataCriacao,
                                Competencia,
                                Status
                            )
                            VALUES
                            (
                                @Descricao,
                                @Tipo,
                                @ValorOriginal,
                                @PercentualTaxa,
                                @PercentualDesconto,
                                @ValorCalculado,
                                @DataLancamento,
                                @DataCriacao,
                                @Competencia,
                                @Status
                            )";

            using (SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Descricao", lancamento.Descricao);
                    command.Parameters.AddWithValue("@Tipo", lancamento.Tipo);
                    command.Parameters.AddWithValue("@ValorOriginal", lancamento.ValorOriginal);
                    command.Parameters.AddWithValue("@PercentualTaxa", (object)lancamento.PercentualTaxa ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PercentualDesconto", (object)lancamento.PercentualDesconto ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ValorCalculado", lancamento.ValorCalculado);
                    command.Parameters.AddWithValue("@DataLancamento", lancamento.DataLancamento);
                    command.Parameters.AddWithValue("@DataCriacao", lancamento.DataCriacao);
                    command.Parameters.AddWithValue("@Competencia", lancamento.Competencia);
                    command.Parameters.AddWithValue("@Status", lancamento.Status);

                    command.ExecuteNonQuery();
                }
            }
        }

        public decimal ObterSaldo()
        {
            string sql = @"SELECT 
                            ISNULL(SUM(
                                CASE 
                                 WHEN Tipo = 'Credito' THEN ValorCalculado
                                 WHEN Tipo = 'Debito' THEN -ValorCalculado
                                 ELSE 0
                                END
                            ), 0)
                        FROM LancamentoFinanceiro
                        WHERE Status = 'Pago'";

            using(SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    return Convert.ToDecimal(command.ExecuteScalar());
                }
            }
        }
    }
}
