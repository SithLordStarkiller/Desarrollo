

Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
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
        clientedomiciliofiscal: this.getUrl('/Solicitud/DomicilioFiscal'),
        solicitantes: this.getUrl('/Solicitud/Solicitantes'),
        solicitante: this.getUrl('/Solicitud/Solicitante'),
        solicitanteguardar: this.getUrl('/Solicitud/SolicitanteGuardar'),
        contactos: this.getUrl('/Solicitud/Contactos'),
        contacto: this.getUrl('/Solicitud/Contacto'),
        contactoguardar: this.getUrl('/Solicitud/ContactoGuardar'),
        entidades: this.getUrl('/Catalogo/ObtenerEstado'),
        municipios: this.getUrl('/Catalogo/MunicipiosObtener'),
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
                            DomicilioFiscal: getDomicilioFiscal()
                        }, Paging: {
                            CurrentPage: self.controls.grids.cliente.currentPage,
                            Rows: self.controls.grids.cliente.pageSize
                        }, Query: {
                            IsActive: $("input[name=estatus]:checked").val(),
                        }
                    };

                    self.SendAjax('POST', self.urls.guardar, 'json', data1, success);
                }
            });
            self.loadControls();

            // Una vez que se ha cargado la vista para la edición o consulta del cliente
            // se deben cargar las vistas parciales de Solicitantes y Contactos
            self.SendAjax('POST', self.urls.clientedomiciliofiscal, 'html', model, getPartialDomFisc);

            var solicitanteList = new solicitantesHnd(self, 'POST', self.urls, 'html', model.model, '#clienteSolicitantes');
            solicitanteList.ajaxCall(self.urls.solicitantes);

            var contactosList = new contactosHnd(self, 'POST', self.urls, 'html', model.model, '#clienteContactos');
            contactosList.ajaxCall(self.urls.contactos);
        };

        var getPartialDomFisc = function (t) {
            $('#clienteDomicilio').html(t);
            // Hay que cargar los eventos de los combos que hacen cascada
            // Cargamos los combos

            var domicilioProxy = new domicilioHnd(self, {
                IdEstado: domicilioFiscalModel.IdEstado,
                IdMunicipio: domicilioFiscalModel.IdMunicipio,
                IdAsentamiento: domicilioFiscalModel.IdAsentamiento,
                CodigoPostal: domicilioFiscalModel.CodigoPostal
            }, {
                    codigoPostal: '#CodigoPostal',
                    estado: '#Estados',
                    municipio: '#Municipios',
                    asentamiento: '#Asentamientos'
            });

            domicilioProxy.init();
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
                this.model = $.toJSON(data);
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.cambiarestatus;
                var object = evt.dataRow;
                object.IsActive = !object.IsActive;
                data = {
                    Query: {
                        IsActive: $("input[name=estatus]:checked").val(),
                    },
                    ObjectResult: object,
                    Paging: {
                        CurrentPage: self.controls.grids.cliente.currentPage,
                        Rows: self.controls.grids.cliente.pageSize
                    }
                };
                self.SendAjax('POST', url, 'json', data, success);
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

Ui.prototype.loadControls = function () {
    var self = this;
    var destino = "";
    var execute = function (data) {
        if (data.Result === 1) {
            self.loadCombo(data.List, destino)
        }
    };
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
        self.controls.grids.cliente.loadRows([]);
        if (data.Result) {
            self.controls.grids.cliente.currentPage = data.Paging.CurrentPage;
            self.controls.grids.cliente.pages = data.Paging.Pages;
            self.controls.grids.cliente.loadRows(data.List);
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

Ui.prototype.SendAjax = function (method, url, dataType, data, $success, $complete) {
    $.ajax({
        type: method,
        url: url,
        dataType: dataType,
        data: $.toJSON(data),
        beforeSend: function () { },
        contentType: 'application/json; charset=utf-8',
        success: $success,
        error: function (e) {
            message = e.responseText;
        },
        complete: $complete
    });
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

function init() {
    var ui = new Ui();
    ui.init();
};

init();

var domicilioHnd = function (helper, data, controls) {
    var self = this;
    var proxy = helper;

    this.default = {
        codigoPostal: '',
        estado: '',
        municipio: '',
        asentamiento: ''
    };
    var Urls = {
        entidades: '/Catalogo/ObtenerEstado',
        municipios: '/Catalogo/MunicipiosObtener',
        asentamientos: '/Catalogo/AsentamientosObtener'
    };
    this.controls = $.extend(this.default, controls, true);
    this.prop = {
        IdEstado: data.IdEstado,
        IdMunicipio: data.IdMunicipio,
        IdAsentamiento: data.IdAsentamiento,
        CodigoPostal: data.CodigoPostal
    };

    this.init = function () {
        $(this.controls.codigoPostal).on('keydown', function (e) {
            var key = e.charCode || e.keyCode || 0;
            if (key == 13 && $(this).val().length == 5) {
                self.prop.codigoPostal = $(this).val();
                proxy.SendAjax('Post', Urls.asentamientos, 'Json', { idEstado: 0, idMunicipio: 0, codigoPostal: self.prop.codigoPostal }, self.loadCodigoPostal);
            }

            return 
                key == 8 ||
                key == 9 ||
                key == 13 ||
                key == 46 ||
                key == 110 ||
                key == 190 ||
                (key >= 35 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105);
        });

        self.loadEstado();
        if (self.prop.IdEstado && self.prop.IdEstado != 0) {
            self.loadMunicipios(self.prop.IdEstado);
            if (self.prop.IdMunicipio && self.prop.IdMunicipio != 0)
                self.loadAsentamiento(self.prop.IdEstado, self.prop.IdMunicipio, '');
        }
    }

    this.getData = function (method, url, selector, data, value, text, selectedId, initialItem, updateData) {
        var response = new responseObject({
            selector: selector,
            data: data,
            value: value,
            text: text,
            selectedId: selectedId,
            initialItem: initialItem,
            updateData: updateData
        });
        
        proxy.SendAjax(method, url, 'Json', data, response.loadDrop);
    };

    var responseObject = function (options) {
        var options = options;
        this.loadDrop = function (t) {
            $(options.selector).html(''); // Limpia el dropdown
            $(options.selector).append('<option value="0">' + options.initialItem + '</option>');  //Agrega el elemento Inicial de seleccione
            $(t.List).each(function () {
                $(options.selector).append('<option' +
                    (this[options.value] == options.selectedId ? ' selected' : '') +
                    (options.updateData ? ' codigoPostal=\'' + this['CodigoPostal'] + '\'' : '') +
                    ' value="' + this[options.value] + '">' + this[options.text] + '</option>');
            });
        };
    }

    this.loadEstado = function () {
        self.getData('Post', Urls.entidades, self.controls.estado, { page: 1, rows: 1 }, 'Identificador', 'Nombre', self.prop.IdEstado, 'Selecciona un Estado');

        $(self.controls.estado).on('change', function () {
            $(self.controls.codigoPostal).val('');
            self.loadMunicipio($(this).val(), 0);
            self.loadAsentamiento($(this).val(), 0, '');
        });
    };

    this.loadMunicipio = function (idEstado) {
        self.getData('Get', Urls.municipios + '/' + idEstado, self.controls.municipio, {}, 'Identificador', 'Nombre', self.prop.IdMunicipio, 'Selecciona un Municipio');

        $(self.controls.municipio).on('change', function () {
            $(self.controls.codigoPostal).val('');
            self.loadAsentamiento($(self.controls.estado).val(), $(this).val(), $(self.controls.codigoPostal).val());
        });
    };

    this.loadAsentamiento = function (idEstado, idMunicipio, codigoPostal) {
        self.getData('Post', Urls.asentamientos, self.controls.asentamiento, { idEstado: idEstado, idMunicipio: idMunicipio, codigoPostal: codigoPostal }, 'Identificador', 'Nombre', self.prop.IdAsentamiento, 'Selecciona un Asentamiento', true);
        $(self.controls.asentamiento).on('change', function () {
            var selectedValue = $(this).val();
            var cp = $('option[value=' + selectedValue + ']', this).attr('codigoPostal');
            $(self.controls.codigoPostal).val(cp);
        });
    };

    this.loadCodigoPostal = function (t) {
        var item1 = t.List.length ? t.List[0] : undefined;

        if (item1) {
            self.prop.IdEstado = item1["Estado"]["Identificador"];
            self.prop.IdMunicipio = item1["Municipio"]["Identificador"];

            $('option[selected]', self.controls.estado).removeAttr('selected');
            $(self.controls.estado).val(self.prop.IdEstado);

            self.loadMunicipio(self.prop.IdEstado);
            self.loadAsentamiento(0, 0, $(self.controls.codigoPostal).val());
        }
        else {
            alert('El código postal no se localiza');
        }
    };
}

var solicitantesHnd = function (helper, method, urls, dataType, data, region) {
    var self = this;
    this.prop = {
        ajaxHelper: null,
        method: '',
        urls: {},
        dataType: '',
        data: {},
        region: ''
    };
    this.prop.ajaxHelper = helper;
    this.prop.method = method;
    this.prop.urls = urls;
    this.prop.dataType = dataType;
    this.prop.data = data;
    this.prop.region = region;

    this.ajaxCall = function (url, data) {
        var p = self.prop;

        if (data)
            p.ajaxHelper.SendAjax(p.method, url, p.dataType, data, self.renderSection);
        else
            p.ajaxHelper.SendAjax(p.method, url, p.dataType, p.data, self.renderSection);
    };

    this.renderSection = function (t) {
        $(self.prop.region).html(t);

        $('span.field-validation-error', '#formSolicitante').each(function () {
            $(this).html("");
            $(this).removeClass('field-validation-error');
        });
        $('input', '#formSolicitante').each(function () {
            $(this).removeClass('input-validation-error');
        });

        self.addListener();
    };

    this.addListener = function () {
        $('#btnNewSolicitante').off('click').click(function (e) {
            var p = self.prop;
            var data = {
                Identificador: 0,
                IdTipoPersona: 1,
                IdCliente: p.data.IdCliente,
                Action: 0 //Nuevo
            };

            self.ajaxCall(p.urls.solicitante, data);
        });
        $('a[name=solicitanteEdit]').off('click').click(function () {
            var dbSaved = $(this).attr('dbSaved') == 'True';
            var p = self.prop;
            var data = {
                Numero: parseInt($(this).attr('numero')),
                Identificador: parseInt($(this).attr('idSolicitante')),
                IdTipoPersona: 1,
                IdCliente: parseInt($(this).attr('idCliente')),
                DbSaved: dbSaved,
                Action: 1 //Editar
            };
            self.ajaxCall(p.urls.solicitante, data);
        });
        $('a[name=solicitanteDetail]').on('click', function () {
            var p = self.prop;
            var data = {
                Numero: parseInt($(this).attr('numero')),
                IdCliente: parseInt($(this).attr('idCliente')),
                Action: 2 //Editar
            };
            self.ajaxCall(p.urls.solicitante, data);
        });
        $('a[name=solicitanteDelete]').on('click', function () {
            var idSolicitante = parseInt($(this).attr('idSolicitante'));
            // Pedimos la confirmación para cambiar el estatus del solicitante
        });
        $('#btnBackSolicitante').on('click', function () {
            self.ajaxCall(self.prop.urls.solicitantes);
        });
        $('#btnSaveSolicitante').on('click', function () {
            $.validator.unobtrusive.parse("#formSolicitante");
            var valid = $('#formSolicitante').valid();
            if (valid) {
                var solicitanteData = {
                    Numero: parseInt($('#clienteSolicitantes #Numero').val()),
                    Identificador: parseInt($('#clienteSolicitantes #Identificador').val()),
                    IdCliente: self.prop.data.Identificador,
                    IdTipoPersona: 1,
                    Nombre: $('#clienteSolicitantes #Nombre').val(),
                    ApellidoPaterno: $('#clienteSolicitantes #ApellidoPaterno').val(),
                    ApellidoMaterno: $('#clienteSolicitantes #ApellidoMaterno').val(),
                    Cargo: $('#clienteSolicitantes #Cargo').val(),
                    Action: parseInt($('#clienteSolicitantes #Action').val())
                };
                self.ajaxCall(self.prop.urls.solicitanteguardar, solicitanteData);
            }
        });
    }
}

var contactosHnd = function (helper, method, urls, dataType, data, region) {
    var self = this;
    this.prop = {
        ajaxHelper: null,
        method: '',
        urls: {},
        dataType: '',
        data: {},
        region: ''
    };
    this.prop.ajaxHelper = helper;
    this.prop.method = method;
    this.prop.urls = urls;
    this.prop.dataType = dataType;
    this.prop.data = data;
    this.prop.region = region;

    this.ajaxCall = function (url, data) {
        var p = self.prop;

        if (data)
            p.ajaxHelper.SendAjax(p.method, url, p.dataType, data, self.renderSection);
        else
            p.ajaxHelper.SendAjax(p.method, url, p.dataType, p.data, self.renderSection);
    };

    this.renderSection = function (t) {
        $(self.prop.region).html(t);

        $('span.field-validation-error', '#formContacto').each(function () {
            $(this).html("");
            $(this).removeClass('field-validation-error');
        });
        $('input', '#formContacto').each(function () {
            $(this).removeClass('input-validation-error');
        });

        self.addListener();
    };

    this.addListener = function () {
        $('#btnNewContacto').on('click', function (e) {
            var p = self.prop;
            var data = {
                Identificador: 0,
                IdTipoPersona: 2,
                IdCliente: p.data.IdCliente,
                Action: 0 //Nuevo
            };

            self.ajaxCall(p.urls.contacto, data);
        });
        $('a[name=contactoEdit]').on('click', function () {
            var dbSaved = $(this).attr('dbSaved') == 'True';
            var p = self.prop;
            var data = {
                Numero: parseInt($(this).attr('numero')),
                Identificador: parseInt($(this).attr('idSolicitante')),
                IdTipoPersona: 2,
                IdCliente: parseInt($(this).attr('idCliente')),
                DbSaved: dbSaved,
                Action: 1 //Editar
            };
            self.ajaxCall(p.urls.contacto, data);
        });
        $('a[name=contactoDetail]').on('click', function () {
            var p = self.prop;
            var data = {
                Numero: parseInt($(this).attr('numero')),
                IdCliente: parseInt($(this).attr('idCliente')),
                Action: 2 //Consultar
            };
            self.ajaxCall(p.urls.contacto, data);
        });
        $('a[name=contactoDelete]').on('click', function () {
            var idContacto = parseInt($(this).attr('idContacto'));
            // Pedimos la confirmación para cambiar el estatus del solicitante
        });
        $('#btnBackContacto').on('click', function () {
            self.ajaxCall(self.prop.urls.contactos);
        });
        $('#btnSaveContacto').on('click', function () {
            $.validator.unobtrusive.parse("#formContacto");
            var valid = $('#formContacto').valid();
            if (valid) {
                var contactoData = {
                    Numero: parseInt($('#clienteContactos #Numero').val()),
                    Identificador: $('#clienteContactos #Identificador').val(),
                    IdCliente: self.prop.data.Identificador,
                    IdTipoPersona: 1,
                    Nombre: $('#clienteContactos #Nombre').val(),
                    ApellidoPaterno: $('#clienteContactos #ApellidoPaterno').val(),
                    ApellidoMaterno: $('#clienteContactos #ApellidoMaterno').val(),
                    Cargo: $('#clienteContactos #Cargo').val(),
                    Action: parseInt($('#clienteContactos #Action').val())
                };
                self.ajaxCall(self.prop.urls.contactoguardar, contactoData);
            }
        });
    }
}