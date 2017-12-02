Ext.define('app.model.Administracion.RegistroPersonas.mdCertificacionRegistro', {
    extend: 'Ext.data.Model',
    fields: [
    //Registro de Certificacion
                {name: 'idCertificacionRegistro', type: 'int' },
                { name: 'idCertificacion', type: 'int' },
                { name: 'idRegistro', type: 'int' },

                { name: 'crFechaExamen', type: 'string' },
                { name: 'crHora', type: 'string' },

                { name: 'idLugarAplica', type: 'int' },
                { name: 'idEvaluador', type: 'int' },
                { name: 'crFechaModificacion', type: 'date' },
                { name: 'crVigente', type: 'int' },
                { name: 'idUsuario', type: 'int' },
                { name: 'idInstitucionAplicaExamen', type: 'int' },

                { name: 'certificacion', type: 'string' },
                { name: 'dfDomicilioLugarExamen', type: 'string' },


            //Evaluacion Certificacion
                {name: 'idEvaluacion', type: 'int' },
                { name: 'evalCalif', type: 'float' },
                { name: 'evaluacionDesc', type: 'string' },
                { name: 'folio', type: 'string' },
                { name: 'calificacionDesc', type: 'string' },
                { name: 'vigenciaDesc', type: 'string' },
                

                { name: 'idNivelSeguridad', type: 'int' },
                { name: 'idDependenciaExterna', type: 'int' },
                { name: 'idInstitucionExterna', type: 'int' },

                // { name: 'deDescripcion', type: 'string' },///hoy
                   {name: 'idZona', type: 'int' }, //////02-08-2016
                   {name: 'idServicio', type: 'int' }, ////02-08-2016
                   {name: 'idInstalacion', type: 'int' }, ////02-08-2016
                   {name: 'ZonDescripcion', type: 'string' },
                   { name: 'serDescripcion', type: 'string' },
                   { name: 'insNombre', type: 'string' },

                   { name: 'idCalificacion', type: 'int' },

                   { name: 'nsDescripcion', type: 'string' },
                   { name: 'deDescripcion', type: 'string' },
                   { name: 'ieDescripcion', type: 'string' },
                   { name: 'actualiza', type: 'int' }
        ]
});