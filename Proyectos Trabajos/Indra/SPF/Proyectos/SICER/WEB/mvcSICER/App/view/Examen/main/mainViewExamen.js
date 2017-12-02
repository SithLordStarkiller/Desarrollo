

/*Nota al programador:
    No insertar ningún alert de javascript en esta ventana o alguna otra función que detenga al Ext.TaskManager
    
*/
var taskTema;
var scope;
var hora;
var back = true;
Ext.define('app.view.Examen.main.mainViewExamen', {
    extend: 'Ext.container.Viewport',
    //closable: true,
    alias: 'widget.mainViewExamen',
    initComponent: function () {
        sessionExamen();


         //Evita que se cierre el explorador en medio del examen. //PROBLEMA!!!!!! El taskmanager se detiene cuando aparece la ventana
                window.onbeforeunload = function () {
//                    return false;
                    return '¿Estás seguro de salir del examen?';
                };
//        window.onbeforeunload = function () {
//            Ext.Msg.show({
//                title: '<span style="font-size: 140%;margin-top:20px;">CERRANDO VENTANA</span>',
//                message: '<span style="font-size: 120%;margin-top:20px;">¿Éstas seguro de terminar el tema?<br>Ya no podrás regresar para cambiar tus respuestas.</span>',
//                closable: false,
//                //modal: true,
//                buttons: Ext.Msg.YESNO,
//                buttonText: {
//                    no: 'Cancelar',
//                    yes: 'Aceptar'
//                },
//                icon: Ext.Msg.WARNING,
//                fn: function (btn) {
//                    if (btn === 'yes') {
//                       
//                    }
//                    else {

//                        return true;
//                    }
//                }
//            });
//            return;
//        };
//        Ext.EventManager.on(window, 'beforeunload', function () {
//           Ext.Msg.show({
//                title: '<span style="font-size: 140%;margin-top:20px;">CERRANDO VENTANA</span>',
//                message: '<span style="font-size: 120%;margin-top:20px;">¿Éstas seguro de terminar el tema?<br>Ya no podrás regresar para cambiar tus respuestas.</span>',
//                closable: false,
//                //modal: true,
//                buttons: Ext.Msg.YESNO,
//                buttonText: {
//                    no: 'Cancelar',
//                    yes: 'Aceptar'
//                },
//                icon: Ext.Msg.WARNING,
//                fn: function (btn) {
//                    if (btn === 'yes') {
//                       
//                    }
//                    else {

//                        return true;
//                    }
//                }
//            });

//        });

        scope = this;
        Ext.apply(this, {
            layout: 'border',
            items: [
             {
                 region: 'north',
                 border: false,
                 height: 70,
                 items: [{ xtype: 'image',
                     renderTo: Ext.getBody(),
                     id: 'pleca',
                     src: 'Imagenes/CabeceraPlecaSmall.png',
                     height: 70
                 },
                {
                    xtype: 'image',
                    id: 'blanco',
                    width: 100,
                    height: 70

                },
                { xtype: 'image',
                    id: 'logo',
                    src: 'Imagenes/siglasSICER3.png',
                    renderTo: Ext.getBody(),
                    height: 70
                }]
             }
                    , {
                        xtype: 'box',
                        id: 'header',
                        region: 'north',
                        height: 40,
                        html: '<h3>SISTEMA DE CERTIFICACIONES</h3>'
                    }
                    , {
                        region: 'south',
                        border: false,
                        tbar: { items: [
                            /* { text: 'Usuario: ' },
                            , { xtype: 'label', id: 'lblUsuario', text: '-' }
                            
                            ,{ text: 'Perfil: ' }
                            ,{ xtype: 'label', id: 'lblPerfil', text: '-' }
                            ,{ xtype: 'hiddenfield', id: 'hfPermisoCaptura', value: '' }
                            ,{ xtype: 'hiddenfield', id: 'hfPermisoModifica', value: '' }
                            */
                    ]
                        }
                    },
                    {
                        region: 'west',
                        //                        title: 'Instrucciones',
                        layout: 'fit',
                        items: [
                            {
                                xtype: 'panel',
                                title: '<span style="font-size: 140%;margin-top:20px;">Instrucciones para realizar el examen de certificación</span>',
                                id: 'panInstrucciones',
                                bodyPadding: '10 10 10 10',
                                width: 450,
                                autoScroll: true,
                                layout: {
                                    type: 'vbox',
                                    align: 'left'
                                    //  columns: 2
                                },
                                items: [
                                            {
                                                html: '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><html xmlns="http://www.w3.org/1999/xhtml" ><head><title>Instrucciones Certificación</title></head><body><table align="left"><tr><th align="left", width="400">Seleccinar Examen</th></tr><tr><td width="400">Seleccionar el examen de certificación que realizará, en la lista de exámenes disponibles.</td></tr><tr><td align="center",width="350"><img src = "../Imagenes/selecExamen.png" /></td></tr><tr><td width="400">Dar clic en el botón de comenzar examen.</td></tr><tr><td align="center",width="350"><img src = "../Imagenes/comenzarExamen.png" /></td></tr><tr><th align="left", width="400">Iniciar Examen</th></tr><tr><td width="400">Una ventana emergente preguntará si desea comenzar el examen.</td></tr><tr><td width="400">Después de confirmar, el tiempo del examen comenzara a correr.</td></tr><tr><th align="left", width="400">Resolver Examen</th></tr><tr><td width="400">Aparecerán preguntas con imágenes y respuestas para responder.</td></tr><tr><td width="400">Se tienen que contestar todas las preguntas para completar una función.</td></tr><tr><td width="400">Para completar un tema, se tienen que contestar todas las funciones.</td></tr><tr><td width="400">Una vez contestadas todas las funciones de un tema, se oprime el botón "Siguiente tema".</td></tr><tr><td align="center",width="350"><img src = "../Imagenes/siguienteTema.png" /></td></tr><tr><td width="400">Una vez terminados todos los temas se oprime el botón "Finalizar examen".</td></tr><tr><td align="center",width="350"><img src = "../Imagenes/finalizarExamen.png" /></td></tr><tr><td width="400"><b>ADVERTENCIA:</b> No cerrar el explorador en ningún momento durante el examen.</td></tr><tr><td width="400">Una vez finalizado el examen, los resultados aparecerán en pantalla.</td></tr><tr><th align="left", width="400">Resultados</th></tr><tr><td width="400">En la pantalla de resultados se muestran el número total de preguntas del examen y el número necesario de preguntas para aprobarlo.</td></tr><tr><td width="400">Los resultados son:</td></tr><tr><td width="400">Competente - Resultado que se obtiene solo cuando el examen se aprobó.</td></tr><tr><td width="400">Todavía No Competente - Indica que no se aprobó el examen.</td></tr></table></body></html>'
                                            },
                                        ]
                            }
                        ]
                    },
                     {
                         region: 'center',
                         //title: 'Contenedor principal',
                         layout: 'fit',

                         items:
                            {
                                xtype: 'panel',
                                id: 'panPrincipal',
                                modal: true,
                                layout: {
                                    type: 'vbox',
                                    align: 'center'
                                    , pack: 'center'
                                }
                                , //autoScroll: true,
                                items: [
                                { xtype: 'hiddenfield', id: 'hfTema', value: '0',
                                    getValue: function () {
                                        var value;
                                        var v = this.getSubmitValue();
                                        value = parseInt(v);
                                        return value;
                                    }
                                },
                                { xtype: 'hiddenfield', id: 'hfCantidadTemas', value: '0',
                                    getValue: function () {
                                        var value;
                                        var v = this.getSubmitValue();
                                        value = parseInt(v);
                                        return value;
                                    }
                                },

                                { xtype: 'hiddenfield', id: 'hfCertificacion', value: '0',
                                    getValue: function () {
                                        var value;
                                        var v = this.getSubmitValue();
                                        value = parseInt(v);
                                        return value;
                                    }
                                },
                              
                                    {
                                        xtype: 'panel',
                                        title: '<span style="font-size: 160%;margin-top:20px;">Bienvenido al Sistema de Certificaciones</span>',
                                        id: 'panSeleccionarCertificacion',
                                        bodyPadding: '10 10 10 10',
                                        margin: '200 0 200 0',
                                        width: '50%',
                                        layout: {
                                            type: 'vbox',
                                            align: 'center'
                                            //  columns: 2
                                        },
                                        items: [
                                                 {
                                                     xtype: 'label',
                                                     //id: 'lblElijaExamen',
                                                     //text: 'DOMICILIO PARTICULAR',
                                                     html: '<h4 style="font-size: 160%;">Elija el examen de certificación que realizará:</h4>'
                                                     //  , width: 800
                                                 },
                                                {
                                                    xtype: 'combo',
                                                    id: 'ddlExamenesCertificacionUsuario',
                                                    store: 'Examen.stCertificacionesRegistro',
                                                    emptyText: 'SELECCIONAR',
                                                    triggerAction: 'all',
                                                    queryMode: 'local',
                                                    grow: true,
                                                    editable: false,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    /*fieldLabel: "ESTADO",
                                                    labelAlign: 'right',
                                                    labelWidth: 120,*/
                                                    width: 300,
                                                    valueField: 'idCertificacionRegistro',
                                                    displayField: 'cerNombre',
                                                    //  disabled: true,
                                                    // msgTarget: 'side',
                                                    listeners:
                                                    {
                                                        afterRender: function (field, newValue, oldValue) {
                                                            /*  if (Ext.getCmp('ddlExamenesCertificacionUsuario').store.getCount() == 0) {
                                                            Ext.getCmp('ddlExamenesCertificacionUsuario').setDisabled(true);
                                                            }*/
                                                        }
                                                    }
                                                },
                                                {
                                                    xtype: 'button'
                                                    , text: 'Comenzar Examen',
                                                    margin: '0 0 20 0'
                                                    , handler: function () {
                                                        sessionExamen();
                                                        if (Ext.getCmp('ddlExamenesCertificacionUsuario').getValue() == null || Ext.getCmp('ddlExamenesCertificacionUsuario').getValue() == 0 || Ext.getCmp('ddlExamenesCertificacionUsuario').getValue() == 'SELECCIONAR') {

                                                            if (Ext.getCmp('ddlExamenesCertificacionUsuario').store.getCount() == 0) {
                                                                Ext.Msg.alert('SICER', 'Usted no tiene ninguna certificación asignada, por favor contacte al administrador.');
                                                            }
                                                            else {
                                                                Ext.Msg.alert('SICER', 'Es necesario seleccionar una certificación');
                                                            }
                                                            return;
                                                        } else {
                                                            var certificacionRecord = Ext.getCmp('ddlExamenesCertificacionUsuario').store.findRecord('idCertificacionRegistro', Ext.getCmp('ddlExamenesCertificacionUsuario').getValue(), 0, false, true, true);

                                                            Ext.getCmp('hfCertificacion').setValue(certificacionRecord.get('idCertificacion'));
                                                            
                                                            Ext.Ajax.request({
                                                                method: 'POST',
                                                                url: 'Sistema/validaIngresoCertificacion',
                                                                params: { idCertificacionRegistro: certificacionRecord.get('idCertificacionRegistro'), idRegistro: certificacionRecord.get('idRegistro') },

                                                                failure: function (response) {
                                                                    Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');

                                                                },
                                                                success: function (response) {
                                                                    var retorno = Ext.decode(response.responseText);

                                                                    switch (retorno) {
                                                                        case 1:
                                                                            //Busco los temas de la certificación seleccionada
                                                                            Ext.data.StoreManager.lookup('Examen.stTemas').load({ params: { idCertificacion: certificacionRecord.get('idCertificacion')} });

                                                                            Ext.MessageBox.show({
                                                                                msg: 'Realizando la petición al servidor, por favor espere...',
                                                                                modal: true,
                                                                                progressText: 'Procesando...',
                                                                                width: 200,
                                                                                wait: true,
                                                                                icon: 'ext-mb-download'
                                                                            });



                                                                            //Espero 0.1 segundos para que el Store stTemas termine de llenarse
                                                                            var task = new Ext.util.DelayedTask(function () {
                                                                                if (Ext.data.StoreManager.lookup('Examen.stTemas').getCount() > 0) {

                                                                                    Ext.getCmp('hfCantidadTemas').setValue(Ext.data.StoreManager.lookup('Examen.stTemas').getCount());

                                                                                    var tiempoTotalEx = 0;
                                                                                    Ext.data.StoreManager.lookup('Examen.stTemas').each(function (rec) {
                                                                                        tiempoTotalEx += rec.data.ctTiempo;
                                                                                    });

                                                                                    Ext.Msg.show({
                                                                                        title: '<span style="font-size: 140%;">Comenzar Examen</span>',
                                                                                        message: '<span style="font-size: 120%;">El tiempo del examen es de ' + tiempoTotalEx + ' minutos. ¿Estás seguro de comenzar ahora?</span>',
                                                                                        closable: false,
                                                                                        modal: true,
                                                                                        buttons: Ext.Msg.YESNO,
                                                                                        buttonText: {
                                                                                            no: 'Esperar',
                                                                                            yes: 'Sí'
                                                                                        },
                                                                                        icon: Ext.Msg.QUESTION,
                                                                                        fn: function (btn) {
                                                                                            if (btn === 'yes') {

                                                                                                
                                                                                                taskMatieneSesion = Ext.TaskManager.start({
                                                                                                run: function () {

                                                                                                Ext.Ajax.request({
                                                                                                method: 'POST',
                                                                                                url: 'Account/mantieneSesion',
                                                                                                failure: function (response) {
                                                                                                Ext.MessageBox.alert('SICER', '<b>Se ha perdido la sesión por inactividad</b>');
                                                                                                },
                                                                                                success: function (response) {
                                                                                                var retorno = Ext.decode(response.responseText);
                                                                                                if (retorno != true) {
                                                                                                Ext.MessageBox.alert('SICER', '<b>Se ha perdido la sesión por inactividad</b>');
                                                                                                }
                                                                                                }
                                                                                                });

                                                                                                // Ext.TaskManager.stop(taskMatieneSesion);

                                                                                                },
                                                                                                interval: 300000 //5 minutos
                                                                                                //interval: 3000 //3 segundos
                                                                                                });
                                                                                                
                                                                                                
                                                                                                
                                                                                                
                                                                                                Ext.getCmp('panInstrucciones').setVisible(false);
                                                                                                Ext.getCmp('panSeleccionarCertificacion').setVisible(false);
                                                                                                if (Ext.getCmp('panEncabezadoPreguntas') == undefined) {
                                                                                                    Ext.getCmp('panPrincipal').add(

                                                                                                                {
                                                                                                                    xtype: 'panel',
                                                                                                                    id: 'panEncabezadoPreguntas',
                                                                                                                    //title: 'Tab Panel',
                                                                                                                    hidden: true,
                                                                                                                    width: '100%',
                                                                                                                    height: 160,
                                                                                                                    //margin: '20 0 0 0',
                                                                                                                    bodyPadding: '10 0 0 0',
                                                                                                                    bodyStyle: { "background-color": "#D4D4D4" },
                                                                                                                    //bodyStyle: { "background-color": "#D4D4D4" },
                                                                                                                    //                                                                                                                    defaults:
                                                                                                                    //                                                                                                                    {
                                                                                                                    //                                                                                                                        frame: true//,
                                                                                                                    //                                                                                                                        //hideLabel: true
                                                                                                                    //                                                                                                                        //,anchor:'0'
                                                                                                                    //                                                                                                                    },
                                                                                                                    layout: {
                                                                                                                        type: 'table',
                                                                                                                        columns: 3,
                                                                                                                        align: 'center'
                                                                                                                    },
                                                                                                                    //height:'600',
                                                                                                                    /*header: {
                                                                                                                    title: {
                                                                                                                    text: 'Tab Panel',
                                                                                                                    flex: 0
                                                                                                                    }
                                                                                                                    },*/
                                                                                                                    items: [{
                                                                                                                        xtype: 'displayfield',
                                                                                                                        id: 'dfNombreCertificacionEx',
                                                                                                                        value: '<span style="font-size: 200%;">' + certificacionRecord.get('cerNombre') + '</span>',
                                                                                                                        grow: true,
                                                                                                                        //margin: '30 0 0 0',
                                                                                                                        fieldLabel: '<span style="font-size: 200%;margin-top:20px;">Certificación</span>',
                                                                                                                        labelAlign: 'right',
                                                                                                                        labelWidth: 200,
                                                                                                                        //colspan: 3,
                                                                                                                        //rowspan:2,
                                                                                                                        width: '100%'
                                                                                                                    }, {
                                                                                                                        xtype: 'displayfield',
                                                                                                                        id: 'dfExamenTiempo',
                                                                                                                        value: '-',
                                                                                                                        fieldLabel: '<span style="font-size: 150%;margin-top:20px;">Tiempo restante</span>',
                                                                                                                        labelAlign: 'right',
                                                                                                                        labelWidth: 150,
                                                                                                                        //margin: '0 80 0 0',
                                                                                                                        margin: '0 80 0 0',
                                                                                                                        colspan: 2,
                                                                                                                        rowspan: 2
                                                                                                                        //width: 500
                                                                                                                    },
                                                                                                                            {
                                                                                                                                xtype: 'displayfield',
                                                                                                                                id: 'dfNombreTemaEx',
                                                                                                                                value: '<span style="font-size: 150%;">-</span>',
                                                                                                                                fieldLabel: '<span style="font-size: 150%;margin-top:20px;">Tema</span>',
                                                                                                                                labelAlign: 'right',
                                                                                                                                margin: '20 0 0 0',
                                                                                                                                labelWidth: 200
                                                                                                                                //  colspan: 3,
                                                                                                                                //width: 500
                                                                                                                            },
                                                                                                                        { xtype: 'label',
                                                                                                                            text: ''
                                                                                                                        },
                                                                                                                        {
                                                                                                                            xtype: 'button',
                                                                                                                            //text: 'Terminar tema',
                                                                                                                            //rowspan:2,
                                                                                                                            // colspan: 2,
                                                                                                                            align: 'right',
                                                                                                                            width: 105,
                                                                                                                            height: 65,
                                                                                                                            iconCls: 'siguienteTema',
                                                                                                                            //  margin: '0 0 0 100',
                                                                                                                            handler: function () {
                                                                                                                                sessionExamen();

                                                                                                                                if (validaRespuestas() == '') {

                                                                                                                                    //Cierra todas las window abiertas
                                                                                                                                    Ext.WindowMgr.hideAll();
                                                                                                                                    //Ext.getCmp('panPrincipal').Ext.getCmp('panPrincipal').getEl().mask();
                                                                                                                                    Ext.getCmp('panPrincipal').disable();
                                                                                                                                    Ext.Msg.show({
                                                                                                                                        title: '<span style="font-size: 140%;margin-top:20px;">Terminar tema</span>',
                                                                                                                                        message: '<span style="font-size: 120%;margin-top:20px;">¿Éstas seguro de terminar el tema?<br>Ya no podrás regresar para cambiar tus respuestas.</span>',
                                                                                                                                        closable: false,
                                                                                                                                        //modal: true,
                                                                                                                                        buttons: Ext.Msg.YESNO,
                                                                                                                                        buttonText: {
                                                                                                                                            no: 'Cancelar',
                                                                                                                                            yes: 'Aceptar'
                                                                                                                                        },
                                                                                                                                        icon: Ext.Msg.WARNING,
                                                                                                                                        fn: function (btn) {
                                                                                                                                            if (btn === 'yes') {

                                                                                                                                                Ext.TaskManager.stop(taskTema);
                                                                                                                                                Ext.getCmp('tabpanFunciones' + Ext.getCmp('hfTema').getValue()).setVisible(false);
                                                                                                                                                Ext.getCmp('panEncabezadoPreguntas').setVisible(false);
                                                                                                                                                almacenaResultado();
                                                                                                                                                cambiarTema();

                                                                                                                                            }
                                                                                                                                            Ext.getCmp('panPrincipal').enable();
                                                                                                                                        }
                                                                                                                                    });
                                                                                                                                    //Ext.getCmp('panPrincipal').Ext.getCmp('panPrincipal').getEl().unmask();

                                                                                                                                }
                                                                                                                                else {
                                                                                                                                    Ext.Msg.alert('SICER', 'Es necesario responder todas las preguntas.');
                                                                                                                                }
                                                                                                                            }
                                                                                                                        },
                                                                                                                        {
                                                                                                                            xtype: 'button',
                                                                                                                            //text: 'Finalizar Examen',
                                                                                                                            //rowspan:2,
                                                                                                                            //colspan: 2,
                                                                                                                            align: 'right',
                                                                                                                            width: 105,
                                                                                                                            height: 65,
                                                                                                                            iconCls: 'finalizaExamen',
                                                                                                                            //  margin: '0 0 0 100',
                                                                                                                            handler: function () {
                                                                                                                                sessionExamen();
                                                                                                                                //Cierra todas las window abiertas
                                                                                                                                Ext.WindowMgr.hideAll();
                                                                                                                                Ext.getCmp('panPrincipal').disable();
                                                                                                                                var win = new Ext.Window({
                                                                                                                                    id: 'win',
                                                                                                                                    title: 'Finalizar Examen'
                                                                                                                                    , width: 500
                                                                                                                                    , resizable: false
                                                                                                                                    , modal: false
                                                                                                                                    , plain: true
                                                                                                                                    , closable: false
                                                                                                                                    , bodyPadding: '10 10 10 10'
                                                                                                                                    , items: [
                                                                                                                                        { xtype: 'panel',
                                                                                                                                            bodyPadding: '10 10 10 10'
											                                                                                                , layout: {
											                                                                                                    type: 'vbox'
                                                                                                                                                , align: 'center'
											                                                                                                },
                                                                                                                                            items: [
                                                                                                                                                {
                                                                                                                                                    xtype: 'displayfield',
                                                                                                                                                    value: 'Usted está saliendo del examen por lo que será redireccionado directamente a la pantalla de resultados. Solo se tomarán en cuenta las preguntas que se hayan contestado hasta ahora y no podrá contestar los temas siguientes. </br></br>Si está de acuerdo escriba la palabra "TERMINAR" en el siguiente recuadro:',
                                                                                                                                                    width: 400

                                                                                                                                                },
                                                    			                                                                                {
                                                    			                                                                                    xtype: 'textfield',
                                                    			                                                                                    id: 'txtFinalizarExamen',
                                                    			                                                                                    width: 400

                                                    			                                                                                }
                                                                                                                                             ]
                                                                                                                                        }
                                                                                                                                    ],
                                                                                                                                    buttons: [
                                                                                                                                            {
                                                                                                                                                text: 'Aceptar', handler: function () {
                                                                                                                                                    if (Ext.getCmp('txtFinalizarExamen').getValue() == 'TERMINAR') {
                                                                                                                                                        Ext.TaskManager.stop(taskTema);
                                                                                                                                                        Ext.getCmp('tabpanFunciones' + Ext.getCmp('hfTema').getValue()).setVisible(false);
                                                                                                                                                        Ext.getCmp('panEncabezadoPreguntas').setVisible(false);
                                                                                                                                                        almacenaResultado();
                                                                                                                                                        //cambiarTema();
                                                                                                                                                        muestraCalificacion();
                                                                                                                                                        Ext.getCmp('panPrincipal').enable();
                                                                                                                                                        win.destroy();
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            },
                                                                                                                                            {
                                                                                                                                                text: 'Cancelar', handler: function () {
                                                                                                                                                    Ext.getCmp('panPrincipal').enable();
                                                                                                                                                    win.destroy();
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                      ]
                                                                                                                                });
                                                                                                                                win.show();
                                                                                                                            }
                                                                                                                        }

                                                                                                                        ]
                                                                                                                }
                                                                                                            );

                                                                                                    /*
                                                                                                    Ext.getCmp('panPrincipal').add(
                                                                                                    { xtype: 'panelExamen',
                                                                                                    id: 'panPrueba'
                                                                                                    });
                                                                                                    */

                                                                                                    Ext.getCmp('panPrincipal').add(
                                                                                                                { xtype: 'panel',
                                                                                                                    id: 'panelTemas',
                                                                                                                    layout: 'fit',
                                                                                                                    border: false,
                                                                                                                    // autoScroll: true,
                                                                                                                    //bodyPadding: 5,
                                                                                                                    //height: '60%',
                                                                                                                    width: '100%'
                                                                                                                }
                                                                                                                );

                                                                                                    var contadorT = 0;
                                                                                                    Ext.data.StoreManager.lookup('Examen.stTemas').each(function (recTema) {
                                                                                                        Ext.getCmp('panelTemas').add(
                                                                                                    {
                                                                                                        xtype: 'tabpanel',
                                                                                                        id: 'tabpanFunciones' + contadorT,
                                                                                                        //title: 'Tab Panel'+this.getItemId(),
                                                                                                        //tabBarHeaderPosition: 1,
                                                                                                        tabRotation: 0,
                                                                                                        tabPosition: 'left',
                                                                                                        hidden: true,
                                                                                                        autoScroll: true,
                                                                                                        //height: '60%',

                                                                                                        tabBar: {
                                                                                                            flex: 1
                                                                                                        }//,
                                                                                                        /*header: {
                                                                                                        title: {
                                                                                                        text: 'Tab Panel',
                                                                                                        flex: 0
                                                                                                        }
                                                                                                        },
                                                                                                        items: []*/
                                                                                                    });
                                                                                                        contadorT = contadorT + 1;
                                                                                                    });

                                                                                                }

                                                                                                creaExamen();


                                                                                            }
                                                                                        }
                                                                                    });

                                                                                } else {
                                                                                    Ext.Msg.alert('ERROR', 'Ocurrio un error al cargar el Examen.');
                                                                                }

                                                                            });

                                                                            task.delay(200);


                                                                            break;
                                                                        case 2:
                                                                            Ext.Msg.alert('SICER', 'Este examen de certificación solo puede ser realizado el día ' + certificacionRecord.get('crFechaExamen') + ' a las ' + certificacionRecord.get('crHora') + ' horas.');
                                                                            break;

                                                                        case 3:
                                                                            Ext.Msg.alert('SICER', 'Este examen de certificación ya fue resuelto.');
                                                                            break;


                                                                        default:
                                                                            Ext.Msg.alert('SICER', 'No es posible validar el ingreso a la certificación.');
                                                                            break;

                                                                    }
                                                                }
                                                            });
                                                        }
                                                    }
                                                }
                                             ]
                                    }


                                ]
                            }
                     }
                     ]
        });
        this.callParent(arguments);
    }
});

function obtenerStores(idTema) {
    
    Ext.data.StoreManager.lookup('Examen.stFunciones').load({ params: { idTema: idTema} });
    Ext.data.StoreManager.lookup('Examen.stPreguntas').load({ params: { idTema: idTema} });

    //Creo una tarea para esperar que terminen de cargar estos stores
      taskTarea2 = Ext.TaskManager.start({
          run: function () {

              //Espero a que cargue el store de preguntas, ya que es el que consulta y obtiene las imagenes en el controlador   
              if (!Ext.data.StoreManager.lookup('Examen.stFunciones').isLoading() && !Ext.data.StoreManager.lookup('Examen.stPreguntas').isLoading()) {
                  Ext.TaskManager.stop(taskTarea2);
                  Ext.data.StoreManager.lookup('Examen.stFunciones').each(function (recFun) {
                      if (Ext.data.StoreManager.lookup('stImagenesPreguntas' + recFun.data.idFuncion) == undefined) {
                          Ext.create('Ext.data.Store', {
                              model: 'app.model.Examen.mdPregunta',
                              storeId: 'stImagenesPreguntas' + recFun.data.idFuncion,
                              proxy: new Ext.data.HttpProxy({ url: 'Sistema/consultaImagenesPreguntas', reader: { type: 'json'} })

                          });
                      }
                      Ext.data.StoreManager.lookup('stImagenesPreguntas' + recFun.data.idFuncion).load({ params: { idFuncion: recFun.data.idFuncion} });
                  });


                  //Espero a que termine de carguar el store de preguntas para cargar respuestas
                  Ext.data.StoreManager.lookup('Examen.stRespuestas').load({ params: { idTema: idTema} });


                  //Creo una tarea para esperar que terminen de cargar estos stores
                  taskTarea = Ext.TaskManager.start({
                      run: function () {

                          //Espero a que cargue el store de respuestas, ya que es el que consulta y obtiene las imagenes en el controlador   
                          if (!Ext.data.StoreManager.lookup('Examen.stRespuestas').isLoading()) {
                              Ext.TaskManager.stop(taskTarea);
                              Ext.data.StoreManager.lookup('Examen.stFunciones').each(function (recFun) {
                                  if (Ext.data.StoreManager.lookup('stImagenesRespuestas' + recFun.data.idFuncion) == undefined) {
                                      Ext.create('Ext.data.Store', {
                                          model: 'app.model.Examen.mdRespuesta',
                                          storeId: 'stImagenesRespuestas' + recFun.data.idFuncion,
                                          proxy: new Ext.data.HttpProxy({ url: 'Sistema/consultaImagenesRespuestas', reader: { type: 'json'} })

                                      });
                                  }
                                  Ext.data.StoreManager.lookup('stImagenesRespuestas' + recFun.data.idFuncion).load({ params: { idFuncion: recFun.data.idFuncion} });
                              });
                          }

                      },
                      interval: 100
                  });

              }
          },
          interval: 100
      });




}
function verificaCargaStores() {
    //Si regresa true quiere decir que si hay alguno cargando
    if (Ext.data.StoreManager.lookup('Examen.stFunciones').isLoading()) return true;
    if (Ext.data.StoreManager.lookup('Examen.stPreguntas').isLoading()) return true;
    if (Ext.data.StoreManager.lookup('Examen.stRespuestas').isLoading()) return true;

    var retorno=false;
    Ext.data.StoreManager.lookup('Examen.stFunciones').each(function (recFun) {
        if (Ext.data.StoreManager.lookup('stImagenesRespuestas' + recFun.data.idFuncion) == undefined || Ext.data.StoreManager.lookup('stImagenesRespuestas' + recFun.data.idFuncion).isLoading()) retorno = true;
    });

    Ext.data.StoreManager.lookup('Examen.stFunciones').each(function (recFun) {
        if (Ext.data.StoreManager.lookup('stImagenesPreguntas' + recFun.data.idFuncion) == undefined || Ext.data.StoreManager.lookup('stImagenesPreguntas' + recFun.data.idFuncion).isLoading()) retorno = true;
    });

    return retorno;
}

function verificaCargaImagenes() {
    //Si regresa true es que hubo un problema al cargar una imagen
    var retorno = false;

    //Para las imagenes de las respuestas
    Ext.data.StoreManager.lookup('Examen.stFunciones').each(function (recFun) {
        Ext.data.StoreManager.lookup('Examen.stRespuestas').each(function (recResp) {
            if (recFun.data.idFuncion == recResp.data.idFuncion && recResp.data.resTipoArchivo == 'I' && 
                Ext.data.StoreManager.lookup('stImagenesRespuestas' + recFun.data.idFuncion).getCount() == 0)
                retorno = true;
            });
        Ext.data.StoreManager.lookup('stImagenesRespuestas' + recFun.data.idFuncion).each(function (recIR) {
            if (recIR.data.resDescripcion != '') retorno = true;
        });
    });

    //Para las imagenes de las preguntas
    Ext.data.StoreManager.lookup('Examen.stFunciones').each(function (recFun) {
        Ext.data.StoreManager.lookup('Examen.stPreguntas').each(function (recPreg) {
            if (recFun.data.idFuncion == recPreg.data.idFuncion && recPreg.data.preTipoArchivo == 'I' &&
                Ext.data.StoreManager.lookup('stImagenesPreguntas' + recFun.data.idFuncion).getCount() == 0)
                retorno = true;
        });
        Ext.data.StoreManager.lookup('stImagenesPreguntas' + recFun.data.idFuncion).each(function (recIP) {
            if (recIP.data.preDescripcion != '') retorno = true;
        });
    });

    return retorno;
}

function creaExamen() {
    //Cierra todas las windows abiertas
    Ext.WindowMgr.hideAll();

    //Selecciono los datos de un Tema
    var recordTema = Ext.data.StoreManager.lookup('Examen.stTemas').getAt(Ext.getCmp('hfTema').getValue());
                                                                
    //Obtengo las funciones, preguntas y respuestas del tema
    obtenerStores(recordTema.data.idTema);

    Ext.getCmp('dfNombreTemaEx').setValue('<span style="font-size: 150%;margin-top:20px;margin-bottom:30px;">' + recordTema.data.temDescripcion + ' (' + (Ext.getCmp('hfTema').getValue() + 1) + '/' + Ext.getCmp('hfCantidadTemas').getValue() + ')</span>');

    var contFuncion = 0;
    
    var horas = Math.floor(parseInt(recordTema.data.ctTiempo)/60);
    var minutos = parseInt(recordTema.data.ctTiempo)%60;
    //var hora = new Date('1/1/2016 00:05:00 AM GMT-0600');
    var minutosTema = recordTema.data.ctTiempo;
    hora = new Date('1/1/2016 '+horas+':'+minutos+':00 AM GMT-0600');
   
    Ext.MessageBox.show({
        msg: 'Obteniendo datos del tema, por favor espere...',
        progressText: 'Procesando...',
        width: 200,
        wait: true,
        modal: true,
        icon: 'ext-mb-download'
    });


    Ext.Ajax.request({
        method: 'POST',
        url: 'Sistema/reiniciaReloj',
        //params: { idCertificacionRegistro: certificacionRecord.get('idCertificacionRegistro'), idRegistro: certificacionRecord.get('idRegistro') },
        failure: function (response) {
            Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación. La sesión ha terminado. Es necesario reiniciar el Examen.</b>');

        },
        success: function (response) {




                        try {
                            var tiempoCarga = 0;
                            taskInicia = Ext.TaskManager.start({
                                run: function () {
                                    tiempoCarga += 1;
                                    if (tiempoCarga > 3)   //Por lo menos dejamos que cargue 1s
                                        if (tiempoCarga < 360)//Si tarda más de 3 minutos termina.
                                        {
                                            if (!verificaCargaStores()) {
                                                Ext.TaskManager.stop(taskInicia);
                                                if (Ext.data.StoreManager.lookup('Examen.stFunciones').getCount() > 0) {
                                                    if (Ext.data.StoreManager.lookup('Examen.stPreguntas').getCount() > 0) {
                                                        if (Ext.data.StoreManager.lookup('Examen.stRespuestas').getCount() > 0) {
                                                            if (!verificaCargaImagenes()) {
                                                                //Por cada funcion creo un tab en el tabpanel del tema
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Ext.data.StoreManager.lookup('Examen.stFunciones').each(function (recFun) {
                                                if (recFun.data.idTema == recordTema.data.idTema) {
                                                    contFuncion = contFuncion + 1;
                                                    Ext.getCmp('tabpanFunciones' + Ext.getCmp('hfTema').getValue()).add(
                                                    {
                                                        title: '<span style="font-size: 200%;line-height: 120%;">Función ' + contFuncion + '</span>',
                                                        id: 'tabFuncion' + recFun.data.idFuncion,
                                                        items: [
                                                            { xtype: 'panel',
                                                                id: 'Funcion' + recFun.data.idFuncion,
                                                                autoScroll: true,
                                                                //bodyPadding: 5,
                                                                height: window.innerHeight - (70 + 40 + 160),
                                                                width: '100%',
                                                                items: [
                                                                        {
                                                                            xtype: 'displayfield',
                                                                            value: '<span style="font-size: 250%;line-height: 120%;">' + recFun.data.funNombre + '</span>',
                                                                            //value: '<span style="font-size: 250%;">' + recFun.data.funNombre + '</span>',
                                                                            grow: true,
                                                                            align: 'center',
                                                                            autoScroll: true,
                                                                            //fieldLabel: 'Función ' + contFuncion,
                                                                            //rowspan:2,
                                                                            width: '90%',
                                                                            margin: '30 0 30 30'
                                                                            //bodypadding:'20 0 20 40'

                                                                        }
                                                                ]
                                                            },
                                                        ]
                                                    });
                                                }
                                            }); //Cierra Funciones


                                                                Ext.getCmp('tabpanFunciones' + Ext.getCmp('hfTema').getValue()).setActiveTab(0);


                                                                var contPreg = 0;
                                                                var funcionActual = 0;
                                                                funcionActual = Ext.data.StoreManager.lookup('Examen.stPreguntas').first().data.idFuncion;
                                                                //Creo los paneles de las preguntas en el tab de la funcion
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        Ext.data.StoreManager.lookup('Examen.stPreguntas').each(function (recPreg) {
                                                if (funcionActual != recPreg.data.idFuncion) {
                                                    //Debido a que desde el controlador se están ordenando por función esta lógica funciona
                                                    contPreg = 0;
                                                    funcionActual = recPreg.data.idFuncion
                                                }
                                                if (recPreg.data.idTema == recordTema.data.idTema) {
                                                    contPreg = contPreg + 1;
                                                    
                                                    recPreg.data.preDescripcion = recPreg.data.preDescripcion.replace(/\n/g, '<br>');
                                                    
                                                    Ext.getCmp('Funcion' + recPreg.data.idFuncion).add(
                                                    {
                                                        xtype: 'panel',
                                                        id: 'panPregunta' + recPreg.data.idPregunta,
                                                        //autoScroll: true,
                                                        width: '100%',
                                                        layout: {
                                                            type: 'vbox', align: 'center'
                                                        },
                                                        items: [
                                                            {
                                                                xtype: 'displayfield',
                                                                value: '<span style=" font-weight: bold;line-height: 120%;font-size: 200%;">' + recPreg.data.preDescripcion + '</span>',
                                                                colspan: 4,
                                                                autoScroll: true,
                                                                style: 'background-color:#C4E7FB;vertical-align: middle;',
                                                                fieldLabel: 'Pregunta ' + contPreg,
                                                                labelAlign: 'right',
                                                                labelWidth: 100,
                                                                margin: '20 0 15 40',
                                                                align: 'center',
                                                                width: '90%'
                                                                //,height: 30
                                                            }
                                                            ]
                                                    });

                                                    //Se ingresan 
                                                    if (recPreg.data.preTipoArchivo == 'I') {
                                                        var pregImgRecord = Ext.data.StoreManager.lookup('stImagenesPreguntas' + recPreg.data.idFuncion).findRecord('idPregunta', recPreg.data.idPregunta, 0, false, true, true);

                                                        Ext.getCmp('panPregunta' + recPreg.data.idPregunta).add(
                                                    {
                                                        xtype: 'image',
                                                        src: pregImgRecord.get('imagen'),
                                                        height: 200,
                                                        width: 200,
                                                        margin: '0 0 20 0'
                                                    });
                                                    }

                                                    if (recPreg.data.preTipoArchivo == 'P') {
                                                        Ext.getCmp('panPregunta' + recPreg.data.idPregunta).add(
                                                        {
                                                            xtype: 'button'
                                                            , text: 'Ver documento',
                                                            height: 50,
                                                            width: 100,
                                                            margin: '0 0 20 0'
                                                            , handler: function () {
                                                                verDocumento(recPreg.data.preNombreArchivo, 'Documento pregunta');
                                                            }
                                                        });
                                                    }



                                                    Ext.getCmp('panPregunta' + recPreg.data.idPregunta).add({
                                                        xtype: 'fieldcontainer',
                                                        id: 'rgRespCertificacion' + recPreg.data.idPregunta,
                                                        //fieldLabel: 'Respuestas de:' + recPreg.data.idPregunta,
                                                        defaultType: 'radiofield',
                                                        margin: '0 0 0 40',
                                                        layout: {
                                                            type: 'table',
                                                            columns: 4
                                                        }
                                                    });

                                                }
                                            }); //Termina Preguntas

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    Ext.data.StoreManager.lookup('Examen.stRespuestas').each(function (recResp) {
                                                //Inserto las respuestas en el fieldcontainer de la pregunta
                                                if (recResp.data.idTema == recordTema.data.idTema) {
                                                    recResp.data.resDescripcion = recResp.data.resDescripcion.replace(/\n/g, '<br>');
                                                    var desc = '<span style="font-size:150%;margin-top:20px;">' + recResp.data.resDescripcion + '</span>';

                                                    if (recResp.data.resTipoArchivo == 'I') {
                                                        var respImgRecord = Ext.data.StoreManager.lookup('stImagenesRespuestas' + recResp.data.idFuncion).findRecord('idRespuesta', recResp.data.idRespuesta, 0, false, true, true);
                                                        desc += '</br><img src="' + respImgRecord.get('imagen') + '" height="200" width="200"/>'
                                                    }

                                                    if (recResp.data.resTipoArchivo == 'P')
                                                        desc += '</br><input type="submit" onclick="verDocumento(\'' + recResp.data.resNombreArchivo + '\',\'' + recResp.data.resDescripcion + '\')" value="Ver Documento">';

                                                    Ext.getCmp('rgRespCertificacion' + recResp.data.idPregunta).add(
                                                {
                                                    boxLabel: desc,
                                                    name: 'respPregunta' + recResp.data.idPregunta,
                                                    inputValue: recResp.data.idRespuesta,
                                                    width: '250px',
                                                    margin: '0 0 30 0'
                                                    //,id        : 'p1radio1'
                                                });

                                                }
                                            });

                                                            //////////////////////////RELOJ////////////////////////////////////////////////
                                                                //Cada 20 segundos envía pregunta al servidor cuanto tiempo queda para responder el examen

                                                                var task2 = new Ext.util.DelayedTask(function () {
                                                                    Ext.getCmp('tabpanFunciones' + Ext.getCmp('hfTema').getValue()).setVisible(true);
                                                                    Ext.getCmp('panEncabezadoPreguntas').setVisible(true);

                                                                    Ext.MessageBox.hide();
                                                                    var auxSegundos = 0;
                                                                    //Comienzo a correr el tiempo para contestar totalmente el tema
                                                                    taskTema = Ext.TaskManager.start({
                                                                        run: function () {

                                                                            Ext.Ajax.request({
                                                                                method: 'POST',
                                                                                url: 'Sistema/consultaTiempoExam',
                                                                                params: {
                                                                                    tiempoExam: minutosTema
                                                                                },
                                                                                failure: function () {

                                                                                },
                                                                                success: function (response) {
                                                                                    var retorno = Ext.decode(response.responseText);

                                                                                    var hh = retorno.Hours;
                                                                                    var mm = retorno.Minutes;

                                                                                    if (hh < 10) { hh = "0" + hh; }
                                                                                    if (mm < 10) { mm = "0" + mm; }

                                                                                    var scrollPosition = Ext.getCmp('panPrincipal').getScrollY();
                                                                                    Ext.getCmp('dfExamenTiempo').setValue('<span style="font-size: 300%;background:black;color: white;">' + hh + ':' + mm + '</span>');
                                                                                    Ext.getCmp('panPrincipal').scrollTo(0, scrollPosition, false);


                                                                                    if (retorno.Hours == 0 && retorno.Minutes <= 0 && retorno.Seconds <= 10) {
                                                                                        Ext.TaskManager.stop(taskTema);
                                                                                        //Ext.MessageBox.hide();
                                                                                        Ext.getCmp('panPrincipal').enable();
                                                                                        Ext.WindowMgr.hideAll();
                                                                                        Ext.getCmp('tabpanFunciones' + Ext.getCmp('hfTema').getValue()).setVisible(false);
                                                                                        Ext.getCmp('panEncabezadoPreguntas').setVisible(false);
                                                                                        Ext.Msg.show({
                                                                                            title: '<span style="font-size: 140%;margin-top:20px;">Tiempo agotado</span>',
                                                                                            message: '<span style="font-size: 120%;margin-top:20px;">Se ha agotado el tiempo para resolver este tema.</span>',
                                                                                            closable: false,
                                                                                            buttons: Ext.Msg.YES,
                                                                                            modal: false,
                                                                                            buttonText: {
                                                                                                yes: 'Aceptar'
                                                                                            },
                                                                                            icon: Ext.Msg.WARNING,
                                                                                            fn: function (btn) {
                                                                                                almacenaResultado();
                                                                                                cambiarTema();
                                                                                            }
                                                                                        });
                                                                                    }
                                                                                }
                                                                            });

                                                                        },
                                                                        interval: 20000//20seg    //1000//1seg
                                                   
                                                                    });
                                                                });

                                                                //////////////////////////TERMINA RELOJ////////////////////////////////////////////////

                                                                task2.delay(1000);  //Espero un segundo a que se generen todos los paneles




                                                            }
                                                            else {
                                                                //Ext.Msg.alert('ERROR', 'Ocurrio un error al cargar las imágenes del examen');
                                                                var mismoTema = Ext.getCmp('hfTema').getValue();
                                                                mismoTema = mismoTema - 1;
                                                                Ext.getCmp('hfTema').setValue(mismoTema);
                                                                Ext.MessageBox.hide();
                                                                Ext.Msg.show({
                                                                    title: '<span style="font-size: 140%;margin-top:20px;">Error</span>',
                                                                    message: '<span style="font-size: 120%;margin-top:20px;">Ocurrió un error al cargar las imágenes del tema. <br> ¿Cargar de nuevo?</span>',
                                                                    closable: false,
                                                                    modal: true,
                                                                    buttons: Ext.Msg.YES,
                                                                    buttonText: {
                                                                        yes: 'Aceptar'
                                                                    },
                                                                    icon: Ext.Msg.WARNING,
                                                                    fn: function (btn) {
                                                                        if (btn === 'yes') {
                                                                            cambiarTema();
                                                                        }
                                                                    }
                                                                });
                                                            }
                                                        }
                                                        else {
                                                            //Ext.Msg.alert('ERROR', 'Ocurrio un error al cargar las respuestas del examen');
                                                            var mismoTema = Ext.getCmp('hfTema').getValue();
                                                            mismoTema = mismoTema - 1;
                                                            Ext.getCmp('hfTema').setValue(mismoTema);
                                                            Ext.MessageBox.hide();
                                                            Ext.Msg.show({
                                                                title: '<span style="font-size: 140%;margin-top:20px;">Error</span>',
                                                                message: '<span style="font-size: 120%;margin-top:20px;">Ocurrió un error al cargar las respuestas del tema. <br> ¿Cargar de nuevo?</span>',
                                                                closable: false,
                                                                modal: true,
                                                                buttons: Ext.Msg.YES,
                                                                buttonText: {
                                                                    yes: 'Aceptar'
                                                                },
                                                                icon: Ext.Msg.WARNING,
                                                                fn: function (btn) {
                                                                    if (btn === 'yes') {
                                                                        cambiarTema();
                                                                    }
                                                                }
                                                            });
                                                        }
                                                    } else {
                                                        // Ext.Msg.alert('ERROR', 'Ocurrio un error al cargar las preguntas del examen');
                                                        var mismoTema = Ext.getCmp('hfTema').getValue();
                                                        mismoTema = mismoTema - 1;
                                                        Ext.getCmp('hfTema').setValue(mismoTema);
                                                        Ext.MessageBox.hide();
                                                        Ext.Msg.show({
                                                            title: '<span style="font-size: 140%;margin-top:20px;">Error</span>',
                                                            message: '<span style="font-size: 120%;margin-top:20px;">Ocurrió un error al cargar las preguntas del tema. <br> ¿Cargar de nuevo?</span>',
                                                            closable: false,
                                                            modal: true,
                                                            buttons: Ext.Msg.YES,
                                                            buttonText: {
                                                                yes: 'Aceptar'
                                                            },
                                                            icon: Ext.Msg.WARNING,
                                                            fn: function (btn) {
                                                                if (btn === 'yes') {
                                                                    cambiarTema();
                                                                }
                                                            }
                                                        });
                                                    }
                                                }
                                                else {
                                                    //Ext.Msg.alert('ERROR', 'Ocurrio un error al cargar las funciones del examen');
                                                    var mismoTema = Ext.getCmp('hfTema').getValue();
                                                    mismoTema = mismoTema - 1;
                                                    Ext.getCmp('hfTema').setValue(mismoTema);
                                                    Ext.MessageBox.hide();
                                                    Ext.Msg.show({
                                                        title: '<span style="font-size: 140%;margin-top:20px;">Error</span>',
                                                        message: '<span style="font-size: 120%;margin-top:20px;">Ocurrió un error al cargar las funciones del tema. <br> ¿Cargar de nuevo?</span>',
                                                        closable: false,
                                                        modal: true,
                                                        buttons: Ext.Msg.YES,
                                                        buttonText: {
                                                            yes: 'Aceptar'
                                                        },
                                                        icon: Ext.Msg.WARNING,
                                                        fn: function (btn) {
                                                            if (btn === 'yes') {
                                                                cambiarTema();
                                                            }
                                                        }
                                                    });
                                                }
                                            }
                                        } else {
                                            //Ext.Msg.alert('ERROR', 'Ocurrio un error al cargar los datos del examen');
                                            Ext.TaskManager.stop(taskInicia);
                                            var mismoTema = Ext.getCmp('hfTema').getValue();
                                            mismoTema = mismoTema - 1;
                                            Ext.getCmp('hfTema').setValue(mismoTema);
                                            Ext.MessageBox.hide();
                                            Ext.Msg.show({
                                                title: '<span style="font-size: 140%;margin-top:20px;">Error</span>',
                                                message: '<span style="font-size: 120%;margin-top:20px;">Ocurrió un error al cargar los datos del tema. <br> ¿Cargar de nuevo?</span>',
                                                closable: false,
                                                modal: true,
                                                buttons: Ext.Msg.YES,
                                                buttonText: {
                                                    yes: 'Aceptar'
                                                },
                                                icon: Ext.Msg.WARNING,
                                                fn: function (btn) {
                                                    if (btn === 'yes') {
                                                        cambiarTema();
                                                    }
                                                }
                                            });
                                        }
                                },
                                interval: 500
                            });
                        }
                        catch (err) {
                            Ext.TaskManager.stop(taskInicia);
                            var mismoTema = Ext.getCmp('hfTema').getValue();
                            mismoTema = mismoTema - 1;
                            Ext.getCmp('hfTema').setValue(mismoTema);
                            Ext.MessageBox.hide();
                            Ext.Msg.show({
                                title: '<span style="font-size: 140%;margin-top:20px;">Error</span>',
                                message: '<span style="font-size: 120%;margin-top:20px;">Ocurrió un error al cargar los datos del tema. <br> ¿Intentar de nuevo?</span>',
                                closable: false,
                                modal: true,
                                buttons: Ext.Msg.YES,
                                buttonText: {
                                    yes: 'Aceptar'
                                },
                                icon: Ext.Msg.WARNING,
                                fn: function (btn) {
                                    if (btn === 'yes') {
                                       cambiarTema();
                                    }
                                }
                            });
                        }

        }//fin success reiniciaReloj
    }); //fin request reiniciaReloj

}

function dateTimeReviver (key, value) {
    var a;
    if (typeof value === 'string') {
        a = /\/Date\((\d*)\)\//.exec(value);
        if (a) {
            return new Date(+a[1]);
        }
    }
    return value;
}

function cambiarTema(){
        limpiaStoresImg();
        //Recursividad - sale de la recursividad cuando se terminen los temas
        var nuevoTema=Ext.getCmp('hfTema').getValue();
        nuevoTema=nuevoTema+1;
        Ext.getCmp('hfTema').setValue(nuevoTema);
        /*
        Ext.MessageBox.show({
            msg: 'Realizando la petición al servidor, por favor espere...',
            progressText: 'Procesando...',
            width: 200,
            wait: true,
            icon: 'Imagenes/download.gif'
        });*/

        if (Ext.data.StoreManager.lookup('Examen.stTemas').getCount() > nuevoTema) {
            creaExamen();
        }
        else { 
            //Termina
            Ext.getCmp('panEncabezadoPreguntas').setVisible(false);

            muestraCalificacion();

        }
}

function almacenaResultado(){
   // var cadena='';
   Ext.data.StoreManager.lookup('Examen.stPreguntas').each(function (recPreg) {
            //cadena+=Ext.getCmp('rgRespCertificacion' + recPreg.data.idPregunta).items.first().getGroupValue()+'_';
            //Ext.getCmp('p1radio1').getGroupValue()
            //cadena+=Ext.getCmp('rgRespCertificacion' + recPreg.data.idPregunta).getValues()+'_';
       Ext.data.StoreManager.lookup('Examen.stEvaluacionRespuesta').add({
                idTema: recPreg.data.idTema,
                idFuncion: recPreg.data.idFuncion,
                idPregunta: recPreg.data.idPregunta,
                idRespuesta:Ext.getCmp('rgRespCertificacion' + recPreg.data.idPregunta).items.first().getGroupValue()
            });
    });

}


function validaRespuestas() {
        //Valida que todas las preguntas sean contestadas
        var respuestas='';
        
        Ext.data.StoreManager.lookup('Examen.stPreguntas').each(function (recPreg) {
            if (Ext.getCmp('rgRespCertificacion' + recPreg.data.idPregunta).items.first().getGroupValue() == null || Ext.getCmp('rgRespCertificacion' + recPreg.data.idPregunta).items.first().getGroupValue() == 0) {
                respuestas += Ext.getCmp('rgRespCertificacion' + recPreg.data.idPregunta).items.first().getGroupValue() + '  ';
            }
        });
        return respuestas;
    }


function verDocumento(nombreArchivo,respuesta){

        var call = 'Generales/handlerPDF.ashx?nombreArchivo=' + nombreArchivo+'';
        var frm = '<object type="text/html" data=' + call + ' style="float:left;width:100%;height:100%; background-image:url(../Imagenes/cargandoObjeto.gif); background-repeat:no-repeat; background-position:center; " />';
        var win = new Ext.Window({
            title: respuesta
            , plain: true
            , height: window.innerHeight / 1.1//'100%'//700
            , width: window.innerWidth/1.2//'100%'//700
            , constrain:true
            , layout: 'fit'
            , autoScroll: true
            , html: frm
            ,buttons: [
                        {
                            text: 'Cerrar Documento', handler: function () {win.destroy();}
                        }
                    ]
                    });
            //win.setHeight();
            //win.setWidth(window.innerWidth/4);

        win.show();
}

function limpiaStoresImg() {
    Ext.data.StoreManager.lookup('Examen.stFunciones').each(function (recFun) {
        if (Ext.data.StoreManager.lookup('stImagenesRespuestas' + recFun.data.idFuncion) != undefined) {
            Ext.data.StoreManager.lookup('stImagenesRespuestas' + recFun.data.idFuncion).removeAll();
        }
    });

}


function muestraCalificacion() {

    Ext.MessageBox.show({
        msg: 'Realizando la petición al servidor, por favor espere...',
        progressText: 'Procesando...',
        modal: true,
        width: 200,
        wait: true,
        icon: 'Imagenes/download.gif'
    });

    var cadEvaluacionRespuesta = '';

    Ext.data.StoreManager.lookup('Examen.stEvaluacionRespuesta').each(function (evResp) {
                    cadEvaluacionRespuesta +=
                                evResp.data.idTema + '┐'+
                                evResp.data.idFuncion + '┐'+
                                evResp.data.idPregunta + '┐'+
                                evResp.data.idRespuesta + '|';
                });

    //var certificacionRecord = Ext.getCmp('ddlExamenesCertificacionUsuario').store.findRecord('idCertificacion', Ext.getCmp('hfCertificacion').getValue(), 0, false, true, true);
    var certificacionRecord = Ext.getCmp('ddlExamenesCertificacionUsuario').store.findRecord('idCertificacionRegistro', Ext.getCmp('ddlExamenesCertificacionUsuario').getValue(), 0, false, true, true);
    var cadIdentificadores = certificacionRecord.get('idCertificacionRegistro') + '┐' + certificacionRecord.get('idRegistro') + '┐' + certificacionRecord.get('idCertificacion') + '┐';

    if (Ext.data.StoreManager.lookup('stCalificacion') == undefined) {
        Ext.create('Ext.data.Store', {
            model: 'app.model.Examen.mdCalificacion',
            storeId: 'stCalificacion',
            proxy: new Ext.data.HttpProxy({ url: '/Sistema/consultarCalificacion', reader: { type: 'json' },
            pageParam: false, //to remove param "page"
            startParam: false, //to remove param "start"
            limitParam: false, //to remove param "limit"
            noCache: false //to remove param "_dc" 
            }
            )
        });
}


////////////////////////////////////////////////////////////////////////////////////////
///////////Validación para evitar registrar dos calificaciones////////////////////////////////////////

Ext.Ajax.request({
    method: 'POST',
    url: 'Sistema/validaIngresoCertificacion',
    params: { idCertificacionRegistro: certificacionRecord.get('idCertificacionRegistro'), idRegistro: certificacionRecord.get('idRegistro') },

    failure: function (response) {
        Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');

    },
    success: function (response) {
        var retorno = Ext.decode(response.responseText);

        switch (retorno) {
            case 3:
                Ext.MessageBox.hide();
                Ext.getCmp('panPrincipal').add(
                            { xtype: 'panel',
                                // title: 'Resultados de certificación',
                                id: 'panCalificacion',
                                bodyPadding: '10 10 10 10'
                                , autoScroll: true
                                , width: '100%'
                                , layout: {
                                    type: 'vbox',
                                    align: 'center'
                                    , pack: 'center'
                                    //tdAttrs: { style: 'padding: 2px;' }
                                },
                                items: [
                                                {
                                                    xtype: 'displayfield',
                                                    value: '<span style="font-size: 250%;font-weight: bold;">FIN DEL EXAMEN</span>',
                                                    margin: '0 0 30 0'
                                                },
                                                {
                                                    xtype: 'displayfield',
                                                    value: '<span style="font-size: 250%;line-height: 120%;font-weight: bold;">Gracias por su participación. <br> La calificación de este examen no se almacenará debido a que ya se tiene un resultado registrado con anterioridad.</span>',
                                                    margin: '0 0 30 0'
                                                    , width: 600
                                                },
                                                {
                                                    xtype: 'button'
                                                    , text: 'Salir',
                                                    //rowspan:2,
                                                    colspan: 2,
                                                    align: 'right',
                                                    margin: '0 0 0 150'
                                                    , handler: function () {
                                                        sessionExamen();
                                                        window.onbeforeunload = null;
                                                        Ext.TaskManager.stop(taskMatieneSesion);
                                                        scope.destroy();
                                                        window.location = '';
                                                    }
                                                }
                                             ]
                            });
                break;

            case 1:
                var idCertificacion = Ext.getCmp('hfCertificacion').getValue();

                Ext.data.StoreManager.lookup('stCalificacion').load({ params: { strEvaluacionRespuesta: cadEvaluacionRespuesta, strIdentificadores: cadIdentificadores} });

                var tiempoCarga = 0;
                var taskCalificacion = Ext.TaskManager.start({
                    run: function () {
                        tiempoCarga += 1;
                        if (tiempoCarga > 3)
                            if (!Ext.data.StoreManager.lookup('stCalificacion').isLoading() && !Ext.data.StoreManager.lookup('Examen.stTemas').isLoading()) {
                                Ext.TaskManager.stop(taskCalificacion);
                                if (Ext.data.StoreManager.lookup('stCalificacion').getCount() > 0) {

                                    Ext.MessageBox.hide();
                                    Ext.getCmp('panPrincipal').add(
                                    { xtype: 'panel',
                                        // title: 'Resultados de certificación',
                                        id: 'panCalificacion',
                                        bodyPadding: '10 10 10 10'
                                        , autoScroll: true
                                        , width: '100%'
                                        , layout: {
                                            type: 'vbox',
                                            align: 'center'
                                            , pack: 'center'
                                            //tdAttrs: { style: 'padding: 2px;' }
                                        },
                                        items: [
                                                        {
                                                            xtype: 'displayfield',
                                                            value: '<span style="font-size: 250%;font-weight: bold;">FIN DEL EXAMEN</span>',
                                                            margin: '0 0 30 0'
                                                        },
                                                        {
                                                            xtype: 'displayfield',
                                                            value: '<span style="font-size: 200%;">Su puntuación es la siguiente:</span>',
                                                            margin: '0 0 30 0'
                                                            , width: 600
                                                        }
                                                     ]
                                    });
                                    var totalCorrectas = 0;
                                    var totalRequeridas = 0;
                                    var totalPreguntasAlea = 0;

                                    Ext.data.StoreManager.lookup('stCalificacion').each(function (calif) {
                                        var tempCorrectas = calif.data.preguntasCorrectas;
                                        // var tempPreguntasAlea = Ext.data.StoreManager.lookup('Examen.stTemas').findRecord('idTema', calif.data.idTema, 0, false, true, true).get('ctAleatorias');
                                        // totalRequeridas += Ext.data.StoreManager.lookup('Examen.stTemas').findRecord('idTema', calif.data.idTema, 0, false, true, true).get('ctCorrectas');
                                        var tempPreguntasAlea = calif.data.preguntasPresentadas;
                                        totalRequeridas += calif.data.preguntasNecesarias;

                                        Ext.getCmp('panCalificacion').add(
                                                 {
                                                     xtype: 'displayfield',
                                                     value: '<span style="font-size: 150%;">' + tempCorrectas + ' correctas de ' + tempPreguntasAlea + '</span>',
                                                     fieldLabel: '<span style="font-size: 150%;font-weight: bold;">TEMA ' + calif.data.numero + '</span>',
                                                     labelAlign: 'right'
                                                 }
                                                );


                                        totalCorrectas += tempCorrectas;
                                        totalPreguntasAlea += tempPreguntasAlea;

                                    });


                                    Ext.getCmp('panCalificacion').add({
                                        xtype: 'displayfield',
                                        value: '<span style="font-size: 150%;">' + totalCorrectas + ' correctas de ' + totalPreguntasAlea + '</span>',
                                        fieldLabel: '<span style="font-size: 150%;font-weight: bold;">TOTAL</span>',
                                        style: 'border-top-style: solid;',
                                        margin: '5 0 30 0',
                                        labelAlign: 'right'
                                    });

                                    Ext.getCmp('panCalificacion').add({
                                        xtype: 'displayfield',
                                        value: '<span style="font-size: 200%;">' + totalRequeridas + '</span>',
                                        fieldLabel: '<span >Puntuación requerida</span>',
                                        margin: '20 0 30 0'
                                                    , labelAlign: 'right',
                                        labelWidth: 300,
                                        labelStyle: 'font-size: 200%;'
                                                    , width: 600
                                    });

                                    Ext.getCmp('panCalificacion').add({
                                        xtype: 'displayfield',
                                        value: '<span style="font-size: 200%;">' + totalCorrectas + '</span>',
                                        fieldLabel: '<span >Su puntuación</span>',
                                        margin: '0 0 30 0'
                                                    , labelAlign: 'right',
                                        labelWidth: 300,
                                        labelStyle: 'font-size: 200%;'
                                                    , width: 600
                                    });

                                    //var resultado = totalCorrectas >= totalRequeridas ? 'APROBADO' : 'NO APROBADO'
                                    var resultado = totalCorrectas >= totalRequeridas ? 'COMPETENTE' : 'TODAVÍA NO ES COMPETENTE';
                                    Ext.getCmp('panCalificacion').add({
                                        xtype: 'displayfield',
                                        value: '<span style="font-size: 300%;line-height: 120%;">' + resultado + '</span>',
                                        fieldLabel: 'Resultado',
                                        margin: '20 0 30 0'
                                                    , labelAlign: 'right'
                                                    , labelStyle: 'font-size: 200%;',
                                        labelWidth: 300
                                                    , width: 600
                                    });

                                    Ext.getCmp('panCalificacion').add({
                                        xtype: 'button'
                                                    , text: 'Salir',
                                        //rowspan:2,
                                        colspan: 2,
                                        align: 'right',
                                        margin: '0 0 0 150'
                                                    , handler: function () {
                                                        sessionExamen();
                                                        window.onbeforeunload = null;
                                                        Ext.TaskManager.stop(taskMatieneSesion);
                                                        scope.destroy();
                                                        window.location = '';
                                                    }
                                    });

                                }
                                else {
                                    Ext.MessageBox.hide();
                                    Ext.Msg.show({
                                        title: '<span style="font-size: 140%;margin-top:20px;">Error</span>',
                                        message: '<span style="font-size: 120%;margin-top:20px;">Ocurrió un error al cargar la calificación <br> ¿Intentarlo de nuevo?</span>',
                                        closable: false,
                                        modal: true,
                                        buttons: Ext.Msg.YES,
                                        buttonText: {
                                            yes: 'Aceptar'
                                        },
                                        icon: Ext.Msg.WARNING,
                                        fn: function (btn) {
                                            if (btn === 'yes') {
                                                muestraCalificacion();
                                            }
                                        }
                                    });

                                }
                            }

                    },
                    interval: 500
                });

                break;

            default:
                Ext.MessageBox.hide();
                Ext.getCmp('panPrincipal').add(
                            { xtype: 'panel',
                                // title: 'Resultados de certificación',
                                id: 'panCalificacion',
                                bodyPadding: '10 10 10 10'
                                , autoScroll: true
                                , width: '100%'
                                , layout: {
                                    type: 'vbox',
                                    align: 'center'
                                    , pack: 'center'
                                    //tdAttrs: { style: 'padding: 2px;' }
                                },
                                items: [
                                                {
                                                    xtype: 'displayfield',
                                                    value: '<span style="font-size: 250%;font-weight: bold;">FIN DEL EXAMEN</span>',
                                                    margin: '0 0 30 0'
                                                },
                                                {
                                                    xtype: 'displayfield',
                                                    value: '<span style="font-size: 250%;line-height: 120%;font-weight: bold;">Ocurrió un error al generar la calificación. Por favor contacte a un administrador.</span>',
                                                    margin: '0 0 30 0'
                                                    , width: 600
                                                },
                                                {
                                                    xtype: 'button'
                                                    , text: 'Salir',
                                                    //rowspan:2,
                                                    colspan: 2,
                                                    align: 'right',
                                                    margin: '0 0 0 150'
                                                    , handler: function () {
                                                        sessionExamen();
                                                        window.onbeforeunload = null;
                                                        Ext.TaskManager.stop(taskMatieneSesion);
                                                        scope.destroy();
                                                        window.location = '';
                                                    }
                                                }
                                             ]
                            });

        }
    }
});

}

