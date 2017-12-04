Ui.prototype.init = function () {

    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        tiposServicio: this.getUrl('/Catalogo/TiposServicioConsulta'),
        tipoServicio: this.getUrl('/Catalogo/TiposServicio'),
        guardar: this.getUrl('/Catalogo/TiposServicioGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/TiposServicioCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/TiposServicioConsultaCriterio')
    };
    this.messages = {
        updatequestion: "Al realizar esta acción afectará la configuración de los servicios. ¿Está seguro de modificar el tipo de servicio?.",
        activequestion: "Al realizar esta acción afectará la configuración de los servicios. ¿Está seguro de que desea @ el tipo de servicio seleccionado?.",
    };
    this.controls = {
        grids: {
            tipoServicio: new controls.Grid('divTiposServicio', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Name', css: 'col-xs-3' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-5' },
        { text: 'Clave', field: 'Clave', css: 'col-xs-1' },
        { text: '', field: 'Clave', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Clave', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.tipoServicio.init(columns);
    this.controls.grids.tipoServicio.addListener("onPage", function (evt) {
        self.controls.grids.tipoServicio.currentPage = evt.currentPage;
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
                self.controls.grids.tipoServicio.reload([]);
                self.controls.grids.tipoServicio.reload(data);
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
                Identificador: $("#Servicios option:selected").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.tipoServicio.currentPage,
                Rows: self.controls.grids.tipoServicio.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);
        $("#Servicios").val(null);
        $("#Servicios").select2().val(null);
    });

    this.controls.grids.tipoServicio.addListener("onButtonClick", function (evt) {
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
                            Name: $("#Name").val(),
                            Descripcion: $("#Descripcion").val(),
                            Clave: $("#Clave").val(),
                            Identificador: $("#Identificador").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.tipoServicio.currentPage,
                            Rows: self.controls.grids.tipoServicio.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                };
                $.validator.unobtrusive.parse("#tipoServicioForm");

                if ($("#tipoServicioForm").valid()) {
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
                url = self.urls.tipoServicio;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.tipoServicio;
                data = { model: { Action: evt.event } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.cambiarestatus;
                var object = evt.dataRow;
                object.IsActive = !object.IsActive;
                var actualizarRegistro = function () {
                    var data = {
                        Query: {
                            IsActive: $("input[name=estatus]:checked").val(),
                        },
                        ObjectResult: object,
                        Paging: {
                            CurrentPage: self.controls.grids.tipoServicio.currentPage,
                            Rows: self.controls.grids.tipoServicio.pageSize
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
};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.tipoServicio.currentPage;
    var rows = self.controls.grids.tipoServicio.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.tipoServicio.loadRows([]);
        if (data.Result) {
            self.controls.grids.tipoServicio.reload(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.tiposServicio, 'json', data, loadGrid);
};


function init() {
    var ui = new Ui();
    ui.init();
};

init();