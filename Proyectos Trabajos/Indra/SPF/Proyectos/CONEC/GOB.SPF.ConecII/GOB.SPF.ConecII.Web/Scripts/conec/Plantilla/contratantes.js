Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        contratantesConsulta: this.getUrl('/Contratantes/Consulta'),
        contratantesConsultaCriterio: this.getUrl('/Contratantes/ConsultaCriterio'),
        contratantesPorId: this.getUrl('/Contratantes/Item'),
        contratantesGuardar: this.getUrl('/Contratantes/Guardar'),
        contratantesCambiarEstatus: this.getUrl('/Contratantes/CambiarEstatus'),
    };
    this.messages = {
        updatequestion: "¿Está seguro de modificar el contratante?.",
        activequestion: "¿Está seguro de que desea @ el contratante seleccionado?.",
    };
    this.controls = {
        grids: {
            contratantes: new controls.Grid('divContratantes', { maxHeight: 400, pager: { show: false, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-4' },
        { text: 'Cargo', field: 'Cargo', css: 'col-xs-3' },
        { text: 'Domicilio', field: 'Domicilio', css: 'col-xs-3' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'Activo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.contratantes.init(columns);
    this.controls.grids.contratantes.addListener("onPage", function (evt) {
        self.controls.grids.contratantes.currentPage = evt.currentPage;
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
                self.controls.grids.contratantes.reload([]);
                self.controls.grids.contratantes.reload(data);
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
    //            self.controls.grids.contratantes.reload([]);
    //            self.controls.grids.contratantes.reload(data);
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
    //            CurrentPage: self.controls.grids.contratantes.currentPage,
    //            Rows: self.controls.grids.contratantes.pageSize
    //        }
    //    };

    //    self.SendAjax('POST', self.urls.contratantesBuscar, 'json', data, success);//no se ocupa

    //});

    this.controls.grids.contratantes.addListener("onButtonClick", function (evt) {
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
                    var PagingData = PagingBusqueda(self.controls.grids.contratantes.currentPage, self.controls.grids.contratantes.pageSize);
                    
                    var data = {
                        ObjectResult: {
                            Identificador: $("#Identificador", "#contratantesForm").val(),
                            IdParteDocumento: $("#IdParteDocumento", "#contratantesForm").val(),
                            Nombre: $("#Nombre", "#contratantesForm").val(),
                            Cargo: $("#Cargo", "#contratantesForm").val(),
                            Domicilio: $("#Domicilio", "#contratantesForm").val(),
                            FechaInicial: $("#FechaInicial", "#contratantesForm").val(),
                            FechaFinal: $("#FechaFinal", "#contratantesForm").val(),
                            Activo: $("#EsActivo", "#contratantesForm").is(':checked')
                        }, ObjectSearch: {
                            IdParteDocumento: $("#IdParteDocumentoBusqueda").val(),
                            Activo: true
                        }, Paging: PagingData
                    };
                    self.SendAjax('POST', self.urls.contratantesGuardar, 'json', data, success);
                };
                $.validator.unobtrusive.parse("#contratantesForm");
                if ($("#contratantesForm").valid()) {
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
                url = self.urls.contratantesPorId;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.contratantesPorId;
                data = { model: { Action: evt.event, IdParteDocumento: $("#IdParteDocumentoBusqueda").val() } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.contratantesCambiarEstatus;
                var object = evt.dataRow;
                object.Activo = !object.Activo;
                var actualizarRegistro = function () {
                    var PagingData = PagingBusqueda(self.controls.grids.contratantes.currentPage, self.controls.grids.contratantes.pageSize);
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
    var page = self.controls.grids.contratantes.currentPage;
    var rows = self.controls.grids.contratantes.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.contratantes.loadRows([]);
        if (data.Result) {
            self.controls.grids.contratantes.currentPage = data.Paging.CurrentPage;
            self.controls.grids.contratantes.pages = data.Paging.Pages;
            self.controls.grids.contratantes.loadRows(data.List);
        }

    };
    //var data = { page: page, rows: rows };
    
    var PagingData = PagingBusqueda(self.controls.grids.contratantes.currentPage,self.controls.grids.contratantes.pageSize);
    var data = {
        ObjectResult: {
            IdParteDocumento: $("#IdParteDocumentoBusqueda").val(),
            Activo: true
        },
        Paging: PagingData
    };
    this.SendAjax('POST', self.urls.contratantesConsultaCriterio, 'json', data, loadGrid);
    
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