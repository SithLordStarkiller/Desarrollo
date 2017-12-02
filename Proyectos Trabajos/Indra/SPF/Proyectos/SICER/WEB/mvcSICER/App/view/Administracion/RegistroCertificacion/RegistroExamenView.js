Ext.define('app.view.Administracion.RegistroCertificacion.RegistroExamenView', {
    extend: 'Ext.form.Panel',
    alias: 'widget.RegistroExamenView',
    autoScroll: true,
    layout: {
        type: 'vbox',
        align: 'left'
    },
    defaults:
    {
        width: '100%'
    },
    closable: true,
    bodyPadding: '10 10 10 10',
    title: 'Registro de Certificación',
    initComponent: function () {
        var scopePrincipal = this;
        var permiso = (Ext.getCmp('hfPermisoCaptura').getValue() == 'true' ? false : true);
        Ext.apply(this, {
            lbar: [
                /*
                { text: 'Limpiar',
                    textAlign: 'left',
                    id: 'btnLimpiarCertificacion',
                    icon: 'Imagenes/policeIco.png',
                    handler: function () {
                        //AGREGAR PARA LA BUSQUEDA 

                    }
                },
                */

                { text: 'Guardar',
                    textAlign: 'left',
                    id: 'btnGuardarCertificacion',
                    //iconCls: 'guardar',
                    icon: 'Extjs/resources/images/icons/fam/save.gif',
                    handler: function () {
                        session();

                        Ext.MessageBox.show({
                            msg: 'Guardando',
                            progressText: 'Procesando...',
                            modal: true,
                            width: 200,
                            wait: true,
                            icon: 'Imagenes/download.gif'
                        });

                        validarDatos();

                       
                    }
                }
        ],
            collapsible: true,

            layout: {
                type: 'vbox',
                align: 'center'
            },
            items: [

             { xtype: 'panelDatosCertificacion'/*, height: 300*/ },
             { xtype: 'panelTemasCertificaciones'/*, height: 300 */ },
             { xtype: 'panelFuncionesCertificaciones'/*, height: 300*/ },
             { xtype: 'panelPreguntasCertificaciones'/*, height: 300*/ },
             { xtype: 'panelRespuestasCertificaciones'/*, height: 300*/ },
             { xtype: 'hiddenfield', id: 'hfIdCertificacion',value:'0' },

             ]
        });


        this.callParent(arguments);
    }
});


function validarDatos() {

    if (Ext.getCmp('txbNombreCertificacion').getValue().trim() == "") {
        Ext.MessageBox.alert('SICER', '<b>Nombre Certificación</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('txADescripcionCertificacion').getValue().trim() == "") {
        Ext.MessageBox.alert('SICER', '<b>Descripcíón Certificación</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('txbSiglas').getValue().trim() == "") {
        Ext.MessageBox.alert('SICER', '<b>Siglas Certificación</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('txbTiempoValidezCertificacionPrimeraVez').getValue().trim() == "") {
        Ext.MessageBox.alert('SICER', '<b>Tiempo Validez Certificación</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('txbTiempoValidezCertificacionRenovacion').getValue().trim() == "") {
        Ext.MessageBox.alert('SICER', '<b>Tiempo Validez Certificación Renovación</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('dfFechaDeAlta').getValue() == ("" || null)) {
        Ext.MessageBox.alert('SICER', '<b>Fecha de Alta Certificación</b></br> Campo obligatorio');
        return false;
    }


//    if (Ext.getCmp('txbNumeroDePreguntasExamen').getValue().trim() == "") {
//        Ext.MessageBox.alert('SICER', '<b>Número de Preguntas Examen</b></br> Campo obligatorio');
//        return false;
//    }

//    if (Ext.getCmp('txbNumeroDePreguntasCorrectasParaAprobar').getValue().trim() == "") {
//        Ext.MessageBox.alert('SICER', '<b>Número de Preguntas Correctas para Aprobar</b></br> Campo obligatorio');
//        return false;
//    }

    if (Ext.getCmp('txbTiempoParaNuevoIntento').getValue().trim() == "") {
        Ext.MessageBox.alert('SICER', '<b>Tiempo para nuevo intento</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('cbEntidadesCertificadoras').getValue() == ("" || null)) {
        Ext.MessageBox.alert('SICER', '<b>Entidades Certificadoras</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('cbEntidadesEvaluadoras').getValue() == ("" || null)) {
        Ext.MessageBox.alert('SICER', '<b>Entidades Evaluadoras</b></br> Campo obligatorio');
        return false;
    }

    guardar();

};

function guardar() {
    var strCertificacion = "";

    var strCertificacion =
            Ext.getCmp('hfIdCertificacion').getValue() + '|' +
            Ext.getCmp('txbNombreCertificacion').getValue().trim() + '|' +
            Ext.getCmp('txADescripcionCertificacion').getValue().trim() + '|' +
            Ext.getCmp('txbSiglas').getValue().trim() + '|' +
            Ext.getCmp('dfFechaDeAlta').getRawValue() + '|' +
            // Ext.getCmp('dfFechaDeBaja').getRawValue() + '|' +
             '|' +
            Ext.getCmp('txbTiempoValidezCertificacionPrimeraVez').getValue().trim() + '|' +
            Ext.getCmp('txbTiempoValidezCertificacionRenovacion').getValue().trim() + '|' +
            Ext.getCmp('txbNumeroDePreguntasExamen').getValue() + '|' +
            Ext.getCmp('txbNumeroDePreguntasCorrectasParaAprobar').getValue() + '|' +
            Ext.getCmp('txbTiempoParaNuevoIntento').getValue().trim() + '|' +
            Ext.getCmp('cbEntidadesEvaluadoras').getValue() + '|' +
            Ext.getCmp('cbEntidadesCertificadoras').getValue() + '|'+
            Ext.getCmp('CertificacionActiva').getValue() + '|';


    var strTemas = "";


    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').each(function (rec) {
        strTemas = strTemas +
      rec.data.idTema + '┐' +
      rec.data.temDescripcion + '┐' +
      rec.data.temCodigo + '┐' +
      rec.data.ctActivo + '┐' +
      rec.data.ctOrden + '┐' +
      rec.data.ctAleatorias + '┐' +
      rec.data.ctCorrectas + '┐' +
      rec.data.ctTiempo + '┐' +
      rec.data.idCertificacionTema + '┐' +
      rec.data.idTematemporal + '|';


    });


    var strFunciones = "";


    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').each(function (rec) {

        strFunciones = strFunciones +
        rec.data.idFuncion + '┐' +
        rec.data.funNombre + '┐' +
        rec.data.funAleatorias + '┐' +
        rec.data.funCorrectas + '┐' +
        rec.data.funTiempo + '┐' +
        rec.data.funOrden + '┐' +
        rec.data.funCodigo + '┐' +
        rec.data.tfActivo + '┐' +
        rec.data.idFuncionTemporal + '┐' +
        rec.data.idTema + '┐' +
        rec.data.idTemaFuncion + '┐' +              
        rec.data.idTemaTemporal + '|';     


    });


    var strPreguntas = "";

    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').each(function (rec) {
        strPreguntas = strPreguntas +
        rec.data.idPregunta + '┐' + //0
        rec.data.preDescripcion + '┐' + //1
        rec.data.preObligatoria + '┐' + //2
        rec.data.preCodigo + '┐' + //3
        rec.data.preActiva + '┐' + //4
        rec.data.idFuncionTemporal + '┐' +
         rec.data.idFuncion + '┐' +
        rec.data.idPreguntaTemporal + '┐' + 
        rec.data.identificadorImagen + '┐' + 
        //rec.data.imagen + '┐' + 
        rec.data.preNombreArchivo + '┐' + 
        rec.data.preTipoArchivo  +'|';


   });


  var strRespuestas = "";
      Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').each(function (rec) {
          strRespuestas = strRespuestas +
          rec.data.idRespuesta + '┐' +
          rec.data.resDescripcion + '┐' +
          rec.data.resExplicacion + '┐' +
          rec.data.resCorrecta + '┐' +
          rec.data.resActiva + '┐' +
          rec.data.idPreguntaTemporal + '┐' +
          rec.data.idPregunta + '┐' +
          rec.data.idRespuestaTemporal + '┐' +
          rec.data.resTipoArchivo + '┐' +
          rec.data.resNombreArchivo + '┐' +
         // rec.data.imagen + '┐' +
          rec.data.identificadorImagen + '|';    
          
    });


    Ext.Ajax.request({
        method: 'POST',
        url: 'Home/insertarCertificacion',
        params: { strCertificacion: strCertificacion, strTemas: strTemas, strFunciones: strFunciones, strPreguntas: strPreguntas, strRespuestas: strRespuestas },
        failure: function (response) {
           Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
          /*  var retorno = Ext.decode(response.responseText);
            if (retorno.success) {
                Ext.MessageBox.alert('Confirm', retorno.message);
            } else {
                // Show the error message 
                Ext.MessageBox.alert('Error Message', retorno.message);
            } */
        },
        success: function (response) {
            var retorno = Ext.decode(response.responseText);

            if (retorno == '') {
                Ext.MessageBox.alert('SICER', 'GUARDADO EXITOSAMENTE');
                cerrar();
            } else {
                Ext.MessageBox.alert('SICER - ERROR', retorno);
            }
        }
    });
                   };

function cerrar() {

    Ext.getCmp('tabRegistroCertificacion').destroy();
   
   //Actualizo Tree Certificaciones
    Ext.data.StoreManager.lookup('Administracion.stTreeCertificaciones').load();
    Ext.getCmp('treePanelCertificaciones').reconfigure(Ext.data.StoreManager.lookup('Administracion.stTreeCertificaciones'));
   
}




function desactivaPregunta(idPregunta, idPreguntaTemporal) {

    var respuestasPregunta = 0, respuestasCorrectas = 0;
    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').each(function (rec) {
        if (rec.data.resActiva == true
            && rec.data.idPregunta == idPregunta
            && rec.data.idPreguntaTemporal == idPreguntaTemporal) {
            respuestasPregunta++;
            if (rec.data.resCorrecta)
                respuestasCorrectas++;
        }
    });
    if (respuestasPregunta < 2 || respuestasCorrectas == 0) {
        var idFuncion=0, idFuncionTemporal=0,preCodigo='';
        
        Ext.getCmp('gridPreguntas').store.each(function (rec) {
            if (rec.data.idPregunta == idPregunta && rec.data.idPreguntaTemporal == idPreguntaTemporal) {
                rec.data.preActiva = false;
                idFuncion = rec.data.idFuncion;
                idFuncionTemporal = rec.data.idFuncionTemporal;
                preCodigo = rec.data.preCodigo;
            }
        });
        Ext.getCmp('gridPreguntas').getView().refresh(true);

        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').findRecord('preCodigo', preCodigo, 0, false, true, true).data.preActiva = false;

        desactivaFuncion(idFuncion, idFuncionTemporal);
       
    }


}

function desactivaFuncion(idFuncion, idFuncionTemporal) {
    var preguntasFuncion = 0;
    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').each(function (rec) {
        if (rec.data.preActiva == true
            && rec.data.idFuncion == idFuncion
            && rec.data.idFuncionTemporal == idFuncionTemporal)
            preguntasFuncion++;
    });
    if ( preguntasFuncion == 0) {
        var idTemaTemporal = 0, idTema = 0, funCodigo = '';
        Ext.getCmp('gridFunciones').store.each(function (rec) {
            if (rec.data.idFuncion == idFuncion && rec.data.idFuncionTemporal == idFuncionTemporal) {
                rec.data.tfActivo = false;
                idTema = rec.data.idTema;
                idTemaTemporal = rec.data.idTemaTemporal;
                funCodigo = rec.data.funCodigo;
            }
        });

        Ext.getCmp('gridFunciones').getView().refresh(true);

        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').findRecord('funCodigo', funCodigo, 0, false, true, true).data.tfActivo = false;

        desactivaTema(idTema,idTemaTemporal);

    }
}

function desactivaTema(idTema, idTemaTemporal) {

    var funcionesTema = 0;
    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').each(function (rec) {
        if (rec.data.tfActivo == true
            && rec.data.idTema == idTema
            && rec.data.idTemaTemporal == idTemaTemporal)
            funcionesTema++;
    });

    if (funcionesTema == 0) {

        var temCodigo = '';

        Ext.getCmp('gridTemas').store.each(function (rec) {
            if (rec.data.idTema == idTema && rec.data.idTematemporal == idTemaTemporal) {
                rec.data.ctActivo = false;
                temCodigo = rec.data.temCodigo;
            }

        });

        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').findRecord('temCodigo', temCodigo, 0, false, true, true).data.ctActivo = false;
        
        Ext.getCmp('gridTemas').getView().refresh(true);

        desactivaCertificacion();
    }
}

function desactivaCertificacion() {

    var tiempo = 0,
        preguntas = 0,
        preguntasCorrectas = 0;
    Ext.getCmp('gridTemas').store.each(function (recTema) {
        if (recTema.data.ctActivo) {
            tiempo += recTema.data.ctTiempo;
            preguntas += recTema.data.ctAleatorias;
            preguntasCorrectas += recTema.data.ctCorrectas;
        }
    });

    Ext.getCmp('txbTiempoTotalExamen').setValue(tiempo);
    Ext.getCmp('txbNumeroDePreguntasExamen').setValue(preguntas);
    Ext.getCmp('txbNumeroDePreguntasCorrectasParaAprobar').setValue(preguntasCorrectas);




    var temaCertificacion = 0;
    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').each(function (rec) {
        if (rec.data.ctActivo == true)
            temaCertificacion++;
    });

    if (temaCertificacion == 0) {
        Ext.getCmp('CertificacionActiva').setValue(false);
    }


}
