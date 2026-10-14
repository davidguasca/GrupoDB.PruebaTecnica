using System;
using System.Globalization;
using System.Linq;
using GDB.Usuarios.Web.Servicios;
using Contratos = GDB.Usuarios.Contratos;

namespace GDB.Usuarios.Web
{
    /// <summary>Formulario de usuario: registra uno nuevo o, si recibe ?id=, carga y actualiza el existente.</summary>
    public partial class Usuario : System.Web.UI.Page
    {
        private const string FormatoFecha = "yyyy-MM-dd";   // formato que envía y espera el calendario HTML5

        /// <summary>Limita el calendario a hoy y, en la primera carga, trae el usuario a modificar.</summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFechaNacimiento.Attributes["max"] = DateTime.Today.ToString(FormatoFecha);

            if (!IsPostBack && int.TryParse(Request.QueryString["id"], out int id))
                ServicioUsuarios.Intentar(() => CargarUsuario(id), lblError);
        }

        /// <summary>Crea o actualiza el usuario y redirige al listado, para que recargar la página no repita el envío.</summary>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!IsValid) return;

            var usuario = LeerFormulario();
            bool esNuevo = usuario.Id == 0;
            ServicioUsuarios.Intentar(() =>
            {
                bool guardado = esNuevo
                    ? ServicioUsuarios.Ejecutar(s => s.Agregar(usuario)) > 0
                    : ServicioUsuarios.Ejecutar(s => s.Modificar(usuario));

                if (guardado) Response.Redirect("UsuarioConsulta.aspx?aviso=" + (esNuevo ? "creado" : "actualizado"));
                else lblError.Text = "El usuario ya no existe.";
            }, lblError);
        }

        /// <summary>Llena el formulario con los datos del usuario indicado.</summary>
        private void CargarUsuario(int id)
        {
            var usuario = ServicioUsuarios.Ejecutar(s => s.Consultar(id)).FirstOrDefault();
            if (usuario == null)
            {
                lblError.Text = "El usuario no existe.";
                return;
            }

            hfId.Value = usuario.Id.ToString();
            txtNombre.Text = usuario.Nombre;
            txtFechaNacimiento.Text = usuario.FechaNacimiento.ToString(FormatoFecha);
            ddlSexo.SelectedValue = usuario.Sexo;
            litTitulo.Text = "Modificar usuario";
            btnGuardar.Text = "Guardar cambios";
        }

        /// <summary>Construye el usuario a partir de los campos del formulario.</summary>
        private Contratos.Usuario LeerFormulario() => new Contratos.Usuario
        {
            Id = int.TryParse(hfId.Value, out int id) ? id : 0,
            Nombre = txtNombre.Text.Trim(),
            FechaNacimiento = DateTime.ParseExact(txtFechaNacimiento.Text, FormatoFecha, CultureInfo.InvariantCulture),
            Sexo = ddlSexo.SelectedValue
        };
    }
}
