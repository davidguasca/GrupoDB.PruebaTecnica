using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.ServiceModel;
using GDB.Usuarios.Contratos;
using GDB.Usuarios.Servicio.Datos;
using GDB.Usuarios.Servicio.Negocio;

namespace GDB.Usuarios.Servicio
{
    /// <summary>Implementa el contrato: valida las reglas de negocio y delega la persistencia al repositorio.</summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioRepositorio _repositorio = new UsuarioRepositorio();

        /// <summary>Valida y registra un usuario nuevo.</summary>
        public int Agregar(Usuario usuario)
        {
            UsuarioValidador.Validar(usuario, esNuevo: true);
            return Ejecutar(() => _repositorio.Agregar(usuario));
        }

        /// <summary>Valida y actualiza un usuario existente.</summary>
        public bool Modificar(Usuario usuario)
        {
            UsuarioValidador.Validar(usuario, esNuevo: false);
            return Ejecutar(() => _repositorio.Modificar(usuario));
        }

        /// <summary>Consulta todos los usuarios o uno por Id.</summary>
        public List<Usuario> Consultar(int? id) => Ejecutar(() => _repositorio.Consultar(id));

        /// <summary>Elimina un usuario por Id.</summary>
        public bool Eliminar(int id)
        {
            if (id <= 0) throw new FaultException("Id inválido.");
            return Ejecutar(() => _repositorio.Eliminar(id));
        }

        /// <summary>Registra el error técnico y devuelve al cliente un mensaje controlado.</summary>
        private static T Ejecutar<T>(Func<T> operacion)
        {
            try
            {
                return operacion();
            }
            catch (SqlException ex)
            {
                Trace.TraceError(ex.ToString());
                throw new FaultException("Error al acceder a la base de datos.");
            }
        }
    }
}
