<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRespuestas.aspx.cs" Inherits="PANEL.forms.frmRespuestas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Ventana Para Respuestas</title>
    <link href="../css/master.css" rel="stylesheet" />
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Content/font-awesome.css" rel="stylesheet" />
</head>
<body>
   <div class="container-fluid bg-light py-3">
    <form id="frmMensaje" runat="server">
    <div class="row">
        <div class="col-sm">
            <div class="form-group">
                <label for="lblDestinatario">Nombre*</label>
                <asp:TextBox ID="txtDestinatario" runat="server" Columns="30" Rows="2" ></asp:TextBox>
<%--                <asp:Label ID="lblDestinatario"  runat="server" BackColor="White" CssClass="mw-100"></asp:Label>--%>
            </div>
        </div>
        <div class="col-sm">
            <div class="form-group">
                <label for="form_titulo">Titulo</label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" ValidateRequestMode="Enabled"></asp:TextBox>
            </div>
        </div>
    </div>
        <div class="clearfix"></div>

        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label for="form_respuesta">Respuesta *</label>
                    <asp:TextBox ID="txtDescripcion" runat="server" Columns="58" TextMode="MultiLine" Rows="6" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-12">
                <asp:Button id="btnEnviar" runat="server" CssClass="btn btn-warning btn-success" Text="Enviar Respuesta" OnClick="btnEnviar_Click"/>
            </div>
        </div>

    </form>
    </div>
</body>
</html>
