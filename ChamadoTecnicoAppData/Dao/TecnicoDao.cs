using ChamadoTecnicoAppData.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadoTecnicoAppData.Dao
{
    public class TecnicoDao : BdSqlServerDao
    {
        public int IncluiTecnico(Tecnico tecnico)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Insert Into " +
                "Tecnicos(Nome, Especialidade, CodigoUsuario) " +
                "Values(@Nome, @Especialidade, @CodigoUsuario) " +
                "Select SCOPE_IDENTITY();";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do Tecnico
            comando.Parameters.AddWithValue("@Nome", tecnico.Nome);
            comando.Parameters.AddWithValue("@Especialidade", tecnico.Especialidade);
            comando.Parameters.AddWithValue("@CodigoUsuario", tecnico.CodigoUsuario);
            //Setar a execucação do comando na conexao com o B.D
            comando.Connection = conexao;
            //Tratamento de erro para execução do comando
            try
            {
                //Conectar no B.D
                conexao.Open();
                //Executar o comando no B.D
                int codigo = Convert.ToInt32(comando.ExecuteScalar());
                return codigo;
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

        public bool AlteraTecico(Tecnico tecnico)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Update Tecnicos Set " +
                "Nome = @Nome, Especialidade = @Especialidade" +
                "Where CodigoTecnico = @CodigoTecnico;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do cliente
            comando.Parameters.AddWithValue("@CodigoTecnico", tecnico.CodigoTecnico);
            comando.Parameters.AddWithValue("@Nome", tecnico.Nome);
            comando.Parameters.AddWithValue("@Especialidade", tecnico.Especialidade);
            //Setar a execucação do comando na conexao com o B.D
            comando.Connection = conexao;
            //Tratamento de erro para execução do comando
            try
            {
                //Conectar no B.D
                conexao.Open();
                //Executar o comando no B.D
                comando.ExecuteNonQuery();
                return true;
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

        public void ExcluiTecnico(int codigoTecnico)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Delete From Tecnicos " +
                "Where CodigoTecnico = @CodigoTecnico;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do tecnico
            comando.Parameters.AddWithValue("@CodigoTecnico", codigoTecnico);

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

        public void ExcluiTecnicoPorCodigoTecnico(int codigoTecnico)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Delete From Tecnicos " +
                "Where CodigoTecnico = @CodigoTecnico;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do tecnico
            comando.Parameters.AddWithValue("@CodigoTecnico", codigoTecnico);

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

        public Tecnico ObtemTecnico(int codigoTecnico)
        {

            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Select * From Tecnicos Where CodigoTecnico=@CodigoTecnico;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do cliente
            comando.Parameters.AddWithValue("@CodigoTecnico", codigoTecnico);

            //Setar a execucação do comando na conexao com o B.D
            comando.Connection = conexao;
            //Tratamento de erro para execução do comando
            try
            {
                //Conectar no B.D
                conexao.Open();
                //Ler os dados da tabela do B.D e transferir para a memoria
                SqlDataReader drTabela = comando.ExecuteReader();
                //Verificar se tem dados
                if (drTabela.Read())
                {
                    Tecnico tecnico = new Tecnico();
                    tecnico.CodigoTecnico= int.Parse(drTabela["CodigoTecnico"].ToString());
                    tecnico.Nome = drTabela["Nome"].ToString();
                    tecnico.Especialidade = drTabela["Especialidade"].ToString();

                    return tecnico;
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

        public Tecnico ObtemTecnicoPorUsuario(int codigoUsuario)
        {

            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Select * From Tecnicos Where CodigoUsuario=@CodigoUsuario;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do cliente
            comando.Parameters.AddWithValue("@CodigoUsuario", codigoUsuario);

            //Setar a execucação do comando na conexao com o B.D
            comando.Connection = conexao;
            //Tratamento de erro para execução do comando
            try
            {
                //Conectar no B.D
                conexao.Open();
                //Ler os dados da tabela do B.D e transferir para a memoria
                SqlDataReader drTabela = comando.ExecuteReader();
                //Verificar se tem dados
                if (drTabela.Read())
                {
                    Tecnico tecnico = new Tecnico();
                    tecnico.CodigoTecnico = int.Parse(drTabela["CodigoTecnico"].ToString());
                    tecnico.CodigoUsuario = int.Parse(drTabela["CodigoUsuario"].ToString());
                    tecnico.Nome = drTabela["Nome"].ToString();
                    tecnico.Especialidade = drTabela["Especialidade"].ToString();

                    return tecnico;
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

        public DataSet BuscaTecnico(string pesquisa)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Select * From Tecnicos Where Nome Like +'%'+ @Pesquisa +'%' Order By Nome;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do tecnico
            comando.Parameters.AddWithValue("@Pesquisa", pesquisa);
            //Setar a execucação do comando na conexao com o B.D
            comando.Connection = conexao;
            //Tratamento de erro para execução do comando
            try
            {
                //Abrir a conexão com B.D
                conexao.Open();
                //Criar o dataset 
                DataSet dsTabela = new DataSet();
                //Criar adaptador para preencher o dataset com os dados da tabela
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                //Fazer a consulta no B.D e preecher o dataset com os dados da tabela Tecnicos
                adaptador.Fill(dsTabela, "Tecnicos");
                //Retorna o dataset com os dados da tabela do B.D
                return dsTabela;
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
