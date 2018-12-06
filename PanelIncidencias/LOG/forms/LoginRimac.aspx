<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginRimac.aspx.cs" Inherits="LOGIN.forms.LoginRimac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Login RIMAC</title>
    <link href="../UTILITARIOS/Login.css" rel="stylesheet" />
    <link href="../Content/bootstrap-grid.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap-reboot.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/font-awesome.min.css" rel="stylesheet" />
    
</head>
<body class="main">
    <div class="container">
        <div class="form-bg">
            <form id="form1" runat="server">
                <h2>Inicio de Sesion</h2>
                <p>
                    <asp:TextBox ID="txtUsuario" type="text" placeholder="Usuario" runat="server"></asp:TextBox></p>
                <p>
                    <asp:TextBox ID="txtPassword" type="password" placeholder="Contraseña" runat="server"></asp:TextBox></p>
                <label for="remember">
                    <input type="checkbox" id="remember" value="remember" />
                    <span>Remember me on this computer</span>
                </label>
                <asp:Button ID="btnLogin" CssClass="button" runat="server" OnClick="txtLogin_Click" />
            </form>
        </div>
    </div>

<script src="../UTILITARIOS/Login.js"></script>
   
    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Scripts/jquery-3.0.0.slim.min.js"></script>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
   <%-- <script src="../Scripts/popper-utils.min.js"></script>
    <script src="../Scripts/popper.min.js"></script>--%>
</body> 
</html>
