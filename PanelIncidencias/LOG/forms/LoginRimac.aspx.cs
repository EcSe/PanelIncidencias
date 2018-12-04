using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;
using System.Web.Security;

namespace LOGIN.forms
{
    public partial class LoginRimac : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Context.User = null;
            Session.Abandon();
        }

        protected void txtLogin_Click(object sender, EventArgs e)
        {
            UsuarioBE usuario = new UsuarioBE();
            usuario.IdUsuario = txtUsuario.Text;
            usuario.Password = txtPassword.Text.Trim().Equals("") ? "$$$" : txtPassword.Text.Trim();

            List<UsuarioBE> lstUsuarios = UsuarioBL.ListarUsuarios(usuario);

            if (lstUsuarios.Count == 1)
            {
                Session["Usuario"] = lstUsuarios[0];

                FormsAuthentication.SetAuthCookie(lstUsuarios[0].IdUsuario, true);
               // Response.Redirect("/PANEL/forms/PaginaIncidencias.aspx?iduser=" + usuario.IdUsuario + "");
                Response.Redirect("/PANEL/forms/PaneldeIncidencias.aspx?iduser=" + usuario.IdUsuario + "");
            }
            else
            {
                string script = @"<script type='text/javascript'>
                            alert('Usuario y/o Contraseña Incorrectos');
                                    </script>";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
        }
    }
}