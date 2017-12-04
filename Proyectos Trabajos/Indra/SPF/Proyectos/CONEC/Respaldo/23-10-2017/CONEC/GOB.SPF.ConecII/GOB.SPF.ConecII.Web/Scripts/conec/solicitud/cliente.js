

Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.Cliente = {
        Solicitantes: [],
        Contactos: [],
        Documentos: []
    };

    $(function () {

        // We can attach the `fileselect` event to all file inputs on the page
        $(document).on('change', ':file', function () {
            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
            $("#fileUrl").val(label);
            input.trigger('fileselect', [numFiles, label]);
        });

    });

    this.Numero = 0;
    this.urls = {
        clientes: this.getUrl('/Solicitud/ClientesConsulta'),
        cliente: this.getUrl('/Solicitud/Cliente'),
        guardar: this.getUrl('/Solicitud/ClienteGuardar'),
        cambiarestatus: this.getUrl('/Solicitud/ClienteCambiarEstatus'),
        buscar: this.getUrl('/Solicitud/ClienteConsultaCriterio'),
        intalaciones: this.getUrl('/Solicitud/ClienteInstalaciones'),
        autocompleterazonsocial: this.getUrl('/Solicitud/ClientesObtenerPorRazonSocial'),
        autocompletenombrecorto: this.getUrl('/Solicitud/ClientesObtenerPorNombreCorto'),
        regimenFiscal: this.getUrl('/Catalogo/RegimenFiscalConsulta'),
        sector: this.getUrl('/Catalogo/SectorConsulta'),
        clientedomicilio: this.getUrl('/Solicitud/Domicilio'),
        solicitantes: this.getUrl('/Solicitud/Solicitantes'),
        solicitante: this.getUrl('/Solicitud/Solicitante'),
        solicitanteguardar: this.getUrl('/Solicitud/SolicitanteGuardar'),
        contactos: this.getUrl('/Solicitud/Contactos'),
        contacto: this.getUrl('/Solicitud/Contacto'),
        contactoguardar: this.getUrl('/Solicitud/ContactoGuardar'),
        entidades: this.getUrl('/Catalogo/ObtenerEstado'),
        municipios: this.getUrl('/Catalogo/MunicipiosObtener/'),
        asentamiento: this.getUrl('/Catalogo/AsentamientosObtener'),
        documentos: this.getUrl('/Solicitud/ClienteDocumentos'),
        documento: this.getUrl('/Files/UploadFile')
    };
    this.controls = {
        grids: {
            cliente: new controls.Grid('divClientes', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: true })            
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    this.mensajes = {
        activaCliente: '¿Está seguro de que desea activar el cliente?',
        desactivaCliente: '¿Está seguro de que desea desactivar el cliente?',
        activaMensaje: '¿Está seguro de que desea activar el registro?',
        desactivaMensaje: '¿Está seguro de que desea desactivar el registro?',
        codigoPostalNotFound: 'No se localiza el código postal ingresado',
        guardarCliente: '¿Son correctos los datos?'
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Régimen fiscal', field: 'RegimenFiscal', css: 'col-xs-2' },
        { text: 'Sector', field: 'Sector', css: 'col-xs-2' },
        { text: 'Razón social (Dependencia o Empresa Privada)', field: 'RazonSocial', css: 'col-xs-4' },
        { text: 'Nombre corto', field: 'NombreCorto', css: 'col-xs-3' },
        { text: 'RFC', field: 'RFC', css: 'col-xs-2' },
        { text: '', field: 'Editar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } },
        { text: '', field: 'Instalaciones', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Location' } } }];

    this.controls.grids.cliente.init(columns);
    this.controls.grids.cliente.addListener("onPage", function (evt) {
        self.controls.grids.cliente.currentPage = evt.currentPage;
        self.load();
    });
    this.functions = {
        hideGrid: function (data) {
            $(".flip div.front").slideToggle()
            $(".flip div.back").html(data).fadeIn();
        },
        hideForm: function (data) {
            $(".flip div.back div").remove().slideToggle()
            $(".flip div.front").fadeIn();
        }
    };

    var success = function (data) {
        if (data.Result === 1) {
            var loadGrid = function (data) {
                self.controls.grids.cliente.reload([]);
                self.controls.grids.cliente.reload(data);
            };
            loadGrid(data.List);
            self.functions.hideForm();
        }
        else
        {
            self.showMessage(data.Message);
        }
    };
    var populateRegimenFiscal = function(data) {
        // Obtenemos el listado de regímenes fiscales
        $('<option value="0">Seleccione un régimen fiscal</option>').appendTo('#regimenFiscal');
        $.each(data.List, function (i, data) {
            var div_data = "<option value=" + data.Identificador + ">" + data.Name + "</option>";
            $(div_data).appendTo('#regimenFiscal');
        });
    };
    var populateSector = function (data) {
        // Obtenemos el listado de Sectores
        $('<option value="0">Seleccione un sector</option>').appendTo('#sector');
        $.each(data.List, function (i, data) {
            var div_data = "<option value=" + data.Identificador + ">" + data.Descripcion + "</option>";
            $(div_data).appendTo('#sector');
        });
    };

    $(this.controls.buttons.buscar).click(function () {
        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val() == 'undefined' ? null : $("input[name=estatus]:checked").val(),
                RazonSocial: $("input[name=razonSocial]").val(),
                NombreCorto: $("input[name=nombreCorto]").val(),
                IdRegimenFiscal:$("select[name=regimenFiscal]").val() ,
                IdSector:  $("select[name=sector]").val() 
            }, Paging: {
                CurrentPage: self.controls.grids.cliente.currentPage,
                Rows: self.controls.grids.cliente.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.cliente.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var model = {};
        var validacion = "";
        var tipoResultado = null;

        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#clienteForm");
                if ($("#clienteForm").valid()) {
                    // Extrae la informacion del domicilio fiscal
                    getDomicilioFiscal = function () {
                        domicilioFiscalData = {
                            Identificador: $("#IdentificadorDomicilioFiscal").val(),
                            IdCliente: $("#Identificador").val(),
                            IdPais: 79,
                            IdEstado: $('#Estados').val(),
                            IdMunicipio: $('#Municipios').val(),
                            IdAsentamiento: $('#Asentamientos').val(),
                            CodigoPostal: $("#CodigoPostal").val(),
                            Calle: $("#Calle").val(),
                            NoInterior: $("#NoInterior").val(),
                            NoExterior: $("#NoExterior").val()
                        };
                        return domicilioFiscalData;
                    };

                    var data1 = {
                        ObjectResult: {
                            Identificador: $("#Identificador").val(),
                            RazonSocial: $("#RazonSocial").val(),
                            NombreCorto: $("#NombreCorto").val(),
                            RFC: $("#RFC").val(),
                            IdRegimenFiscal: $("#IdRegimenFiscal").val(),
                            IdSector: $("#IdSector").val(),
                            DomicilioFiscal: getDomicilioFiscal(),
                            Contactos: self.Cliente.Contactos,
                            Solicitantes: self.Cliente.Solicitantes,
                            Documentos: self.Cliente.Documentos,
                            UniqueId: clienteModel.UniqueId,
                        }, Paging: {
                            CurrentPage: self.controls.grids.cliente.currentPage,
                            Rows: self.controls.grids.cliente.pageSize
                        }, Query: {
                            IsActive: $("input[name=estatus]:checked").val(),
                        }
                    };

                    var messageOptions = {
                        title: "Guardar",
                        aceptar: function () {
                            self.SendAjax('POST', self.urls.guardar, 'json', data1, success);
                        }
                    };

                    self.confirmacion(self.mensajes.guardarCliente, messageOptions);
                }
            });


           /*
                Inicializamos el objeto de cliente con sus solicitantes y contactos,
                para que estos sean cargados en la vista a partir del objeto model inicializado
                en este contexto Linea 123
            */
            if (clienteModel) {
                self.Cliente.Solicitantes = clienteModel.Solicitantes ? clienteModel.Solicitantes : [];
                
                self.Cliente.Contactos = clienteModel.Contactos ? clienteModel.Contactos : [];

                self.Cliente.Documentos = clienteModel.Documentos ? clienteModel.Documentos : [];
            }

            //self.loadControls();
            self.SendAjax('POST', self.urls.clientedomicilio, 'html', model, getPartialDomFisc);

            self.contactos();
            self.solicitantes();
            self.documentos();
        };

        var getPartialDomFisc = function (t) {
            $('#domicilio').html(t);

            if (clienteModel.Action !== 2) {
                var parameters = {
                    codigoPostal: {
                        urlDestino: self.urls.asentamiento,
                        field: 'Identificador',
                        text: 'Nombre',
                        value: 'Identificador',
                        control: document.getElementById("CodigoPostal"),
                        destino: document.getElementById("Asentamientos")
                    },
                    estado: {
                        urlDestino: self.urls.municipios,
                        field: 'Identificador',
                        text: 'Nombre',
                        value: 'Identificador',
                        control: document.getElementById("Estados"),
                        destino: document.getElementById("Municipios")
                    },
                    municipio: {
                        urlDestino: self.urls.asentamiento,
                        field: 'Identificador',
                        text: 'Nombre',
                        value: 'Identificador',
                        control: document.getElementById("Municipios"),
                        destino: document.getElementById("Asentamientos")
                    }
                };
                self.loadEventDomicilio(parameters);
                $("#Estados").change(function () {
                    self.loadMunicipios(this.value, parameters.estado.destino, parameters.estado.urlDestino);
                    self.loadAsentamientos(this.value, 0, parameters.municipio.destino, parameters.municipio.urlDestino);
                    $('#CodigoPostal').val('');
                });
                $("#Municipios").change(function () {
                    var estado = $("#Estados").val();
                    self.loadAsentamientos(estado, this.value, parameters.municipio.destino, parameters.municipio.urlDestino);
                    $('#CodigoPostal').val('');
                });
                $("#Asentamientos").change(function () {
                    var option = $(this.options[this.selectedIndex]);
                    if (option.data("option") !== undefined)
                        $("#CodigoPostal").val(option.data("option").CodigoPostal);
                    else
                        $("#CodigoPostal").val("");
                });

                /* Cargamos los combos con la información que se requiere y ejecutamos sus eventos change */
                if (clienteModel.DomicilioFiscal && clienteModel.DomicilioFiscal.Identificador != 0) {
                    values = {
                        codigoPostal: clienteModel.DomicilioFiscal.CodigoPostal,
                        IdEstado: clienteModel.DomicilioFiscal.IdEstado,
                        IdMunicipio: clienteModel.DomicilioFiscal.IdMunicipio,
                        IdAsentamiento: clienteModel.DomicilioFiscal.IdAsentamiento
                    };

                    self.ExecuteFillCombo(parameters.codigoPostal.urlDestino, values, parameters.codigoPostal.destino, function () {
                        $('#Asentamientos option[value=' + clienteModel.DomicilioFiscal.IdAsentamiento + ']').attr('selected', 'selected');
                    });
                }
            }
        };

        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.cliente;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                model = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.cliente;
                data = { model: { Action: evt.event } };
                this.model = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.cambiarestatus;
                var object = evt.dataRow;
                object.IsActive = !object.IsActive;
                data = {
                    Query: {
                        IsActive: $("input[name=estatus]:checked").val() == 'undefined' ? null : $("input[name=estatus]:checked").val(),
                        RazonSocial: $("input[name=razonSocial]").val(),
                        NombreCorto: $("input[name=nombreCorto]").val(),
                        IdRegimenFiscal: $("select[name=regimenFiscal]").val(),
                        IdSector: $("select[name=sector]").val()
                    },
                    ObjectResult: object,
                    Paging: {
                        CurrentPage: self.controls.grids.cliente.currentPage,
                        Rows: self.controls.grids.cliente.pageSize
                    }
                };
                messageOptions = {
                    title: evt.event == 'Inactive' ? "Activar" : "Desactivar",
                    aceptar: function () {
                        self.SendAjax('POST', url, 'json', data, success);
                    }
                };
                self.confirmacion(evt.event == 'Inactive' ? self.mensajes.activaCliente : self.mensajes.desactivaCliente, messageOptions);
                
                break;
            case 'Location':
                /* Aquí enviamos la pantalla */
                break;
        }
    });

    this.load();
    this.autocomplete('#razonSocial', this.urls.autocompleterazonsocial);
    this.autocomplete('#nombreCorto', this.urls.autocompletenombrecorto);

    self.SendAjax('POST', self.urls.regimenFiscal, 'json', { page: 1, rows: 15 }, populateRegimenFiscal);
    self.SendAjax('POST', self.urls.sector, 'json', {}, populateSector);
};



Ui.prototype.contactos = function () {
    var self = this;
    this.contactosArray = [];
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.controls.grids = $.extend(this.controls.grids, {
        contactos: new controls.Grid('clienteContactos', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: clienteModel.Action !== 2 }),
    }, true);

    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
       { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
       { text: 'Apellido Paterno', field: 'ApellidoPaterno', css: 'col-xs-2' },
       { text: 'Apellido Materno', field: 'ApellidoMaterno', css: 'col-xs-2' },
       { text: 'Cargo', field: 'Cargo', css: 'col-xs-3' },
       { text: 'Tipo', field: 'TipoContacto', css: 'col-xs-4' },
       { text: '', field: 'Editar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
       { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
       { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];

    /* Si el registro unicamente es para visualizar entonces no debe mostrar los campos de editar y activar y desactivar solo el de ver detalle */
    if (clienteModel.Action === 2) {
        columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
           { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
           { text: 'Apellido Paterno', field: 'ApellidoPaterno', css: 'col-xs-2' },
           { text: 'Apellido Materno', field: 'ApellidoMaterno', css: 'col-xs-2' },
           { text: 'Cargo', field: 'Cargo', css: 'col-xs-3' },
           { text: 'Tipo', field: 'TipoContacto', css: 'col-xs-4' },
           { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } }];
    }

    this.controls.grids.contactos.init(columns);
    this.controls.grids.contactos.loadRows([]);

    if (self.Cliente)
        if (self.Cliente.Contactos)
            this.controls.grids.contactos.loadRows(self.Cliente.Contactos);
    
    this.controls.grids.contactos.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        var dataRow = undefined;
        var getPartial = function (data) {
            var divContacto = document.getElementById("partialContacto");

            $(divContacto).html('');
            $(divContacto).append(data);
            $(divContacto).toggle("slow");//.addClass("display-form").removeClass("hide-form");
            $(self.controls.grids.contactos.grid).toggle("slow");//.addClass("hide-form");
            self.removeValidations(divContacto);
            self.loadTelefonosCorreos(dataRow);
                
            $('#btnBackContacto').click(function () {
                $(divContacto).toggle("slow");//.addClass("hide-form").removeClass("display-form");
                $(self.controls.grids.contactos.grid).toggle("slow");//.removeClass("hide-form").addClass("display-form");
            });
            
            $('#btnSaveContacto').click(function () {
                $.validator.unobtrusive.parse("#formContacto");
                if (!$("#formContacto").valid()) return;
                var data = {
                    Identificador: $("#Identificador", "#formContacto").val(),
                    Nombre: $("#Nombre", "#formContacto").val(),
                    ApellidoPaterno: $("#ApellidoPaterno", "#formContacto").val(),
                    ApellidoMaterno: $("#ApellidoMaterno", "#formContacto").val(),
                    Cargo: $("#Cargo", "#formContacto").val(),
                    IdTipoPersona: 2,
                    IdTipoContacto: $("#IdTipoContacto", "#formContacto").val(),
                    TipoContacto: $("#IdTipoContacto option:selected", "#formContacto").text(),
                    IsActive: $("#IsActive", "#formContacto").val() == 'True',
                    Numero: self.Cliente.Contactos.length === 0 ? 1 : self.Cliente.Contactos.length + 1,
                    Telefonos: self.Contacto.Telefonos,
                    Correos: self.Contacto.Correos
                };

                var contactos = $.grep(self.Cliente.Contactos, function (element, index) {
                    return (evt.dataRow !== undefined) ? (element.Numero !== evt.dataRow.Numero) : true;
                });

                if (contactos === undefined)
                    self.Cliente.Contactos.push(data);
                else {
                    self.Cliente.Contactos = contactos;
                    if (evt.dataRow !== undefined)
                        data.Numero = evt.dataRow.Numero;
                    self.Cliente.Contactos.push(data);
                }
                var order = function (a,b) {
                    if (a.Nombre < b.Nombre)
                        return -1
                    if (a.Nombre > b.Nombre)
                        return 1;
                    return 0;
                };
                self.Cliente.Contactos.sort(order);
                self.controls.grids.contactos.reload(self.Cliente.Contactos);
                $(divContacto).toggle("slow");
                $(self.controls.grids.contactos.grid).toggle("slow");

                self.Contacto = { Telefonos: [], Correos: [] };
            });
        };

        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.contacto;
                data = { contacto: $.extend(evt.dataRow, { Action: evt.event }) };
                model = data;
                dataRow = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.contacto;
                data = { model: { Action: evt.event } };
                this.model = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                var messageOptions = {
                    title: !evt.dataRow.IsActive ? "Activar" : "Desactivar",
                    aceptar: function () {
                        var findElement = function (item) {
                            return item.Numero == evt.dataRow.Numero;
                        };
                        var contactoIndex = self.Cliente.Contactos.findIndex(findElement);
                        self.Cliente.Contactos[contactoIndex].IsActive = !self.Cliente.Contactos[contactoIndex].IsActive;
                        self.controls.grids.contactos.loadRows([]);
                        self.controls.grids.contactos.loadRows(self.Cliente.Contactos);
                    }
                };

                self.confirmacion(evt.dataRow.IsActive ? self.mensajes.desactivaMensaje : self.mensajes.activaMensaje, messageOptions);

                break;
        }
    });
};

Ui.prototype.loadTelefonosCorreos = function (data) {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.Contacto = data === undefined ? { Telefonos: [], Correos: [] } : data.contacto === undefined ? { Telefonos: [], Correos: [] } : data.contacto;

    this.controls.grids = $.extend(this.controls.grids, {
            telefonos: new controls.Grid('clienteContactosTelefonos', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: false }),
            correos: new controls.Grid('clienteContactosCorreos', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: false })
    }, true);
     
    var columnsTelefonos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
       { text: 'Tipo', field: 'TipoTelefono', css: 'col-xs-2' },
       { text: 'Numero', field: 'Numero', css: 'col-xs-2' },
       { text: 'Extension', field: 'Extension', css: 'col-xs-4' },
       { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];

    var columnsCorreos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
       { text: 'Correo', field: 'CorreoElectronico', css: 'col-xs-2' },
       { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];

    /* Si el registro unicamente es para visualizar entonces no debe mostrar los campos de editar y activar y desactivar solo el de ver detalle */
    if(data)
        if (data.contacto.Action === "View") {
            columnsTelefonos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
               { text: 'Tipo', field: 'TipoTelefono', css: 'col-xs-4' },
               { text: 'Numero', field: 'Numero', css: 'col-xs-4' },
               { text: 'Extension', field: 'Extension', css: 'col-xs-3' }];

            columnsCorreos = [{ text: 'No.', iskey: true, css: 'col-xs-2', visible: true },
               { text: 'Correo', field: 'CorreoElectronico', css: 'col-xs-10' }];
        }

    this.controls.grids.telefonos.init(columnsTelefonos);
    this.controls.grids.telefonos.loadRows(self.Contacto.Telefonos === undefined ? [] : self.Contacto.Telefonos);
        
    this.controls.grids.correos.init(columnsCorreos);
    this.controls.grids.correos.loadRows(self.Contacto.Correos === undefined ? [] : self.Contacto.Correos);

    this.controls.grids.telefonos.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        switch (evt.event) {

            case 'Active':
            case 'Inactive':
                var messageOptions = {
                    title: !evt.dataRow.IsActive ? "Activar" : "Desactivar",
                    aceptar: function () {
                        var findElement = function (item) {
                            return item.Indice == evt.dataRow.Indice;
                        };
                        var telefonoIndex = self.Contacto.Telefonos.findIndex(findElement);
                        self.Contacto.Telefonos[telefonoIndex].IsActive = !self.Contacto.Telefonos[telefonoIndex].IsActive;
                        self.controls.grids.telefonos.reload(self.Contacto.Telefonos);
                    }
                };

                self.confirmacion(evt.dataRow.IsActive ? self.mensajes.desactivaMensaje : self.mensajes.activaMensaje, messageOptions);
                break;
        }
    });
    this.controls.grids.correos.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        switch (evt.event) {

            case 'Active':
            case 'Inactive':
                var messageOptions = {
                    title: !evt.dataRow.IsActive ? "Activar" : "Desactivar",
                    aceptar: function () {
                        var findElement = function (item) {
                            return item.Indice == evt.dataRow.Indice;
                        };
                        var correoIndex = self.Contacto.Correos.findIndex(findElement);
                        self.Contacto.Correos[correoIndex].IsActive = !self.Contacto.Correos[correoIndex].IsActive;
                        self.controls.grids.correos.reload(self.Contacto.Correos);
                    }
                };

                self.confirmacion(evt.dataRow.IsActive ? self.mensajes.desactivaMensaje : self.mensajes.activaMensaje, messageOptions);
                break;
        }
    });
    $("#partialContacto #btnTelefono").click(function () {
        $.validator.unobtrusive.parse($("#formTelefono", "#partialContacto"));
        if (!$("#formTelefono", "#partialContacto").valid()) return;

        var data = {
            Indice: self.Contacto.Telefonos.length + 1,
            IdTipoTelefono: $("#IdTipoTelefono", "#partialContacto").val(),
            TipoTelefono: $("#IdTipoTelefono option:selected", "#partialContacto").text(),
            Numero: $("#Numero", "#partialContacto").val(),
            Extension: $("#Extension", "#partialContacto").val(),
            IsActive : true
        };
        self.Contacto.Telefonos.push(data);
        self.controls.grids.telefonos.reload(self.Contacto.Telefonos);
        $("#IdTipoTelefono option:selected", "#partialContacto").removeAttr("selected");
        $("#IdTipoTelefono:first-child", "#partialContacto").attr("selected", "selected");
        $("#Numero", "#partialContacto").val("");
        $("#Extension", "#partialContacto").val("");
    });

    $("#partialContacto #btnCorreo").click(function () {
        $.validator.unobtrusive.parse($("#formCorreo", "#partialContacto"));
        if (!$("#formCorreo", "#partialContacto").valid()) return;

        var data = {
            Indice: self.Contacto.Correos.length + 1,
            CorreoElectronico: $("#CorreoElectronico", "#partialContacto").val(),
            IsActive: true
        };
        self.Contacto.Correos.push(data);
        self.controls.grids.correos.reload(self.Contacto.Correos);
        $("#CorreoElectronico", "#partialContacto").val("");
    });
};



Ui.prototype.solicitantes = function () {
    var self = this;
    this.contactosArray = [];
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.controls.grids = $.extend(this.controls.grids, {
        solicitantes: new controls.Grid('clienteSolicitantes', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: clienteModel.Action !== 2 }),
    }, true);

    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
       { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
       { text: 'Apellido Paterno', field: 'ApellidoPaterno', css: 'col-xs-2' },
       { text: 'Apellido Materno', field: 'ApellidoMaterno', css: 'col-xs-2' },
       { text: 'Cargo', field: 'Cargo', css: 'col-xs-3' },
       { text: '', field: 'Editar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
       { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
       { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];

    /* Si el registro unicamente es para visualizar entonces no debe mostrar los campos de editar y activar y desactivar solo el de ver detalle */
    if (clienteModel.Action === 2) {
        columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
           { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
           { text: 'Apellido Paterno', field: 'ApellidoPaterno', css: 'col-xs-2' },
           { text: 'Apellido Materno', field: 'ApellidoMaterno', css: 'col-xs-2' },
           { text: 'Cargo', field: 'Cargo', css: 'col-xs-3' },
           { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } }];
    }

    this.controls.grids.solicitantes.init(columns);
    this.controls.grids.solicitantes.loadRows([]);

    if (self.Cliente)
        if (self.Cliente.Solicitantes)
            this.controls.grids.solicitantes.loadRows(self.Cliente.Solicitantes);

    this.controls.grids.solicitantes.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        var dataRow = undefined;
        var getPartial = function (data) {
            var divSolicitante = document.getElementById("partialSolicitante");
            $(divSolicitante).html('');
            $(divSolicitante).append(data);
            $(divSolicitante).toggle("slow");
            $(self.controls.grids.solicitantes.grid).toggle("slow");
            self.removeValidations(divSolicitante);
            self.loadTelefonosCorreosSolicitante(dataRow);

            $('#btnBackSolicitante').click(function () {
                $(divSolicitante).toggle("slow");
                $(self.controls.grids.solicitantes.grid).toggle("slow");
            });

            $('#btnSaveSolicitante').click(function () {
                $.validator.unobtrusive.parse("#formSolicitante");
                if (!$("#formSolicitante").valid()) return;
                var data = {
                    Identificador: $("#Identificador", "#formSolicitante").val(),
                    Nombre: $("#Nombre", "#formSolicitante").val(),
                    ApellidoPaterno: $("#ApellidoPaterno", "#formSolicitante").val(),
                    ApellidoMaterno: $("#ApellidoMaterno", "#formSolicitante").val(),
                    Cargo: $("#Cargo", "#formSolicitante").val(),
                    IdTipoPersona: 1,
                    IsActive: $("#IsActive", "#formSolicitante").val() == 'True',
                    Numero: self.Cliente.Solicitantes.length === 0 ? 1 : self.Cliente.Solicitantes.length + 1,
                    Telefonos: self.Solicitante.Telefonos,
                    Correos: self.Solicitante.Correos
                };

                var solicitantes = $.grep(self.Cliente.Solicitantes, function (element, index) {
                    return (evt.dataRow !== undefined) ? (element.Numero !== evt.dataRow.Numero) : true;
                });

                if (solicitantes === undefined)
                    self.Cliente.Solicitantes.push(data);
                else {
                    self.Cliente.Solicitantes = solicitantes;
                    if (evt.dataRow !== undefined)
                        data.Numero = evt.dataRow.Numero;
                    self.Cliente.Solicitantes.push(data);
                }
                var order = function (a, b) {
                    if (a.Nombre < b.Nombre)
                        return -1
                    if (a.Nombre > b.Nombre)
                        return 1;
                    return 0;
                };
                self.Cliente.Solicitantes.sort(order);
                self.controls.grids.solicitantes.reload(self.Cliente.Solicitantes);
                $(divSolicitante).toggle("slow");
                $(self.controls.grids.solicitantes.grid).toggle("slow");

                self.Solicitante = { Telefonos: [], Correos: [] };
            });
        };

        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.solicitante;
                data = { solicitante: $.extend(evt.dataRow, { Action: evt.event }) };
                model = data;
                dataRow = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.solicitante;
                data = { model: { Action: evt.event } };
                this.model = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                var messageOptions = {
                    title: !evt.dataRow.IsActive ? "Activar" : "Desactivar",
                    aceptar: function () {
                        var findElement = function (item) {
                            return item.Numero == evt.dataRow.Numero;
                        };
                        var solicitanteIndex = self.Cliente.Solicitantes.findIndex(findElement);
                        self.Cliente.Solicitantes[solicitanteIndex].IsActive = !self.Cliente.Solicitantes[solicitanteIndex].IsActive;
                        self.controls.grids.solicitantes.loadRows([]);
                        self.controls.grids.solicitantes.loadRows(self.Cliente.Solicitantes);
                    }
                };

                self.confirmacion(evt.dataRow.IsActive ? self.mensajes.desactivaMensaje : self.mensajes.activaMensaje, messageOptions);
                break;
        }
    });
};

Ui.prototype.loadTelefonosCorreosSolicitante = function (data) {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.Solicitante = data === undefined ? { Telefonos: [], Correos: [] } : data.solicitante === undefined ? { Telefonos: [], Correos: [] } : data.solicitante;

    this.controls.grids = $.extend(this.controls.grids, {
        telefonosSolicitante: new controls.Grid('clienteSolicitantesTelefonos', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: false }),
        correosSolicitante: new controls.Grid('clienteSolicitantesCorreos', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: false })
    }, true);

    var columnsTelefonosSolicitantes = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
       { text: 'Tipo', field: 'TipoTelefono', css: 'col-xs-2' },
       { text: 'Numero', field: 'Numero', css: 'col-xs-2' },
       { text: 'Extension', field: 'Extension', css: 'col-xs-4' },
       { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    
    var columnsCorreosSolicitantes = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
       { text: 'Correo', field: 'CorreoElectronico', css: 'col-xs-2' },
       { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];

    /* Si el registro unicamente es para visualizar entonces no debe mostrar los campos de editar y activar y desactivar solo el de ver detalle */
    if (data)
        if (data.solicitante.Action === "View") {
            columnsTelefonosSolicitantes = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
               { text: 'Tipo', field: 'TipoTelefono', css: 'col-xs-4' },
               { text: 'Numero', field: 'Numero', css: 'col-xs-4' },
               { text: 'Extension', field: 'Extension', css: 'col-xs-3' }];

            columnsCorreosSolicitantes = [{ text: 'No.', iskey: true, css: 'col-xs-2', visible: true },
               { text: 'Correo', field: 'CorreoElectronico', css: 'col-xs-10' }];
        }

    this.controls.grids.telefonosSolicitante.init(columnsTelefonosSolicitantes);
    this.controls.grids.telefonosSolicitante.loadRows(self.Solicitante.Telefonos === undefined ? [] : self.Solicitante.Telefonos);

    this.controls.grids.correosSolicitante.init(columnsCorreosSolicitantes);
    this.controls.grids.correosSolicitante.loadRows(self.Solicitante.Correos === undefined ? [] : self.Solicitante.Correos);

    this.controls.grids.telefonosSolicitante.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        switch (evt.event) {

            case 'Active':
            case 'Inactive':
                var messageOptions = {
                    title: !evt.dataRow.IsActive ? "Activar" : "Desactivar",
                    aceptar: function () {
                        var findElement = function (item) {
                            return item.Indice == evt.dataRow.Indice;
                        };
                        var telefonoIndex = self.Solicitante.Telefonos.findIndex(findElement);
                        self.Solicitante.Telefonos[telefonoIndex].IsActive = !self.Solicitante.Telefonos[telefonoIndex].IsActive;
                        self.controls.grids.telefonosSolicitante.reload(self.Solicitante.Telefonos);
                    }
                };

                self.confirmacion(evt.dataRow.IsActive ? self.mensajes.desactivaMensaje : self.mensajes.activaMensaje, messageOptions);
                break;
        }
    });
    this.controls.grids.correosSolicitante.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        switch (evt.event) {
            case 'Active':
            case 'Inactive':
                var messageOptions = {
                    title: !evt.dataRow.IsActive ? "Activar" : "Desactivar",
                    aceptar: function () {
                        var findElement = function (item) {
                            return item.Indice == evt.dataRow.Indice;
                        };
                        var correoIndex = self.Solicitante.Correos.findIndex(findElement);
                        self.Solicitante.Correos[correoIndex].IsActive = !self.Solicitante.Correos[correoIndex].IsActive;
                        self.controls.grids.correosSolicitante.reload(self.Solicitante.Correos);
                    }
                };

                self.confirmacion(evt.dataRow.IsActive ? self.mensajes.desactivaMensaje : self.mensajes.activaMensaje, messageOptions);
                break;
        }
    });
    $("#partialSolicitante #btnTelefono").click(function () {
        $.validator.unobtrusive.parse($("#formTelefono", "#partialSolicitante"));
        if (!$("#formTelefono", "#partialSolicitante").valid()) return;

        var data = {
            Indice: self.Solicitante.Telefonos.length + 1,
            IdTipoTelefono: $("#IdTipoTelefono", "#partialSolicitante").val(),
            TipoTelefono: $("#IdTipoTelefono option:selected", "#partialSolicitante").text(),
            Numero: $("#Numero", "#partialSolicitante").val(),
            Extension: $("#Extension", "#partialSolicitante").val(),
            IsActive: true
        };
        self.Solicitante.Telefonos.push(data);
        self.controls.grids.telefonosSolicitante.reload(self.Solicitante.Telefonos);

        $("#IdTipoTelefono option:selected", "#partialSolicitante").removeAttr("selected");
        $("#IdTipoTelefono:first-child", "#partialSolicitante").attr("selected", "selected");
        $("#Numero", "#partialSolicitante").val("");
        $("#Extension", "#partialSolicitante").val("");
    });

    $("#partialSolicitante #btnCorreo").click(function () {
        $.validator.unobtrusive.parse($("#formCorreo", "#partialSolicitante"));
        if (!$("#formCorreo", "#partialSolicitante").valid()) return;

        var data = {
            Indice: self.Solicitante.Correos.length + 1,
            CorreoElectronico: $("#CorreoElectronico", "#partialSolicitante").val(),
            IsActive: true
        };
        self.Solicitante.Correos.push(data);
        self.controls.grids.correosSolicitante.reload(self.Solicitante.Correos);
        $("#CorreoElectronico", "#partialSolicitante").val("");
    });
};



/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.cliente.currentPage;
    var rows = self.controls.grids.cliente.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.cliente.reload([]);
        if (data.Result) {
            self.controls.grids.cliente.currentPage = data.Paging.CurrentPage;
            self.controls.grids.cliente.pages = data.Paging.Pages;
            self.controls.grids.cliente.reload(data.List);
        }

    };
    
    var data = {
        page: page, rows: rows,
        Query: {
            IsActive: $("input[name=estatus]:checked").val(),
        },
    };

    this.SendAjax('POST', self.urls.clientes, 'json', data, loadGrid);
};

Ui.prototype.loadControls = function () {
    var self = this;
    var destino = "";
    var execute = function (data) {
        if (data.Result === 1) {
            self.loadCombo(data.List, destino)
        }
    };
};

Ui.prototype.autocomplete = function (selector, url, $function) {
    if ($(selector).length)
        $(selector).autocomplete({
            source: url,
            minLength: 3,
            scroll: true,
            select: $function
        });
};

Ui.prototype.documentos = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.controls.grids = $.extend(this.controls.grids, {
        documentos: new controls.Grid('clienteDocumentos', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: false }),
    }, true);

    var columns = [
        { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];

    /* Si el registro unicamente es para visualizar entonces no debe mostrar los campos de editar y activar y desactivar solo el de ver detalle */
    if (clienteModel.Action === 2) {
        columns = [
            { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
            { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' }];
    }

    this.controls.grids.documentos.init(columns);
    this.controls.grids.documentos.loadRows([]);

    if (self.Cliente)
        if (self.Cliente.Documentos)
            this.controls.grids.documentos.loadRows(self.Cliente.Documentos);


    this.controls.grids.documentos.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();

        switch (evt.event) {
            case 'Active':
            case 'Inactive':
                var messageOptions = {
                    title: !evt.dataRow.IsActive ? "Activar" : "Desactivar",
                    aceptar: function () {
                        var findElement = function (item) {
                            return item.Numero == evt.dataRow.Numero;
                        };
                        var documentoIndex = self.Cliente.Documentos.findIndex(findElement);
                        self.Cliente.Documentos[documentoIndex].IsActive = !self.Cliente.Documentos[documentoIndex].IsActive;
                        self.controls.grids.documentos.loadRows([]);
                        self.controls.grids.documentos.loadRows(self.Cliente.Documentos);
                    }
                };

                self.confirmacion(evt.dataRow.IsActive ? self.mensajes.desactivaMensaje : self.mensajes.activaMensaje, messageOptions);
                break;
        }
    });

    var jqXHRData = undefined;
    var getPartial = function (data) {
        if (data.length) {
            this.controls.grids.documentos.loadRows(data);
        }

        'use strict';

        $('#documento').fileupload({
            formData: { Directory: clienteModel.UniqueId },
            maxFileSize: 5000000000,
            url: self.urls.documento,
            dataType: 'json',
            add: function (e, data) {
                jqXHRData = data;
                if (jqXHRData) {
                    jqXHRData.submit();
                }
                //return false;
            },
            done: function (event, data) {
                if (data.result.isUploaded) {
                    data.result.documento.Numero = self.Cliente.Documentos.length + 1;
                    self.Cliente.Documentos.push(data.result.documento);

                    self.controls.grids.documentos.loadRows([]);
                    self.controls.grids.documentos.loadRows(self.Cliente.Documentos);

                    //$('#documento').val('');
                    //$("#fileUrl").val("No hay archivo seleccionado");
                }
                else {

                }
                //alert(data.result.message);
            },
            fail: function (event, data) {
                if (data.files[0].error) {
                    alert(data.files[0].error);
                }
            }
        });

        $(document).on('change', ':file', function () {
            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
            $("#fileUrl").val(label);
            input.trigger('fileselect', [numFiles, label]);
        });
    };

    var data = { ClienteId: clienteModel.Identificador };
    self.SendAjax('POST', self.urls.documentos, 'json', data, getPartial);
};


function init() {
    var ui = new Ui();
    ui.init();
};

init();