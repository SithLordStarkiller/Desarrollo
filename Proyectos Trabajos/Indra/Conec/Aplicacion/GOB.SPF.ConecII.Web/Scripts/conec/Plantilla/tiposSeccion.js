Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        tiposSeccionConsulta: this.getUrl('/TiposSeccion/Consulta'),
        tiposSeccionConsultaCriterio: this.getUrl('/TiposSeccion/ConsultaCriterio'),
        tiposSeccionPorId: this.getUrl('/TiposSeccion/Item'),
        tiposSeccionGuardar: this.getUrl('/TiposSeccion/Guardar'),
        tiposSeccionCambiarEstatus: this.getUrl('/TiposSeccion/CambiarEstatus'),
    };
    this.messages = {
        updatequestion: "¿Está seguro de modificar la institución?.",
        activequestion: "¿Está seguro de que desea @ la institución seleccionada?.",
    };
    this.controls = {
        grids: {
            tiposSeccion: new controls.Grid('divTiposSeccion', { maxHeight: 400, pager: { show: false, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-4' },
        //{ text: 'Negrita', field: 'Negrita', css: 'col-xs-3' },
        //{ text: 'Orden', field: 'Orden', css: 'col-xs-3' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'Activo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.tiposSeccion.init(columns);
    this.controls.grids.tiposSeccion.addListener("onPage", function (evt) {
        self.controls.grids.tiposSeccion.currentPage = evt.currentPage;
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
                self.controls.grids.tiposSeccion.reload([]);
                self.controls.grids.tiposSeccion.reload(data);
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
    var success = function (data) {
        debugger;
        if (data.Result === 1) {
            var loadGrid = function (data) {
                self.controls.grids.tiposSeccion.reload([]);
                self.controls.grids.tiposSeccion.reload(data);
            };
            loadGrid(data.List);
            self.functions.hideForm();
        } else {
            self.showMessage(data.Message);
        }
    };

    $(this.controls.buttons.buscar).click(function () {
        debugger;
        var data =
            {

                ObjectResult:
                    {
                        //Activo: $("input[name=estatus]:checked").val(),
                        Activo: $("#NumeradoBuscar").val(),
                    },
                Paging:
                    {
                        CurrentPage: self.controls.grids.tiposSeccion.currentPage,
                        Rows: self.controls.grids.tiposSeccion.pageSize
                    }
            };
        self.SendAjax('POST', self.urls.tiposSeccionConsultaCriterio, 'json', data, success);//no se ocupa

    });

    this.controls.grids.tiposSeccion.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            debugger;
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                debugger;
                var identificador = $("#Identificador").val();
                var ejecutarGuardado = function () {
                    var PagingData = PagingBusqueda(self.controls.grids.tiposSeccion.currentPage, self.controls.grids.tiposSeccion.pageSize);
                    var data =
                        {
                            ObjectResult:
                                {
                                    Identificador: $("#Identificador", "#tipoSeccionForm").val(),
                                    Nombre: $("#Nombre", "#tipoSeccionForm").val(),
                                    Descripcion: $("#Descripcion", "#tipoSeccionForm").val(),
                                    Orden: $("#Orden", "#tipoSeccionForm").val(),
                                    Numerado: $("#Numerado", "#tipoSeccionForm").val(),
                                    Mensaje: $("#Mensaje", "#tipoSeccionForm").val(),
                                    Activo: $("#EsActivo", "#tipoSeccionForm").val()
                                },
                            ObjectSearch:
                                {
                                    IdParteDocumento: 1,
                                    Activo: true
                                },
                            Paging: PagingData
                        };

                    self.SendAjax('POST', self.urls.tiposSeccionGuardar, 'json', data, success);
                };
                //$.validator.unobtrusive.parse("#tipoSeccionForm");
                if ($("#tipoSeccionForm").valid()) {
                    if (parseInt(identificador) > 0)
                        ejecutarGuardado();
                        //self.confirmacion(self.messages.updatequestion, { title: "Modificación", aceptar: ejecutarGuardado });
                    else
                        ejecutarGuardado();
                }
            });

        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                debugger;
                url = self.urls.tiposSeccionPorId;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.tiposSeccionPorId;
                data = { model: { Action: evt.event, IdParteDocumento: $("#IdParteDocumentoBusqueda").val() } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.tiposSeccionCambiarEstatus;
                var object = evt.dataRow;
                object.Activo = !object.Activo;
                var actualizarRegistro = function () {
                    var PagingData = PagingBusqueda(self.controls.grids.tiposSeccion.currentPage, self.controls.grids.tiposSeccion.pageSize);
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
    var page = self.controls.grids.tiposSeccion.currentPage;
    var rows = self.controls.grids.tiposSeccion.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.tiposSeccion.loadRows([]);
        if (data.Result) {
            self.controls.grids.tiposSeccion.currentPage = data.Paging.CurrentPage;
            self.controls.grids.tiposSeccion.pages = data.Paging.Pages;
            self.controls.grids.tiposSeccion.loadRows(data.List);
        }

    };

    var PagingData = PagingBusqueda(self.controls.grids.tiposSeccion.currentPage, self.controls.grids.tiposSeccion.pageSize);
    var data = {
        ObjectResult: {
            Activo: true
        },
        Paging: PagingData
    };

    this.SendAjax('POST', self.urls.tiposSeccionConsultaCriterio, 'json', data, loadGrid);
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