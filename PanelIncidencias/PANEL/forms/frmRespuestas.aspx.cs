using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;

namespace PANEL.forms
{
    public partial class frmRespuestas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IncidenciasBE incidenciaBE = new IncidenciasBE();
                incidenciaBE.IdIncidencia = Request.QueryString["IdIncidencia"];

                List<IncidenciasBE> lstIncidencia = IncidenciasBL.ListaIncidencia(incidenciaBE,"Z");

                List<IncidenciasBE> lstInciCompleto = IncidenciasBL.ListaIncidencia(incidenciaBE);
                UsuarioBE usuarioSesion = new UsuarioBE();
                usuarioSesion.IdUsuario = lstInciCompleto[0].IdReceptor.IdUsuario;
                List<UsuarioBE> lstUserSession = UsuarioBL.ListarUsuarios(usuarioSesion);
                Session["Usuario"] = lstUserSession[0];
                txtDestinatario.Text = lstIncidencia[0].IdEmisor.NombreCompleto;
                txtDestinatario.Enabled = false;
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            IncidenciasBE incidenciaPrevia = new IncidenciasBE();
            UsuarioBE usuEmisor = new UsuarioBE();
            UsuarioBE usuReceptor = new UsuarioBE();
           incidenciaPrevia.IdIncidencia = Request.QueryString["IdIncidencia"];

            List<IncidenciasBE> lstIncidenciaPrevia = IncidenciasBL.ListaIncidencia(incidenciaPrevia,"S");

            usuEmisor.IdUsuario = lstIncidenciaPrevia[0].IdReceptor.IdUsuario;
            List<UsuarioBE> lstEmisor = UsuarioBL.ListarUsuarios(usuEmisor);

            usuReceptor.IdUsuario = lstIncidenciaPrevia[0].IdEmisor.IdUsuario;
            List<UsuarioBE> lstReceptor = UsuarioBL.ListarUsuarios(usuReceptor);

            #region Grabar la nueva Incidencia
            IncidenciasBE nuevaIncidencia = new IncidenciasBE();
            nuevaIncidencia.IdIncidencia = GenerarID();
            nuevaIncidencia.Titulo = txtTitulo.Text;
            nuevaIncidencia.IdEmisor.IdUsuario = lstEmisor[0].IdUsuario;
            nuevaIncidencia.IdReceptor.IdUsuario = lstReceptor[0].IdUsuario;
            nuevaIncidencia.Fecha = DateTime.Now;
            nuevaIncidencia.Estado = "P";
            nuevaIncidencia.Descripcion = txtDescripcion.Text;

            IncidenciasBL.InsertarIncidencia(nuevaIncidencia);
            #endregion

            #region Cambiando el estado de la Incidencia Anterior 
            IncidenciasBE inciActualizada = new IncidenciasBE();
            inciActualizada.IdIncidencia = lstIncidenciaPrevia[0].IdIncidencia;
            inciActualizada.Titulo = lstIncidenciaPrevia[0].Titulo;
            inciActualizada.IdEmisor.IdUsuario = lstIncidenciaPrevia[0].IdEmisor.IdUsuario;
            inciActualizada.IdReceptor.IdUsuario = lstIncidenciaPrevia[0].IdReceptor.IdUsuario;
            inciActualizada.Fecha = lstIncidenciaPrevia[0].Fecha;
            inciActualizada.Estado = "H";
            inciActualizada.Descripcion = lstIncidenciaPrevia[0].Descripcion;

            IncidenciasBL.ActualizarIncidencia(inciActualizada);
            #endregion

            #region Asociando las Incidencias
            IncidenciaDetalleBE inciDetalle = new IncidenciaDetalleBE();
            inciDetalle.IdIncidenciaPregunta.IdIncidencia = lstIncidenciaPrevia[0].IdIncidencia;
            inciDetalle.IdIncidenciaRespuesta.IdIncidencia = nuevaIncidencia.IdIncidencia;
            inciDetalle.FechaPregunta = lstIncidenciaPrevia[0].Fecha;
            inciDetalle.FechaRespuesta = nuevaIncidencia.Fecha;

            IncidenciaDetalleBL.InsertarIncidenciaDetalle(inciDetalle);
            #endregion

            //ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "window.opener.location.reload(true);self.close();", true);
        }

        public String GenerarID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString("N").Substring(0, 6);
           
        }
    }
}