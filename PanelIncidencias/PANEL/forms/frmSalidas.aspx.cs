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
    public partial class frmSalidas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IncidenciaDetalleBE inciRespuesta = new IncidenciaDetalleBE();
                inciRespuesta.IdIncidenciaRespuesta.IdIncidencia = Request.QueryString["IdInciRespuesta"];

                UsuarioBE usuarioSession = (UsuarioBE)Session["Usuario"];

                List<IncidenciaDetalleBE> lstIncidenciaDetalle = IncidenciaDetalleBL.ListaIncidenciaDetalle(inciRespuesta);
              
                    #region Buscando las incidencias originales

                    IncidenciasBE inciPregunta = new IncidenciasBE();
                inciPregunta.IdIncidencia = lstIncidenciaDetalle[0].IdIncidenciaPregunta.IdIncidencia;

                List<IncidenciasBE> lstInciPregunta = IncidenciasBL.ListaIncidencia(inciPregunta,"Z");
             
                    IncidenciasBE incidenciaRespuesta = new IncidenciasBE();
                    incidenciaRespuesta.IdIncidencia = Request.QueryString["IdInciRespuesta"];
                    List<IncidenciasBE> lstInciRespuesta = IncidenciasBL.ListaIncidencia(incidenciaRespuesta, "Z");
                    #endregion

                    #region Rellenado los txt
                    txtMiNombre.Text = lstInciRespuesta[0].IdEmisor.NombreCompleto;
                    txtRespuesta.Text = lstInciRespuesta[0].Descripcion;
                    txtDestinatario.Text = lstInciPregunta[0].IdEmisor.NombreCompleto;
                    txtPregunta.Text = lstInciPregunta[0].Descripcion;

                    txtMiNombre.Enabled = false;
                    txtDestinatario.Enabled = false;
                    txtPregunta.Enabled = false;
                    txtRespuesta.Enabled = false;
                #endregion
            }
        }
    }
}