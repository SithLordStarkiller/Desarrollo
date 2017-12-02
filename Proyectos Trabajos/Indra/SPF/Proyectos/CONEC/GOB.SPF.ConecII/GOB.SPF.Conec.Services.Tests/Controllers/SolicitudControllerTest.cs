using System;
using System.Collections.Generic;
using System.Linq;
using GOB.SPF.ConecII.Business;
using GOB.SPF.ConecII.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GOB.SPF.Conec.Services.Tests.Controllers
{
    [TestClass]
    public class SolicitudControllerTest
    {
        private ClienteBusiness clienteBusiness = new ClienteBusiness();
        private InstalacionBusiness instalacionBusiness = new InstalacionBusiness();
        #region Cliente

        [TestMethod]
        public Cliente ClientesInsertarTest()
        {

            var cliente = new Cliente
            {
                IsActive = true,
                RazonSocial = "Razón social Insertar test",
                NombreCorto = "Nombre",
                Rfc = $"RFC920322{RandomNumberBetween(123, 839)}",
                RegimenFiscal = new RegimenFiscal { Identificador = 1 },
                Sector = new Sector { Identificador = 1 },
                Contactos = new List<Externo>
                {
                    new Externo
                    {
                        Activo = true,
                        ApellidoMaterno = "Apellido Materno",
                        ApellidoPaterno = "Apellido paterno",
                        Nombre = "Nombre",
                        Cargo = "Cargo X",
                        Correos = new List<Correo>
                        {
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(987,999)}@correo.com" },
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(987,999)}@correo.com" },
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(987,999)}@correo.com" }
                        },
                        Telefonos = new List<Telefono>
                        {
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 1}, Numero = $"23419234{RandomNumberBetween(10,99)}"},
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 2}, Numero = $"23419234{RandomNumberBetween(10,99)}"},
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 3}, Numero = $"23419234{RandomNumberBetween(10,99)}"},
                        },
                        IdTipoPersona = 1,
                        TipoContacto = {Identificador = 2}


                    },
                    new Externo
                    {
                        Activo = true,
                        ApellidoMaterno = "Apellido Materno",
                        ApellidoPaterno = "Apellido paterno",
                        Nombre = "Nombre",
                        Cargo = "Cargo X",
                        Correos = new List<Correo>
                        {
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(967,999)}@correo.com" },
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(934,999)}@correo.com" },
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(937,999)}@correo.com" }
                        },
                        Telefonos = new List<Telefono>
                        {
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 1}, Numero = $"23419234{RandomNumberBetween(10,99)}"},
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 2}, Numero = $"23419234{RandomNumberBetween(10,98)}"},
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 3}, Numero = $"23419234{RandomNumberBetween(10,90)}"},
                        },
                        IdTipoPersona = 1,
                        TipoContacto = {Identificador = 2}
                    }
                },
                Solicitantes = new List<Externo>
                {
                    new Externo
                    {
                        Activo = true,
                        ApellidoMaterno = "Apellido Materno",
                        ApellidoPaterno = "Apellido paterno",
                        Nombre = "Nombre",
                        Cargo = "Cargo X",
                        Correos = new List<Correo>
                        {
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(987,999)}@correo.com" },
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(987,999)}@correo.com" },
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(987,999)}@correo.com" }
                        },
                        Telefonos = new List<Telefono>
                        {
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 1}, Numero = $"23419134{RandomNumberBetween(10,99)}"},
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 2}, Numero = $"23419224{RandomNumberBetween(10,99)}"},
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 3}, Numero = $"23413234{RandomNumberBetween(10,99)}"},
                        },
                        IdTipoPersona = 1,
                        TipoContacto = {Identificador = 1}


                    },
                    new Externo
                    {
                        Activo = true,
                        ApellidoMaterno = "Apellido Materno",
                        ApellidoPaterno = "Apellido paterno",
                        Nombre = "Nombre",
                        Cargo = "Cargo X",
                        Correos = new List<Correo>
                        {
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(967,999)}@correo.com" },
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(934,999)}@correo.com" },
                            new Correo {CorreoElectronico = $"correo{RandomNumberBetween(937,999)}@correo.com" }
                        },
                        Telefonos = new List<Telefono>
                        {
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 1}, Numero = $"23419234{RandomNumberBetween(10,99)}"},
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 2}, Numero = $"23419234{RandomNumberBetween(10,98)}"},
                            new Telefono{TipoTelefono = new TipoTelefono{Identificador = 3}, Numero = $"23419234{RandomNumberBetween(10,90)}"},
                        },
                        IdTipoPersona = 1,
                        TipoContacto = {Identificador = 1}
                    }
                },
                DomicilioFiscal = new DomicilioFiscal
                {
                    Asentamiento = new Asentamiento
                    {
                        CodigoPostal = "91697",
                        Estado = new Estado { Identificador = 30 },
                        Municipio = new Municipio { Identificador = 193 },
                        Identificador = 443435
                    },
                    Calle = "Av Framboyanes",
                    NoExterior = "6",
                    NoInterior = "1 15"
                },
                Documentos = null
            };
            var resultadoCliente = clienteBusiness.Guardar(cliente);
            Assert.IsTrue(resultadoCliente > 0);
            return cliente;
        }

        [TestMethod]
        public void ClientesActualizarTest()
        {
            var cliente = ClientesInsertarTest();
            var clienteEditar = new Cliente
            {
                IsActive = true,
                RazonSocial = "Razón Social Actualizar Test",
                NombreCorto = "RSA",
                Rfc = cliente.Rfc,
                RegimenFiscal = new RegimenFiscal { Identificador = 2 },
                Sector = new Sector { Identificador = 2 },
                Contactos = null,
                Solicitantes = null,
                DomicilioFiscal = new DomicilioFiscal
                {
                    Asentamiento = new Asentamiento
                    {
                        CodigoPostal = "55870",
                        Estado = new Estado { Identificador = 15 },
                        Municipio = new Municipio { Identificador = 2 },
                        Identificador = 476742
                    },
                    Calle = "Av. Canal de Miramontes",
                    NoExterior = "2549",
                    NoInterior = "",
                    Identificador = cliente.DomicilioFiscal.Identificador
                },
                Documentos = null,
                Identificador = cliente.Identificador
            };
            var resultadoCliente = clienteBusiness.Guardar(clienteEditar);
            Assert.IsTrue(resultadoCliente > 0);
        }

        [TestMethod]
        public void ClientesCambiarEstatusTest()
        {
            var cliente = ClientesInsertarTest();
            var clienteCambiarEstatus = new Cliente
            {
                IsActive = false,
                Identificador = cliente.Identificador
            };
            var resultadoCliente = clienteBusiness.CambiarEstatus(clienteCambiarEstatus);
            Assert.IsTrue(resultadoCliente);
        }

        [TestMethod]
        public void ClientesObtenerTodosTest()
        {
            var paging = new Paging
            {
                All = true,
                CurrentPage = 1,
                Pages = 1,
                Rows = 20
            };
            var list = clienteBusiness.ObtenerTodos(paging);
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        public void ClientesObtenerPorCriterioTest()
        {
            var paging = new Paging
            {
                All = true,
                CurrentPage = 1,
                Pages = 1,
                Rows = 20
            };

            var cliente = ClientesInsertarTest();

            var list = clienteBusiness.ObtenerPorCriterio(paging, cliente);
            Assert.IsTrue(list.Any(p => p.RazonSocial.Contains(cliente.RazonSocial)));
        }

        [TestMethod]
        public void ClientesObtenerPorIdTest()
        {
            var idCliente = 1;
            var resultadoCliente = clienteBusiness.ObtenerPorId(idCliente);
            Assert.IsTrue(resultadoCliente.Identificador.Equals(idCliente));
        }
        #endregion

        #region Instalaciones

        [TestMethod]
        public Cliente InstalacionesInsertarTest()
        {
            var cliente = ClientesInsertarTest();
            cliente.Instalaciones.Add(new Instalacion
            {
                Zona = new Zona { Identificador = 1 },
                Estacion = new Estacion { Identificador = 1 },
                Nombre = $"Instalación {RandomNumberBetween(1, 1234)}",
                FechaInicio = DateTime.Now,
                FechaFin = null,
                Calle = "Oceania",
                NumInterior = RandomNumberBetween(1, 7).ToString(),
                NumExterior = "915",
                Referencia = "spf",
                Colindancia = "SEP",
                Asentamiento = new Asentamiento
                {
                    CodigoPostal = "04330",
                    Estado = new Estado { Identificador = 9 },
                    Nombre = "EL ROSEDAL",
                    Municipio = new Municipio { Identificador = 3 },
                    Identificador = 502821
                },
                Latitud = 19.3430751,
                Longitud = -99.1555381,
                Fraccion = new Fraccion { Identificador = 1 },
                TipoInstalacion = new TipoInstalacion { Identificador = 7 },
                TelefonosInstalacion = new List<Telefono>
                {
                    new Telefono
                    {
                        TipoTelefono = new TipoTelefono{Identificador = 1},
                        Numero = $"55687619{RandomNumberBetween(10,19)}"
                    },new Telefono
                    {
                        TipoTelefono = new TipoTelefono{Identificador = 2},
                        Numero = $"55687619{RandomNumberBetween(20,29)}"
                    },new Telefono
                    {
                        TipoTelefono = new TipoTelefono{Identificador = 3},
                        Numero = $"55687619{RandomNumberBetween(30,39)}"
                    },
                },
                CorreosInstalacion = new List<Correo>
                {
                    new Correo {CorreoElectronico = $"correoPrueba{RandomNumberBetween(10,19)}@correo.com"},
                    new Correo {CorreoElectronico = $"correoPrueba{RandomNumberBetween(20,29)}@correo.com"},
                    new Correo {CorreoElectronico = $"correoPrueba{RandomNumberBetween(30,39)}@correo.com"}
                }
            });
            instalacionBusiness.Guardar(cliente);
            Assert.IsTrue(cliente != null);
            return cliente;
        }

        [TestMethod]
        public void InstalacionesActualizarTest()
        {
            var cliente = InstalacionesInsertarTest();
            cliente.Instalaciones.FirstOrDefault().FechaFin = DateTime.Now.AddDays(9);
            var result = instalacionBusiness.Guardar(cliente);
            Assert.IsTrue(result != 0);
        }

        [TestMethod]
        public void InstalacionesCambiarEstatusTest()
        {
            var cliente = InstalacionesInsertarTest();
            var instalacion = cliente.Instalaciones.FirstOrDefault();
            instalacion.Activo = false;
            var result = instalacionBusiness.CambiarEstatus(instalacion);
            Assert.IsTrue(result != 0);
        }

        [TestMethod]
        public void InstalacionesObtenerTodosTest()
        {
            var paging = new Paging
            {
                All = true,
                CurrentPage = 1,
                Pages = 1,
                Rows = 20
            };
            InstalacionesInsertarTest();
            var result = instalacionBusiness.ObtenerTodos(paging);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void InstalacionesObtenerPorCriterioTest()
        {
            var paging = new Paging
            {
                All = true,
                CurrentPage = 1,
                Pages = 1,
                Rows = 20
            };
            var instalacion = InstalacionesInsertarTest();
            var result = instalacionBusiness.ObtenerPorCriterio(paging, instalacion);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void InstalacionesObtenerPorIdTest()
        {
            var paging = new Paging
            {
                All = true,
                CurrentPage = 1,
                Pages = 1,
                Rows = 20
            };
            var instalacion = InstalacionesInsertarTest();
            if (instalacion != null)
            {
                var first = instalacion.Instalaciones.FirstOrDefault();
                var result = instalacionBusiness.ObtenerPorId(first.Identificador);
                Assert.IsTrue(result != null);
            }
        }

        #endregion
        private static Int64 RandomNumberBetween(double minValue, double maxValue)
        {
            var numero = new Random();
            var next = numero.NextDouble();
            return Convert.ToInt64(minValue + next * (maxValue - minValue));
        }
    }
}
