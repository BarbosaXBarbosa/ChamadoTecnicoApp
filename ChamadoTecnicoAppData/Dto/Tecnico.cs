using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadoTecnicoAppData.Dto
{
    public class Tecnico
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

        [Display(Name = "Código do Tecnico")] //Exibição do campo
        public int CodigoTecnico { get; set; }

        [Display(Name = "Código do usuário")] //Exibição do campo
        [Required(ErrorMessage = "Peenchimento obrigatório!")]
        public int CodigoUsuario { get; set; }

        [Required(ErrorMessage = "Peenchimento obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "Especialidade")] //Exibição do campo
        public string Especialidade { get; set; }

        public Tecnico()
        {
        }

    }
}

