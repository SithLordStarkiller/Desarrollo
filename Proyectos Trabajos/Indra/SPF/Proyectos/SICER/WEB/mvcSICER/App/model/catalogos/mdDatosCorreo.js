Ext.define('app.model.catalogos.mdDatosCorreo', {
    extend: 'Ext.data.Model',
    fields: [

        { name: 'sender', type: 'string' },
        { name: 'asunto', type: 'string' },
        { name: 'texto', type: 'string' }

    ]
});