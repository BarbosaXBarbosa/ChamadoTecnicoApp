using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadoTecnicoAppData.Dao
{
    public class BdSqlServerDao
    {
        //Configuração do banco de dados
        public readonly string conexaoSqlServer = @"Data Source=localhost\SQLEXPRESS;" +
            " Database=ChamadosTecnicosBd;" +
            " Integrated Security=True";
    }
}
