Ext.define('app.model.Administracion.RegistroCertificacion.mdFuncion', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idFuncion', type: 'int', },
                { name: 'idFuncionTemporal', type: 'int', },
                { name: 'idTema', type: 'int', },
                { name: 'idTemaTemporal', type: 'int', },
                { name: 'idTemaFuncion', type: 'int' },
                { name: 'funNombre', type: 'string' },
                { name: 'funAleatorias', type: 'int' },
                { name: 'funCorrectas', type: 'int' },
                { name: 'funTiempo', type: 'int' },
                { name: 'funOrden', type: 'int' },
                { name: 'funCodigo', type: 'string' },
                { name: 'tfActivo', type: 'boolean' }
                ]
});