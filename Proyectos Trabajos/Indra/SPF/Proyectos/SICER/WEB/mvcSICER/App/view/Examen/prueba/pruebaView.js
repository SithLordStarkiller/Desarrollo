Ext.define('app.view.Examen.main.pruebaView', {
    extend: 'Ext.container.Viewport',
    //closable: true,
    alias: 'widget.pruebaView',
    initComponent: function () {
        //session();
        var scope = this;
        Ext.apply(this, {
            layout: 'border',
            items: [{
                region: 'north',
                border: false,
                height: 70,
                items: [{ xtype: 'image',
                    renderTo: Ext.getBody(),
                    id: 'pleca',
                    src: 'Imagenes/CabeceraPlecaSmall.png',
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
                        { text: 'Usuario: ' },
                        { xtype: 'label', id: 'lblUsuario', text: '-' },
                        { text: 'Perfil: ' },
                        { xtype: 'label', id: 'lblPerfil', text: '-' },
                        { xtype: 'hiddenfield', id: 'hfPermisoCaptura', value: '' },
                        { xtype: 'hiddenfield', id: 'hfPermisoModifica', value: '' }
                    ]
                        }
                    },
                     {
                         region: 'center',
                         //title: 'Contenedor principal',
                         layout: 'fit',
                         items:
                            {
                                xtype: 'panel',
                                id: 'panPrincipal',
                                layout: {
                                    type: 'vbox',
                                    align: 'center',
                                    pack: 'center'
                                },
                                items: [
                                    {
                                        xtype: 'panel',
                                        title: '<span style="font-size: 160%;margin-top:20px;">Bienvenido al Sistema de Certificaciones</span>',
                                        id: 'panSeleccionarCertificacion',
                                        bodyPadding: '10 10 10 10',
                                        width: '50%',
                                        layout: {
                                            type: 'vbox',
                                            align: 'center'
                                            //  columns: 2
                                        },
                                        items: [
                                                 {
                                                     xtype: 'label',
                                                     //id: 'lblDomicilioParticular',
                                                     //text: 'DOMICILIO PARTICULAR',
                                                     html: '<h4 style="font-size: 160%;">Elija el examen de certificación que realizará:</h4>'
                                                     //  , width: 800
                                                 },
                                                {
                                                    xtype: 'combo',
                                                    id: 'ddlExamenesCertificacionUsuario',
                                                    //store: Ext.getStore('catalogos.stEstados').load(),
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
                                                    //valueField: 'idEstado',
                                                    //displayField: 'estDescripcion',
                                                    //  disabled: true,
                                                    msgTarget: 'side',
                                                    listeners:
                                                    {
                                                        select: function (field, newValue, oldValue) {

                                                        }
                                                    }
                                                },
                                                {
                                                    xtype: 'button'
                                                    , text: 'Comenzar Examen',
                                                    margin: '0 0 20 0'
                                                    , handler: function () {
                                                        //    session();

                                                        Ext.Msg.show({
                                                            title: '<span style="font-size: 140%;margin-top:20px;">Comenzar Examen</span>',
                                                            message: '<span style="font-size: 120%;margin-top:20px;">El tiempo del examen es de 2 horas. ¿Estás seguro de comenzar ahora?</span>',
                                                            closable: false,
                                                            buttons: Ext.Msg.YESNO,
                                                            buttonText: {
                                                                no: 'Esperar',
                                                                yes: 'Sí'
                                                            },
                                                            icon: Ext.Msg.QUESTION,
                                                            fn: function (btn) {
                                                                if (btn === 'yes') {
                                                                    Ext.MessageBox.alert('SICER', '<b>ENTRASTE!!</b>');

                                                                }
                                                            }
                                                        });



                                                    }
                                                }
                                             ]
                                    }


                                ]
                            }
                     }]
        });


        this.callParent(arguments);
    }

});
