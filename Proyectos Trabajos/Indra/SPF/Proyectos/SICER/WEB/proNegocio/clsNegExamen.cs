using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proDatos;
using proEntidad;
using System.Timers;
using System.Globalization;
using proSeguridad;

namespace proNegocio
{
    public class clsNegExamen
    {

        public static List<spuConsultarTemasCertificacionExamen_Result> consultaTemasdeCertificacion(int idCertificacion)
        {
            List<spuConsultarTemasCertificacionExamen_Result> lstTemas=clsDatExamen.consultaTemasdeCertificacion(idCertificacion);

            if(lstTemas.Count!=0) //Condición para evitar borrar los temas si se abre una nueva pestaña en el navegador
                System.Web.HttpContext.Current.Session["lstTemas" + System.Web.HttpContext.Current.Session.SessionID] = lstTemas;

            return lstTemas;
        }
        public static List<spuConsultarFuncionesTema_Result> consultaFuncionesTema(int idTema)
        {
            return clsDatExamen.consultaFuncionesTema(idTema);
        }

        public static List<spuConsultarPreguntasTema_Result> consultaPreguntasTema(int idTema)
        {
            List<spuConsultarPreguntasTema_Result> lista = new List<spuConsultarPreguntasTema_Result>();
            List<spuConsultarPreguntasTema_Result> listaAleatoria = new List<spuConsultarPreguntasTema_Result>();
            
            List<clsEntPreguntaImagen> listaImagenes = new List<clsEntPreguntaImagen>();
            lista = clsDatExamen.consultaPreguntasTema(idTema);

            if(lista.Count!=0)
            {
                    foreach (spuConsultarPreguntasTema_Result pregunta in lista)
                    {
                        clsEntPreguntaImagen elementoPregImagen = new clsEntPreguntaImagen();
                        clsEntResponseImagen preImag = new clsEntResponseImagen();
                        elementoPregImagen.idFuncion = pregunta.idFuncion;
                        elementoPregImagen.idPregunta = pregunta.idPregunta;
                        elementoPregImagen.preDescripcion = "";
                        if (pregunta.preTipoArchivo == "I")
                        {
                           preImag=clsNegImagen.obtieneImagenExamen(pregunta.preNombreArchivo);

                           if (preImag.response == "")
                           {
                               elementoPregImagen.imagen = "data:image/jpeg;base64," + preImag.strImagen;
                           }
                           else
                           {
                               elementoPregImagen.preDescripcion = preImag.response; //resDescripcion funcionará como control de errores
                           }
                           listaImagenes.Add(elementoPregImagen);


                        }

                    }

                    //Se vuelve a llenar por cada tema, no es necesario guardar las imagenes de un tema anterior
                    System.Web.HttpContext.Current.Session["lstImagenesPreguntas" + System.Web.HttpContext.Current.Session.SessionID] = listaImagenes;


                    //Seleccionando preguntas aleatorias
                    IEnumerable<IGrouping<int, spuConsultarPreguntasTema_Result>>  listaAl= lista.GroupBy(u => u.idFuncion).ToList();

                   foreach (IGrouping<int, spuConsultarPreguntasTema_Result> funcionGrupo in listaAl)
                   { 
                       
                       int preguntasAleatorias= Convert.ToInt32(funcionGrupo.First().funAleatorias);

                       /*
                       //Otra forma de resolverlo
                       //Warning: Si se usa este algoritmo existe la muy remota probabilidad de que nunca se elija una ultima pregunta para completar la cantidad necesaria. Aunque por improbabilidad no debería ocurrir
                       //if (preguntasAleatorias < funcionGrupo.Count())
                       //{
                       
                          foreach (spuConsultarPreguntasTema_Result e in funcionGrupo)
                          {
                               if(Convert.ToBoolean(e.preObligatoria))
                                   listaAleatoria.Add(e);
                          }
                           while (listaAleatoria.FindAll(x => x.idFuncion == funcionGrupo.Key).Count < preguntasAleatorias)
                          {
                              Random random = new Random(DateTime.Now.Millisecond);

                              spuConsultarPreguntasTema_Result pregunta = funcionGrupo.ElementAt(random.Next(0, funcionGrupo.Count()));

                              if (!listaAleatoria.Exists(x => x.idPregunta == pregunta.idPregunta))
                              {
                                  listaAleatoria.Add(pregunta);
                              }
                          }
                        
                          }
                          else {
                              foreach(spuConsultarPreguntasTema_Result e in funcionGrupo )
                              listaAleatoria.Add(e);
                          }
                       */


                       List<spuConsultarPreguntasTema_Result> auxFuncionGrupo = funcionGrupo.ToList(); //Selecciono todas las preguntas que tiene
                       List<spuConsultarPreguntasTema_Result> listaAleatoriaAux = new List<spuConsultarPreguntasTema_Result>();
                       //Eligiendo las preguntas obligatorias 
                       foreach (spuConsultarPreguntasTema_Result e in funcionGrupo)
                       {
                           if (listaAleatoriaAux.Count == preguntasAleatorias)
                           {
                               break;
                           }

                           if (Convert.ToBoolean(e.preObligatoria))
                           {
                               listaAleatoriaAux.Add(e);
                               auxFuncionGrupo.Remove(e);
                           }
                       }
                       int faltantes = preguntasAleatorias - listaAleatoriaAux.Count;

                       //Se completan las preguntas que faltan con las sobrantes no obligatorias de manera aleatoria
                       listaAleatoriaAux = listaAleatoriaAux.Union(randomShuffle(auxFuncionGrupo.ToList()).Take(faltantes)).ToList();
                       
                       //Después de obtener la lista de las preguntas se ordenan de manera aleatoria
                       listaAleatoria = listaAleatoria.Union(randomShuffle(listaAleatoriaAux)).Select(x => x as spuConsultarPreguntasTema_Result).ToList();
                   }

                   System.Web.HttpContext.Current.Session["lstPreguntasAleatorias" + System.Web.HttpContext.Current.Session.SessionID] = listaAleatoria;
            }
          



            return listaAleatoria;
        }


        #region desordenarLista
        public static List<spuConsultarPreguntasTema_Result> randomShuffle(List<spuConsultarPreguntasTema_Result> lista)
        {
            List<spuConsultarPreguntasTema_Result> listRetorno = new List<spuConsultarPreguntasTema_Result>();
            int cantidadInicial =lista.Count;

            //Metodo: Modern Fisher–Yates shuffle
            Random random = new Random();
            while(listRetorno.Count<cantidadInicial)
            {
                spuConsultarPreguntasTema_Result obj = lista.ElementAt(random.Next(0, lista.Count));

                listRetorno.Add(obj);
                lista.Remove(obj);
            }
            return listRetorno;
        }
        public static List<clsEntRespuestaExamen> randomShuffle(List<clsEntRespuestaExamen> lista)
        {
            List<clsEntRespuestaExamen> listRetorno = new List<clsEntRespuestaExamen>();
            int cantidadInicial = lista.Count;

            //Metodo: Modern Fisher–Yates shuffle
            Random random = new Random();
            while (listRetorno.Count < cantidadInicial)
            {
                clsEntRespuestaExamen obj = lista.ElementAt(random.Next(0, lista.Count));

                listRetorno.Add(obj);
                lista.Remove(obj);
            }
            return listRetorno;
        }
        #endregion desordenarLista

        public static List<clsEntRespuestaExamen> consultaRespuestasTema(int idTema)
        {
            List<spuConsultarRespuestasTema_Result> lista = new List<spuConsultarRespuestasTema_Result>();

            lista = clsDatExamen.consultaRespuestasTema(idTema);
            List<clsEntRespuestaExamen> listaRespuestas = new List<clsEntRespuestaExamen>();
            List<clsEntRespuestaExamen> listaRespuestasAleatorias = new List<clsEntRespuestaExamen>();
             List<spuConsultarPreguntasTema_Result> listaPreguntas = new List<spuConsultarPreguntasTema_Result>();
            List<clsEntRespuestaImagen> listaImagenes = new List<clsEntRespuestaImagen>();
            
            foreach (spuConsultarRespuestasTema_Result respuesta in lista)
            {
                //Creo una lista de respuestas para enviarla a la vista (cliente), de esta forma no se envia la respuesta correcta al cliente
                clsEntRespuestaExamen elementoRespuesta = new clsEntRespuestaExamen();
                elementoRespuesta.idTema = (Int32)respuesta.idTema;
                elementoRespuesta.idFuncion = respuesta.idFuncion;
                elementoRespuesta.idPregunta = respuesta.idPregunta;
                elementoRespuesta.idRespuesta = respuesta.idRespuesta;
                elementoRespuesta.resDescripcion = respuesta.resDescripcion;
                elementoRespuesta.resNombreArchivo = respuesta.resNombreArchivo;
                elementoRespuesta.resTipoArchivo = respuesta.resTipoArchivo;
                listaRespuestas.Add(elementoRespuesta);


                if (respuesta.resTipoArchivo == "I") //Solo Imágenes, para otros archivos funcionará diferente
                {
                    clsEntRespuestaImagen elementoResImagen = new clsEntRespuestaImagen();
                    clsEntResponseImagen resImag = new clsEntResponseImagen();
                    elementoResImagen.idRespuesta = respuesta.idRespuesta;
                    elementoResImagen.idFuncion = respuesta.idFuncion;
                    elementoResImagen.resDescripcion = "";
                    resImag = clsNegImagen.obtieneImagenExamen(respuesta.resNombreArchivo);
                    if (resImag.response == "")
                    {
                        elementoResImagen.imagen = "data:image/jpeg;base64," + resImag.strImagen;
                    }
                    else {
                        elementoResImagen.resDescripcion = resImag.response; //resDescripcion funcionará como control de errores
                    }
                    listaImagenes.Add(elementoResImagen);
                }
                
            }

            //Se vuelve a llenar por cada tema, no es necesario guardar las imagenes de un tema anterior
            System.Web.HttpContext.Current.Session["lstImagenesRespuestas" + System.Web.HttpContext.Current.Session.SessionID] = listaImagenes;

            if (System.Web.HttpContext.Current.Session["lstRespuestas" + System.Web.HttpContext.Current.Session.SessionID] == null)
            {
                System.Web.HttpContext.Current.Session["lstRespuestas" + System.Web.HttpContext.Current.Session.SessionID] = lista;
            }
            else {
                List<spuConsultarRespuestasTema_Result> listaTemp = (List<spuConsultarRespuestasTema_Result>)System.Web.HttpContext.Current.Session["lstRespuestas" + System.Web.HttpContext.Current.Session.SessionID];
                //Si no se han agregado las respuestas de un tema, se agregan.
                //Aquí agregamos todas las respuestas de la certificación en la variable de sesión sin tener que consultar de nuevo a la Base de Datos
                if(!listaTemp.Exists(x => x.idTema == idTema))
                {
                    listaTemp.AddRange(lista);
                    System.Web.HttpContext.Current.Session["lstRespuestas" + System.Web.HttpContext.Current.Session.SessionID] = listaTemp;
                }

            }

            //Elimino las respuestas que no tienen una pregunta, debido al filtro de las preguntas aleatorias
            if(System.Web.HttpContext.Current.Session["lstPreguntasAleatorias" + System.Web.HttpContext.Current.Session.SessionID]!=null)
            {
                listaPreguntas=(List<spuConsultarPreguntasTema_Result>)System.Web.HttpContext.Current.Session["lstPreguntasAleatorias" + System.Web.HttpContext.Current.Session.SessionID];

                foreach(clsEntRespuestaExamen respuesta in listaRespuestas)
                {   
                    if(listaPreguntas.Exists(x => x.idPregunta == respuesta.idPregunta))
                    {
                             listaRespuestasAleatorias.Add(respuesta);
                    }
                }

                listaRespuestasAleatorias = randomShuffle(listaRespuestasAleatorias);

            }

            


            //Envío lista de respuestas sin imágenes. Debido a que no es posible enviar una cadena json demasiado grande.
            //Uso la funcion consultaImagenesRespuestas(idFuncion) para enviar poco a poco las imágenes.
            return listaRespuestasAleatorias;
        }
        

        public static List<spuConsultarCertificacionesRegistro_Result> consultaCertificacionesRegistro(int idRegistro)
        {
            return clsDatExamen.consultaCertificacionesRegistro(idRegistro);
        }

        public static List<clsEntRespuestaImagen> consultaImagenesRespuestas(int idFuncion)
        {
            List<clsEntRespuestaImagen> listaRespuestas = new List<clsEntRespuestaImagen>();
            List<clsEntRespuestaImagen> listaRespuestasFuncion = new List<clsEntRespuestaImagen>();

             if (System.Web.HttpContext.Current.Session["lstImagenesRespuestas" + System.Web.HttpContext.Current.Session.SessionID] != null)
                 listaRespuestas = (List<clsEntRespuestaImagen>)System.Web.HttpContext.Current.Session["lstImagenesRespuestas" + System.Web.HttpContext.Current.Session.SessionID];
                    
                
                 // clsEntResponseImagen resImag = new clsEntResponseImagen();

                  listaRespuestasFuncion = listaRespuestas.FindAll(
                          delegate(clsEntRespuestaImagen bk)
                          {
                              if (bk.idFuncion == idFuncion)
                              {
                                  return true;
                              } 
                              return false;
                          }
                      );

                  return listaRespuestasFuncion;
        }

        public static List<clsEntPreguntaImagen> consultaImagenesPreguntas(int idFuncion)
        {
            List<clsEntPreguntaImagen> listaPreguntas = new List<clsEntPreguntaImagen>();
            List<clsEntPreguntaImagen> listaPreguntasFuncion = new List<clsEntPreguntaImagen>();

            if (System.Web.HttpContext.Current.Session["lstImagenesPreguntas" + System.Web.HttpContext.Current.Session.SessionID] != null)
                listaPreguntas = (List<clsEntPreguntaImagen>)System.Web.HttpContext.Current.Session["lstImagenesPreguntas" + System.Web.HttpContext.Current.Session.SessionID];


            // clsEntResponseImagen resImag = new clsEntResponseImagen();

            listaPreguntasFuncion = listaPreguntas.FindAll(
                    delegate(clsEntPreguntaImagen bk)
                    {
                        if (bk.idFuncion == idFuncion)
                        {
                            return true;
                        }
                        return false;
                    }
                );

            return listaPreguntasFuncion;
        }




        public static List<spuConsultarCertificacion_Result> consultarCertificacion(int idCertificacion)
        {
            return clsDatExamen.consultarCertificacion(idCertificacion);
        }

        public static int validaIngresoCertificacion(int idRegistro, int idCertificacionRegistro)
        {

            return clsDatExamen.validaIngresoCertificacion(idCertificacionRegistro, idRegistro);
        }

        public static List<clsEntResponseCalificacion> consultarCalificacion(string strEvaluacionRespuesta,string identificadores)
        {
            List<clsEntResponseCalificacion> lstRespCalificacion = new List<clsEntResponseCalificacion>();
            
            if (strEvaluacionRespuesta != "")
            {
                    List<clsEntRespuestaExamen> lstEvaluacionRespuestas = new List<clsEntRespuestaExamen>();
                    
                    List<spuConsultarRespuestasTema_Result> lstRespuestas = (List<spuConsultarRespuestasTema_Result>)System.Web.HttpContext.Current.Session["lstRespuestas" + System.Web.HttpContext.Current.Session.SessionID];

                    List<spuConsultarTemasCertificacionExamen_Result> lstTemas = (List<spuConsultarTemasCertificacionExamen_Result>)System.Web.HttpContext.Current.Session["lstTemas" + System.Web.HttpContext.Current.Session.SessionID];

                    string[] strPregRespondida = strEvaluacionRespuesta.Split('|');
                    int i=0;
                    foreach(string cadena in strPregRespondida)
                    {
                        
                        string[] datos = cadena.Split('┐');
                        if (datos[0] != "")
                        {
                            clsEntRespuestaExamen evaluacionRepuesta = new clsEntRespuestaExamen();
                            evaluacionRepuesta.idTema = Convert.ToInt32(datos[0]);
                            evaluacionRepuesta.idFuncion = Convert.ToInt32(datos[1]);
                            evaluacionRepuesta.idPregunta = Convert.ToInt32(datos[2]);
                            evaluacionRepuesta.idRespuesta = Convert.ToInt32(datos[3]);

                            lstEvaluacionRespuestas.Add(evaluacionRepuesta);

                            if (!lstRespCalificacion.Exists(x => x.idTema == evaluacionRepuesta.idTema))
                            {
                                i++;
                                lstRespCalificacion.Add(new clsEntResponseCalificacion { idTema = evaluacionRepuesta.idTema, numero = i});
                            }
                            if (evaluacionRepuesta.idRespuesta!=0 && Convert.ToBoolean(lstRespuestas.Find(x => x.idRespuesta == evaluacionRepuesta.idRespuesta).resCorrecta))
                                lstRespCalificacion.Find(x => x.numero == i).preguntasCorrectas += 1;
                        }
                    }
                    
                    //Se comprueba que todos los temas fueron respondidos, si alguno no lo fue se agrega un cero a las preguntas correctas de ese tema
                    foreach (spuConsultarTemasCertificacionExamen_Result tema in lstTemas)
                    {

                        if (!lstRespCalificacion.Exists(x => x.idTema == tema.idTema))
                        {
                            i++;
                            lstRespCalificacion.Add(new clsEntResponseCalificacion
                                {
                                    idTema = tema.idTema,
                                    numero = i,
                                    preguntasCorrectas = 0,
                                    preguntasNecesarias = tema.ctCorrectas,
                                    preguntasPresentadas = tema.ctAleatorias
                                });
                        }
                        else {
                            lstRespCalificacion.Find(x => x.idTema == tema.idTema).preguntasNecesarias = tema.ctCorrectas;
                            lstRespCalificacion.Find(x => x.idTema == tema.idTema).preguntasPresentadas = tema.ctAleatorias;

                        }
                    }

                    int calificacion = lstRespCalificacion.Sum(x => x.preguntasCorrectas);

                    string[] ids = identificadores.Split('┐');
                    int idCertificacionRegistro = Convert.ToInt32(ids[0]);
                    int idRegistro = Convert.ToInt32(ids[1]);
                    int idCertificacion = Convert.ToInt32(ids[2]);

                    clsDatInsertar.insertarEvaluacion(lstEvaluacionRespuestas, calificacion, idCertificacionRegistro, idRegistro, idCertificacion);


                  //  System.Web.HttpContext.Current.Application[(string)System.Web.HttpContext.Current.Session["inicioSesion"]] = null;
                    System.Web.HttpContext.Current.Session["inicioSesion"] = null;//Cerramos la sesión activa
                    
            }

            return lstRespCalificacion;
        }
                    
        public static void reiniciaReloj()
        {
            //Cada vez que llame esta función, la variable de sesión se volverá nula, para reiniciar el reloj del examen para el nuevo tema.
            System.Web.HttpContext.Current.Session["tiempoExam" + System.Web.HttpContext.Current.Session.SessionID] = null;
        }

        public static TimeSpan consultaTiempoExam(int tiempo)
        {

            TimeSpan timespan;

            //La variable de sesión se reinicia en la funcion consultaFuncionesTema para resetear el reloj
            if (System.Web.HttpContext.Current.Session["tiempoExam" + System.Web.HttpContext.Current.Session.SessionID] == null)
            {
                System.Web.HttpContext.Current.Session["tiempoExam" + System.Web.HttpContext.Current.Session.SessionID] = DateTime.Now.AddMinutes(tiempo);
                timespan = Convert.ToDateTime(System.Web.HttpContext.Current.Session["tiempoExam" + System.Web.HttpContext.Current.Session.SessionID]).Subtract(DateTime.Now.AddSeconds(1)); //agrego un segundo para que el reloj inicie con menos un segundo
            }
            else {
                timespan = Convert.ToDateTime(System.Web.HttpContext.Current.Session["tiempoExam" + System.Web.HttpContext.Current.Session.SessionID]).Subtract(DateTime.Now);
        }

            return timespan;

    }
}
}
