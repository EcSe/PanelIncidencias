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
    public partial class frmNuevaIncidencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            UsuarioBE UsuarioActual = (UsuarioBE)Session["Usuario"];
            IncidenciasBE nuevaIncidencia = new IncidenciasBE();
            nuevaIncidencia.IdIncidencia = GenerarID();
            nuevaIncidencia.Titulo = txtTitulo.Text;
            nuevaIncidencia.IdEmisor.IdUsuario = UsuarioActual.IdUsuario;
            nuevaIncidencia.IdReceptor.IdUsuario = txtDestinatario.Text;
            nuevaIncidencia.Fecha = DateTime.Now;
            nuevaIncidencia.Descripcion = txtDescripcion.Text;
            nuevaIncidencia.Estado = "P";

            IncidenciasBL.InsertarIncidencia(nuevaIncidencia);

            //PaneldeIncidencias panel = new PaneldeIncidencias();
            //GridView gvEntrada = (GridView)panel.Controls[1];
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