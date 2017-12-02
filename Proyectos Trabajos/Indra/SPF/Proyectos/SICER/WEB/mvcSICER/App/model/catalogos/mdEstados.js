Ext.define('app.model.catalogos.mdEstados', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idPais', type: 'int' },
                { name: 'idEstado', type: 'int' },
                { name: 'estDescripcion', type: 'string' }
        ]
});