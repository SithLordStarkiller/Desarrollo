Ext.define('app.model.Administracion.RegistroPersonas.mdBusqueda', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idEmpleado', type: 'string' },
                { name: 'idRegistro', type: 'int' },
                { name: 'empPaterno', type: 'string' },
                { name: 'empMaterno', type: 'string' },
                { name: 'empNombre', type: 'string' },
                { name: 'empCURP', type: 'string' },
                { name: 'empRFC', type: 'string' },
                { name: 'empNumero', type: 'string' },
                { name: 'participante', type: 'string' }

        ]
});