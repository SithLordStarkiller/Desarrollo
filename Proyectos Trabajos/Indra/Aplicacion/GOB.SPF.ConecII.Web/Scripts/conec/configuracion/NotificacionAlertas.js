Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        buscar: this.getUrl('/Configuracion/ObtenerNotificaciones'),
        nuevo: this.getUrl('/Configuracion/CreacionDeNotifacionesAlertas')
    };
    this.controls = {
        grids: {
            factor: new controls.Grid('divNotificaciones', { maxHeight: 400, pager: { show: true, pageSize: 3, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [
        { text: 'No.', /*iskey: true*/field: 'IdNotificacion', css: 'col-xs-1', visible: true },
        { text: 'Tipo Servicio', field: 'TipoServicio', css: 'col-xs-2' },
        { text: 'Face', field: 'Fase', css: 'col-xs-2' },
        { text: 'Actividad', field: 'Actividad', css: 'col-xs-2' },
        { text: 'Metodo de envio', field: 'CuerpoCorreo', css: 'col-xs-2' },
        { text: 'Alertas', field: 'EmitirAlerta', css: 'col-xs-3' },
        { text: 'Frecuencia', field: 'Frecuencia', css: 'col-xs-1' },
        { text: '', field: 'IdNotificacion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        //{ text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        //{ text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }
    ];
    this.controls.grids.factor.init(columns);
    this.controls.grids.factor.addListener("onPage", function (evt) {
        self.controls.grids.factor.currentPage = evt.currentPage;
        self.load();
    });

    this.functions = {
        hideGrid: function (data) {
            $(".flip div.front").slideToggle(),
            $(".flip div.back").html(data).fadeIn();
        },
        hideForm: function (data) {
            $(".flip div.back div").remove().slideToggle(),
            $(".flip div.front").fadeIn();
        }

    };

    var success = function (data) {
        if (data.Result === 1) {
            var loadGrid = function (data) {
                self.controls.grids.factor.reload([]);
                self.controls.grids.factor.reload(data);
            };
            loadGrid(data.List);
            self.functions.hideForm();
        }
    };

    $(this.controls.buttons.buscar).click(function () {
        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val()
            }, Paging: {
                CurrentPage: self.controls.grids.factor.currentPage,
                Rows: self.controls.grids.factor.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.factor.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#factorForm");
                if ($("#factorForm").valid()) {
                    var data = {
                        ObjectResult: {
                            IdTipoServicio: $("#TipoServicio").val(),
                            TipoServicio: $("#TipoServicio").text(),
                            IdClasificacionFactor: $("#Clasificacion").val(),
                            ClasificadorFactor: $("#Clasificacion").text(),
                            IdMedidaCobro: $("#MedidaCobro").val(),
                            MedidaCobro: $("#MedidaCobro").text(),
                            Name: $("#Name").val(),
                            Descripcion: $("#Descripcion").val(),
                            CuotaFactor: $("#CuotaFactor").val(),
                            FechaAutorizacion: $("#FechaAutorizacion").val(),
                            FechaEntradaVigor: $("#FechaEntradaVigor").val(),
                            FechaTermino: $("#FechaTermino").val(),
                            FechaPublicacionDof: $("#FechaPublicacionDof").val(),
                            Identificador: $("#Identificador").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.factor.currentPage,
                            Rows: self.controls.grids.factor.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
            });

        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.factor;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.nuevo;
                window.location.href = url;//'http://localhost:30669/Configuracion/CreacionDeNotifacionesAlertas';
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
                        CurrentPage: self.controls.grids.factor.currentPage,
                        Rows: self.controls.grids.factor.pageSize
                    }
                };
                self.SendAjax('POST', url, 'json', data, success);
                break;
        }
    });

    this.load();

};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.factor.currentPage;
    var rows = self.controls.grids.factor.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.factor.loadRows([]);
        if (data.Result) {
            self.controls.grids.factor.currentPage = data.Paging.CurrentPage;
            self.controls.grids.factor.pages = data.Paging.Pages;
            self.controls.grids.factor.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.buscar, 'json', data, loadGrid);
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
};

init();