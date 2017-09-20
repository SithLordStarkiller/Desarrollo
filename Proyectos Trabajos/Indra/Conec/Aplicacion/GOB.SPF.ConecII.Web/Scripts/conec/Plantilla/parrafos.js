Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        parrafosConsulta: this.getUrl('/Parrafos/Consulta'),
        parrafosConsultaCriterio: this.getUrl('/Parrafos/ConsultaCriterio'),
        parrafosPorId: this.getUrl('/Parrafos/Item'),
        parrafosGuardar: this.getUrl('/Parrafos/Guardar'),
        parrafosCambiarEstatus: this.getUrl('/Parrafos/CambiarEstatus'),
    };
    this.messages = {
        updatequestion: "¿Está seguro de modificar el párrafo?.",
        activequestion: "¿Está seguro de que desea @ el párrafo seleccionado?.",
    };
    this.controls = {
        grids: {
            parrafos: new controls.Grid('divParrafos', { maxHeight: 400, pager: { show: false, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [
        { text: 'Sección', field: 'Nombre', css: 'col-xs-4' },
        { text: 'Texto', field: 'Texto', css: 'col-xs-5' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'Activo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.parrafos.init(columns);
    this.controls.grids.parrafos.addListener("onPage", function (evt) {
        self.controls.grids.parrafos.currentPage = evt.currentPage;
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
                self.controls.grids.parrafos.reload([]);
                self.controls.grids.parrafos.reload(data);
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
    //            self.controls.grids.parrafos.reload([]);
    //            self.controls.grids.parrafos.reload(data);
    //        };
    //        loadGrid(data.List);
    //        self.functions.hideForm();
    //    } else {
    //        self.showMessage(data.Message);
    //    }
    //};

    this.controls.grids.parrafos.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                var identificador = $("#Identificador").val();
                var ejecutarGuardado = function () {
                    var PagingData = PagingBusqueda(self.controls.grids.parrafos.currentPage, self.controls.grids.parrafos.pageSize);
                    //$.validator.unobtrusive.parse("#parrafosForm");
                    //if ($("#parrafosForm").valid()) {
                    var data = {
                        ObjectResult: {
                            Identificador: $("#Identificador", "#parrafosForm").val(),
                            IdParteDocumento: $("#IdParteDocumentoParrafos", "#parrafosForm").val(),
                            IdTipoSeccion: $("#IdTipoSeccion", "#parrafosForm").val(),
                            Nombre: $("#Nombre", "#parrafosForm").val(),
                            Texto: $("#Texto", "#parrafosForm").val(),
                            FechaInicial: $("#FechaInicial", "#parrafosForm").val(),
                            FechaFinal: $("#FechaFinal", "#parrafosForm").val(),
                            Activo: $("#EsActivo", "#parrafosForm").is(':checked')
                        }, ObjectSearch: {
                            IdParteDocumento: $("#IdParteDocumentoBusqueda").val(),
                            Activo: true
                        }, Paging: PagingData
                    };
                    self.SendAjax('POST', self.urls.parrafosGuardar, 'json', data, success);
                };
                $.validator.unobtrusive.parse("#parrafosForm");
                if ($("#parrafosForm").valid()) {
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
                url = self.urls.parrafosPorId;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.parrafosPorId;
                data = { model: { Action: evt.event, IdParteDocumento: $("#IdParteDocumentoBusqueda").val() } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.parrafosCambiarEstatus;
                var object = evt.dataRow;
                object.Activo = !object.Activo;
                var actualizarRegistro = function () {
                    var PagingData = PagingBusqueda(self.controls.grids.parrafos.currentPage, self.controls.grids.parrafos.pageSize);
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
    var page = self.controls.grids.parrafos.currentPage;
    var rows = self.controls.grids.parrafos.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.parrafos.loadRows([]);
        if (data.Result) {
            self.controls.grids.parrafos.currentPage = data.Paging.CurrentPage;
            self.controls.grids.parrafos.pages = data.Paging.Pages;
            self.controls.grids.parrafos.loadRows(data.List);
        }

    };
    var PagingData = PagingBusqueda(self.controls.grids.parrafos.currentPage, self.controls.grids.parrafos.pageSize);
    var data = {
        ObjectResult: {
            IdParteDocumento: $("#IdParteDocumentoBusqueda").val(),
            Activo: true
        },
        Paging: PagingData
    };

    this.SendAjax('POST', self.urls.parrafosConsultaCriterio, 'json', data, loadGrid);
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