using System;
using System.ServiceModel;
using System.Web.UI;
using GDB.Usuarios.Contratos;

namespace GDB.Usuarios.Web.Servicios
{
    /// <summary>Punto único de acceso de la web al servicio WCF de usuarios.</summary>
    public static class ServicioUsuarios
    {
        // La fábrica es costosa de crear y segura entre hilos: se crea una vez y se reutiliza.
        private static readonly ChannelFactory<IUsuarioService> Fabrica =
            new ChannelFactory<IUsuarioService>("UsuarioService");

        /// <summary>Abre un canal, ejecuta la operación y lo cierra; si algo falla, lo aborta.</summary>
        public static T Ejecutar<T>(Func<IUsuarioService, T> operacion)
        {
            var canal = Fabrica.CreateChannel();
            var comunicacion = (ICommunicationObject)canal;
            try
            {
                var resultado = operacion(canal);
                comunicacion.Close();
                return resultado;
            }
            catch
            {
                comunicacion.Abort();
                throw;
            }
        }

        /// <summary>Ejecuta una llamada al servicio; si falla, escribe el motivo en el control indicado.</summary>
        public static void Intentar(Action llamada, ITextControl mensajeError)
        {
            try
            {
                llamada();
            }
            catch (FaultException ex)              // error de negocio enviado por el servicio
            {
                mensajeError.Text = ex.Message;
            }
            catch (Exception ex) when (ex is CommunicationException || ex is TimeoutException)   // servicio caído o lento
            {
                mensajeError.Text = "El servicio no está disponible. Intente más tarde.";
            }
        }
    }
}
