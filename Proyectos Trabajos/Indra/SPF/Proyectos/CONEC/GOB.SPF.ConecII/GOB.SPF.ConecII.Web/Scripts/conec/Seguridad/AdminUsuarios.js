Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        ConsultaUsuarios: this.getUrl('/Seguridad/ConsultarUsuarios')
    };
    this.messages = {
        Error: ""
    };
    this.controls = {
        grids: {
            ListaUsuarios: new controls.Grid('divUsuarios', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Usuario', field: 'Usuario', css: 'col-xs-1' },
        { text: 'No.Empleado', field: 'Descripcion', css: 'col-xs-1' },
        { text: 'Tipo de usuario', field: 'Descripcion', css: 'col-xs-1' },
        { text: 'Nombre o razon social', field: 'Descripcion', css: 'col-xs-1' },
        { text: 'Tipo de contacto', field: 'Descripcion', css: 'col-xs-1' },
        { text: 'Nombre completo', field: 'Descripcion', css: 'col-xs-1' },
        { text: 'No.Empleado', field: 'Descripcion', css: 'col-xs-1' },
        { text: 'Area', field: 'Descripcion', css: 'col-xs-1' },
        { text: 'Jerarquia', field: 'Descripcion', css: 'col-xs-1' },
        { text: 'Rol', field: 'Descripcion', css: 'col-xs-1' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.ListaUsuarios.init(columns);
    this.controls.grids.ListaUsuarios.addListener("onPage", function (evt) {
        self.load();
    });

    this.functions = {
        hideGrid: function (data) {
            $(".flip div.front").slideToggle();
            $(".flip div.back").html(data).fadeIn();
        },
        hideForm: function (data) {
            $(".flip div.back div").remove().slideToggle();
            $(".flip div.front").fadeIn();
        }

    };

    var success = function (data) {
        if (data.Result === 1) {
            var loadGrid = function (data) {
                self.controls.grids.ListaUsuarios.reload([]);
                self.controls.grids.ListaUsuarios.reload(data);
            };
            if (data.Message != null & data.Message !== "")
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
            }
        };

        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.ListaUsuarios.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function() {
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
                            IsActive: $("input[name=estatus]:checked").val()
                        }
                    };
                    self.SendAjax('POST', url, 'json', data, success);
                };

                var anuncio = self.messages.activequestion.replace("@", (object.IsActive === true ? "activar" : "desactivar"));
                self.confirmacion(anuncio, { title: "Cambiar estatus", aceptar: actualizarRegistro });

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
    var page = self.controls.grids.ListaUsuarios.currentPage;
    var rows = self.controls.grids.ListaUsuarios.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.ListaUsuarios.loadRows([]);
        if (data.Result) {
            self.controls.grids.ListaUsuarios.reload(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.ConsultaUsuarios, 'json', data, loadGrid);
};


function init() {
    var ui = new Ui();
    ui.init();
};

init();



