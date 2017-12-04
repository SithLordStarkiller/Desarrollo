Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    var columns = [
        { text: 'No. Instalación', iskey: true, isHierarchy: true, css: 'col-xs-2', visible: true },
        { text: 'Nombre o Razón Social', field: 'RazonSocial', css: 'col-xs-3' },
        { text: 'Nombre corto', field: 'NombreCorto', css: 'col-xs-3' },
        { text: 'RFC', field: 'RFC', css: 'col-xs-3' },
        { text: '', field: 'Nuevo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, title: 'Nuevo', cssClass: { normal: 'Plus' } } }];
    var columnsIns = [
       { text: 'No. Instalación', iskey: true, ishisHierarchy: false, css: 'col-xs-1', visible: true },
       { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
       { text: 'Zona', field: 'NombreZona', css: 'col-xs-2' },
       { text: 'Estación', field: 'NombreEstacion', css: 'col-xs-2' },
       { text: 'Entidad Federativa', field: 'NombreEstado', css: 'col-xs-2' },
       { text: 'Municipio/Delegación', field: 'NombreMunicipio', css: 'col-xs-2' },
       { text: '', field: 'Editar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, title: 'Editar', cssClass: { normal: 'Edit' } } },
       { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
       { text: '', field: 'Activo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }
    ];

    this.urls = {
        instalaciones: this.getUrl('/Solicitud/InstalacionConsulta'),
        instalacion: this.getUrl('/Solicitud/Instalacion'),
        guardar: this.getUrl('/Solicitud/InstalacionGuardar'),
        cambiarestatus: this.getUrl('/Solicitud/InstalacionCambiarEstatus'),
        buscar: this.getUrl('/Solicitud/InstalacionConsultaCriterio'),

        entidades: this.getUrl('/Catalogo/ObtenerEstado'),
        municipios: this.getUrl('/Catalogo/MunicipiosObtener/'),
        asentamiento: this.getUrl('/Catalogo/AsentamientosObtener')
    };
    this.messages = {
        CambiarEstado: "Deceas modificar a @ el estado en la configuracion del servicio"
    };
    this.controls = {
        grids: {
            instalacion: new controls.Grid('divInstalaciones', { pager: { show: true, pageSize: 10, currentPage: 1, pages: 1 }, detail: { field: "Instalaciones", columns: columnsIns } })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar"),
            telefono: document.getElementById("btnTelefono")
        }
    };

    this.listatelefonos = [];
    this.listacorreos = [];

    this.controls.grids.instalacion.init(columns);
    this.controls.grids.instalacion.addListener("onPage", function (evt) {
        //    initializeMaps();
        self.controls.grids.instalacion.currentPage = evt.currentPage;
        self.load();
    });

    this.functions = {
        hideGrid: function (data) {
            $(".flip div.front").slideToggle();
            $(".flip div.back").html(data).fadeIn();
        },
        hideForm: function (data) {
            $(".flip div.back div").remove().slideToggle();
            $(".flip div.front").fadeIn();
        }

    };

    var success = function (data) {
        if (data.Result === 1) {
            var loadGrid = function (data) {
                self.controls.grids.instalacion.reload([]);
                self.controls.grids.instalacion.reload(data);
            };
            loadGrid(data.List);
            self.functions.hideForm();
        }
        else if (data.Result === 0) {
            self.showMessage(data.Message);
        } else {
            self.showMessage(data.Message);
        }
    };

    $(this.controls.buttons.buscar).click(function () {
        var successGridCliente = function (data) {
            if (data.Result === 1) {
                var loadGrid = function (data) {
                    self.controls.grids.instalacion.reload([]);
                    self.controls.grids.instalacion.reload(data);

                    //window.initMap();
                };
                loadGrid(data.List);
                self.functions.hideForm();
            } else {
                self.showMessage(data.Message);
            }
        };

        var data = {
            Query: {
                NombreCorto: $("#txbNombreCorto").val(),
                RazonSocial: $("#txbRazonSocial").val(),
                RFC: $("#txbRfc").val(),
                Instalaciones: [{ Nombre: $("#tbxNombreInstalacion").val(), Activo: $("input[name=rdbEstatus]:checked").val() }]
            }
        };
        self.SendAjax('POST', self.urls.instalaciones, 'json', data, successGridCliente);
    });

    this.controls.grids.instalacion.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var validacion = "";
        var tipoResultado = null;
        var dataRow = undefined;
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#instalacionForm");
                var valido = $("#instalacionForm").valid();

                if (valido) {
                    var data1 = {
                        model: {
                            Identificador : $("#Identificador").val(),
                            Cliente: { Identificador: $("#IdCliente").val() },
                            Nombre: $("#Nombre").val(),
                            TipoInstalacion: { Identificador: $("#TipoInstalacion_Identificador").val() },
                            Zona: { Identificador: $("#Zona_IdZona").val() },
                            Estacion: { Identificador: $("#Estacion_IdEstacion").val() },
                            TelefonosInstalacion: self.listatelefonos,
                            CorreosInstalacion: self.listacorreos,
                            FechaInicio: $("#FechaInicio").val(),
                            FechaFin: $("#FechaFin").val(),
                            Calle: $("#Calle").val(),
                            NumInterior: $("#NumInterior").val(),
                            NumExterior: $("#NumExterior").val(),
                            Referencia: $("#Referencia").val(),
                            Colindancia: $("#Colindancia").val(),
                            Asentamiento: {
                                CodigoPostal: $("#Asentamiento_CodigoPostal").val(),
                                Identificador: $("#Asentamiento_Identificador").val(),
                                Estado: {
                                    Identificador: $("#Asentamiento_Estado_Identificador").val()
                                },
                                Municipio: {
                                    Identificador: $("#Asentamiento_Municipio_Identificador").val()
                                }
                            },
                            Latitud: $("#Latitud").val(),
                            Longitud: $("#Longitud").val(),
                            Divicion: {
                                Identificador: $("#Divicion_Identificador").val()
                            },
                            Grupo: {
                                Identificador: $("#Grupo_Identificador").val()
                            },
                            Fraccion: {
                                Identificador: $("#Fraccion_Identificador").val()
                            },
                            Activo : true
                        }
                    };

                    self.SendAjax('POST', self.urls.guardar, 'json', data1, success);

                }
            });

            var parameters = {
                codigoPostal: {
                    urlDestino: self.urls.asentamiento,
                    field: 'Identificador',
                    text: 'Nombre',
                    value: 'Identificador',
                    control: document.getElementById("Asentamiento_CodigoPostal"),
                    destino: document.getElementById("Asentamiento_Identificador")
                },
                estado: {
                    urlDestino: self.urls.municipios,
                    field: 'Identificador',
                    text: 'Nombre',
                    value: 'Identificador',
                    control: document.getElementById("Asentamiento_Estado_Identificador"),
                    destino: document.getElementById("Asentamiento_Municipio_Identificador")
                },
                municipio: {
                    urlDestino: self.urls.asentamiento,
                    field: 'Identificador',
                    text: 'Nombre',
                    value: 'Identificador',
                    control: document.getElementById("Asentamiento_Municipio_Identificador"),
                    destino: document.getElementById("Asentamiento_Identificador")
                }
            };

            if (instalacionModel) {
                self.listatelefonos = instalacionModel.TelefonosInstalacion ? instalacionModel.TelefonosInstalacion : [];
                self.listacorreos = instalacionModel.CorreosInstalacion ? instalacionModel.CorreosInstalacion : [];
                //$("#FechaInicio").val(new Date(parseInt(instalacionModel.FechaInicio.replace(/\/Date\(|\)\//g, ''))));
                //$("#FechaFinal").val(instalacionModel.FechaFinal);
            }

            self.loadEventDomicilio(parameters);

            $("#Asentamiento_Estado_Identificador").change(function () {
                self.loadMunicipios(this.value, parameters.estado.destino, parameters.estado.urlDestino);
                //self.loadAsentamientos(this.value, 0, parameters.municipio.destino, parameters.municipio.urlDestino);
                $('#Asentamiento_CodigoPostal').val('');
            });
            $("#Asentamiento_Municipio_Identificador").change(function () {
                var estado = $("#Asentamiento_Estado_Identificador").val();
                self.loadAsentamientos(estado, this.value, parameters.municipio.destino, parameters.municipio.urlDestino);
                $('#Asentamiento_CodigoPostal').val('');
            });
            $("#Asentamiento_Identificador").change(function () {
                var option = $(this.options[this.selectedIndex]);
                if (option.data("option") !== undefined)
                    $("#Asentamiento_CodigoPostal").val(option.data("option").CodigoPostal);
                else
                    $("#Asentamiento_CodigoPostal").val("");
            });
            self.loadTelefonosCorreos(dataRow);
            self.loadControls();
        };

        switch (evt.event) {
            case 'Edit':
            case 'View':

                var dataValue = $.extend(evt.dataRow.parent, {}, true);
                dataValue.Instalaciones = dataValue.Instalaciones.filter(function (item) {
                    if (item.Identificador === evt.dataRow.child.Identificador) {
                        var data = $.extend(evt.dataRow.child, {}, true);

                        if (!(data.FechaInicio instanceof Date))
                            data.FechaInicio = new Date(parseInt(data.FechaInicio.replace(/\/Date\(|\)\//g, '')));

                        //if (!(data.FechaFinal instanceof Date))
                        //    data.FechaFinal = new Date(parseInt(data.FechaFinal.replace(/\/Date\(|\)\//g, '')));
                        return data;
                    }
                });
                url = self.urls.instalacion;
                data = { model: $.extend(dataValue, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Plus':
                evt.dataRow.Action = "New";
                url = self.urls.instalacion;
                data = { model: /*{ Action: evt.event }, Query:*/ evt.dataRow };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.cambiarestatus;
                var object = evt.dataRow.child;
                var activo;

                if (object.Activo === true) {
                    activo = object.Activo === false;
                } else {
                    activo = object.Activo === true;
                }

                object.Activo = activo;

                var actualizarRegistro = function () {
                    data = {
                        Query: {
                            Activo : $("input[name=rdbEstatus]:checked").val()
                        },
                        ObjectResult: object // {EsActivo : object.IsActive}
                    };
                    self.SendAjax('POST', url, 'json', data, success);
                };

                var anuncio = self.messages.CambiarEstado.replace("@", (object.IsActive === false ? "activar" : "desactivar"));
                self.confirmacion(anuncio, { title: "Cambiar estatus", aceptar: actualizarRegistro });
                break;
        }
    });
    this.load();
};

Ui.prototype.loadControls = function () {
    var self = this;
    var destino = "";
    var execute = function (data) {
        if (data.Result === 1) {
            self.loadCombo(data.List, destino);
        }
    };

};

Ui.prototype.loadTelefonosCorreos = function (data) {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.Solicitante = data === undefined ? { Telefonos: [], Correos: [] } : data.solicitante === undefined ? { Telefonos: [], Correos: [] } : data.solicitante;

    this.controls.grids = $.extend(this.controls.grids, {
        telefonos: new controls.Grid('clienteContactosTelefonos', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: false }),
        correos: new controls.Grid('clienteContactosCorreos', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: false })
    }, true);

    var columnsTelefonos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
    { text: 'Tipo', field: 'TipoTelefonoTexto', css: 'col-xs-2' },
    { text: 'Numero', field: 'Numero', css: 'col-xs-2' },
    { text: 'Extension', field: 'Extension', css: 'col-xs-4' },
    { text: '', field: 'Numero', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Remove' } } }];

    var columnsCorreos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
    { text: 'Correo', field: 'CorreoElectronico', css: 'col-xs-2' },
    { text: '', field: 'Correo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Remove' } } }];

    /* Si el registro unicamente es para visualizar entonces no debe mostrar los campos de editar y activar y desactivar solo el de ver detalle */
    if (data)
        if (data.contacto.Action === "View") {
            columnsTelefonos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
            { text: 'Tipo', field: 'TipoTelefono', css: 'col-xs-4' },
            { text: 'Numero', field: 'Numero', css: 'col-xs-4' },
            { text: 'Extension', field: 'Extension', css: 'col-xs-3' }];

            columnsCorreos = [{ text: 'No.', iskey: true, css: 'col-xs-2', visible: true },
            { text: 'Correo', field: 'CorreoElectronico', css: 'col-xs-10' }];
        }

    this.controls.grids.telefonos.init(columnsTelefonos);
    this.controls.grids.telefonos.loadRows(this.listatelefonos);

    this.controls.grids.correos.init(columnsCorreos);
    this.controls.grids.correos.loadRows(this.listacorreos);

    this.controls.grids.telefonos.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        switch (evt.event) {

            case 'Remove':

                var item = evt.dataRow.Indice;

                self.listatelefonos.splice(item, 1);

                self.controls.grids.telefonos.reload(self.listatelefonos);

                self.showMessage("Se elimino el telefono");
                break;
        };


    });
    this.controls.grids.correos.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        switch (evt.event) {

            case 'Remove':

                var item = evt.dataRow.Indice;

                self.listacorreos.splice(item, 1);

                self.controls.grids.correos.reload(self.listacorreos);

                self.showMessage("Se elimino el correo");
                break;

        }
    });

    $("#formTelefono #btnTelefono").click(function () {

        $.validator.unobtrusive.parse($("#formTelefono"));
        if (!$("#formTelefono").valid()) return;

        var data = {
            Indice: self.listatelefonos.length + 1,
            TipoTelefono: {
                Identificador: $("#TipoTelefono_Identificador").val(),
                Name: $("#TipoTelefono_Identificador option:selected").text()
            },
            TipoTelefonoTexto: $("#TipoTelefono_Identificador option:selected").text(),
            Numero: $("#Numero").val(),
            Extension: $("#Extension").val(),
            IsActive: true
        };
        self.listatelefonos.push(data);
        self.controls.grids.telefonos.reload(self.listatelefonos);
        //$("#IdTipoTelefono option:selected").removeAttr("selected");
        $("#IdTipoTelefono:first-child").attr("selected", "selected");
        $("#Numero").val("");
        $("#Extension").val("");
    });

    $("#btnCorreo").click(function () {

        $.validator.unobtrusive.parse($("#formCorreo"));
        if (!$("#formCorreo").valid()) return;

        var data = {
            Indice: self.listacorreos.length + 1,
            CorreoElectronico: $("#CorreoElectronico").val(),
            IsActive: true
        };
        self.listacorreos.push(data);
        self.controls.grids.correos.reload(self.listacorreos);
        $("#CorreoElectronico").val("");
    });
}
/***************/


Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.instalacion.pages;
    var rows = self.controls.grids.instalacion.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.instalacion.loadRows([]);

        if (data.Result) {
            self.controls.grids.instalacion.loadRows(data.List);
        }

    };

    var data = {
        Paging: {
            Pages: page,
            Rows: rows
        },
        Query: {
            IsActive: $("input[name=rdbEstatus]:checked").val()
        }
    };

    this.SendAjax('POST', self.urls.instalaciones, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();

$(document).ready(function () {
    $("#FechaInicio").datepicker({ changeYear: true });
});