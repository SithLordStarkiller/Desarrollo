Ext.define('app.view.Administracion.RegistroCertificacion.gridTemas', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.gridTemas',
    id: 'gridTemas',
    initComponent: function () {
        session();
        var contadorFunciones = 0;

        var contPregTema = 0;

        var contadorTemas = 0;


        Ext.create('Ext.data.Store', {
            model: 'app.model.Administracion.RegistroCertificacion.mdFuncion',
            storeId: 'stFuncionesTemporal'
        });


        Ext.apply(this, {
            store: 'Administracion.RegistroCertificacion.stTemas',
            collapsible: false,
            loadMask: true,
            selModel: 'rowmodel',
            /*selModel:
            {
            pruneRemoved: false
            },*/
            multiSelect: false,
            viewConfig:
            {
                trackOver: false
                //,preserveScrollOnRefresh: true
            },
            listeners: {
                rowclick: function (gridView) {
                    session();
                    /*
                    Ext.MessageBox.show({
                    msg: 'Actualizando funciones de tema...',
                    progressText: 'Procesando...',
                    width: 200,
                    wait: true,
                    icon: 'ext-mb-download'
                    });
                    */
                    var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                    if (gridView.getSelectionModel().getSelection()[0].data.idTema == 0) {
                        ///////////////////////Se actualiza GRID//////////////////////////////
                        Ext.data.StoreManager.lookup('stFuncionesTemporal').removeAll();
                        var idTemaTemporal = gridView.getSelectionModel().getSelection()[0].data.idTematemporal;
                        var funcionesTemporal = [];
                        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').each(function (rec) {
                            if (rec.data.idTemaTemporal == idTemaTemporal)
                                funcionesTemporal.push(rec.copy());
                        });
                        Ext.data.StoreManager.lookup('stFuncionesTemporal').add(funcionesTemporal);
                        Ext.getCmp('gridFunciones').reconfigure(Ext.data.StoreManager.lookup('stFuncionesTemporal'));
                        Ext.getCmp('gridPreguntas').store.removeAll();
                        Ext.getCmp('gridRespuestas').store.removeAll();
                        // Ext.getCmp('gridPreguntas').focus();

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }

                    else {

                        ///////////////////////Se actualiza GRID//////////////////////////////
                        Ext.data.StoreManager.lookup('stFuncionesTemporal').removeAll();
                        var idTema = gridView.getSelectionModel().getSelection()[0].data.idTema;
                        var funcionesTemporal = [];
                        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').each(function (rec) {
                            if (rec.data.idTema == idTema)
                                funcionesTemporal.push(rec.copy());
                        });
                        Ext.data.StoreManager.lookup('stFuncionesTemporal').add(funcionesTemporal);
                        Ext.getCmp('gridFunciones').reconfigure(Ext.data.StoreManager.lookup('stFuncionesTemporal'));

                        Ext.getCmp('gridPreguntas').store.removeAll();
                        Ext.getCmp('gridRespuestas').store.removeAll();
                        // Ext.getCmp('gridPreguntas').focus();

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                    Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                    Ext.getCmp('panelFuncionesCertificaciones').setTitle('<span style="font-size: 160%;margin-top:20px;">Funciones del tema: ' + gridView.getSelectionModel().getSelection()[0].data.temCodigo + '</span>')

                    //Ext.MessageBox.hide();
                    /*
                    Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').each(function (rec) {

                    var temas = Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas');
                    //                   //     for (i = 0; i < temas.getCount(); i++) {
                    //                            if (rec.data.temActivo) {
                    //                                contPregTema = contPregTema + rec.data.ctAleatorias;
                    //                            }

                    if (rec.data.ctActivo) {
                    contPregTema = contPregTema + rec.data.ctAleatorias;
                    }
                    //                     //   }

                    if (contPregTema > Ext.getCmp("txbNumeroDePreguntasExamen").getValue()) {
                    Ext.MessageBox.alert('SICER - ERROR DE VALIDACIÓN.', 'Las preguntas totales de los Temas no debe exceder a las del campo <b>Número de Preguntas del Examen</b>.');
                    contPregTema = 0;
                    }
                    });*/
                }
            },
            columns: [
                {
                    xtype: 'rownumberer',
                    text: 'Orden',
                    width: '4%'
                },
                 {
                     text: "Código",
                     dataIndex: 'temCodigo',
                     width: '15%',
                     hidden: false,
                     align: 'center',
                     sortable: false
                 },
                {
                    text: "Tema",
                    dataIndex: 'temDescripcion',
                    width: '59%',
                    align: 'left',
                    hidden: false,
                    sortable: false
                },
                 {
                     text: "Preguntas",
                     dataIndex: 'ctAleatorias',
                     width: '5%',
                     align: 'center',
                     hidden: false,
                     sortable: false
                 },
                {
                    text: "Preg. Correctas",
                    dataIndex: 'ctCorrectas',
                    width: '6%',
                    hidden: false,
                    align: 'center',
                    sortable: false
                },
                {
                    text: "Minutos",
                    dataIndex: 'ctTiempo',
                    width: '5%',
                    hidden: false,
                    align: 'center',
                    sortable: false
                },
            /*{
            text: "Activa",
            dataIndex: 'ctActivo',
            width: '5%',
            hidden: false,
            align: 'center',
            sortable: true
            }*/
                {
                xtype: 'checkcolumn',
                header: 'Activo',
                id: 'checkColTemas',
                dataIndex: 'ctActivo',
                //itemId: 'checkcolumnId',
                width: '5%',
                stopSelection: false,
                listeners: {
                    checkchange: function (column, recordIndex, checked, rowIndex) {

                        //alert(checked);
                        /*alert("hi");
                        alert(column);
                        alert(recordIndex);
                        alert(rowIndex);
                        */
                        //Ext.data.StoreManager.lookup('stPruebasDinamico').getAt(recordIndex).data.actualiza = 1;
                        //alert(Ext.getCmp('gridTemas').store.getAt(recordIndex).data.temDescripcion);

                        //Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctActivo = checked;
                        //var store = Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas');
                        var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                        var funcionesTema = 0;
                        Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').each(function (rec) {
                            if (rec.data.tfActivo == true
                            && rec.data.idTema == Ext.getCmp('gridTemas').store.getAt(recordIndex).data.idTema
                            && rec.data.idTemaTemporal == Ext.getCmp('gridTemas').store.getAt(recordIndex).data.idTematemporal)
                                funcionesTema++;
                        });

                        if (checked == true && funcionesTema == 0) {
                            Ext.MessageBox.alert('SICER', 'Es necesario que el tema tenga por lo menos una función activa para ser activado');
                            Ext.getCmp('gridTemas').store.getAt(recordIndex).data.ctActivo = false;
                            Ext.getCmp('gridTemas').getView().refresh(true);

                            Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                            return;
                        }



                        if (Ext.getCmp('gridTemas').store.getAt(recordIndex).data.idTema == 0) {
                            Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas')
                                    .findRecord('idTematemporal', Ext.getCmp('gridTemas').store.getAt(recordIndex).data.idTematemporal, 0, false, true, true).data.ctActivo = checked;
                        } else {
                            Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas')
                                    .findRecord('idTema', Ext.getCmp('gridTemas').store.getAt(recordIndex).data.idTema, 0, false, true, true).data.ctActivo = checked;
                        }

                       
                        desactivaCertificacion();

                        Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                    }

                }
            }
            ],
            /* bbar: new Ext.PagingToolbar({
            pageSize: 20,
            //store: ProjectsDataStore,
            displayInfo: true,
            emptyMsg: "No records found."
            }),*/
            tbar: [{
                text: 'Agregar Tema',
                tooltip: 'Agregar un tema a esta Certificación',
                id: 'tbarAgrTema',
                handler: function () {
                    session();
                    var window = new Ext.Window({
                        id: 'window',
                        title: 'Agregar Tema',
                        //width: 1100,
                        resizable: true,
                        modal: true,
                        plain: true,
                        layout: {
                            type: 'vbox',
                            align: 'center'
                        },
                        bodyPadding: '10 10 10 10',
                        items: [
                                        {
                                            xtype: 'panel',

                                            bodyPadding: '10 10 10 10',
                                            //width: '100%',
                                            layout: {
                                                type: 'table',
                                                columns: 2
                                                //tdAttrs: { style: 'padding: 2px;' }
                                            },
                                            items: [

                                                {
                                                    xtype: 'textfield',
                                                    id: 'txbNombreTema',
                                                    fieldLabel: '<b>Nombre del Tema</b>',
                                                    labelAlign: 'right',
                                                    labelWidth: 130,
                                                    width: 600,
                                                    colspan: 2,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    maskRe: /[áéíóúÁÉÍÓÚA-Za-zñÑ0-9():.,-_ ]/,
                                                    maxLength: 500,
                                                    enforceMaxLength: true,
                                                    //value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temDescripcion,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            field.setValue(newValue.toUpperCase());
                                                        }
                                                    }
                                                },
                                            /*{
                                            xtype: 'checkbox',
                                            boxLabel: 'Tema Activo',
                                            id: 'TemaActivo',
                                            //margin: '20 200 20 110',
                                            //value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temActivo,
                                            //value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctActivo,
                                            allowBlank: true
                                            },*/
                                                {
                                                xtype: 'textfield',
                                                id: 'txbCodigoTema',
                                                fieldLabel: 'Código Tema',
                                                labelAlign: 'right',
                                                labelWidth: 130,
                                                width: 300,
                                                allowBlank: false,
                                                blankText: 'Campo Obligatorio',
                                                maskRe: /[A-Za-zñÑ0-9-]/,
                                                maxLength: 30,
                                                enforceMaxLength: true,
                                                //value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temCodigo,
                                                listeners: {
                                                    change: function (field, newValue, oldValue) {
                                                        field.setValue(newValue.toUpperCase());
                                                    }
                                                }
                                            },
                                            /*{
                                            xtype: 'combo',
                                            id: 'cbOrdenTema',
                                            //store: Ext.getStore('Administracion.RegistroCertificacion.stOrdenTema').load(),
                                            emptyText: 'SELECCIONAR',
                                            triggerAction: 'all',
                                            queryMode: 'local',
                                            grow: true,
                                            editable: false,
                                            fieldLabel: "Orden del Tema",
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 500,
                                            valueField: 'idTema',
                                            displayField: 'ordenTema',
                                            // autoSelect: true,
                                            //  disabled: true,
                                            allowBlank: false,
                                            value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctOrden,
                                            blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                            msgTarget: 'side'
                                            },

                                            {
                                            xtype: 'textfield',
                                            id: 'txbNoPreguntasAleatoriasTema',
                                            fieldLabel: 'Núm. Preguntas aleatorias',
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 250,
                                            allowBlank: false,
                                            blankText: 'Campo Obligatorio',
                                            maxLength: 4,
                                            enforceMaxLength: true,
                                            maskRe: /[0-9]/,
                                            //value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctAleatorias,
                                            listeners: {
                                            change: function (me, newValue, oldValue, eOpts) {

                                            /*  if (newValue > Ext.getCmp("txbNumeroDePreguntasExamen").getValue()) {
                                            Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>No. de preguntas aleatorias por Tema</b> no debe ser mayor al campo <b>Número de Preguntas del Examen</b>');
                                            }* /
                                            }
                                            }
                                            },
                                            {
                                            xtype: 'textfield',
                                            id: 'txbNoPreguntasCorrectasAprobarTema',
                                            fieldLabel: 'Núm. de preguntas correctas:',
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 250,
                                            allowBlank: false,
                                            blankText: 'Campo Obligatorio',
                                            maskRe: /[0-9]/,
                                            maxLength: 4,
                                            enforceMaxLength: true,
                                            //value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctCorrectas,
                                            listeners: {
                                            change: function (me, newValue, oldValue, eOpts) {
                                            /* if (newValue > Ext.getCmp("txbNoPreguntasAleatoriasTema").getValue()) {
                                            Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>No. De preguntas correctas para aprobar el Tema</b> no debe ser mayor al campo <b>No. de preguntas aleatorias por Tema</b>');
                                            }* /
                                            }
                                            }
                                            },*/
                                                {
                                                xtype: 'textfield',
                                                id: 'txbTiempoExamenContestarTema',
                                                grow: true,
                                                editable: true,
                                                fieldLabel: 'Minutos para contestar el Tema',
                                                labelAlign: 'right',
                                                labelWidth: 200,
                                                width: 300,
                                                allowBlank: false,
                                                blankText: 'Campo Obligatorio',
                                                maskRe: /[0-9]/,
                                                maxLength: 4,
                                                enforceMaxLength: true,
                                                //value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctTiempo,
                                                listeners: {
                                                    change: function (field, newValue, oldValue) {
                                                        //field.setValue(newValue.substring(0, 30));
                                                    }
                                                }
                                            }

                                            ],

                                            buttons: [

                                                        {
                                                            text: 'Aceptar', width: 150, handler: function () {

                                                                //AGREGAR TEMA
                                                                session();

                                                                if (Ext.getCmp('txbNombreTema').getRawValue() == (null || '')
                                                                  || Ext.getCmp('txbCodigoTema').getRawValue() == (null || '')
                                                                //|| Ext.getCmp('cbOrdenTema').getRawValue() == 0
                                                                //  || Ext.getCmp('txbNoPreguntasAleatoriasTema').getRawValue() == 0
                                                                // || Ext.getCmp('txbNoPreguntasCorrectasAprobarTema').getRawValue() == 0
                                                                  || Ext.getCmp('txbTiempoExamenContestarTema').getRawValue() == 0) {
                                                                    Ext.MessageBox.alert('SICER', 'Es necesario llenar todos los campos.');
                                                                    return false;
                                                                }



                                                                var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                                                                // var index =Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].rowIndex;
                                                                var retorno = false, aux = 0;
                                                                Ext.getCmp('gridTemas').store.each(function (recTema) {
                                                                    if (//index != aux && 
                                                                        Ext.getCmp('txbCodigoTema').getRawValue() == recTema.data.temCodigo) retorno = true;
                                                                    // aux++;
                                                                });
                                                                if (retorno) {
                                                                    Ext.MessageBox.alert('SICER', 'Algún tema tiene el mismo código de tema');
                                                                    return;
                                                                }


                                                                contadorTemas = contadorTemas + 1;
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').add({
                                                                    ctOrden: Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').count() + 1,
                                                                    ctActivo: 0,
                                                                    temDescripcion: Ext.getCmp('txbNombreTema').getRawValue(),
                                                                    temCodigo: Ext.getCmp('txbCodigoTema').getRawValue(),
                                                                    //ctAleatorias: Ext.getCmp('txbNoPreguntasAleatoriasTema').getRawValue(),
                                                                    ctAleatorias: 0, //Agrego cero ya que 
                                                                    //ctCorrectas: Ext.getCmp('txbNoPreguntasCorrectasAprobarTema').getRawValue(),
                                                                    ctCorrectas: 0,
                                                                    ctTiempo: Ext.getCmp('txbTiempoExamenContestarTema').getRawValue(),
                                                                    idTematemporal: contadorTemas
                                                                });

                                                                //                                                                var 
                                                                //                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas').each(function (recTema) { 
                                                                //                                                                    
                                                                //                                                                
                                                                //                                                                });



                                                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                                                                window.destroy();
                                                            }
                                                        },
                                                        {
                                                            text: 'Cancelar', width: 150, handler: function () {

                                                                session();
                                                                window.destroy();
                                                            }
                                                        }
                                                    ]
                                        }
                                    ]
                    });
                    window.show();

                }
            },
                    '-',
                     {
                         text: 'Modificar Tema',
                         tooltip: 'Modifica los datos del tema seleccionado',
                         id: 'tbarModTema',
                         //iconCls: 'edit',
                         handler: function () {
                             session();
                             // grid.getSelectionModel().getSelections()
                             if (Ext.getCmp('gridTemas').getSelectionModel().hasSelection()) {
                                 //if (grid.getSelectionModel().hasSelection()) {
                                 var window = new Ext.Window({
                                     id: 'window',
                                     title: 'Modificar Tema',
                                     //width: 1100,
                                     resizable: true,
                                     modal: true,
                                     plain: true,
                                     layout: {
                                         type: 'vbox',
                                         align: 'center'
                                     },
                                     bodyPadding: '10 10 10 10',
                                     items: [
                                        {
                                            xtype: 'panel',

                                            bodyPadding: '10 10 10 10',
                                            //width: '100%',
                                            layout: {
                                                type: 'table',
                                                columns: 2
                                                //tdAttrs: { style: 'padding: 2px;' }
                                            },
                                            items: [

                                                {
                                                    xtype: 'textfield',
                                                    id: 'txbNombreTema',
                                                    fieldLabel: '<b>Nombre del Tema</b>',
                                                    labelAlign: 'right',
                                                    labelWidth: 130,
                                                    width: 600,
                                                    colspan: 2,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    maskRe: /[áéíóúÁÉÍÓÚA-Za-zñÑ0-9():.,-_ ]/,
                                                    maxLength: 500,
                                                    enforceMaxLength: true,
                                                    value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temDescripcion,
                                                    listeners: {
                                                        change: function (field, newValue, oldValue) {
                                                            field.setValue(newValue.toUpperCase());
                                                        }
                                                    }
                                                },
                                            /*{
                                            xtype: 'checkbox',
                                            boxLabel: 'Tema Activo',
                                            id: 'TemaActivo',
                                            //margin: '20 200 20 110',
                                            //value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temActivo,
                                            //value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctActivo,
                                            allowBlank: true
                                            },*/
                                                {
                                                xtype: 'textfield',
                                                id: 'txbCodigoTema',
                                                fieldLabel: 'Código Tema',
                                                labelAlign: 'right',
                                                labelWidth: 130,
                                                width: 300,
                                                allowBlank: false,
                                                blankText: 'Campo Obligatorio',
                                                maskRe: /[A-Za-zñÑ0-9-]/,
                                                maxLength: 30,
                                                enforceMaxLength: true,
                                                value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temCodigo,
                                                listeners: {
                                                    change: function (field, newValue, oldValue) {
                                                        field.setValue(newValue.toUpperCase());
                                                    }
                                                }
                                            },
                                            /*{
                                            xtype: 'combo',
                                            id: 'cbOrdenTema',
                                            //store: Ext.getStore('Administracion.RegistroCertificacion.stOrdenTema').load(),
                                            emptyText: 'SELECCIONAR',
                                            triggerAction: 'all',
                                            queryMode: 'local',
                                            grow: true,
                                            editable: false,
                                            fieldLabel: "Orden del Tema",
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 500,
                                            valueField: 'idTema',
                                            displayField: 'ordenTema',
                                            // autoSelect: true,
                                            //  disabled: true,
                                            allowBlank: false,
                                            value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctOrden,
                                            blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                            msgTarget: 'side'
                                            },

                                            {
                                            xtype: 'textfield',
                                            id: 'txbNoPreguntasAleatoriasTema',
                                            fieldLabel: 'Núm. Preguntas aleatorias',
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 250,
                                            allowBlank: false,
                                            blankText: 'Campo Obligatorio',
                                            maxLength: 4,
                                            enforceMaxLength: true,
                                            maskRe: /[0-9]/,
                                            value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctAleatorias,
                                            listeners: {
                                            change: function (me, newValue, oldValue, eOpts) {

                                            /*  if (newValue > Ext.getCmp("txbNumeroDePreguntasExamen").getValue()) {
                                            Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>No. de preguntas aleatorias por Tema</b> no debe ser mayor al campo <b>Número de Preguntas del Examen</b>');
                                            }* /
                                            }
                                            }
                                            },
                                            {
                                            xtype: 'textfield',
                                            id: 'txbNoPreguntasCorrectasAprobarTema',
                                            fieldLabel: 'Núm. de preguntas correctas:',
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 250,
                                            allowBlank: false,
                                            blankText: 'Campo Obligatorio',
                                            maskRe: /[0-9]/,
                                            maxLength: 4,
                                            enforceMaxLength: true,
                                            value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctCorrectas,
                                            listeners: {
                                            change: function (me, newValue, oldValue, eOpts) {
                                            /* if (newValue > Ext.getCmp("txbNoPreguntasAleatoriasTema").getValue()) {
                                            Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>No. De preguntas correctas para aprobar el Tema</b> no debe ser mayor al campo <b>No. de preguntas aleatorias por Tema</b>');
                                            }* /
                                            }
                                            }
                                            },*/
                                                {
                                                xtype: 'textfield',
                                                id: 'txbTiempoExamenContestarTema',
                                                grow: true,
                                                editable: true,
                                                fieldLabel: 'Minutos para contestar el Tema',
                                                labelAlign: 'right',
                                                labelWidth: 200,
                                                width: 300,
                                                allowBlank: false,
                                                blankText: 'Campo Obligatorio',
                                                maskRe: /[0-9]/,
                                                maxLength: 4,
                                                enforceMaxLength: true,
                                                value: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctTiempo,
                                                listeners: {
                                                    change: function (field, newValue, oldValue) {
                                                        //   field.setValue(newValue.substring(0, 30));
                                                    }
                                                }
                                            }

                                            ],

                                            buttons: [

                                                        {
                                                            text: 'Aceptar', width: 150, handler: function () {
                                                                session();

                                                                if (Ext.getCmp('txbNombreTema').getRawValue() == (null || '')
                                                                  || Ext.getCmp('txbCodigoTema').getRawValue() == (null || '')
                                                                //|| Ext.getCmp('txbNoPreguntasAleatoriasTema').getRawValue() == 0
                                                                //|| Ext.getCmp('txbNoPreguntasCorrectasAprobarTema').getRawValue() == 0
                                                                  || Ext.getCmp('txbTiempoExamenContestarTema').getRawValue() == 0) {
                                                                    Ext.MessageBox.alert('SICER', 'Es necesario llenar todos los campos.');
                                                                    return false;
                                                                }

                                                                //var index = Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].rowIndex;

                                                                var index = Ext.getCmp('gridTemas').store.indexOf(Ext.getCmp('gridTemas').getSelection()[0]);

                                                                var retorno = false, aux = 0;
                                                                Ext.getCmp('gridTemas').store.each(function (recTema) {

                                                                    if (index != aux && Ext.getCmp('txbCodigoTema').getRawValue() == recTema.data.temCodigo)
                                                                    { retorno = true; }
                                                                    aux++;
                                                                });
                                                                if (retorno) {
                                                                    Ext.MessageBox.alert('SICER', 'Algún tema tiene el mismo código de tema');
                                                                    return;
                                                                }


                                                                var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                                                                //Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctOrden = Ext.getCmp('cbOrdenTema').getRawValue();
                                                                //Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temActivo = Ext.getCmp('TemaActivo').getValue();
                                                                //Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctActivo = Ext.getCmp('TemaActivo').getValue();
                                                                Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temDescripcion = Ext.getCmp('txbNombreTema').getRawValue();
                                                                Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temCodigo = Ext.getCmp('txbCodigoTema').getRawValue();
                                                                //Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctAleatorias = Ext.getCmp('txbNoPreguntasAleatoriasTema').getRawValue();
                                                                //Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctCorrectas = Ext.getCmp('txbNoPreguntasCorrectasAprobarTema').getRawValue();
                                                                Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctTiempo = Ext.getCmp('txbTiempoExamenContestarTema').getRawValue();
                                                                //Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.idTemaTemporal = 0;


                                                                var tiempo = 0
                                                                //,preguntas = 0,
                                                                //preguntasCorrectas = 0;
                                                                Ext.getCmp('gridTemas').store.each(function (recTema) {
                                                                    if (recTema.data.ctActivo) {
                                                                        tiempo += recTema.data.ctTiempo;
                                                                        //    preguntas += recTema.data.ctAleatorias;
                                                                        //    preguntasCorrectas += recTema.data.ctCorrectas;
                                                                    }
                                                                });

                                                                Ext.getCmp('txbTiempoTotalExamen').setValue(tiempo);
                                                                //Ext.getCmp('txbNumeroDePreguntasExamen').setValue(preguntas);
                                                                //Ext.getCmp('txbNumeroDePreguntasCorrectasParaAprobar').setValue(preguntasCorrectas);

                                                                Ext.getCmp('gridTemas').getView().refresh(true);

                                                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);

                                                                window.destroy();
                                                            }
                                                        },
                                                        {
                                                            text: 'Cancelar', width: 150, handler: function () {

                                                                session();
                                                                window.destroy();
                                                            }
                                                        }
                                                    ]
                                        }
                                    ]
                                 });
                                 window.show();
                             }
                             else {
                                 Ext.MessageBox.alert('SICER', 'Debes seleccionar un tema');
                             }
                         }
                     },
                     '-',
                    {
                        text: 'Agregar Función',
                        tooltip: 'Agrega una función al tema seleccionado',
                        id: 'tbarAgrFun',
                        handler: function () {
                            session();
                            if (Ext.getCmp('gridTemas').getSelectionModel().hasSelection()) {
                                var window = new Ext.Window({
                                    // id: 'window',
                                    title: 'Agregar Función',
                                    //width: 1000,
                                    resizable: false,
                                    modal: true,
                                    plain: true,
                                    layout: {
                                        type: 'vbox',
                                        align: 'center'
                                    },
                                    bodyPadding: '10 10 10 10',
                                    items: [
                                        {
                                            xtype: 'panel',

                                            bodyPadding: '10 10 10 10',
                                            //width: '100%',
                                            layout: {
                                                type: 'table',
                                                columns: 3
                                                //tdAttrs: { style: 'padding: 2px;' }
                                            },
                                            items: [
                                                {
                                                    xtype: 'textfield',
                                                    id: 'txbNombreFuncion',
                                                    fieldLabel: 'Nombre Función',
                                                    labelAlign: 'right',
                                                    labelWidth: 150,
                                                    width: 570,
                                                    colspan: 2,
                                                    maxLength: 500,
                                                    enforceMaxLength: true,
                                                    allowBlank: false,
                                                    blankText: 'Campo Obligatorio',
                                                    maskRe: /[áéíóúÁÉÍÓÚA-Za-zñÑ0-9():.,-_ ]/,
                                                    listeners: {
                                                        blur: function (field) {
                                                            field.setValue(field.getValue().toUpperCase());
                                                        }
                                                    }
                                                },
                                            /*{
                                            xtype: 'checkbox',
                                            boxLabel: 'Función Activa',
                                            id: 'FuncionActiva',
                                            //margin: '20 200 20 110',
                                            name: 'FuncionActiva',
                                            allowBlank: true
                                            },*/
                                                {
                                                xtype: 'textfield',
                                                id: 'txbCodigoFuncion',
                                                fieldLabel: 'Código Función',
                                                labelAlign: 'right',
                                                labelWidth: 130,
                                                width: 320,
                                                allowBlank: false,
                                                blankText: 'Campo Obligatorio',
                                                maskRe: /[A-Za-zñÑ0-9-]/,
                                                maxLength: 30,
                                                enforceMaxLength: true,
                                                listeners: {
                                                    change: function (field, newValue, oldValue) {
                                                        field.setValue(newValue.toUpperCase());
                                                    }
                                                }
                                            },
                                            /*{
                                            xtype: 'combo',
                                            id: 'cbOrdenFuncion',
                                            //store: Ext.getStore('Administracion.RegistroCertificacion.stOrdenTema').load(),
                                            emptyText: 'SELECCIONAR',
                                            triggerAction: 'all',
                                            queryMode: 'local',
                                            fieldLabel: "Orden de la Función",
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 350,
                                            colspan: 2,
                                            valueField: 'idTema',
                                            displayField: 'ordenTema',
                                            // autoSelect: true,
                                            //  disabled: true,
                                            allowBlank: false,
                                            blankText: 'Campo Obligatorio, Por favor seleccione una opción',
                                            msgTarget: 'side'
                                            },*/

        				                        {
        				                        xtype: 'textfield',
        				                        id: 'txbNoPreguntasAleatoriasFuncion',
        				                        fieldLabel: 'Núm. de <b>preguntas aleatorias</b> por Función',
        				                        labelAlign: 'right',
        				                        labelWidth: 150,
        				                        width: 250,
        				                        allowBlank: false,
        				                        blankText: 'Campo Obligatorio',
        				                        maskRe: /[0-9]/,
        				                        maxLength: 4,
        				                        enforceMaxLength: true,
        				                        listeners: {
        				                            change: function (me, newValue, oldValue, eOpts) {
        				                                /*if (newValue > gridView.getSelectionModel().getSelection()[0].data.ctAleatorias) {
        				                                Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>No. de preguntas aleatorias por Función</b> no debe ser mayor al campo <b>No. de Preguntas Aleatorias por Tema</b>');
        				                                }*/
        				                            }
        				                        }
        				                    },


        				                    {
        				                        xtype: 'textfield',
        				                        id: 'txbNoPreguntasCorrectasAprobarFuncion',
        				                        fieldLabel: 'Núm. <b>preguntas correctas</b> para aprobar la Función',
        				                        labelAlign: 'right',
        				                        labelWidth: 200,
        				                        width: 300,
        				                        //colspan: 2,
        				                        allowBlank: false,
        				                        blankText: 'Campo Obligatorio',
        				                        maskRe: /[0-9. ]/,
        				                        margin: '0 0 0 20',
        				                        maxLength: 4,
        				                        enforceMaxLength: true,
        				                        listeners: {
        				                            change: function (me, newValue, oldValue, eOpts) {
        				                                /* if (newValue > Ext.getCmp("txbNoPreguntasAleatoriasFuncion").getValue()) {
        				                                Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>No. De preguntas correctas para aprobar la Función</b> no debe ser mayor al campo <b>No. de preguntas aleatorias por Función</b>');
        				                                }*/
        				                            }
        				                        }
        				                    }
                                            /*,{
                                            xtype: 'textfield',
                                            id: 'txbTiempoExamenContestarFuncion',
                                            fieldLabel: 'Tiempo de examen para contestar la Función (minutos)',
                                            labelAlign: 'right',
                                            labelWidth: 200,
                                            width: 300,
                                            colspan: 3,
                                            allowBlank: false,
                                            blankText: 'Campo Obligatorio',
                                            maskRe: /[0-9. ]/,
                                            margin: '0 0 0 20',
                                            maxLength: 4,
                                            enforceMaxLength: true,
                                            listeners: {
                                            change: function (me, newValue, oldValue, eOpts) {
                                            /*   if (newValue > gridView.getSelectionModel().getSelection()[0].data.ctTiempo) {
                                            Ext.Msg.alert('SICER - ERROR DE VALIDACIÓN.', 'El campo <b>Tiempo de examen para contestar la Función (minutos)</b> no debe ser mayor al campo <b>Tiempo de examen para contestar el Tema (minutos)</b>');
                                            }* /
                                            }
                                            }
                                            }*/
                                            ]
                                        }
                                    ],
                                    buttons: [
                                                        {
                                                            text: 'Aceptar', width: 100, handler: function () {
                                                                session();

                                                                if (Ext.getCmp('txbNombreFuncion').getRawValue() == (null || '')
                                                                || Ext.getCmp('txbCodigoFuncion').getRawValue() == (null || '')
                                                                // || Ext.getCmp('cbOrdenFuncion').getRawValue() == 0
                                                                || Ext.getCmp('txbNoPreguntasAleatoriasFuncion').getRawValue() == 0
                                                                || Ext.getCmp('txbNoPreguntasCorrectasAprobarFuncion').getRawValue() == 0
                                                                //|| Ext.getCmp('txbTiempoExamenContestarFuncion').getRawValue() == 0
                                                                ) {
                                                                    Ext.MessageBox.alert('SICER', 'Es necesario llenar todos los campos.');
                                                                    return;
                                                                }

                                                                if (parseInt(Ext.getCmp('txbNoPreguntasAleatoriasFuncion').getRawValue()) < parseInt(Ext.getCmp('txbNoPreguntasCorrectasAprobarFuncion').getRawValue())) {
                                                                    Ext.MessageBox.alert('SICER', 'El número de preguntas que se mostrarán en el examen no debe </br>sobrepasar al número de preguntas correctas para aprobar.');
                                                                    return;
                                                                }

                                                                //var idFT = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].idFuncionTemporal;
                                                                //var idF = Ext.getCmp('gridFunciones').getSelectionModel().getSelection()[0].idFuncion;
                                                                var retorno = false;
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').each(function (rec) {
                                                                    if (//(rec.data.idFuncion != idF && rec.data.idFuncionTemporal != idFT) &&
                                                                         Ext.getCmp('txbCodigoFuncion').getRawValue() == rec.data.funCodigo) retorno = true;
                                                                });
                                                                if (retorno) {
                                                                    Ext.MessageBox.alert('SICER', 'Alguna función tiene el mismo código de función');
                                                                    return;
                                                                }

                                                                //var id = 'stFuncionesTema' + gridView.getSelectionModel().getSelection()[0].data.idTemporal;
                                                                var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();


                                                                contadorFunciones = contadorFunciones + 1;

                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').add({
                                                                    funNombre: Ext.getCmp('txbNombreFuncion').getValue(),
                                                                    funAleatorias: Ext.getCmp('txbNoPreguntasAleatoriasFuncion').getValue(),
                                                                    funCorrectas: Ext.getCmp('txbNoPreguntasCorrectasAprobarFuncion').getValue(),
                                                                    //funTiempo: Ext.getCmp('txbTiempoExamenContestarFuncion').getValue(),
                                                                    funTiempo: 0, //Lo dejo en Cero por que no se toma encuenta el tiempo para resolver cada función
                                                                    //funOrden: Ext.getCmp('cbOrdenFuncion').getRawValue(),
                                                                    funOrden: Ext.getCmp('gridFunciones').store.count() + 1,
                                                                    funCodigo: Ext.getCmp('txbCodigoFuncion').getValue(),
                                                                    tfActivo: 0,
                                                                    //tfActivo: Ext.getCmp('FuncionActiva').getValue(),
                                                                    idFuncionTemporal: contadorFunciones,
                                                                    idTemaTemporal: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.idTematemporal,
                                                                    idTema: Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.idTema
                                                                });


                                                                ///////////////////////Se actualiza GRID//////////////////////////////
                                                                Ext.data.StoreManager.lookup('stFuncionesTemporal').removeAll();
                                                                var funcionesTemporal = [];
                                                                Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stFunciones').each(function (rec) {
                                                                    if (rec.data.idTema == Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.idTema && rec.data.idTemaTemporal == Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.idTematemporal)
                                                                        funcionesTemporal.push(rec.copy());
                                                                });
                                                                Ext.data.StoreManager.lookup('stFuncionesTemporal').add(funcionesTemporal);
                                                                Ext.getCmp('gridFunciones').reconfigure(Ext.data.StoreManager.lookup('stFuncionesTemporal'));
                                                                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                                                /*
                                                                Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctAleatorias += Ext.getCmp('txbNoPreguntasAleatoriasFuncion').getValue();
                                                                Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.ctCorrectas += Ext.getCmp('txbNoPreguntasCorrectasAprobarFuncion').getValue();

                                                                var 
                                                                //tiempo = 0, //El tiempo no se modifica
                                                                preguntas = 0,
                                                                preguntasCorrectas = 0;
                                                                Ext.getCmp('gridTemas').store.each(function (recTema) {
                                                                if (recTema.data.temCodigo == Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0].data.temCodigo) {

                                                                }
                                                                //    if (recTema.data.ctActivo) {
                                                                //        tiempo += recTema.data.ctTiempo;
                                                                preguntas += recTema.data.ctAleatorias;
                                                                preguntasCorrectas += recTema.data.ctCorrectas;
                                                                //    }
                                                                });
                                                                // Ext.getCmp('txbTiempoTotalExamen').setValue(tiempo);
                                                                Ext.getCmp('txbNumeroDePreguntasExamen').setValue(preguntas);
                                                                Ext.getCmp('txbNumeroDePreguntasCorrectasParaAprobar').setValue(preguntasCorrectas);

                                                                */

                                                                Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);
                                                                window.destroy();
                                                            }
                                                        },
                                                        {
                                                            text: 'Cancelar', width: 100, handler: function () {
                                                                session();
                                                                window.destroy();
                                                            }
                                                        }
                                                    ]
                                });
                                window.show();
                            } else {
                                Ext.MessageBox.alert('SICER', 'Debes seleccionar un tema');
                            }
                        }

                    }, '-',
                    {
                        text: 'Subir',
                        tooltip: 'Cambia el orden en que aparecerá el tema seleccionado en el examen'
                        //    iconCls: 'remove',
                       , handler: function () {

                           var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                           var recordTema = Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0];

                           if (!recordTema) {
                               Ext.MessageBox.alert('SICER', 'Debes seleccionar un tema');
                               return;
                           }
                           var index = Ext.getCmp('gridTemas').store.indexOf(recordTema);
                           //  alert(index);
                           //if (direction < 0) {
                           index--;
                           if (index < 0) {
                               return;
                           }
                           /*} else {
                           index++;
                           if (index >= grid.getStore().getCount()) {
                           return;
                           }
                           }*/
                           Ext.getCmp('gridTemas').store.remove(recordTema);
                           Ext.getCmp('gridTemas').store.insert(index, recordTema);
                           Ext.getCmp('gridTemas').getSelectionModel().select(index, true);

                           var orden = 0;
                           Ext.getCmp('gridTemas').store.each(function (recTema) {
                               orden++;
                               Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas')
                                            .findRecord('temCodigo', recTema.data.temCodigo, 0, false, true, true).data.ctOrden = orden;
                           });
                           Ext.getCmp('gridTemas').getView().refresh(true);

                           Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);

                       }
                    }, '-',
                    {
                        text: 'Bajar',
                        tooltip: 'Cambia el orden en que aparecerá el tema seleccionado en el examen'
                        //    iconCls: 'remove',
                       , handler: function () {

                           var scrollPosition = Ext.getCmp('tabRegistroCertificacion').getScrollY();

                           var recordTema = Ext.getCmp('gridTemas').getSelectionModel().getSelection()[0];

                           if (!recordTema) {
                               Ext.MessageBox.alert('SICER', 'Debes seleccionar un tema');
                               return;
                           }
                           var index = Ext.getCmp('gridTemas').store.indexOf(recordTema);
                           //  alert(index);
                           //if (direction < 0) {
                           index++;
                           if (index >= Ext.getCmp('gridTemas').store.getCount()) {
                               return;
                           }

                           Ext.getCmp('gridTemas').store.remove(recordTema);
                           Ext.getCmp('gridTemas').store.insert(index, recordTema);
                           Ext.getCmp('gridTemas').getSelectionModel().select(index, true);

                           var orden = 0;
                           Ext.getCmp('gridTemas').store.each(function (recTema) {
                               orden++;
                               Ext.data.StoreManager.lookup('Administracion.RegistroCertificacion.stTemas')
                                            .findRecord('temCodigo', recTema.data.temCodigo, 0, false, true, true).data.ctOrden = orden;
                           });
                           Ext.getCmp('gridTemas').getView().refresh(true);

                           Ext.getCmp('tabRegistroCertificacion').scrollTo(0, scrollPosition, false);

                       }
                    }
                 ]
        })
        this.callParent(arguments);
    }
});
