Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        partesDocumentoConsulta: this.getUrl('/Instituciones/Consulta'),
        partesDocumentoConsultaCriterio: this.getUrl('/Instituciones/ConsultaCriterio'),
        partesDocumentoPorId: this.getUrl('/Instituciones/Item'),
        partesDocumentoGuardar: this.getUrl('/Instituciones/Guardar'),
        partesDocumentoCambiarEstatus: this.getUrl('/Instituciones/CambiarEstatus'),
    };
    this.controls = {
        grids: {
            partesDocumento: new controls.Grid('divInstituciones', { maxHeight: 400, pager: { show: false, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [
        { text: 'Institución', field: 'Nombre', css: 'col-xs-4' },
        { text: 'Negrita', field: 'Negrita', css: 'col-xs-3' },
        { text: 'Orden', field: 'Orden', css: 'col-xs-3' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'Activo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.partesDocumento.init(columns);
    this.controls.grids.partesDocumento.addListener("onPage", function (evt) {
        self.controls.grids.partesDocumento.currentPage = evt.currentPage;
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
                self.controls.grids.partesDocumento.reload([]);
                self.controls.grids.partesDocumento.reload(data);
            };
            loadGrid(data.List);
            self.functions.hideForm();
        } else {
            self.showMessage(data.Message);
        }
    };

    //$(this.controls.buttons.buscar).click(function () {
    //    var data = {
    //        ObjectResult: {
    //            Activo: $("input[name=estatus]:checked").val(),

    //        }, Paging: {
    //            CurrentPage: self.controls.grids.partesDocumento.currentPage,
    //            Rows: self.controls.grids.partesDocumento.pageSize
    //        }
    //    };

    //    self.SendAjax('POST', self.urls.partesDocumentoBuscar, 'json', data, success);//no se ocupa

    //});

    this.controls.grids.partesDocumento.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#institucionForm");
                if ($("#institucionForm").valid()) {
                    var data = {
                        ObjectResult: {
                            Identificador: $("#Identificador").val(),
                            IdTipoDocumento: $("#IdTipoDocumentoInstituciones").val(),
                            Nombre: $("#Nombre").val(),
                            Negrita: $("#Negrita").is(':checked'),
                            Orden: $("#Orden").val(),
                            FechaInicial: $("#FechaInicial").val(),
                            FechaFinal: $("#FechaFinal").val(),
                            Activo: $("#Activo").is(':checked')
                        }, Paging: {
                            CurrentPage: self.controls.grids.partesDocumento.currentPage,
                            Rows: self.controls.grids.partesDocumento.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.partesDocumentoGuardar, 'json', data, success);
                }
            });

        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.partesDocumentoPorId;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.partesDocumentoPorId;
                data = { model: { Action: evt.event, IdTipoDocumento: $("#IdTipoDocumento").val() } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.partesDocumentoCambiarEstatus;
                var object = evt.dataRow;
                object.Activo = !object.Activo;
                data = {
                    Query: {
                        IsActive: $("input[name=estatus]:checked").val(),
                    },
                    ObjectResult: object, Paging: {
                        CurrentPage: self.controls.grids.partesDocumento.currentPage,
                        Rows: self.controls.grids.partesDocumento.pageSize
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
    var page = self.controls.grids.partesDocumento.currentPage;
    var rows = self.controls.grids.partesDocumento.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.partesDocumento.loadRows([]);
        if (data.Result) {
            self.controls.grids.partesDocumento.currentPage = data.Paging.CurrentPage;
            self.controls.grids.partesDocumento.pages = data.Paging.Pages;
            self.controls.grids.partesDocumento.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.partesDocumentoConsultaCriterio, 'json', data, loadGrid);
};

Ui.prototype.SendAjax = function (method, url, dataType, data, $function) {
    $.ajax({
        type: 'POST',
        url: url,
        dataType: dataType,
        data: $.toJSON(data),
        beforeSend: function () { },
        contentType: 'application/json; charset=utf-8',
        success: $function,
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }

    });
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();