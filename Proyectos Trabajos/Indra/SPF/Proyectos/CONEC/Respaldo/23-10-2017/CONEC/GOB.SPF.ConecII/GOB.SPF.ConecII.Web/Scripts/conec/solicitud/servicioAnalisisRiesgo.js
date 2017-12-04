 Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var controlsx = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        serviciosAnalisisRiesgo: this.getUrl('/Catalogo/ServicioAnalisisRiesgoConsulta'),
        servicioAnalisisRiesgo: this.getUrl('/Catalogo/ServicioAnalisisRiesgo'),
        instalaciones: this.getUrl('/Catalogo/ServicioAnalisisRiesgo'),
        guardar: this.getUrl('/Catalogo/ServicioAnalisisRiesgoGuardar'),
        buscar: this.getUrl('/Catalogo/ServicioAnalisisRiesgoConsultaCriterio')
    };
    this.controls = {
        grids: {
            servicioAnalisisRiesgo: new controls.Grid('divServicioAnalisisRiesgo', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: false })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    this.controlsx = {
        grids: {
            instalaciones: new controlsx.Grid('divInstalaciones', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: false })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
       { text: 'Nombre Archivo', field: 'ClasificadorFactor', css: 'col-xs-10' },
       { text: 'Eliminar', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.servicioAnalisisRiesgo.init(columns);
    this.controls.grids.servicioAnalisisRiesgo.addListener("onPage", function (evt) {
        self.controls.grids.servicioAnalisisRiesgo.currentPage = evt.currentPage;
        self.load();
    });
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Zona', field: 'Zona', css: 'col-xs-1' },
        { text: 'Estación', field: 'Estacion', css: 'col-xs-1' },
        { text: 'Nombre Instalación', field: 'Nombre', css: 'col-xs-3' },
        { text: 'Estado', field: 'Estado', css: 'col-xs-1' },
        { text: 'Municipio', field: 'Municipio', css: 'col-xs-2' },
        { text: 'Analisis de Riesgo', field: 'AnalisisRiesgo', css: 'col-xs-3' },
        { text: 'Vigencia', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } },
        { text: '', field: 'Consultar', css: 'col-xs-1' }];
    this.controlsx.grids.instalaciones.init(columns);
    this.controlsx.grids.instalaciones.addListener("onPage", function (evt) {
        self.controlsx.grids.instalaciones.currentPage = evt.currentPage;
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
                self.controls.grids.servicioAnalisisRiesgo.reload([]);
                self.controls.grids.servicioAnalisisRiesgo.reload(data);
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
                IdFraccion: $("#Fracciones option:selected").val(),
                IdClasificacionFactor: $("#Clasificaciones option:selected").val(),
                IdFactor: $("#Factores option:selected").val(),
                IdDivision: $("#Divisiones option:selected").val(),
                IdGrupo: $("#Grupos option:selected").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.servicioAnalisisRiesgo.currentPage,
                Rows: self.controls.grids.servicioAnalisisRiesgo.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.servicioAnalisisRiesgo.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#servicioAnalisisRiesgoForm");
                if ($("#servicioAnalisisRiesgoForm").valid()) {

                    var getFracciones = function (estados) {
                        var array = [];
                        estados.each(function (i, option) {
                            array.push({ Identificador: option.value });
                        });
                        return array;
                    };

                    var data = {
                        ObjectResult: {
                            IdClasificacionFactor: $("#IdClasificacionFactor").val(),
                            IdFactor: $("#IdFactor").val(),
                            Identificador: $("#Identificador").val(),
                            Descripcion: $("#Descripcion").val(),
                            Fracciones: getFracciones($("#FraccionesTodos").find("option"))
                        }, Paging: {
                            CurrentPage: self.controls.grids.servicioAnalisisRiesgo.currentPage,
                            Rows: self.controls.grids.servicioAnalisisRiesgo.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
            });
            self.loadControls();

            $('#EstadosDestino option:first-child').attr('selected', 'selected');
        };

        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.servicioAnalisisRiesgo;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.servicioAnalisisRiesgo;
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
                        IsActive: $("input[name=estatus]:checked").val(),
                    },
                    ObjectResult: object, Paging: {
                        CurrentPage: self.controls.grids.servicioAnalisisRiesgo.currentPage,
                        Rows: self.controls.grids.servicioAnalisisRiesgo.pageSize
                    }
                };
                self.SendAjax('POST', url, 'json', data, success);
                break;
        }
    });

    this.load();
    //$("#Clasificaciones").select2().change(function () {
    //    var id = $(this).val();

    //    if (id != "" || id != 0) {

    //        $("#Factores").empty();
    //        $.ajax({
    //            //type: "POST",
    //            url: "/Catalogo/FiltroClasificacionObtieneFactor",
    //            datatype: "json",
    //            data: {
    //                IdClasificadorFactor: id
    //            },
    //            traditional: true,
    //            success: function (data) {

    //                var optionhtml = "<option value='0'>Seleccione</option>";

    //                $.each(data, function (i) {

    //                    optionhtml += '<option value="' +
    //                data[i].Identificador + '">' + data[i].Nombre + '</option>';

    //                });

    //                $("#Factores").append(optionhtml);
    //            }
    //        });
    //    }
    //    else {
    //        $("#Factores").empty();
    //        var optionhtml = "<option value='0'>Seleccione</option>";
    //        $("#Factores").append(optionhtml);
    //    }

    //});

    //$("#Divisiones").select2().change(function () {// divisiones es padre de grupo
    //    var id = $(this).val();

    //    if (id != "" || id != 0) {

    //        $("#Grupos").empty();

    //        $.ajax({
    //            //type: "POST",
    //            url: "/Catalogo/FiltroDivisionObtieneGrupos",
    //            datatype: "json",
    //            data: {
    //                IdDivision: id
    //            },
    //            traditional: true,
    //            success: function (data) {
    //                var optionhtml = "<option value=''>Seleccione</option>";

    //                $.each(data, function (i) {

    //                    optionhtml += '<option value="' +
    //                data[i].Identificador + '">' + data[i].Name + '</option>';

    //                });

    //                $("#Grupos").append(optionhtml);
    //            }
    //        });
    //    }

    //});

};


Ui.prototype.loadControls = function () {

    //$("#IdClasificacionFactor").on("change", function (evt) {
    //    var id = $(this).val();

    //    if (id != "" || id != 0) {

    //        $("#IdFactor").empty();
    //        $.SendAjax


    //        $.ajax({
    //            //type: "POST",
    //            url: "/Catalogo/FiltroClasificacionObtieneFactor",
    //            datatype: "json",
    //            data: {
    //                IdClasificadorFactor: id
    //            },
    //            traditional: true,
    //            success: function (data) {
    //                var optionhtml = "<option value=''>Seleccione</option>";

    //                $.each(data, function (i) {

    //                    optionhtml += '<option value="' +
    //                data[i].Identificador + '">' + data[i].Nombre + '</option>';

    //                });

    //                $("#IdFactor").append(optionhtml);
    //            }
    //        });
    //    }

    //});   

};

/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.servicioAnalisisRiesgo.currentPage;
    var rows = self.controls.grids.servicioAnalisisRiesgo.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.servicioAnalisisRiesgo.loadRows([]);
        if (data.Result) {
            self.controls.grids.servicioAnalisisRiesgo.currentPage = data.Paging.CurrentPage;
            self.controls.grids.servicioAnalisisRiesgo.pages = data.Paging.Pages;
            self.controls.grids.servicioAnalisisRiesgo.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.serviciosAnalisisRiesgo, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();