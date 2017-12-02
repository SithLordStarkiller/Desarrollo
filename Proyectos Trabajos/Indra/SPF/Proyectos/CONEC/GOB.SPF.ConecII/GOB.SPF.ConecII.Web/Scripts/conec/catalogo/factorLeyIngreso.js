Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        factoresLeyIngreso: this.getUrl('/Catalogo/FactorLeyIngresoConsulta'),
        factorLeyIngreso: this.getUrl('/Catalogo/FactorLeyIngreso'),
        guardar: this.getUrl('/Catalogo/FactorLeyIngresoGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/FactorLeyIngresoCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/FactorLeyIngresoConsultaCriterio')
    };
    this.messages = {
        updatequestion: "Al realizar esta acción afectará a la administración de los recibos y cotizaciones. ¿Está seguro de modificar el factor de Ley de Ingresos?.",
        activequestion: "Al realizar esta acción afectará a la administración de los recibos y cotizaciones. ¿Está seguro de que desea @ el factor de Ley de Ingreso seleccionado?.",
    };
    this.controls = {
        grids: {
            factorLeyIngreso: new controls.Grid('divFactorLeyIngreso', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Año', field: 'Anio', css: 'col-xs-3' },
        { text: 'Mes', field: 'Mes', css: 'col-xs-3' },
        { text: 'Factor', field: 'FactorTexto', css: 'col-xs-3' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.factorLeyIngreso.init(columns);
    this.controls.grids.factorLeyIngreso.addListener("onPage", function (evt) {
        self.controls.grids.factorLeyIngreso.currentPage = evt.currentPage;
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
                self.controls.grids.factorLeyIngreso.reload([]);
                self.controls.grids.factorLeyIngreso.reload(data);
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
                IdAnio: $("#Anio option:selected").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.factorLeyIngreso.currentPage,
                Rows: self.controls.grids.factorLeyIngreso.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);
        $("#Anio").val(null);
        $("#Anio").select2().val(null);
    });

    this.controls.grids.factorLeyIngreso.addListener("onButtonClick", function (evt) {
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
                            IdAnio: $("#IdAnio").val(),
                            Anio: $("#IdAnio option:selected").text(),
                            IdMes: $("#IdMes").val(),
                            Mes: $("#IdMes option:selected").text(),
                            FactorTexto: $("#FactorTexto").val(),
                            Identificador: $("#Identificador").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.factorLeyIngreso.currentPage,
                            Rows: self.controls.grids.factorLeyIngreso.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                };

                $.validator.unobtrusive.parse("#factorLeyIngresoForm");
                if ($("#factorLeyIngresoForm").valid()) {
                    if (parseInt(identificador) > 0)
                        self.confirmacion(self.messages.updatequestion, { title: "Modificación", aceptar: ejecutarGuardado });
                    else
                        ejecutarGuardado();
                }
            });
        };


        switch (evt.event) {
            case 'Edit':
                url = self.urls.factorLeyIngreso;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'View':
                url = self.urls.factorLeyIngreso;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.factorLeyIngreso;
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
                            CurrentPage: self.controls.grids.factorLeyIngreso.currentPage,
                            Rows: self.controls.grids.factorLeyIngreso.pageSize
                        }
                    };
                    self.SendAjax('POST', url, 'json', data, success);
                }
                var anuncio = self.messages.activequestion.replace("@", (object.IsActive == true ? "activar" : "desactivar"));
                self.confirmacion(anuncio, { title: "Cambiar estatus", aceptar: actualizarRegistro })

                break;
        }
    });

    this.load();
    $("#Anio").select2();

};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.factorLeyIngreso.currentPage;
    var rows = self.controls.grids.factorLeyIngreso.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.factorLeyIngreso.loadRows([]);
        if (data.Result) {
            self.controls.grids.factorLeyIngreso.currentPage = data.Paging.CurrentPage;
            self.controls.grids.factorLeyIngreso.pages = data.Paging.Pages;
            self.controls.grids.factorLeyIngreso.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.factoresLeyIngreso, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();