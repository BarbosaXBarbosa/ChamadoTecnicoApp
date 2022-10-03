using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamadoTecnicoAppData.Dto
{
    public class Usuario
    {
        //Propriedades
        public int CodigoUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Perfil { get; set; }

        //Metodo construtor 
        public Usuario()
        {

        }
    }
}
