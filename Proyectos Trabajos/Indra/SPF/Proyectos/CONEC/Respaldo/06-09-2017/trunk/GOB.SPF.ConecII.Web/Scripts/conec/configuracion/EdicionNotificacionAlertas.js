Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        ObtenerIntegrantesArea: this.getUrl('/Configuracion/ConsultaIntegrantes')
    };
    this.controls = {
        grids: {
            GridIntegrantesArea: new controls.Grid('divIntegrantesRol', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 1, pages: 1 } })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        },
        dropdownlist: {
            ddlAreasResR: $("#ddlAreasResR")
        }
    };
    var columns =
        [
        { text: 'No.', field: 'Identificador', css: 'col-xs-1', visible: true },
        { text: 'Tipo de servicio', field: 'TipoServicio', css: 'col-xs-2' },
        { text: 'Fase', field: 'Fase', css: 'col-xs-2' },
        { text: 'Actividad', field: 'Actividad', css: 'col-xs-3' },
        { text: 'Fase', field: 'Fase', css: 'col-xs-2' },
        { text: 'Alerto por correo', field: 'AlertaEsCorreo', css: 'col-xs-1' },
        { text: 'Alerta en sistema', field: 'AlertaEsSistema', css: 'col-xs-1' }];

    this.controls.grids.GridIntegrantesArea.init(columns);
    this.controls.grids.GridIntegrantesArea.addListener("onPage", function (evt) {
        self.controls.grids.GridIntegrantesArea.currentPage = evt.currentPage;
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
                self.controls.grids.GridIntegrantesArea.reload([]);
                self.controls.grids.GridIntegrantesArea.reload(data);
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
                CurrentPage: self.controls.grids.GridIntegrantesArea.currentPage,
                Rows: self.controls.grids.GridIntegrantesArea.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    $(ddlAreasResR).change(function (e) {
        var data = {
            ObjectResult: {
                Area: $(this).val()
            }, Paging: {
                CurrentPage: self.controls.grids.GridIntegrantesArea.currentPage,
                Rows: self.controls.grids.GridIntegrantesArea.pageSize
            }
        };
        self.SendAjax('POST', self.urls.ObtenerIntegrantesArea, 'json', data, success);
    });

    this.controls.grids.GridIntegrantesArea.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#divisionForm");
                if ($("#divisionForm").valid()) {
                    var data = {
                        ObjectResult: {
                            Name: $("#Name").val(),
                            Descripcion: $("#Descripcion").val(),
                            Identificador: $("#Identificador").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.GridIntegrantesArea.currentPage,
                            Rows: self.controls.grids.GridIntegrantesArea.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
            });

        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.division;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.division;
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
                        CurrentPage: self.controls.grids.division.currentPage,
                        Rows: self.controls.grids.division.pageSize
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
    var page = self.controls.grids.GridIntegrantesArea.currentPage;
    var rows = self.controls.grids.GridIntegrantesArea.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.GridIntegrantesArea.loadRows([]);
        if (data.Result) {
            self.controls.grids.GridIntegrantesArea.currentPage = data.Paging.CurrentPage;
            self.controls.grids.GridIntegrantesArea.pages = data.Paging.Pages;
            self.controls.grids.GridIntegrantesArea.loadRows(data.List);
        }

    };
    //var data = { page: page, rows: rows };
    var data = {
        ObjectResult: {
            Area: ddlAreasResR.value
        }, Paging: {
            CurrentPage: self.controls.grids.GridIntegrantesArea.currentPage,
            Rows: self.controls.grids.GridIntegrantesArea.pageSize
        }
    };

    this.SendAjax('POST', self.urls.ObtenerIntegrantesArea, 'json', data, loadGrid);
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
