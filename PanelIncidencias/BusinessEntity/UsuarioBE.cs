using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class UsuarioBE
    {
        public String IdUsuario { get; set; }
        public String NombreCompleto { get; set; }
        public String Nombre { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Empresa { get; set; } 
        public PerfilBE Perfil { get; set; }

        public UsuarioBE()
        {
            IdUsuario = String.Empty;
            Nombre = String.Empty;
            ApellidoPaterno = String.Empty;
            ApellidoMaterno = String.Empty;
            Email = String.Empty;
            Password = String.Empty;
            Empresa = String.Empty;
            Perfil = new PerfilBE();
            NombreCompleto = String.Empty;
        }
    }
}
