
Ext.define('app.view.Administracion.login.loginAdmin', {
    extend: 'Ext.container.Viewport',
    alias: 'widget.loginAdmin',
    initComponent: function () {
        var scope = this;

        Ext.apply(this, {
            layout: 'border',
            items: [{
                region: 'north',
                border: false,
                height: 70,
                items: [
                    { xtype: 'image',
                        id: 'pleca',
                        src: 'Imagenes/CabeceraPlecaSmall.png',
                        renderTo: Ext.getBody(),
                        height: 70
                    },
                    {
                        xtype: 'image',
                        id: 'blanco',
                        width:100,
                        height:70
                        
                    },
                     { xtype: 'image',
                         id: 'logo',
                         src: 'Imagenes/siglasSICER3.png',
                         renderTo: Ext.getBody(),
                         height: 70
                     }]


                 },
            {
                xtype: 'box',
                id: 'header',
                region: 'north',
                html: '<h2>SISTEMA DE CERTIFICACIONES</h2>',
                height: 35
            },
            {
                region: 'south',
                border: false,
                tbar: { items: ['->', { text: 'Estatus de la sesión: AUTENTICACIÓN'}] }
            },
            {
                region: 'center',
                border: false,
                bodyCls: 'fondo_gris',
                layout: { type: 'hbox', pack: 'end', align: 'middle', padding: 100 }

                , items: [{
                    xtype: 'panel',
                    id: 'pnlAutenticacion',
                    layout: 'absolute',
                    height: 170,
                    collapseDirection: 'left',
                    collapsible: false,
                    collapsed: false,
                    title: 'AUTENTICACIÓN DE ADMINISTRADOR',
                    border: false,
                    width: 350,
                    frame: true,
                    fieldDefaults:
                        {
                            labelWidth: 120,
                            msgTarget: 'side'
                        },
                    items: [
						    { xtype: 'label', text: 'Usuario:', cls: 'texto', x: 112, y: 15 },
						    { xtype: 'textfield', width: 150, id: 'txbUsuario', x: 180, y: 15,
						        listeners:
							{
							    focus: function () {
							        Ext.getCmp('img').setSrc('Imagenes/Login.png');
							        Ext.getCmp('btnEntrar').setSrc('Imagenes/Check.png');
							        Ext.getCmp('btnCancelar').setSrc('Imagenes/eliminar_grid.png');
							    },
							    change: function (field, newValue, oldValue)
							    { field.setValue(newValue.substring(0, 30)); },
							    render: function () {
							        this.getEl().on('paste', function (e, t, eOpts) {
							            e.stopEvent();
							            Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
							        });
							    }
							}
						    },
						{ xtype: 'label', text: 'Contraseña:', cls: 'texto', x: 90, y: 40 },
                        { xtype: 'textfield', width: 150, id: 'txbContrasenia', inputType: 'password', x: 180, y: 40,
                            listeners:
							{
							    focus: function () {
							        Ext.getCmp('img').setSrc('Imagenes/Login.png');
							        Ext.getCmp('btnEntrar').setSrc('Imagenes/Check.png');
							        Ext.getCmp('btnCancelar').setSrc('Imagenes/eliminar_grid.png');
							    },
							    specialkey: function (f, e) {
							        if (e.getKey() == e.ENTER) {
							            submit(scope);
							        }
							    }

							}
                        },
						{ xtype: 'image',
						    id: 'img',
						    src: 'Imagenes/LoginBn.png',
						    renderTo: Ext.getBody(),
						    x: 10,
						    y: 10,
						    height: 70,
						    width: 70
						},
						{ xtype: 'image',
						    id: 'btnEntrar',
						    src: 'Imagenes/CheckBn.png',
						    renderTo: Ext.getBody(),
						    x: 250,
						    y: 70,
						    height: 35,
						    width: 35,
						    listeners:
							{
							    el: {
							        click: function () {
							            submit(scope);
							        }
							    }
							}
						},
						{ xtype: 'image',
						    id: 'btnCancelar',
						    src: 'Imagenes/eliminar_gridBn.png',
						    renderTo: Ext.getBody(),
						    x: 290,
						    y: 70,
						    height: 35,
						    width: 35,
						    listeners:
                            {
                                el:
                                {
                                    click: function () {
                                        window.close();
                                    }
                                }
                            }
						}
                ]
                }]
            }]
        });
        this.callParent(arguments);
    }

});



function submit(scope) {
    Ext.MessageBox.show({
        msg: 'Realizando la petición al servidor, por favor espere...',
        progressText: 'Procesando...',
        width: 200,
        wait: true,
        icon: 'ext-mb-download'
    });
    var txbUsuario = Ext.getCmp('txbUsuario').getValue();
    var txtPassword = Ext.getCmp('txbContrasenia').getValue();

        if (txbUsuario == '' && txtPassword != '') {
            Ext.MessageBox.alert('SICER', '<b>Parámetros incompletos</b></br>Debe ingresar un nombre de usuario');
            return;
        }

        if (txbUsuario != '' && txtPassword == '') {
            Ext.MessageBox.alert('SICER', '<b>Parámetros incompletos</b></br>Debe ingresar la contraseña');
            return;
        }

        if (txbUsuario == '' && txtPassword == '') {
            Ext.MessageBox.alert('SICER', '<b>Parámetros incompletos</b></br>Debe ingresar un nombre de usuario y contraseña');
            return;
        }

        Ext.Ajax.request({
            method: 'POST',
            url: 'Account/Autenticacion',
            params:
            {
                usuario: txbUsuario,
                contrasena: txtPassword
            },
            failure: function () {
                Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
            },
            success: function (response) {
                var retorno = Ext.decode(response.responseText);

                

                switch (retorno.response) {
                    case true:
                        scope.destroy();
                        Ext.create(retorno.message);
                        
                        Ext.MessageBox.hide();
                        Ext.getCmp('lblUsuario').setText('[' + retorno.objSesion.Usuario.UsuNombre + ']');
                        Ext.getCmp('lblPerfil').setText('[' + retorno.objSesion.Usuario.Perfil.PerDescripcion + ']');

                        

                        //Ext.getCmp('treePrincipal').add({ xtype: 'menutreeAdmin' });
                        if (Ext.getCmp('tabInicio') == undefined) {
                            Ext.getCmp('tblPrincipal').add(
                            { xtype: 'inicio',
                                id: 'tabInicio'
                            });
                            Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                        }
                        else {
                            Ext.getCmp('tblPrincipal').setActiveTab('tabInicio');
                        }
                        
//                        //Ext.getCmp('treePrincipal').add({ xtype: 'treeReportes' });
//                        Ext.getCmp('treePrincipal').add({ xtype: 'sistema' });


//                        Ext.getCmp('lblBien').setText('[' + retorno.objSesion.Usuario.UsuNombre + ']');
//                        Ext.getCmp('lblNombreUsu').setValue(retorno.objSesion.Usuario.UsuNombre);
//                        Ext.getCmp('idEmpleadoUsuario').setValue(retorno.objSesion.Usuario.IdEmpleado);

                        break;
                    case false:
                        Ext.MessageBox.alert('SICER', '<b>Error de autenticación</b></br>' + retorno.message);
                        return;
                        break;
                }
            }
        });
   
}
