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
    this.controls = {
        grids: {
            factor: new controls.Grid('divFactor', { maxHeight: 400, pager: { show: true, pageSize: 3, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Tipo Servicio', field: 'TipoServicio', css: 'col-xs-2' },
        { text: 'Clasificación', field: 'ClasificadorFactor', css: 'col-xs-2' },
        { text: 'Medida Cobro', field: 'MedidaCobro', css: 'col-xs-2' },
        { text: 'Factor', field: 'Nombre', css: 'col-xs-2' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-2' },
        { text: 'Cuota', field: 'CuotaFactor', css: 'col-xs-1' },
        //{ text: 'Fecha Autorización', field: 'FechaAutorizacion', css: 'col-xs-1', style: 'display:none' },
        //{ text: 'Fecha Entrada Vigor', field: 'FechaEntradaVigor', css: 'col-xs-1', style: 'display:none' },
        //{ text: 'Fecha Termino', field: 'FechaTermino', css: 'col-xs-1', style: 'display:none' },
        //{ text: 'Fecha Publicación', field: 'FechaPublicacionDof', css: 'col-xs-1', style: 'display:none' },
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
                Identificador: $("#FactorId").val(),
                IdTipoServicio: $("#TipoServicioId").val(),
                IdClasificacionFactor: $("#ClasificacionId").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.factor.currentPage,
                Rows: self.controls.grids.factor.pageSize
            }
        };

        var x1 = $("#AutComTS").val();
        var x2 = $("#AutComCF").val();
        var x3 = $("#AutComF").val();

        if (x1 == "") {
            $("#TipoServicioId").val(0);
        }
        if (x2 == "") {
            $("#ClasificacionId").val(0);
        }
        if (x3 == "") {
            $("#FactorId").val(0);
        }

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
                debugger;
                $.validator.unobtrusive.parse("#factorForm");
                if ($("#factorForm").valid()) {
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
                url = self.urls.factor;
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

    this.SendAjax('POST', self.urls.factores, 'json', data, loadGrid);
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

$(document).ready(function () {

    $(function () {
        $("#AutComTS").autocomplete({
            source: "/Catalogo/ObtenerTipoServicio",
            minLength: 1,
            scroll: true,
            select: function (event, ui) {
                console.log("Selected: " + ui.item.value + " aka " + ui.item.id);
                $("#IdTipoServicio").val(ui.item.id).attr("selected", "selected").change();
                var id = ui.item.id;
                $("#TipoServicioId").val(id);
                console.log("el id es: " + id);
            }
        });
    });

    $(function () {
        $("#AutComCF").autocomplete({
            source: "/Catalogo/ObtenerClasificacion",
            minLength: 1,
            scroll: true,
            select: function (event, ui) {
                console.log("Selected: " + ui.item.value + " aka " + ui.item.id);
                $("#IdClasificacion").val(ui.item.id).attr("selected", "selected").change();
                var id = ui.item.id;
                $("#ClasificacionId").val(id);
                console.log("el id es: " + id);
            }
        });
    });

    $(function () {
        $("#AutComF").autocomplete({
            source: "/Catalogo/ObtenerFactor",
            minLength: 1,
            scroll: true,
            select: function (event, ui) {
                console.log("Selected: " + ui.item.value + " aka " + ui.item.id);
                $("#IdFactor").val(ui.item.id).attr("selected", "selected").change();
                var id = ui.item.id;
                $("#FactorId").val(id);
                console.log("el id es: " + id);
            }
        });
    });

});

init();