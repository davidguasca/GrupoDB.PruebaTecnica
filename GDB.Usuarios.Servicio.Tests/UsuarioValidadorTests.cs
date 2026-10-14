using System;
using System.ServiceModel;
using GDB.Usuarios.Contratos;
using GDB.Usuarios.Servicio.Negocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GDB.Usuarios.Servicio.Tests
{
    [TestClass]
    public class UsuarioValidadorTests
    {
        [TestMethod]
        public void UsuarioValido_SeAcepta() => UsuarioValidador.Validar(Valido(), esNuevo: false);

        [TestMethod]
        public void NombreVacio_SeRechaza() => DebeRechazar(u => u.Nombre = "   ");

        [TestMethod]
        public void NombreDeMasDe100Caracteres_SeRechaza() => DebeRechazar(u => u.Nombre = new string('a', 101));

        [TestMethod]
        public void FechaFutura_SeRechaza() => DebeRechazar(u => u.FechaNacimiento = DateTime.Today.AddDays(1));

        [TestMethod]
        public void SexoDistintoDeMoF_SeRechaza() => DebeRechazar(u => u.Sexo = "X");

        [TestMethod]
        public void ModificarSinId_SeRechaza() => DebeRechazar(u => u.Id = 0, esNuevo: false);

        /// <summary>Usuario que cumple todas las reglas; cada prueba le cambia un solo dato.</summary>
        private static Usuario Valido() =>
            new Usuario { Id = 1, Nombre = "Ana Pérez", FechaNacimiento = new DateTime(1990, 5, 10), Sexo = "F" };

        /// <summary>Aplica el cambio a un usuario válido y comprueba que el validador lo rechace.</summary>
        private static void DebeRechazar(Action<Usuario> cambio, bool esNuevo = true)
        {
            var usuario = Valido();
            cambio(usuario);
            Assert.ThrowsException<FaultException>(() => UsuarioValidador.Validar(usuario, esNuevo));
        }
    }
}
