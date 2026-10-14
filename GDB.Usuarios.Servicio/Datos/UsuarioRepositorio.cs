using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GDB.Usuarios.Contratos;

namespace GDB.Usuarios.Servicio.Datos
{
    /// <summary>Único punto de acceso a la base de datos; todo pasa por dbo.sp_Usuario_CRUD.</summary>
    public class UsuarioRepositorio
    {
        private const string Procedimiento = "dbo.sp_Usuario_CRUD";

        private readonly string _cadenaConexion =
            ConfigurationManager.ConnectionStrings["BDUsuarios"].ConnectionString;

        /// <summary>Inserta el usuario y devuelve el Id generado.</summary>
        public int Agregar(Usuario usuario) =>
            EjecutarEscalar("C", cmd => AgregarDatos(cmd, usuario));

        /// <summary>Actualiza el usuario; true si existía.</summary>
        public bool Modificar(Usuario usuario) =>
            EjecutarEscalar("U", cmd => { AgregarId(cmd, usuario.Id); AgregarDatos(cmd, usuario); }) > 0;

        /// <summary>Elimina el usuario; true si existía.</summary>
        public bool Eliminar(int id) =>
            EjecutarEscalar("D", cmd => AgregarId(cmd, id)) > 0;

        /// <summary>Lee todos los usuarios, o solo el del Id indicado.</summary>
        public List<Usuario> Consultar(int? id)
        {
            var usuarios = new List<Usuario>();

            using (var conexion = new SqlConnection(_cadenaConexion))
            using (var cmd = CrearComando(conexion, "R"))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = (object)id ?? DBNull.Value;
                conexion.Open();

                using (var lector = cmd.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        usuarios.Add(new Usuario
                        {
                            Id = lector.GetInt32(0),
                            Nombre = lector.GetString(1),
                            FechaNacimiento = lector.GetDateTime(2),
                            Sexo = lector.GetString(3)
                        });
                    }
                }
            }

            return usuarios;
        }

        /// <summary>Ejecuta el procedimiento y devuelve el primer valor del resultado (Id o filas afectadas).</summary>
        private int EjecutarEscalar(string accion, Action<SqlCommand> agregarParametros)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            using (var cmd = CrearComando(conexion, accion))
            {
                agregarParametros(cmd);
                conexion.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>Crea el comando del procedimiento CRUD para la acción indicada.</summary>
        private static SqlCommand CrearComando(SqlConnection conexion, string accion)
        {
            var cmd = new SqlCommand(Procedimiento, conexion) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@Accion", SqlDbType.Char, 1).Value = accion;
            return cmd;
        }

        /// <summary>Agrega el parámetro @Id.</summary>
        private static void AgregarId(SqlCommand cmd, int id) =>
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

        /// <summary>Agrega los parámetros con los datos del usuario, tipados como en la tabla.</summary>
        private static void AgregarDatos(SqlCommand cmd, Usuario usuario)
        {
            cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = usuario.Nombre.Trim();
            cmd.Parameters.Add("@FechaNacimiento", SqlDbType.Date).Value = usuario.FechaNacimiento.Date;
            cmd.Parameters.Add("@Sexo", SqlDbType.Char, 1).Value = usuario.Sexo;
        }
    }
}
