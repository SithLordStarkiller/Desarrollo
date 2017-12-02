

Ext.define('app.model.catalogos.mdInstitucionExterna', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idInstitucionExterna', type: 'int' },
                { name: 'ieDescripcion', type: 'string' },
                { name: 'ieFechaModificacion', type: 'date' },
                { name: 'idUsuario', type: 'int' },
                { name: 'ieVigente', type: 'int' },
                { name: 'idDependenciaExterna', type: 'int' }
                
        ]
});