
Ext.define('app.view.Examen.login.loginExamen', {
    extend: 'Ext.container.Viewport',
    alias: 'widget.loginExamen',
    initComponent: function () {
        var scope = this;

        Ext.apply(this, {
            layout: 'border',
            items: [
            {
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
                        width: 100,
                        height: 70

                    },
                    { xtype: 'image',
                        id: 'logo',
                        src: 'Imagenes/siglasSICER3.png',
                        renderTo: Ext.getBody(),
                        height: 70
                    }
                ]
            }, 
            
            {
                xtype: 'box',
                id: 'header',
                region: 'north',
                html: '<h2>BIENVENIDO</h2>',
                height: 35
            },
           /* {
                region: 'north',
                xtype: 'image',
                src: 'Imagenes/collagePrincipal.jpg',
                //renderTo: Ext.getBody(),
                width: '80%',
                height: 270
            },*/
             {
             region: 'north',
            
                 items:[{
                    xtype: 'panel',
                    layout: { type: 'vbox'/*, pack: 'center'*/, align: 'center'
                    //,bodyStyle:{"background-color:red"},
                   // width: '80%'
                },
                //height: 270,
                    
                    items: [{
                        xtype: 'image',
                        src: 'Imagenes/collageSICER.jpg',
                        //renderTo: Ext.getBody(),
                        width: '100%',
                        height: 400
                    }]
                 }]
                 
             },
            {
                region: 'south',
                border: false,
                tbar: { items: ['->', { xtype: 'image',
                    id: 'img',
                    src: 'Imagenes/icon16/salir.png',
                    renderTo: Ext.getBody(),
                    listeners:
							{
							    el: {
							        click: function () {
							            scope.destroy();
							            view = 'app.view.Administracion.login.loginAdmin';
							            Ext.create(view);
							            //admin = 1;
							        }
							    }
							}
                }]
                }
            },
             /*{
                 region: 'north',
                 xtype: 'image',
                 //src: 'Imagenes/pleca.png',
                 //renderTo: Ext.getBody(),
                 height: 270
             },*/
            {
                region: 'center',
                border: false,

                layout: { type: 'vbox'/*, pack: 'center'*/, align: 'middle' },
                items: [
                     {
                         xtype: 'panel',
                         id: 'pnlAutentica',
                         layout: {
                             /*type: 'table',
                             align: 'center',
                             columns: 2*/
                             type: 'vbox',
                             width: '100%',
                             align: 'center',
                             pack: 'center'
                         },
                         height: 200,
                         margin: '40 0 0 0',
                         width: 450,
                         items: [
                           /* { xtype: 'label',
                              text: 'LOGIN',
                              style: 'font: normal 28px arial;',
                              margin: '0 0 20 0' 
                            },*/
                            {
                                xtype: 'textfield',
                                id: 'txbUsuario',
                                //regex: /[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}/,
                                //regexText: 'No es un usuario válido',
                                maskRe: /[A-Za-z0-9]/,
                                invalidCls:'',
                                enforceMaxLength: true,
                                maxLength: 18,
                                fieldLabel: 'USUARIO',
                                labelAlign: 'right',
                                labelWidth: 150,

                                labelStyle: 'font-weight:bold;font-size: 20px;',
                                width: 350,
                                height: 30,
                                margin: '0 0 20 0',
                                listeners: {
                                   focus: function () {

                                    },
                                    change: function (field, newValue, oldValue) {
                                        field.setValue(newValue.toUpperCase().substring(0, 18));
                                    }
//                                    ,render: function () {
//                                        this.getEl().on('paste', function (e, t, eOpts) {
//                                            e.stopEvent();
//                                            Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'La opción <b>pegar</b> se encuentra deshabilitada para este campo');
//                                        });
//                                    }
                                }
                            }, 
                            {
                                xtype: 'textfield',
                                id: 'txbContrasenia',
                                //fieldStyle: 'text-transform:uppercase',
                                enforceMaxLength: true,
                                maxLength: 99, //, maskRe: /[a-zA-ZñÑ0-9\s]/,
                                inputType: 'password',
                                fieldLabel: 'CONTRASEÑA',
                                labelAlign: 'right',
                                labelWidth: 150,
                                labelStyle: 'font-weight:bold;font-size: 20px;',
                                width: 350,
                                height: 30,
                                margin: '0 0 20 0',
                                listeners: {
                                    focus: function () {

                                    },
                                    specialkey: function (f, e) {
                                        if (e.getKey() == e.ENTER) {
                                            submitExamen(scope);
                                        }
                                    }

                                }
                            }, 
                            {
                                xtype: 'button',
                                id: 'btnEntrar',
                                text: '<div style="font-size: 20px;padding: 10px;">Ingresar</div>',
                                cls: 'btn-examen',
                                renderTo: Ext.getBody(),
                                listeners: {
                                    el: {
                                        click: function () {
                                            submitExamen(scope);
                                        }
                                    }
                                }
                            }/*,
						{ xtype: 'image',
                            id: 'btnAyuda',
                            src: 'Imagenes/ayuda.png',
                            renderTo: Ext.getBody(),
                            tooltip: 'Ayuda',
                            x: 360,
                            y: 125,
                            height: 20,
                            width: 20,
//                            plugins: {
//                                ptype: 'ux-tooltip',
//                                html: 'Guía Rápida'
//                            },

                            listeners:
							{
							    el: {
							        click: function () {
							            var win = new Ext.Window({
							                width: 600,
							                height: 400,
							                maximizable: true,
							                title: 'Bienvenido',
							                layout: 'fit',
							                items: [{
							                    html: '<object type="text/html" data="Manual/manualUsuario.pdf"  style="float:left;width:100%;height:100%;" background-image:url(../Imagenes/cargandoObject.gif); background-repeat:no-repeat; background-position:center;></object>'
							                }]
							            })
							            win.show();

							        }
							    }
							}
                        }*/
                ]
                     }]
            }]
        });
        this.callParent(arguments);
    }

});



function submitExamen(scope) {

    if (Ext.getCmp('txbUsuario').validate() == false) {
        Ext.MessageBox.alert('SICER', '<b>Parámetros incorrectos</b></br>Verifique sus credenciales');
        return;
    }
       
    var txbUsuario = Ext.getCmp('txbUsuario').getValue();
    var txtPassword = Ext.getCmp('txbContrasenia').getValue(); 

    if (txbUsuario == '' || txtPassword == '') {
        Ext.MessageBox.alert('SICER', '<b>Parámetros incorrectos</b></br>Verifique sus credenciales');
        return;
    }

    Ext.Ajax.request({
        method: 'POST',
        url: 'Account/AutenticacionExamen',
        params:
            {
                usuario: txbUsuario,
                contrasena: txtPassword
            },
        failure: function () {
            Ext.MessageBox.alert('SICER', '<b>Error en el canal de comunicación</b>');
        },
        success: function (response) {
            retorno = Ext.decode(response.responseText);

            //scope.destroy();
            // Ext.create('app.view.Examen.main.mainViewExamen');
            if (retorno.message == '') {
                switch (retorno.response) {
                    case 0:
                    case 4:
                        //Ext.MessageBox.alert('SICER', '<b>Usuario no registrado</b>, verificar datos</br>' + retorno.message);
                        Ext.MessageBox.alert('SICER', '<b>Parámetros incorrectos</b></br>Verifique sus credenciales');
                        //return;
                        break;
                    case 1:
                    case 2:
                        scope.destroy();
                        Ext.create('app.view.Examen.main.mainViewExamen');
                        Ext.getStore('Examen.stCertificacionesRegistro').load({ params: { idRegistro: retorno.idRegistro} });
                        break;
                    case 5:
                        Ext.MessageBox.alert('SICER', '<b>Sesión activa</b></br>No es posible ingresar, debido a que ya se tiene una sesión activa.</br>Si no es así, cierre el explorador y vuelvalo a intentar.');
                        //return;
                        break;
                    case 6:
                        Ext.MessageBox.alert('SICER', '<b>Sesión activa</b></br>No es posible ingresar, debido a que el usuario se encuentra con una sesión activa.');
                        //return;
                        break;
                    default:
                        Ext.MessageBox.alert('SICER', '<b>Ocurrió un error al verificar credenciales</b>');

                        break;
                }
            }
            else {
                Ext.MessageBox.alert('SICER', '<b>' + retorno.message + '</b>');
            }


        }
    });      //Fin Ajax 
       
       

        }
