using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using wsBroxel.App_Code;

namespace wsBroxel
{
    /// <summary>
    /// Summary description for wsCuentaTarjeta
    /// </summary>
    [WebService(Namespace = "wsCuentasTarjetas")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsCuentasTarjetas : System.Web.Services.WebService
    {
        [WebMethod]
        public String CierreODT(String folio)
        {
            var ctx = new broxelco_rdgEntities();
            var ordenDeTrabajo = ctx.OrdenDeTrabajo.FirstOrDefault(x => x.Folio == folio);
            if (ordenDeTrabajo == null)
                return "ERROR : NO SE ENCONTRO FOLIO";

            if (ordenDeTrabajo.Estado == "PROCESADO" || ordenDeTrabajo.Estado == "NUEVO" || ordenDeTrabajo.Estado == "SINRESPCASA")
            {
                String queryi = @"select r.* from DetalleTarjetasOrdenDeTrabajo dt left join registro_tc r on r.IdODTTarj = dt.ID where dt.FolioOrdenTrabajo = '"
                    + folio + @"'and (r.numero_de_cuenta is null or numero_tc is null)";

                String queryb = @"select count(*) as total from DetalleTarjetasOrdenDeTrabajo where FolioOrdenTrabajo = '" + folio + @"'";

                var registros = ctx.Database.SqlQuery<registro_tc>(queryi).ToList();
                var total = ctx.Database.SqlQuery<int>(queryb).FirstOrDefault();
                int sinterminar = registros.Count;

                if (sinterminar <= 0)
                {
                    String queryMarcar = @"update OrdenDeTrabajo set Estado = 'COMPLETA' where Folio = '" + folio + @"';";
                    ctx.Database.ExecuteSqlCommand(queryMarcar);
                    return "COMPLETA";
                }
                if (sinterminar < total)
                {
                    foreach (registro_tc r in registros)
                    {
                        maquila maq = ctx.maquila.Where(x => x.id == r.idmaquila).FirstOrDefault();
                        if (maq != null && (maq.num_cuenta == null || maq.nro_tarjeta == null))
                        {
                            String queryCopiarMaquila = @"insert into maquilaCanceladasODT (id, `nro-corr`, `nro-tarjeta`, nombre_titular, num_cuenta, domicilio, NumeroCalle, piso, localidad, Colonia, codigo_postal, provincia, TipoDomicilio, NumDocumento, TipoDocumento, Telefono, Sexo, FechaDeNacimiento, EstadoCivil, Hijos, Ocupacion, nombre_tarjethabiente, limite_compras, limite_credito, imp_adelantos, grupo_cuenta, producto, import, maquila, saldo_restante, total_movimientos, fecha_ultimo_movimiento, disponible, estado_operativo, fecha_disponible, fecha_ultima_modificacion, cliente_bx, cuenta_madre, programa, clave_cliente, 4ta_linea, usuario_web, email, referenciaCliente, MesesInactiva, Fondeo, CodigoPostalFiscal, Calle, ColoniaFiscal, EstadoFiscal, LocalidadFiscal, MunicipioFiscal, NumExterior, Estado, NumInterior, NombreCompleto, RFC, CLABE, CURP, IMSS, DCClaveCliente, DCConsecCliente, CodigoPostalFiscat, IdODTTarj, IdTipoNotificacionSMS) select id, `nro-corr`, `nro-tarjeta`, nombre_titular, num_cuenta, domicilio, NumeroCalle, piso, localidad, Colonia, codigo_postal, provincia, TipoDomicilio, NumDocumento, TipoDocumento, Telefono, Sexo, FechaDeNacimiento, EstadoCivil, Hijos, Ocupacion,nombre_tarjethabiente, limite_compras, limite_credito, imp_adelantos, grupo_cuenta, producto, import, maquila,saldo_restante, total_movimientos, fecha_ultimo_movimiento, disponible, estado_operativo, fecha_disponible,fecha_ultima_modificacion, cliente_bx, cuenta_madre, programa, clave_cliente, 4ta_linea, usuario_web, email,referenciaCliente, MesesInactiva, Fondeo, CodigoPostalFiscal, Calle, ColoniaFiscal, EstadoFiscal, LocalidadFiscal,MunicipioFiscal, NumExterior, Estado, NumInterior, NombreCompleto, RFC, CLABE, CURP, IMSS, DCClaveCliente, DCConsecCliente,CodigoPostalFiscat, IdODTTarj, IdTipoNotificacionSMS from maquila where id = " + r.idmaquila + @";";
                            String queryBorrarMaquila = @"delete from maquila where id = " + r.idmaquila + @";";
                            ctx.Database.ExecuteSqlCommand(queryCopiarMaquila);
                            ctx.Database.ExecuteSqlCommand(queryBorrarMaquila);
                        }
                        String queryCopiarRegistro = @"insert into registro_tcCanceladasODT (id, idmaquila, IdODTTarj, tipo_de_registro, numero_de_cuenta, numero_tc, nombre, NumDocumento, estado_operativo, fecha_alta, fecha_baja, num_anterior, num_nuevo, fecha_proceso_alta, fecha_proceso_baja, marca_alta_renov_inh, importe_der_emis_renov, cant_cuotas_emis_renov, codigo_afinidad, grupo, codigo_limite_compra, importe_limite_de_credito, codigo_de_producto, codigo_bco_sucursal, num_referencia_ABM, cant_cuotas_pend_emis_renov, fecha_ultima_actualizacion, filler, registroBroxel, embozada, tipo, codigoServicio, broxelEntrega, idRegTcOLD) select id, idmaquila, IdODTTarj, tipo_de_registro, numero_de_cuenta, numero_tc, nombre, NumDocumento, estado_operativo, fecha_alta,  fecha_baja, num_anterior, num_nuevo, fecha_proceso_alta, fecha_proceso_baja, marca_alta_renov_inh, importe_der_emis_renov, cant_cuotas_emis_renov, codigo_afinidad, grupo, codigo_limite_compra, importe_limite_de_credito, codigo_de_producto, codigo_bco_sucursal, num_referencia_ABM, cant_cuotas_pend_emis_renov, fecha_ultima_actualizacion, filler, registroBroxel, embozada, tipo, codigoServicio, broxelEntrega, idRegTcOLD from registro_tc where id = " + r.id + @";";
                        String queryBorrar = @"delete from registro_tc where id = " + r.id + @";";
                        ctx.Database.ExecuteSqlCommand(queryCopiarRegistro);
                        ctx.Database.ExecuteSqlCommand(queryBorrar);
                    }
                    String queryMarcar = @"update OrdenDeTrabajo set Estado = 'INCOMPLETO' where Folio = '" + folio + @"';";
                    ctx.Database.ExecuteSqlCommand(queryMarcar);
                    return "INCOMPLETA";
                }
                else
                {
                    String queryMarcar = @"update OrdenDeTrabajo set Estado = 'SINRESPCASA' where Folio = '" + folio + @"';";
                    ctx.Database.ExecuteSqlCommand(queryMarcar);
                    return "SINRESPCASA";
                }
            } 
            return "ERROR";
        }

        [WebMethod]
        public String CancelaODT(String folio)
        {
            var ctx = new broxelco_rdgEntities();
            var ordenDeTrabajo = ctx.OrdenDeTrabajo.FirstOrDefault(x => x.Folio == folio);
            if (ordenDeTrabajo == null)
                return "ERROR : NO SE ENCONTRO FOLIO";
            if (ordenDeTrabajo.Estado == "PROCESADO" || ordenDeTrabajo.Estado == "NUEVO" || ordenDeTrabajo.Estado == "SINRESPCASA")
            {
                String queryi = @"select r.* from DetalleTarjetasOrdenDeTrabajo dt left join registro_tc r on r.IdODTTarj = dt.ID where dt.FolioOrdenTrabajo = '" + folio + @"'and (r.numero_de_cuenta is null or numero_tc is null)";
                String queryb = @"select count(*) as total from DetalleTarjetasOrdenDeTrabajo where FolioOrdenTrabajo = '" + folio + @"'";

                var registros = ctx.Database.SqlQuery<registro_tc>(queryi).ToList();
                var total = ctx.Database.SqlQuery<int>(queryb).FirstOrDefault();
                int sinterminar = registros.Count;

                if (sinterminar == total && total > 0)
                {
                    foreach (registro_tc r in registros)
                    {
                        maquila maq = ctx.maquila.Where(x => x.id == r.idmaquila).FirstOrDefault();
                        if (maq != null && (maq.num_cuenta == null || maq.nro_tarjeta == null))
                        {
                            String queryCopiarMaquila =
                                @"insert into maquilaCanceladasODT (id, `nro-corr`, `nro-tarjeta`, nombre_titular, num_cuenta, domicilio, NumeroCalle, piso, localidad, Colonia, codigo_postal, provincia, TipoDomicilio, NumDocumento, TipoDocumento, Telefono, Sexo, FechaDeNacimiento, EstadoCivil, Hijos, Ocupacion, nombre_tarjethabiente, limite_compras, limite_credito, imp_adelantos, grupo_cuenta, producto, import, maquila, saldo_restante, total_movimientos, fecha_ultimo_movimiento, disponible, estado_operativo, fecha_disponible, fecha_ultima_modificacion, cliente_bx, cuenta_madre, programa, clave_cliente, 4ta_linea, usuario_web, email, referenciaCliente, MesesInactiva, Fondeo, CodigoPostalFiscal, Calle, ColoniaFiscal, EstadoFiscal, LocalidadFiscal, MunicipioFiscal, NumExterior, Estado, NumInterior, NombreCompleto, RFC, CLABE, CURP, IMSS, DCClaveCliente, DCConsecCliente, CodigoPostalFiscat, IdODTTarj, IdTipoNotificacionSMS) select id, `nro-corr`, `nro-tarjeta`, nombre_titular, num_cuenta, domicilio, NumeroCalle, piso, localidad, Colonia, codigo_postal, provincia, TipoDomicilio, NumDocumento, TipoDocumento, Telefono, Sexo, FechaDeNacimiento, EstadoCivil, Hijos, Ocupacion,nombre_tarjethabiente, limite_compras, limite_credito, imp_adelantos, grupo_cuenta, producto, import, maquila,saldo_restante, total_movimientos, fecha_ultimo_movimiento, disponible, estado_operativo, fecha_disponible,fecha_ultima_modificacion, cliente_bx, cuenta_madre, programa, clave_cliente, 4ta_linea, usuario_web, email,referenciaCliente, MesesInactiva, Fondeo, CodigoPostalFiscal, Calle, ColoniaFiscal, EstadoFiscal, LocalidadFiscal,MunicipioFiscal, NumExterior, Estado, NumInterior, NombreCompleto, RFC, CLABE, CURP, IMSS, DCClaveCliente, DCConsecCliente,CodigoPostalFiscat, IdODTTarj, IdTipoNotificacionSMS from maquila where id = " +
                                r.idmaquila + @";";
                            String queryBorrarMaquila = @"delete from maquila where id = " + r.idmaquila + @";";
                            ctx.Database.ExecuteSqlCommand(queryCopiarMaquila);
                            ctx.Database.ExecuteSqlCommand(queryBorrarMaquila);
                        }
                        String queryCopiarRegistro =
                            @"insert into registro_tcCanceladasODT (id, idmaquila, IdODTTarj, tipo_de_registro, numero_de_cuenta, numero_tc, nombre, NumDocumento, estado_operativo, fecha_alta, fecha_baja, num_anterior, num_nuevo, fecha_proceso_alta, fecha_proceso_baja, marca_alta_renov_inh, importe_der_emis_renov, cant_cuotas_emis_renov, codigo_afinidad, grupo, codigo_limite_compra, importe_limite_de_credito, codigo_de_producto, codigo_bco_sucursal, num_referencia_ABM, cant_cuotas_pend_emis_renov, fecha_ultima_actualizacion, filler, registroBroxel, embozada, tipo, codigoServicio, broxelEntrega, idRegTcOLD) select id, idmaquila, IdODTTarj, tipo_de_registro, numero_de_cuenta, numero_tc, nombre, NumDocumento, estado_operativo, fecha_alta,  fecha_baja, num_anterior, num_nuevo, fecha_proceso_alta, fecha_proceso_baja, marca_alta_renov_inh, importe_der_emis_renov, cant_cuotas_emis_renov, codigo_afinidad, grupo, codigo_limite_compra, importe_limite_de_credito, codigo_de_producto, codigo_bco_sucursal, num_referencia_ABM, cant_cuotas_pend_emis_renov, fecha_ultima_actualizacion, filler, registroBroxel, embozada, tipo, codigoServicio, broxelEntrega, idRegTcOLD from registro_tc where id = " +
                            r.id + @";";
                        String queryBorrar = @"delete from registro_tc where id = " + r.id + @";";
                        ctx.Database.ExecuteSqlCommand(queryCopiarRegistro);
                        ctx.Database.ExecuteSqlCommand(queryBorrar);
                        String queryMarcar = @"update OrdenDeTrabajo set Estado = 'CANCELADA' where Folio = '" + folio +
                                             @"';";
                        ctx.Database.ExecuteSqlCommand(queryMarcar);
                    }
                    return "COMPLETA";
                }
            }
            return "ERROR";
        }

        [WebMethod]
        public Int32 GeneraODT(String pNombreCompletoSolicitante,
            String correoNotificacion, String nombreEnTarjeta, String sexo, String fechaDeNacimiento, String rfc, String calleYNumero, String numeroInterior, String telefono,
            String localidad, String colonia, String codigoPostal,
            String tipoDeDocumentoIdent, String numeroDeDocumento, String correoElectronico)
        {

            return 0;
        }


        [WebMethod]
        public Int32 AgregarTarjetaACola(Int32 idDeRegistroTC)
        {
            var ctxT = new broxelco_rdgEntities();
            var registro = ctxT.registro_tc.Where(x => x.id == idDeRegistroTC).FirstOrDefault();
            var maquilaEntry = ctxT.maquila.Where(x => x.id == registro.id).FirstOrDefault();
            String producto = registro.codigo_de_producto;

            int titular = registro.tipo == "00" ? 1 : 0;
            Tarjeta tarjeta = Helper.GetTarjetaFromCuenta(registro.numero_de_cuenta);
            String cvc1 = registro.TransaRech.Substring(3, 1) + registro.TransaRech.Substring(0, 1) + registro.TransaRech.Substring(5, 1);
            String cuartaLinea = maquilaEntry.C4ta_linea;

            int medida;
            if (cuartaLinea != null && cuartaLinea.Length > 0)
                medida = 5;
            else
                medida = ctxT.productos_broxel.Where(x => x.codigo == producto).FirstOrDefault().TipoMedidasMaquila.Value;

            try
            {
                var ctx = new EmbosadoEntities();
                String tarjetaSinEspacios = tarjeta.NumeroTarjeta == "" || tarjeta.NumeroTarjeta == null ? "" : tarjeta.NumeroTarjeta.Replace(" ", "");
                String tarjetaConEspacios = tarjetaSinEspacios.Length == 0 ? "" : tarjetaSinEspacios.Insert(4, " ").Insert(9, " ").Insert(14, " ");
                String fechaSinFormato = tarjeta.FechaExpira.Replace("/", ""); //MMAA
                String fechaTrack = fechaSinFormato.Substring(2) + fechaSinFormato.Substring(0, 2); //AAMM
                String fechaConFormato = fechaSinFormato.Insert(2, "/");
                String denominacionTrimmed = tarjeta.NombreTarjeta.Trim();
                String denominacionConPadding = denominacionTrimmed.PadRight(26, ' ');
                String cvcReverso = tarjetaSinEspacios.Substring(12) + " " + tarjeta.Cvc2;

                //B + Tarjeta + ^ + DenPlastico + ^ + AAMM + 101 + CVC1 + CodProd
                String track1 = "B" + tarjetaSinEspacios + "^" + denominacionConPadding + "^" + fechaTrack + "101" + cvc1 + producto;

                //Tarjeta + = + AAMM + 101 + CVC1 + 000000 + Titular/Adicional + 140
                String track2 = tarjetaSinEspacios + "=" + fechaTrack + "101" + cvc1 + "000000" + titular + "140";

                var medidas = ctx.medidasEmbosado.FirstOrDefault(x => x.perfilID == medida);
                ctx.colaEmbosado.Add(new colaEmbosado
                {

                    NumeroTarjeta = tarjetaConEspacios.Length == 0 ? "" : "~EM%2;" + medidas.numeroX + ';' + medidas.numeroY + ';' + tarjetaConEspacios,
                    FechaExpiracion = "~EM%1;" + medidas.fechaX + ';' + medidas.fechaY + ';' + fechaConFormato,
                    Nombre = "~EM%1;" + medidas.nombreX + ';' + medidas.nombreY + ';' + tarjeta.NombreTarjeta,
                    Track1 = "~1%" + track1 + '?',
                    Track2 = "~2;" + track2 + '?',
                    Track3 = "",
                    Exitoso = 1,
                    EntranteId = 0,
                    CVC = "~EM%4;" + medidas.cvcX + ';' + medidas.cvcY + ';' + cvcReverso,
                    CuartaLinea = string.IsNullOrEmpty(cuartaLinea) ? "" :
                                    "~EM%1;" + medidas.cuartaLineaX + ';' + medidas.cuartaLineaY + ';' + cuartaLinea
                });
            }
            catch (Exception e)
            {
                return 0;
            }
            return 1;
        }

        [WebMethod]
        public string AltaClienteBroxel(String pNombreCorto, String pNombre, String pApPaterno, String pApMaterno,
            String pRazonSocial, DateTime pFechaNacimientoConstitucion, String pRfc, String pNombreRepLegal,
            String pApPaternoRepLegal, String pApMaternoRepLegal, DateTime pFecNacRepLegal, String pRfcRepLegal,
            String pCalle, String pNumExterior, String pNumInterior, String pColonia, String pDelegacionMunicipio,
            String pCp, String pEstado, String pClavePais, String pTelefono, String pCorreoContacto, Boolean pReportaPld,
            Boolean pEmiteFactura)
        {
            try
            {
                //Se genera el folioCliente
                var db = new broxelco_rdgEntities();
                var query = "SELECT COALESCE(substr(max(`ClaveAgrupacion`),7,6)+1,1,substr(max(`ClaveAgrupacion`),7,6)+1) AS folio FROM AgrupacionClientes WHERE SUBSTRING(`ClaveAgrupacion`,1,5)='" + DateTime.Now.ToString("yy") + "MYO'";

                var firstOrDefault = db.Database.SqlQuery<String>(query).FirstOrDefault();
                if (firstOrDefault != null)
                {
                    var folio = DateTime.Now.ToString("yy") + "MYO" + firstOrDefault.PadLeft(6).Replace(' ', '0');
                }

                //Se inserta en AgrupacionClientes
                


                return "";
            }
            catch (Exception exception)
            {

                return "Error en AltaCliente";
            }
        }
    }
}
