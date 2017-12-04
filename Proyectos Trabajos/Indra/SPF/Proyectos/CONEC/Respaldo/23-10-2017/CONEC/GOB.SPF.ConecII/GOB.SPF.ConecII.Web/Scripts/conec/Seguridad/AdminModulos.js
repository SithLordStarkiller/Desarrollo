Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        Modulo: this.getUrl('/Seguridad/AdminModulo'),
        ObtenerModulos: this.getUrl('/Seguridad/AdminModuloConsultar'),
        ObtenerControles: this.getUrl('/Seguridad/ObtenerControles'),
        ObtenerSubModulos: this.getUrl('/Seguridad/ObtenerSubModulos'),
        ObtenerAcciones: this.getUrl('/Seguridad/ObtenerAcciones'),
        CambiarEstatusModulos: this.getUrl('/Seguridad/AdminModuloCambiarEstatus'),
        GuardarModulo: this.getUrl('/Seguridad/AdminModuloGuardar')
    };
    this.messages = {
        AregarControl: "Agregar nuevo control",
        activequestion: "Deceas cambiar el estado del modulo por @ "
    };
    this.controls = {
        grids: {
            ListaModulos: new controls.Grid('divModulos', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            Buscar: $("#btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
        { text: 'Descripcion', field: 'Descripcion', css: 'col-xs-4' },
        { text: 'Controlador', field: 'Controlador', css: 'col-xs-1' },
        { text: 'Accion', field: 'Accion', css: 'col-xs-1' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'Activo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.ListaModulos.init(columns);
    this.controls.grids.ListaModulos.addListener("onPage", function (evt) {
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
                self.controls.grids.ListaModulos.reload([]);
                self.controls.grids.ListaModulos.reload(data.List);
            };
            if (data.Message != null & data.Message !== "")
                self.showMessage(data.Message);
            else
                loadGrid(data);
            self.functions.hideForm();
        }
        else {
            self.showMessage(data.Message);

        }
    };

    $(this.controls.buttons.Buscar).click(function () {
        var data = {
            Query: {
                Activo: $("input[name=rdbActivo]:checked").val(),
                Id: $("#ddlModulo").val(),
                SubModulos: {
                    Id: $("#ddlSubModulo").val()
                }

            }
        };

        self.SendAjax('POST', self.urls.ObtenerModulos, 'json', data, success);

    });

    $("#ddlModulo").change(function () {
        var model = {
            idPadre: $("#ddlModulo").val()
        };

        var getPartial = function(data) {
            $("#ddlSubModulo").html(data);
            $("#ddlSubModulo").val(0).change();
        }

        self.SendAjax('POST', self.urls.ObtenerSubModulos, 'html', model, getPartial);
    });

    $("#ddlModulo").select2();
    $("#ddlSubModulo").select2();

    this.controls.grids.ListaModulos.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {

                var id = $("#Id").val();
                var nombre = $("#Nombre").val();
                var descripcion = $("#Descripcion").val();
                var accion = $("#Accion").val();
                var controlador = $("#Controlador").val();
                var idPadre = $("#IdPadre").val() === 0 ? null : $("#IdPadre").val();
                var controles = self.listaControles;

                var model = {
                    Id: id,
                    Nombre: nombre,
                    Descripcion: descripcion,
                    Controlador: controlador,
                    accion: accion,
                    IdPadre: idPadre,
                    Activo: true,
                    Controles: controles
                }

                self.SendAjax('POST', self.urls.GuardarModulo, 'json', model, success);
            });

            self.grids();

        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.Modulo;
                model = { model: $.extend(evt.dataRow, { Action: evt.event }) };

                self.SendAjax('POST', url, 'html', model, getPartial);
                break;
            case 'New':
                url = self.urls.Modulo;
                data = { model: { Action: evt.event } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.CambiarEstatusModulos;
                var object = evt.dataRow;
                object.Activo = !object.Activo;
                var actualizarRegistro = function () {
                    var data = {
                        model: {
                            Id: object.Id,
                            Activo: object.Activo
                        }
                    };
                    self.SendAjax('POST', url, 'json', data, success);
                };

                var anuncio = self.messages.activequestion.replace("@", (object.Activo === true ? "activar" : "desactivar"));
                self.confirmacion(anuncio, { title: "Cambiar estatus", aceptar: actualizarRegistro });

                break;
        }
    });

    this.load();
};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var loadGrid = function (data) {
        self.controls.grids.ListaModulos.loadRows([]);
        if (data.Result) {
            self.controls.grids.ListaModulos.reload(data.List);
        }

    };
    var data = {};

    this.SendAjax('POST', self.urls.ObtenerModulos, 'json', data, loadGrid);
};


function init() {
    var ui = new Ui();
    ui.init();
};

init();



