Ext.define('app.model.catalogos.mdLugarAplicacion', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idLugarAplica', type: 'int' },
                { name: 'laDescripcion', type: 'string' },
                { name: 'laDomicilio', type: 'string' },
                { name: 'laFechaModificacion', type: 'date' },
                { name: 'idUsuario', type: 'int' },
                { name: 'laVigente', type: 'int' }
        ]
});