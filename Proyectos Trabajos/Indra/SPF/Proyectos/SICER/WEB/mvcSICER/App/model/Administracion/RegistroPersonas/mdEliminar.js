Ext.define('app.model.Administracion.RegistroPersonas.mdEliminar', {
    extend: 'Ext.data.Model',
    fields: [

                { name: 'idCertificacionRegistro', type: 'int' },
                { name: 'idCertificacion', type: 'int' },
                { name: 'idRegistro', type: 'int' },
                { name: 'idUsuario', type: 'int' }

                 , { name: 'elimino', type: 'int' }
        ]
});