Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        referencias: this.getUrl('/Catalogo/ReferenciasConsulta'),
        referencia: this.getUrl('/Catalogo/Referencia'),
        guardar: this.getUrl('/Catalogo/ReferenciaGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/ReferenciaCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/ReferenciasConsultaCriterio')
    };
    this.messages = {
        updatequestion: "Al realizar esta acción afectará a la administración de cuotas, recibos u hoja de ayuda e5 ¿Está seguro que desea modificar la clave de referencia?.",
        activequestion: "Al realizar esta acción afectará a la administración de cuotas, recibos u hoja de ayuda e5 ¿Está seguro de que desea @ la clave de referencia seleccionada?.",
    };
    this.controls = {
        grids: {
            referencia: new controls.Grid('divReferencia', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 1, pages: 1 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Clave', field: 'ClaveReferencia', css: 'col-xs-3' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-4' },
        { text: 'Tipo', field: 'TipoProducto', css: 'col-xs-2' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View'} } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.referencia.init(columns);
    this.controls.grids.referencia.addListener("onPage", function (evt) {
        self.controls.grids.referencia.currentPage = evt.currentPage;
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
                self.controls.grids.referencia.reload([]);
                self.controls.grids.referencia.reload(data);
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
                Identificador: $("#Referencias").val()
            }, Paging: {
                CurrentPage: self.controls.grids.referencia.currentPage,
                Rows: self.controls.grids.referencia.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);
        $("#Referencias").val(null);
        $("#Referencias").select2().val(null);
    });

    this.controls.grids.referencia.addListener("onButtonClick", function (evt) {
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
                            ClaveReferencia: $("#ClaveReferencia").val(),
                            Descripcion: $("#Descripcion").val(),
                            Identificador: $("#Identificador").val(),
                            EsProducto: $("#EsProducto").val(),

                        }, Paging: {
                            CurrentPage: self.controls.grids.referencia.currentPage,
                            Rows: self.controls.grids.referencia.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                };

                $.validator.unobtrusive.parse("#referenciaForm");
                if ($("#referenciaForm").valid())
                {
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
                url = self.urls.referencia;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.referencia;
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
                            CurrentPage: self.controls.grids.referencia.currentPage,
                            Rows: self.controls.grids.referencia.pageSize
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
    $("#Referencias").select2();
};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.referencia.currentPage;
    var rows = self.controls.grids.referencia.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.referencia.loadRows([]);
        if (data.Result) {
            self.controls.grids.referencia.currentPage = data.Paging.CurrentPage;
            self.controls.grids.referencia.pages = data.Paging.Pages;
            self.controls.grids.referencia.loadRows(data.List);
        }
        
    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.referencias, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();