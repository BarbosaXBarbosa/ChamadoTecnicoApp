using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ChamadoTecnicoWebApp.Models
{
    public class ClienteViewModel
    {
        [Display(Name = "Código")]
        public int CodigoCliente { get; set; }

        [Display(Name = "Nome")] //Exibição do campo
        [Required(ErrorMessage = "Peenchimento obrigatório!")] //Preenchimento do campo obrigatória
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido!")] //Define o tipo de campo com o formato de dado específico
        public string Nome { get; set; }

        [Display(Name = "Profissão")] //Exibição do campo
        [Required(ErrorMessage = "Peenchimento obrigatório!")] //Preenchimento do campo obrigatória
        public string Profissao { get; set; }

        [Display(Name = "Setor")] //Exibição do campo
        [Required(ErrorMessage = "Peenchimento obrigatório!")] //Preenchimento do campo obrigatória
        public string Setor { get; set; }


        [Display(Name = "Código Usuário")]
        public int CodigoUsuario { get; set; }

        public ClienteViewModel()
        {
            CodigoCliente = 0; // Atribuímos o zero para novos clientes/usuarios
        }
    }
}
