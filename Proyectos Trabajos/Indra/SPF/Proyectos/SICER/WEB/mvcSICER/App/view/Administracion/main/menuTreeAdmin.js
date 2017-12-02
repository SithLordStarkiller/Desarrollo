Ext.define('app.view.Administracion.main.menutreeAdmin', {
    extend: 'Ext.form.Panel',
    layout: 'accordion',
    id: 'treePrincipal',
    alias: 'widget.menutreeAdmin',
    layoutConfig: {
        animate: true,
        activeOnTop: true
    },
    initComponent: function () {

        var permisoPan1;
        var permisoPan2;
        var permisoPan3;
        var permisoPan4;
        var permisoPan5;
        var permisoPan6;

        Ext.Ajax.request({
            method: 'POST',
            url: 'Sistema/rolPermiso',

            failure: function () {
                Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
            },

            success: function (response) {
                var retorno = Ext.decode(response.responseText);

                permisoPan1 = retorno[0].pmCaptura;
                permisoPan2 = retorno[1].pmCaptura;
                permisoPan3 = retorno[2].pmCaptura;
                permisoPan4 = retorno[3].pmCaptura;
                permisoPan5 = retorno[4].pmCaptura;
                permisoPan6 = retorno[5].pmCaptura;

                if (!permisoPan2) { Ext.getCmp('treePanelCertificaciones').setVisible(false) }
                //                Ext.getCmp('treeRegistros').setVisible(false);
                //                Ext.getCmp('treePanelCertificaciones').setVisible(false);
                //                Ext.getCmp('treeSistema').setVisible(false);
                //                Ext.getCmp('treeReportes').setVisible(false);
            }
        });

        var storeRegistros = Ext.create('app.store.Administracion.stTreeMenu').load({ params: { tipoOperacion: 'REGISTRO'} });
        var storeCertificaciones = Ext.create('app.store.Administracion.stTreeCertificaciones').load();
        var storeSistema = Ext.create('app.store.Administracion.stTreeMenu').load({ params: { tipoOperacion: 'SISTEMA'} });
        var storeReportes = Ext.create('app.store.Administracion.stTreeMenu').load({ params: { tipoOperacion: 'REPORTES'} });

        Ext.apply(this, {
            items: [
            {
                xtype: 'treepanel',
                title: 'Registro',
                id: 'treeRegistros',
                rootVisible: false,
                store: storeRegistros,
                //                store: Ext.create('app.store.Administracion.stTreeMenu').load({ params: { tipoOperacion: 'REGISTRO'} }),

                listeners:
                {
                    itemclick: function (view, record, item, index, e) {
                        session();
                        Ext.getCmp('hfPermisoCaptura').setValue(record.raw.pmCaptura);
                        Ext.getCmp('hfPermisoModifica').setValue(record.raw.pmModifica);
                        switch (record.raw.idMenu) {
                            case 1: //Registro de Personas

                                if (Ext.getCmp('tabRegistroCertificacion') != undefined) {
                                    Ext.MessageBox.alert('SICER', 'Es necesario cerrar la pestaña actual antes de abrir otra.');
                                    return;
                                }

                                if (Ext.getCmp('tabRegistroPersonas') == undefined) {

                                    Ext.getCmp('tblPrincipal').add(
                                         { xtype: 'RegistroPersonasView', /*, closable: false*/
                                             id: 'tabRegistroPersonas',
                                             closeAction: 'destroy',
                                             listeners: {
                                                 //activate : { fn: function(fA) { alert('A' + fA) } },
                                                 //close : { fn: function(fA) { alert('A' + fA) } },
                                                 beforeclose: { fn: function () {
                                                     //                                                     Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                                                     return (confirm('Cualquier cambio no guardado, se perderá \n ¿Estas seguro de cerrar la ventana?'/*, function (btnText) {
                                                         if (btnText == 'Aceptar') {
                                                             Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                                                         } else if (btnText == 'Cancelar') {
                                                             Ext.getCmp('tblPrincipal').setActiveTab('tabRegistroPersonas');
                                                         }
                                                 }*/));
                                                 } 
                                                 },
                                                 close: { fn: function () {
                                                     //                                                     Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                                                     Ext.getCmp('gridCertificaciones').store.removeAll();
                                                     Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stRegistroPersona').removeAll();
                                                     Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stEliminar').removeAll();


                                                 }
                                                 }
                                             }
                                         }
                                        );
                                    Ext.getCmp('tblPrincipal').setActiveTab('tabRegistroPersonas');
                                }
                                else {
                                    Ext.getCmp('tblPrincipal').setActiveTab('tabRegistroPersonas');
                                }
                                break;

                            case 2: //Agregar Certificación.

                                if (Ext.getCmp('tabRegistroPersonas') != undefined) {
                                    Ext.MessageBox.alert('SICER', 'Es necesario cerrar la pestaña actual antes de abrir otra.');
                                    return;
                                }

                                if (Ext.getCmp('tabRegistroCertificacion') == undefined) {

                                    Ext.getCmp('tblPrincipal').add(
                                         { xtype: 'RegistroExamenView', /*, closable: false*/
                                             id: 'tabRegistroCertificacion',
                                             closeAction: 'destroy',
                                             listeners: {
                                                 //activate : { fn: function(fA) { alert('A' + fA) } },
                                                 //close : { fn: function(fA) { alert('A' + fA) } },
                                                 beforeclose: { fn: function () {
                                                     //                                                     Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                                                     return (confirm('Cualquier cambio no guardado, se perderá \n ¿Estas seguro de cerrar la ventana?'/*, function (btnText) {
                                                         if (btnText == 'Aceptar') {
                                                             Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                                                         } else if (btnText == 'Cancelar') {
                                                             Ext.getCmp('tblPrincipal').setActiveTab('tabRegistroCertificacion');
                                                         }
                                                     }*/));
                                                 }
                                                 }
                                                 //                                                , close: { fn: function () {
                                                 //                                                     Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                                                 //                                                 }
                                                 //                                                 }
                                             }
                                         }
                                        );

                                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').removeAll();
                                    Ext.data.StoreManager.lookup('stFuncionesTemporal').removeAll();
                                    Ext.data.StoreManager.lookup('stPreguntasTemporal').removeAll();
                                    Ext.data.StoreManager.lookup('stRespuestasTemporal').removeAll();

                                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').removeAll();
                                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').removeAll();
                                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').removeAll();
                                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').removeAll();

                                    Ext.getCmp('tblPrincipal').setActiveTab('tabRegistroCertificacion');
                                    Ext.getCmp('hfIdCertificacion').setValue('0');
                                }
                                else {

                                    Ext.getCmp('tblPrincipal').setActiveTab('tabRegistroCertificacion');
                                }
                                break;


                        }
                    },
                    afterrender: function () {

                        this.items.length == 0 ? this.setVisible(false) : this.setVisible(true);
                        //                        this.getStore().getCount() == 0 ? this.setVisible(false) : this.setVisible(true);
                    }
                }
            }
            ,
            /*{
            xtype: 'panel',
            scrollable: true,
            title: 'Certificaciones',
            items:[*/
                    {
                    xtype: 'treepanel',
                    title: 'Certificaciones',
                    id: 'treePanelCertificaciones',
                    animate: true,
                    rootVisible: false,
                    store: storeCertificaciones,
                    //                    store: Ext.create('app.store.Administracion.stTreeCertificaciones').load(),
                    listeners: {
                        itemclick: function (view, record, item, index, e) {
                            session();

                            if (Ext.getCmp('tabRegistroPersonas') != undefined) {
                                Ext.MessageBox.alert('SICER', 'Es necesario cerrar la pestaña actual antes de abrir otra.');
                                return;
                            }

                            if (Ext.getCmp('tabRegistroCertificacion') == undefined) {

                                Ext.MessageBox.show({
                                    msg: 'Realizando la petición al servidor, por favor espere...',
                                    progressText: 'Procesando...',
                                    modal: true,
                                    width: 200,
                                    wait: true,
                                    icon: 'Imagenes/download.gif'
                                });

                                //removeAll todos los store temporales
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').removeAll();
                                if (!(Ext.data.StoreManager.lookup('stFuncionesTemporal') == undefined))
                                    Ext.data.StoreManager.lookup('stFuncionesTemporal').removeAll();
                                if (!(Ext.data.StoreManager.lookup('stPreguntasTemporal') == undefined))
                                    Ext.data.StoreManager.lookup('stPreguntasTemporal').removeAll();
                                if (!(Ext.data.StoreManager.lookup('stRespuestasTemporal') == undefined))
                                    Ext.data.StoreManager.lookup('stRespuestasTemporal').removeAll();

                                var idCertificacion = record.raw.idMenu;

                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').load({ params: { idCertificacion: idCertificacion} });
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').load({ params: { idCertificacion: idCertificacion} });
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').load({ params: { idCertificacion: idCertificacion} });
                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').load({ params: { idCertificacion: idCertificacion} });

                                Ext.Ajax.request({
                                    method: 'POST',
                                    url: 'Home/buscarCertificacion',
                                    params: { idCertificacion: idCertificacion },
                                    failure: function () { },
                                    success: function (response) {
                                        var retorno = Ext.decode(response.responseText);


                                        var tiempoCarga = 0;
                                        var taskCertificacion = Ext.TaskManager.start({
                                            run: function () {
                                                tiempoCarga += 1;
                                                if (tiempoCarga > 3)
                                                    if (!Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').isLoading()
                                                            && !Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stPreguntas').isLoading()
                                                            && !Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stRespuestas').isLoading()) {
                                                        Ext.TaskManager.stop(taskCertificacion);
                                                        Ext.MessageBox.hide();

                                                        Ext.getCmp('tblPrincipal').add(
                                                             { xtype: 'RegistroExamenView',
                                                                 id: 'tabRegistroCertificacion',
                                                                 listeners: {
                                                                     beforeclose: { fn: function () { return (confirm('Cualquier cambio no guardado, se perderá \n ¿Estas seguro de cerrar la ventana?')); } }
                                                                 }
                                                             }
                                                            );

                                                        Ext.getCmp('tblPrincipal').setActiveTab('tabRegistroCertificacion');

                                                        Ext.Ajax.request({
                                                            method: 'POST',
                                                            url: 'Catalogos/consultarCalificacion',
                                                            params: { idCertificacion: retorno[0].idCertificacion },
                                                            failure: function () { },
                                                            success: function (response) {
                                                                var regreso = Ext.decode(response.responseText);

                                                                if (regreso) {
                                                                    Ext.getCmp('txbNombreCertificacion').setDisabled(true);
                                                                    Ext.getCmp('txADescripcionCertificacion').setDisabled(true);
                                                                    Ext.getCmp('txbSiglas').setDisabled(true);
                                                                    Ext.getCmp('txbTiempoValidezCertificacionPrimeraVez').setDisabled(true);
                                                                    Ext.getCmp('txbTiempoValidezCertificacionRenovacion').setDisabled(true);
                                                                    Ext.getCmp('dfFechaDeAlta').setDisabled(true);
                                                                    Ext.getCmp('txbTiempoParaNuevoIntento').setDisabled(true);
                                                                    Ext.getCmp('cbEntidadesCertificadoras').setDisabled(true);
                                                                    Ext.getCmp('cbEntidadesEvaluadoras').setDisabled(true);
                                                                    Ext.getCmp('tbarAgrTema').setDisabled(true);
                                                                    Ext.getCmp('tbarModTema').setDisabled(true);
                                                                    Ext.getCmp('tbarAgrFun').setDisabled(true);
                                                                    Ext.getCmp('tbarModFun').setDisabled(true);
                                                                    Ext.getCmp('checkColTemas').setDisabled(true);
                                                                    Ext.getCmp('checkColFunciones').setDisabled(true);
                                                                }
                                                            }
                                                        });
                                                        var tiempo = 0;
                                                        Ext.getCmp('gridTemas').store.each(function (recTema) {
                                                            if (recTema.data.ctActivo) tiempo += recTema.data.ctTiempo;
                                                        });
                                                        Ext.getCmp('txbTiempoTotalExamen').setValue(tiempo);


                                                        Ext.getCmp('cbEntidadesCertificadoras').store.load();
                                                        Ext.getCmp('cbEntidadesEvaluadoras').store.load();

                                                        idCertificacion = retorno[0].idCertificacion;
                                                        Ext.getCmp('hfIdCertificacion').setValue(retorno[0].idCertificacion);
                                                        Ext.getCmp('txbNombreCertificacion').setValue(retorno[0].cerNombre);
                                                        Ext.getCmp('txADescripcionCertificacion').setValue(retorno[0].cerDescripcion);
                                                        Ext.getCmp('txbSiglas').setValue(retorno[0].cerSiglas);
                                                        Ext.getCmp('txbTiempoValidezCertificacionPrimeraVez').setValue(retorno[0].cerPrimeraValidez);
                                                        Ext.getCmp('txbTiempoValidezCertificacionRenovacion').setValue(retorno[0].cerRenovacionValidez);
                                                        Ext.getCmp('dfFechaDeAlta').setValue(retorno[0].cerFechaAlta);
                                                        //Ext.getCmp('dfFechaDeBaja').setValue(retorno[0].cerFechaBaja);
                                                        Ext.getCmp('CertificacionActiva').setValue(retorno[0].cerActiva);

                                                        Ext.getCmp('txbNumeroDePreguntasExamen').setValue(retorno[0].cerPreguntas);
                                                        Ext.getCmp('txbNumeroDePreguntasCorrectasParaAprobar').setValue(retorno[0].cerPreguntasCorrectas);
                                                        Ext.getCmp('txbTiempoParaNuevoIntento').setValue(retorno[0].cerTiempoIntento);
                                                        //Ext.getCmp('cbEntidadesCertificadoras').setValue(retorno[0].ecDescripcion);
                                                        // xt.getCmp('cbEntidadesEvaluadoras').setValue(retorno[0].eeDescripcion);
                                                        Ext.getCmp('cbEntidadesCertificadoras').setValue(retorno[0].idEntidadCertificadora);
                                                        Ext.getCmp('cbEntidadesEvaluadoras').setValue(retorno[0].idEntidadEvaluadora);
                                                    }
                                            },
                                            interval: 100
                                        });


                                    }
                                });




                            }
                            /* else {

                            Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').removeAll();
                            Ext.data.StoreManager.lookup('stFuncionesTemporal').removeAll();
                            Ext.data.StoreManager.lookup('stPreguntasTemporal').removeAll();
                            Ext.data.StoreManager.lookup('stRespuestasTemporal').removeAll();

                            Ext.getCmp('tblPrincipal').setActiveTab('tabRegistroCertificacion');

                            //
                            }*/
                        }
                    }
                },

            {
                xtype: 'treepanel',
                title: 'Reportes',
                id: 'treeReportes',
                rootVisible: false,
                //                store: Ext.create('app.store.Administracion.stTreeMenu').load({ params: { tipoOperacion: 'REPORTES'} }),
                store: storeReportes,
                listeners:
                {
                    itemclick: function (view, record, item, index, e) {
                        session();
                        //Ext.getCmp('hfPermisoCaptura').setValue(record.raw.pmCaptura);
                        //Ext.getCmp('hfPermisoModifica').setValue(record.raw.pmModifica);
                        switch (record.raw.idMenu) {

                            case 6:
                                if (Ext.getCmp('tabReporteCertificaciones') == undefined) {
                                    Ext.getCmp('tblPrincipal').add({ xtype: 'reporteCertificaciones', id: 'tabReporteCertificaciones' });
                                    Ext.getCmp('tblPrincipal').setActiveTab('tabReporteCertificaciones');
                                }
                                else {
                                    Ext.getCmp('tblPrincipal').setActiveTab('tabReporteCertificaciones');
                                }


                        }
                    },
                    afterrender: function () {

                        this.items.length == 0 ? this.setVisible(false) : this.setVisible(true);
                        //                        storeReportes.getCount() == 0 ? this.setVisible(true) : this.setVisible(false);
                    }
                }
            },
            {
                xtype: 'treepanel',
                title: 'Sistema',
                id: 'treeSistema',
                rootVisible: false,
                store: storeSistema,
                //                store: Ext.create('app.store.Administracion.stTreeMenu').load({ params: { tipoOperacion: 'SISTEMA'} }),
                listeners:
                {
                    itemclick: function (view, record, item, index, e) {
                        session();
                        //Ext.getCmp('hfPermisoCaptura').setValue(record.raw.pmCaptura);
                        //Ext.getCmp('hfPermisoModifica').setValue(record.raw.pmModifica);
                        switch (record.raw.idMenu) {
                            case 3: //Acerca de
                                if (Ext.getCmp('tabAcerca') == undefined) {

                                    Ext.getCmp('tblPrincipal').add({
                                        title: 'Acerca de',
                                        closable: true,
                                        id: 'tabAcerca',
                                        html: 'Nomenclatura: DTIC-075​ SICER</br>Módulo: Sistema de Certificaciones</br>Versión 1.1.0<br/>----------------------------------------------------<br>' +
                                                      'Liberación <br/>27/07/2016 VER. 1.0.0<br/>----------------------------------------------------<br>' +
                                                      'Actualización <br/>1/09/2016 VER. 1.1.0<br/>----------------------------------------------------<br>'+
                                                      'Actualización <br/>28/10/2016 VER. 1.2.0<br/>----------------------------------------------------<br>'+
                                                      'Actualización <br/>02/12/2016 VER. 1.2.1<br/>----------------------------------------------------<br>'

                                                , activeOnTop: true
                                    }
                                          );
                                    Ext.getCmp('tblPrincipal').setActiveTab('tabAcerca');

                                }
                                else {
                                    Ext.getCmp('tblPrincipal').setActiveTab('tabAcerca');
                                }

                                break;
                            case 4: //Manual
                                if (Ext.getCmp('tabManual') == undefined) {
                                    Ext.getCmp('tblPrincipal').add(
                                    {
                                        title: 'Manual de Usuario',
                                        id: 'tabManual',
                                        closable: true,
                                        //items:[
                                        //{
                                        //    xtype: 'panel',
                                        //    layout: 'vbox',
                                        html: '<object type="text/html" data="Manual/manualAdministracion.pdf"  style="float:left;width:100%;height:100%;" background-image:url(../Imagenes/cargandoObjeto.gif); background-repeat:no-repeat; background-position:center;></object>'
                                        //}
                                        //]
                                    });
                                    Ext.getCmp('tblPrincipal').setActiveTab('tabManual');
                                }
                                else {
                                    Ext.getCmp('tblPrincipal').setActiveTab('tabManual');
                                }

                                break;

                            case 5: //Salir
                                if (Ext.getCmp('tabInicio') == undefined) {
                                    Ext.getCmp('tblPrincipal').add({ xtype: 'inicio', id: 'tabInicio', activeOnTop: true });
                                }

                                Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                                Ext.MessageBox.show({
                                    title: 'SICER',
                                    msg: '<p ALIGN=center>¿Desea finalizar la sesión?</p>',
                                    buttonText: { yes: "Sí", no: "No" },
                                    fn: function (btn) {
                                        switch (btn) {
                                            case 'yes':
                                                window.location = '';
                                                break;
                                            case 'no':
                                                this.close;
                                                break;
                                        }
                                    },
                                    icon: Ext.MessageBox.QUESTION
                                });
                                //                                        Ext.getCmp('lblBien').setText(Ext.getCmp('lblUsuario').text);

                                break;
                        }
                    }
                }
            }

            /*]//Termina Items

                
            }//Termina panel scrollable
            */

            ]
        });
        this.callParent(arguments);
    }
});


