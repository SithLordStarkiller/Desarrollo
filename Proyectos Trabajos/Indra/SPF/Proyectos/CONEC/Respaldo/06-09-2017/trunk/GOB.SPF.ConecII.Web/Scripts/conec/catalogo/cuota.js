Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        cuotas: this.getUrl('/Catalogo/CuotasConsulta'),
        cuota: this.getUrl('/Catalogo/Cuota'),
        guardar: this.getUrl('/Catalogo/CuotaGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/CuotaCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/CuotaConsultaCriterio')
    };
    this.controls = {
        grids: {
            cuota: new controls.Grid('divCuota', { maxHeight: 400, pager: { show: true, pageSize: 3, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Servicio', field: 'TipoServicio', css: 'col-xs-2' },
        { text: 'Referencia', field: 'Referencia', css: 'col-xs-2' },
        { text: 'Dependencia', field: 'Dependencia', css: 'col-xs-2' },
        { text: 'Dep. Cadena', field: 'DescripcionDependencia', css: 'col-xs-2' },
        { text: 'Concepto', field: 'Concepto', css: 'col-xs-3' },
        { text: 'Cuota Base', field: 'CuotaBase', css: 'col-xs-1' },
        { text: 'F. Autorización', field: 'FechaAutorizacion', css: 'col-xs-1', style: 'display:none' },
        { text: 'F.E. Vigor', field: 'FechaEntradaVigor', css: 'col-xs-1', style: 'display:none' },
        //{ text: 'Fecha Termino', field: 'FechaTermino', css: 'col-xs-1', style: 'display:none' },
        //{ text: 'Fecha Publicación', field: 'FechaPublicacionDof', css: 'col-xs-1', style: 'display:none' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.cuota.init(columns);
    this.controls.grids.cuota.addListener("onPage", function (evt) {
        self.controls.grids.cuota.currentPage = evt.currentPage;
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
                self.controls.grids.cuota.reload([]);
                self.controls.grids.cuota.reload(data);
            };
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
            }, Paging: {
                CurrentPage: self.controls.grids.cuota.currentPage,
                Rows: self.controls.grids.cuota.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.cuota.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {

            self.functions.hideGrid(data);
           
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#cuotaForm");
                if ($("#cuotaForm").valid()) {
                    $('.pik').datetimepicker();

                    var data = {
                        ObjectResult: {
                            IdTipoServicio: $("#TiposServicio").val(),
                            TipoServicio: $("#TiposServicio").text(),
                            IdReferencia: $("#Referencia").val(),
                            Referencia: $("#Referencia").text(),
                            IdDependencia: $("#Dependencia").val(),
                            Dependencia: $("#Dependencia").text(),
                            IdJerarquia: $("#Jerarquia").val(),
                            Jerarquia: $("#Jerarquia").text(),
                            IdGrupoTarifario: $("#GrupoTarifario").val(),
                            GrupoTarifario: $("#GrupoTarifario").text(),
                            IdMedidaCobro: $("#MedidaCobro").val(),
                            MedidaCobro: $("#MedidaCobro").text(),
                            Name: $("#Name").val(),
                            Concepto: $("#Concepto").val(),
                            CuotaBase: $("#CuotaBase").val(),
                            Iva: $("#Iva").val(),
                            FechaAutorizacion: $("#FechaAutorizacion").val(),
                            FechaEntradaVigor: $("#FechaEntradaVigor").val(),
                            FechaTermino: $("#FechaTermino").val(),
                            FechaPublicaDof: $("#FechaPublicaDof").val(),
                            Identificador: $("#Identificador").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.cuota.currentPage,
                            Rows: self.controls.grids.cuota.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
            });
            self.loadControls();
            if (data.Result == 1) {
                self.loadCombo(data.List, destino)
            }
        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.cuota;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.cuota;
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
                        CurrentPage: self.controls.grids.cuota.currentPage,
                        Rows: self.controls.grids.cuota.pageSize
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
    var page = self.controls.grids.cuota.currentPage;
    var rows = self.controls.grids.cuota.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.cuota.loadRows([]);
        if (data.Result ) {
            self.controls.grids.cuota.currentPage = data.Paging.CurrentPage;
            self.controls.grids.cuota.pages = data.Paging.Pages;
            self.controls.grids.cuota.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.cuotas, 'json', data, loadGrid);
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

//JZR Controles para cargar Date piker
Ui.prototype.loadControls = function () {
    var self = this;
    FechaCondicionMin = "2017-12-12";
    FechaCondicionMax = "2017-12-12";

    self.DatepikerNormal();
    self.DatepikerMinDateActual();
    self.DatepikerMinDateCondicion(FechaCondicionMin);
    self.DatepikerMaxDateActual();
    self.DatepikerMaxDateCondicion(FechaCondicionMax);

    $('.PickerFechaActua').datepicker('setDate', new Date());

};

Ui.prototype.DatepikerNormal = function () {

    $('.datepickerFechaActua').datepicker({
        dateFormat: 'yy/mm/dd'
    });
}

Ui.prototype.DatepikerMinDateActual = function () {

    $('.datepickerMinFechaActua').datepicker({
        dateFormat: 'yy/mm/dd',
        minDate: new Date()
    });
}

Ui.prototype.DatepikerMaxDateActual = function () {

    $('.datepickerMaxFechaActua').datepicker({
        dateFormat: 'yy/mm/dd',
        maxDate: new Date()
    });
}

Ui.prototype.DatepikerMinDateCondicion = function (FechaCondicionMin) {

    $('.datepickerMinFechaCondicion').datepicker({
        dateFormat: 'yy/mm/dd',
        minDate: FechaCondicionMin
    });
}

Ui.prototype.DatepikerMaxDateCondicion = function (FechaCondicionMax) {

    $('.datepickerMaxFechaCondicion').datepicker({
        dateFormat: 'yy/mm/dd',
        minDate: FechaCondicionMin
    });
}

function init() {
    var ui = new Ui();
    ui.init();
    
};

init();