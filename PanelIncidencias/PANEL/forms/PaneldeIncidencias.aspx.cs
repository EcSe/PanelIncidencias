using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Text;

namespace PANEL.forms
{
    public partial class PaneldeIncidencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)    
            {
                UsuarioBE usuarioBE = new UsuarioBE();
                usuarioBE.IdUsuario = Request.QueryString["iduser"];

                List<UsuarioBE> lstUsuario = UsuarioBL.ListarUsuarios(usuarioBE);
                if (lstUsuario.Count == 1)
                {
                    Session["Usuario"] = lstUsuario[0];
                    FormsAuthentication.SetAuthCookie(lstUsuario[0].IdUsuario, true);

                    if (lstUsuario[0].Perfil.IdPerfil.Equals("000001"))
                    {
                        liCreacion.Visible = true;
                    }
                    else liCreacion.Visible = false;
                }

                #region Rellenando Incidencias Entrantes
                IncidenciasBE incidenciaBE = new IncidenciasBE();
                incidenciaBE.IdReceptor.IdUsuario = Request.QueryString["iduser"];
                List<IncidenciasBE> lstIncidenciasRecepcionadas = IncidenciasBL.ListaIncidencia(incidenciaBE, "Z");
                Session["lstIncidenciasRecepcionadas"] = lstIncidenciasRecepcionadas;
                if (lstIncidenciasRecepcionadas.Count >= 1)
                {
                    UsuarioBE usuarioEmisor = new UsuarioBE();
                    usuarioEmisor.IdUsuario = lstIncidenciasRecepcionadas[0].IdEmisor.IdUsuario;
                    List<UsuarioBE> lstEmisor = UsuarioBL.ListarUsuarios(usuarioEmisor);
                }
                Session["lstIncidenciasRecepcionadas"] = lstIncidenciasRecepcionadas;
                gvEntrada.DataSource = lstIncidenciasRecepcionadas;
                gvEntrada.DataBind();
                #endregion

                #region Rellenando Incidencias Salientes
                IncidenciasBE IncidenciaEmitida = new IncidenciasBE();
                IncidenciaEmitida.IdEmisor.IdUsuario = Request.QueryString["iduser"];

                List<IncidenciasBE> lstIncidenciasEmitidas = new List<IncidenciasBE>();
                lstIncidenciasEmitidas = IncidenciasBL.ListaIncidencia(IncidenciaEmitida, "Z");
                Session["lstIncidenciasEmitidas"] = lstIncidenciasEmitidas;
                gvSalida.DataSource = lstIncidenciasEmitidas;
                gvSalida.DataBind();
                #endregion

                #region Validando y Rellenando la tabla de Usuarios

                UsuarioBE UsuarioVacio = new UsuarioBE();

                UsuarioVacio.IdUsuario = txtIdUsuario.Text;
                UsuarioVacio.Password = txtPassword.Text;
                UsuarioVacio.Nombre = txtNombre.Text;
                UsuarioVacio.ApellidoPaterno = txtApellidoPaterno.Text;
                UsuarioVacio.ApellidoMaterno = txtApellidoMaterno.Text;
                UsuarioVacio.Email = txtEmail.Text;
                UsuarioVacio.Empresa = txtEmpresa.Text;
                List<UsuarioBE> lstListaUsuarios = new List<UsuarioBE>();
                lstListaUsuarios = UsuarioBL.ListarUsuarios(UsuarioVacio, "Z");
                Session["UsuarioVacio"] = UsuarioVacio;
                gvUsuarios.DataSource = lstListaUsuarios;
                gvUsuarios.DataBind();

                #endregion
            }

            #region Actualizar las gridview en cada postback

            #region Rellenando Incidencias Entrantes
            IncidenciasBE incidenciaRecepcion = new IncidenciasBE();
            incidenciaRecepcion.IdReceptor.IdUsuario = Request.QueryString["iduser"];
            List<IncidenciasBE> lstIncidenciasRecepcion = IncidenciasBL.ListaIncidencia(incidenciaRecepcion, "Z");
            gvEntrada.DataSource = lstIncidenciasRecepcion;
            gvEntrada.DataBind();
            #endregion

            #region Rellenando Incidencias Salientes
            IncidenciasBE IncidenciaEmision = new IncidenciasBE();
            IncidenciaEmision.IdEmisor.IdUsuario = Request.QueryString["iduser"];
            List<IncidenciasBE>  lstIncidenciasEmision = IncidenciasBL.ListaIncidencia(IncidenciaEmision, "Z");
            gvSalida.DataSource = lstIncidenciasEmision;
            gvSalida.DataBind();
            #endregion

            #endregion



        }

        protected void lnkButtonEntrada_Click(object sender, EventArgs e)
        {


            int rowiD = ((GridViewRow)((LinkButton)sender).Parent.Parent).RowIndex;
            String IdIncidencia = gvEntrada.Rows[rowiD].Cells[0].Text;
            String ruta = "window.open('frmRespuestas.aspx?IdIncidencia="+IdIncidencia+"','Mensaje','width=680,height=500,top=150,left=300,scrollbars=no,menubar=no,titlebar=no,status=no,toolbar=no,resizable=no')";
            Response.Write("<script languaje=javascript>" + ruta + "</script>");
        }

        protected void lnkButtonSalida_Click(object sender, EventArgs e)
        {
            int rowiD = ((GridViewRow)((LinkButton)sender).Parent.Parent).RowIndex;
            String IdIncidenciaSalida = gvSalida.Rows[rowiD].Cells[0].Text;

            //Corroborar si hay respuesta a esa incidencia
            IncidenciaDetalleBE inciRespuesta = new IncidenciaDetalleBE();
            inciRespuesta.IdIncidenciaRespuesta.IdIncidencia = IdIncidenciaSalida;

            List<IncidenciaDetalleBE> lstIncidenciaDetalle = IncidenciaDetalleBL.ListaIncidenciaDetalle(inciRespuesta);

            if (lstIncidenciaDetalle.Count >= 1)
            {

                String ruta = "window.open('frmSalidas.aspx?IdInciRespuesta=" + IdIncidenciaSalida + "','Mensaje','width=680,height=500,top=150,left=300,scrollbars=no,menubar=no,titlebar=no,status=no,toolbar=no,resizable=no')";
                Response.Write("<script languaje=javascript>" + ruta + "</script>");
            }
            else
            {
                string script = @"<script type='text/javascript'>
                            alert('Esta incidencia no tiene respuesta');
                                    </script>";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
        }

        protected void gvEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            String IdIncidenciaSalida = gvSalida.Rows[0].Cells[0].Text;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            #region Agregando el nuevo Usuario
            UsuarioBE nuevoUsuario = new UsuarioBE();

            nuevoUsuario.IdUsuario = txtIdUsuario.Text;
            nuevoUsuario.Password = txtPassword.Text;
            nuevoUsuario.Nombre = txtNombre.Text;
            nuevoUsuario.ApellidoPaterno = txtApellidoPaterno.Text;
            nuevoUsuario.ApellidoMaterno = txtApellidoMaterno.Text;
            nuevoUsuario.Email = txtEmail.Text;
            nuevoUsuario.Empresa = txtEmpresa.Text;
            nuevoUsuario.Perfil.IdPerfil = ddlPerfil.SelectedValue;

                UsuarioBL.InsertarUsuario(nuevoUsuario);

            UsuarioBE UsuarioVacio = (UsuarioBE)Session["UsuarioVacio"];

            List<UsuarioBE> lstUsuarios = UsuarioBL.ListarUsuarios(UsuarioVacio, "Z");
            
            gvUsuarios.DataSource = lstUsuarios;
            gvUsuarios.DataBind();
            #endregion

            #region Limpiando los txt
            txtIdUsuario.Text = "";
            txtPassword.Text = "";
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtEmail.Text = "";
            txtEmpresa.Text = "";
           
            #endregion

            
            string message = "El Usuario se Agrego Correctamente.";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
           
            #region Actualizando el  Usuario
            UsuarioBE nuevoUsuario = new UsuarioBE();

            nuevoUsuario.IdUsuario = txtIdUsuario.Text;
            nuevoUsuario.Password = txtPassword.Text;
            nuevoUsuario.Nombre = txtNombre.Text;
            nuevoUsuario.ApellidoPaterno = txtApellidoPaterno.Text;
            nuevoUsuario.ApellidoMaterno = txtApellidoMaterno.Text;
            nuevoUsuario.Email = txtEmail.Text;
            nuevoUsuario.Empresa = txtEmpresa.Text;
            nuevoUsuario.Perfil.IdPerfil = ddlPerfil.SelectedValue;

            UsuarioBL.ActualizarUsuario(nuevoUsuario);

            UsuarioBE UsuarioVacio = (UsuarioBE)Session["UsuarioVacio"];

            List<UsuarioBE> lstUsuarios = UsuarioBL.ListarUsuarios(UsuarioVacio, "Z");

            gvUsuarios.DataSource = lstUsuarios;
            gvUsuarios.DataBind();
            #endregion

            #region Limpiando los txt
            txtIdUsuario.Text = "";
            txtPassword.Text = "";
            txtNombre.Text = "";
            txtApellidoPaterno.Text = "";
            txtApellidoMaterno.Text = "";
            txtEmail.Text = "";
            txtEmpresa.Text = "";

            #endregion


            string message = "El Usuario se Actualizo Correctamente.";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

            btnAgregar.Visible = true;
            btnActualizar.Visible = false;
            txtIdUsuario.Enabled = true;
        }

        protected void lnkbtnEditar_Click(object sender, EventArgs e)
        {
            btnActualizar.Visible = true;
            btnAgregar.Visible = false;
            int rowiD = ((GridViewRow)((LinkButton)sender).Parent.Parent).RowIndex;
            String CodigoUsuario = gvUsuarios.Rows[rowiD].Cells[0].Text;

            UsuarioBE usuarioEdit = new UsuarioBE();
            usuarioEdit.IdUsuario = CodigoUsuario;
            List<UsuarioBE> lstUsuarioEdit = UsuarioBL.ListarUsuarios(usuarioEdit);

            txtIdUsuario.Text = lstUsuarioEdit[0].IdUsuario; txtIdUsuario.Enabled = false;
            txtPassword.Text = lstUsuarioEdit[0].Password;
            txtNombre.Text = lstUsuarioEdit[0].Nombre;
            txtApellidoPaterno.Text = lstUsuarioEdit[0].ApellidoPaterno;
            txtApellidoMaterno.Text = lstUsuarioEdit[0].ApellidoMaterno;
            txtEmail.Text = lstUsuarioEdit[0].Email;
            txtEmpresa.Text = lstUsuarioEdit[0].Empresa;
            ddlPerfil.SelectedValue = lstUsuarioEdit[0].Perfil.IdPerfil;
        }

        protected void lnkbtnEliminar_Click(object sender, EventArgs e)
        {
            int rowiD = ((GridViewRow)((LinkButton)sender).Parent.Parent).RowIndex;
            String CodigoUsuario = gvUsuarios.Rows[rowiD].Cells[0].Text;

            UsuarioBE usuarioDelete = new UsuarioBE();
            usuarioDelete.IdUsuario = CodigoUsuario;
            UsuarioBL.EliminarUsuario(usuarioDelete);

            UsuarioBE UsuarioVacio = (UsuarioBE)Session["UsuarioVacio"];
          List<UsuarioBE>  lstUsuarios = UsuarioBL.ListarUsuarios(UsuarioVacio,"Z");
            gvUsuarios.DataSource = lstUsuarios;
            gvUsuarios.DataBind();
        }

        protected void lnkBtnNuevaInc_Click(object sender, EventArgs e)
        {
            String ruta = "window.open('frmNuevaIncidencia.aspx','Mensaje','width=680,height=500,top=150,left=300,scrollbars=no,menubar=no,titlebar=no,status=no,toolbar=no,resizable=no')";
            Response.Write("<script languaje=javascript>" + ruta + "</script>");

        }


    }
}