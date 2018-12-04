<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginRimac.aspx.cs" Inherits="LOG.forms.LoginRimac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login RIMAC</title>
    <link rel="stylesheet" href="../Estilos/css/base.css"/>
	<link rel="stylesheet" href="../Estilos/css/skeleton.css"/>
	<link rel="stylesheet" href="../Estilos/css/layout.css"/>
</head>
<body>
    <div class="container">
        <div class="form-bg">
    <form id="form1" runat="server">
    <h2>Inicio de Sesion</h2>
				<%--<p><input type="text" placeholder="Usuario" id="txtUsuario" runat="server"/></p>
				<p><input type="password" placeholder="Password" id="txtPassword" runat="server"/></p>--%>
        <p><asp:TextBox ID="txtUsuario" type="text" placeholder ="Usuario" runat="server"></asp:TextBox></p>
        <p><asp:TextBox ID="txtPassword" type="password" placeholder ="Contraseña" runat="server"></asp:TextBox></p>
				<label for="remember">
				  <input type="checkbox" id="remember" value="remember" />
				  <span>Remember me on this computer</span>
				</label>
                <asp:Button id="btnLogin" CssClass="button" runat="server" OnClick="txtLogin_Click"/>
           </form>     
        </div>
     </div>
      <script src="../js/jquery-1.5.1.min.js"></script>
	  <script src="../js/app.js"></script>
</body> 
</html>
