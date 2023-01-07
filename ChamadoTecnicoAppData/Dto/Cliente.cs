using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Código do cliente")] //Exibição do campo
        public int CodigoCliente { get; set; }

        [Display(Name = "Código do usuário")] //Exibição do campo
        [Required(ErrorMessage = "Peenchimento obrigatório!")]
        public int CodigoUsuario { get; set; }

        [Required(ErrorMessage = "Peenchimento obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "Profissão")] //Exibição do campo
        public string Profissao { get; set; }

        public string Setor { get; set; }

        public Cliente()
        {
        }

    }
}
