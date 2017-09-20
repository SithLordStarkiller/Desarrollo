Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        instalaciones: this.getUrl('/Solicitud/InstalacionConsulta'),
        instalacion: this.getUrl('/Solicitud/Instalacion'),
        guardar: this.getUrl('/Solicitud/InstalacionGuardar'),
        cambiarestatus: this.getUrl('/Solicitud/InstalacionCambiarEstatus'),
        buscar: this.getUrl('/Solicitud/InstalacionConsultaCriterio')
    };
    this.controls = {
        grids: {
            instalacion: new controls.Grid('divInstalaciones', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 2 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [
        { text: 'No. Instalación', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre o Razón Social', field: 'RazonSocial', css: 'col-xs-6' },
        //{ text: 'Nombre corto', field: 'Sector', css: 'col-xs-1' },
        //{ text: 'Zona', field: 'RazonSocial', css: 'col-xs-3' },
        //{ text: 'Estación', field: 'NombreCorto', css: 'col-xs-2' },
        //{ text: 'Entidad Federativa', field: 'NombreCorto', css: 'col-xs-2' },
        //{ text: 'Municipio/Delegación', field: 'NombreCorto', css: 'col-xs-2' },
        { text: '', field: 'Editar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, title: 'Editar', cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'Activo', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }
    ];
    this.controls.grids.instalacion.init(columns);
    this.controls.grids.instalacion.addListener("onPage", function (evt) {
        initializeMaps();
        self.controls.grids.instalacion.currentPage = evt.currentPage;
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
                self.controls.grids.instalacion.reload([]);
                self.controls.grids.instalacion.reload(data);

                window.initMap();
            };
            loadGrid(data.List);
            self.functions.hideForm();
        } else {
            self.showMessage(data.Message);
        }
    };

    $(this.controls.buttons.buscar).click(function () {
        var data = {
            Query: {
                Activo: $("input[name=rdbEstatus]:checked").val(),
                Nombre: $("#txbNombre").val(),
                RazonSocial: $("#txbRazonSocial").val(),
                NombreCorto: $("#tbxNombreCorto").val(),
                RFC: $("#txbRfc").val()
            }, Paging: {
                CurrentPage: self.controls.grids.instalacion.currentPage,
                Rows: self.controls.grids.instalacion.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    function geocodeAddress(geocoder, resultsMap, direccion) {
        geocoder.geocode({ 'address': direccion }, function (results, status) {
            if (status === 'OK') {
                resultsMap.setCenter(results[0].geometry.location);
                var marker = new google.maps.Marker({
                    map: resultsMap,
                    position: results[0].geometry.location
                });
            } else {
                alert('Geocode was not successful for the following reason: ' + status);
            }
        });
    }

    $("#btnGeo").click(function () {
        var direccion = $("#txbDireccion").val();
        var geocoder = new google.maps.Geocoder();
        var mapa = new google.maps.Map($("#map"));
        geocodeAddress(geocoder, mapa, direccion);
    });

    this.controls.grids.instalacion.addListener("onButtonClick", function (evt) {
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
                $.validator.unobtrusive.parse("#clienteForm");
                if ($("#clienteForm").valid()) {
                    var data1 = {
                        ObjectResult: {
                            Identificador: $("#Identificador").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.instalacion.currentPage,
                            Rows: self.controls.grids.instalacion.pageSize
                        }, Query: {
                            IsActive: $("input[name=estatus]:checked").val()
                        }
                    };

                    self.SendAjax('POST', self.urls.guardar, 'json', data1, success);

                }
            });
            self.loadControls();
        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.instalacion;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.instalacion;
                data = { model: { Action: evt.event } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.cambiarestatus;
                var object = evt.dataRow;
                object.IsActive = !object.IsActive;
                data = {
                    Query: {
                        Active: $("input[name=estatus]:checked").val()
                    },
                    ObjectResult: object, Paging: {
                        CurrentPage: self.controls.grids.instalacion.currentPage,
                        Rows: self.controls.grids.instalacion.pageSize
                    }
                };
                self.SendAjax('POST', url, 'json', data, success);
                break;
        }
    });
    this.load();
};

Ui.prototype.loadControls = function () {
    var self = this;
    var destino = "";
    var execute = function (data) {
        if (data.Result === 1) {
            self.loadCombo(data.List, destino);
        }
    };
};

Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.instalacion.currentPage;
    var rows = self.controls.grids.instalacion.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.instalacion.loadRows([]);
        if (data.Result) {
            self.controls.grids.instalacion.currentPage = data.Paging.CurrentPage;
            self.controls.grids.instalacion.pages = data.Paging.Pages;
            self.controls.grids.instalacion.loadRows(data.List);
        }

    };

    var data = {
        page: page, rows: rows,
        Query: {
            IsActive: $("input[name=estatus]:checked").val(),
        },
    };

    this.SendAjax('POST', self.urls.instalaciones, 'json', data, loadGrid);
};

Ui.prototype.SendAjax = function (method, url, dataType, data, $function) {
    $.ajax({
        type: 'POST',
        url: url,
        dataType: dataType,
        data: $.toJSON(data),
        beforeSend: function () { },
        contentType: 'application/json; charset=utf-8',
        success: $function
    });
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();

//function maps() {
//    var direccion = $("#txbDireccion").val();
//    var geocoder = new google.maps.Geocoder();
//    var mapa = new google.maps.Map(document.getElementById('mapa'));

//    geocoder.geocode({ 'address': direccion }, function (results, status) {
//        if (status === 'OK') {
//            mapa.setCenter(results[0].geometry.location);
//            var marker = new google.maps.Marker({
//                map: mapa,
//                position: results[0].geometry.location
//            });
//        } else {
//            alert('Geocode was not successful for the following reason: ' + status);
//        }
//    });

//}