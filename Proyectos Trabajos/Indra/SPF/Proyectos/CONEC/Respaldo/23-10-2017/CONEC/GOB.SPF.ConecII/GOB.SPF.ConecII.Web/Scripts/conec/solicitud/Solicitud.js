Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        factores: this.getUrl('/Catalogo/FactorConsulta'),
        factor: this.getUrl('/Catalogo/Factor'),
        guardar: this.getUrl('/Catalogo/FactorGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/FactorCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/FactorConsultaCriterio')
    };
    this.messages = {
        updatequestion: "¿Está seguro que desea actualizar la cuota del factor?.",
        activequestion: "Al realizar esta acción afectará a la generación de las cotizaciones y la generación de recibos. “¿Está seguro de que desea @ el factor seleccionado?.",
    };
    this.controls = {
        grids: {
            factor: new controls.Grid('divSolicitud', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 1, pages: 1 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [
        //{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Folio de la solicitud', field: 'TipoServicio', css: 'col-xs-2' },
        { text: 'Tipo de servicio', field: 'ClasificadorFactor', css: 'col-xs-2' },
        { text: 'Razón social', field: 'MedidaCobro', css: 'col-xs-2' },
        { text: 'Nombre Corto', field: 'Nombre', css: 'col-xs-2' },
        { text: 'RFC', field: 'Descripcion', css: 'col-xs-2' },
        { text: 'Fecha de solicitud', field: 'Descripcion', css: 'col-xs-2' },
        { text: 'Estatus', field: 'Descripcion', css: 'col-xs-2' },
        { text: 'Tipo de solicitud', field: 'Descripcion', css: 'col-xs-2' },
        { text: '', field: 'Identificador', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.factor.init(columns);
    this.controls.grids.factor.addListener("onPage", function (evt) {
        self.controls.grids.factor.currentPage = evt.currentPage;
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
                self.controls.grids.factor.reload([]);
                self.controls.grids.factor.reload(data);
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

    $(this.controls.buttons.buscar).click(function () {
        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val(),
                Identificador: $("#Factores option:selected").val(),
                IdTipoServicio: $("#Servicios option:selected").val(),
                IdClasificacionFactor: $("#Clasificaciones option:selected").val(),
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
                var identificador = $("#Identificador").val();
                var ejecutarGuardado = function () {
                    var data = {
                        ObjectResult: {
                            IdTipoServicio: $("#IdTipoServicio").val(),
                            TipoServicio: $("#IdTipoServicio option:selected").text(),
                            IdClasificacionFactor: $("#IdClasificacionFactor").val(),
                            ClasificadorFactor: $("#IdClasificacionFactor option:selected").text(),
                            IdMedidaCobro: $("#IdMedidaCobro").val(),
                            MedidaCobro: $("#IdMedidaCobro option:selected").text(),
                            Nombre: $("#Nombre").val(),
                            Descripcion: $("#Descripcion").val(),
                            CuotaTexto: $("#CuotaTexto").val(),
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
                };

                $.validator.unobtrusive.parse("#factorForm");
                if ($("#factorForm").valid()) {
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
                evt.dataRow.FechaAutorizacion = self.replaceFormaFecha(evt.dataRow.FechaAutorizacion);
                evt.dataRow.FechaEntradaVigor = self.replaceFormaFecha(evt.dataRow.FechaEntradaVigor);
                evt.dataRow.FechaTermino = self.replaceFormaFecha(evt.dataRow.FechaTermino);
                evt.dataRow.FechaPublicacionDof = self.replaceFormaFecha(evt.dataRow.FechaPublicacionDof);

                //evt.dataRow.FechaAutorizacion = $.datepicker.formatDate('dd/mm/yy', new Date(parseInt(evt.dataRow.FechaAutorizacion.substr(6))));
                //evt.dataRow.FechaEntradaVigor = $.datepicker.formatDate('dd/mm/yy', new Date(parseInt(evt.dataRow.FechaEntradaVigor.substr(6))));
                //evt.dataRow.FechaTermino = $.datepicker.formatDate('dd/mm/yy', new Date(parseInt(evt.dataRow.FechaTermino.substr(6))));
                //evt.dataRow.FechaPublicacionDof = $.datepicker.formatDate('dd/mm/yy', new Date(parseInt(evt.dataRow.FechaPublicacionDof.substr(6))));
                url = self.urls.factor;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.factor;
                data = { model: { Action: evt.event } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.cambiarestatus;
                var object = evt.dataRow;
                object.IsActive = !object.IsActive;
                var actualizarRegistro = function () {
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
                };

                var anuncio = self.messages.activequestion.replace("@", (object.IsActive == true ? "activar" : "desactivar"));
                self.confirmacion(anuncio, { title: "Cambiar estatus", aceptar: actualizarRegistro })

                break;
        }
    });

    this.load();
    $("#Servicios").select2();
    $("#Clasificaciones").select2();
    $("#Factores").select2();
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

    this.SendAjax('POST', self.urls.factores, 'json', data, loadGrid);
};


//JZR Controles para cargar Date piker
Ui.prototype.loadControls = function () {
    var self = this;
    //self.FuncionesFechaPiker();
    //JZR
    //Condiciones para fechas minimas o maximas si es que lo requiere el caso de uso
    FechaCondicionMin = "2017-12-12";
    FechaCondicionMax = "2017-12-12";
    //JZR
    //Obtencion de fecha de campos datetime

    var FechaAutorizacion = $("#FechaAutorizacion").val();
    var FechaEntradaVigor = $("#FechaEntradaVigor").val();
    var FechaTermino = $("#FechaTermino").val();
    var FechaPublicacionDof = $("#FechaPublicacionDof").val();

    //Formato de fechas para los campos date llama una funcion "replaceFechaHora" 
    //Parametro de entrada : fecha del campo date
    //Retorno la fecha cn formato deacuerdo al caso de uso

    $("#FechaAutorizacion").val(self.replaceFechaHora(FechaAutorizacion));
    $("#FechaEntradaVigor").val(self.replaceFechaHora(FechaEntradaVigor));
    $("#FechaTermino").val(self.replaceFechaHora(FechaTermino));
    $("#FechaPublicacionDof").val(self.replaceFechaHora(FechaPublicacionDof));

    //Creacionde date piker 
    self.DatepikerNormal();
    self.DatepikerMinDateActual();
    self.DatepikerMinDateActualsimple();
    self.DatepikerMinDateCondicion(FechaCondicionMin);
    self.DatepikerMaxDateActual();
    self.DatepikerMaxDateCondicion(FechaCondicionMax);

};

Ui.prototype.DatepikerNormal = function () {

    $('.datepickerFechaActua').datepicker({
        dateFormat: "dd/mm/yyyy"
    });
    $(".datepickerFechaActua").attr('readonly', true);

}

Ui.prototype.DatepikerMinDateActual = function () {

    $('.datepickerMinFechaActua').datepicker({
        dateFormat: "dd/mm/yyyy",
        minDate: new Date()
    });
    $(".datepickerMinFechaActua").attr('readonly', true);

}

Ui.prototype.DatepikerMaxDateActual = function () {

    $('.datepickerMaxFechaActua').datepicker({
        dateFormat: "dd/mm/yyyy",
        maxDate: new Date()
    });
    $(".datepickerMaxFechaActua").attr('readonly', true);
}

Ui.prototype.DatepikerMinDateCondicion = function (FechaCondicionMin) {

    $('.datepickerMinFechaCondicion').datepicker({
        dateFormat: "dd/mm/yyyy",
        minDate: FechaCondicionMin
    });
    $(".datepickerMinFechaCondicion").attr('readonly', true);

}

Ui.prototype.DatepikerMaxDateCondicion = function (FechaCondicionMax) {

    $('.datepickerMaxFechaCondicion').datepicker({
        dateFormat: "dd/mm/yyyy",
        minDate: FechaCondicionMin
    });
    $(".datepickerMaxFechaCondicion").attr('readonly', true);

}

Ui.prototype.replaceFechaHora = function (fecha) {
    if (fecha != null) {
        resplacefecha = fecha.replace(" 12:00:00 a.m.", "");
        resplacefecha = fecha.replace(" 0:00:00", "");
        var res = resplacefecha.split("/");
        var fechaDate = res[2] + "/" + res[1] + "/" + res[0];
        //var nuevafecha = new Date(fechaDate)
        //var resfechaformat = nuevafecha.getFullYear() + "/" + ("0" + (nuevafecha.getMonth() + 1)).slice(-2) + "/" + ("0" + nuevafecha.getDate()).slice(-2);
    }
    return fechaDate;
}

Ui.prototype.replaceFormaFecha = function (cellValue) {
    var self = this;
    if (cellValue instanceof Date) {
        resfechaformat = cellValue.getFullYear() + "/" + ("0" + (cellValue.getMonth() + 1)).slice(-2) + "/" + ("0" + cellValue.getDate()).slice(-2);
        return cellValue;
    }

    cellValue = cellValue.replace(/</g, '&lt;').replace(/>/g, '&gt;')
    if (cellValue.indexOf('/Date(') !== -1) {
        var date = cellValue.replace(/\/Date\(|\)\//g, '');
        var newDate = new Date(parseInt(date));
        return newDate;
    }
}

Ui.prototype.DatepikerMinDateActualsimple = function () {
    var self = this;
    $('.datepickerMinFechaActuaSimple').datepicker({
        dateFormat: "dd/mm/yyyyy",
        minDate: new Date()
    });
    $(".datepickerMinFechaActuaSimple").val('');
}

function init() {
    var ui = new Ui();
    ui.init();
};

init();