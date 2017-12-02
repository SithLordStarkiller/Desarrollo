Ext.define('app.model.catalogos.mdMunicipios', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idPais', type: 'int' },
                { name: 'idEstado', type: 'int' },
                { name: 'idMunicipio', type: 'int' },
                { name: 'munDescripcion', type: 'string' }
        ]
});