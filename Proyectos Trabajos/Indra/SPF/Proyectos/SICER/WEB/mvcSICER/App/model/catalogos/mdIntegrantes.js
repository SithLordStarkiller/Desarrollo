Ext.define('app.model.catalogos.mdIntegrantes', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'idEmpleado', type: 'string' },
        { name: 'empPaterno', type: 'string' },
        { name: 'empMaterno', type: 'string' },
        { name: 'empNombre', type: 'string' },
        { name: 'empCURP', type: 'string' },
        { name: 'empRFC', type: 'string' },
        { name: 'empActivo', type: 'string' },
        { name: 'empNumero', type: 'int' },
        { name: 'cargo', type: 'string' },
        { name: 'jerDescripcion', type: 'string' }
    ]
});