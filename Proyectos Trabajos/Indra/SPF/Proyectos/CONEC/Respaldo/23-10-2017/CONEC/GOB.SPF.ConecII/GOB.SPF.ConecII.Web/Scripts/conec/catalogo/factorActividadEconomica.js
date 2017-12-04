 Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        factoresActividadEconomica: this.getUrl('/Catalogo/FactorActividadEconomicaConsulta'),
        factorActividadEconomica: this.getUrl('/Catalogo/FactorActividadEconomica'),
        guardar: this.getUrl('/Catalogo/FactorActividadEconomicaGuardar'),
        buscar: this.getUrl('/Catalogo/FactorActividadEconomicaConsultaCriterio'),
        cambiarestatus: this.getUrl('/Catalogo/FactorActividadEconomicaCambiarEstatus')
        
    };
    this.controls = {
        grids: {
            factorActividadEconomica: new controls.Grid('divFactorActividadEconomica', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    this.messages = {
        activequestion: "Al realizar esta acción afectará la configuración de los servicios. ¿Está seguro de que desea @ el tipo de servicio seleccionado?.",
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-0', visible: true },
        { text: 'Clasificación', field: 'ClasificadorFactor', css: 'col-xs-2' },
        { text: 'Factor', field: 'Factor', css: 'col-xs-1' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-2' },
        { text: 'División', field: 'Division', css: 'col-xs-3' },
        { text: 'Grupo', field: 'Grupo', css: 'col-xs-2' },
        { text: 'Actividades', field: 'Actividades', css: 'col-xs-5' },
        { text: '', field: 'Identificador', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } }];
    //{ text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.factorActividadEconomica.init(columns);
    this.controls.grids.factorActividadEconomica.addListener("onPage", function (evt) {
        self.controls.grids.factorActividadEconomica.currentPage = evt.currentPage;
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
                self.controls.grids.factorActividadEconomica.reload([]);
                self.controls.grids.factorActividadEconomica.reload(data);
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
                CurrentPage: self.controls.grids.factorActividadEconomica.currentPage,
                Rows: self.controls.grids.factorActividadEconomica.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);
        $("#Clasificaciones").val(null);
        $("#Clasificaciones").select2().val(null);
        $("#Factores").val(null);
        $("#Factores").select2().val(null);
        $("#Divisiones").val(null);
        $("#Divisiones").select2().val(null);
        $("#Grupos").val(null);
        $("#Grupos").select2().val(null);
    });

    this.controls.grids.factorActividadEconomica.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#factorActividadEconomicaForm");
                if ($("#factorActividadEconomicaForm").valid()) {

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
                            CurrentPage: self.controls.grids.factorActividadEconomica.currentPage,
                            Rows: self.controls.grids.factorActividadEconomica.pageSize
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
                url = self.urls.factorActividadEconomica;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.factorActividadEconomica;
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
                            CurrentPage: self.controls.grids.factorActividadEconomica.currentPage,
                            Rows: self.controls.grids.factorActividadEconomica.pageSize
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
    $("#Clasificaciones").select2().change(function () {
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#Factores").empty();
            $.ajax({
                //type: "POST",
                url: "/Catalogo/FiltroClasificacionObtieneFactor",
                datatype: "json",
                data: {
                    IdClasificadorFactor: id
                },
                traditional: true,
                success: function (data) {

                    var optionhtml = "<option value='0'>Seleccione</option>";

                    $.each(data, function (i) {

                        optionhtml += '<option value="' +
                    data[i].Identificador + '">' + data[i].Nombre + '</option>';

                    });

                    $("#Factores").append(optionhtml);
                }
            });
        }
        else {
            $("#Factores").empty();
            var optionhtml = "<option value='0'>Seleccione</option>";
            $("#Factores").append(optionhtml);
        }

    });

    $("#Divisiones").select2().change(function () {// divisiones es padre de grupo
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#Grupos").empty();

            $.ajax({
                //type: "POST",
                url: "/Catalogo/FiltroDivisionObtieneGrupos",
                datatype: "json",
                data: {
                    IdDivision: id
                },
                traditional: true,
                success: function (data) {
                    var optionhtml = "<option value=''>Seleccione</option>";

                    $.each(data, function (i) {

                        optionhtml += '<option value="' +
                    data[i].Identificador + '">' + data[i].Name + '</option>';

                    });

                    $("#Grupos").append(optionhtml);
                }
            });
        }

    });

    $("#Factores").select2();
    $("#Grupos").select2();

};


Ui.prototype.loadControls = function () {

    $("#IdClasificacionFactor").on("change", function (evt) {
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#IdFactor").empty();
            $.SendAjax


            $.ajax({
                //type: "POST",
                url: "/Catalogo/FiltroClasificacionObtieneFactor",
                datatype: "json",
                data: {
                    IdClasificadorFactor: id
                },
                traditional: true,
                success: function (data) {
                    var optionhtml = "<option value=''>Seleccione</option>";

                    $.each(data, function (i) {

                        optionhtml += '<option value="' +
                    data[i].Identificador + '">' + data[i].Nombre + '</option>';

                    });

                    $("#IdFactor").append(optionhtml);
                }
            });
        }

    });

    $(".pAdd").click(function () {
        return !$('#Fracciones option:selected').remove().appendTo('#FraccionesTodos');
    });

    $(".pAddAll").click(function () {
        $("#Fracciones option").prop('selected', true);
        return !$('#Fracciones option:selected').remove().appendTo('#FraccionesTodos');
    });

    $(".pRemove").click(function () {
        return !$('#FraccionesTodos option:selected').remove().appendTo('#Fracciones');
    });

    $(".pRemoveAll").click(function () {
        $("#FraccionesTodos option").prop('selected', true);
        return !$('#FraccionesTodos option:selected').remove().appendTo('#Fracciones');
    });

    $("#IdDivision").on("change", function (evt) {
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#IdGrupo").empty();
            $.SendAjax


            $.ajax({
                //type: "POST",
                url: "/Catalogo/FiltroDivisionObtieneGrupos",
                datatype: "json",
                data: {
                    IdDivision: id
                },
                traditional: true,
                success: function (data) {
                    var optionhtml = "<option value=''>Seleccione</option>";

                    $.each(data, function (i) {

                        optionhtml += '<option value="' +
                    data[i].Identificador + '">' + data[i].Name + '</option>';

                    });

                    $("#IdGrupo").append(optionhtml);
                }
            });
        }

    });

    $("#IdGrupo").change(function () {// grupo es padre de fracción
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#Fracciones").empty();

            $.ajax({
                //type: "POST",
                url: "/Catalogo/FiltroGrupoObtieneFracciones",
                datatype: "json",
                data: {
                    IdClasificacionFactor: $("#IdClasificacionFactor").val(),
                    IdFactor: $("#IdFactor").val(),
                    IdDivision: $("#IdDivision").val(),
                    IdGrupo: id
                },
                traditional: true,
                success: function (data) {
                    var optionhtml = "";

                    $.each(data, function (i) {
                        optionhtml += '<option value="' +
                    data[i].Identificador + '">' + data[i].Nombre + '</option>';

                    });

                    $("#Fracciones").append(optionhtml);
                }
            });
        }
    });

};

/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.factorActividadEconomica.currentPage;
    var rows = self.controls.grids.factorActividadEconomica.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.factorActividadEconomica.loadRows([]);
        if (data.Result) {
            self.controls.grids.factorActividadEconomica.currentPage = data.Paging.CurrentPage;
            self.controls.grids.factorActividadEconomica.pages = data.Paging.Pages;
            self.controls.grids.factorActividadEconomica.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.factoresActividadEconomica, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();