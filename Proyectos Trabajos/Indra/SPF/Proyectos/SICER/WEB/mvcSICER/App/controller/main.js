
Ext.define('app.controller.main', {
    extend: 'Ext.app.Controller',
    models: [
   'catalogos.mdEstados'
    , 'catalogos.mdMunicipios'
    , 'catalogos.mdEvaluador'
    , 'catalogos.mdCertificaciones'
    , 'catalogos.mdIntegrantes'
    , 'catalogos.mdDependenciaExterna'
    , 'catalogos.mdNivelSeguridad'
    , 'catalogos.mdInstitucionExterna'
    , 'catalogos.mdInstAplicaExamen'
    , 'catalogos.mdLugarAplicacion'
    , 'catalogos.mdServicio'
    , 'catalogos.mdZona'
    , 'catalogos.mdDatosCorreo'
    , 'catalogos.mdEntidadCertificadora'
    , 'catalogos.mdEntidadEvaluadora'
    , 'Administracion.RegistroPersonas.mdCertificacionRegistro'
    , 'Administracion.RegistroPersonas.mdRegistroPersona'
    , 'Administracion.RegistroCertificacion.mdFuncion'
    , 'Administracion.RegistroCertificacion.mdOrdenTema'
    , 'Administracion.RegistroCertificacion.mdTemas'
    , 'Administracion.RegistroCertificacion.mdFuncion'
    , 'Administracion.RegistroCertificacion.mdPregunta'
    , 'Administracion.RegistroCertificacion.mdRespuesta'
    , 'Administracion.RegistroPersonas.mdContrasenaReg'
    , 'Administracion.RegistroPersonas.mdBusqueda'
    , 'Administracion.RegistroPersonas.mdEliminar'
    , 'Examen.mdTema'
    , 'Examen.mdFuncion'
    , 'Examen.mdPregunta'
    , 'Examen.mdRespuesta'
    , 'Examen.mdEvaluacionRespuesta'
    , 'Examen.mdCertificacionesRegistro'
    , 'Examen.mdCalificacion'
    , 'Examen.mdCertificacionPDF'
    ],

    stores: [
      'Administracion.stTreeMenu'
    , 'Administracion.stTreeCertificaciones'
    , 'catalogos.stEstados'
    , 'catalogos.stMunicipios'
    , 'catalogos.stEvaluador'
    , 'catalogos.stIntegrantes'
    , 'catalogos.stCertificaciones'
    , 'catalogos.stDependenciaExterna'
    , 'catalogos.stNivelSeguridad'
    , 'catalogos.stInstitucionExterna'
    , 'catalogos.stInstAplicaExamen'
    , 'catalogos.stLugarAplicacion'
    , 'catalogos.stZona'
    , 'catalogos.stServicio'
    , 'catalogos.stDatosCorreo'
    , 'catalogos.stEntidadCertificadora'
    , 'catalogos.stEntidadEvaluadora'
    , 'Administracion.RegistroPersonas.stCertificacionRegistro'
    , 'Administracion.RegistroPersonas.stRegistroPersona'
    , 'Administracion.RegistroCertificacion.stOrdenTema'
    , 'Administracion.RegistroCertificacion.stTemas'
    , 'Administracion.RegistroCertificacion.stFunciones'
    , 'Administracion.RegistroCertificacion.stPreguntas'
    , 'Administracion.RegistroCertificacion.stRespuestas'
    , 'Administracion.RegistroPersonas.stContrasenaReg'
    , 'Administracion.RegistroPersonas.stBusqueda'
    , 'Administracion.RegistroPersonas.stEliminar'
    , 'Examen.stTemas'
    , 'Examen.stFunciones'
    , 'Examen.stPreguntas'
    , 'Examen.stRespuestas'
    , 'Examen.stEvaluacionRespuesta'
    , 'Examen.stCertificacionesRegistro'
    , 'Examen.stCertificacionPDF'
    ],

    views: [
     'Administracion.login.loginAdmin'
     , 'Administracion.main.menutreeAdmin'
     , 'Administracion.main.mainViewAdmin'
     , 'Administracion.RegistroPersonas.RegistroPersonasView'
     , 'Administracion.RegistroPersonas.gridPanBusquedaIntegrantes'
     , 'Administracion.RegistroPersonas.gridpanBusqueda'/************************/
     , 'Administracion.RegistroPersonas.panDatosBasicos'
     , 'Administracion.RegistroPersonas.panDatosComplementarios'
     , 'Administracion.RegistroPersonas.panCertificaciones'
     , 'Administracion.RegistroPersonas.gridCertificaciones'
     , 'Administracion.RegistroCertificacion.RegistroExamenView'
     , 'Administracion.RegistroCertificacion.panDatosCertificacion'
     , 'Administracion.RegistroCertificacion.panTemasCertificaciones'
     , 'Administracion.RegistroCertificacion.panFuncionesCertificaciones'
     , 'Administracion.RegistroCertificacion.panPreguntasCertificaciones'
     , 'Administracion.RegistroCertificacion.gridTemas'
     , 'Administracion.RegistroCertificacion.gridFunciones'
     , 'Administracion.RegistroCertificacion.gridPreguntas'
     , 'Administracion.RegistroCertificacion.gridRespuestas'
     , 'Administracion.RegistroCertificacion.panRespuestasCertificaciones'
     , 'Administracion.RegistroCertificacion.pnlAgregarTemas'
     , 'Administracion.main.inicio'
     , 'Examen.login.loginExamen'
     , 'Examen.main.mainViewExamen'
     , 'Reportes.filtrosReporteCertificaciones'
     , 'Reportes.reporteCertificacionesView'
     , 'Reportes.reporteViewr'

    ],


    init: function () {
        var view;
        if (Ext.urlDecode(window.location.search.substring(1)).xcode == '500') {
            view = 'app.view.Examen.login.loginExamen';
            Ext.create(view);
            Ext.MessageBox.alert('SICER', '<b>Sesión Anulada</b></br>Al no existir actividad en el módulo por un periodo prolongado</br>usted será direccionado nuevamente a la pantalla de autenticación</br><b>Error</b>: El tiempo de la sesión a expirado');
            return false;
        }
        var res = document.getElementById("Message").value.split("|");

         if (res[0] == 'True') {
                //Se conecta por MAG
                view = 'app.view.Administracion.main.mainViewAdmin';
                Ext.create(view);
                            
                Ext.getCmp('lblUsuario').setText('[' + res[1] + ']');
                Ext.getCmp('lblPerfil').setText('[' + res[3] + ']');

                if (Ext.getCmp('tabInicio') == undefined) {
                    Ext.getCmp('tblPrincipal').add(
                    {
                        xtype: 'inicio',
                        id: 'tabInicio'
                    });
                    Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                }
                else {
                    Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                }


         }else{
                //view = 'app.view.Administracion.login.loginAdmin';
                view = 'app.view.Examen.login.loginExamen';
                Ext.create(view);
         }
       
        }
    }
);

    Ext.Ajax.setTimeout(60000); //Se da un minuto de espera, para evitar un timeout por parte de EXTJS en certificaciones muy grandes

    Ext.Date.monthNames = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];
    Ext.define("Ext.locale.es.picker.Date", {
        override: "Ext.picker.Date",
        todayText: "Hoy",
        minText: "Esta fecha es anterior a la fecha mínima",
        maxText: "Esta fecha es posterior a la fecha máxima",
        disabledDaysText: "",
        disabledDatesText: "",
        nextText: 'Mes Siguiente (Control+Right)',
        prevText: 'Mes Anterior (Control+Left)',
        monthYearText: 'Seleccione un mes (Control+Up/Down para desplazar el año)',
        todayTip: "{0} (Barra espaciadora)",
        format: "d/m/Y",
        startDay: 1
    });
