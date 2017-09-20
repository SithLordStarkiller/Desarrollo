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
            factor: new controls.Grid('divFactor', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 }, showPlusButton: true })
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
                };

                $.validator.unobtrusive.parse("#factorForm");
                if ($("#factorForm").valid()){
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

function init() {
    var ui = new Ui();
    ui.init();
};

init();