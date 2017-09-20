Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        AreasValidadoras: this.getUrl('/Configuracion/AreasValidadorasConsulta'),
        AreaValidadora: this.getUrl('/Configuracion/AreasValidadora'),
        AgregarValidador: this.getUrl('Configuracion/AgregarValidador'),
        guardar: this.getUrl('/Configuracion/AreasValidadorasGuardar'),
        ActualizarValidadorasGuardar: this.getUrl('ActualizarValidadorasGuardar'),
        cambiarestatus: this.getUrl('/Configuracion/AreasValidadorasCambiarEstatus'),
        buscar: this.getUrl('/Configuracion/AreasValidadoraCriterio'),
        ObtenerValidadores: this.getUrl('/Configuracion/ObtenerValidadores'),
        QuitarValidador: this.getUrl('Configuracion/QuitarValidador')
    };
    this.messages = {
        CamiarEstado: "Deceas modificar a @ el estado en la configuracion del servicio",
        activequestion: "Al realizar esta acción afectará a la administración de cuotas, recibos u hoja de ayuda e5 ¿Está seguro de que desea @ la clave de referencia seleccionada?.",
    };
    this.controls = {
        grids: {
            GridAreasValidadoras: new controls.Grid('divAreasValidadoras', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-2', visible: true },
        { text: 'Tipo de servicio', field: 'TipoServicio', css: 'col-xs-6' },
        // { text: 'Actividad', field: 'Actividades', css: 'col-xs-4' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'EsActivo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.GridAreasValidadoras.init(columns);
    this.controls.grids.GridAreasValidadoras.addListener("onPage", function (evt) {
        self.controls.grids.GridAreasValidadoras.currentPage = evt.currentPage;
        self.load();
    });

    this.functions = {
        hideGrid: function (data) {
            $(".flip div.front").slideToggle(),
            $(".flip div.back").html(data).fadeIn();
        },
        hideForm: function (data) {
            $(".flip div.back div").remove().slideToggle(),
            $(".flip div.front").fadeIn();
        }

    };
    var success = function (data) {
        if (data.Result === 1) {
            var loadGrid = function (data) {
                self.controls.grids.GridAreasValidadoras.reload([]);
                self.controls.grids.GridAreasValidadoras.reload(data);
            };

            if (data.Message != null & data.Message != "")
                self.showMessage(data.Message);

            loadGrid(data.List);
            self.functions.hideForm();
        } else {
            self.showMessage(data.Message);
        }
    };

    var successGuardar = function (data) {
        if (data.Result === 1) {
            if (data.Message != null & data.Message !== "")
                self.showMessage(data.Message);

            self.functions.hideForm();
        } else {
            self.showMessage(data.Message);
        }

        //location.reload();
    };

    $(this.controls.buttons.buscar).click(function () {
        
        var data = {
            Query: {
                EsActivo: $("input[name=estatus]:checked").val()
            }
        };

        self.SendAjax('POST', self.urls.AreasValidadoras, 'json', data, success);

    });

    this.controls.grids.GridAreasValidadoras.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                var ejecutarGuardado = function () {

                    self.SendAjax('POST', '../Configuracion/AreasValidadorasGuardar', 'json', data, successGuardar);
                };

                ejecutarGuardado();
            });
            $('#btnUpdate').on('click', function () {
                var ejecutarGuardado = function () {

                    self.SendAjax('POST', '../Configuracion/AreasValidadorasActualizar', 'json', data, successGuardar);
                };

                ejecutarGuardado();
            });

            self.gridValidadores(data);
        };


        switch (evt.event) {
            case 'Edit':
                url = self.urls.AreaValidadora;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'View':
                url = self.urls.AreaValidadora;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.AreaValidadora;
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
                            EsActivo: $("input[name=estatus]:checked").val()
                        },
                        ObjectResult: object
                    };
                    self.SendAjax('POST', url, 'json', data, success);
                };

                var anuncio = self.messages.CamiarEstado.replace("@", (object.IsActive === true ? "activar" : "desactivar"));
                self.confirmacion(anuncio, { title: "Cambiar estatus", aceptar: actualizarRegistro });

                break;
        }
    });

    this.load();

};


Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.GridAreasValidadoras.currentPage;
    var rows = self.controls.grids.GridAreasValidadoras.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.GridAreasValidadoras.loadRows([]);
        if (data.Result) {
            self.controls.grids.GridAreasValidadoras.loadRows(data.List);
        }

    };

    var data = {
        Query: {
            EsActivo: $("input[name=estatus]:checked").val()
        },
        ObjectResult: object, page: page, rows: rows
    };

    this.SendAjax('POST', self.urls.AreasValidadoras, 'json', data, loadGrid);
};


function init() {
    var ui = new Ui();
    ui.init();
};
init();