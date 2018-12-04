<%@ Page Title="" Language="C#" MasterPageFile="~/master/MasterPage.Master" AutoEventWireup="true" CodeBehind="PaneldeIncidencias.aspx.cs" Inherits="PANEL.forms.PaneldeIncidencias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
    <link href="../css/Panel.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="cphContenido" ContentPlaceHolderID="cphContenido" runat="server"> 
    
    <ul class="nav nav-tabs" role="tablist" >
        <li class="nav-item"><a class="nav-link active" href="#tabEntrada" role="tab" data-toggle="tab"><h3>Entrada Incidencias</h3></a></li>
        <li class="nav-item"><a class="nav-link" href="#tabSalida" role="tab" data-toggle="tab"><h3>Salida Incidencias</h3></a></li>
        <li id="liCreacion" runat="server" class="nav-item"><a class="nav-link" href="#tabCreacionUsuario" role="tab" data-toggle="tab"><h3>Creacion de Usuario</h3></a></li>
    </ul>
   <div id="divContent" class="tab-content">
        <div role="tabpanel" class="tab-pane fade show active" id="tabEntrada">  
            <asp:LinkButton ID="lnkBtnNuevaIncEntrada" OnClick="lnkBtnNuevaInc_Click" runat="server"><span class="btn btn-primary">Nueva Incidencia</span></asp:LinkButton>        
            <asp:GridView ID="gvEntrada" runat="server" AutoGenerateColumns="False"  CssClass="table table-bordered table-hover" OnSelectedIndexChanged="gvEntrada_SelectedIndexChanged">  <%--OnRowDataBound="gvEntrada_RowDataBound"--%>
                <Columns>
                    <asp:BoundField  DataField="IdIncidencia" HeaderText="ID" />
                    <asp:BoundField  DataField="Estado" HeaderText="Estado" />
                    <asp:BoundField  DataField="IdEmisor.NombreCompleto" HeaderText="Nombre"/>
                    <asp:BoundField  DataField="Titulo" HeaderText="Titulo"/>
                    <asp:BoundField  DataField="Descripcion" HeaderText="Descripcion"/>
                    <asp:BoundField  DataField="Fecha" HeaderText="Fecha"/>
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkButtonEntrada" runat="server" OnClick="lnkButtonEntrada_Click" >&nbsp;&nbsp;&nbsp; &nbsp;<span class="fa fa-send"></span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>  <%--fin de div role tab panel--%>
       <div role="tabpanel" class="tab-pane fade" id="tabSalida">
<%--           <asp:LinkButton ID="lnkBtnNuevaIncSalida" OnClick="lnkBtnNuevaInc_Click" runat="server"><span class="btn btn-primary">Nueva Incidencia</span></asp:LinkButton>--%>
           <asp:GridView ID="gvSalida" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover">
               <Columns>
                   <asp:BoundField DataField="IdIncidencia" HeaderText="ID" />
                   <asp:BoundField DataField="Estado" HeaderText="Estado" />
                   <asp:BoundField DataField="idEmisor.NombreCompleto" HeaderText="Nombre" />
                   <asp:BoundField DataField="Titulo" HeaderText="Titulo" />
                   <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                   <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                   <asp:TemplateField HeaderText="Acciones">
                       <ItemTemplate>
                           <asp:LinkButton ID="lnkButtonSalida" runat="server" OnClick="lnkButtonSalida_Click">&nbsp;&nbsp;&nbsp; &nbsp; <span class="fa fa-shower"></span></asp:LinkButton>
                       </ItemTemplate>
                   </asp:TemplateField>
               </Columns>
           </asp:GridView>
       </div>
       <div role="tabpanel" class="tab-pane fade" id="tabCreacionUsuario">
           <div class="container">
               <div class="row">
                   <div class="form-group col-sm">
                       <label for="txtIdUsuario" class="badge badge-primary">ID Usuario</label>
                       <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control" ></asp:TextBox>
                   </div>
                   <div class="form-group col-sm">
                       <label for="txtPassword" class="badge badge-primary">Password</label>
                       <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control" ></asp:TextBox>
                   </div>      
                   <div class="form-group col-md-4">
                       <label for="txtNombre" class="badge badge-primary">Nombre</label>
                       <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" ></asp:TextBox>
                   </div>
                   <div class="form-group col-md-4">
                       <label for="txtApellidoPaterno" class="badge badge-primary">Apellido Paterno</label>
                       <asp:TextBox ID="txtApellidoPaterno" runat="server" CssClass="form-control" ></asp:TextBox>
                   </div>
                 </div>
               <div class="row">
                   <div class="form-group col-sm">
                       <label for="txtApellidoMaterno" class="badge badge-primary">Apellido Materno</label>
                       <asp:TextBox ID="txtApellidoMaterno" runat="server" CssClass="form-control" ></asp:TextBox>
                   </div>
                   <div class="form-group col-sm">
                       <label for="txtEmail" class="badge badge-primary">Email</label>
                       <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" CssClass="form-control" ></asp:TextBox>
                   </div>
                   <div class="form-group col-sm">
                       <label for="txtEmpresa" class="badge badge-primary">Empresa</label>
                       <asp:TextBox ID="txtEmpresa" runat="server" CssClass="form-control" ></asp:TextBox>
                   </div>
               </div>
               <div class ="row">
                   <div class="form-group col-sm">
                       <label for="ddlPerfil" class="badge badge-primary">Perfil</label>
                       <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-control col-md-4">
                           <asp:ListItem Enabled="true" Text="Seleccionar Perfil"></asp:ListItem>
                           <asp:ListItem Text="ADMIN" Value="000001"></asp:ListItem>
                           <asp:ListItem Text="USUARIO" Value="000002"></asp:ListItem>
                       </asp:DropDownList>
                   </div>
                   <div class="form-group col-sm">
                       <label for="btnAgregar"> </label>
                       <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-primary" OnClick="btnAgregar_Click" Text="Agregar" />
                   </div>
                   <div class="form-group col-sm">
                       <label for="btnAgregar"> </label>
                       <asp:Button ID="btnActualizar" runat="server" Visible="false" CssClass="btn btn-primary" OnClick="btnActualizar_Click" Text="Actualizar" />
                   </div>
               </div>
               <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns ="false" CssClass="table table-bordered table-hover">
                   <Columns>
                       <asp:BoundField DataField ="IdUsuario" HeaderText="CODIGO"/>
                       <asp:BoundField DataField="NombreCompleto" HeaderText="NOMBRE"/>
                       <asp:BoundField DataField="Email" HeaderText="EMAIL"/>
                       <asp:BoundField DataField="Empresa" HeaderText="EMPRESA"/>
                       <asp:BoundField DataField="Perfil.NombrePerfil" HeaderText="PERFIL"/>
                       <asp:TemplateField HeaderText="Acciones">
                           <ItemTemplate>
                               <asp:LinkButton ID="lnkbtnEditar" runat="server"  OnClick="lnkbtnEditar_Click"><span class="fa fa-edit">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</span></asp:LinkButton>
                               <asp:LinkButton ID="lnkbtnEliminar" runat="server"  OnClick="lnkbtnEliminar_Click"><span class="fa fa-trash"></span></asp:LinkButton>
                           </ItemTemplate>
                       </asp:TemplateField>
                   </Columns>
               </asp:GridView>
        </div>
    </div> <%--fin de tabCreacioUsuario--%>
    </div>  <%--fin de div content--%>
</asp:Content>
<asp:Content ID="cphPie" ContentPlaceHolderID="cphPie" runat="server">
</asp:Content>
