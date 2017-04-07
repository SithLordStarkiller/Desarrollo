using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.qrcode;
using PubliPayments.Entidades;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Entidades.Originacion.Modelos;
using PubliPayments.Utiles;
using System.Configuration;

namespace PubliPayments.Negocios.Originacion
{
    public class GeneraPdf
    {
        private string DirectorioPlantillasHtml {
            get { return ConfigurationManager.AppSettings["DirectorioPlantillasHTML"]; }
        }

        public void GenerarDocumentoPreventivo(DocumentoPreventivoDerhabienteModel modelo, ref PdfStamper pdfStamper)
        {
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            pdfFormFields.SetField("DocPrev_Nombre", modelo.Nombre??string.Empty);
            pdfFormFields.SetField("DocPrev_NoCredito", modelo.NoCredito??string.Empty);
            pdfFormFields.SetField("DocPrev_NoUnicoAsocATarjeta", EncriptaTarjeta.DesencriptarTarjeta(modelo.NoUnicoAsocATarjeta ?? string.Empty));
            pdfFormFields.SetField("DocPrev_Fecha", modelo.FechaCompleta ?? string.Empty);
            pdfFormFields.SetField("DocPrev_Lugar", modelo.Lugar ?? string.Empty);

            pdfFormFields.SetFieldProperty("DocPrev_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("DocPrev_NoCredito", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("DocPrev_NoUnicoAsocATarjeta", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("DocPrev_Fecha", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("DocPrev_Lugar", "setfflags", PdfFormField.FF_READ_ONLY, null);
        }

        public void GenerarCartaSesionIrrevocable(CartadeSesionIrrevocableModel modelo, ref PdfStamper pdfStamper)
        {
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            pdfFormFields.SetField("CartSesionI_Ciudad", modelo.Ciudad);
            pdfFormFields.SetField("CartSesionI_FechaDia", modelo.FechaDia);
            pdfFormFields.SetField("CartSesionI_FechaMes", modelo.FechaMes);
            pdfFormFields.SetField("CartSesionI_FechaAnio", modelo.FechaAnio);
            pdfFormFields.SetField("CartSesionI_NoSeguridadSocial", modelo.NoSeguridadSocial);
            pdfFormFields.SetField("Campo_NombreTrabajador", modelo.NombreTrabajador);


            pdfFormFields.SetFieldProperty("CartSesionI_Ciudad", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CartSesionI_FechaDia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CartSesionI_FechaMes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CartSesionI_FechaAnio", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CartSesionI_NoSeguridadSocial", "setfflags", PdfFormField.FF_READ_ONLY, null);
        }

        public void GeneraReciboTarjeta(ReciboTarjetaModel modelo, ref PdfStamper pdfStamper)
        {
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            #region Fields
            pdfFormFields.SetField("AcuseReciboT_FechaDia", modelo.Dia);
            pdfFormFields.SetField("AcuseReciboT_FechaMes", modelo.Mes);
            pdfFormFields.SetField("AcuseReciboT_FechaAnio", modelo.Anno);
            pdfFormFields.SetField("AcuseReciboT_Nombre", modelo.NombreTrabajador);
            pdfFormFields.SetField("AcuseReciboT_NoTarjeta", EncriptaTarjeta.DesencriptarTarjeta(modelo.NumeroTarjeta));
            pdfFormFields.SetField("AcuseReciboT_Banco", modelo.Banco);
            pdfFormFields.SetField("AcuseReciboT_NoCuenta", modelo.Credito);

            pdfFormFields.SetFieldProperty("AcuseReciboT_FechaDia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("AcuseReciboT_FechaMes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("AcuseReciboT_FechaAnio", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("AcuseReciboT_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("AcuseReciboT_NoTarjeta", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("AcuseReciboT_Banco", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("AcuseReciboT_NoCuenta", "setfflags", PdfFormField.FF_READ_ONLY, null);

            #endregion
        }        

        public void GeneraCaratulaContrato(CaratulaContratoModel modelo, ref PdfStamper pdfStamper)
        {
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            #region Fields
            pdfFormFields.SetField("CaratulaC_NumeroCredito", modelo.NumeroCredito);
            pdfFormFields.SetField("CaratulaC_NumeroUnicoAsociadoATarjeta", EncriptaTarjeta.DesencriptarTarjeta(modelo.NumeroTarjeta));
            pdfFormFields.SetField("CaratulaC_NombreCliente", modelo.NombreAcreditado);
            pdfFormFields.SetField("CaratulaC_NoSeguridadSocialCliente", modelo.NSS);
            pdfFormFields.SetField("CaratulaC_CURPCliente", modelo.CURP);
            pdfFormFields.SetField("CaratulaC_RFCCliente", modelo.RFC);
            pdfFormFields.SetField("CaratulaC_DomicilioCliente", modelo.Domicilio);
            pdfFormFields.SetField("CaratulaC_IdentiticacionCliente", modelo.Identificacion);
            pdfFormFields.SetField("CaratulaC_CatCostoAnualTotal", modelo.TasaInteres);
            pdfFormFields.SetField("CaratulaC_TasaInteresAnual", modelo.TasaInteres);
            pdfFormFields.SetField("CaratulaC_MontoCredito", modelo.MontoCredito);
            pdfFormFields.SetField("CaratulaC_MontoTotalAPagar", modelo.MontoTotal);
            //pdfFormFields.SetField("CaratulaC_ComisionPorApertura", modelo.ComisionXApertura);
            //pdfFormFields.SetField("CaratulaC_ComisionPorPrepago", modelo.ComisionXPrepago);
            //pdfFormFields.SetField("CaratulaC_ComisionPorCobranza", modelo.ComisionXCobranza);
            //pdfFormFields.SetField("CaratulaC_MetodologiaCalculoInteres", modelo.MetodologiaCalculoInteres);
            //pdfFormFields.SetField("CaratulaC_PlazoCredito", "");
            //pdfFormFields.SetField("CaratulaC_DescripcionAmortizaciones", "");
            pdfFormFields.SetField("CaratulaC_CondicionesFinancierasNumero", modelo.PlazoCredito);
            pdfFormFields.SetField("CaratulaC_CondicionesFinancierasMonto", modelo.AmortizacionMensual);
            //pdfFormFields.SetField("CaratulaC_PermisoMercadeoSi", string.Empty);
            //pdfFormFields.SetField("CaratulaC_PermisoMercadeoNo", string.Empty);
            //if (modelo.PermisoParaMercado != null)
            //{
            //    if ((bool)modelo.PermisoParaMercado)
            //        pdfFormFields.SetField("CaratulaC_PermisoMercadeoSi", "1");
            //    else 
            //        pdfFormFields.SetField("CaratulaC_PermisoMercadeoNo", "1");
            //}           
            //pdfFormFields.SetField("CaratulaC_PeriodicidadEdoCta", modelo.PeriodicidadEdoCta);
            //pdfFormFields.SetField("CaratulaC_ConsultaViaInternet", modelo.ConsultaViaInternet);
            //pdfFormFields.SetField("CaratulaC_DatosInscripcion", modelo.DatosInscripcionRegAdhesion);
            pdfFormFields.SetField("CaratulaC_LugarContrato", modelo.LugarFecha);
            pdfFormFields.SetField("CaratulaC_NombreContrato", modelo.NombreAcreditado);

            pdfFormFields.SetFieldProperty("CaratulaC_NumeroCredito", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_NumeroUnicoAsociadoATarjeta", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_NombreCliente", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_NoSeguridadSocialCliente", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_CURPCliente", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_RFCCliente", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_DomicilioCliente", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_IdentiticacionCliente", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_CatCostoAnualTotal", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_TasaInteresAnual", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_MontoCredito", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_MontoTotalAPagar", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_ComisionPorApertura", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_ComisionPorPrepago", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_ComisionPorCobranza", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_MetodologiaCalculoInteres", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_PlazoCredito", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_DescripcionAmortizaciones", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_CondicionesFinancierasNumero ", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_CondicionesFinancierasMonto", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_PermisoMercadeoSi", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_PermisoMercadeoNo", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_PeriodicidadEdoCta", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_ConsultaViaInternet", "setfflags", PdfFormField.FF_READ_ONLY, null);
            //pdfFormFields.SetFieldProperty("CaratulaC_DatosInscripcion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_LugarContrato", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("CaratulaC_NombreContrato", "setfflags", PdfFormField.FF_READ_ONLY, null);
            #endregion
        }

        public void GeneraSolicitudInscripcionCredito(SolicitudInscripcionCreditoModel modelo,ref PdfStamper pdfStamper)
        {
            #region Info Radios
            //LISTA RADIOBUTTONS
            //-Genero
            //    1 -> M = 1
            //    2 -> F = 2
            //-Regimen
            //    1 -> SEPARACION DE BIENES = 1
            //    2 -> SOCIEDAD CONYUGAL    = 2
            //    3 -> SOCIEDAD LEGAL       = 3
            //-Escolaridad
            //    1 -> SIN ESTUDIOS = SinEstudios 
            //    2 -> PRIMARIA = Primaria
            //    3 -> SECUNDARIA = Secundaria
            //    4 -> PREPARATORIA = Preparatoria
            //    5 -> TECNICO = Tecnico
            //    6 -> LICENCIATURA = Licenciatura
            //    7 -> POSGRADO = PosGrado
            //-Vivienda
            //    1 -> PROPIA = 0
            //    2 -> FAMILIARES = 1
            //-EstadoCivil
            //    1 -> SOLTERO = 2
            //    2 -> CASADO = 1
            //-Plazo
            //    1 -> 12 MESES = 12
            //    2 -> 18 MESES = 18
            //    3 -> 24 MESES = 24
            //    4 -> 30 MESES = 30
#endregion
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            #region Fields
            pdfFormFields.SetField("SolCredito_NoSeguroSocial", modelo.NSS);
            pdfFormFields.SetField("SolCredito_CURP", modelo.CURP);
            pdfFormFields.SetField("SolCredito_RFC", modelo.RFC);
            pdfFormFields.SetField("SolCredito_APaterno", modelo.ApellidoPaterno);
            pdfFormFields.SetField("SolCredito_AMaterno", modelo.ApellidoMaterno);
            pdfFormFields.SetField("SolCredito_Nombre", modelo.Nombre);
            pdfFormFields.SetField("SolCredito_CalleYNumDHab", modelo.Domicilio);
            pdfFormFields.SetField("SolCredito_ColOFraccionamientoDHab", modelo.Colonia);
            pdfFormFields.SetField("SolCredito_EntidadDHab", modelo.Entidad);
            pdfFormFields.SetField("SolCredito_MunicipioODelegDHab", modelo.Municipio);
            pdfFormFields.SetField("SolCredito_CPDHab", modelo.CodigoPostal);
            pdfFormFields.SetField("SolCredito_TipoIdentificacion", modelo.TipoIdentificacion);
            pdfFormFields.SetField("SolCredito_NoIdentificacion", modelo.NumeroIdentificacion);

            pdfFormFields.SetField("SolCredito_FechaIdentificacion_Dia", modelo.DiaIdentificacion);
            pdfFormFields.SetField("SolCredito_FechaIdentificacion_Mes", modelo.MesIdentificacion);
            pdfFormFields.SetField("SolCredito_FechaIdentificacion_Anio", modelo.AnoIdentificacion);



            pdfFormFields.SetField("SolCredito_Telefono_Lada", modelo.TelefonoLada);
            pdfFormFields.SetField("SolCredito_Telefono_Numero", modelo.Telefono);
            pdfFormFields.SetField("SolCredito_Celular_Numero", modelo.Celular);

            pdfFormFields.SetField("SolCredito_Genero_M", string.Empty);
            pdfFormFields.SetField("SolCredito_Genero_F", string.Empty);
            if (modelo.Genero == "1")
                pdfFormFields.SetField("SolCredito_Genero_M", "1");
            else if (modelo.Genero == "2")
                pdfFormFields.SetField("SolCredito_Genero_F", "1");

            pdfFormFields.SetField("SolCredito_EMail", modelo.CorreoElectronico);
            pdfFormFields.SetField("SolCredito_Soltero", string.Empty);
            pdfFormFields.SetField("SolCredito_Casado", string.Empty);
            if (modelo.EstadoCivil == "1")
                pdfFormFields.SetField("SolCredito_Casado", "1");
            else if (modelo.EstadoCivil == "2")
                pdfFormFields.SetField("SolCredito_Soltero", "1");

            pdfFormFields.SetField("SolCredito_RPMatrim_SepDeBienes", string.Empty);
            pdfFormFields.SetField("SolCredito_RPMatrim_SocConyugal", string.Empty);
            pdfFormFields.SetField("SolCredito_RPMatrim_SocLegal", string.Empty);
            if (modelo.Regimen == "1")
                pdfFormFields.SetField("SolCredito_RPMatrim_SepDeBienes", "1");
            else if (modelo.Regimen == "2")
                pdfFormFields.SetField("SolCredito_RPMatrim_SocConyugal", "1");
            else if (modelo.Regimen == "3")
                pdfFormFields.SetField("SolCredito_RPMatrim_SocLegal", "1");

            pdfFormFields.SetField("SolCredito_Vivienda_Propia", string.Empty);
            pdfFormFields.SetField("SolCredito_Vivienda_DeFamiliares", string.Empty);
            if (modelo.ANombreDh == "SI")
                pdfFormFields.SetField("SolCredito_Vivienda_Propia", "1");
            else 
                pdfFormFields.SetField("SolCredito_Vivienda_DeFamiliares", "1");
            

            pdfFormFields.SetField("SolCredito_NombreEmpresa", modelo.NombrePatron);
            pdfFormFields.SetField("SolCredito_NoRegPatronal", modelo.NRPPatron ); 
            pdfFormFields.SetField("SolCredito_TelTrabajo_Lada", modelo.EmpresaLada);
            pdfFormFields.SetField("SolCredito_TelTrabajo_Numero", modelo.EmpresaTelefono);
            pdfFormFields.SetField("SolCredito_TelTrabajo_Extension", modelo.EmpresaExtension);

            pdfFormFields.SetField("SolCredito_HorarioTrabajo_InicioHr", modelo.HorarioHoraDe);
            pdfFormFields.SetField("SolCredito_HorarioTrabajo_InicioMin", modelo.HorarioMinutosDe);
            pdfFormFields.SetField("SolCredito_HorarioTrabajo_FinHr", modelo.HorarioHoraA);
            pdfFormFields.SetField("SolCredito_HorarioTrabajo_FinMin", modelo.HorarioMinutosA);
            pdfFormFields.SetField("SolCredito_RefFam1_APaterno", modelo.ReferenciaApellidoPaterno1);
            pdfFormFields.SetField("SolCredito_RefFam1_AMaterno", modelo.ReferenciaApellidoMaterno1);
            pdfFormFields.SetField("SolCredito_RefFam1_Nombre", modelo.ReferenciaNombre1);
            pdfFormFields.SetField("SolCredito_RefFam1_TelLada", modelo.ReferenciaLada1);
            pdfFormFields.SetField("SolCredito_RefFam1_TelNumero", modelo.ReferenciaTelefono1);
            pdfFormFields.SetField("SolCredito_RefFam1_NumCelular", modelo.ReferenciaCelular1);
            pdfFormFields.SetField("SolCredito_RefFam2_APaterno", modelo.ReferenciaApellidoPaterno2);
            pdfFormFields.SetField("SolCredito_RefFam2_AMaterno", modelo.ReferenciaApellidoMaterno2);
            pdfFormFields.SetField("SolCredito_RefFam2_Nombre", modelo.ReferenciaNombre2);
            pdfFormFields.SetField("SolCredito_RefFam2_TelLada", modelo.ReferenciaLada2);
            pdfFormFields.SetField("SolCredito_RefFam2_TelNumero", modelo.ReferenciaTelefono2);
            pdfFormFields.SetField("SolCredito_RefFam2_NumCelular", modelo.ReferenciaCelular2);
            pdfFormFields.SetField("SolCredito_Benef_APaterno", modelo.Benef_ApPaterno);
            pdfFormFields.SetField("SolCredito_Benef_AMaterno", modelo.Benef_ApMaterno);
            pdfFormFields.SetField("SolCredito_Benef_Nombre", modelo.Benef_Nombre);
            pdfFormFields.SetField("SolCredito_DMCred_DescuentoPensionA", modelo.PensionAlimenticia);

            pdfFormFields.SetField("SolCredito_DMCred_PlazoM12", string.Empty);
            pdfFormFields.SetField("SolCredito_DMCred_PlazoM18", string.Empty);
            pdfFormFields.SetField("SolCredito_DMCred_PlazoM24", string.Empty);
            pdfFormFields.SetField("SolCredito_DMCred_PlazoM30", string.Empty);
            if (modelo.Plazo == "12")
                pdfFormFields.SetField("SolCredito_DMCred_PlazoM12", "1");
            else if (modelo.Plazo == "18") 
                pdfFormFields.SetField("SolCredito_DMCred_PlazoM18", "1");
            else if (modelo.Plazo == "24") 
                pdfFormFields.SetField("SolCredito_DMCred_PlazoM24", "1");
            else if (modelo.Plazo == "30") 
                pdfFormFields.SetField("SolCredito_DMCred_PlazoM30", "1");
            
            pdfFormFields.SetField("SolCredito_DMCred_MManoObra", modelo.MontoManoObra);
            pdfFormFields.SetField("SolCredito_DMCred_MCredSolic", modelo.MontoCredito);
            pdfFormFields.SetField("SolCredito_NoCredXInfonavit", modelo.NumeroCredito);
            pdfFormFields.SetField("SolCredito_NoUnicoAsocATarjeta", EncriptaTarjeta.DesencriptarTarjeta(modelo.NumeroTarjeta));
            pdfFormFields.SetField("SolCredito_CLABEAsocATrabajador", modelo.Clabe);
            pdfFormFields.SetField("SolCredito_Ciudad", modelo.Ciudad);
            pdfFormFields.SetField("SolCredito_FechaDia", modelo.Dia);
            pdfFormFields.SetField("SolCredito_FechaMes", modelo.Mes);
            pdfFormFields.SetField("SolCredito_FechaAnio", modelo.Ano);

            pdfFormFields.SetFieldProperty("SolCredito_NoSeguroSocial", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_CURP", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RFC", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_APaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_AMaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_CalleYNumDHab", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_ColOFraccionamientoDHab", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_EntidadDHab", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_MunicipioODelegDHab", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_CPDHab", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_TipoIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NoIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_FechaValidezIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Telefono_Lada", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Telefono_Numero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Celular_Numero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Genero_M", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Genero_F", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_EMail", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Soltero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Casado", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RPMatrim_SepDeBienes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RPMatrim_SocConyugal ", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RPMatrim_SocLegal", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NombreEmpresa", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NoRegPatronal", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_TelTrabajo_Lada", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_TelTrabajo_Numero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_TelTrabajo_Extension", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_HorarioTrabajo_InicioHr", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_HorarioTrabajo_InicioMin", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_HorarioTrabajo_FinHr", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_HorarioTrabajo_FinMin", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_APaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_AMaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_TelLada", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_TelNumero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_NumCelular", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_APaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_AMaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_TelLada", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_TelNumero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_NumCelular", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Benef_APaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Benef_AMaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Benef_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_DescuentoPensionA", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_PlazoM12", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_PlazoM18", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_PlazoM24", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_PlazoM30", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_MManoObra", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_MCredSolic", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NoCredXInfonavit", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NoUnicoAsocATarjeta", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_CLABEAsocATrabajador", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Ciudad", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_FechaDia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_FechaMes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_FechaAnio", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Vivienda_Propia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Vivienda_DeFamiliares", "setfflags", PdfFormField.FF_READ_ONLY, null);

            #endregion
        }

        public void GeneraConsultaBuro(ConsultaBuroModel modelo,ref PdfStamper pdfStamper)
        {
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            pdfFormFields.SetField("Campo_Nss", modelo.NSS);
            pdfFormFields.SetField("Campo_Dia", modelo.FechaDia ?? string.Empty);
            pdfFormFields.SetField("Campo_Mes", modelo.FechaMes ?? string.Empty);
            pdfFormFields.SetField("Campo_Anio", modelo.FechaAnio ?? string.Empty);

            pdfFormFields.SetField("Campo_Nombre", modelo.Nombre);
            pdfFormFields.SetField("Campo_EntidadFinanciera", modelo.EntidadFinanciera??string.Empty);
            

            pdfFormFields.SetFieldProperty("Campo_Nss", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Campo_Dia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Campo_Mes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Campo_Anio", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Campo_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("Campo_EntidadFinanciera", "setfflags", PdfFormField.FF_READ_ONLY, null);

        }


        public string Upload(DocumentoPreventivoDerhabienteModel modelo, string visita, int orden, string campo, string url,
            string fullpath)
        {


            if (modelo == null)
                throw new Exception(" Modelo nulo ");


            Logger.WriteLine(Logger.TipoTraceLog.Log, 0, "GeneraPdf", "Upload DocumentoPreventivo Orden: " + orden.ToString(CultureInfo.InvariantCulture));
            try
            {

                var htmlTemplate = ConfigurationManager.AppSettings["DocumentoPreventivoHTMLTemplate"];                
                var generador = new GeneradorPdfDocumentoPreventivo(htmlTemplate, fullpath, DirectorioPlantillasHtml);
                generador.GenerarPdfxModelo(modelo);

                //var pdfTemplate = ConfigurationManager.AppSettings["pdfTemplates"];
                //const string nombrePdf = "DocumentoPreventivo";
                //const string ext = ".pdf";
                //var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                //var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));
                //GenerarDocumentoPreventivo(modelo, ref pdfStamper);
                //pdfStamper.FormFlattening = false;
                //pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf",
                    "Upload - OK: " + fullpath + " - url: " + url);

                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Upload DocumentoPreventivo Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error:" + ex.Message);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento DocumentoPreventivo", "Error as generar archivo");
                return "-1";
            }
        }

        public string Upload(CartadeSesionIrrevocableModel modelo, string visita, int orden, string campo, string url,
            string fullpath)
        {
            if (modelo == null)
                throw new Exception(" Modelo nulo ");

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "Upload CartadeSesionIrrevocable Orden: " + orden.ToString(CultureInfo.InvariantCulture));
            try
            {
                var htmlTemplate = ConfigurationManager.AppSettings["CartaSesionIrrevocableHTMLTemplate"];               
                var generador = new GeneradorPdfCartaSesionIrrevocable(htmlTemplate, fullpath, DirectorioPlantillasHtml);
                generador.GenerarPdfxModelo(modelo);

                //var pdfTemplate = ConfigurationManager.AppSettings["pdfTemplates"];
                //const string nombrePdf = "CartadeSesionIrrevocable";
                //const string ext = ".pdf";
                //var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                //var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));

                //GenerarCartaSesionIrrevocable(modelo, ref pdfStamper);

                //pdfStamper.FormFlattening = false;
                //pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf",
                    "Upload - OK: " + fullpath + " - url: " + url);

                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Upload CartadeSesionIrrevocable Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error:" + ex.Message);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento CartadeSesionIrrevocable", "Error as generar archivo");
                return "-1";
            }
        }

        public string Upload(ReciboTarjetaModel modelo,string visita,int orden,string campo,string url,string fullpath)
        {


            if (modelo == null)
                throw new Exception(" Modelo nulo ");
            

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "Upload ReciboTarjeta Orden: " + orden.ToString(CultureInfo.InvariantCulture));
            try
            {
                var htmlTemplate = ConfigurationManager.AppSettings["ReciboTarjetaHTMLTemplate"];
                new GeneradorPdfReciboTarjeta(htmlTemplate, fullpath, DirectorioPlantillasHtml)
                    .GenerarPdfxModelo(modelo);

                //var pdfTemplate = ConfigurationManager.AppSettings["pdfTemplates"];
                //const string nombrePdf = "ReciboTarjeta";
                //const string ext = ".pdf";
                //var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                //var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));
                //GeneraReciboTarjeta(modelo, ref pdfStamper);
                //pdfStamper.FormFlattening = false;
                //pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf",
                    "Upload - OK: " + fullpath + " - url: " + url);

                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Upload ReciboTarjeta Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error:" + ex.Message );
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento ReciboTarjeta", "Error as generar archivo");
                return "-1";
            }
        }

        public string Upload(CaratulaContratoModel modelo, string visita, int orden, string campo, string url, string fullpath)
        {
            if (modelo == null)
                throw new Exception(" Modelo nulo ");
                
            try
            {
                var pdfTemplate = ConfigurationManager.AppSettings["pdfTemplates"];

                const string nombrePdf = "CaratulaContrato";
                const string ext = ".pdf";
                
                var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));

                GeneraCaratulaContrato(modelo, ref pdfStamper);

                pdfStamper.FormFlattening = false;
                pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "Upload - OK: " + fullpath + " - url:" + url);
                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf",
                    "Upload CaratulaContrato Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error:" + ex.Message);

                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento CaratulaContrato", "Error al generar archivo");
                return "-1";
            }
        }

        public string Upload(SolicitudInscripcionCreditoModel modelo, string visita, int orden, string campo, string url, string fullpath)
        {
            if (modelo == null)
                throw new Exception(" Modelo nulo ");
            
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "Upload SolicitudInscripcionCredito Orden: " + orden.ToString(CultureInfo.InvariantCulture));

                string pdfTemplate = ConfigurationManager.AppSettings["pdfTemplates"];

                const string nombrePdf = "SolicitudInscripcionCredito";
                const string ext = ".pdf";
                
                var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));

                GeneraSolicitudInscripcionCredito(modelo, ref pdfStamper);

                pdfStamper.FormFlattening = false;
                pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf",
                    "Upload - OK: " + fullpath + " - url:" + url);
                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf",
                    "Upload Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error:" + ex.Message);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento SolicitudInscripcionCredito", "Error al generar el archivo");
                return "-1";
            }
        }

        public string UploadFront(SolicitudInscripcionCreditoModel modelo, string url, string fullpath)
        {
            if (modelo == null)
                throw new Exception(" Modelo nulo ");

            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "UploadFront SolicitudInscripcionCreditoFront ");

                string pdfTemplate = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacionFront"];

                const string nombrePdf = "SolicitudInscripcionCreditoFront";
                const string ext = ".pdf";

                var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));

                GeneraSolicitudInscripcionCredito(modelo, ref pdfStamper);

                pdfStamper.FormFlattening = false;
                pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf",
                    "UploadFront - OK: " + fullpath + " - url:" + url);
                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf","UploadFront");
                return "-1";
            }
        }

        public string Upload(ConsultaBuroModel modelo, string visita, int orden, string campo, string url, string fullpath)
        {
            try
            {
                if (modelo == null)
                    throw new Exception("Modelo nulo ");

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "Upload ConsultaBuro Orden: " + orden.ToString(CultureInfo.InvariantCulture));

                var htmlTemplate = ConfigurationManager.AppSettings["ConsultaBuroHTMLTemplate"];
                var generador = new GeneradorPdfConsultaBuro(htmlTemplate, fullpath);
                generador.GenerarPdfxModelo(modelo);

                //const string nombrePdf = "ConsultaBuro";
                //const string ext = ".pdf";
                //var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                //var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));
                //GeneraConsultaBuro(modelo, ref pdfStamper);
                //pdfStamper.FormFlattening = false;
                //pdfStamper.Close();

                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf",
                    "Upload - OK: " + fullpath + " - url:" + url);
                return url;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf",
                    "Upload ConsultaBuro Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error: " + ex.Message);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento ConsultaBuro", "Error al generar el archivo");
                return "-1";
            }
        }


        public void Rutas(ref string fullpath, ref string url, string visita, string orden, string campo = "",string ext = "pdf")
        {
            try
            {
                var fase = "";
                if (fullpath == null) throw new ArgumentNullException("fullpath");
                var directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];
                var urlmagenes = ConfigurationManager.AppSettings["CWDirectorioDocumentosOriginacionDescarga"];

                if (visita != "")
                    fase = @"\" + (visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion"));
                
                var path = directorioImagenes + orden + fase;

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                fullpath = path + @"\";
                if (campo != "")
                    fullpath += campo + "." + ext;
                
                url = urlmagenes + orden + "/" + fase + "/" + campo + "." + ext;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Rutas - Error: " + ex.Message);
            }
        }

        public void RutasOriginacion(ref string fullpath, ref string url, string visita, string orden, string campo = "", string ext = "pdf")
        {
            try
            {
                var fase = "";
                if (fullpath == null) throw new ArgumentNullException("fullpath");
                var directorioImagenes = ConfigurationManager.AppSettings["DirectorioImagenesFormiik"];
                var urlmagenes = ConfigurationManager.AppSettings["urlImagenesFormiik"];

                if (visita != "")
                    fase = @"\" + (visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion"));

                var path = directorioImagenes + orden + fase;

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                fullpath = path + @"\";
                if (campo != "")
                    fullpath += campo + "." + ext;

                url = urlmagenes + orden + "/" + fase + "/" + campo + "." + ext;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Rutas - Error: " + ex.Message);
            }
        }

        public Dictionary<string,string> ObtenerPaths(string orden)
        {
            var lista = new Dictionary<string, string>();
            var fullpath = "";
            var url = "";

            try
            {
                for(var i = 1 ; i <= 3 ; i++ )
                {
                    Rutas(ref fullpath, ref url, i.ToString(CultureInfo.InvariantCulture), orden.ToString(CultureInfo.InvariantCulture));
                    
                    var rutasArchivos = Directory.GetFiles(fullpath).ToList();

                    foreach (var rutaArchivo in rutasArchivos)
                    {
                        if ((rutaArchivo.Contains("Foto") || rutaArchivo.Contains("ListaNominal")) && !rutaArchivo.Contains("FotoBiometrico"))
                        {
                            var file = Path.GetFileNameWithoutExtension(rutaArchivo);
                            
                            Rutas(ref fullpath, ref url, i.ToString(CultureInfo.InvariantCulture), orden.ToString(CultureInfo.InvariantCulture), file,"jpg");
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "ObtenerPaths - Ruta: " + fullpath);
                            if (File.Exists(fullpath))
                                lista.Add(file,fullpath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "ObtenerPaths - Error: " + ex.Message);
            }
            return lista;
        }

        public Dictionary<string, string> ObtenerPathsOriginacion(string orden)
        {
            var lista = new Dictionary<string, string>();
            var fullpath = "";
            var url = "";

            try
            {
                for (var i = 1; i <= 3; i++)
                {
                    RutasOriginacion(ref fullpath, ref url, i.ToString(CultureInfo.InvariantCulture), orden.ToString(CultureInfo.InvariantCulture));
                    
                    var rutasArchivos = Directory.GetFiles(fullpath).ToList();

                    foreach (var rutaArchivo in rutasArchivos)
                    {
                        var file = Path.GetFileNameWithoutExtension(rutaArchivo);

                        RutasOriginacion(ref fullpath, ref url, i.ToString(CultureInfo.InvariantCulture), orden.ToString(CultureInfo.InvariantCulture), file,"jpg");

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "Orden : " + orden + "Ruta Fisica : " + fullpath);

                        if (File.Exists(fullpath) && !lista.ContainsKey(file))
                            lista.Add(file, fullpath);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "ObtenerPaths Orden : " + orden + " Error: " + ex.Message);
            }
            return lista;
        }

        public void GeneraPdfCompleto(int orden, string campo = "Unificado")
        {
            var fullpath = "";
            var url = "";
            const string visita = "";

            string[] fotosOrden = {"FotoSolCredito","FotoIdOficial",
                                   "FotoCompDomicilio","FotoAvRetencion",
                                   "FotoCarta_CesionIrrev","FotoBuroCredito",
                                   "FotoAcuRecTarjeta","FotoCarContrato",
                                   "FotoContrato1",
                                   "FotoContrato2","FotoContrato3",
                                   "FotoContrato4","FotoContrato5",
                                   "FotoContrato6","FotoContrato7",
                                   "FotoContrato8","FotoContrato9",
                                   "FotoContrato9","FotoContrato10",
                                  "FotoActNacimiento","FotoConsVigINE",
                                  "FotoEdoCta_CLABE","FotoDH_IDEOficial",
                                  "FotoDH_FirmasInd","FotoDoc_PrevDerecho",
                                  "FotoActaNacimientoHijos","FotoActaMatrimonio"
                                  };

           
            try
            {
                var sourceFiles = ObtenerPaths(orden.ToString(CultureInfo.InvariantCulture));

                if(sourceFiles.Count == 0)
                    throw new Exception("No existe ningun archivo para realizar el documento unificado de la orden " + orden.ToString(CultureInfo.InvariantCulture));

                Rutas(ref fullpath, ref url, visita, orden.ToString(CultureInfo.InvariantCulture), campo);

                var document = new Document(PageSize.A4, 3, 3, 15, 3);
                var pdfNuevo = PdfWriter.GetInstance(document,new FileStream(fullpath,FileMode.Create));
                document.Open();
                foreach (var foto in fotosOrden)
                {
                    foreach (var file in sourceFiles)
                    {
                        if (foto == file.Key)
                        {
                            var imagen = Image.GetInstance(file.Value);
                            imagen.Alignment = Element.ALIGN_MIDDLE;
                            imagen.ScaleAbsoluteWidth(600);
                            imagen.ScaleAbsoluteHeight(840);
                            document.Add(imagen);
                            break;
                        }
                    }
                }
                document.Close();
                pdfNuevo.Close();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "GeneraPdfCompleto - Error: " + error);
                Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Error Documento Unificado Originacion", "Error al generar el archivo unificado Error - " + error);
            }
        }

        public string GeneraPdfCompletoOriginacion(int orden, string campo = "UnificadoOriginacion")
        {
            //var pathUnificado = ConfigurationManager.AppSettings["DirectorioImagenesFormiik"] + orden + "\\" + campo + ".pdf";
            //var urlUnificado = ConfigurationManager.AppSettings["urlImagenesFormiik"] + orden + "/" + campo + ".pdf";
            var fullpath = "";
            var url = "";
            const string visita = "";

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "Orden : " + orden + " Generando PDF");

            //string[] fotosOrden = { "FotoSolCredito","FotoIdOficial",
            //                          "FotoConsVigINE","FotoActNacimiento",
            //                          "FotoCompDomicilio","FotoEdoCta_CLABE",
            //                          "FotoAvRetencion","FotoBuroCredito",
            //                          "FotoAcuRecTarjeta","FotoCarContrato",
            //                          "FotoContrato1","FotoContrato2",
            //                          "FotoContrato3","FotoContrato4",
            //                          "FotoContrato5","FotoContrato6",
            //                          "FotoCarta_CesionIrrev","FotoDerForCre",
            //                          "FotoIdConInd","DocListaNominal",
            //                          "FotoContrato7","FotoContrato8",
            //                          "FotoContrato9","FotoContrato9",
            //                          "FotoContrato10","FotoActaNacimientoHijos",
            //                      "FotoActaMatrimonio"};

            string[] fotosOrden = {"FotoSolCredito","FotoIdOficialAnverso",
                                   "FotoIdOficialReverso","FotoConsVigINE",
                                   "FotoActNacimiento","FotoEdoCta_CLABE",
                                   "FotoCURP", "FotoCompDomicilio",
                                   "FotoActaNacimientoHijos","FotoActaMatrimonio",
                                   "FotoAvRetencion","FotoOficio",
                                   "FotoBuroCredito",
                                   "FotoCarContrato", "FotoContrato1", 
                                   "FotoContrato2", "FotoContrato3",
                                   "FotoContrato4", "FotoContrato5", 
                                   "FotoContrato6", "FotoContrato7",
                                   "FotoContrato8", "FotoContrato9",
                                   "FotoContrato10", "FotoAcuRecTarjeta",
                                   "FotoDoc_PrevDerecho","FotoDoc_PrevDerecho2",
                                   "FotoCarta_CesionIrrev",
                                   "FotoDH_IDEOficial", "FotoDH_FirmasIndAnv" , 
                                   "FotoDH_FirmasIndRev"
                                  };

            try
            {
                var sourceFiles = ObtenerPathsOriginacion(orden.ToString(CultureInfo.InvariantCulture));

                if (sourceFiles.Count == 0)
                    throw new Exception("No existe ningun archivo para realizar el documento unificado de la orden " + orden.ToString(CultureInfo.InvariantCulture));

                RutasOriginacion(ref fullpath, ref url, visita, orden.ToString(CultureInfo.InvariantCulture), campo);

                var document = new Document(PageSize.A4, 3, 3, 15, 3);
                var pdfNuevo = PdfWriter.GetInstance(document, new FileStream(fullpath, FileMode.Create));
                document.Open();
                foreach (var foto in fotosOrden)
                {
                    foreach (var file in sourceFiles)
                    {
                        if (foto == file.Key)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "GeneraPdf", "Orden : " + orden + "Agregando Imagen : " + foto + "Ruta : " + file.Value);
                            var imagen = Image.GetInstance(file.Value);
                            imagen.Alignment = Element.ALIGN_MIDDLE;
                            imagen.ScaleAbsoluteWidth(600);
                            imagen.ScaleAbsoluteHeight(840);
                            document.Add(imagen);
                            break;
                        }
                    }
                }
                document.Close();
                pdfNuevo.Close();
                return url;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf", "Orden : " + orden + " Error: " + error);
                return "Error:" + ex.Message;
            }
        }

        #region Solicitud crédito Simplificado
        public void GeneraSolicitudInscripcionCredito(SolicitudInscripcionCreditoSimpleModel modelo, ref PdfStamper pdfStamper)
        {
            #region Info Radios
            //LISTA RADIOBUTTONS
            //-Genero
            //    1 -> M = 1
            //    2 -> F = 2
            //-Regimen
            //    1 -> SEPARACION DE BIENES = 1
            //    2 -> SOCIEDAD CONYUGAL    = 2
            //    3 -> SOCIEDAD LEGAL       = 3
            //-Escolaridad
            //    1 -> SIN ESTUDIOS = SinEstudios 
            //    2 -> PRIMARIA = Primaria
            //    3 -> SECUNDARIA = Secundaria
            //    4 -> PREPARATORIA = Preparatoria
            //    5 -> TECNICO = Tecnico
            //    6 -> LICENCIATURA = Licenciatura
            //    7 -> POSGRADO = PosGrado
            //-Vivienda
            //    1 -> PROPIA = 0
            //    2 -> FAMILIARES = 1
            //-EstadoCivil
            //    1 -> SOLTERO = 2
            //    2 -> CASADO = 1
            //-Plazo
            //    1 -> 12 MESES = 12
            //    2 -> 18 MESES = 18
            //    3 -> 24 MESES = 24
            //    4 -> 30 MESES = 30
            #endregion
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            #region Fields
            pdfFormFields.SetField("SolCredito_NoSeguroSocial", modelo.nss);
            pdfFormFields.SetField("SolCredito_CURP", modelo.curp);
            pdfFormFields.SetField("SolCredito_RFC", modelo.rfc);
            pdfFormFields.SetField("SolCredito_APaterno", modelo.apellidoPaterno);
            pdfFormFields.SetField("SolCredito_AMaterno", modelo.apellidoMaterno);
            pdfFormFields.SetField("SolCredito_Nombre", modelo.nombre);
            pdfFormFields.SetField("SolCredito_CalleYNumDHab", modelo.calle + (String.IsNullOrEmpty(modelo.numCasaExterior) ? "" : "Ext. " + modelo.numCasaExterior) + (String.IsNullOrEmpty(modelo.numCasaInterior) ? "" : "INT. " + modelo.numCasaInterior));
            pdfFormFields.SetField("SolCredito_ColOFraccionamientoDHab", modelo.colonia);
            pdfFormFields.SetField("SolCredito_EntidadDHab", modelo.entidad);
            pdfFormFields.SetField("SolCredito_MunicipioODelegDHab", modelo.municipio);
            pdfFormFields.SetField("SolCredito_CPDHab", modelo.cp);
            pdfFormFields.SetField("SolCredito_TipoIdentificacion", modelo.identificacionTipo);
            pdfFormFields.SetField("SolCredito_NoIdentificacion", modelo.identificacionNum);

            pdfFormFields.SetField("SolCredito_FechaIdentificacion_Dia", modelo.identificacionFechaDia);
            pdfFormFields.SetField("SolCredito_FechaIdentificacion_Mes", modelo.identificacionFechaMes);
            pdfFormFields.SetField("SolCredito_FechaIdentificacion_Anio", modelo.identificacionFechaAno);

            pdfFormFields.SetField("SolCredito_Telefono_Lada", modelo.telefonoLada);
            pdfFormFields.SetField("SolCredito_Telefono_Numero", modelo.telefonoNumero);
            pdfFormFields.SetField("SolCredito_Celular_Numero", modelo.celular);

            pdfFormFields.SetField("SolCredito_Genero_M", string.Empty);
            pdfFormFields.SetField("SolCredito_Genero_F", string.Empty);
            if (modelo.genero == "1")
                pdfFormFields.SetField("SolCredito_Genero_M", "1");
            else if (modelo.genero == "2")
                pdfFormFields.SetField("SolCredito_Genero_F", "1");

            pdfFormFields.SetField("SolCredito_EMail", modelo.email);
            pdfFormFields.SetField("SolCredito_Soltero", string.Empty);
            pdfFormFields.SetField("SolCredito_Casado", string.Empty);
            if (modelo.estadoCivil == "1")
                pdfFormFields.SetField("SolCredito_Casado", "1");
            else if (modelo.estadoCivil == "2")
                pdfFormFields.SetField("SolCredito_Soltero", "1");

            pdfFormFields.SetField("SolCredito_RPMatrim_SepDeBienes", string.Empty);
            pdfFormFields.SetField("SolCredito_RPMatrim_SocConyugal", string.Empty);
            pdfFormFields.SetField("SolCredito_RPMatrim_SocLegal", string.Empty);
            if (modelo.regimen == "1")
                pdfFormFields.SetField("SolCredito_RPMatrim_SepDeBienes", "1");
            else if (modelo.regimen == "2")
                pdfFormFields.SetField("SolCredito_RPMatrim_SocConyugal", "1");
            else if (modelo.regimen == "3")
                pdfFormFields.SetField("SolCredito_RPMatrim_SocLegal", "1");

            pdfFormFields.SetField("SolCredito_Vivienda_Propia", string.Empty);
            pdfFormFields.SetField("SolCredito_Vivienda_DeFamiliares", string.Empty);
            if (modelo.tipoDevivienda == "1")
                pdfFormFields.SetField("SolCredito_Vivienda_Propia", "1");
            else if (modelo.tipoDevivienda == "2")
                pdfFormFields.SetField("SolCredito_Vivienda_DeFamiliares", "1");

            pdfFormFields.SetField("SolCredito_NoDepEconomicos", modelo.dependientes);
            pdfFormFields.SetField("SolCredito_NombreEmpresa", modelo.empresaNombre);

            pdfFormFields.SetField("SolCredito_NombreEmpresa", modelo.empresaNombre);
            pdfFormFields.SetField("SolCredito_NoRegPatronal", modelo.empresaNRP);
            pdfFormFields.SetField("SolCredito_TelTrabajo_Lada", modelo.empresaLada);
            pdfFormFields.SetField("SolCredito_TelTrabajo_Numero", modelo.empresaTelefono);
            pdfFormFields.SetField("SolCredito_TelTrabajo_Extension", modelo.empresaExt);

            pdfFormFields.SetField("SolCredito_HorarioTrabajo_InicioHr", modelo.horarioLaboralEntrada);
            pdfFormFields.SetField("SolCredito_HorarioTrabajo_InicioMin", string.Empty);
            pdfFormFields.SetField("SolCredito_HorarioTrabajo_FinHr", modelo.horarioLaboralSalida);
            pdfFormFields.SetField("SolCredito_HorarioTrabajo_FinMin", string.Empty);
            pdfFormFields.SetField("SolCredito_RefFam1_APaterno", modelo.referencia1ApellidoPaterno);
            pdfFormFields.SetField("SolCredito_RefFam1_AMaterno", modelo.referencia1ApellidoMaterno);
            pdfFormFields.SetField("SolCredito_RefFam1_Nombre", modelo.referencia1Nombre);
            pdfFormFields.SetField("SolCredito_RefFam1_TelLada", modelo.referencia1Lada);
            pdfFormFields.SetField("SolCredito_RefFam1_TelNumero", modelo.referencia1Telefono);
            pdfFormFields.SetField("SolCredito_RefFam1_NumCelular", modelo.referencia1Celular);
            pdfFormFields.SetField("SolCredito_RefFam2_APaterno", modelo.referencia2ApellidoPaterno);
            pdfFormFields.SetField("SolCredito_RefFam2_AMaterno", modelo.referencia2ApellidoMaterno);
            pdfFormFields.SetField("SolCredito_RefFam2_Nombre", modelo.referencia2Nombre);
            pdfFormFields.SetField("SolCredito_RefFam2_TelLada", modelo.referencia2Lada);
            pdfFormFields.SetField("SolCredito_RefFam2_TelNumero", modelo.referencia2Telefono);
            pdfFormFields.SetField("SolCredito_RefFam2_NumCelular", modelo.referencia2Celular);
            pdfFormFields.SetField("SolCredito_Benef_APaterno", modelo.beneficiarioApellidoPaterno);
            pdfFormFields.SetField("SolCredito_Benef_AMaterno", modelo.beneficiarioApellidoMaterno);
            pdfFormFields.SetField("SolCredito_Benef_Nombre", modelo.beneficiarioNombre);
            pdfFormFields.SetField("SolCredito_DMCred_DescuentoPensionA", modelo.pensionAlimenticia);

            pdfFormFields.SetField("SolCredito_DMCred_PlazoM12", string.Empty);
            pdfFormFields.SetField("SolCredito_DMCred_PlazoM18", string.Empty);
            pdfFormFields.SetField("SolCredito_DMCred_PlazoM24", string.Empty);
            pdfFormFields.SetField("SolCredito_DMCred_PlazoM30", string.Empty);
            if (modelo.creditoPlazo == "12")
                pdfFormFields.SetField("SolCredito_DMCred_PlazoM12", "1");
            else if (modelo.creditoPlazo == "18")
                pdfFormFields.SetField("SolCredito_DMCred_PlazoM18", "1");
            else if (modelo.creditoPlazo == "24")
                pdfFormFields.SetField("SolCredito_DMCred_PlazoM24", "1");
            else if (modelo.creditoPlazo == "30")
                pdfFormFields.SetField("SolCredito_DMCred_PlazoM30", "1");

            pdfFormFields.SetField("SolCredito_DMCred_MManoObra", modelo.montoManoDeObra);
            pdfFormFields.SetField("SolCredito_DMCred_MCredSolic", modelo.montoCreditoSolicitado);
            pdfFormFields.SetField("SolCredito_NoCredXInfonavit", string.Empty);
            pdfFormFields.SetField("SolCredito_NoUnicoAsocATarjeta", string.Empty);//EncriptaTarjeta.DesencriptarTarjeta(modelo.NumeroTarjeta));
            pdfFormFields.SetField("SolCredito_CLABEAsocATrabajador", modelo.clabe);
            pdfFormFields.SetField("SolCredito_Ciudad", string.Empty);
            pdfFormFields.SetField("SolCredito_FechaDia", string.Empty);
            pdfFormFields.SetField("SolCredito_FechaMes", string.Empty);
            pdfFormFields.SetField("SolCredito_FechaAnio", string.Empty);

            pdfFormFields.SetFieldProperty("SolCredito_NoSeguroSocial", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_CURP", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RFC", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_APaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_AMaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_CalleYNumDHab", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_ColOFraccionamientoDHab", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_EntidadDHab", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_MunicipioODelegDHab", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_CPDHab", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_TipoIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NoIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_FechaValidezIdentificacion", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Telefono_Lada", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Telefono_Numero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Celular_Numero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Genero_M", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Genero_F", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_EMail", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Soltero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Casado", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RPMatrim_SepDeBienes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RPMatrim_SocConyugal ", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RPMatrim_SocLegal", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NombreEmpresa", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NoRegPatronal", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_TelTrabajo_Lada", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_TelTrabajo_Numero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_TelTrabajo_Extension", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_HorarioTrabajo_InicioHr", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_HorarioTrabajo_InicioMin", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_HorarioTrabajo_FinHr", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_HorarioTrabajo_FinMin", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_APaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_AMaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_TelLada", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_TelNumero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam1_NumCelular", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_APaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_AMaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_TelLada", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_TelNumero", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_RefFam2_NumCelular", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Benef_APaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Benef_AMaterno", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Benef_Nombre", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_DescuentoPensionA", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_PlazoM12", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_PlazoM18", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_PlazoM24", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_PlazoM30", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_MManoObra", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_DMCred_MCredSolic", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NoCredXInfonavit", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NoUnicoAsocATarjeta", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_CLABEAsocATrabajador", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Ciudad", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_FechaDia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_FechaMes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_FechaAnio", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Vivienda_Propia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_Vivienda_DeFamiliares", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_NoDepEconomicos", "setfflags", PdfFormField.FF_READ_ONLY, null);

            pdfFormFields.SetFieldProperty("SolCredito_FechaIdentificacion_Dia", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_FechaIdentificacion_Mes", "setfflags", PdfFormField.FF_READ_ONLY, null);
            pdfFormFields.SetFieldProperty("SolCredito_FechaIdentificacion_Anio", "setfflags", PdfFormField.FF_READ_ONLY, null);

            #endregion
        }

        public string Upload(SolicitudInscripcionCreditoSimpleModel modelo, string url, string fullpath)
        {
            if (modelo == null)
                throw new Exception(" Modelo nulo ");

            try
            {
                var pdfTemplate = ConfigurationManager.AppSettings["pdfTemplates"];   
                const string nombrePdf = "SolicitudInscripcionCredito";
                const string ext = ".pdf";

                var pdfReader = new PdfReader(pdfTemplate + nombrePdf + ext);
                var pdfStamper = new PdfStamper(pdfReader, new FileStream(fullpath, FileMode.Create));

                GeneraSolicitudInscripcionCredito(modelo, ref pdfStamper);

                pdfStamper.FormFlattening = false;
                pdfStamper.Close();

                return url;
            }
            catch (Exception ex)
            {
                //Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "GeneraPdf",
                //    "Upload Orden: " + orden.ToString(CultureInfo.InvariantCulture) + " - Error:" + ex.Message);
                //Email.EnviarEmail("sistemasdesarrollo@publipayments.com", "Documento SolicitudInscripcionCredito", "Error al generar el archivo");
                return "-1";
            }
        }
        #endregion
    }
}
