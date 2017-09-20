Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        clasificacionesFactor: this.getUrl('/Catalogo/ClasificacionFactorConsulta'),
        clasificacionFactor: this.getUrl('/Catalogo/ClasificacionFactor'),
        guardar: this.getUrl('/Catalogo/ClasificacionFactorGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/ClasificacionFactorCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/ClasificacionFactorConsultaCriterio')
    };
    this.messages = {
        updatequestion: "Al realizar esta acción afectará la administración de los Factores. ¿Está seguro de modificar la clasificación?.",
        activequestion: "Al realizar esta acción afectará la administración de los Factores. ¿Está seguro de que desea @ la clasificación seleccionado?.",
    };
    this.controls = {
        grids: {
            clasificacionFactor: new controls.Grid('divClasificacionFactor', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-3' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-6' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.clasificacionFactor.init(columns);
    this.controls.grids.clasificacionFactor.addListener("onPage", function (evt) {
        self.controls.grids.clasificacionFactor.currentPage = evt.currentPage;
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
                self.controls.grids.clasificacionFactor.reload([]);
                self.controls.grids.clasificacionFactor.reload(data);
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
        var x1 = $("#AutComCF").val();

        if (x1 == "")
        {
            $("#ClasificacionId").val(0);
        }

        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val(),
                Identificador: $("#ClasificacionId").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.clasificacionFactor.currentPage,
                Rows: self.controls.grids.clasificacionFactor.pageSize
            }
        };

        $("#ClasificacionId").val(0);
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.clasificacionFactor.addListener("onButtonClick", function (evt) {
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
                            Nombre: $("#Nombre").val(),
                            Descripcion: $("#Descripcion").val(),
                            Identificador: $("#Identificador").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.clasificacionFactor.currentPage,
                            Rows: self.controls.grids.clasificacionFactor.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
                $.validator.unobtrusive.parse("#clasificacionFactorForm");
                if ($("#clasificacionFactorForm").valid()) {
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
                url = self.urls.clasificacionFactor;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.clasificacionFactor;
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
                            CurrentPage: self.controls.grids.clasificacionFactor.currentPage,
                            Rows: self.controls.grids.clasificacionFactor.pageSize
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
    $("#Clasificaciones").select2();
};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.clasificacionFactor.currentPage;
    var rows = self.controls.grids.clasificacionFactor.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.clasificacionFactor.loadRows([]);
        if (data.Result) {
            self.controls.grids.clasificacionFactor.currentPage = data.Paging.CurrentPage;
            self.controls.grids.clasificacionFactor.pages = data.Paging.Pages;
            self.controls.grids.clasificacionFactor.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.clasificacionesFactor, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();