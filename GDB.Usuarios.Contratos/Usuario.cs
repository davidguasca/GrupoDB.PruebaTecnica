using System;
using System.Runtime.Serialization;

namespace GDB.Usuarios.Contratos
{
    /// <summary>Datos de un usuario que viajan entre la web y el servicio.</summary>
    [DataContract(Namespace = "http://grupodigitalbank.com/usuarios")]
    public class Usuario
    {
        [DataMember] public int Id { get; set; }
        [DataMember] public string Nombre { get; set; }
        [DataMember] public DateTime FechaNacimiento { get; set; }
        [DataMember] public string Sexo { get; set; }
    }
}
