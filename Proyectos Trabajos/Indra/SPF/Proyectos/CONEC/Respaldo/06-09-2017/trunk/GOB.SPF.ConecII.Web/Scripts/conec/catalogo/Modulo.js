Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        modulos: this.getUrl('/Catalogo/ModulosConsulta'),
        modulo: this.getUrl('/Catalogo/Modulo'),
        guardar: this.getUrl('/Catalogo/ModuloGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/ModuloCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/ModulosConsultaCriterio')
    };
    this.controls = {
        grids: {
            modulo: new controls.Grid('divModulo', { maxHeight: 400, pager: { show: true, pageSize: 3, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [
                    { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
                    { text: 'Nombre', field: 'Name', css: 'col-xs-3' },
                    { text: 'Descripción', field: 'Descripcion', css: 'col-xs-4' },
                    { text: 'Deriv.', field: 'IdPadre', css: 'col-xs-2' },
                    { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
                    { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
                    { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }
                 ];
    this.controls.grids.modulo.init(columns);
    this.controls.grids.modulo.addListener("onPage", function (evt) {
        self.controls.grids.modulo.currentPage = evt.currentPage;
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
                self.controls.grids.modulo.reload([]);
                self.controls.grids.modulo.reload(data);
            };
            loadGrid(data.List);
            self.functions.hideForm();
        }
    };

    $(this.controls.buttons.buscar).click(function () {
        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.modulo.currentPage,
                Rows: self.controls.grids.modulo.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.modulo.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#moduloForm");
                if ($("#moduloForm").valid()) {
                    debugger;
                    var data = {
                        ObjectResult: {
                            Name:           $("#Name").val(),
                            Descripcion:    $("#Descripcion").val(),
                            Identificador:  $("#Identificador").val(),
                            IdPadre:        $("#Modulos").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.modulo.currentPage,
                            Rows: self.controls.grids.modulo.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
            });

        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.modulo;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.modulo;
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
                        CurrentPage: self.controls.grids.modulo.currentPage,
                        Rows: self.controls.grids.modulo.pageSize
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
    var page = self.controls.grids.modulo.currentPage;
    var rows = self.controls.grids.modulo.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.modulo.loadRows([]);
        if (data.Result) {
            self.controls.grids.modulo.currentPage = data.Paging.CurrentPage;
            self.controls.grids.modulo.pages = data.Paging.Pages;
            self.controls.grids.modulo.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.modulos, 'json', data, loadGrid);
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