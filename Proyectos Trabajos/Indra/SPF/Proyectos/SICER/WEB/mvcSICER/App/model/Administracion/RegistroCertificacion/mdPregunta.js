Ext.define('app.model.Administracion.RegistroCertificacion.mdPregunta', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idPregunta', type: 'int', defaultValue: 0 },
                { name: 'idPreguntaTemporal', type: 'int', defaultValue: 0 },
                { name: 'idFuncion', type: 'int', defaultValue: 0 },
                { name: 'idFuncionTemporal', type: 'int', defaultValue: 0 },
                { name: 'preDescripcion', type: 'string' },
                { name: 'preCodigo', type: 'string' },
                { name: 'preObligatoria', type: 'boolean' },
                { name: 'preActiva', type: 'boolean' },
                { name: 'preTipoArchivo', type: 'string' },
                { name: 'preNombreArchivo', type: 'string' },
                { name: 'imagen', type: 'string' },
                { name: 'identificadorImagen', type: 'string' }
   

                ]
});
