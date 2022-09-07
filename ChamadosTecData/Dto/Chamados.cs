using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadosTecData.Dto
{
    internal class Chamados
    {
        public int CodigoChamado { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoTecnico  { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Ocorrencia { get; set; }
        public string Problema { get; set; }
        public bool Concluido { get; set; }
    }
}
