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
    public class UsuarioBL
    {
        public static List<UsuarioBE> ListarUsuarios(UsuarioBE usuarioBE,String TipoTransaccion = "S")
        {
            List<UsuarioBE> lstUsuarios = new List<UsuarioBE>();
            DBBaseDatos baseDatosDA = new DBBaseDatos();
            baseDatosDA.Configurar();
            baseDatosDA.Conectar();
            try
            {
                baseDatosDA.CrearComando("RMC_USUARIOS", CommandType.StoredProcedure);
                baseDatosDA.AsignarParametroCadena("@PCH_TIPO_TRANSACCION",TipoTransaccion, true);
                if (usuarioBE.IdUsuario.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ID_USUARIO", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ID_USUARIO", usuarioBE.IdUsuario, true);
                if (usuarioBE.Nombre.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_NOMBRE", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_NOMBRE", usuarioBE.Nombre, true);
                if (usuarioBE.ApellidoPaterno.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_APELLIDO_PATERNO", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_APELLIDO_PATERNO", usuarioBE.ApellidoPaterno, true);
                if (usuarioBE.ApellidoMaterno.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_APELLIDO_MATERNO", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_APELLIDO_MATERNO", usuarioBE.ApellidoMaterno, true);
                if (usuarioBE.Email.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_EMAIL", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_EMAIL", usuarioBE.Email, true);
                if (usuarioBE.Password.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_PASSWORD", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_PASSWORD", usuarioBE.Password, true);
                if (usuarioBE.Empresa.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_EMPRESA", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_EMPRESA", usuarioBE.Empresa, true);
                if (usuarioBE.Perfil.IdPerfil.Equals("")) baseDatosDA.AsignarParametroNulo("@PVC_ID_PERFIL", true);
                else baseDatosDA.AsignarParametroCadena("@PVC_ID_PERFIL", usuarioBE.Perfil.IdPerfil, true);
                DbDataReader drDatos = baseDatosDA.EjecutarConsulta();

                while (drDatos.Read())
                {
                    UsuarioBE usu = new UsuarioBE();

                    if (TipoTransaccion.Equals("S"))
                    {
                        usu.IdUsuario = drDatos.GetString(drDatos.GetOrdinal("VC_ID_USUARIO"));
                        usu.Nombre = drDatos.GetString(drDatos.GetOrdinal("VC_NOMBRE"));
                        usu.ApellidoPaterno = drDatos.GetString(drDatos.GetOrdinal("VC_APELLIDO_PATERNO"));
                        usu.ApellidoMaterno = drDatos.GetString(drDatos.GetOrdinal("VC_APELLIDO_MATERNO"));
                        usu.Email = drDatos.GetString(drDatos.GetOrdinal("VC_EMAIL"));
                        usu.Password = drDatos.GetString(drDatos.GetOrdinal("VC_PASSWORD"));
                        usu.Empresa = drDatos.GetString(drDatos.GetOrdinal("VC_EMPRESA"));
                        usu.Perfil.IdPerfil = drDatos.GetString(drDatos.GetOrdinal("VC_ID_PERFIL"));
                    }

                    if (TipoTransaccion.Equals("Z"))
                    {

                        usu.IdUsuario = drDatos.GetString(drDatos.GetOrdinal("VC_ID_USUARIO"));
                        usu.NombreCompleto = drDatos.GetString(drDatos.GetOrdinal("NOMBRE"));
                        usu.Email = drDatos.GetString(drDatos.GetOrdinal("VC_EMAIL"));
                        usu.Empresa = drDatos.GetString(drDatos.GetOrdinal("VC_EMPRESA"));
                        usu.Perfil.NombrePerfil = drDatos.GetString(drDatos.GetOrdinal("VC_NOMBRE_PERFIL"));
                    }
                    lstUsuarios.Add(usu);
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

            return lstUsuarios;
        }

        public static void InsertarUsuario(UsuarioBE usuarioBE)
        {
            DBBaseDatos baseDatosDA = new DBBaseDatos();
            baseDatosDA.Configurar();
            baseDatosDA.Conectar();
            try
            {
                baseDatosDA.CrearComando("RMC_USUARIOS", CommandType.StoredProcedure);
                baseDatosDA.AsignarParametroCadena("@PCH_TIPO_TRANSACCION", "I", true);
                baseDatosDA.AsignarParametroCadena("@PVC_ID_USUARIO", usuarioBE.IdUsuario, true);
                baseDatosDA.AsignarParametroCadena("@PVC_NOMBRE", usuarioBE.Nombre, true);
                baseDatosDA.AsignarParametroCadena("@PVC_APELLIDO_PATERNO", usuarioBE.ApellidoPaterno, true);
                baseDatosDA.AsignarParametroCadena("@PVC_APELLIDO_MATERNO", usuarioBE.ApellidoMaterno, true);
                baseDatosDA.AsignarParametroCadena("@PVC_EMAIL", usuarioBE.Email, true);
                baseDatosDA.AsignarParametroCadena("@PVC_PASSWORD", usuarioBE.Password, true);
                baseDatosDA.AsignarParametroCadena("@PVC_EMPRESA", usuarioBE.Empresa, true);
                baseDatosDA.AsignarParametroCadena("@PVC_ID_PERFIL", usuarioBE.Perfil.IdPerfil, true);
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

        public static void ActualizarUsuario(UsuarioBE usuarioBE)
        {
            DBBaseDatos baseDatosDA = new DBBaseDatos();
            baseDatosDA.Configurar();
            baseDatosDA.Conectar();
            try
            {
                baseDatosDA.CrearComando("RMC_USUARIOS", CommandType.StoredProcedure);
                baseDatosDA.AsignarParametroCadena("@PCH_TIPO_TRANSACCION", "U", true);
                baseDatosDA.AsignarParametroCadena("@PVC_ID_USUARIO", usuarioBE.IdUsuario, true);
                baseDatosDA.AsignarParametroCadena("@PVC_NOMBRE", usuarioBE.Nombre, true);
                baseDatosDA.AsignarParametroCadena("@PVC_APELLIDO_PATERNO", usuarioBE.ApellidoPaterno, true);
                baseDatosDA.AsignarParametroCadena("@PVC_APELLIDO_MATERNO", usuarioBE.ApellidoMaterno, true);
                baseDatosDA.AsignarParametroCadena("@PVC_EMAIL", usuarioBE.Email, true);
                baseDatosDA.AsignarParametroCadena("@PVC_PASSWORD", usuarioBE.Password, true);
                baseDatosDA.AsignarParametroCadena("@PVC_EMPRESA", usuarioBE.Empresa, true);
                baseDatosDA.AsignarParametroCadena("@PVC_ID_PERFIL", usuarioBE.Perfil.IdPerfil, true);
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

        public static void EliminarUsuario(UsuarioBE usuarioBE)
        {
            DBBaseDatos baseDatosDA = new DBBaseDatos();
            baseDatosDA.Configurar();
            baseDatosDA.Conectar();
            try
            {
                baseDatosDA.CrearComando("RMC_USUARIOS",CommandType.StoredProcedure);
                baseDatosDA.AsignarParametroCadena("@PCH_TIPO_TRANSACCION","D",true);
                baseDatosDA.AsignarParametroCadena("@PVC_ID_USUARIO", usuarioBE.IdUsuario,true);
                baseDatosDA.EjecutarComando();

                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        //PRUEBA PARA REALIZAR Y LUEGO BORRAR
        //public static void Actualizar(UsuarioBE usuarioBE)
        //{
        //    DBBaseDatos baseDatosDA = new DBBaseDatos();
        //    baseDatosDA.Configurar();
        //    baseDatosDA.Conectar();
        //    try
        //    {
        //        baseDatosDA.CrearComando("RMC_USUARIOS",CommandType.StoredProcedure);
        //        baseDatosDA.AsignarParametroCadena("@PCH_TIPO_TRANSACCION","D",true);
        //        baseDatosDA.AsignarParametroCadena("@PVC_ID_USUARIO",usuarioBE.IdUsuario,true);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}
