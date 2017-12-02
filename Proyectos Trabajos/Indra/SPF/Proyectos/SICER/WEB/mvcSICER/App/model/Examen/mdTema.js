Ext.define('app.model.Examen.mdTema', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idCertificacionTema', type: 'int' },
                { name: 'idCertificacion', type: 'int' },
                { name: 'idTema', type: 'int' },
                { name: 'ctOrden', type: 'int' },
                { name: 'ctAleatorias', type: 'int' },
                { name: 'ctCorrectas', type: 'int' },
                { name: 'ctTiempo', type: 'int' },
                { name: 'temDescripcion', type: 'string' },
                { name: 'temCodigo', type: 'string' }
        ]
});