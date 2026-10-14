<%@ Page Title="Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Usuario.aspx.cs" Inherits="GDB.Usuarios.Web.Usuario" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <a href="UsuarioConsulta.aspx" class="volver">&larr; Volver a usuarios</a>

    <section class="tarjeta formulario">
        <h1><asp:Literal ID="litTitulo" runat="server" Text="Registrar usuario" /></h1>
        <p class="texto-suave">Todos los campos son obligatorios.</p>

        <asp:Label ID="lblError" runat="server" CssClass="alerta alerta-error" role="alert" EnableViewState="false" />
        <asp:HiddenField ID="hfId" runat="server" />

        <div class="campo">
            <asp:Label runat="server" AssociatedControlID="txtNombre" Text="Nombre completo" />
            <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" CssClass="control" autocomplete="off" placeholder="Ej. Ana María Pérez" />
            <span class="ayuda">Máximo 100 caracteres.</span>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre"
                ErrorMessage="Ingresa el nombre." CssClass="error-campo" Display="Dynamic" />
        </div>

        <div class="campo">
            <asp:Label runat="server" AssociatedControlID="txtFechaNacimiento" Text="Fecha de nacimiento" />
            <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFechaNacimiento"
                ErrorMessage="Selecciona la fecha de nacimiento." CssClass="error-campo" Display="Dynamic" />
        </div>

        <div class="campo">
            <asp:Label runat="server" AssociatedControlID="ddlSexo" Text="Sexo" />
            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="control">
                <asp:ListItem Text="Selecciona una opción" Value="" />
                <asp:ListItem Text="Masculino" Value="M" />
                <asp:ListItem Text="Femenino" Value="F" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSexo" InitialValue=""
                ErrorMessage="Selecciona el sexo." CssClass="error-campo" Display="Dynamic" />
        </div>

        <div class="acciones">
            <a href="UsuarioConsulta.aspx" class="boton boton-secundario">Cancelar</a>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="boton boton-primario" OnClick="btnGuardar_Click" />
        </div>
    </section>
</asp:Content>
