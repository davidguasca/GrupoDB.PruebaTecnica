using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using GDB.Usuarios.Web.Servicios;

namespace GDB.Usuarios.Web
{
    /// <summary>Listado de usuarios: muestra la grilla y atiende las acciones de cada fila.</summary>
    public partial class UsuarioConsulta : System.Web.UI.Page
    {
        // Avisos que llegan por ?aviso= tras guardar. Solo se muestran textos de este diccionario, nunca texto de la URL.
        private static readonly Dictionary<string, string> Avisos = new Dictionary<string, string>
        {
            ["creado"] = "Usuario registrado correctamente.",
            ["actualizado"] = "Los cambios se guardaron correctamente."
        };

        /// <summary>En la primera visita muestra el aviso pendiente y carga la grilla.</summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (Avisos.TryGetValue(Request.QueryString["aviso"] ?? "", out string aviso))
                lblExito.Text = aviso;
            CargarGrilla();
        }

        /// <summary>Elimina el usuario de la fila (ya confirmado en el navegador) y recarga la grilla.</summary>
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Eliminar") return;

            int id = Convert.ToInt32(e.CommandArgument);
            ServicioUsuarios.Intentar(() =>
            {
                if (ServicioUsuarios.Ejecutar(s => s.Eliminar(id)))
                    lblExito.Text = "Usuario eliminado.";
            }, lblError);
            CargarGrilla();
        }

        /// <summary>Iniciales del nombre (primera letra de hasta dos palabras) para el avatar.</summary>
        protected static string Iniciales(object nombre) =>
            string.Concat(((string)nombre).Split(' ')
                                          .Select(palabra => palabra.FirstOrDefault(char.IsLetter))
                                          .Where(letra => letra != default(char))
                                          .Take(2)
                                          .Select(char.ToUpper));

        /// <summary>Edad cumplida a hoy, para mostrarla junto a la fecha de nacimiento.</summary>
        protected static string Edad(object fechaNacimiento)
        {
            var fecha = (DateTime)fechaNacimiento;
            int edad = DateTime.Today.Year - fecha.Year;
            if (fecha.Date > DateTime.Today.AddYears(-edad)) edad--;   // aún no cumple años este año
            return edad == 1 ? "1 año" : edad + " años";
        }

        /// <summary>Traduce el código del sexo (M/F) a su nombre.</summary>
        protected static string NombreSexo(object codigo) => (string)codigo == "M" ? "Masculino" : "Femenino";

        /// <summary>Consulta todos los usuarios, los enlaza a la grilla y actualiza el total.</summary>
        private void CargarGrilla()
        {
            ServicioUsuarios.Intentar(() =>
            {
                var usuarios = ServicioUsuarios.Ejecutar(s => s.Consultar(null));
                gvUsuarios.DataSource = usuarios;
                gvUsuarios.DataBind();
                litTotal.Text = usuarios.Count == 1 ? "1 usuario registrado" : usuarios.Count + " usuarios registrados";
            }, lblError);
        }
    }
}
