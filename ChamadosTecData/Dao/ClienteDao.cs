using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //Ado.Net SQL Server
using ChamadosTecData.Dto;


namespace ChamadosTecData.Dao
{
    class ClienteDao : BdSqlServerDao
    {
        public void IncluiCliente(Clientes cliente)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Insert Into " +
                "Clientes(Nome, Profissao, Setor) " +
                "Values(@Nome, @Profissao, @Setor);";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do cliente
            comando.Parameters.AddWithValue("@Nome", cliente.Nome);
            comando.Parameters.AddWithValue("@Profissao", cliente.Profissao);
            comando.Parameters.AddWithValue("@Setor", cliente.Setor);
            //Setar a execucação do comando na conexao com o B.D
            comando.Connection = conexao;
            //Tratamento de erro para execução do comando
            try
            {
                //Conectar no B.D
                conexao.Open();
                //Executar o comando no B.D
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                //Desconectar do B.D
                conexao.Close();
            }
        }

        public void AtualizaCliente(Clientes cliente)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Update Clientes Set" +
                "Nome = @Nome, Profissao = @Profissao, Setor = @Setor" +
                "Where CodigoCliente = @CodigoCliente;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do cliente
            comando.Parameters.AddWithValue("@CodigoCliente", cliente.CodigoCliente);
            comando.Parameters.AddWithValue("@Nome", cliente.Nome);
            comando.Parameters.AddWithValue("@Profissao", cliente.Profissao);
            comando.Parameters.AddWithValue("@Setor", cliente.Setor);
            //Setar a execucação do comando na conexao com o B.D
            comando.Connection = conexao;
            //Tratamento de erro para execução do comando
            try
            {
                //Conectar no B.D
                conexao.Open();
                //Executar o comando no B.D
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                //Desconectar do B.D
                conexao.Close();
            }
        }

        public void ExcluiCliente(Clientes cliente)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Delete From Clientes" +
                "Where CodigoCliente = @CodigoCliente;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do cliente
            comando.Parameters.AddWithValue("@CodigoCliente", cliente.CodigoCliente);
            //Setar a execucação do comando na conexao com o B.D
            comando.Connection = conexao;
            //Tratamento de erro para execução do comando
            try
            {
                //Conectar no B.D
                conexao.Open();
                //Executar o comando no B.D
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                //Desconectar do B.D
                conexao.Close();
            }
        }

        public Clientes ObtemCliente(int codigoCliente)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Select * From Clientes" +
                "Where CodigoCliente = @CodigoCliente;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do cliente
            comando.Parameters.AddWithValue("@CodigoCliente", codigoCliente);
            //Setar a execucação do comando na conexao com o B.D
            comando.Connection = conexao;
            //Tratamento de erro para execução do comando

            try
            {
                //Connectar no B.D
                conexao.Open();
                //Lê os dados e transfere para a tabela em memória
                SqlDataReader drTabela = comando.ExecuteReader();
                //Verificar se tem dados
                if (drTabela.Read())
                {
                    Clientes cliente = new Clientes();
                    cliente.CodigoCliente = int.Parse(drTabela["CodigoCliente"].ToString());
                    cliente.Nome = drTabela["Nome"].ToString();
                    cliente.Profissao = drTabela["Profissao"].ToString();
                    cliente.Setor = drTabela["Setor"].ToString();
                    
                    return cliente;
                }

                return null;

            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                //Desconectar do B.D
                conexao.Close();
            }


        }
    }
}
