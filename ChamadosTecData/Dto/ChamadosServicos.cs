using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosTecData.Dto
{
    internal class ChamadosServicos
    {
        public int CodigoChamadoServico { get; set; }
        public int CodigoChamado { get; set; }
        public int CodigoServico { get; set; }
        public int CodigoTecnico { get; set; }
        public int Quantidade { get; set; }
        public float ValorTotal { get; set; }
    }
}
