Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        ConsultaRoles: this.getUrl('/Seguridad/AdminRol'),
        GuardarRol: this.getUrl('/Seguridad/AdminRolGuardar'),
        ObtenerRoles: this.getUrl('/Seguridad/AdminRolConsultar'),
        CambiarEstatusAdminRoles: this.getUrl('/Seguridad/RolCambiarEstatus')
};
    this.messages = {
        Error: "",
        activequestion: "Deceas cambiar el estado del modulo por @ "
    };
    this.controls = {
        grids: {
            ListaRoles: new controls.Grid('divRoles', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            Buscar: $("#btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Area', field: 'Area', css: 'col-xs-2' },
        { text: 'Rol', field: 'Name', css: 'col-xs-2' },
        { text: 'Descripcion', field: 'Descripcion', css: 'col-xs-4' },
        //{ text: 'Descripcion', field: 'Descripcion', css: 'col-xs-1' },
        //{ text: 'Fecha de creacion', field: 'Descripcion', css: 'col-xs-1' },
        //{ text: 'Fecha de baja', field: 'Descripcion', css: 'col-xs-1' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'Activo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.ListaRoles.init(columns);
    this.controls.grids.ListaRoles.addListener("onPage", function (evt) {
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
                self.controls.grids.ListaRoles.reload([]);
                self.controls.grids.ListaRoles.reload(data);
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

    $(this.controls.buttons.Buscar).click(function () {
        var data = {
            Query: {
                IdArea: $("#ddlAreas").val(),
                Externo: $('input[name="rdbTipoRol"]:checked').val(),
                Activo: $('input[name="rdbActivo"]:checked').val(),
                Nombre: $("#txbRol").val()
            }
        };

        self.SendAjax('POST', self.urls.ObtenerRoles, 'json', data, success);

    });

    this.controls.grids.ListaRoles.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#AdminRolForm");
                var valido = $("#AdminRolForm").valid();

                if (valido) {
                    var id = $("#Id").val();
                    var idArea = $("#IdArea").val();
                    var nombre = $("#Name").val();
                    var descripcion = $("#Descripcion").val();

                    var model = {
                        Id: id,
                        Name: nombre,
                        Descripcion: descripcion,
                        IdArea: idArea,
                        Activo: true
                    }

                    self.SendAjax('POST', self.urls.GuardarRol, 'json', model, success);
                }
            });

            $("input[name=rdbTipoRolPa]:radio").change(function () {
                var tipo = $("input[name=rdbTipoRolPa]:checked").val();

                if (tipo === "Externo") {
                    $("#divArea").hide();
                }
                else {
                    $("#divArea").show();
                }
            });

        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.ConsultaRoles;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':

                url = self.urls.ConsultaRoles;
                model = { model: { Action: evt.event } };
                self.SendAjax('POST', url, 'html', model, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.CambiarEstatusAdminRoles;

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

    $("#ddlAreas").select2();
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
        self.controls.grids.ListaRoles.loadRows([]);
        if (data.Result) {
            self.controls.grids.ListaRoles.reload(data.List);
        }

    };
    var data = {
        Query: {
            IdArea: $("#ddlAreas").val(),
            Externo: $("input[name=rdbTipoRol]:checked").val(),
            Activo: $("input[name=rdbActivo]:checked").val(),
            Nombre: $("#txbRol").val()
        }
    };

    this.SendAjax('POST', self.urls.ObtenerRoles, 'json', data, loadGrid);
};


function init() {
    var ui = new Ui();
    ui.init();
};

init();



