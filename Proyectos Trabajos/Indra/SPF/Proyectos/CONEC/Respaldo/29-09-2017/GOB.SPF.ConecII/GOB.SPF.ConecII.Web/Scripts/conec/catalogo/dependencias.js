Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        dependencias: this.getUrl('/Catalogo/DependenciaConsulta'),
        dependencia: this.getUrl('/Catalogo/Dependencia'),
        guardar: this.getUrl('/Catalogo/DependenciaGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/DependenciaCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/DependenciaConsultaCriterio')
    };
    this.messages = {
        updatequestion: "Al realizar esta acción afectará a la administración de las cuotas, recibos y la emisión de hoja de ayuda e5 ¿Está seguro que desea modificar el registro de la cadena de dependencia?.",
        activequestion: "Al realizar esta acción afectará a la administración de las cuotas, recibos y la emisión de hoja de ayuda e5. ¿Está seguro de que desea @ la cadena seleccionada.?.",
    };
    this.controls = {
        grids: {
            dependencia: new controls.Grid('divDependencias', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Name', css: 'col-xs-3' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-6' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.dependencia.init(columns);
    this.controls.grids.dependencia.addListener("onPage", function (evt) {
        self.controls.grids.dependencia.currentPage = evt.currentPage;
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
                self.controls.grids.dependencia.reload([]);
                self.controls.grids.dependencia.reload(data);
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
                Identificador: $("#Dependencias option:selected").val()
            }, Paging: {
                CurrentPage: self.controls.grids.dependencia.currentPage,
                Rows: self.controls.grids.dependencia.pageSize
            }
        };

        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.dependencia.addListener("onButtonClick", function (evt) {
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
                                Identificador: $("#Identificador").val()
                            }, Paging: {
                                CurrentPage: self.controls.grids.dependencia.currentPage,
                                Rows: self.controls.grids.dependencia.pageSize
                            }
                        };
                        self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                    };
                    
                    $.validator.unobtrusive.parse("#dependenciasForm");
                    if ($("#dependenciasForm").valid()) {
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
                url = self.urls.dependencia;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.dependencia;
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
                            CurrentPage: self.controls.grids.dependencia.currentPage,
                            Rows: self.controls.grids.dependencia.pageSize
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
    $("#Dependencias").select2();
};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.dependencia.currentPage;
    var rows = self.controls.grids.dependencia.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.dependencia.loadRows([]);
        if (data.Result) {
            self.controls.grids.dependencia.currentPage = data.Paging.CurrentPage;
            self.controls.grids.dependencia.pages = data.Paging.Pages;
            self.controls.grids.dependencia.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.dependencias, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();