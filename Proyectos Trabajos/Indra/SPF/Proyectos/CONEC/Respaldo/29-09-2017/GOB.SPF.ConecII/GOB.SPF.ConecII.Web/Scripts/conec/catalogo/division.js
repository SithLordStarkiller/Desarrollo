Ui.prototype.init = function () {
    
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        divisiones: this.getUrl('/Catalogo/DivisionesConsulta'),
        division: this.getUrl('/Catalogo/Division'),
        guardar: this.getUrl('/Catalogo/DivisionGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/DivisionCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/DivisionesConsultaCriterio')
    };
    this.messages = {
        updatequestion: "Al realizar esta acción afectará la administración de grupos, fracciones y factores de actividad económica. ¿Está seguro de modificar el registro de la división?.",
        activequestion: "Al realizar esta acción afectará la administración de grupos, fracciones y factores de actividad económica. ¿Está seguro de que desea @ la división seleccionada?.",
    };
    this.controls = {
        grids: {
            division: new controls.Grid('divDivision', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
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




    this.controls.grids.division.init(columns);
    this.controls.grids.division.addListener("onPage", function (evt) {
        self.controls.grids.division.currentPage = evt.currentPage;
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
                self.controls.grids.division.reload([]);
                self.controls.grids.division.reload(data);
            };

            if (data.Message!= null & data.Message != "")
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
                Identificador: $("#Division").val()
            }, Paging: {
                CurrentPage: self.controls.grids.division.currentPage,
                Rows: self.controls.grids.division.pageSize
            }
        };

        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.division.addListener("onButtonClick", function (evt) {
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
                            Identificador: identificador
                        }, Paging: {
                            CurrentPage: self.controls.grids.division.currentPage,
                            Rows: self.controls.grids.division.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                };
                $.validator.unobtrusive.parse("#divisionForm");

                if ($("#divisionForm").valid()) {
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
                url = self.urls.division;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.division;
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
                                ObjectResult: {
                                    EsActivo : object.IsActive
                                },
                                Paging: {
                                            CurrentPage: self.controls.grids.division.currentPage,
                                            Rows: self.controls.grids.division.pageSize
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
    $("#Division").select2();
};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.division.currentPage;
    var rows = self.controls.grids.division.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.division.loadRows([]);
        if (data.Result) {
            self.controls.grids.division.reload(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.divisiones, 'json', data, loadGrid);
};


function init() {
    var ui = new Ui();
    ui.init();
};

init();



