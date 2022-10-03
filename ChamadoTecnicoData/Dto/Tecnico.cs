using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadoTecnicoData.Dto
{
    public class Tecnico
    {
        //Propriedades
        public int CodigoTecnico { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }

        public Tecnico(string nome)
        {
            Nome = nome;
        }

    }
}
