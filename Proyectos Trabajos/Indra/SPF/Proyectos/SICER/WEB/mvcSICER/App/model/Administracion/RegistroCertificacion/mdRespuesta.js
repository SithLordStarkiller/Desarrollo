Ext.define('app.model.Administracion.RegistroCertificacion.mdRespuesta', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idRespuesta', type: 'int', defaultValue: 0 },
                { name: 'idRespuestaTemporal', type: 'int', defaultValue: 0 },
                { name: 'idPregunta', type: 'int', defaultValue: 0 },
                { name: 'idPreguntaTemporal', type: 'int', defaultValue: 0 },
                { name: 'resDescripcion', type: 'string' },
                { name: 'resExplicacion', type: 'string' },
                { name: 'resTipoArchivo', type: 'string' },
                { name: 'resNombreArchivo', type: 'string' },
                { name: 'resCorrecta', type: 'boolean' },
                { name: 'resActiva', type: 'boolean' },
                { name: 'imagen', type: 'string' },
                { name: 'identificadorImagen', type: 'string' }
            ]
});