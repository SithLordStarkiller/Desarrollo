Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        tipoDocumentos: this.getUrl('/Catalogo/TipoDocumentoConsulta'),
        tipoDocumento: this.getUrl('/Catalogo/TipoDocumento'),
        guardar: this.getUrl('/Catalogo/TipoDocumentoGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/TipoDocumentoCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/TipoDocumentoConsultaCriterio')
    };
    this.messages = {
        updatequestion: "¿Está seguro de modificar el tipo de documento?.",
        activequestion: "¿Está seguro de que desea @ el tipo de documento seleccionado?.",
    };
    this.controls = {
        grids: {
            tipoDocumento: new controls.Grid('divTipoDocumento', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 1, pages: 1 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Name', css: 'col-md-2' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-md-4' },
        { text: 'Confidencial', field: 'Confidencial', css: 'col-md-1' },
        { text: '', field: 'Descripcion', css: 'col-md-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Descripcion', css: 'col-md-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-md-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.tipoDocumento.init(columns);
    this.controls.grids.tipoDocumento.addListener("onPage", function (evt) {
        self.controls.grids.tipoDocumento.currentPage = evt.currentPage;
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
                self.controls.grids.tipoDocumento.reload([]);
                self.controls.grids.tipoDocumento.reload(data);
            };

            if (data.Message != null & data.Message != "")
                self.showMessage(data.Message);

            loadGrid(data.List);
            self.functions.hideForm();
        } else {
            self.showMessage(data.Message);
        }
    };
    $(this.controls.buttons.buscar).click(function () {
        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val(),
                Identificador: $("#TipoDocumento").val()
            }, Paging: {
                CurrentPage: self.controls.grids.tipoDocumento.currentPage,
                Rows: self.controls.grids.tipoDocumento.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);
        $("#TipoDocumento").val(null);
        $("#TipoDocumento").select2().val(null);
    });

    this.controls.grids.tipoDocumento.addListener("onButtonClick", function (evt) {
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
                            Identificador: $("#Identificador").val(),
                            Name: $("#Name").val(),
                            Confidencial: $("input[name=Confidencial]:checked").val(),
                            Descripcion: $("#Descripcion").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.tipoDocumento.currentPage,
                            Rows: self.controls.grids.tipoDocumento.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                };

                $.validator.unobtrusive.parse("#tipoDocumentoForm");
                if ($("#tipoDocumentoForm").valid()) {
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
                url = self.urls.tipoDocumento;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.tipoDocumento;
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
                            CurrentPage: self.controls.grids.tipoDocumento.currentPage,
                            Rows: self.controls.grids.tipoDocumento.pageSize
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
    $("#TipoDocumento").select2();
};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.tipoDocumento.currentPage;
    var rows = self.controls.grids.tipoDocumento.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.tipoDocumento.loadRows([]);
        if (data.Result) {
            self.controls.grids.tipoDocumento.currentPage = data.Paging.CurrentPage;
            self.controls.grids.tipoDocumento.pages = data.Paging.Pages;
            self.controls.grids.tipoDocumento.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.tipoDocumentos, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();