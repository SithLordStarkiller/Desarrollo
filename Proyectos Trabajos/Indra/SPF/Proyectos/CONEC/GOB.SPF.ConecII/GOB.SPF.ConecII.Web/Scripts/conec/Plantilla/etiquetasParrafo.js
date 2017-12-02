Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        etiquetasParrafoConsulta: this.getUrl('/EtiquetasParrafo/Consulta'),
        etiquetasParrafoConsultaCriterio: this.getUrl('/EtiquetasParrafo/ObtenerPorParteDocumento'),
        etiquetasParrafoPorId: this.getUrl('/EtiquetasParrafo/Item'),
        etiquetasParrafoGuardar: this.getUrl('/EtiquetasParrafo/GuardarEtiqueta'),
        etiquetasParrafoCambiarEstatus: this.getUrl('/EtiquetasParrafo/CambiarEstatus'),
    };
    this.messages = {
        updatequestion: "¿Está seguro de modificar la etiqueta del párrafo?.",
        activequestion: "¿Está seguro de que desea @ la etiqueta del párrafo seleccionado?.",
    };
    this.controls = {
        grids: {
            etiquetasParrafo: new controls.Grid('divEtiquetasParrafo', { maxHeight: 400, pager: { show: false, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [
        { text: 'Etiqueta', field: 'Etiqueta', css: 'col-xs-4' },
        { text: 'Contenido', field: 'Contenido', css: 'col-xs-4' },
        { text: 'Negrita', field: 'Negrita', css: 'col-xs-3' },
        { text: 'Orden', field: 'Orden', css: 'col-xs-3' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'Activo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.etiquetasParrafo.init(columns);
    this.controls.grids.etiquetasParrafo.addListener("onPage", function (evt) {
        self.controls.grids.etiquetasParrafo.currentPage = evt.currentPage;
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
                self.controls.grids.etiquetasParrafo.reload([]);
                self.controls.grids.etiquetasParrafo.reload(data);
            };

            if (data.Message != null & data.Message != "")
                self.showMessage(data.Message);
            loadGrid(data.List);
            self.functions.hideForm();
        }
        else {
            self.showMessage(data.Message);
        }
    };

    //var success = function (data) {
    //    if (data.Result === 1) {
    //        var loadGrid = function (data) {
    //            self.controls.grids.etiquetasParrafo.reload([]);
    //            self.controls.grids.etiquetasParrafo.reload(data);
    //        };
    //        loadGrid(data.List);
    //        self.functions.hideForm();
    //    } else {
    //        self.showMessage(data.Message);
    //    }
    //};

    //$(this.controls.buttons.buscar).click(function () {
    //    var data = {
    //        ObjectResult: {
    //            Activo: $("input[name=estatus]:checked").val(),

    //        }, Paging: {
    //            CurrentPage: self.controls.grids.etiquetasParrafo.currentPage,
    //            Rows: self.controls.grids.etiquetasParrafo.pageSize
    //        }
    //    };

    //    self.SendAjax('POST', self.urls.etiquetasParrafoBuscar, 'json', data, success);//no se ocupa

    //});

    this.controls.grids.etiquetasParrafo.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                debugger;
                var identificador = $("#Identificador").val();
                var ejecutarGuardado = function () {
                    var PagingData = PagingBusqueda(self.controls.grids.etiquetasParrafo.currentPage, self.controls.grids.etiquetasParrafo.pageSize);
                    //$.validator.unobtrusive.parse("#etiquetasParrafoForm");
                    //if ($("#etiquetasParrafoForm").valid()) {
                    var data = {
                        ObjectResult: {
                            Identificador: $("#Identificador", "#etiquetasParrafoForm").val(),
                            IdParrafo: $("#IdParrafo", "#etiquetasParrafoForm").val(),
                            Etiqueta: $("#Etiqueta", "#etiquetasParrafoForm").val(),
                            Contenido: $("#Contenido", "#etiquetasParrafoForm").val(),
                            Negrita: $("#EsNegrita", "#etiquetasParrafoForm").is(':checked'),
                            Orden: $("#Orden", "#etiquetasParrafoForm").val(),
                            FechaInicial: $("#FechaInicial", "#etiquetasParrafoForm").val(),
                            FechaFinal: $("#FechaFinal", "#etiquetasParrafoForm").val(),
                            Activo: $("#EsActivo", "#etiquetasParrafoForm").is(':checked')
                        }, ObjectSearch: {
                            IdParteDocumento: $("#IdParteDocumentoBusqueda").val(),
                            Activo: true
                        }, Paging: PagingData
                    };
                    self.SendAjax('POST', self.urls.etiquetasParrafoGuardar, 'json', data, success);
                };
                $.validator.unobtrusive.parse("#etiquetasParrafoForm");
                if ($("#etiquetasParrafoForm").valid()) {
                    if (parseInt(identificador) > 0)
                        self.confirmacion(self.messages.updatequestion, { title: "Modificación", aceptar: ejecutarGuardado });
                    else
                        ejecutarGuardado();
                }
            });

        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.etiquetasParrafoPorId;
                data = { model: $.extend(evt.dataRow, { Action: evt.event, IdParteDocumento: $("#IdParteDocumentoBusqueda").val()})
                };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.etiquetasParrafoPorId;
                data = { model: { Action: evt.event, IdTipoDocumento: $("#IdTipoDocumento").val(), IdParteDocumento: $("#IdParteDocumentoBusqueda").val()} };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.etiquetasParrafoCambiarEstatus;
                var object = evt.dataRow;
                object.Activo = !object.Activo;
                var actualizarRegistro = function () {
                    var PagingData = PagingBusqueda(self.controls.grids.etiquetasParrafo.currentPage, self.controls.grids.etiquetasParrafo.pageSize);
                    data = {
                        ObjectResult: object,
                        ObjectSearch: {
                            IdParteDocumento: $("#IdParteDocumentoBusqueda").val(),
                            Activo: true
                        },
                        Paging: PagingData
                    };
                    self.SendAjax('POST', url, 'json', data, success);
                }
                var anuncio = self.messages.activequestion.replace("@", (object.Activo == true ? "activar" : "desactivar"));
                self.confirmacion(anuncio, { title: "Cambiar estatus", aceptar: actualizarRegistro })

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
    var page = self.controls.grids.etiquetasParrafo.currentPage;
    var rows = self.controls.grids.etiquetasParrafo.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.etiquetasParrafo.loadRows([]);
        if (data.Result) {
            self.controls.grids.etiquetasParrafo.currentPage = data.Paging.CurrentPage;
            self.controls.grids.etiquetasParrafo.pages = data.Paging.Pages;
            self.controls.grids.etiquetasParrafo.loadRows(data.List);
        }

    };

    var PagingData = PagingBusqueda(self.controls.grids.etiquetasParrafo.currentPage,self.controls.grids.etiquetasParrafo.pageSize);
    var data = {
        ObjectResult: {
            IdParteDocumento: $("#IdParteDocumentoBusqueda").val(),
            Activo: true
        },
        Paging: PagingData
    };

    this.SendAjax('POST', self.urls.etiquetasParrafoConsultaCriterio, 'json', data, loadGrid);
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