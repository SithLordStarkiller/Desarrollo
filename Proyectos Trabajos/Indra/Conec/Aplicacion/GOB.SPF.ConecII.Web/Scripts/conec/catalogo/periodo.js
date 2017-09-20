Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        periodos: this.getUrl('/Catalogo/PeriodosConsulta'),
        periodo: this.getUrl('/Catalogo/Periodo'),
        guardar: this.getUrl('/Catalogo/PeriodoGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/PeriodoCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/PeriodosConsultaCriterio')
    };
    this.messages = {
        updatequestion: "Al realizar esta acción afectara la emisión de la hoja de ayuda e5 y recibo. ¿Está seguro de modificar el periodo?.",
        activequestion: "Al realizar esta acción afectara la emisión de la hoja de ayuda e5 y recibo.  ¿Está seguro de que desea @ el periodo seleccionado?.",
    };
    this.controls = {
        grids: {
            periodo: new controls.Grid('divPeriodo', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 }, showPlusButton: true })
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
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View'} } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.periodo.init(columns);
    this.controls.grids.periodo.addListener("onPage", function (evt) {
        self.controls.grids.periodo.currentPage = evt.currentPage;
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
                self.controls.grids.periodo.reload([]);
                self.controls.grids.periodo.reload(data);
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
                Identificador: $("#Periodos option:selected").val()
            }, Paging: {
                CurrentPage: self.controls.grids.periodo.currentPage,
                Rows: self.controls.grids.periodo.pageSize
            }
        };

        

        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.periodo.addListener("onButtonClick", function (evt) {
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
                            CurrentPage: self.controls.grids.periodo.currentPage,
                            Rows: self.controls.grids.periodo.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                };
                $.validator.unobtrusive.parse("#periodoForm");
                if ($("#periodoForm").valid()) {
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
                url = self.urls.periodo;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.periodo;
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
                            CurrentPage: self.controls.grids.periodo.currentPage,
                            Rows: self.controls.grids.periodo.pageSize
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
    $("#Periodos").select2();
};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.periodo.currentPage;
    var rows = self.controls.grids.periodo.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.periodo.loadRows([]);
        if (data.Result) {
            self.controls.grids.periodo.currentPage = data.Paging.CurrentPage;
            self.controls.grids.periodo.pages = data.Paging.Pages;
            self.controls.grids.periodo.loadRows(data.List);
        }
        
    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.periodos, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();