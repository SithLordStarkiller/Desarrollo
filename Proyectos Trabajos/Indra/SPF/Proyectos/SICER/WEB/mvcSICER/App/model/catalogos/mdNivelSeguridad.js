Ext.define('app.model.catalogos.mdNivelSeguridad', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idNivelSeguridad', type: 'int' },
                { name: 'nsDescripcion', type: 'string' },
                { name: 'nsFechaModificacion', type: 'date' },
                { name: 'idUsuario', type: 'int' },
                { name: 'nsVigente', type: 'int' }
                
        ]
});