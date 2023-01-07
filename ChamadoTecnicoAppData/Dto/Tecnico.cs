using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadoTecnicoAppData.Dto
{
    public class Tecnico
    {

        public int CodigoTecnico { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public int CodigoUsuario { get; set; }
        public Tecnico(string nome)
        {
            Nome = nome;
        }

        public Tecnico()
        {

        }
    }
}
