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
    public class IncidenciasBL
    {
        public static List<IncidenciasBE> ListaIncidencia(IncidenciasBE incidenciaBE,String TipoTransaccion = "S")
        {
            List<IncidenciasBE> lstIncidencia = new List<IncidenciasBE>();
            DBBaseDatos baseDatosDA = new DBBaseDatos();
            baseDatosDA.Configurar();
            baseDatosDA.Conectar();
            try
            {
                baseDatosDA.CrearComando("RMC_INCIDENCIA", CommandType.StoredProcedure);
                baseDatosDA.AsignarParametroCadena("@PCH_TIPO_TRANSACCION", TipoTransaccion, true);
                if (incidenciaBE.IdIncidencia.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ID_INCIDENCIA", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ID_INCIDENCIA", incidenciaBE.IdIncidencia, true);
                if (incidenciaBE.Titulo.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_TITULO", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_TITULO", incidenciaBE.Titulo, true);
                if (incidenciaBE.IdEmisor.IdUsuario.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ID_EMISOR", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ID_EMISOR", incidenciaBE.IdEmisor.IdUsuario, true);
                if (incidenciaBE.IdReceptor.IdUsuario.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ID_RECEPTOR", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ID_RECEPTOR", incidenciaBE.IdReceptor.IdUsuario, true);
                if (incidenciaBE.Fecha.Equals(Convert.ToDateTime("01/01/0001 00:00:00.00"))) baseDatosDA.AsignarParametroNulo("@PDT_FECHA", true);
                else baseDatosDA.AsignarParametroFecha("@PDT_FECHA", incidenciaBE.Fecha, true);
                if (incidenciaBE.Descripcion.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_DESCRIPCION", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_DESCRIPCION", incidenciaBE.Descripcion, true);
                if (incidenciaBE.Estado.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ESTADO", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ESTADO", incidenciaBE.Estado, true);
                
                DbDataReader drDatos = baseDatosDA.EjecutarConsulta();

                while (drDatos.Read())
                {
                    IncidenciasBE inci = new IncidenciasBE();

                    if (TipoTransaccion.Equals("S"))
                    {

                        inci.IdIncidencia = drDatos.GetString(drDatos.GetOrdinal("VC_ID_INCIDENCIA"));
                        inci.Titulo = drDatos.GetString(drDatos.GetOrdinal("VC_TITULO"));
                        inci.IdEmisor.IdUsuario = drDatos.GetString(drDatos.GetOrdinal("VC_ID_EMISOR"));
                        inci.IdReceptor.IdUsuario = drDatos.GetString(drDatos.GetOrdinal("VC_ID_RECEPTOR"));
                        inci.Fecha = drDatos.GetDateTime(drDatos.GetOrdinal("DT_FECHA"));
                        inci.Descripcion = drDatos.GetString(drDatos.GetOrdinal("VC_DESCRIPCION"));
                        inci.Estado = drDatos.GetString(drDatos.GetOrdinal("VC_ESTADO"));
                        if (!drDatos.IsDBNull(drDatos.GetOrdinal(("VB_VALOR_BINARIO_1"))))
                        {
                            inci.ValorBinario1 = (Byte[])drDatos.GetValue(drDatos.GetOrdinal("VB_VALOR_BINARIO_1"));
                        }
                        if (!drDatos.IsDBNull(drDatos.GetOrdinal(("VB_VALOR_BINARIO_2"))))
                        {
                            inci.ValorBinario2 = (Byte[])drDatos.GetValue(drDatos.GetOrdinal("VB_VALOR_BINARIO_2"));
                        }
                        if (!drDatos.IsDBNull(drDatos.GetOrdinal(("VB_VALOR_BINARIO_3"))))
                        {
                            inci.ValorBinario3 = (Byte[])drDatos.GetValue(drDatos.GetOrdinal("VB_VALOR_BINARIO_3"));
                        }
                        if (!drDatos.IsDBNull(drDatos.GetOrdinal(("VB_VALOR_BINARIO_4"))))
                        {
                            inci.ValorBinario4 = (Byte[])drDatos.GetValue(drDatos.GetOrdinal("VB_VALOR_BINARIO_4"));
                        }
                        if (!drDatos.IsDBNull(drDatos.GetOrdinal(("VB_VALOR_BINARIO_5"))))
                        {
                            inci.ValorBinario5 = (Byte[])drDatos.GetValue(drDatos.GetOrdinal("VB_VALOR_BINARIO_5"));
                        }
                    }

                    if (TipoTransaccion.Equals("Z"))
                    {
                        inci.IdIncidencia = drDatos.GetString(drDatos.GetOrdinal("VC_ID_INCIDENCIA"));
                        inci.Estado = drDatos.GetString(drDatos.GetOrdinal("VC_ESTADO"));
                        inci.IdEmisor.NombreCompleto = drDatos.GetString(drDatos.GetOrdinal("NOMBRE"));
                        inci.Titulo = drDatos.GetString(drDatos.GetOrdinal("VC_TITULO"));
                        inci.Descripcion = drDatos.GetString(drDatos.GetOrdinal("VC_DESCRIPCION"));
                        inci.Fecha = drDatos.GetDateTime(drDatos.GetOrdinal("DT_fECHA"));
                    }
                        lstIncidencia.Add(inci);
                    
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
            return lstIncidencia;
        }

        public static void InsertarIncidencia(IncidenciasBE incidenciaBE)
        {
            DBBaseDatos baseDatosDA = new DBBaseDatos();
            baseDatosDA.Configurar();
            baseDatosDA.Conectar();
            try
            {
                baseDatosDA.CrearComando("RMC_INCIDENCIA", CommandType.StoredProcedure);
                baseDatosDA.AsignarParametroCadena("@PCH_TIPO_TRANSACCION", "I", true);
                baseDatosDA.AsignarParametroCadena("@PVC_ID_INCIDENCIA", incidenciaBE.IdIncidencia, true);
                baseDatosDA.AsignarParametroCadena("@PVC_TITULO", incidenciaBE.Titulo, true);
                baseDatosDA.AsignarParametroCadena("@PVC_ID_EMISOR", incidenciaBE.IdEmisor.IdUsuario, true);
                baseDatosDA.AsignarParametroCadena("@PVC_ID_RECEPTOR", incidenciaBE.IdReceptor.IdUsuario, true);
                baseDatosDA.AsignarParametroFecha("@PDT_FECHA", incidenciaBE.Fecha, true);
                baseDatosDA.AsignarParametroCadena("@PVC_DESCRIPCION", incidenciaBE.Descripcion, true);
                baseDatosDA.AsignarParametroCadena("@PVC_ESTADO", incidenciaBE.Estado, true);

                if (incidenciaBE.ValorBinario1 == null ||incidenciaBE.ValorBinario1.Length.Equals(0))
                    baseDatosDA.AsignarParametroNulo("@PVB_VALOR_BINARIO_1", true, ParameterDirection.Input, DbType.Binary);
                else
                    baseDatosDA.AsignarParametroArrayByte("@PVB_VALOR_BINARIO_1", incidenciaBE.ValorBinario1, true, ParameterDirection.Input, DbType.Binary);

                if (incidenciaBE.ValorBinario2 == null || incidenciaBE.ValorBinario2.Length.Equals(0))
                    baseDatosDA.AsignarParametroNulo("@PVB_VALOR_BINARIO_2", true, ParameterDirection.Input, DbType.Binary);
                else
                    baseDatosDA.AsignarParametroArrayByte("@PVB_VALOR_BINARIO_2", incidenciaBE.ValorBinario2, true, ParameterDirection.Input, DbType.Binary);

                if (incidenciaBE.ValorBinario3 == null || incidenciaBE.ValorBinario3.Length.Equals(0))
                    baseDatosDA.AsignarParametroNulo("@PVB_VALOR_BINARIO_3", true, ParameterDirection.Input, DbType.Binary);
                else
                    baseDatosDA.AsignarParametroArrayByte("@PVB_VALOR_BINARIO_3", incidenciaBE.ValorBinario3, true, ParameterDirection.Input, DbType.Binary);

                if (incidenciaBE.ValorBinario4 == null || incidenciaBE.ValorBinario4.Length.Equals(0))
                    baseDatosDA.AsignarParametroNulo("@PVB_VALOR_BINARIO_4", true, ParameterDirection.Input, DbType.Binary);
                else
                    baseDatosDA.AsignarParametroArrayByte("@PVB_VALOR_BINARIO_4", incidenciaBE.ValorBinario4, true, ParameterDirection.Input, DbType.Binary);

                if (incidenciaBE.ValorBinario5 == null || incidenciaBE.ValorBinario5.Length.Equals(0))
                    baseDatosDA.AsignarParametroNulo("@PVB_VALOR_BINARIO_5", true, ParameterDirection.Input, DbType.Binary);
                else
                    baseDatosDA.AsignarParametroArrayByte("@PVB_VALOR_BINARIO_5", incidenciaBE.ValorBinario5, true, ParameterDirection.Input, DbType.Binary);

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

        public static void ActualizarIncidencia(IncidenciasBE incidenciaBE)
        {
            DBBaseDatos baseDatosDA = new DBBaseDatos();
            baseDatosDA.Configurar();
            baseDatosDA.Conectar();
            try
            {
                baseDatosDA.CrearComando("RMC_INCIDENCIA", CommandType.StoredProcedure);
                baseDatosDA.AsignarParametroCadena("@PCH_TIPO_TRANSACCION", "U", true);

                baseDatosDA.AsignarParametroCadena("@PVC_ID_INCIDENCIA", incidenciaBE.IdIncidencia, true);

                if (incidenciaBE.Titulo.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_TITULO", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_TITULO", incidenciaBE.Titulo, true);
                if (incidenciaBE.IdEmisor.IdUsuario.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ID_EMISOR", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ID_EMISOR", incidenciaBE.IdEmisor.IdUsuario, true);
                if (incidenciaBE.IdReceptor.IdUsuario.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ID_RECEPTOR", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ID_RECEPTOR", incidenciaBE.IdReceptor.IdUsuario, true);
                if (incidenciaBE.Fecha.Equals(Convert.ToDateTime("01/01/0001 00:00:00.00"))) baseDatosDA.AsignarParametroNulo("@PDT_FECHA", true);
                else baseDatosDA.AsignarParametroFecha("@PDT_FECHA", incidenciaBE.Fecha, true);
                if (incidenciaBE.Descripcion.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_DESCRIPCION", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_DESCRIPCION", incidenciaBE.Descripcion, true);
                if (incidenciaBE.Estado.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ESTADO", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ESTADO", incidenciaBE.Estado, true);

                if (incidenciaBE.ValorBinario1 == null || incidenciaBE.ValorBinario1.Length.Equals(0))
                    baseDatosDA.AsignarParametroNulo("@PVB_VALOR_BINARIO_1", true, ParameterDirection.Input, DbType.Binary);
                else
                    baseDatosDA.AsignarParametroArrayByte("@PVB_VALOR_BINARIO_1", incidenciaBE.ValorBinario1, true, ParameterDirection.Input, DbType.Binary);

                if (incidenciaBE.ValorBinario2 == null || incidenciaBE.ValorBinario2.Length.Equals(0))
                    baseDatosDA.AsignarParametroNulo("@PVB_VALOR_BINARIO_2", true, ParameterDirection.Input, DbType.Binary);
                else
                    baseDatosDA.AsignarParametroArrayByte("@PVB_VALOR_BINARIO_2", incidenciaBE.ValorBinario2, true, ParameterDirection.Input, DbType.Binary);

                if (incidenciaBE.ValorBinario3 == null || incidenciaBE.ValorBinario3.Length.Equals(0))
                    baseDatosDA.AsignarParametroNulo("@PVB_VALOR_BINARIO_3", true, ParameterDirection.Input, DbType.Binary);
                else
                    baseDatosDA.AsignarParametroArrayByte("@PVB_VALOR_BINARIO_3", incidenciaBE.ValorBinario3, true, ParameterDirection.Input, DbType.Binary);

                if (incidenciaBE.ValorBinario4 == null || incidenciaBE.ValorBinario4.Length.Equals(0))
                    baseDatosDA.AsignarParametroNulo("@PVB_VALOR_BINARIO_4", true, ParameterDirection.Input, DbType.Binary);
                else
                    baseDatosDA.AsignarParametroArrayByte("@PVB_VALOR_BINARIO_4", incidenciaBE.ValorBinario4, true, ParameterDirection.Input, DbType.Binary);

                if (incidenciaBE.ValorBinario5 == null || incidenciaBE.ValorBinario5.Length.Equals(0))
                    baseDatosDA.AsignarParametroNulo("@PVB_VALOR_BINARIO_5", true, ParameterDirection.Input, DbType.Binary);
                else
                    baseDatosDA.AsignarParametroArrayByte("@PVB_VALOR_BINARIO_5", incidenciaBE.ValorBinario5, true, ParameterDirection.Input, DbType.Binary);

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
