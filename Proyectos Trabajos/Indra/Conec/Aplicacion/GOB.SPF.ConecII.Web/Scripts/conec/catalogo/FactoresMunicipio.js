Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.urls = {
        factoresMunicipio: this.getUrl('/Catalogo/FactorMunicipioConsulta'),
        factorMunicipio: this.getUrl('/Catalogo/FactorMunicipio'),
        guardar: this.getUrl('/Catalogo/FactorMunicipioGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/FactorMunicipioCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/FactorMunicipioConsultaCriterio'),
        estado: this.getUrl('/Catalogo/ClasificacionObtieneEstados'),
        municipios: this.getUrl('/Catalogo/IdEstadosObtenerMunicipios/')
    };
    this.controls = {
        grids: {
            factorMunicipio: new controls.Grid('divFactoresMunicipio', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-0', visible: true },
        { text: 'Clasificación', field: 'ClasificadorFactor', css: 'col-xs-2' },
        { text: 'Factor', field: 'Factor', css: 'col-xs-2' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-2' },
        { text: 'Estado', field: 'NomEstado', css: 'col-xs-2' },
        { text: 'Municipios', field: 'MunicipiosGrupo', css: 'col-xs-5' },
        { text: '', field: 'Identificador', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } }];
    this.controls.grids.factorMunicipio.init(columns);
    this.controls.grids.factorMunicipio.addListener("onPage", function (evt) {
        self.controls.grids.factorMunicipio.currentPage = evt.currentPage;
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
                self.controls.grids.factorMunicipio.reload([]);
                self.controls.grids.factorMunicipio.reload(data);
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
                IdEstado: $("#Estados option:selected").val(),
                IdClasificacionFactor: $("#Clasificaciones option:selected").val(),
                IdFactor: $("#Factores option:selected").val(),
                IdMunicipio: $("#Municipios option:selected").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.factorMunicipio.currentPage,
                Rows: self.controls.grids.factorMunicipio.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.factorMunicipio.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#factorMunicipioForm");
                if ($("#factorMunicipioForm").valid()) {

                    var getMunicipios = function (estados) {
                        var array = [];
                        estados.each(function (i, option) {
                            array.push({ Identificador: option.value });
                        });
                        return array;
                    };

                    var data = {
                        ObjectResult: {
                            Identificador: $("#Identificador").val(),
                            IdFactor: $("#IdFactor").val(),
                            IdClasificacionFactor: $("#IdClasificacionFactor").val(),
                            Descripcion: $("#Descripcion").val(),
                            IdEstado: $("#IdEstado").val(),
                            Municipios: getMunicipios($("#MunicipiosDestino").find("option"))
                        }, Paging: {
                            CurrentPage: self.controls.grids.factorMunicipio.currentPage,
                            Rows: self.controls.grids.factorMunicipio.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
            });
            self.loadControls();
        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.factorMunicipio;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.factorMunicipio;
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
                        CurrentPage: self.controls.grids.factorMunicipio.currentPage,
                        Rows: self.controls.grids.factorMunicipio.pageSize
                    }
                };
                self.SendAjax('POST', url, 'json', data, success);
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
                url: "/Catalogo/ClasificacionObtieneFactor",
                datatype: "json",
                data: {
                    IdClasificacionFactor: id
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

    $("#Factores").select2();

    $("#Estados").select2().change(function () {
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#Municipios").empty();
            $.ajax({
                //type: "POST",
                url: "/Catalogo/IdEstadosObtenerMunicipios",
                datatype: "json",
                data: {
                    id: id
                },
                traditional: true,
                success: function (data) {

                    var optionhtml = "<option value='0'>Seleccione</option>";

                    $.each(data, function (i) {

                        optionhtml += '<option value="' +
                    data[i].Value + '">' + data[i].Text + '</option>';

                    });

                    $("#Municipios").append(optionhtml);
                }
            });
        }
        else
        {
            $("#Municipios").empty();
            var optionhtml = "<option value='0'>Seleccione</option>";
            $("#Municipios").append(optionhtml);
        }

    });

    $("#Municipios").select2();
};

Ui.prototype.loadControls = function () {

    $("#IdEstado").change(function () {
        var id = $(this).val();

        if (id != "" || id != 0) {
            //debugger;
            $("#Municipios").empty();
            $.ajax({
                //type: "POST",
                url: "/Catalogo/FactoresMunicipioObtenerMunicipios",
                datatype: "json",
                data: {
                    IdClasificacionFactor: $("#IdClasificacionFactor").val(),
                    IdEstado: $("#IdEstado").val(),
                },
                traditional: true,
                success: function (data) {

                    var optionhtml = "";
                    $(data).each(function() {
                        optionhtml += '<option value="' +
                            this.Value +
                            '">' +
                            this.Text +
                            '</option>';
                    });

                    $("#Municipios").append(optionhtml);
                }
            });
        }
    });


    $("#IdClasificacionFactor").on("change", function (evt) {
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#IdFactor").empty();
            $.ajax({
                //type: "POST",
                url: "/Catalogo/ClasificacionObtieneFactor",
                datatype: "json",
                data: {
                    IdClasificacionFactor: id
                },
                traditional: true,
                success: function (data) {

                    var optionhtml = "<option value='0'>Seleccione</option>";

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
        return !$('#Municipios option:selected').remove().appendTo('#MunicipiosDestino');
    });

    $(".pAddAll").click(function () {
        $("#Municipios option").prop('selected', true);
        return !$('#Municipios option:selected').remove().appendTo('#MunicipiosDestino');
    });

    $(".pRemove").click(function () {
        return !$('#MunicipiosDestino option:selected').remove().appendTo('#Municipios');
    });

    $(".pRemoveAll").click(function () {
        $("#MunicipiosDestino option").prop('selected', true);
        return !$('#MunicipiosDestino option:selected').remove().appendTo('#Municipios');
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
    var page = self.controls.grids.factorMunicipio.currentPage;
    var rows = self.controls.grids.factorMunicipio.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.factorMunicipio.loadRows([]);
        if (data.Result) {
            self.controls.grids.factorMunicipio.currentPage = data.Paging.CurrentPage;
            self.controls.grids.factorMunicipio.pages = data.Paging.Pages;
            self.controls.grids.factorMunicipio.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.factoresMunicipio, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();