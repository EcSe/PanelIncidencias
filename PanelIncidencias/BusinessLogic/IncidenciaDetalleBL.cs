using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntity;
using DataAccess;
using System.Data;
using System.Data.Common;

namespace BusinessLogic
{
    public class IncidenciaDetalleBL
    {
        public static List<IncidenciaDetalleBE> ListaIncidenciaDetalle(IncidenciaDetalleBE incidenciaDetalleBE, String TipoTransaccion = "S")
        {
            List<IncidenciaDetalleBE> lstIncidenciaDetalle = new List<IncidenciaDetalleBE>();
            DBBaseDatos baseDatosDA = new DBBaseDatos();
            baseDatosDA.Configurar();
            baseDatosDA.Conectar();

            try
            {
                baseDatosDA.CrearComando("RMC_INCIDENCIA_DET", CommandType.StoredProcedure);
                baseDatosDA.AsignarParametroCadena("@PCH_TIPO_TRANSACCION", TipoTransaccion, true);

                if (incidenciaDetalleBE.IdIncidenciaPregunta.IdIncidencia.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ID_INCIDENCIA_PREGUNTA", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ID_INCIDENCIA_PREGUNTA", incidenciaDetalleBE.IdIncidenciaPregunta.IdIncidencia, true);

                if (incidenciaDetalleBE.IdIncidenciaRespuesta.IdIncidencia.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ID_INCIDENCIA_RESPUESTA", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ID_INCIDENCIA_RESPUESTA", incidenciaDetalleBE.IdIncidenciaRespuesta.IdIncidencia, true);

                if (incidenciaDetalleBE.FechaPregunta.Equals(Convert.ToDateTime("01/01/0001 00:00:00.00"))) baseDatosDA.AsignarParametroNulo("@PDT_FECHA_PREGUNTA", true);
                else baseDatosDA.AsignarParametroFecha("@PDT_FECHA_PREGUNTA", incidenciaDetalleBE.FechaPregunta, true);

                if (incidenciaDetalleBE.FechaRespuesta.Equals(Convert.ToDateTime("01/01/0001 00:00:00.00"))) baseDatosDA.AsignarParametroNulo("@PDT_FECHA_RESPUESTA", true);
                else baseDatosDA.AsignarParametroFecha("@PDT_FECHA_RESPUESTA", incidenciaDetalleBE.FechaRespuesta, true);

                DbDataReader drDatos = baseDatosDA.EjecutarConsulta();

                while (drDatos.Read())
                {
                    IncidenciaDetalleBE inciDetalle = new IncidenciaDetalleBE();
                    inciDetalle.IdIncidenciaPregunta.IdIncidencia = drDatos.GetString(drDatos.GetOrdinal("VC_ID_INCIDENCIA_PREGUNTA"));
                    inciDetalle.IdIncidenciaRespuesta.IdIncidencia = drDatos.GetString(drDatos.GetOrdinal("VC_ID_INCIDENCIA_RESPUESTA"));
                    inciDetalle.FechaPregunta = drDatos.GetDateTime(drDatos.GetOrdinal("DT_FECHA_PREGUNTA"));
                    inciDetalle.FechaRespuesta = drDatos.GetDateTime(drDatos.GetOrdinal("DT_FECHA_RESPUESTA"));

                    lstIncidenciaDetalle.Add(inciDetalle);
                }
                drDatos.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                baseDatosDA.Desconectar();
                baseDatosDA = null;
            }

            return lstIncidenciaDetalle;
        }

        public static void InsertarIncidenciaDetalle(IncidenciaDetalleBE incidenciaDetalleBE)
        {
            DBBaseDatos baseDatosDA = new DBBaseDatos();
            baseDatosDA.Configurar();
            baseDatosDA.Conectar();

            try
            {
                baseDatosDA.CrearComando("RMC_INCIDENCIA_DET",CommandType.StoredProcedure);
                baseDatosDA.AsignarParametroCadena("@PCH_TIPO_TRANSACCION", "I", true);
                baseDatosDA.AsignarParametroCadena("@PVC_ID_INCIDENCIA_PREGUNTA", incidenciaDetalleBE.IdIncidenciaPregunta.IdIncidencia, true);
                baseDatosDA.AsignarParametroCadena("@PVC_ID_INCIDENCIA_RESPUESTA", incidenciaDetalleBE.IdIncidenciaRespuesta.IdIncidencia, true);
                baseDatosDA.AsignarParametroFecha("@PDT_FECHA_PREGUNTA", incidenciaDetalleBE.FechaPregunta, true);
                baseDatosDA.AsignarParametroFecha("@PDT_FECHA_RESPUESTA", incidenciaDetalleBE.FechaRespuesta, true);

                baseDatosDA.EjecutarComando();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                baseDatosDA.Desconectar();
                baseDatosDA = null;
            }
        }
    }
}
