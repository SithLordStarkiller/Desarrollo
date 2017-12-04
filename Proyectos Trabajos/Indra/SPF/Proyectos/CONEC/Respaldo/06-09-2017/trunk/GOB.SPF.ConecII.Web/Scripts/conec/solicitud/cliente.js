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
        intalaciones: this.getUrl('/Solicitud/ClienteInstalaciones')
    };
    this.controls = {
        grids: {
            cliente: new controls.Grid('divClientes', { maxHeight: 400, pager: { show: true, pageSize: 15, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Régimen fiscal', field: 'RegimenFiscal', css: 'col-xs-1' },
        { text: 'Sector', field: 'Sector', css: 'col-xs-1' },
        { text: 'Razón social', field: 'RazonSocial', css: 'col-xs-3' },
        { text: 'Nombre corto', field: 'NombreCorto', css: 'col-xs-2' },
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

    $(this.controls.buttons.buscar).click(function () {
        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val(),
                RazonSocial: $("input[name=razonSocial]").val(),
                NombreCorto: $("input[name=nombreCorto]").val(),
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
                    var data1 = {
                        ObjectResult: {
                            Identificador: $("#Identificador").val(),
                            /*Nombre: $("#Nombre").val(),
                            Descripcion: $("#Descripcion").val(),
                            IdGrupo: $("#IdGrupo").val(),
                            IdDivision: $("#IdDivision").val()*/
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
        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.cliente;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.cliente;
                data = { model: { Action: evt.event } };
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
                    ObjectResult: object, Paging: {
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

};

Ui.prototype.loadControls = function () {
    var self = this;
    var destino = "";
    var execute = function (data) {
        if (data.Result == 1) {
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

Ui.prototype.SendAjax = function (method, url, dataType, data, $function) {
    $.ajax({
        type: 'POST',
        url: url,
        dataType: dataType,
        data: $.toJSON(data),
        beforeSend: function () { },
        contentType: 'application/json; charset=utf-8',
        success: $function
    });
};

function init() {
    var ui = new Ui();
    ui.init();
    
    $(function () {
        $("#razonSocial").autocomplete({
            source: "/Solicitud/ClientesObtenerPorRazonSocial",
            minLength: 3,
            scroll: true,
            
            select: function (event, ui) {

            }
        });
    });
    $(function () {
        $("#nombreCorto").autocomplete({
            source: "/Solicitud/ClientesObtenerPorNombreCorto",
            minLength: 3,
            scroll: true,

            select: function (event, ui) {

            }
        });
    });
};

init();
