Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        AdminPermiso: this.getUrl('/Seguridad/AdminPermiso'),
        AdminPermisosConsultar: this.getUrl('/Seguridad/AdminPermisosConsultar'),
        AdminPermisosGuardar: this.getUrl('/Seguridad/AdminPermisosGuardar')
    };
    this.messages = {
        Error: ""
    };
    this.controls = {
        grids: {
            ListaPermisos: new controls.Grid('divPermisos', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Área', field: 'Usuario', css: 'col-xs-1' },
        { text: 'Rol', field: 'Descripcion', css: 'col-xs-1' },
        { text: 'Menú', field: 'Descripcion', css: 'col-xs-1' },
        { text: 'Submenú', field: 'Descripcion', css: 'col-xs-1' },
        //{ text: 'Tipo de contacto', field: 'Descripcion', css: 'col-xs-1' },
        //{ text: 'Nombre completo', field: 'Descripcion', css: 'col-xs-1' },
        //{ text: 'No.Empleado', field: 'Descripcion', css: 'col-xs-1' },
        //{ text: 'Area', field: 'Descripcion', css: 'col-xs-1' },
        //{ text: 'Jerarquia', field: 'Descripcion', css: 'col-xs-1' },
        //{ text: 'Rol', field: 'Descripcion', css: 'col-xs-1' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.ListaPermisos.init(columns);
    this.controls.grids.ListaPermisos.addListener("onPage", function (evt) {
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
                self.controls.grids.ListaPermisos.reload([]);
                self.controls.grids.ListaPermisos.reload(data);
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

    this.controls.grids.ListaPermisos.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
            });

        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.AdminPermisosGuardar;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.AdminPermiso;
                data = { model: { Action: evt.event } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                //url = self.urls.cambiarestatus;
                //var object = evt.dataRow;
                //object.IsActive = !object.IsActive;
                //var actualizarRegistro = function () {
                //    var data = {
                //        Query: {
                //            IsActive: $("input[name=estatus]:checked").val()
                //        }
                //    };
                //    self.SendAjax('POST', url, 'json', data, success);
                //};

                //var anuncio = self.messages.activequestion.replace("@", (object.IsActive === true ? "activar" : "desactivar"));
                //self.confirmacion(anuncio, { title: "Cambiar estatus", aceptar: actualizarRegistro });

                break;
        }
    });

    this.load();
};

Ui.prototype.load = function (evt) {
    var self = this;
    var loadGrid = function (data) {
        self.controls.grids.ListaPermisos.loadRows([]);
        if (data.Result) {
            self.controls.grids.ListaPermisos.reload(data.List);
        }

    };
    var data = {};

    this.SendAjax('POST', self.urls.AdminPermisoConsultar, 'json', data, loadGrid);
};


function init() {
    var ui = new Ui();
    ui.init();
};

init();



