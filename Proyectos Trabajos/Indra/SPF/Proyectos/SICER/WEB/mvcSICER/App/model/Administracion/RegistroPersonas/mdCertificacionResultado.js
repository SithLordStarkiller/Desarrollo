Ext.define('app.model.Administracion.RegistroPersonas.mdCertificacionResultado', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idResultado', type: 'int' },
                { name: 'idCertificacionRegistro', type: 'int' },
                { name: 'idCertificacion', type: 'int' },
                { name: 'idRegistro', type: 'int' },
                { name: 'resCalificacion', type: 'float' },
                { name: 'resFechaResultado', type: 'date' },
                { name: 'resFechaModificacion', type: 'date' },
                { name: 'resVigente', type: 'bool' },
                { name: 'idUsuario', type: 'int' }
                

        ]
});