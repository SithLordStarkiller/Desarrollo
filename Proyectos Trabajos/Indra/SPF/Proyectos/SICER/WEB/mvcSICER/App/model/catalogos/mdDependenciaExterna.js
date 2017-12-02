Ext.define('app.model.catalogos.mdDependenciaExterna', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idDependenciaExterna', type: 'int' },
                { name: 'deDescripcion', type: 'string' },
                { name: 'deFechaModificacion', type: 'date' },
                { name: 'idUsuario', type: 'int' },
                { name: 'deVigente', type: 'int' },
                { name: 'idNivelSeguridad', type: 'int' } 
        ]
});