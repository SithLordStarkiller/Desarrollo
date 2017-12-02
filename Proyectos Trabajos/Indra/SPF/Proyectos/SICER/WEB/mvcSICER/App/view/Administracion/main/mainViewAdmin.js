Ext.define('app.view.Administracion.main.mainViewAdmin', {
    extend: 'Ext.container.Viewport',
    //closable: true,
    alias: 'widget.mainViewAdmin',
    initComponent: function () {
        Ext.create('app.store.Administracion.stTreeMenu');

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
                        { xtype: 'hiddenfield', id: 'hfIdRol', value: '' },
                        { text: 'Usuario: ' },
                        { xtype: 'label', id: 'lblUsuario', text: '-' },
                            //{ xtype: 'hiddenfield', id: 'hfBandera', value: '0' },
                            //{ xtype: 'hiddenfield', id: 'lblNombreUsu', value: '' },
                            //{ xtype: 'hiddenfield', id: 'lblTipoPostulacion', value: '4'},
                            //'->',
                            //{ xtype: 'hiddenfield', id: 'idEmpleadoUsuario', value: '' },
                        {text: 'Perfil: ' },
                        { xtype: 'label', id: 'lblPerfil', text: '-' },
                        { xtype: 'hiddenfield', id: 'hfPermisoCaptura', value: '' },
                        { xtype: 'hiddenfield', id: 'hfPermisoModifica', value: '' }
                        
                    ]
                        }
                    }, {
                        region: 'west',
                        collapsible: true,
                        title: 'Seleccione una opción',
                        split: false,
                        scrollable: true,
                        width: 300,
                        items: [{
                            xtype: 'menutreeAdmin',
                            region: 'west',
                            border: false
                            , width: '100%'
                        }]
                    }, {
                        region: 'center',
                        //title: 'Contenedor principal',
                        layout: 'fit',
                        items:
                            {
                                xtype: 'tabpanel',
                                id: 'tblPrincipal',
                                items: []
                            }
                    }]
        });



        this.callParent(arguments);
    }

});