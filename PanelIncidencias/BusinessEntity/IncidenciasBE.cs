using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class IncidenciasBE
    {
        public String IdIncidencia { get; set; }
        public String Titulo { get; set; }
        public UsuarioBE IdEmisor { get; set; }
        public UsuarioBE IdReceptor { get; set; }
        public DateTime Fecha { get; set; }
        public String Descripcion { get; set; }
        public String Estado { get; set; }
        public Byte[] ValorBinario1 { get; set; }
        public Byte[] ValorBinario2 { get; set; }
        public Byte[] ValorBinario3 { get; set; }
        public Byte[] ValorBinario4 { get; set; }
        public Byte[] ValorBinario5 { get; set; }

        public IncidenciasBE()
        {
            IdIncidencia = String.Empty;
            Titulo = String.Empty;
            IdEmisor = new UsuarioBE();
            IdReceptor = new UsuarioBE();
            //faltaria fecha para el constructor
            Descripcion = String.Empty;
            Estado = String.Empty;
        }



    }
}
