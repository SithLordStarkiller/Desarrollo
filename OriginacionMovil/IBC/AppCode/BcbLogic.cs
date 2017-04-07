using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using IBC.buroCredito;

namespace IBC.AppCode
{
    class BcbLogic
    {
        public EncabezadoBC GetEncabezadoBc(int idEmpresa, int idConsultaConfig)
        {
            EncabezadoBC ret = null;
            try
            {
                using (var ctx = new Broxel_SociedadesCreditoEntities())
                {
                    var values = ctx.catEncabezadoXEmpresa.SingleOrDefault(e => e.idEmpresa==idEmpresa && e.idConsultaConfig==idConsultaConfig);
                    if (values != null)
                    {
                        ret = new EncabezadoBC
                        {
                            ClavePais = values.clavePais,
                            ClaveUnidadMonetaria = values.claveUnidadMonetaria,
                            ClaveUsuario = values.claveUsuario,
                            IdentificadorBuro = values.identificadorBuro,
                            Idioma = values.idioma,
                            ImporteContrato = values.importeContrato,
                            NumeroReferenciaOperador = values.numeroReferenciaOperador,
                            Password = values.password,
                            ProductoRequerido = values.productoRequerido,
                            TipoConsulta = values.tipoConsulta,
                            TipoContrato = values.tipoContrato,
                            TipoSalida = values.tipoSalida,
                            Version = values.version
                        };
                    }
                }
            }
            catch
            {
                ret = null;
            }
            return ret;
        }

        public Direccion GetDireccion(string calle, string numeroExt, string numeroInt, string colonia,
            string delegacion, string estado, string cp, ref string detalle)
        {
            Direccion ret = null;
            try
            {
                using (var ctx = new Broxel_SociedadesCreditoEntities())
                {
                    var sepomexInfo = ctx.CPSepomex.FirstOrDefault(d => d.d_codigo == cp);
                    if (sepomexInfo != null)
                    {
                        ret = new Direccion();
                        var dirs = GetDirecciones(calle.ToUpper(), numeroExt.ToUpper(), numeroInt.ToUpper());
                        if (dirs != null)
                        {
                            ret.Direccion1 = dirs[0];
                            if (dirs.Length > 1)
                                ret.Direccion2 = dirs[1];
                            ret.ColoniaPoblacion = colonia.ToUpper();
                            ret.DelegacionMunicipio = delegacion.ToUpper() == sepomexInfo.D_mnpio
                                ? delegacion.ToUpper()
                                : sepomexInfo.D_mnpio;
                            ret.Estado = estado.ToUpper() == sepomexInfo.cve_edo_bc
                                ? estado.ToUpper() 
                                : sepomexInfo.cve_edo_bc;
                            ret.CP = cp;
                        }
                        else
                        {
                            detalle = "Error al obtener la dirección";
                            ret = null;
                        }

                    }
                    else
                    {
                        detalle = "El código postal " + cp + " no existe en Sepomex, favor de verificarlo";
                    }
                }
            }
            catch
            {
                ret = null;
                detalle = "Existio un error al cotejar los datos de dirección, reintente más tarde.";
            }
            return ret;
        }
        public string GetPrimerNombre(string nombre)
        {
            return GetNombrePos(nombre, 1);
        }

        public string GetSegundoNombre(string nombre)
        {
            return GetNombrePos(nombre, 2);
        }

        private string GetNombrePos(string nombre, int pos)
        {
            var nombres = nombre.Split(' ');
            if (nombres.Length <= 0) return nombre;
            switch (pos)
            {
                case 1:
                    return nombres[0];
                case 2:
                    var segundoNombre = "";
                    for (var i = 1; i < nombres.Length; i++)
                        segundoNombre += (nombres[i] + " ");
                    return segundoNombre;
            }
            return nombre;
        }
        private string[] GetDirecciones(string calle, string numeroExt, string numeroInt)
        {
            string[] direcciones = null;
            try
            {
                var direccion = calle + " " + numeroExt + " " + numeroInt;
                if (direccion.Length > 40)
                {
                    direcciones = new string[2];
                    direcciones[0] = direccion.Substring(0, 40);
                    direcciones[1] = direccion.Substring(40, direccion.Length > 80 ? 40 : direccion.Length-40);
                }
                else
                {
                    direcciones = new[] {direccion};
                }
            }
            catch
            {
                direcciones = null;
            }
            return direcciones;
        }

        public bool ValidaRespuestaBc(RespuestaBC response, ref string  detalle)
        {
            var ret = false;
            try
            {


            }
            catch (Exception e)
            {
                detalle = "Error inesperado en el proceso, detalle sistemas: " + e.Message;
                ret = false;
            }
            return ret;
        }

        public bool ValidaConsultaBc(string apaterno, string amaterno, string nombreP, string nombreS, string rfc,
            string calle,
            string numeroExt, string numeroInt, string colonia, string delegacion, string estado, string cp,
            ref bool result)
        {
            var res = false;
            try
            {
                using (var ctx = new Broxel_SociedadesCreditoEntities())
                {
                    var bcData = ctx.spCheckBuroHistoric(apaterno, amaterno, nombreP, nombreS, rfc, calle, numeroExt,
                        numeroInt, colonia, delegacion, estado, cp).First();
                    if (bcData!=null)
                    {
                        res = true;
                        if (bcData.numeroMOP96 == "00" && bcData.numeroMOP97 == "00" && bcData.numeroMOP99 == "00")
                            result = true;
                        else
                            result = false;
                    }
                }
            }
            catch (Exception e)
            {
                res = false;
                result = false;
            }
            return res;
        }


        public int InsertaConsultaBc(string apaterno, string amaterno, string nombreP, string nombreS, string rfc, string calle,
            string numeroExt, string numeroInt, string colonia, string delegacion, string estado, string cp, IBC.buroCredito.Direccion dir)
        {
            var idConsultaBC = 0;
            try
            {
                using (var ctx = new Broxel_SociedadesCreditoEntities())
                {
                    var consultaBcId = new ObjectParameter("idConsultaBC", typeof (int));
                        ctx.spInsConsultaBC(apaterno, amaterno, nombreP, nombreS, rfc, consultaBcId);
                    idConsultaBC = (int)consultaBcId.Value;
                    if (idConsultaBC > 0)
                    {
                        var dom = new IBC.DomicilioBCRequest
                        {
                            idConsultaBC = idConsultaBC,
                            calle = calle,
                            colonia = colonia,
                            cp = cp,
                            delegacion = delegacion,
                            estado = estado,
                            numeroExt = numeroExt,
                            numeroInt = numeroInt,
                            direccionBC1 = dir.Direccion1,
                            direccionBC2 = dir.Direccion2,
                            coloniaBC = dir.ColoniaPoblacion,
                            delegacionBC = dir.DelegacionMunicipio,
                            estadoBC = dir.Estado
                        };
                        ctx.DomicilioBCRequest.Add(dom);
                        ctx.SaveChanges();
                    }
                }
            }
            catch
            {
                
            }
            return idConsultaBC;
        }

        public bool AnalizaReponse(IBC.buroCredito.RespuestaBC resp, ref string result)
        {
            var res = false;
            try
            {
                if (resp != null)
                {
                    if (resp.Personas[0].Error != null)
                    {
                        if (resp.Personas[0].Error.AR != null)
                        {
                            if (string.IsNullOrEmpty(resp.Personas[0].Error.AR.ClaveOPasswordErroneo))
                                result = "Clave o password erroneo";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.AR.EtiquetaSegmentoErronea))
                                result = "Etiqueta de segmento erronea";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.AR.FaltaCampoRequerido))
                                result = "Falta campo requerido " + resp.Personas[0].Error.AR.FaltaCampoRequerido;
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.AR.ReferenciaOperador))
                                result = "Falta referencia operador";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.AR.SujetoNoAutenticado))
                                result = "Sujeto no autenticado";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.AR.ErrorSistemaBC))
                                result = "Error del sistema BC: " + resp.Personas[0].Error.AR.ErrorSistemaBC;
                            return res;
                        }
                        else if (resp.Personas[0].Error.UR != null)
                        {
                            if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.ErrorReporteBloqueado))
                                result = "Error reporte bloqueado";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.EtiquetaSegmentoErronea))
                                result = "Etiqueta de segmento erronea";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.FaltaCampoRequerido))
                                result = "Falta campo requerido " + resp.Personas[0].Error.UR.FaltaCampoRequerido;
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.InformacionErroneaParaConsulta))
                                result = "Informacion Erronea para consulta";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.NumeroErroneoSegmentos))
                                result = "Numero erroneo de segmentos";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.NumeroReferenciaOperador))
                                result = "Numero referencia de operador";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.OrdenErroneoSegmento))
                                result = "Orden erroneo de los segmentos de consulta";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.PasswordOClaveErronea))
                                result = "Password o clave erronea";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.ProductoSolicitadoErroneo))
                                result = "Producto solicitado erroneo";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.SegmentoRequeridoNoProporcionado))
                                result = "Segmento requerido no proporcionado";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.SolicitudClienteErronea))
                                result = "Solicitud Cliente Erronea";
                            else if (string.IsNullOrEmpty(resp.Personas[0].Error.UR.ErrorSistemaBuroCredito))
                                result = "Error del sistema BC: " + resp.Personas[0].Error.UR.ErrorSistemaBuroCredito;
                            return res;
                        }
                    }

                    if (resp.Personas[0].Cuentas != null)
                    {
                        foreach (var cuenta in resp.Personas[0].Cuentas)
                        {
                            if (cuenta.FormaPagoActual == "96" || cuenta.FormaPagoActual == "97" ||
                                cuenta.FormaPagoActual == "99")
                            {
                                result = "Cuenta con atraso de más de 12 meses o deuda sin recuperar";
                                return res;
                            }

                        }
                    }
                    res = true;
                }
            }
            catch (Exception e)
            {
                
            }
            return res;
        }

        public void PersisteResponse(IBC.buroCredito.RespuestaBC resp, int idConsultaBc)
        {
            try
            {
                using (var ctx = new Broxel_SociedadesCreditoEntities())
                {
                    foreach (var persona in resp.Personas)
                    {
                        var personaRespBcId = new ObjectParameter("pIdPersonaRespBC", typeof (int));
                        ctx.spInsPersonaRespBC(idConsultaBc, persona.ReporteImpreso, personaRespBcId);
                        var idPersonaBc = (int)personaRespBcId.Value;

                        if (persona.ResumenReporte != null)
                        {
                            foreach (var resumen in persona.ResumenReporte)
                            {
                                var r = new ResumenReporteResp
                                {
                                    idPersonaRespBC = idPersonaBc,
                                    fechaIngreso = resumen.FechaIngresoBD,
                                    numeroMOP7 = resumen.NumeroMOP7,
                                    numeroMOP6 = resumen.NumeroMOP6,
                                    numeroMOP5 = resumen.NumeroMOP5,
                                    numeroMOP4 = resumen.NumeroMOP4,
                                    numeroMOP3 = resumen.NumeroMOP3,
                                    numeroMOP2 = resumen.NumeroMOP2,
                                    numeroMOP1 = resumen.NumeroMOP1,
                                    numeroMOP0 = resumen.NumeroMOP0,
                                    numeroMOPUR = resumen.NumeroMOPUR,
                                    numeroCuentas = resumen.NumeroCuentas,
                                    cuentasPagosFijosHipotecas = resumen.CuentasPagosFijosHipotecas,
                                    cuentasRevolventesAbiertas = resumen.CuentasRevolventesAbiertas,
                                    cuentasCerradas = resumen.CuentasCerradas,
                                    cuentasNegativasActuales = resumen.CuentasNegativasActuales,
                                    cuentasClaveHistoriaNegativa = resumen.CuentasClavesHistoriaNegativa,
                                    cuentasDisputa = resumen.CuentasDisputa,
                                    numeroSolicitudesUltimos6Meses = resumen.NumeroSolicitudesUltimos6Meses,
                                    nuevaDireccionReportadaUltimos60Dias = resumen.NuevaDireccionReportadaUltimos60Dias,
                                    mensajesAlertaField = resumen.MensajesAlerta,
                                    existenciaDeclaracionesConsumidor = resumen.ExistenciaDeclaracionesConsumidor,
                                    tipoMoneda = resumen.TipoMoneda,
                                    totalCreditosMaximosRevolventes = resumen.TotalCreditosMaximosRevolventes,
                                    totalLimitesCreditoRevolventes = resumen.TotalLimitesCreditoRevolventes,
                                    totalSaldosActualesRevolventes = resumen.TotalSaldosActualesRevolventes,
                                    totalSaldosVencidosRevolventes = resumen.TotalSaldosVencidosRevolventes,
                                    totalPagosPagosFijos = resumen.TotalPagosPagosFijos,
                                    numeroMOP96 = resumen.NumeroMOP96,
                                    numeroMOP97 = resumen.NumeroMOP97,
                                    numeroMOP99 = resumen.NumeroMOP99,
                                    fechaAperturaCuentaMasAntigua = resumen.FechaAperturaCuentaMasAntigua,
                                    fechaAperturaCuentaMasReciente = resumen.FechaAperturaCuentaMasReciente,
                                    totalSolicitudesReporte = resumen.TotalSolicitudesReporte,
                                    fechaSolicitudReporteMasReciente = resumen.FechaSolicitudReporteMasReciente,
                                    numeroTotalCuentasDespachoCobranza = resumen.NumeroTotalCuentasDespachoCobranza,
                                    fechaSolicitudMasRecienteDespachoCobranza =
                                        resumen.FechaSolicitudMasRecienteDespachoCobranza
                                };
                                ctx.ResumenReporteResp.Add(r);
                                ctx.SaveChanges();
                            }
                        }

                        var encabezado = persona.Encabezado;
                        if (encabezado != null)
                        {
                            var enc = new EncabezadoBCResp
                            {
                                idPersonaRespBC = idPersonaBc,
                                claveOtorgante = encabezado.ClaveOtorgante,
                                claveRetornoConsumidorPrincipal = encabezado.ClaveRetornoConsumidorPrincipal,
                                claveRetornoConsumidorSecundario = encabezado.ClaveRetornoConsumidorSecundario,
                                numeroControlConsulta = encabezado.NumeroControlConsulta
                            };
                            ctx.EncabezadoBCResp.Add(enc);
                            ctx.SaveChanges();
                        }
                        var nombre = persona.Nombre;
                        if (nombre != null)
                        {
                            var n = new NombreBCResp
                            {
                                //checar NombreBCResp, faltan campos

                                idPersonaBCResp = idPersonaBc,
                                apellidoPaterno = nombre.ApellidoPaterno,
                                apellidoMaterno = nombre.ApellidoMaterno,
                                apellidoAdicional = nombre.ApellidoAdicional,
                                primerNombre = nombre.PrimerNombre,
                                segundoNombre = nombre.SegundoNombre,
                                fechaNacimiento = nombre.FechaNacimiento,
                                rfc= nombre.RFC,
                                prefijo = nombre.Prefijo,
                                sufijo = nombre.Sufijo,
                                nacionalidad = nombre.Nacionalidad,
                                residencia = nombre.Residencia,
                                estadoCivil = nombre.EstadoCivil,
                                sexo = nombre.Sexo,
                                numeroCedulaProfesional = nombre.NumeroCedulaProfesional,
                                numeroRegistroElectoral= nombre.NumeroRegistroElectoral,
                                claveImpuestosOtroPais = nombre.ClaveImpuestosOtroPais,
                                claveOtroPais = nombre.ClaveOtroPais,
                                numeroDependientes = nombre.NumeroDependientes,
                                edadesDependientes = nombre.EdadesDependientes,
                                fechaDefuncion = nombre.FechaDefuncion,
                                fechaRecepciónInformacionDependientes = nombre.FechaRecepcionInformacionDependientes
                            };
                            ctx.NombreBCResp.Add(n);
                            ctx.SaveChanges();
                        }

                        if (persona.Domicilios != null)
                        {
                            foreach (var direccion in persona.Domicilios)
                            {
                                var d = new DireccionBCResp
                                {
                                    idPersonaRespBC = idPersonaBc,
                                    direccion1 = direccion.Direccion1,
                                    direccion2 = direccion.Direccion2,
                                    coloniaPoblacion = direccion.ColoniaPoblacion,
                                    delegacionMunicipio = direccion.DelegacionMunicipio,
                                    ciudad = direccion.Ciudad,
                                    estado = direccion.Estado,
                                    cp = direccion.CP,
                                    fechaResidencia = direccion.FechaResidencia,
                                    numeroTelefono = direccion.NumeroTelefono,
                                    extension = direccion.Extension,
                                    fax = direccion.Fax,
                                    tipoDomicilio = direccion.TipoDomicilio,
                                    indicadorEspecialDomicilio = direccion.IndicadorEspecialDomicilio,
                                    fechaReporteDireccion = direccion.FechaReporteDireccion
                                };

                                ctx.DireccionBCResp.Add(d);
                                ctx.SaveChanges();
                            }
                        }

                        if (persona.Empleos != null)
                        {
                            foreach (var empleo in persona.Empleos)
                            {
                                var e = new EmpleoBCResp
                                {
                                    idPersonaBCResp = idPersonaBc,
                                    nombreEmpresa = empleo.NombreEmpresa,
                                    direccion1 = empleo.Direccion1,
                                    direccion2 = empleo.Direccion2,
                                    coloniaPoblacion = empleo.ColoniaPoblacion,
                                    delegacionMunicipio = empleo.DelegacionMunicipio,
                                    ciudad = empleo.Ciudad,
                                    estado = empleo.Estado,
                                    cp = empleo.CP,
                                    numeroTelefono = empleo.NumeroTelefono,
                                    extension = empleo.Extension,
                                    fax = empleo.Fax,
                                    cargo = empleo.Cargo,
                                    fechaContratacion = empleo.FechaContratacion,
                                    claveMoneda = empleo.ClaveMonedaSalario,
                                    salario = empleo.Salario,
                                    baseSalarial = empleo.BaseSalarial,
                                    fechaUltimoDiaEmpleo = empleo.FechaUltimoDiaEmpleo,
                                    fechaReportoEmpleo = empleo.FechaReportoEmpleo,
                                    fechaVerificacionEmpleo = empleo.FechaVerificacion,
                                    modoVerificacion = empleo.ModoVerificacion
                                };
                                ctx.EmpleoBCResp.Add(e);
                                ctx.SaveChanges();
                            }
                        }

                        if (persona.Cuentas != null)
                        {
                            foreach (var cuenta in persona.Cuentas)
                            {
                                var c = new CuentaBCResp
                                {
                                    idPersonaRespBC = idPersonaBc,
                                    fechaActualizacion = cuenta.FechaActualizacion,
                                    registroImpugnado = cuenta.RegistroImpugnado,
                                    claveOtorgante = cuenta.ClaveOtorgante,
                                    nombreOtorgante = cuenta.NombreOtorgante,
                                    numeroCuentaActual = cuenta.NumeroCuentaActual,
                                    indicadorTipoResponsabilidad = cuenta.IndicadorTipoResponsabilidad,
                                    tipoCuenta = cuenta.TipoCuenta,
                                    tipoContrato = cuenta.TipoContrato,
                                    claveUnidadMonetaria = cuenta.ClaveUnidadMonetaria,
                                    valorActivoValuacion = cuenta.ValorActivoValuacion,
                                    numeroPagos = cuenta.NumeroPagos,
                                    frecuenciaPagos = cuenta.FrecuenciaPagos,
                                    montoPagar = cuenta.MontoPagar,
                                    fechaAperturaCuenta = cuenta.FechaAperturaCuenta,
                                    fechaUltimoPago = cuenta.FechaUltimoPago,
                                    fechaUltimaCompra = cuenta.FechaUltimaCompra,
                                    fechaCierreCuenta = cuenta.FechaCierreCuenta,
                                    fechaReporte = cuenta.FechaReporte,
                                    modoReportarField = cuenta.ModoReportar,
                                    ultimaFechaSaldoCero = cuenta.UltimaFechaSaldoCero,
                                    garantia = cuenta.Garantia,
                                    creditoMaximo = cuenta.CreditoMaximo,
                                    saldoActual = cuenta.SaldoActual,
                                    limiteCredito = cuenta.LimiteCredito,
                                    saldoVencido = cuenta.SaldoVencido,
                                    numeroPagosVencidos = cuenta.NumeroPagosVencidos,
                                    formaPagoActual = cuenta.FormaPagoActual,
                                    historicoPagos = cuenta.HistoricoPagos,
                                    fechaRecienteHistoricoPagos = cuenta.FechaMasRecienteHistoricoPagos,
                                    fechaMasAntiguaHistoricoPagos = cuenta.FechaMasAntiguaHistoricoPagos,
                                    claveObservacion = cuenta.ClaveObservacion,
                                    totalPagosReportados = cuenta.TotalPagosReportados,
                                    totalPagosCalificadosMOP2 = cuenta.TotalPagosCalificadosMOP2,
                                    totalPagosCalificadosMOP3 = cuenta.TotalPagosCalificadosMOP3,
                                    totalPagosCalificadosMOP4 = cuenta.TotalPagosCalificadosMOP4,
                                    totalPagosCalificadosMOP5 = cuenta.TotalPagosCalificadosMOP5,
                                    importeSaldoMorosidadHistMasGrave = cuenta.ImporteSaldoMorosidadHistMasGrave,
                                    fechaHistoricaMorosidadMasGrave = cuenta.FechaHistoricaMorosidadMasGrave,
                                    mopHistoricoMorosidadMasGrave = cuenta.MopHistoricoMorosidadMasGrave,
                                    fechaInicioReestructura = cuenta.FechaInicioReestructura,
                                    montoUltimoPago = cuenta.MontoUltimoPago
                                };

                                ctx.CuentaBCResp.Add(c);
                                ctx.SaveChanges();
                            }
                        }

                        if (persona.ConsultasEfectuadas != null)
                        {
                            foreach (var consultaEfectuada in persona.ConsultasEfectuadas)
                            {
                                var ce = new ConsultaEfectuadaRespBC
                                {
                                    idPersonaRespBC = idPersonaBc,
                                    fechaConsulta = consultaEfectuada.FechaConsulta,
                                    identificacionBuro = consultaEfectuada.IdentificacionBuro,
                                    claveOtorgante = consultaEfectuada.ClaveOtorgante,
                                    nombreOtorgante = consultaEfectuada.NombreOtorgante,
                                    telefonoOtorgante = consultaEfectuada.TelefonoOtorgante,
                                    tipoContrato = consultaEfectuada.TipoContrato,
                                    claveUnidadMonetaria = consultaEfectuada.ClaveUnidadMonetaria,
                                    importeContrato = consultaEfectuada.ImporteContrato,
                                    indicadorTipoResponsabilidad = consultaEfectuada.IndicadorTipoResponsabilidad,
                                    consumidorNuevo = consultaEfectuada.ConsumidorNuevo,
                                    resultadoFinal = consultaEfectuada.ResultadoFinal,
                                    identificadorOrigenConsulta = consultaEfectuada.IdentificadorOrigenConsulta
                                };
                                ctx.ConsultaEfectuadaRespBC.Add(ce);
                                ctx.SaveChanges();
                            }
                        }

                        if (persona.HawkAlertConsulta != null)
                        {
                            foreach (var hawkC in persona.HawkAlertConsulta)
                            {
                                var h1 = new HawkAlertConsultaRespBC
                                {
                                    idPersonaRespBC = idPersonaBc,
                                    fechaReporte = hawkC.FechaReporte,
                                    codigoClave = hawkC.CodigoClave,
                                    tipoInstitucion = hawkC.TipoInstitucion,
                                    mensaje = hawkC.Mensaje
                                };
                                ctx.HawkAlertConsultaRespBC.Add(h1);
                                ctx.SaveChanges();
                            }
                        }

                        if (persona.HawkAlertBD != null)
                        {
                            foreach (var hawkBd in persona.HawkAlertBD)
                            {
                                var h2 = new HawkAlertBDRespBC
                                {
                                    idPersonaRespBC = idPersonaBc,
                                    fechaReporte = hawkBd.FechaReporte,
                                    codigoClave = hawkBd.CodigoClave,
                                    tipoInstitucion = hawkBd.TipoInstitucion,
                                    mensaje = hawkBd.Mensaje
                                };
                                ctx.HawkAlertBDRespBC.Add(h2);
                                ctx.SaveChanges();
                            }
                        }

                        if (persona.DeclaracionesCliente != null)
                        {
                            var dc = new DeclaracionesClienteRespBC
                            {
                                idPersonaRespBC = idPersonaBc,
                                declaracionConsumidor = persona.DeclaracionesCliente.DeclaracionConsumidor
                            };
                            ctx.DeclaracionesClienteRespBC.Add(dc);
                            ctx.SaveChanges();
                        }

                        if (persona.ScoreBuroCredito != null)
                        {
                            foreach (var score in persona.ScoreBuroCredito)
                            {
                                var codigoRazon = score.CodigoRazon.Aggregate("", (current, s1) => current + '|' + s1);
                                var s = new ScoreBuroCreditoRespBC
                                {
                                    idPersonaRespBC = idPersonaBc,
                                    nombreScore = score.nombreScore,
                                    codigoScore = score.CodigoScore,
                                    valorScore = score.ValorScore,
                                    codigoRazon = codigoRazon,
                                    codigoError = score.CodigoError
                                };
                                ctx.ScoreBuroCreditoRespBC.Add(s);
                                ctx.SaveChanges();
                            }
                        }

                        if (persona.Caracteristicas != null)
                        {
                            foreach (var caracteristica in persona.Caracteristicas)
                            {
                                var car = new CaracteristicasBC
                                {
                                    idPersonaRespBC = idPersonaBc,
                                    plantilla = caracteristica.Plantilla,
                                    idCaracteristica = caracteristica.IdCaracteristica,
                                    valor = caracteristica.Valor,
                                    codigoError = caracteristica.CodigoError
                                };

                                ctx.CaracteristicasBC.Add(car);
                                ctx.SaveChanges();
                            }
                        }

                        if (persona.Error != null)
                        {
                            var idErrorRespBc = 0;
                            var pIdErrorRespBc = new ObjectParameter("idErrorRespBC", typeof (int));
                            ctx.spInsErrorRespBC(idPersonaBc, pIdErrorRespBc);
                            idErrorRespBc = (int)pIdErrorRespBc.Value;
                            if (idErrorRespBc != 0)
                            {
                                if (persona.Error.AR != null)
                                {
                                    var ar = new ARBC
                                    {
                                        idErrorRespBC = idErrorRespBc,
                                        referenciaOperador = persona.Error.AR.ReferenciaOperador,
                                        sujetoNoAutenticado = persona.Error.AR.SujetoNoAutenticado,
                                        claveOPasswordErroneso = persona.Error.AR.ClaveOPasswordErroneo,
                                        errorSistemaBC = persona.Error.AR.ErrorSistemaBC,
                                        etiquetaSegmentoErronea = persona.Error.AR.EtiquetaSegmentoErronea,
                                        faltaCampoRequerido = persona.Error.AR.FaltaCampoRequerido
                                    };
                                    ctx.ARBC.Add(ar);
                                    ctx.SaveChanges();
                                }
                                if (persona.Error.UR != null)
                                {
                                    var ur = new URBC
                                    {
                                        idErrorRespBC = idErrorRespBc,
                                        numeroReferenciaOperador = persona.Error.UR.NumeroReferenciaOperador,
                                        solicitudClienteErronea = persona.Error.UR.SolicitudClienteErronea,
                                        versionProporcionadaErronea = persona.Error.UR.VersionProporcionadaErronea,
                                        productoSolicitadoErroneo = persona.Error.UR.ProductoSolicitadoErroneo,
                                        passwordOClaveErronea = persona.Error.UR.PasswordOClaveErronea,
                                        segmentoRequerido = persona.Error.UR.SegmentoRequeridoNoProporcionado,
                                        ultimaInformacionValidaCliente = persona.Error.UR.UltimaInformacionValidaCliente,
                                        informacionErroneaParaConsulta = persona.Error.UR.InformacionErroneaParaConsulta,
                                        valorErroneoCampoRelacionado = persona.Error.UR.ValorErroneoCampoRelacionado,
                                        errorSistemaBuroCredito = persona.Error.UR.ErrorSistemaBuroCredito,
                                        etiquetaSegmentoErronea = persona.Error.UR.EtiquetaSegmentoErronea,
                                        ordenErroneoSegmento = persona.Error.UR.OrdenErroneoSegmento,
                                        numeroErroneoSegmentos = persona.Error.UR.NumeroErroneoSegmentos,
                                        faltaCampoRequerido = persona.Error.UR.FaltaCampoRequerido,
                                        errorReporteBloqueado = persona.Error.UR.ErrorReporteBloqueado
                                    };
                                    ctx.URBC.Add(ur);
                                    ctx.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (idConsultaBc != 0)
                {
                    var msg = "Error al consultar buro de crédito en PersisteResponse:" + e.Message;
                    if (e.InnerException != null)
                        msg = msg + e.InnerException;

                    InsertBcException(msg, idConsultaBc);
                }
            }
        }

        public string GetParametro(int idParam)
        {
            string parametro = null;
            try
            {
                using (var ctx = new Broxel_SociedadesCreditoEntities())
                {
                    var dbParam = ctx.CatParametros.SingleOrDefault(p => p.idParametro == idParam);
                    if (dbParam != null)
                    {
                        parametro = dbParam.valor;
                    }
                }
            }
            catch (Exception e)
            {
                parametro = null;
            }
            return parametro;
        }

        public void InsertBcException(string msg, int idConsultaBC)
        {
            try
            {
                using (var ctx = new Broxel_SociedadesCreditoEntities())
                {
                    var err = new LogErrorConsultaBC {descripcion = msg, idConsultaBC = idConsultaBC};
                    ctx.LogErrorConsultaBC.Add(err);
                    ctx.SaveChanges();
                }
            }
            catch
            {
                
            }
        }
    }
}
