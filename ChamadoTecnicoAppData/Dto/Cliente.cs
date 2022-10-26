using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadoTecnicoAppData.Dto
{
    public class Cliente
    {
        //Encapsulamento = jeito padrão
        //Atributo
        //int codigoCliente;
        //public int GetCodigoCliente()
        //{
        //    return codigoCliente;
        //} 
        //public void SetCodigoCliente(int codigo)
        //{
        //    codigoCliente = codigo;
        //}

        //Propriedade = Encapsulamento
        public int CodigoCliente { get; set; }
        public int CodigoUsuario { get; set; }
        public string Nome { get; set; }
        public string Profissao { get; set; }
        public string Setor { get; set; }

        public Cliente()
        {
        }

    }
}
