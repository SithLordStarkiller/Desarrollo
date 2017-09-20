Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        fracciones: this.getUrl('/Catalogo/FraccionConsulta'),
        fraccion: this.getUrl('/Catalogo/Fraccion'),
        guardar: this.getUrl('/Catalogo/FraccionGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/FraccionCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/FraccionConsultaCriterio'),
        grupos: this.getUrl('/Catalogo/FraccionEnlistarGrupoPorDivision')
    };
    this.messages = {
        updatequestion: "¿Está seguro de modificar la fracción?.",
        activequestion: "¿Está seguro de que desea @ la fracción seleccionada?.",
    };
    this.controls = {
        grids: {
            fraccion: new controls.Grid('divFraccion', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 }, showPlusButton: true })
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
         { text: 'División', field: 'Division', css: 'col-xs-6' },
         { text: 'Grupo', field: 'Grupo', css: 'col-xs-6' },
         { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
         { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
         { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.fraccion.init(columns);
    this.controls.grids.fraccion.addListener("onPage", function (evt) {
        self.controls.grids.fraccion.currentPage = evt.currentPage;
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
                self.controls.grids.fraccion.reload([]);
                self.controls.grids.fraccion.reload(data);
            };
            if (data.Message != null & data.Message != "")
                self.showMessage(data.Message);

            loadGrid(data.List);
            self.functions.hideForm();
        }
        else
        {
            self.showMessage(data.Message);
        }
    };

    $(this.controls.buttons.buscar).click(function () {
        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.fraccion.currentPage,
                Rows: self.controls.grids.fraccion.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.fraccion.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var validacion = "";
        var tipoResultado = null;

        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                var identificador = $("#Identificador").val();
                var ejecutarGuardado = function () {
                    var data1 = {
                        ObjectResult: {
                            Identificador: $("#Identificador").val(),
                            Nombre: $("#Nombre").val(),
                            Descripcion: $("#Descripcion").val(),
                            IdGrupo: $("#IdGrupo").val(),
                            IdDivision: $("#IdDivision").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.fraccion.currentPage,
                            Rows: self.controls.grids.fraccion.pageSize
                        }, Query: {
                            IsActive: $("input[name=estatus]:checked").val(),
                        }
                    };

                    self.SendAjax('POST', self.urls.guardar, 'json', data1, success);
                };

                $.validator.unobtrusive.parse("#fraccionForm");
                if ($("#fraccionForm").valid()) {
                    if (parseInt(identificador) > 0)
                        self.confirmacion(self.messages.updatequestion, { title: "Modificación", aceptar: ejecutarGuardado });
                    else
                        ejecutarGuardado();
                }
            });
            self.loadControls();
        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.fraccion;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.fraccion;
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
                            CurrentPage: self.controls.grids.fraccion.currentPage,
                            Rows: self.controls.grids.fraccion.pageSize
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

};

Ui.prototype.loadControls = function () {
    $("#IdDivision").select2();
    $("#IdGrupo").select2();
    var self = this;
    var destino = "";
    var execute = function (data) {
        if (data.Result == 1) {
            self.loadCombo(data.List, destino)
        }
    };
};
/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.fraccion.currentPage;
    var rows = self.controls.grids.fraccion.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.fraccion.loadRows([]);
        if (data.Result) {
            self.controls.grids.fraccion.currentPage = data.Paging.CurrentPage;
            self.controls.grids.fraccion.pages = data.Paging.Pages;
            self.controls.grids.fraccion.loadRows(data.List);
        }

    };
    
    var data = {
        page: page, rows: rows,
        Query: {
            IsActive: $("input[name=estatus]:checked").val(),
        },
    };

    this.SendAjax('POST', self.urls.fracciones, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();
