using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntity;
using BusinessLogic;

namespace PANEL.master
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    UsuarioBE usuarioBE = (UsuarioBE)Session["Usuario"];
                    lblUsuario.InnerText = usuarioBE.Nombre;
                }
            }
        }

        protected void lnkButtonSalir_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/LOGIN/forms/LoginRimac.aspx",true);
        }
    }
}