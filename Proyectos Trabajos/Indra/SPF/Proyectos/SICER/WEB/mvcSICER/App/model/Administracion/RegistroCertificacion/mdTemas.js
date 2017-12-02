Ext.define('app.model.Administracion.RegistroCertificacion.mdTemas', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'idTema', type: 'int', defaultValue: 0 },
                { name: 'idCertificacionTema', type: 'int' },
                { name: 'idCertificacion', type: 'int' },
                { name: 'temDescripcion', type: 'string' },
                { name: 'temCodigo', type: 'string' },
//                { name: 'temActivo', type: 'boolean' },
                { name: 'ctOrden', type: 'int' },
                { name: 'ctAleatorias', type: 'int' },
                { name: 'ctCorrectas', type: 'int' },
                { name: 'ctTiempo', type: 'int' },
                { name: 'ctActivo', type: 'boolean' },
                { name: 'idTematemporal', type: 'int', defaultValue: 0 }

        ]
});