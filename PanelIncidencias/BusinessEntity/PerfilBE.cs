using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
   public class PerfilBE
    {
       public String IdPerfil { get; set; }
       public String NombrePerfil { get; set; }

        public PerfilBE()
        {
            IdPerfil = String.Empty;
            NombrePerfil = String.Empty;
        }
    }
}
