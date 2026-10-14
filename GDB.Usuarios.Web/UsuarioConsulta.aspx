<%@ Page Title="Usuario consulta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UsuarioConsulta.aspx.cs" Inherits="GDB.Usuarios.Web.UsuarioConsulta" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="cabecera">
        <div>
            <span class="antetitulo">Gestión de usuarios</span>
            <h1>Usuarios registrados</h1>
            <p class="texto-suave"><asp:Literal ID="litTotal" runat="server" /></p>
        </div>
        <a href="Usuario.aspx" class="boton boton-primario">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" aria-hidden="true"><path d="M12 5v14M5 12h14" /></svg>
            Nuevo usuario
        </a>
    </div>

    <asp:Label ID="lblExito" runat="server" CssClass="alerta alerta-exito" role="status" EnableViewState="false" />
    <asp:Label ID="lblError" runat="server" CssClass="alerta alerta-error" role="alert" EnableViewState="false" />

    <div class="tarjeta tabla-contenedor">
        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="tabla"
            UseAccessibleHeader="true" OnRowCommand="gvUsuarios_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Nombre">
                    <ItemTemplate>
                        <div class="persona">
                            <span class="avatar" aria-hidden="true"><%#: Iniciales(Eval("Nombre")) %></span>
                            <%#: Eval("Nombre") %>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha de nacimiento">
                    <ItemTemplate>
                        <%#: Eval("FechaNacimiento", "{0:dd/MM/yyyy}") %>
                        <span class="texto-suave">&middot; <%#: Edad(Eval("FechaNacimiento")) %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sexo">
                    <ItemTemplate><span class="etiqueta"><%#: NombreSexo(Eval("Sexo")) %></span></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="col-acciones" ItemStyle-CssClass="col-acciones">
                    <ItemTemplate>
                        <a href='<%# "Usuario.aspx?id=" + Eval("Id") %>' class="accion">
                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linejoin="round" aria-hidden="true"><path d="M4 20h4L19 9l-4-4L4 16v4z" /></svg>
                            Modificar
                        </a>
                        <asp:LinkButton runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'
                            CssClass="accion accion-peligro" CausesValidation="false" data-confirmar='<%# Eval("Nombre") %>'>
                            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" aria-hidden="true"><path d="M4 7h16M10 11v6M14 11v6M6 7l1 13h10l1-13M9 7V4h6v3" /></svg>
                            Eliminar
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div class="vacio">
                    <h2>Aún no hay usuarios</h2>
                    <p>Registra el primero y aparecerá en esta lista.</p>
                    <a href="Usuario.aspx" class="boton boton-primario">Registrar usuario</a>
                </div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>

    <dialog id="dialogo-confirmar" class="dialogo" aria-labelledby="dialogo-titulo">
        <h2 id="dialogo-titulo">Eliminar usuario</h2>
        <p>Se eliminará a <strong data-nombre></strong>. Esta acción no se puede deshacer.</p>
        <div class="acciones">
            <button type="button" class="boton boton-secundario" data-respuesta="no">Cancelar</button>
            <button type="button" class="boton boton-peligro" data-respuesta="si">Eliminar</button>
        </div>
    </dialog>
</asp:Content>
