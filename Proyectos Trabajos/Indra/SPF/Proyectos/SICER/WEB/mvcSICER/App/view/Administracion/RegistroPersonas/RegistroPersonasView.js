Ext.define('app.view.Administracion.RegistroPersonas.RegistroPersonasView', {
    extend: 'Ext.form.Panel',
    alias: 'widget.RegistroPersonasView',
//    id: 'pnlRegistro',
    autoScroll: true,
    layout: {
        type: 'vbox',
        align: 'left'
    },
    defaults:
    {
        width: '100%'
    },
    layout: 'fit',
    closable: true,
    bodyPadding: '10 10 10 10',
    title: 'Registro de Personas',
    initComponent: function () {
        var scopePrincipal = this;
        var permiso = (Ext.getCmp('hfPermisoCaptura').getValue() == 'true' ? false : true);

        Ext.apply(this, {


            lbar: [
                 { xtype: 'hiddenfield', id: 'hfIdEmpleado', value: '' },
                 { xtype: 'hiddenfield', id: 'hfIdRegistro', value: '' },
                 { xtype: 'hiddenfield', id: 'hfparticipante', value: 'E' },
                 { xtype: 'hiddenfield', id: 'hfIdCertificacionRegistro', value: '' },
                 { xtype: 'hiddenfield', id: 'hfinserto', value: 'false' },



                { text: 'Nuevo Registro',
                    textAlign: 'left',
                    id: 'btnBuscaDatosIntegrante',
                    icon: 'Imagenes/icon16/limpiar.jpg',

                    handler: function () {

                        Ext.MessageBox.show({
                            msg: 'Realizando la petición al servidor, por favor espere...',
                            progressText: 'Procesando...',
                            modal: true,
                            width: 200,
                            wait: true,
                            icon: 'Imagenes/download.gif'
                        });
                        limpiarPantalla();
                        Ext.MessageBox.hide();
                    }
                },
            /***************************************************************************************************************************************************************************************************************/
                {
                text: 'Buscar Persona',
                textAlign: 'left',
                id: 'btnBuscarPersona',
                icon: 'Extjs/resources/css/images/grid/filters/find.png',

                handler: function () {
                    //AGREGAR PARA LA BUSQUEDA 
                    /**************************************************/

                    var winDI = new Ext.Window({
                        id: 'winDI',
                        title: 'Búsqueda de Participantes'
                                            , width: 700
                                            , resizable: false
                                            , modal: true
                                            , plain: true
                                            , bodyPadding: '10 10 10 10'
                                            , items: [
                                                { xtype: 'panel',
                                                    layout: {
                                                        type: 'table',
                                                        width: '100%',
                                                        columns: 6
                                                    },
                                                    items: [
                                                    { xtype: 'radiogroup', columns: 3, id: 'rblBPIE', colspan: 6, width: '50%', style: { marginLeft: '150px' },
                                                        items: [
	                                                       { xtype: 'radiofield', boxLabel: 'Externo ', id: 'externo', name: 'rblIE', inputValue: 'E', cls: "texto" },
                                                           { xtype: 'radiofield', boxLabel: 'Interno ', id: 'interno', name: 'rblIE', inputValue: 'I', cls: "texto" }
                                                           , { xtype: 'radiofield', boxLabel: 'Todos ', id: 'todos', name: 'rblIE', inputValue: 'T', cls: "texto", checked: true} //inputValue: 'B' (Both-Ambos)
                                                        ]


                                                    },


                                                    { xtype: 'label', cls: 'texto padR', text: '' },
                                                    { xtype: 'label', cls: 'texto padR', text: 'Apellido Paterno' },
                                                    { xtype: 'label', cls: 'texto padR', text: '' },
                                                    { xtype: 'label', cls: 'texto padR', text: 'Apellido Materno' },
                                                    { xtype: 'label', cls: 'texto padR', text: '' },
                                                    { xtype: 'label', cls: 'texto padR', text: 'Nombre(s)' },
                                                    { xtype: 'label', cls: 'texto padR', text: 'Nombre:', margin: "0 0 0 34" },
                                                    { xtype: 'textfield',
                                                        id: 'txbPaternoIE',
                                                        maskRe: /[A-Za-zñÑ\s]/,
                                                        msgTarget: 'side',
                                                        maxLengthText: 'Máximo 30 caracteres el resto sera omitido',
                                                        maxLength: 30,
                                                        allowBlank: true,
                                                        listeners:
                                                    {
                                                        change: function (field, newValue, oldValue)
                                                        { field.setValue(newValue.toUpperCase().substring(0, 30)); } /*,
                                                            render: function () {
                                                                this.getEl().on('paste', function (e, t, eOpts) {
                                                                    e.stopEvent();
                                                                    Ext.MessageBox.alert('EXIN - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
                                                                });
                                                            }*/
                                                    }
                                                    },
                                                    { xtype: 'label', cls: 'texto padR', text: '' },
                                                    { xtype: 'textfield',
                                                        id: 'txbMaternoIE',
                                                        maskRe: /[A-Za-zñÑ\s]/,
                                                        msgTarget: 'side',
                                                        maxLengthText: 'Máximo 30 caracteres el resto sera omitido',
                                                        maxLength: 30,
                                                        allowBlank: true,
                                                        listeners:
                                                        {
                                                            change: function (field, newValue, oldValue)
                                                            { field.setValue(newValue.toUpperCase().substring(0, 30)); } /*,
                                                            render: function () {
                                                                this.getEl().on('paste', function (e, t, eOpts) {
                                                                    e.stopEvent();
                                                                    Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
                                                                });
                                                            }*/
                                                        }
                                                    },
                                                    { xtype: 'label', cls: 'texto padR', text: '' },
                                                    { xtype: 'textfield',
                                                        id: 'txbNombreIE',
                                                        fieldStyle: 'padding-left:5px;',
                                                        maskRe: /[A-Za-zñÑ\s]/,
                                                        msgTarget: 'side',
                                                        maxLengthText: 'Máximo 30 caracteres el resto sera omitido',
                                                        maxLength: 30,
                                                        allowBlank: true,
                                                        listeners:
                                                        {
                                                            change: function (field, newValue, oldValue)
                                                            { field.setValue(newValue.toUpperCase().substring(0, 30)); } /*,
                                                            render: function () {
                                                                this.getEl().on('paste', function (e, t, eOpts) {
                                                                    e.stopEvent();
                                                                    Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
                                                                });
                                                            }*/
                                                        }
                                                    }

                                                        , { xtype: 'label', cls: 'texto padR', text: 'No. Empleado: ', margin: "0 0 0 5" },
                                                    { xtype: 'textfield',
                                                        id: 'txbNumEmpleadoIE',
                                                        maskRe: /[0-9]/, ///[A-Za-zñÑ\s]/,
                                                        msgTarget: 'side',
                                                        maxLengthText: 'Máximo 30 caracteres el resto sera omitido',
                                                        maxLength: 30,
                                                        allowBlank: true,
                                                        listeners:
                                                        {
                                                            change: function (field, newValue, oldValue)
                                                            { field.setValue(newValue.toUpperCase().substring(0, 30)); } /*,
                                                            render: function () {
                                                                this.getEl().on('paste', function (e, t, eOpts) {
                                                                    e.stopEvent();
                                                                    Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
                                                                });
                                                            }*/
                                                        }
                                                    },

                                                        { xtype: 'label', cls: 'texto padR', text: 'RFC: ', margin: "0 0 0 5" },
                                                    { xtype: 'textfield',
                                                        id: 'txbRFCIE',
                                                        maskRe: /[A-Za-zñÑ0-9]/,
                                                        msgTarget: 'side',
                                                        maxLengthText: 'Máximo 30 caracteres el resto sera omitido',
                                                        maxLength: 30,
                                                        allowBlank: true,
                                                        listeners:
                                                        {
                                                            change: function (field, newValue, oldValue)
                                                            { field.setValue(newValue.toUpperCase().substring(0, 30)); } /*,
                                                            render: function () {
                                                                this.getEl().on('paste', function (e, t, eOpts) {
                                                                    e.stopEvent();
                                                                    Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
                                                                });
                                                            }*/
                                                        }
                                                    },

                                                       { xtype: 'label', cls: 'texto padR', text: 'CURP: ', margin: "0 0 0 5" },
                                                    { xtype: 'textfield',
                                                        id: 'txbCURPIE',
                                                        maskRe: /[A-Za-zñÑ0-9]/,
                                                        msgTarget: 'side',
                                                        maxLengthText: 'Máximo 30 caracteres el resto sera omitido',
                                                        maxLength: 30,
                                                        allowBlank: true,
                                                        listeners:
                                                        {
                                                            change: function (field, newValue, oldValue)
                                                            { field.setValue(newValue.toUpperCase().substring(0, 30)); } /*,
                                                            render: function () {
                                                                this.getEl().on('paste', function (e, t, eOpts) {
                                                                    e.stopEvent();
                                                                    Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
                                                                });
                                                            }*/
                                                        }
                                                    }

                                               ],
                                                    buttons: [

                                                    {
                                                        text: 'Buscar', id: 'tabBusqueda',
                                                        //  xtype: 'button', margin: "0 0 0 50",
                                                        handler: function () {
                                                            session();

                                                            if (Ext.getCmp('txbPaternoIE').getValue().length < 3
                                                                    && Ext.getCmp('txbMaternoIE').getValue().length < 3
                                                                    && Ext.getCmp('txbNombreIE').getValue().length < 3
                                                                    && Ext.getCmp('txbCURPIE').getValue().length < 3
                                                                    && Ext.getCmp('txbRFCIE').getValue().length < 3
                                                                    && Ext.getCmp('txbNumEmpleadoIE').getValue().length < 3

                                                                    ) {
                                                                Ext.MessageBox.alert('SICER', 'Es necesario ingresar valores de búsqueda mayores a 3 caracteres');
                                                                return;
                                                            }


                                                            Ext.getCmp('pnlBusqueda').store.load(
                                                            {
                                                                params: {
                                                                    empPaterno: Ext.getCmp('txbPaternoIE').getValue(),
                                                                    empMaterno: Ext.getCmp('txbMaternoIE').getValue(),
                                                                    empNombre: Ext.getCmp('txbNombreIE').getValue(),
                                                                    empCURP: Ext.getCmp('txbCURPIE').getValue(),
                                                                    empRFC: Ext.getCmp('txbRFCIE').getValue(),
                                                                    empNumero: Ext.getCmp('txbNumEmpleadoIE').getValue(),
                                                                    participante: Ext.getCmp('rblBPIE').getValue().rblIE
                                                                    //,start: 0
                                                                    //,limit: 10
                                                                    , buscar: 1
                                                                }
                                                            });
                                                        }
                                                    }
                                               , {
                                                   text: 'Nueva Búsqueda',
                                                   //xtype: 'button', margin: "0 0 0 50",
                                                   handler: function () {
                                                       session();
                                                       Ext.getCmp('txbPaternoIE').setValue('');
                                                       Ext.getCmp('txbMaternoIE').setValue('');
                                                       Ext.getCmp('txbNombreIE').setValue('');
                                                       Ext.getCmp('txbCURPIE').setValue('');
                                                       Ext.getCmp('txbRFCIE').setValue('');
                                                       Ext.getCmp('txbNumEmpleadoIE').setValue('');
                                                       Ext.getCmp("externo").setValue(false);
                                                       Ext.getCmp("interno").setValue(false);
                                                       Ext.getCmp("todos").setValue(true);
                                                       Ext.getCmp('pnlBusqueda').store.removeAll();
                                                   }
                                               }
                                                    ]

                                                }, { xtype: 'gridpanBusquedaParticipantes', height: 300 }
                                                ]
                                              , buttons: [
                                                 {
                                                     text: 'Aceptar',
                                                     //  xtype: 'button', margin: "0 0 0 50",
                                                     handler: function () {
                                                         session();

                                                         limpiarPantalla();

                                                         if (Ext.getCmp('pnlBusqueda').getSelectionModel().hasSelection()) {

                                                             Ext.MessageBox.show({
                                                                 msg: 'Obteniendo Información de Persona',
                                                                 progressText: 'Procesando...',
                                                                 modal: true,
                                                                 width: 200,
                                                                 wait: true,
                                                                 icon: 'Imagenes/download.gif'
                                                             });


                                                             var idRegistro = Ext.getCmp('pnlBusqueda').getSelectionModel().getSelection()[0].data.idRegistro;
                                                             var idEmpleado = Ext.getCmp('pnlBusqueda').getSelectionModel().getSelection()[0].data.idEmpleado;

                                                             Ext.Ajax.request({
                                                                 method: 'POST',
                                                                 url: 'Home/Consulta',
                                                                 failure: function () {
                                                                     Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');

                                                                 },
                                                                 params:
                                                                    {
                                                                        idRegistro: idRegistro, idEmpleado: idEmpleado
                                                                    },
                                                                 success: function (response) {

                                                                     retorno = Ext.decode(response.responseText);


                                                                     if (retorno.participante == 'E') {

                                                                         Ext.getCmp('hfIdRegistro').setValue(idRegistro);
                                                                         Ext.getCmp('txbPaterno').setValue(retorno.Paterno);
                                                                         Ext.getCmp('txbMaterno').setValue(retorno.Materno);
                                                                         Ext.getCmp('txbNombre').setValue(retorno.Nombre);
                                                                         Ext.getCmp('txbNumEmpleado').setValue(retorno.numeroEmpleado);
                                                                         Ext.getCmp('txbCURP').setValue(retorno.CURP).setDisabled(true);
                                                                         Ext.getCmp('txbRFC').setValue(retorno.RFC).setDisabled(true);
                                                                         Ext.getCmp('txbCUIP').setValue(retorno.CUIP);
                                                                         Ext.getCmp('txbLOC').setValue(retorno.LOC);
                                                                         Ext.getCmp('txbCargo').setValue(retorno.Cargo);
                                                                         Ext.getCmp('txbGrado').setValue(retorno.Grado);
                                                                         Ext.getCmp('dfFechaNacimiento').setValue(new Date(retorno.fechaNacimiento));
                                                                         Ext.getCmp('txbEscolaridad').setValue(retorno.Escolaridad);
                                                                         Ext.getCmp('txbTelCasa').setValue(retorno.Telefono);

                                                                         Ext.getCmp('txbEmailPersonal').setValue(retorno.emailPersonal);
                                                                         Ext.getCmp('txbTelCelular').setValue(retorno.Celular);
                                                                         Ext.getCmp('txbEmailLaboral').setValue(retorno.emailLaboral);
                                                                         Ext.getCmp('txbTelLaboral').setValue(retorno.telTrabajo);
                                                                         Ext.getCmp('txbDomCalle').setValue(retorno.Calle);
                                                                         Ext.getCmp('txbNumExterior').setValue(retorno.numExterior);
                                                                         Ext.getCmp('txbNumInterior').setValue(retorno.numInterior);
                                                                         Ext.getCmp('txbColonia').setValue(retorno.Colonia);
                                                                         Ext.getCmp('txbCodPostal').setValue(retorno.CP);
                                                                         Ext.getCmp('ddlDelegacionMun').setValue(retorno.munDescripcion);
                                                                         Ext.getCmp('ddlEstado').setValue(retorno.nombreEstado);
                                                                         Ext.getCmp('ddlEstado').setValue(retorno.idEstado);
                                                                         Ext.getCmp('ddlDelegacionMun').setValue(retorno.idMunicipio);
                                                                         Ext.getCmp('dfFechaIngreso').setValue(retorno.fechaRegistro);
                                                                         Ext.getCmp('hfparticipante').setValue(retorno.participante);
                                                                         Ext.getCmp('hfNombreFotografia').setValue(retorno.regFoto); //imgFoto
                                                                         //   Ext.getCmp('fotografia').setValue(retorno.regFoto);
                                                                         // //Ext.getCmp('imgFoto').setSrc('data:image/jpeg;base64,' + data.strImagen);

                                                                     }

                                                                     else if (retorno.participante == 'I') {

                                                                         Ext.getCmp('hfIdRegistro').setValue(idRegistro);
                                                                         Ext.getCmp('hfIdEmpleado').setValue(idEmpleado);
                                                                         Ext.getCmp('txbPaterno').setValue(retorno.Paterno).setDisabled(true);
                                                                         Ext.getCmp('txbMaterno').setValue(retorno.Materno).setDisabled(true);
                                                                         Ext.getCmp('txbNombre').setValue(retorno.Nombre).setDisabled(true);
                                                                         Ext.getCmp('txbNumEmpleado').setValue(retorno.numeroEmpleado).setDisabled(true);
                                                                         Ext.getCmp('txbCURP').setValue(retorno.CURP).setDisabled(true);
                                                                         Ext.getCmp('txbRFC').setValue(retorno.RFC).setDisabled(true);
                                                                         Ext.getCmp('txbCUIP').setValue(retorno.CUIP).setDisabled(true);
                                                                         Ext.getCmp('txbLOC').setValue(retorno.LOC).setDisabled(true);
                                                                         Ext.getCmp('txbCargo').setValue(retorno.Cargo).setDisabled(true);
                                                                         Ext.getCmp('txbGrado').setValue(retorno.Grado).setDisabled(true);
                                                                         Ext.getCmp('dfFechaNacimiento').setValue(retorno.fechaNacimiento==''? '': new Date(retorno.fechaNacimiento)).setDisabled(true);
                                                                         Ext.getCmp('txbEscolaridad').setValue(retorno.Escolaridad).setDisabled(true);
                                                                         Ext.getCmp('txbTelCasa').setValue(retorno.Telefono);
                                                                         Ext.getCmp('txbEmailPersonal').setValue(retorno.emailPersonal);
                                                                         Ext.getCmp('txbTelCelular').setValue(retorno.Celular);
                                                                         Ext.getCmp('txbEmailLaboral').setValue(retorno.emailLaboral);
                                                                         Ext.getCmp('txbTelLaboral').setValue(retorno.telTrabajo);
                                                                         Ext.getCmp('txbDomCalle').setValue(retorno.Calle).setDisabled(true);
                                                                         Ext.getCmp('txbNumExterior').setValue(retorno.numExterior).setDisabled(true);
                                                                         Ext.getCmp('txbNumInterior').setValue(retorno.numInterior).setDisabled(true);
                                                                         Ext.getCmp('txbColonia').setValue(retorno.Colonia).setDisabled(true);
                                                                         Ext.getCmp('txbCodPostal').setValue(retorno.CP).setDisabled(true);
                                                                         Ext.getCmp('ddlDelegacionMun').setValue(retorno.munDescripcion).setDisabled(true);//Trae la descripcion del municipio y no el id
                                                                         Ext.getCmp('ddlEstado').setValue(retorno.nombreEstado).setDisabled(true);
                                                                         //Ext.getCmp('ddlEstado').setValue(retorno.idEstado).setDisabled(true);
                                                                         Ext.getCmp('ddlDelegacionMun').setValue(retorno.munDescripcion).setDisabled(true);
                                                                         Ext.getCmp('dfFechaIngreso').setValue(retorno.fechaRegistro);
                                                                         Ext.getCmp('hfparticipante').setValue(retorno.participante);
                                                                         Ext.getCmp('hfNombreFotografia').setValue(retorno.regFoto);
                                                                     }

                                                                     if (Ext.getCmp('hfNombreFotografia').getValue() != '') {
                                                                         Ext.Ajax.request({
                                                                             method: 'POST',
                                                                             url: 'Home/obtieneImagenPersona',
                                                                             params: { imagen: Ext.getCmp('hfNombreFotografia').getValue() },
                                                                             failure: function () {
                                                                                 Ext.getCmp('imgFoto').setSrc('Imagenes/imgNoDisponible.png');
                                                                                 // Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
                                                                                 Ext.MessageBox.hide();
                                                                             },
                                                                             success: function (response) {

                                                                                 var retImagen = Ext.decode(response.responseText);

                                                                                 if (retImagen.response == '') {
                                                                                     Ext.getCmp('hfFotografia').setValue(retImagen.strImagen);
                                                                                     Ext.getCmp('imgFoto').setSrc('data:image/jpeg;base64,' + retImagen.strImagen);
                                                                                 }
                                                                                 else {
                                                                                     Ext.getCmp('hfFotografia').setValue('');
                                                                                     Ext.getCmp('imgFoto').setSrc('Imagenes/imgNoDisponible.png');
                                                                                 }


                                                                                 Ext.MessageBox.hide();
                                                                             }
                                                                         });
                                                                     } else {
                                                                         Ext.MessageBox.hide();
                                                                     }

                                                                 }

                                                             });



                                                             Ext.getCmp('gridCertificaciones').store.load({ params: { idRegistro: idRegistro} });


                                                             //El boton Generar contraseña se habilita solo si el registro se encuentra en la base de datos 
                                                             if (idRegistro != 0) {
                                                                     Ext.getCmp('btnGeneraContraseña').setDisabled(false);
                                                             }
                                                             


                                                             winDI.destroy();


                                                         } else {
                                                             Ext.MessageBox.alert('SICER', 'Es necesario seleccionar una persona');
                                                         }



                                                     }
                                                 }
                                               , {
                                                   text: 'Cancelar',
                                                   //xtype: 'button', margin: "0 0 0 50",
                                                   handler: function () {
                                                       session();
                                                       winDI.destroy();
                                                   }

                                               }
                                               ]
                    });
                    winDI.show();

                } ///fin handler  de busqueda sicer

            }, //fin text

            /***************************************************************************************************************************************************************************************************************/

                 {text: 'Generar Nueva Contraseña',
                 textAlign: 'left',
                 id: 'btnGeneraContraseña',
                 icon: 'Imagenes/icon16/salir.png',
                 disabled: true,
                 //                    add_click: function(handler){
                 //                        this.get_event().addHandler('click', handler);
                 //                        Ext.MessageBox.alert('SICER', 'AQUI');
                 //}
                 handler: function () {
                     session();
                     generarContrasena();

                 }

             }, //fin text

                {text: 'Guardar',
                textAlign: 'left',
                id: 'btnGuardarPersona',
                //iconCls: 'guardar',
                icon: 'Extjs/resources/images/icons/fam/save.gif',

                handler: function () {
                    session();


                    Ext.MessageBox.show({
                        msg: 'Almacenando registro',
                        progressText: 'Procesando...',
                        modal: true,
                        width: 200,
                        wait: true,
                        icon: 'Imagenes/download.gif'
                    });


                    validarCURP();




                    //guardaImagen();
                    //validarDatosPersona();

                }
            }
        ],
            collapsible: true,

            layout: {
                type: 'vbox',
                align: 'center'
            },
            items: [

             { xtype: 'panelDatosBasicos'/*, height: 300*/ },
             { xtype: 'panelDatosComplementarios'/*, height: 300 */ },
             { xtype: 'panelCertificaciones'/*, height: 300*/ }

             ]
        });
        this.callParent(arguments);
    }
});


function validarCURP() {
    if (Ext.getCmp('txbCURP').getValue().trim() == "" || Ext.getCmp('txbCURP').getValue().trim() == null) {
        Ext.MessageBox.alert('SICER', '<b>CURP</b></br> Campo obligatorio');
        return false;
    }
    else {

        if (Ext.getCmp('hfIdRegistro').getValue() == '') { //Si es cadena vacia es que el integrante no se ha buscado
            Ext.Ajax.request({
                method: 'POST',
                url: 'Home/validaCURP',
                params: { CURP: Ext.getCmp('txbCURP').getValue() },

                failure: function () {
                    Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
                },
                success: function (response) {
                    var retorno = Ext.decode(response.responseText);

                    if (retorno.alerta == '') {
                        if (retorno.respuesta == '1') {
                            //guardaImagen();
                            validarDatosPersona();
                        } else {
                            Ext.MessageBox.alert('SICER', '<b>Error de Validación</b></br>El CURP ingresado ya se encuentra registrado para el participante </br> <b>' + retorno.nombreParticipante + '</b></br>La información no será almacenada.');
                        }
                    } else {
                        Ext.MessageBox.alert('SICER', '<b>Ocurrió un error al validar CURP</b></br>' + response.alerta);
                    }

                }
            });
        } else {
            // guardaImagen();
        validarDatosPersona();
        }
    }
}

function validarDatosPersona() {


    if (Ext.getCmp('hfparticipante').getValue() == 'E' && (Ext.getCmp('txbPaterno').getValue().trim() == "" || Ext.getCmp('txbPaterno').getValue().trim() == null)) {
        Ext.MessageBox.alert('SICER', '<b>APELLIDO PATERNO</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfparticipante').getValue() == 'E' && (Ext.getCmp('txbMaterno').getValue().trim() == "" || Ext.getCmp('txbMaterno').getValue().trim() == null)) {
        Ext.MessageBox.alert('SICER', '<b>APELLIDO MATERNO</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfparticipante').getValue() == 'E' && (Ext.getCmp('txbNombre').getValue().trim() == "" || Ext.getCmp('txbNombre').getValue().trim() == null)) {
        Ext.MessageBox.alert('SICER', '<b>NOMBRE</b></br> Campo obligatorio');
        return false;
    }
    
    if (Ext.getCmp('hfparticipante').getValue()=='E'  && (Ext.getCmp('txbRFC').getValue().trim() == "" || Ext.getCmp('txbRFC').getValue().trim() == null)) {
        Ext.MessageBox.alert('SICER', '<b>RFC</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfparticipante').getValue()=='E'  && (Ext.getCmp('txbGrado').getValue().trim() == "" || Ext.getCmp('txbGrado').getValue().trim() == null)) {
        Ext.MessageBox.alert('SICER', '<b>GRADO</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfparticipante').getValue()=='E'  && (Ext.getCmp('txbCargo').getValue().trim() == "" || Ext.getCmp('txbCargo').getValue().trim() == null)) {
        Ext.MessageBox.alert('SICER', '<b>CARGO</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfparticipante').getValue() == 'E' && (Ext.getCmp('dfFechaNacimiento').getRawValue() == "" || Ext.getCmp('dfFechaNacimiento').getRawValue() == null)) {
        Ext.MessageBox.alert('SICER', '<b>Fecha Nacimiento</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfparticipante').getValue()=='E'  && (Ext.getCmp('txbEscolaridad').getValue().trim() == "" || Ext.getCmp('txbEscolaridad').getValue().trim() == null)) {
        Ext.MessageBox.alert('SICER', '<b>ESCOLARIDAD</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('txbTelCasa').getValue().trim() == "" || Ext.getCmp('txbTelCasa').getValue().trim() == null) {
        Ext.MessageBox.alert('SICER', '<b>TELEFONO DE CASA</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('txbEmailPersonal').getValue().trim() == "" || Ext.getCmp('txbEmailPersonal').getValue().trim() == null) {
        Ext.MessageBox.alert('SICER', '<b>CORREO PERSONAL</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('txbTelCelular').getValue().trim() == "" || Ext.getCmp('txbTelCelular').getValue().trim() == null) {
        Ext.MessageBox.alert('SICER', '<b>TELEFONO CELULAR</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfparticipante').getValue()=='E'  && (Ext.getCmp('txbDomCalle').getValue().trim() == "" || Ext.getCmp('txbDomCalle').getValue().trim() == null)) {
        Ext.MessageBox.alert('SICER', '<b>CALLE</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfparticipante').getValue()=='E'  && (Ext.getCmp('txbColonia').getValue().trim() == "" || Ext.getCmp('txbColonia').getValue().trim() == null)) {
        Ext.MessageBox.alert('SICER', '<b>COLONIA</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfparticipante').getValue()=='E'  && (Ext.getCmp('txbCodPostal').getValue().trim() == "" || Ext.getCmp('txbCodPostal').getValue().trim() == null)) {
        Ext.MessageBox.alert('SICER', '<b>CODIGO POSTAL</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfparticipante').getValue()=='E'  && (Ext.getCmp('ddlEstado').getValue() == 0 || Ext.getCmp('ddlEstado').getValue() == null)) {
        Ext.MessageBox.alert('SICER', '<b>ESTADO</b></br> Campo obligatorio');
        return false;
    }


    if (Ext.getCmp('hfparticipante').getValue()=='E'  && (Ext.getCmp('ddlDelegacionMun').getValue() == 0 || Ext.getCmp('ddlDelegacionMun').getValue() == null)) {
        Ext.MessageBox.alert('SICER', '<b>DELEGACIÓN / MUNICIPIO</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('dfFechaIngreso').getRawValue() == "" || Ext.getCmp('dfFechaIngreso').getRawValue() == null) {
        Ext.MessageBox.alert('SICER', '<b>FECHA DE REGISTRO</b></br> Campo obligatorio');
        return false;
    }

    if (Ext.getCmp('hfFotografia').getValue() == "" || Ext.getCmp('hfFotografia').getValue() == null) {
        Ext.MessageBox.alert('SICER', '<b>Fotografía</b></br> Campo obligatorio');
        return false;
    }

    guardaImagen();
}


function guardaImagen() {

    if (Ext.getCmp('hfFotografia').getValue() != '') {
        Ext.Ajax.request({
            method: 'POST',
            url: 'Home/almacenaImagenPersona',
            params: { imagen: Ext.getCmp('hfFotografia').getValue(),
                nombre: Ext.getCmp('txbCURP').getValue().trim()
            },

            failure: function () {
                Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
                retorno = false;
            },
            success: function (response) {
                var retorno = Ext.decode(response.responseText);

                if (retorno.response != '') {
                    Ext.MessageBox.alert('SICER', 'Ocurrió un error al guardar la fotografía </br>' + retorno.response);
                    retorno = false;
                } else {

                    Ext.getCmp('hfNombreFotografia').setValue(retorno.strImagen);
                    //validarDatosPersona();
                    guardarRegistroPersona();
                }

            }
        });
    }
    else {
        //validarDatosPersona();
        guardarRegistroPersona();
    }
}


function guardarRegistroPersona() {

    var stRegistroPersonas;
    var participante;

    var stRP = Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stRegistroPersona');
    if (stRP.getCount() > 0) {
        stRP.each(function (subrec) {
            subrec.data.idRegistro = Ext.getCmp('hfIdRegistro').getValue();
            subrec.data.idEmpleado = Ext.getCmp('hfIdEmpleado').getValue();
            subrec.data.paterno = Ext.getCmp('txbPaterno').getValue();
            subrec.data.materno = Ext.getCmp('txbMaterno').getRawValue();
            subrec.data.nombre = Ext.getCmp('txbNombre').getValue();
            subrec.data.numempleado = Ext.getCmp('txbNumEmpleado').getValue();
            subrec.data.curp = Ext.getCmp('txbCURP').getValue();
            subrec.data.rfc = Ext.getCmp('txbRFC').getValue();
            subrec.data.cuip = Ext.getCmp('txbCUIP').getValue();
            subrec.data.loc = Ext.getCmp('txbLOC').getValue();
            subrec.data.gradoDesc = Ext.getCmp('txbGrado').getValue();
            subrec.data.cargoDesc = Ext.getCmp('txbCargo').getValue();
            subrec.data.dfFechaNacimiento = Ext.getCmp('dfFechaNacimiento').getRawValue();
            subrec.data.escolaridad = Ext.getCmp('txbEscolaridad').getValue();
            subrec.data.telcasa = Ext.getCmp('txbTelCasa').getValue();
            subrec.data.emailpersonal = Ext.getCmp('txbEmailPersonal').getValue();
            subrec.data.telcelular = Ext.getCmp('txbTelCelular').getValue();
            subrec.data.emaillaboral = Ext.getCmp('txbEmailLaboral').getValue();
            subrec.data.tellaboral = Ext.getCmp('txbTelLaboral').getValue();

            subrec.data.calle = Ext.getCmp('txbDomCalle').getValue();
            subrec.data.numext = Ext.getCmp('txbNumExterior').getValue();
            subrec.data.numint = Ext.getCmp('txbNumInterior').getValue();
            subrec.data.colonia = Ext.getCmp('txbColonia').getValue();
            subrec.data.codpostal = Ext.getCmp('txbCodPostal').getValue();
            subrec.data.idestado = Ext.getCmp('ddlEstado').getValue();
            subrec.data.idMunicipio = Ext.getCmp('ddlDelegacionMun').getValue();
            subrec.data.dfFechaIngreso = Ext.getCmp('dfFechaIngreso').rawValue;
            //          subrec.data.idnivelSeg= Ext.getCmp('ddlNivelSeguridad').getValue();
            //          subrec.data.iddependendiaexterna= Ext.getCmp('ddlDependenciaExterna').getValue();
            //          subrec.data.idinstitucionexterna = Ext.getCmp('ddlInstitucionExterna').getValue();
            subrec.data.participante = Ext.getCmp('hfparticipante').getValue();
            subrec.data.foto = Ext.getCmp('hfNombreFotografia').getValue();

        });

    } else {


        stRP.add({
            idRegistro: Ext.getCmp('hfIdRegistro').getValue()
                , idEmpleado: Ext.getCmp('hfIdEmpleado').getValue()
                , paterno: Ext.getCmp('txbPaterno').getValue()
                , materno: Ext.getCmp('txbMaterno').getValue()
                , nombre: Ext.getCmp('txbNombre').getValue()
                , numempleado: Ext.getCmp('txbNumEmpleado').getValue()
                , curp: Ext.getCmp('txbCURP').getValue()
                , rfc: Ext.getCmp('txbRFC').getValue()
                , cuip: Ext.getCmp('txbCUIP').getValue()
                , loc: Ext.getCmp('txbLOC').getValue()
                , gradoDesc: Ext.getCmp('txbGrado').getValue()
                , cargoDesc: Ext.getCmp('txbCargo').getValue()
                , dfFechaNacimiento: Ext.getCmp('dfFechaNacimiento').getRawValue()
                , escolaridad: Ext.getCmp('txbEscolaridad').getValue()
                , telcasa: Ext.getCmp('txbTelCasa').getValue()
                , emailpersonal: Ext.getCmp('txbEmailPersonal').getValue()
                , telcelular: Ext.getCmp('txbTelCelular').getValue()
                , emaillaboral: Ext.getCmp('txbEmailLaboral').getValue()
                , tellaboral: Ext.getCmp('txbTelLaboral').getValue()
                , calle: Ext.getCmp('txbDomCalle').getValue()
                , numext: Ext.getCmp('txbNumExterior').getValue()
                , numint: Ext.getCmp('txbNumInterior').getValue()
                , colonia: Ext.getCmp('txbColonia').getValue()
                , codpostal: Ext.getCmp('txbCodPostal').getValue()
                , idestado: Ext.getCmp('ddlEstado').getValue()
                , idMunicipio: Ext.getCmp('ddlDelegacionMun').getValue()// idDelMunicipio: Ext.getCmp('ddlDelegacionMun').getValue()
                , dfFechaIngreso: Ext.getCmp('dfFechaIngreso').rawValue
                , participante: Ext.getCmp('hfparticipante').getValue()
                , foto: Ext.getCmp('hfNombreFotografia').getValue()
        });
    }




    Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stRegistroPersona').each(function (subrec) {
        stRegistroPersonas =

                              subrec.data.idEmpleado + '┐'//0
                            + subrec.data.paterno + '┐'//1
                            + subrec.data.materno + '┐'//2
                            + subrec.data.nombre + '┐'//3
                            + subrec.data.numempleado + '┐'//4
                            + subrec.data.curp + '┐'//5
                            + subrec.data.rfc + '┐'//6
                            + subrec.data.cuip + '┐'//7
                            + subrec.data.loc + '┐'//8
                            + subrec.data.gradoDesc + '┐'//9
                            + subrec.data.cargoDesc + '┐'//10
                            + subrec.data.dfFechaNacimiento + '┐'//11
                            + subrec.data.escolaridad + '┐'//12
                            + subrec.data.telcasa + '┐'//13
                            + subrec.data.emailpersonal + '┐'//14
                            + subrec.data.telcelular + '┐'//15
                            + subrec.data.emaillaboral + '┐'//16
                            + subrec.data.tellaboral + '┐'//17
                            + subrec.data.calle + '┐'//18
                            + subrec.data.numext + '┐'//19
                            + subrec.data.numint + '┐'//20
                            + subrec.data.colonia + '┐'//21
                            + subrec.data.codpostal + '┐'//22
                            + subrec.data.idestado + '┐'//23
                            + subrec.data.idMunicipio + '┐'//24// + subrec.data.idDelMunicipio + '┐'//24
                            + subrec.data.dfFechaIngreso + '┐'//25
                            + subrec.data.idRegistro + '┐'
                            + subrec.data.participante + '┐'
                            + subrec.data.foto + '┐'

                            ; ///29
    });

    var stCerRegistro = "";

    Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stCertificacionRegistro').each(function (subrec) {
        stCerRegistro +=

                            subrec.data.idCertificacion + '┐'//0
                            + subrec.data.idInstitucionAplicaExamen + '┐'//1
                            + subrec.data.idLugarAplica + '┐'//2
                            + subrec.data.idEvaluador + '┐'//3
                            + subrec.data.idNivelSeguridad + '┐'//4
                            + subrec.data.idDependenciaExterna + '┐'//5
                            + subrec.data.idInstitucionExterna + '┐'//6
                            + subrec.data.crFechaExamen + '┐'//7
                            + subrec.data.crHora + '┐'//8
							+ subrec.data.idCertificacionRegistro + '┐' //9
                            + subrec.data.idRegistro + '┐'//10
                            + subrec.data.participante + '┐'//11

                            + subrec.data.idZona + '┐'//12
                            + subrec.data.idServicio + '┐'//13
                            + subrec.data.idInstalacion + '┐'//14


                            + subrec.data.inserto + '|'; //15


    });


    var stEliminar = "";

    Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stEliminar').each(function (subrec) {
        stEliminar +=
                              subrec.data.idRegistro + '┐'//0
							+ subrec.data.idCertificacionRegistro + '┐' //1
                            + subrec.data.idCertificacion + '┐'//2
                            + subrec.data.idUsuario + '┐'//3
                            + subrec.data.elimino + '|'//4
    });

    Ext.Ajax.request({
        method: 'POST',
        url: 'Home/insertarRegistroPersona',
        params: { strRegistroPersonas: stRegistroPersonas, participante: Ext.getCmp('hfparticipante').getValue(), strCerRegistro: stCerRegistro, strEliminar: stEliminar },

        failure: function () {
            Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
        },

        success: function (response) {
            var retorno = Ext.decode(response.responseText);

            /*
            Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stCertificacionRegistro').each(function (subrec) {
            subrec.data.idCertificacionRegistro = retorno.idCertificacionRegistro;
            subrec.data.idRegistro = retorno.idRegistro;
            });

            */
            /*
            Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stEliminar').each(function (subrec) {

            subrec.data.idRegistro = retorno.idRegistro;
            subrec.data.idCertificacionRegistro = retorno.idCertificacionRegistro;
            subrec.data.idCertificacion = retorno.idCertificacion;
            });*/

            if (retorno.alerta == "") {
                if (Ext.getCmp('hfIdRegistro').getValue() == '0' || Ext.getCmp('hfIdRegistro').getValue() == '') {

                    Ext.Msg.show({
                        title: '<span style="font-size: 140%;margin-top:20px;">SICER</span>',
                        message: '<span style="font-size: 120%"><b>REGISTRO DE PERSONA GUARDADO EXITOSAMENTE</span>',
                        closable: false,
                        //modal: true,
                        buttons: Ext.Msg.YES,
                        buttonText: {
                            yes: 'Aceptar'
                        },
                       // icon: Ext.Msg.WARNING,
                        fn: function (btn) {
                            if (btn === 'yes') {
                                generarContrasena();
                            }
                        }
                    });

                }else{
                    Ext.MessageBox.alert('SICER', '<span style="font-size: 120%"><b>REGISTRO DE PERSONA GUARDADO EXITOSAMENTE</span>');
                }

                Ext.getCmp('hfIdRegistro').setValue(retorno.idRegistro);

                Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stRegistroPersona').each(function (subrec) {
                    subrec.data.idRegistro = retorno.idRegistro;
                });

                Ext.getCmp('gridCertificaciones').store.removeAll();
                Ext.getCmp('gridCertificaciones').store.load({ params: { idRegistro: retorno.idRegistro} });

            } else {
                Ext.MessageBox.alert('SICER', 'Ocurrio un error: ' + retorno.alerta);

            }

            /***************************************************************************/
        }
    });
    
    //  }//fin else

    Ext.getCmp('btnGeneraContraseña').setDisabled(false);
}


function generarContrasena() {

    var stContrasena   = Ext.getCmp('hfIdRegistro').getValue() + '┐'//0
                            + Ext.getCmp('txbCURP').getValue() + '┐'//1
                            + 0 + '┐'//2
                            ;
    Ext.Ajax.request({
        method: 'POST',
        url: 'Home/insertarContrasena',
        params: { strContrasena: stContrasena},

        failure: function () {
            Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
        },
        success: function (response) {
            var retorno = Ext.decode(response.responseText);
            
            if (retorno.alerta == "") {
                Ext.MessageBox.alert('SICER', 'La contraseña se ha generado correctamente y se ha enviado a los correos registrados.');
            } else {
                Ext.MessageBox.alert('SICER', 'Error al generar la contraseña: '+ retorno.alerta);
            }

         }
    });

}


function limpiarPantalla() {

    Ext.getCmp('hfIdEmpleado').reset();
    Ext.getCmp('hfIdRegistro').reset();
    Ext.getCmp('hfparticipante').reset();
    Ext.getCmp('hfIdCertificacionRegistro').reset();
    Ext.getCmp('hfinserto').reset();

    Ext.getCmp('txbPaterno').reset();
    Ext.getCmp('txbMaterno').reset();
    Ext.getCmp('txbNombre').reset();
    Ext.getCmp('txbNumEmpleado').reset();
    Ext.getCmp('txbPaterno').setDisabled(false);
    Ext.getCmp('txbMaterno').setDisabled(false);
    Ext.getCmp('txbNombre').setDisabled(false);
    Ext.getCmp('txbNumEmpleado').setDisabled(false);
    Ext.getCmp('txbCURP').reset();
    Ext.getCmp('txbCURP').setDisabled(false);
    Ext.getCmp('txbRFC').reset();
    Ext.getCmp('txbRFC').setDisabled(false);
    Ext.getCmp('txbCUIP').reset();
    Ext.getCmp('txbLOC').reset();
    Ext.getCmp('txbCargo').reset();
    Ext.getCmp('txbGrado').reset();
    Ext.getCmp('dfFechaNacimiento').reset();
    Ext.getCmp('txbEscolaridad').reset();
    Ext.getCmp('txbCUIP').setDisabled(false);
    Ext.getCmp('txbLOC').setDisabled(false);
    Ext.getCmp('txbCargo').setDisabled(false);
    Ext.getCmp('txbGrado').setDisabled(false);
    Ext.getCmp('dfFechaNacimiento').setDisabled(false);
    Ext.getCmp('txbEscolaridad').setDisabled(false);


    Ext.getCmp('txbTelCasa').reset();
    Ext.getCmp('txbEmailPersonal').reset();
    Ext.getCmp('txbTelCelular').reset();
    Ext.getCmp('txbEmailLaboral').reset();
    Ext.getCmp('txbTelLaboral').reset();

    Ext.getCmp('txbDomCalle').reset();
    Ext.getCmp('txbNumExterior').reset();
    Ext.getCmp('txbNumInterior').reset();
    Ext.getCmp('txbColonia').reset();
    Ext.getCmp('txbCodPostal').reset();
    Ext.getCmp('ddlDelegacionMun').reset();
    Ext.getCmp('ddlEstado').reset();
    Ext.getCmp('ddlEstado').reset();
    Ext.getCmp('ddlDelegacionMun').reset();
    Ext.getCmp('dfFechaIngreso').reset();
    Ext.getCmp('txbDomCalle').setDisabled(false);
    Ext.getCmp('txbNumExterior').setDisabled(false);
    Ext.getCmp('txbNumInterior').setDisabled(false);
    Ext.getCmp('txbColonia').setDisabled(false);
    Ext.getCmp('txbCodPostal').setDisabled(false);
    Ext.getCmp('ddlDelegacionMun').setDisabled(false);
    Ext.getCmp('ddlEstado').setDisabled(false);
    Ext.getCmp('ddlEstado').setDisabled(false);
    Ext.getCmp('ddlDelegacionMun').setDisabled(false);
    Ext.getCmp('dfFechaIngreso').setDisabled(false);




    Ext.getCmp('hfparticipante').reset();
    Ext.getCmp('hfFotografia').reset();
    Ext.getCmp('hfNombreFotografia').reset();
    Ext.getCmp('imgFoto').setSrc('Imagenes/UserFoto.png');

    Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stRegistroPersona').removeAll();
    Ext.getCmp('gridCertificaciones').store.removeAll();
    Ext.data.StoreManager.lookup('Administracion.RegistroPersonas.stEliminar').removeAll();
    Ext.getCmp('btnGeneraContraseña').setDisabled(true);

}

