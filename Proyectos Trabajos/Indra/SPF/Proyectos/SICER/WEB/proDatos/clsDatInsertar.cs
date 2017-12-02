using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using proEntidad;
using proSeguridad;

using System.Web;
using System.IO;

namespace proDatos
{
    public class clsDatInsertar
    {

        public static clsEntResponseInsertaRegistro insertarRegistroPersona(registro objRegistro, participanteExterno objRegistroE, participanteInterno objRegistroI, string participante, List<clsEntRegistroCert> lstRegCert, List<clsEntEliminar> lstEliminar)
        {
            sicerEntities context_Entiti;
            clsEntResponseInsertaRegistro response = new clsEntResponseInsertaRegistro();
            response.alerta = string.Empty;
            //string alerta = string.Empty;

            clsEntSesion obj = (clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID];
            if (System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID] == null)
                return null;

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);

            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        System.Data.Objects.ObjectParameter paramInserto = new System.Data.Objects.ObjectParameter("inserto", typeof(bool));
                        System.Data.Objects.ObjectParameter idRegistro = new System.Data.Objects.ObjectParameter("idRegistro", typeof(int));
                        System.Data.Objects.ObjectParameter participanteExterno = new System.Data.Objects.ObjectParameter("idRegistro", typeof(int));
                        System.Data.Objects.ObjectParameter participanteInterno = new System.Data.Objects.ObjectParameter("idRegistro", typeof(int));
                        System.Data.Objects.ObjectParameter idCertificacionRegistro = new System.Data.Objects.ObjectParameter("idCertificacionRegistro", typeof(int));
                        System.Data.Objects.ObjectParameter idCertificacion = new System.Data.Objects.ObjectParameter("idCertificacion", typeof(int));

                        idRegistro.Value = objRegistro.idRegistro;


                        participanteExterno.Value = objRegistroE.idRegistro;
                        participanteInterno.Value = objRegistroI.idRegistro;


                        #region Insertar EXPEDIENTE

                        if (participante == "E")
                        {

                            context_Entiti.spuInsertarActualizarRegistro(

                                 idRegistro,
                                objRegistroI.idEmpleado,
                                objRegistroE.pePaterno,
                                objRegistroE.peMaterno,
                                objRegistroE.peNombre,
                                objRegistroE.peNumEmpleado,
                                objRegistroE.peCURP,
                                objRegistroE.peRFC,
                                objRegistroE.peCUIP,
                                objRegistroE.peLOC,
                                objRegistroE.peCargo,
                                objRegistroE.peGrado,
                                objRegistroE.peEdad,
                                objRegistroE.peFechaNacimiento,
                                objRegistroE.idEstado,
                                objRegistroE.idMunicipio,
                                objRegistro.regEscolaridad,
                                objRegistro.regCelular,
                                objRegistro.regTelefono,
                                objRegistro.regTelTrabajo,
                                objRegistro.regCalle,
                                objRegistro.regNumExterior,
                                objRegistro.regNumInterior,
                                objRegistro.regColonia,
                                objRegistro.regCP,
                                objRegistro.regEmailPersonal,
                                objRegistro.regEmailLaboral,
                                null,
                                objRegistro.regFechaEnvio,
                                null,
                                objRegistro.regFechaRegistro,
                                objRegistro.idUsuario,
                                true,
                                participante,
                                paramInserto,
                                objRegistro.regFoto
                               );
                        }
                        else
                        {

                            context_Entiti.spuInsertarActualizarRegistro(

                                     idRegistro,
                                    objRegistroI.idEmpleado,
                                    objRegistroE.pePaterno,
                                    objRegistroE.peMaterno,
                                    objRegistroE.peNombre,
                                    objRegistroE.peNumEmpleado,
                                    objRegistroE.peCURP,
                                    objRegistroE.peRFC,
                                    objRegistroE.peCUIP,
                                    objRegistroE.peLOC,
                                    objRegistroE.peCargo,
                                    objRegistroE.peGrado,
                                    objRegistroE.peEdad,
                                    objRegistroE.peFechaNacimiento,
                                    objRegistroE.idEstado,
                                    objRegistroE.idMunicipio,

                                    objRegistro.regEscolaridad,
                                    objRegistro.regCelular,
                                    objRegistro.regTelefono,
                                    objRegistro.regTelTrabajo,
                                    objRegistro.regCalle,
                                    objRegistro.regNumExterior,
                                    objRegistro.regNumInterior,
                                    objRegistro.regColonia,
                                    objRegistro.regCP,
                                    objRegistro.regEmailPersonal,
                                    objRegistro.regEmailLaboral,
                                    null,//objRegistro.regFechaRecuperacion,////de contr
                                    null,
                                    null,
                                    objRegistro.regFechaRegistro,
                                    objRegistro.idUsuario,
                                    true,
                                    participante,
                                    paramInserto,
                                    objRegistro.regFoto
                                   );

                        }

                        response.idRegistro = Convert.ToInt32(idRegistro.Value);
                        #endregion


                        #region Certificacion
                        //bool inserto = true;
                        if (Convert.ToInt32(paramInserto.Value) == 0)
                        {
                            response.alerta = "Existió un error al insertar";
                            //alert = "Existió un error al insertar";

                        }
                        else
                        {
                            //if (inserto==true)
                            //{

                            if (participante == "E")
                            {
                                foreach (clsEntRegistroCert objRegCert in lstRegCert)
                                {

                                    if (response.alerta != "")
                                    {
                                        break;
                                    }

                                    idCertificacion.Value = objRegCert.idCertificacion;
                                    idCertificacionRegistro.Value = objRegCert.idCertificacionRegistro;

                                    context_Entiti.spuInsertarActualizarCertificacionRegistroUIUE(

                                                participante,
                                                (int)idCertificacionRegistro.Value,
                                                (int)idCertificacion.Value,
                                                response.idRegistro,
                                                objRegCert.crFechaExamen,
                                                objRegCert.crHora,
                                                objRegCert.idEvaluador,
                                                DateTime.Today, //objRegCert.crFechaModificacion,
                                                true,
                                                objRegistro.idUsuario,//objRegCert.idUsuario,
                                                objRegCert.idInstitucionAplicaExamen,
                                                objRegCert.idLugarAplica,
                                                objRegCert.idNivelSeguridad,
                                                objRegCert.idDependenciaExterna,
                                                objRegCert.idInstitucionExterna,
                                                objRegCert.idZona,
                                                objRegCert.idServicio,
                                                objRegCert.idInstalacion,
                                                objRegCert.inserto
                                                );

                                }
                            }
                            #region interno
                            else
                            {

                                foreach (clsEntRegistroCert objRegCert in lstRegCert)
                                {

                                    
                                    if (response.alerta != "")
                                    {
                                        break;
                                    }
                                    idCertificacion.Value = objRegCert.idCertificacion;
                                    idCertificacionRegistro.Value = objRegCert.idCertificacionRegistro;

                                    context_Entiti.spuInsertarActualizarCertificacionRegistroUIUE(

                                                participante,
                                                (int)idCertificacionRegistro.Value,
                                                (int)idCertificacion.Value,
                                                response.idRegistro,
                                                objRegCert.crFechaExamen,
                                                objRegCert.crHora,
                                                objRegCert.idEvaluador,
                                                DateTime.Today, //objRegCert.crFechaModificacion,
                                                true,
                                                objRegistro.idUsuario,//objRegCert.idUsuario,
                                                objRegCert.idInstitucionAplicaExamen,
                                                objRegCert.idLugarAplica,
                                                objRegCert.idNivelSeguridad,
                                                objRegCert.idDependenciaExterna,
                                                objRegCert.idInstitucionExterna,
                                                objRegCert.idZona,
                                                objRegCert.idServicio,
                                                objRegCert.idInstalacion,
                                                objRegCert.inserto
                                                );
                                }

                            }//fin else 
                            #endregion interno
                        #endregion Certificacion_fin

                        }
                        //}


                        // }

                        #region Eliminar
                        if (Convert.ToBoolean(paramInserto.Value) == false)
                        {
                            response.alerta = "Existio un error al eliminar";
                        }
                        else
                        {
                            #region Eliminar
                            foreach (clsEntEliminar objDelete in lstEliminar)
                                context_Entiti.spuEliminarCertificacion(objDelete.idRegistro, objDelete.idCertificacionRegistro, objDelete.idCertificacion, objDelete.idUsuario, paramInserto);
                            #endregion

                            //alerta = string.Empty;
                        }
                        #endregion Eliminar

                        if (response.alerta == "")
                        {
                            ts.Complete();
                        }
                    }

                    catch (Exception ex)
                    {
                        response.alerta = ex.Message;
                    }
                }
            }

            return response;
        }


        public static clsEntContrasena insertPass(registro objRegistro, participanteExterno objRegistroE)
        {


            sicerEntities context_Entiti;
           // clsEntResponseInsertaRegistro response = new clsEntResponseInsertaRegistro();
            clsEntContrasena responseCon = new clsEntContrasena();
            responseCon.alerta = string.Empty;
            string alert = string.Empty;

            clsEntSesion obj = (clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID];
            if (System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID] == null)
                return null;

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);

            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {

                        System.Data.Objects.ObjectParameter paramInserto = new System.Data.Objects.ObjectParameter("inserta", typeof(bool));
                        
                        #region contrasena
                       
                        context_Entiti.spuInsertarActualizarContrasena(
                            objRegistro.idRegistro,
                            objRegistroE.peCURP,
                            paramInserto
                            );

                        #endregion contrasena_fin
                        ts.Complete();
                    }

                    catch (Exception ex)
                    {
                        responseCon.alerta = ex.Message;
                    }

                }
            }
            return responseCon;
        }


        public static string insertarCertificacion(certificacion objCertificacion, List<clsEntCertificacionTemas> lstCertificacionTema, List<clsEntTemaFuncion> lstTemaFuncion, List<clsEntPregunta> lstPregunta, List<clsEntRespuesta> lstRespuesta)
        {
            sicerEntities context_Entiti;

            string alert = string.Empty;
            clsEntSesion obj = (clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID];
            if (System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID] == null)
                return null;
            if (HttpContext.Current.Session["rutaServidor"] == null)
                HttpContext.Current.Session["rutaServidor"] = clsDatCatalogos.catalogoRutaServidor()[0].rsDescripcion;

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity((clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID]);
            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {


                        System.Data.Objects.ObjectParameter paramInserto = new System.Data.Objects.ObjectParameter("inserto", typeof(bool));
                        System.Data.Objects.ObjectParameter idCertificacion = new System.Data.Objects.ObjectParameter("idCertificacion", typeof(int));
                        System.Data.Objects.ObjectParameter idTema = new System.Data.Objects.ObjectParameter("idTema", typeof(int));
                        System.Data.Objects.ObjectParameter idFuncion = new System.Data.Objects.ObjectParameter("idFuncion", typeof(int));
                        System.Data.Objects.ObjectParameter idPregunta = new System.Data.Objects.ObjectParameter("idPregunta", typeof(int));
                        System.Data.Objects.ObjectParameter idRespuesta = new System.Data.Objects.ObjectParameter("idRespuesta", typeof(int));
                        System.Data.Objects.ObjectParameter idCertificacionTema = new System.Data.Objects.ObjectParameter("idCertificacionTema", typeof(int));
                        System.Data.Objects.ObjectParameter idTemaFuncion = new System.Data.Objects.ObjectParameter("idTemaFuncion", typeof(int));
                        System.Data.Objects.ObjectParameter preNombreArchivo = new System.Data.Objects.ObjectParameter("preNombreArchivo", typeof(string));
                        System.Data.Objects.ObjectParameter resNombreArchivo = new System.Data.Objects.ObjectParameter("resNombreArchivo", typeof(string));

                        #region Insertar Certificacion

                        idCertificacion.Value = objCertificacion.idCertificacion;


                        context_Entiti.spuInsertarActualizarCertificacion(
                         idCertificacion,
                         objCertificacion.cerNombre,
                         objCertificacion.cerDescripcion,
                         objCertificacion.cerSiglas,
                         objCertificacion.cerFechaAlta,
                         null,//FechaBAJA
                         objCertificacion.cerPrimeraValidez,
                         objCertificacion.cerRenovacionValidez,
                         objCertificacion.cerPreguntas,
                         objCertificacion.cerPreguntasCorrectas,
                         objCertificacion.cerTiempoIntento,
                         objCertificacion.idEntidadEvaluadora,
                         objCertificacion.idEntidadCertificadora,
                         obj.Usuario.IdUsuario,
                         objCertificacion.cerActiva,
                        paramInserto);

                        if (Convert.ToBoolean(paramInserto.Value) == false)
                        {
                            alert = "Existió un error al insertar";
                        }
                        else
                        {
                        idCertificacion.Value = idCertificacion.Value;
                                                        
                        foreach (clsEntCertificacionTemas objCerTem in lstCertificacionTema)
                                                     
                            
                            {

                                if (alert != "")
                                {
                                    break;
                                }

                                idCertificacionTema.Value = objCerTem.idCertificacionTema;
                                idTema.Value = objCerTem.idTema;
                            
                                context_Entiti.spuInsertarTema(
                                  idTema,
                                    objCerTem.temDescripcion,
                                    objCerTem.temCodigo,
                                   true,
                                    obj.Usuario.IdUsuario,
                                    (int)idCertificacionTema.Value,
                                    (int)idCertificacion.Value,
                                    objCerTem.ctOrden,
                                    objCerTem.ctAleatorias,
                                    objCerTem.ctCorrectas,
                                    objCerTem.ctTiempo,
                                    objCerTem.ctActivo,
                                    paramInserto

                                    );

                                if (Convert.ToBoolean(paramInserto.Value) == false)
                                {
                                    alert = "Existió un error al insertar";
                                    break;
                                }


                                idTema.Value = idTema.Value;



                                    foreach (clsEntTemaFuncion objTemFun in lstTemaFuncion)
                                    {
                                        if (alert != "")
                                        {
                                            break;
                                        }

                                        if (objCerTem.idTematemporal == objTemFun.idTematemporal && objCerTem.idTema == objTemFun.idTema ) 
                                        {

                                            idFuncion.Value = objTemFun.idFuncion;
                                            idTemaFuncion.Value = objTemFun.idTemaFuncion;

                                            context_Entiti.spuInsertarActualizarFuncion(
                                                idFuncion,
                                                objTemFun.funNombre,
                                                objTemFun.funAleatorias,
                                                objTemFun.funCorrectas,
                                                objTemFun.funTiempo,
                                                objTemFun.funOrden,
                                                true,
                                                obj.Usuario.IdUsuario,
                                                objTemFun.funCodigo,
                                                objTemFun.tfActivo,
                                                (int)idTema.Value,
                                                (int)idTemaFuncion.Value,
                                                paramInserto
                                                );
                                            

                                            if (Convert.ToBoolean(paramInserto.Value) == false)
                                            {
                                                alert = "Existió un error al insertar";
                                                break;
                                            }

                                            idFuncion.Value = idFuncion.Value;

                                            foreach (clsEntPregunta objPreg in lstPregunta )
                                            {
                                                if (alert != "")
                                                {
                                                    break;
                                                }
                                            if(objTemFun.idFunciontemporal == objPreg.idFuncionTemporal && objTemFun.idFuncion == objPreg.idFuncion )
                                            {

                                                idPregunta.Value = objPreg.idPregunta;
                                                
                                                context_Entiti.spuInsertarActualizarPreguntas(
                                                    idPregunta,
                                                    (int)idFuncion.Value,
                                                    objPreg.preDescripcion,
                                                    objPreg.preObligatoria,
                                                    objPreg.preTipoArchivo,//==""?null:objPreg.preTipoArchivo,
                                                    preNombreArchivo,
                                                    true,
                                                    obj.Usuario.IdUsuario,
                                                    objPreg.preActiva,
                                                    objPreg.preCodigo,
                                                    paramInserto

                                                    );
                                                
                                                idPregunta.Value = idPregunta.Value;
                                                objPreg.preNombreArchivo = Convert.ToString(preNombreArchivo.Value);

                                                if (Convert.ToBoolean(paramInserto.Value) == false)
                                                {
                                                    alert = "Existió un error al insertar";
                                                    break;
                                                }

                                                if (objPreg.identificadorImagen != "")
                                                {
                                                    alert = almacenaImagenPDFCertificacion(objPreg.identificadorImagen, objPreg.preNombreArchivo).response;
                                                    if (alert != "")
                                                        break;
                                                }

                                                foreach(clsEntRespuesta objRes in lstRespuesta) 
                                                {

                                                    if (alert != "")
                                                    {
                                                        break;
                                                    }
                                                    if (objPreg.idPreguntaTemporal == objRes.idPreguntaTemporal && objPreg.idPregunta == objRes.idPregunta)
                                                    {

                                                        idRespuesta.Value = objRes.idRespuesta;
                                                        
                                                        context_Entiti.spuInsertarActualizarRespuestas(
                                                            (int)idPregunta.Value,
                                                            (int)idRespuesta.Value,
                                                            objRes.resDescripcion,
                                                            objRes.resTipoArchivo,// == "" ? null : objRes.resTipoArchivo,
                                                            resNombreArchivo,
                                                            objRes.resCorrecta,
                                                            true,
                                                            obj.Usuario.IdUsuario,
                                                            objRes.resActiva,
                                                            objRes.resExplicacion,
                                                            paramInserto
                                                            );

                                                        objRes.resNombreArchivo = Convert.ToString(resNombreArchivo.Value);

                                                        if (Convert.ToBoolean(paramInserto.Value) == false)
                                                        {
                                                            alert = "Existió un error al insertar";
                                                            break;
                                                        }
                                                        if (objRes.identificadorImagen != "")
                                                        {
                                                            alert = almacenaImagenPDFCertificacion(objRes.identificadorImagen, objRes.resNombreArchivo).response;
                                                            if (alert != "")
                                                                break;
                                                        }

                                                    }

                                                }
                                                }
                                            }

                                            }
                                        }



                                    }
                            }
                        #endregion

                        if (alert == "")
                        {
                            ts.Complete();
                        }
                    }



                    catch (Exception ex)
                    {
                        alert = ex.Message;
                    }
                }
            }

            return alert;
        }

        public static string insertarEvaluacion(List<clsEntRespuestaExamen> lstRespCalificacion,int calificacion,int idCertificacionRegistro,int idRegistro,int idCertificacion)
        { 
            sicerEntities context_Entiti;

            string alert = string.Empty;
           
            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity();
            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        System.Data.Objects.ObjectParameter idEvaluacion = new System.Data.Objects.ObjectParameter("idEvaluacion", typeof(short));

                            context_Entiti.spuInsertarEvaluacion(
                                idCertificacionRegistro,
                                idCertificacion,
                                idRegistro,
                                calificacion,
                                idEvaluacion
                                );
                          //  if ((int)idEvaluacion.Value != 0)
                            {
                                foreach (clsEntRespuestaExamen calif in lstRespCalificacion)
                                {
                                    if (calif.idRespuesta!=0)
                                    {
                                        context_Entiti.spuInsertaEvaluacionRespuesta(
                                            (byte)idEvaluacion.Value,
                                            idCertificacionRegistro,
                                            idCertificacion,
                                            idRegistro,
                                            calif.idPregunta,
                                            calif.idRespuesta
                                       
                                        );
                                    }
                                }
                            
                            
                            }
                            ts.Complete();

                    }catch(Exception e){
                        alert = e.Message;
                    }

                }
            }

            return alert;
        }


        public static clsEntResponseImagen almacenaImagenPDFCertificacion(string identificador, string nombre)
        {
            clsEntResponseImagen objResponse = new clsEntResponseImagen();
            List<clsEntResponseImagen>listaPDFs= new List<clsEntResponseImagen>();
            string alerta = string.Empty;
            string ruta;

            try
            {
                listaPDFs = (List<clsEntResponseImagen>)System.Web.HttpContext.Current.Session["lstPDFs" + System.Web.HttpContext.Current.Session.SessionID];

              
                ruta = (string)HttpContext.Current.Session["rutaServidor"] + nombre;
                
                // Convert Base64 String to byte[]
                byte[] imageBytes = listaPDFs.Find(x => x.identificador == identificador).byteImagen;
                var fs = new BinaryWriter(new FileStream(ruta, FileMode.Create, FileAccess.Write));
                fs.Write(imageBytes);
                fs.Close();
            }
            catch (Exception ex)
            {
                alerta = ex.Message;
            }

            objResponse.response = alerta;

            return objResponse;

        }
    
    } 


    }  


