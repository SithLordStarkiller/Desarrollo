Ext.define('app.view.Administracion.RegistroPersonas.panDatosBasicos', {
    extend: 'Ext.Panel',
    alias: 'widget.panelDatosBasicos',
    autoScroll: true,
    initComponent: function () {
        Ext.apply(this, {
            xtype: 'panel',
            title: 'Datos Personales ',
            split: true,
            layout: {
                type: 'table',
                align: 'center',
                columns: 5
            },
            bodyPadding: '10 10 10 10',
            width: '100%',

            items: [
            {
                xtype: 'hiddenfield',
                id: 'hfFotografia'
                , value: ''
            },
            {
                xtype: 'hiddenfield',
                id: 'hfNombreFotografia'
                , value: ''
            },
            {

                xtype: 'form',
                id: 'formfoto',
                border: false,
                fileUpload: true,
                width: 200,
                rowspan: 5,
                layout: {
                    type: 'vbox',
                    align: 'middle'
                    // ,pack: 'middle'
                },
                items: [
            {
                xtype: 'image',
                id: 'imgFoto',
                src: 'Imagenes/UserFoto.png',
                height: 150, // Specifying height/width ensures correct layout
                width: 150,
                //                    style: {
                //                     borderColor: 'black',
                //                     borderStyle: 'solid'
                //                     },
                //margin: '20 50 5 50',
                listeners: {
                    render: function (c) {
                        c.getEl().on('click', function (e) {
                            //   alert('User clicked image');
                        }, c);
                    }
                }
            },


            {
                xtype: 'filefield',
                id: 'fotografia',
                emptyText: 'Selecciona un Archivo',
                buttonOnly: true,
                hideLabel: true,
                allowBlank: false,
                margin: '0 0 10 80',
                name: 'Foto',
                regex: /(.)+((\.png)|(\.jpg)|(\.jpeg)(\w)?)$/i,
                regexText: 'Solo imágenes con formatos PNG y JPG son aceptados.',
                buttonText: 'Cargar Fotografía',
                uploadConfig: {
                    maxFileSize: 1000,
                    maxQueueLength: 100
                },
                listeners: {
                    'change': {
                        fn: function (field, e) {
                            session();
                            var form = field.up('form').getForm();
                            if (Ext.getCmp('fotografia').validate()) {
                                if (form.isValid()) {
                                    form.submit
                                ({
                                    url: 'Home/obtieneImagen',
                                    method: 'post',
                                    success: function (form, action) {
                                        var data = Ext.decode(action.response.responseText, action.response.message);

                                        if (data != null) {
                                            if (data.success == false) {
                                                Ext.MessageBox.alert('SICER', '<b>' + data.response + '</b>');
                                            } else {
                                                Ext.getCmp('imgFoto').setSrc('data:image/jpeg;base64,' + data.strImagen);
                                                Ext.getCmp('hfFotografia').setValue(data.strImagen);
                                            }
                                        }
                                        else {
                                            Ext.MessageBox.alert('SICER', '<b>Error al cargar la imagen</b>');
                                        }

                                    },
                                    failure: function (form, action) {
                                        //TRAMPA!

                                        var data = Ext.decode(action.response.responseText, action.response.message);

                                        if (data.response != "") {
                                            Ext.MessageBox.alert('SICER', data.response);
                                        }
                                        else {
                                            Ext.getCmp('imgFoto').setSrc('data:image/jpeg;base64,' + data.strImagen);
                                            Ext.getCmp('hfFotografia').setValue(data.strImagen);
                                        }

                                    }

                                });

                                }
                                else {
                                    Ext.Msg.alert("SICER", "Solo imagenes PNG y JPG son aceptados.");
                                }
                            } else {
                                Ext.Msg.alert("SICER", "Solo imagenes PNG y JPG son aceptados.");
                            }
                        }
                    }
                }
            }
            ]

            }, ///fin inicio para foto




             {xtype: 'hiddenfield', id: 'inserta', value: 'true' },
             //{ xtype: 'hiddenfield', id: 'participante', value: 'E' },

         {
             xtype: 'textfield',
             id: 'txbPaterno',
             maskRe: /[A-Za-zñÑ\s]/,
           //  msgTarget: 'side',
             maxLength: 30,
             enforceMaxLength: true,
             allowBlank: false,
             blankText: 'Campo Obligatorio',
             fieldLabel: 'Apellido Paterno',
             disabledCls: 'af-item-disabled',
             labelAlign: 'right',
             labelWidth: 120,
             width: 300,
             listeners: {
                 blur: function (field, newValue, oldValue) {
                     this.setValue(field.rawValue.toUpperCase().substring(0, 30));
                 }//,
//                 render: function () {
//                     this.getEl().on('paste', function (e, t, eOpts) {
//                         e.stopEvent();
//                         Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
//                     });
//                 }
             }
         },
        {
            xtype: 'textfield',
            id: 'txbMaterno',
            maskRe: /[A-Za-zñÑ\s]/,
          //  msgTarget: 'side',
            enforceMaxLength: true,
            maxLength: 30,
            allowBlank: false,
            blankText: 'Campo Obligatorio',
            fieldLabel: 'Apellido Materno',
            disabledCls: 'af-item-disabled',
            labelAlign: 'right',
            labelWidth: 120,
            width: 300,
            listeners: {
                blur: function (field, newValue, oldValue) {
                    this.setValue(field.rawValue.toUpperCase().substring(0, 30));
                }
            }
        },
         {
             xtype: 'textfield',
             id: 'txbNombre',
             maskRe: /[A-Za-zñÑ\s]/,
         //    msgTarget: 'side',
             enforceMaxLength: true,
             maxLength: 30,
             allowBlank: false,
             blankText: 'Campo Obligatorio',
             fieldLabel: "Nombre",
             disabledCls: 'af-item-disabled',
             labelAlign: 'right',
             labelWidth: 120,
             width: 300,
             listeners: {
                 blur: function (field, newValue, oldValue) {
                     this.setValue(field.rawValue.toUpperCase().substring(0, 30));
                 }
             }
         },
        {
            xtype: 'textfield',
            id: 'txbNumEmpleado',
            maskRe: /[A-Za-z0-9\/.,.-._.-]/,
         //   msgTarget: 'side',
           // maxLengthText: 'Máximo 10 caracteres el resto sera omitido',
            maxLength: 10,
            enforceMaxLength: true,
            allowBlank: true,
            fieldLabel: 'Número de Empleado',
            disabledCls: 'af-item-disabled',
            labelAlign: 'right',
            labelWidth: 120,
            width: 300,
            listeners: {
                blur: function (field, newValue, oldValue) {
                    this.setValue(field.rawValue.toUpperCase().substring(0, 10));
                }
            }
        },
         {
             xtype: 'textfield',
             id: 'txbCURP',
             regex: /[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}/,
             regexText: 'No es un CURP válido',
             maskRe: /[A-Za-z0-9]/,
             enforceMaxLength: true,
             maxLength: 18,
             allowBlank: false,
             blankText: 'Campo Obligatorio',
             fieldLabel: 'CURP',
             disabledCls: 'af-item-disabled',
             labelAlign: 'right',
             labelWidth: 120,
             width: 300,
             listeners: {
                 blur: function (field, newValue, oldValue) {

                     if (Ext.isEmpty(field.rawValue) == false) {
                         this.setValue(field.rawValue.toUpperCase());
                         var filter = /[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}/;
                         if (!filter.test(field.rawValue)) {
                             Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', '<b>CURP NO VALIDO</b>');
                             this.setValue('');
                             return false;
                         }
                         else {
                             Ext.Ajax.request({
                                 method: 'POST',
                                 url: 'Home/validaCURP',
                                 params: { CURP: field.rawValue },

                                 failure: function () {
                                     Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
                                 },
                                 success: function (response) {
                                     var retorno = Ext.decode(response.responseText);

                                     if (retorno.alerta == '') {
                                         if (retorno.respuesta != '1') {
                                             Ext.MessageBox.alert('SICER', '<b>Validación</b></br>El CURP ingresado ya se encuentra registrado para la persona </br> <b>' + retorno.nombreParticipante + '</b>. </br>Es necesario buscar los datos de esta persona para registrarla.');
                                             field.setValue('');
                                         }
                                     } else {
                                         Ext.MessageBox.alert('SICER', '<b>Ocurrió un error al validar CURP</b></br>' + response.alerta);
                                     }
                                 }
                             });
                         }
                     }

                 }

             }
         },
        {
            xtype: 'textfield',
            id: 'txbRFC',
            regex: /(([A-Z]|[a-z]|\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))/,
            regexText: 'No es un RFC válido',
            maskRe: /[A-Za-z0-9]/,
            //msgTarget: 'side',
            allowBlank: false,
            maxLength: 13,
            enforceMaxLength: true,
            blankText: 'Campo Obligatorio',
            fieldLabel: 'RFC',
            disabledCls: 'af-item-disabled',
            labelAlign: 'right',
            labelWidth: 120,
            width: 300,
            listeners: {
                blur: function (field, newValue, oldValue) {

                    if (Ext.isEmpty(field.rawValue) == false) {
                        this.setValue(field.rawValue.toUpperCase());
                        var filter = /(([A-Z]|[a-z]|\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))/;
                        if (!filter.test(field.rawValue)) {
                            Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'RFC NO VALIDO </br><b>' + field.rawValue + '</b> es incorrecto');
                            this.setValue('');
                            return false;
                        }
                    }
                }
            }
        },
        {
            xtype: 'textfield',
            id: 'txbCUIP',
            maskRe: /[A-Za-z0-9]/,
            //msgTarget: 'side',
            enforceMaxLength: true,
            maxLength: 21,
            allowBlank: true,
            fieldLabel: 'CUIP',
            disabledCls: 'af-item-disabled',
            labelAlign: 'right',
            labelWidth: 120,
            width: 300,
            listeners: {
                blur: function (field, newValue, oldValue) {
                    this.setValue(field.rawValue.toUpperCase().substring(0, 21));
                }
            }
        },
        {
            xtype: 'textfield',
            id: 'txbLOC',
            maskRe: /[A-Za-z0-9]/,
            //msgTarget: 'side',
            enforceMaxLength: true,
            maxLength: 18,
            allowBlank: true,
            fieldLabel: 'LOC',
            labelAlign: 'right',
            disabledCls: 'af-item-disabled',
            labelWidth: 120,
            width: 300,
            listeners: {
                blur: function (field, newValue, oldValue) {
                    this.setValue(field.rawValue.toUpperCase().substring(0, 18));
                }
            }
        },
         {
             xtype: 'textfield',
             id: 'txbGrado',
             maskRe: /[A-Z a-z]/,
            // msgTarget: 'side',
             enforceMaxLength: true,
             maxLength: 60,
             allowBlank: false,
             blankText: 'Campo Obligatorio',
             fieldLabel: 'Grado',
             disabledCls: 'af-item-disabled',
             labelAlign: 'right',
             labelWidth: 120,
             width: 300,
             listeners: {
                 blur: function (field, newValue, oldValue) {
                     this.setValue(field.rawValue.toUpperCase().substring(0, 60));
                 }
             }
         },
         {
             xtype: 'textfield',
             id: 'txbCargo',
             maskRe: /[A-Z a-z]/,
           //  msgTarget: 'side',
             enforceMaxLength: true,
             maxLength: 60,
             allowBlank: false,
             blankText: 'Campo Obligatorio',
             fieldLabel: 'Cargo',
             disabledCls: 'af-item-disabled',
             labelAlign: 'right',
             labelWidth: 120,
             width: 300,
             listeners: {
                 blur: function (field, newValue, oldValue) {
                     this.setValue(field.rawValue.toUpperCase().substring(0, 60));
                 }
             }
         }
,
            //{
            //    xtype: 'numberfield',
            //    id: 'nfEdad',
            //    fieldLabel: "EDAD",
            //    labelAlign: 'right',
            //    labelWidth: 120,
            //    width: 300,
            //    allowBlank: false,
            //    blankText: 'Campo Obligatorio',
            //    maskRe: /[0-9]/,
            //    disabledCls: 'af-item-disabled',
            //    //value: 30,
            //    maxValue: 99,
            //    maxLengthText: 'La edad máxima es 99',
            //    maxText: 'La edad máxima es 99',
            //    minValue: 0,
            //    listeners: {
            //        change: function () {
            //            if (Ext.util.Format.trim(this.getRawValue()) != '') {
            //                if (this.getRawValue().length >= 3) {
            //                    Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La edad ingresada <b>' + (this.getRawValue()) + '</b> es invalida');
            //                    this.setValue('');
            //                    return false;
            //                }
            //            }
            //        }
            //    }

            //},
        {
        xtype: 'datefield',
        id: 'dfFechaNacimiento',
        format: 'd/m/Y',
        SubmitFormat: 'd-m-Y H:i:s',
        altFormats: 'd/m/Y',
        editable: false,
        emptyText: '',
        fieldLabel: "Fecha de Nacimiento",
        disabledCls: 'af-item-disabled',
        labelAlign: 'right',
        labelWidth: 120,
        width: 300,
        allowBlank: false,
        blankText: 'Campo Obligatorio, Por favor seleccione una opción'
       // , msgTarget: 'side'
    },

 {
     xtype: 'textfield',
     id: 'txbEscolaridad',
     maskRe: /[A-Z a-z]/,
     //msgTarget: 'side',
     enforceMaxLength: true,
     maxLength: 150,
     allowBlank: false,
     blankText: 'Campo Obligatorio',
     disabledCls: 'af-item-disabled',
     fieldLabel: 'Escolaridad',
     labelAlign: 'right',
     labelWidth: 120,
     width: 300,
     listeners: {
         blur: function (field, newValue, oldValue) {
             this.setValue(field.rawValue.toUpperCase().substring(0, 150));
         }
     }
 },
{
    xtype: 'textfield',
    id: 'txbTelCasa',
    maskRe: /[0-9]/,
   // msgTarget: 'side',
    enforceMaxLength: true,
    maxLength: 10,
    allowBlank: false,
    blankText: 'Campo Obligatorio',
    disabledCls: 'af-item-disabled',
    fieldLabel: 'Teléfono Casa',
    labelAlign: 'right',
    labelWidth: 120,
    width: 300,
    listeners: {
        blur: function (field, newValue, oldValue) {
            this.setValue(field.rawValue.toUpperCase().substring(0, 10));
        }
    }
},

    {
        xtype: 'textfield',
        id: 'txbEmailPersonal',
//        msgTarget: 'side',
        enforceMaxLength: true,
        maxLength: 60,
        allowBlank: false,
        disabledCls: 'af-item-disabled',
        blankText: 'Campo Obligatorio',
        fieldLabel: 'Correo Elec. Personal',
        labelAlign: 'right',
        labelWidth: 120,
        width: 300,
        listeners: {

            blur: function (field, newValue, oldValue) {
                if (Ext.isEmpty(field.rawValue) == false) {
                    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    if (!filter.test(field.rawValue)) {
                        Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'El correo electrónico <b>' + field.rawValue + '</b> es incorrecto, al ingresarlo verifique que se encuentre con la siguiente notación "<b>usuario@dominio.actividad.pais"</b>, ejemplo juan@yahoo.com.mx');
                        this.setValue('');
                        return false;
                    }
                }

            }
        }
    },
 {
     xtype: 'textfield',
     id: 'txbTelCelular',
     maskRe: /[0-9]/,
   //  msgTarget: 'side',
     maxLength: 15,
     enforceMaxLength: true,
     allowBlank: false,
     blankText: 'Campo Obligatorio',
     disabledCls: 'af-item-disabled',
     fieldLabel: 'Tel. Celular',
     labelAlign: 'right',
     labelWidth: 120,
     width: 300,
     listeners: {
         change: function (field, newValue, oldValue) {
             field.setValue(newValue.substring(0, 15));
         }
     }
 },
    {
        xtype: 'textfield',
        id: 'txbEmailLaboral',
      //  msgTarget: 'side',
        maxLength: 60,
        //allowBlank: false,
        //blankText: 'Campo Obligatorio',
        enforceMaxLength: true,
        disabledCls: 'af-item-disabled',
        fieldLabel: 'Correo Elec. Laboral',
        labelAlign: 'right',
        labelWidth: 120,
        width: 300,
        listeners: {

            blur: function (field, newValue, oldValue) {
                if (Ext.isEmpty(field.rawValue) == false) {
                    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                    if (!filter.test(field.rawValue)) {
                        Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'El correo electrónico <b>' + field.rawValue + '</b> es incorrecto, al ingresarlo verifique que se encuentre con la siguiente notación "<b>usuario@dominio.actividad.pais"</b>, ejemplo juan.aguilar@cns.gob.mx');
                        this.setValue('');
                        return false;
                    }
                }

            }
        }
    },
    {
        xtype: 'textfield',
        id: 'txbTelLaboral',
        maskRe: /[0-9() ]/,
     //   msgTarget: 'side',
        enforceMaxLength: true,
        maxLength: 20,
        allowBlank: false,
        blankText: 'Campo Obligatorio',
        disabledCls: 'af-item-disabled',
        fieldLabel: 'Teléfono Laboral (EXT)',
        labelAlign: 'right',
        labelWidth: 120,
        width: 300,
        listeners: {
            change: function (field, newValue, oldValue) {
                field.setValue(newValue.toUpperCase().substring(0, 20));
            }
        }
    }
  , {
      xtype: 'datefield',
      id: 'dfFechaIngreso',
      format: 'd/m/Y',
      SubmitFormat: 'd-m-Y H:i:s',
      altFormats: 'd/m/Y',
      editable: false,
      emptyText: '',
      fieldLabel: "Fecha de Registro",
      disabledCls: 'af-item-disabled',
      labelAlign: 'right',
      labelWidth: 120,
      width: 300,
      allowBlank: false,
      blankText: 'Campo Obligatorio, Por favor seleccione una opción'
      //, msgTarget: 'side'

  },

            ]
        });
        this.callParent(arguments);
    }
});
