Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        factoresEntidadFederativa: this.getUrl('/Catalogo/FactorEntidadFederativaConsulta'),
        factorEntidadFederativa: this.getUrl('/Catalogo/FactorEntidadFederativa'),
        guardar: this.getUrl('/Catalogo/FactorEntidadFederativaGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/FactorEntidadFederativaCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/FactorEntidadFederativaConsultaCriterio'),
    };
    this.controls = {
        grids: {
            factorEntidadFederativa: new controls.Grid('divFactorEntidadFederativa', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-0', visible: true },
        { text: 'Clasificación Factor', field: 'ClasificadorFactor', css: 'col-xs-2' },
        { text: 'No. Zona', field: 'Factor', css: 'col-xs-2' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-3' },
        { text: 'Entidades Federativas', field: 'EntidadesFederativas', css: 'col-xs-5' },
        { text: '', field: 'Identificador', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } }];
    this.controls.grids.factorEntidadFederativa.init(columns);
    this.controls.grids.factorEntidadFederativa.addListener("onPage", function (evt) {
        self.controls.grids.factorEntidadFederativa.currentPage = evt.currentPage;
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
                self.controls.grids.factorEntidadFederativa.reload([]);
                self.controls.grids.factorEntidadFederativa.reload(data);
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
                IdClasificadorFactor: $("#Clasificaciones option:selected").val(),
                IdFactor: $("#Factores option:selected").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.factorEntidadFederativa.currentPage,
                Rows: self.controls.grids.factorEntidadFederativa.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);
        $("#Clasificaciones").val(null);
        $("#Clasificaciones").select2().val(null);
        $("#Factores").val(null);
        $("#Factores").select2().val(null);
        $("#Estado").val(null);
        $("#Estado").select2().val(null);

    });

    this.controls.grids.factorEntidadFederativa.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#factorEntidadFederativaForm");
                if ($("#factorEntidadFederativaForm").valid()) {

                    var getMEntidadesFederativas = function (estados) {
                        var array = [];
                        estados.each(function (i, option) {
                            array.push({ Identificador: option.value });
                        });
                        return array;
                    };

                    var data = {
                        ObjectResult: {
                            IdClasificadorFactor: $("#IdClasificadorFactor").val(),
                            IdFactor: $("#IdFactor").val(),
                            Descripcion: $("#Descripcion").val(),
                            Identificador: $("#Identificador").val(),
                            Estados: getMEntidadesFederativas($("#EstadosDestino").find("option"))
                        }, Paging: {
                            CurrentPage: self.controls.grids.factorEntidadFederativa.currentPage,
                            Rows: self.controls.grids.factorEntidadFederativa.pageSize
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
                url = self.urls.factorEntidadFederativa;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.factorEntidadFederativa;
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
                        CurrentPage: self.controls.grids.factorEntidadFederativa.currentPage,
                        Rows: self.controls.grids.factorEntidadFederativa.pageSize
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

    $("#Factores").select2();
    var optionhtml = "<option value='0'>Seleccione</option>";
    $("#Estado").select2();

};


Ui.prototype.loadControls = function () {
   
        $(".pAdd").click(function (evt) {
            return !$('#Estados option:selected').remove().appendTo('#EstadosDestino');
        });

        $(".pAddAll").click(function (evt) {
            $("#Estados option").prop('selected', true);
            return !$('#Estados option:selected').remove().appendTo('#EstadosDestino');
        });

        $(".pRemove").click(function (evt) {
            return !$('#EstadosDestino option:selected').remove().appendTo('#Estados');
        });

        $(".pRemoveAll").click(function (evt) {
            $("#EstadosDestino option").prop('selected', true);
            return !$('#EstadosDestino option:selected').remove().appendTo('#Estados');
        });

   
  
    $("#IdClasificadorFactor").on("change", function (evt) {
        var id = $(this).val();

        if (id != "" || id != 0) {

           $("#IdFactor").empty();
            $("#Estados").empty();
            $.SendAjax


            $.ajax({
                //type: "POST",
                url: "/Catalogo/ObtenerFactores",
                datatype: "json",
                data: {
                    IdClasificadorFactor: id,

                },
                traditional: true,
                success: function (data) {
                    var optionhtml = "<option value=''>Seleccione</option>";
                    var optionshtml = "";

                    $.each(data, function (i, index) {
                        if (i == "Factores") {
                            for (var dato in index) {
                                //console.log(index[dato]);
                                optionhtml += '<option value="' +
                                    index[dato].Identificador + '">' + index[dato].Nombre + '</option>';
                            }
                        }
                        if (i == "List") {
                            //console.log(index);
                            for (var dato in index) {
                                //console.log(index[dato]);
                                optionshtml += '<option value="' +
                                    index[dato].Identificador + '">' + index[dato].Nombre + '</option>';
                            }
                        }

                    });

                    $("#IdFactor").append(optionhtml);
                    $("#Estados").append(optionshtml);
                }
            });
        }

    });
  

   


    $("#IdFactor").change(function () {
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#Estados").empty();

            $.ajax({
                //type: "POST",
                url: "/Catalogo/ClasificacionYFactorObtieneEstados",
                datatype: "json",
                data: {
                    IdClasificadorFactor: $("#IdClasificadorFactor").val(),
                    IdFactor: id

                },
                traditional: true,
                success: function (data) {

                    var optionhtml = "";

                    $.each(data, function (i) {

                        optionhtml += '<option value="' +
                    data[i].Value + '">' + data[i].Text + '</option>';

                    });

                    $("#Estados").append(optionhtml);
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
    var page = self.controls.grids.factorEntidadFederativa.currentPage;
    var rows = self.controls.grids.factorEntidadFederativa.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.factorEntidadFederativa.loadRows([]);
        if (data.Result) {
            self.controls.grids.factorEntidadFederativa.currentPage = data.Paging.CurrentPage;
            self.controls.grids.factorEntidadFederativa.pages = data.Paging.Pages;
            self.controls.grids.factorEntidadFederativa.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.factoresEntidadFederativa, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();