using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proSeguridad;
using proEntidad;
using proDatos;

namespace proNegocio
{
    public class clsNegInsertar
    {
        public static clsEntResponseInsertaRegistro insertarRegistroPersona(string strRegistroPersonas, string participante, string strCerRegistro, string strEliminar)
        {
            #region Inicilización de Objetos
            clsEntSesion objSesion = (clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID];
            clsEntResponseInsertaRegistro response = new clsEntResponseInsertaRegistro();
            clsEntResponseImagen responseImagen = new clsEntResponseImagen();

            registro objRegistro = new registro();

            participanteExterno objRegistroE = new participanteExterno();
            participanteInterno objRegistroI = new participanteInterno();

            List<clsEntContrasena> lstContrasena = new List<clsEntContrasena>();
            List<clsEntRegistroCert> lstRegCert = new List<clsEntRegistroCert>();

            //List<clsEntRegistroCert> lstEliminar = new List<clsEntRegistroCert>();
            List<clsEntEliminar> lstEliminar = new List<clsEntEliminar>();

            //InsertarPersona
            string[] datos = strRegistroPersonas.Split('┐');
            string[] renglones = strCerRegistro.Split('|');
            string[] datosEliminar = strEliminar.Split('|');

            #region externo
            if (datos[27] == "E")
            {

                objRegistroE.pePaterno = datos[1];
                objRegistroE.peMaterno = datos[2];
                objRegistroE.peNombre = datos[3];
                objRegistroE.peNumEmpleado = datos[4];
                objRegistroE.peCURP = datos[5];
                objRegistroE.peRFC = datos[6];
                objRegistroE.peCUIP = datos[7];
                objRegistroE.peLOC = datos[8];
                objRegistroE.peGrado = datos[9];
                objRegistroE.peCargo = datos[10];
                objRegistroE.peFechaNacimiento = Convert.ToDateTime(datos[11]);
                objRegistroE.idEstado = Convert.ToByte(datos[23]);//23
                objRegistroE.idMunicipio = Convert.ToByte(datos[24]);//24//////AQUI
                objRegistro.idUsuario = Convert.ToInt16(objSesion.Usuario.IdUsuario);
                objRegistro.regEscolaridad = datos[12];
                objRegistro.regCelular = datos[15];
                objRegistro.regTelefono = datos[13];
                objRegistro.regTelTrabajo = datos[17];
                objRegistro.regCalle = datos[18];
                objRegistro.regNumExterior = datos[19];
                objRegistro.regNumInterior = datos[20];
                objRegistro.regColonia = datos[21];
                objRegistro.regCP = datos[22];
                objRegistro.regEmailPersonal = datos[14];
                objRegistro.regEmailLaboral = datos[16];
                objRegistro.regFechaRegistro = Convert.ToDateTime(datos[25]);
                objRegistroE.idRegistro = Convert.ToInt32(datos[26]);
                objRegistro.idRegistro = Convert.ToInt32(datos[26]);
                participante = datos[27];
                objRegistro.regFoto = datos[28];
                objRegistro.regVigente = true;


            }


            else //if(datos[0] != " ")
            {
                objRegistroI.idEmpleado = new Guid(datos[0]);//objRegistroI.idEmpleado = new Guid(datos[0]);//   idEmpleado = new Guid(columns[0]),
                objRegistroI.idRegistro = Convert.ToInt32(datos[26]);
                objRegistro.idRegistro = Convert.ToInt32(datos[26]);
                objRegistro.regEscolaridad = datos[12];
                objRegistro.regCelular = datos[15];
                objRegistro.regTelefono = datos[13];
                objRegistro.regTelTrabajo = datos[17];
                objRegistro.regCalle = datos[18];
                objRegistro.regNumExterior = datos[19];
                objRegistro.regNumInterior = datos[20];
                objRegistro.regColonia = datos[21];
                objRegistro.regCP = datos[22];
                objRegistro.regEmailPersonal = datos[14];
                objRegistro.regEmailLaboral = datos[16];
                objRegistro.regFechaRegistro = Convert.ToDateTime(datos[25]);
                participante = datos[27];
                objRegistro.regFoto = datos[28];
                objRegistro.idUsuario = Convert.ToInt16(objSesion.Usuario.IdUsuario);
                objRegistro.regVigente = true;
            }
            #endregion externoInt

            #region certificacion
            foreach (var row in renglones)
            {
                string[] columnas = row.Split('┐');
                if (columnas[0] != "")
                {
                    clsEntRegistroCert objRegCert = new clsEntRegistroCert();

                    objRegCert.idCertificacion = Convert.ToInt32(columnas[0]);
                    objRegCert.crHora = TimeSpan.Parse(columnas[8]);
                    objRegCert.crVigente = true;
                    objRegCert.idEvaluador = Convert.ToInt16(columnas[3]);
                    objRegCert.idInstitucionAplicaExamen = Convert.ToInt16(columnas[1]);
                    objRegCert.idLugarAplica = Convert.ToByte(columnas[2]);
                    objRegCert.idDependenciaExterna = Convert.ToInt16(columnas[5]);
                    objRegCert.idInstitucionExterna = Convert.ToInt16(columnas[6]);
                    objRegCert.idNivelSeguridad = Convert.ToInt16(columnas[4]);
                    objRegCert.idCertificacionRegistro = Convert.ToInt32(columnas[9]);
                    objRegCert.idRegistro = Convert.ToInt32(columnas[10]);
                    objRegCert.crFechaExamen = Convert.ToDateTime(columnas[7]);//Convert.ToDateTime(columnas[7] == "" ? "01/01/1901" : columnas[7]); //
                    objRegistro.idUsuario = Convert.ToInt16(objSesion.Usuario.IdUsuario);//objRegCert.idUsuario = Convert.ToInt16(objSesion.Usuario.IdUsuario);

                    objRegCert.idZona = Convert.ToInt16(columnas[12]);
                    objRegCert.idServicio = Convert.ToInt32(columnas[13]);
                    objRegCert.idInstalacion = Convert.ToInt32(columnas[14]);

                    objRegCert.inserto = true;
                    lstRegCert.Add(objRegCert);
                }
            }

            #endregion

            string elimino = "0";

            //if (datosEliminar[0] != "")
            //{

            foreach (var eliminar in datosEliminar)
            {
                string[] rowEliminar = eliminar.Split('┐');

                if (rowEliminar[0] != "")
                {

                    clsEntEliminar objDelete = new clsEntEliminar();
                    objDelete.idRegistro = Convert.ToInt32(rowEliminar[0]);
                    objDelete.idCertificacionRegistro = Convert.ToInt32(rowEliminar[1]);
                    objDelete.idCertificacion = Convert.ToInt32(rowEliminar[2]);
                    objDelete.idUsuario = Convert.ToInt16(objSesion.Usuario.IdUsuario);

                    elimino = rowEliminar[4];
                    lstEliminar.Add(objDelete);
                }


            }
            // }

            #endregion

            try
            {
                response = clsDatInsertar.insertarRegistroPersona(objRegistro, objRegistroE, objRegistroI, participante, lstRegCert, lstEliminar);

                //if (response.alerta == "")
                //{
                //    //responseImagen = clsNegImagen.almacenaImagenPersona(strImagen, datos[4] + "_" + response.idRegistro);
                //    response.alerta = responseImagen.response;
                //}

            }
            catch (Exception ex)
            {
                response.alerta = ex.Message;
            }

            #region Response
            return response;

            #endregion
        }


        public static clsEntContrasena insertPass(string strContrasena)
        {

            #region Inicilización de Objetos
            clsEntSesion objSesion = (clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID];
            clsEntContrasena response = new clsEntContrasena();

            //clsEntResponseImagen responseImagen = new clsEntResponseImagen();

            registro objRegistro = new registro();//obtiene objRegistro.idRegistro 
            participanteExterno objRegistroE = new participanteExterno();//obtiene  objRegistroE.peCURP

            List<clsEntContrasena> lstContrasena = new List<clsEntContrasena>();

            //InsertarContrasena
            string[] contrasena = strContrasena.Split('┐');


            #region contrasena

            if (contrasena[0] != "")
            {
                objRegistro.idRegistro = Convert.ToInt32(contrasena[0]);//Convert.ToInt32(contrasena[0]); //
                objRegistroE.peCURP = contrasena[1]; //contrasena[1];//

            }
            #endregion cont


            #endregion

            try
            {
                //responseCont = clsDatInsertar.insertPass(objRegistro, objRegistroE, objRegistroI);
                response = clsDatInsertar.insertPass(objRegistro, objRegistroE);


            }
            catch (Exception ex)
            {
                response.alerta = ex.Message;
            }

            #region Response
            return response;
            #endregion

        }


        public static string insertarCertificacion(string strCertificacion,string strTemas, string strFunciones, string strPreguntas , string strRespuestas)
        {
            #region Inicilización de Objetos
            clsEntSesion objSesion = (clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID];
            string[] tableCertificacion = strCertificacion.Split('|');
            string[] tableTemas = strTemas.Split('|');
            string[] tableFunciones = strFunciones.Split('|');
            string[] tablePreguntas = strPreguntas.Split('|');
            string[] tableRespuestas = strRespuestas.Split('|');

            certificacion objCertificacion = new certificacion();

            //List<tema> lstTemas = new List<tema>();
            //List<funcion> lstFunciones = new List<funcion>();
            List<clsEntPregunta> lstPreguntas = new List<clsEntPregunta>();
            List<clsEntRespuesta> lstRespuestas = new List<clsEntRespuesta>();
            List<clsEntCertificacionTemas> lstCertificacionTema = new List<clsEntCertificacionTemas>();
            List<clsEntTemaFuncion> lstTemaFuncion = new List<clsEntTemaFuncion>();


            objCertificacion.idCertificacion = Convert.ToInt32(tableCertificacion[0]);
            objCertificacion.cerNombre = tableCertificacion[1];
            objCertificacion.cerDescripcion = tableCertificacion[2];
            objCertificacion.cerSiglas = tableCertificacion[3];
            objCertificacion.cerFechaAlta = Convert.ToDateTime( tableCertificacion[4]);
            objCertificacion.cerFechaBaja = Convert.ToDateTime(tableCertificacion[5] == "" ? "01/01/1901" : tableCertificacion[5]);            
            objCertificacion.cerPrimeraValidez = Convert.ToByte( tableCertificacion[6]);
            objCertificacion.cerRenovacionValidez = Convert.ToByte(tableCertificacion[7]);
            objCertificacion.cerPreguntas = Convert.ToInt16(tableCertificacion[8]);
            objCertificacion.cerPreguntasCorrectas = Convert.ToInt16(tableCertificacion[9]);
            objCertificacion.cerTiempoIntento =Convert.ToByte( tableCertificacion[10]);
            objCertificacion.idEntidadEvaluadora = Convert.ToInt16( tableCertificacion[11]);
            objCertificacion.idEntidadCertificadora = Convert.ToInt16( tableCertificacion[12]);
            objCertificacion.cerActiva = Convert.ToBoolean(tableCertificacion[13]);

            //Insertar TEMAS yCERTIFICACION TEMA

            foreach (var row in tableTemas)
            {
                string[] columns = row.Split('┐');
                if (columns[0] != "")
                {
                    clsEntCertificacionTemas objCerTem = new clsEntCertificacionTemas();

                    objCerTem.idTema = Convert.ToInt32(columns[0]);
                    objCerTem.temDescripcion = columns[1];
                    objCerTem.temCodigo = columns[2];
                    objCerTem.ctActivo = Convert.ToBoolean(columns[3]);
                    objCerTem.ctOrden = Convert.ToByte(columns[4]);
                    objCerTem.ctAleatorias = Convert.ToInt16(columns[5]);
                    objCerTem.ctCorrectas = Convert.ToInt16(columns[6]);
                    objCerTem.ctTiempo = Convert.ToInt16(columns[7]);
                    objCerTem.idCertificacionTema = Convert.ToInt32(columns[8]); ;
                    //objCerTem.ctActivo = Convert.ToBoolean(columns[8]);
                    objCerTem.idTematemporal = Convert.ToInt32(columns[9]);                 
                    lstCertificacionTema.Add(objCerTem);
                }
            }

            
            //Insertar FUNCIONES

            foreach (var row in tableFunciones)
            {
                string[] columns = row.Split('┐');
                if (columns[0] != "")
                {
                    clsEntTemaFuncion objFun = new clsEntTemaFuncion();

                    objFun.idFuncion = Convert.ToInt32(columns[0]);
                    objFun.funNombre = columns[1];
                    objFun.funAleatorias =Convert.ToInt16(columns[2]);
                    objFun.funCorrectas = Convert.ToInt16(columns[3]);
                    objFun.funTiempo = Convert.ToInt16(columns[4]);
                    objFun.funOrden = Convert.ToByte(columns[5]);
                    objFun.tfActivo = Convert.ToBoolean(columns[7]);
                    objFun.funCodigo = columns[6];
                    objFun.idFunciontemporal = Convert.ToInt32(columns[8]);
                    objFun.idTema = Convert.ToInt32(columns[9]);
                    objFun.idTemaFuncion = Convert.ToInt32(columns[10]);
                    objFun.idTematemporal = Convert.ToInt32(columns[11]);
                    lstTemaFuncion.Add(objFun);
                }
            }


            //Insertar PREGUNTAS

            foreach (var row in tablePreguntas)
            {
                string[] columns = row.Split('┐');
                if (columns[0] != "")
                {
                    clsEntPregunta objPreg = new clsEntPregunta();
                    objPreg.idPregunta = Convert.ToInt32(columns[0]);              
                    objPreg.preDescripcion = columns[1];
                    objPreg.preObligatoria = Convert.ToBoolean(columns[2]);
                    objPreg.preCodigo = columns[3];
                    objPreg.preActiva = Convert.ToBoolean( columns[4]);
                    objPreg.idFuncionTemporal = Convert.ToInt32(columns[5]);
                    objPreg.idFuncion = Convert.ToInt32(columns[6]);
                    objPreg.idPreguntaTemporal = Convert.ToInt32(columns[7]);
                    objPreg.identificadorImagen = columns[8];
                    //objPreg.imagen = columns[9];
                    objPreg.preNombreArchivo = columns[9];
                    objPreg.preTipoArchivo = columns[10] == "" ? null : columns[10];
                    
                    lstPreguntas.Add(objPreg);
                }
            }



            //Insertar RESPUESTAS

            foreach (var row in tableRespuestas)
            {
                string[] columns = row.Split('┐');
                if (columns[0] != "")
                {
                    clsEntRespuesta objRes = new clsEntRespuesta();

                       objRes.idRespuesta = Convert.ToInt32(columns[0]);                                    
                       objRes.resDescripcion =columns[1];
                       objRes.resExplicacion = columns[2];
                       objRes.resCorrecta =Convert.ToBoolean(columns[3]);
                       objRes.resActiva = Convert.ToBoolean (columns[4]);
                       objRes.idPreguntaTemporal = Convert.ToInt32(columns[5]);
                       objRes.idPregunta = Convert.ToInt32(columns[6]);
                       objRes.idRespuestaTemporal = Convert.ToInt32(columns[7]);
                       objRes.resTipoArchivo = columns[8] == "" ? null : columns[8];
                       objRes.resNombreArchivo = columns[9];
                       //objRes.imagen = columns[10];
                       objRes.identificadorImagen = columns[10];

                    lstRespuestas.Add(objRes);
                }
            }


            string alerta = string.Empty;
            #endregion



            try
            {
                alerta = clsDatInsertar.insertarCertificacion(objCertificacion, lstCertificacionTema, lstTemaFuncion,  lstPreguntas, lstRespuestas);
                
            }
            catch (Exception ex)
            {
                alerta = ex.Message;
            }
       

            #region Response
            return alerta;
            #endregion
        }



    }
}
