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
        factores: this.getUrl('/Catalogo/ClasificacionObtieneFactor'),
        estado: this.getUrl('/Catalogo/ClasificacionObtieneEstados')
    };
    this.controls = {
        grids: {
            factorEntidadFederativa: new controls.Grid('divFactorEntidadFederativa', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Clasificación', field: 'ClasificadorFactor', css: 'col-xs-2' },
        { text: 'Factor', field: 'Factor', css: 'col-xs-2' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-3' },
        { text: 'Estado', field: 'EntidadesFederativas', css: 'col-xs-2' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
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
            loadGrid(data.List);
            self.functions.hideForm();
        }
    };

    $(this.controls.buttons.buscar).click(function () {
        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.factorEntidadFederativa.currentPage,
                Rows: self.controls.grids.factorEntidadFederativa.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

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

};


Ui.prototype.loadControls = function () {
    var self = this;
    var destino = "";

    var execute = function (data) {
        if (data.Result === 1) {
            self.loadCombo(data.List, destino);
        }
    };


    $("#IdClasificadorFactor").on("change", function (evt) {
        destino = document.getElementById("IdFactor");

        var url = self.urls.factores + "/" + this.value + "/";
        if (this.value != "") {
            $.get(url, execute);
        }
    });

    $(".pAdd").click(function () {
        return !$('#Estados option:selected').remove().appendTo('#EstadosDestino');
    });

    $(".pAddAll").click(function () {
        $("#Estados option").prop('selected', true);
        return !$('#Estados option:selected').remove().appendTo('#EstadosDestino');
    });

    $(".pRemove").click(function () {
        return !$('#EstadosDestino option:selected').remove().appendTo('#Estados');
    });

    $(".pRemoveAll").click(function () {
        $("#EstadosDestino option").prop('selected', true);
        return !$('#EstadosDestino option:selected').remove().appendTo('#Estados');
    });


    $("#IdClasificadorFactor").change(function () {
        var id = $(this).val();

        if (id != "" || id != 0) {

            $("#Estados").empty();
            $.ajax({
                //type: "POST",
                url: "/Catalogo/ClasificacionObtieneEstados/",
                datatype: "json",
                data: { id: id },
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


Ui.prototype.loadCombo = function (data, destino) {
    $(destino).empty();
    if (!destino.multiple) {
        var opcion = document.createElement("option");
        opcion.value = "";
        opcion.innerHTML = "Seleccione";
        destino.appendChild(opcion);
    }
    for (var i = 0; i < data.length; i++) {
        var opcion = document.createElement("option");
        opcion.value = data[i].Identificador;
        opcion.innerHTML = data[i].Nombre;
        $(opcion).data("option", data[i]);
        destino.appendChild(opcion);
    };
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