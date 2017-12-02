Ext.define('app.model.catalogos.mdInstAplicaExamen', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idInstitucionAplicaExamen', type: 'int' },
                { name: 'iaeDescripcion', type: 'string' },
                { name: 'iaeFechaModificacion', type: 'date' },
                { name: 'idUsuario', type: 'int' },
                { name: 'iaeVigente', type: 'int' }
                
        ]
});