using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using GDB.Usuarios.Contratos;

namespace GDB.Usuarios.Servicio.Negocio
{
    /// <summary>Reglas de negocio que debe cumplir un usuario antes de guardarse.</summary>
    public static class UsuarioValidador
    {
        private static readonly string[] SexosValidos = { "M", "F" };

        /// <summary>Lanza una FaultException con todos los errores encontrados.</summary>
        public static void Validar(Usuario usuario, bool esNuevo)
        {
            if (usuario == null) throw new FaultException("Los datos del usuario son obligatorios.");

            var errores = new List<string>();
            if (!esNuevo && usuario.Id <= 0) errores.Add("Id inválido.");
            if (string.IsNullOrWhiteSpace(usuario.Nombre)) errores.Add("El nombre es obligatorio.");
            else if (usuario.Nombre.Trim().Length > 100) errores.Add("El nombre no puede superar 100 caracteres.");
            if (usuario.FechaNacimiento.Year < 1900) errores.Add("La fecha de nacimiento no es válida.");
            if (usuario.FechaNacimiento.Date > DateTime.Today) errores.Add("La fecha de nacimiento no puede ser futura.");
            if (!SexosValidos.Contains(usuario.Sexo)) errores.Add("El sexo debe ser M o F.");

            if (errores.Any()) throw new FaultException(string.Join(" ", errores));
        }
    }
}
