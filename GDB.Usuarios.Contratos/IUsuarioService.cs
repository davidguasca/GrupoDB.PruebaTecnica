using System.Collections.Generic;
using System.ServiceModel;

namespace GDB.Usuarios.Contratos
{
    /// <summary>Operaciones CRUD que expone el servicio de usuarios.</summary>
    [ServiceContract(Namespace = "http://grupodigitalbank.com/usuarios")]
    public interface IUsuarioService
    {
        /// <summary>Registra un usuario y devuelve su Id.</summary>
        [OperationContract]
        int Agregar(Usuario usuario);

        /// <summary>Actualiza un usuario; devuelve false si no existe.</summary>
        [OperationContract]
        bool Modificar(Usuario usuario);

        /// <summary>Devuelve todos los usuarios, o solo el del Id indicado.</summary>
        [OperationContract]
        List<Usuario> Consultar(int? id);

        /// <summary>Elimina un usuario; devuelve false si no existe.</summary>
        [OperationContract]
        bool Eliminar(int id);
    }
}
