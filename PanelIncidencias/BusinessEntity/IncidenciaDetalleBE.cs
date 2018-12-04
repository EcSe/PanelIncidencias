using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class IncidenciaDetalleBE
    {
        public IncidenciasBE IdIncidenciaPregunta { get; set; }
        public IncidenciasBE IdIncidenciaRespuesta { get; set; }
        public DateTime FechaPregunta { get; set; }
        public DateTime FechaRespuesta { get; set; }

        public IncidenciaDetalleBE()
        {
            IdIncidenciaPregunta = new IncidenciasBE();
            IdIncidenciaRespuesta = new IncidenciasBE();
        }
    }
}
