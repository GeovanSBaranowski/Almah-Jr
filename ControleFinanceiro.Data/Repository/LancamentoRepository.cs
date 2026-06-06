using ControleFinanceiro.Domain.Models;
using ControleFinanceiro.Data.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ControleFinanceiro.Domain.Enums;
using System.Globalization;

namespace ControleFinanceiro.Data.Repository
{
    public class LancamentoRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public LancamentoRepository()
        {
            _connectionFactory = new DbConnectionFactory();
        }

        public bool Duplicado(string competencia, string descricao, TipoLancamento tipo)
        {
            string sql = @"SELECT COUNT(*)
                            FROM LancamentoFinanceiro
                            WHERE Competencia = @Competencia
                            AND Descricao = @Descricao
                            AND Tipo = @Tipo";

            using(SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Competencia", competencia);
                    command.Parameters.AddWithValue("@Descricao", descricao);
                    command.Parameters.AddWithValue("@Tipo", tipo.ToString());

                    int quantidade = Convert.ToInt32(command.ExecuteScalar());

                    return quantidade > 0;

                }
            }
        }

        public bool DuplicadoEditar(int id, string competencia, string descricao, TipoLancamento tipo)
        {
            string sql = @"SELECT COUNT(*)
                           FROM LancamentoFinanceiro
                                WHERE Competencia = @Competencia
                                    AND Descricao = @Descricao
                                    AND Tipo = @Tipo
                                    AND Id <> @Id";

            using(SqlConnection connect = _connectionFactory.CreateConnection())
            {
                connect.Open();

                using(SqlCommand command = new SqlCommand(sql, connect))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Competencia", competencia);
                    command.Parameters.AddWithValue("@Descricao", descricao);
                    command.Parameters.AddWithValue("@Tipo", tipo.ToString());

                    int quantidade = Convert.ToInt32(command.ExecuteScalar());

                    return quantidade > 0;
                }
            }
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
                            lancamento.Tipo = (TipoLancamento)Enum.Parse(typeof(TipoLancamento), reader["Tipo"].ToString());
                            lancamento.ValorOriginal = Convert.ToDecimal(reader["ValorOriginal"]);
                            lancamento.ValorCalculado = Convert.ToDecimal(reader["ValorCalculado"]);
                            lancamento.DataLancamento = Convert.ToDateTime(reader["DataLancamento"]);
                            lancamento.DataCriacao = Convert.ToDateTime(reader["DataCriacao"]);

                            lancamento.DataPagamento = reader["DataPagamento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataPagamento"]);
                            lancamento.DataCancelamento = reader["DataCancelamento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataCancelamento"]);

                            lancamento.Competencia = reader["Competencia"].ToString();
                            lancamento.Status = (StatusLancamento)Enum.Parse(typeof(StatusLancamento), reader["Status"].ToString());
                            
                            lancamentos.Add(lancamento);
                        }
                    }
                }
            }
            return lancamentos;
        }

        public List<Lancamento> BuscarPorCompetencia(string competencia)
        {
            List<Lancamento> lancamentos = new List<Lancamento>();

            string sql = @"SELECT *
                            FROM LancamentoFinanceiro 
                            WHERE Competencia = @Competencia 
                            ORDER BY DataLancamento";

            using(SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Competencia", competencia);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lancamento lancamento = new Lancamento();

                            lancamento.Id = Convert.ToInt32(reader["ID"]);
                            lancamento.Descricao = reader["Descricao"].ToString();
                            lancamento.Tipo = (TipoLancamento)Enum.Parse(typeof(TipoLancamento), reader["Tipo"].ToString());
                            lancamento.ValorOriginal = Convert.ToDecimal(reader["ValorOriginal"]);
                            lancamento.ValorCalculado = Convert.ToDecimal(reader["ValorCalculado"]);
                            lancamento.DataLancamento = Convert.ToDateTime(reader["DataLancamento"]);
                            lancamento.Competencia = reader["Competencia"].ToString();
                            lancamento.Status = (StatusLancamento)Enum.Parse(typeof(StatusLancamento), reader["Status"].ToString());

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
                    command.Parameters.AddWithValue("@Tipo", lancamento.Tipo.ToString());
                    command.Parameters.AddWithValue("@ValorOriginal", lancamento.ValorOriginal);
                    command.Parameters.AddWithValue("@PercentualTaxa", (object)lancamento.PercentualTaxa ?? DBNull.Value);
                    command.Parameters.AddWithValue("@PercentualDesconto", (object)lancamento.PercentualDesconto ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ValorCalculado", lancamento.ValorCalculado);
                    command.Parameters.AddWithValue("@DataLancamento", lancamento.DataLancamento);
                    command.Parameters.AddWithValue("@DataCriacao", lancamento.DataCriacao);
                    command.Parameters.AddWithValue("@Competencia", lancamento.Competencia);
                    command.Parameters.AddWithValue("@Status", lancamento.Status.ToString());

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

        public void Pagar(int id)
        {
            string sql = @"UPDATE LancamentoFinanceiro
                           SET
                                Status = 'Pago',
                                DataPagamento = GETDATE()
                           WHERE Id = @Id
                           AND Status = 'Aberto'";

            using(SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Cancelar(int id)
        {
            string sql = @"UPDATE LancamentoFinanceiro
                           SET
                                Status = 'Cancelado',
                                DataCancelamento = GETDATE()
                           WHERE Id = @Id
                           AND Status = 'Aberto'";

            using(SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }

        }

        public Lancamento BuscarPorId(int id)
        {
            string sql = @"SELECT * FROM LancamentoFinanceiro WHERE Id = @Id";

            using(SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Lancamento lancamento = new Lancamento();

                            lancamento.Id = Convert.ToInt32(reader["ID"]);
                            lancamento.Descricao = reader["Descricao"].ToString();
                            lancamento.Tipo = (TipoLancamento)Enum.Parse(typeof(TipoLancamento), reader["Tipo"].ToString());
                            lancamento.ValorOriginal = Convert.ToDecimal(reader["ValorOriginal"]);

                            lancamento.PercentualTaxa = reader["PercentualTaxa"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["PercentualTaxa"]);
                            lancamento.PercentualDesconto = reader["PercentualDesconto"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(reader["PercentualDesconto"]);

                            lancamento.ValorCalculado = Convert.ToDecimal(reader["ValorCalculado"]);
                            lancamento.DataLancamento = Convert.ToDateTime(reader["DataLancamento"]);
                            lancamento.DataCriacao = Convert.ToDateTime(reader["DataCriacao"]);

                            lancamento.DataPagamento = reader["DataPagamento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataPagamento"]);
                            lancamento.DataCancelamento = reader["DataCancelamento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataCancelamento"]);

                            lancamento.Competencia = reader["Competencia"].ToString();
                            lancamento.Status = (StatusLancamento)Enum.Parse(typeof(StatusLancamento), reader["Status"].ToString());

                            return lancamento;
                        }
                    }
                }
            }

            return null;
        }

        public void Atualizar(Lancamento lancamento)
        {
            string sql = @"UPDATE LancamentoFinanceiro
                           SET
                                Descricao = @Descricao,
                                Tipo = @Tipo,
                                ValorOriginal = @ValorOriginal,
                                PercentualTaxa = @PercentualTaxa,
                                PercentualDesconto = @PercentualDesconto,
                                ValorCalculado = @ValorCalculado,
                                DataLancamento = @DataLancamento,
                                Competencia = @Competencia
                           WHERE Id = @Id
                                AND Status = 'Aberto'";

            using(SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", lancamento.Id);
                    command.Parameters.AddWithValue("@Descricao", lancamento.Descricao);
                    command.Parameters.AddWithValue("@Tipo", lancamento.Tipo.ToString());
                    command.Parameters.AddWithValue("@ValorOriginal", lancamento.ValorOriginal);
                    command.Parameters.AddWithValue("@PercentualTaxa", (object)lancamento.PercentualTaxa ?? DBNull.Value);
                    command.Parameters.AddWithValue("PercentualDesconto", (object)lancamento.PercentualDesconto ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ValorCalculado", lancamento.ValorCalculado);
                    command.Parameters.AddWithValue("@DataLancamento", lancamento.DataLancamento);
                    command.Parameters.AddWithValue("@Competencia", lancamento.Competencia);

                    command.ExecuteNonQuery();

                }
            }
        }
    }
}
