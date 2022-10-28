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
    public class UsuarioDao : BdSqlServerDao
    {
        public int IncluiUsuario(Usuario usuario)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Insert Into " +
                "Usuarios(Email, Senha, Perfil) " +
                "Values(@Email, @Senha, @Perfil) " +
                "Select SCOPE_IDENTITY();";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do usuario
            comando.Parameters.AddWithValue("@Email", usuario.Email);
            comando.Parameters.AddWithValue("@Senha", usuario.Senha);
            comando.Parameters.AddWithValue("@Perfil", usuario.Perfil);
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

        public void AlteraUsuario(Usuario usuario)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Update Usuarios Set " +
                "Email = @Email, Senha = @Senha, Perfil = @Perfil " +
                "Where CodigoUsuario = @CodigoUsuario;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do usuario
            comando.Parameters.AddWithValue("@CodigoUsuario", usuario.CodigoUsuario);
            comando.Parameters.AddWithValue("@Email", usuario.Email);
            comando.Parameters.AddWithValue("@Senha", usuario.Senha);
            comando.Parameters.AddWithValue("@Perfil", usuario.Perfil);
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

        public void ExcluiUsuario(int codigoUsuario)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Delete From Usuarios " +
                "Where CodigoUsuario = @CodigoUsuario;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do usuario
            comando.Parameters.AddWithValue("@CodigoUsuario", codigoUsuario);

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

        public Usuario ObtemUsuario(int codigoUsuario)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Select * From Usuarios Where CodigoUsuario=@CodigoUsuario;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do usuario
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
                    Usuario usuario = new Usuario();
                    usuario.CodigoUsuario = int.Parse(drTabela["CodigoUsuario"].ToString());
                    usuario.Email = drTabela["Email"].ToString();
                    usuario.Senha = drTabela["Senha"].ToString();
                    usuario.Perfil = drTabela["Perfil"].ToString();

                    return usuario;
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

        public Usuario ObtemUsuario(string email)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Select * From Usuarios Where Email=@Email;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do usuario
            comando.Parameters.AddWithValue("@Email", email);

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
                    Usuario usuario = new Usuario();
                    usuario.CodigoUsuario = int.Parse(drTabela["CodigoUsuario"].ToString());
                    usuario.Email = drTabela["Email"].ToString();
                    usuario.Senha = drTabela["Senha"].ToString();
                    usuario.Perfil = drTabela["Perfil"].ToString();

                    return usuario;
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

        public Usuario ObtemUsuario(string email, string senha)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o camando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Select * From Usuarios Where Email=@Email and Senha=@Senha;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do usuario
            comando.Parameters.AddWithValue("@Email", email);
            comando.Parameters.AddWithValue("@Senha", senha);

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
                    Usuario usuario = new Usuario();
                    usuario.CodigoUsuario = int.Parse(drTabela["CodigoUsuario"].ToString());
                    usuario.Email = drTabela["Email"].ToString();
                    usuario.Senha = drTabela["Senha"].ToString();
                    usuario.Perfil = drTabela["Perfil"].ToString();

                    return usuario;
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

        public DataSet BuscaUsuario(string pesquisa)
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o comando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Select * From Usuarios Where Email Like +'%'+ @Pesquisa +'%' Order By Nome;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Preencher o os parametros do B.D com as informações do cliente
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
                //Fazer a consulta no B.D e preecher o dataset com os dados da tabela Cliente
                adaptador.Fill(dsTabela, "Usuarios");
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
        public IEnumerable<Usuario> ListaUsuario()
        {
            //Instanciar a conexão
            SqlConnection conexao = new SqlConnection();
            //Configurar a conexão
            conexao.ConnectionString = conexaoSqlServer;

            //Instanciar o comando
            SqlCommand comando = new SqlCommand();
            //Criar a instrução sql
            string sql = "Select * From Usuarios;";
            //Setar a instrução sql no comando
            comando.CommandText = sql;
            //Setar o tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            //Setar a execucação do comando na conexao com o B.D
            comando.Connection = conexao;
            //Tratamento de erro para execução do comando
            try
            {
                //Abrir a conexão com B.D
                conexao.Open();
                //Fazer leitura dos dados
                SqlDataReader leitor = comando.ExecuteReader();
                //Criar a lista que vai guardar os dados
                List<Usuario> lista = new List<Usuario>();
                //Preencher a lista de dados
                while (leitor.Read())
                {
                    //Criar o objeto usuario e preencher com os dados lidos
                    Usuario usuario = new Usuario();
                    usuario.CodigoUsuario = int.Parse(leitor["CodigoUsuario"].ToString()); //Tem que ser igual o do banco
                    usuario.Email = leitor["Email"].ToString();
                    usuario.Senha = leitor["Senha"].ToString();
                    usuario.Perfil = leitor["Perfil"].ToString();
                    //Adicionar o objeto na lista
                    lista.Add(usuario);
                }
                //Retornar a lista de dados preenchida
                return lista;
                

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
