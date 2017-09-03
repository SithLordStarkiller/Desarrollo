Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.urls = {
        divisiones: this.getUrl('/Catalogo/FactorMunicipioConsulta'),
        division: this.getUrl('/Catalogo/FactorMunicipio'),
        guardar: this.getUrl('/Catalogo/FactorMunicipioGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/FactorMunicipioCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/FactorMunicipioConsultaCriterio'),
        factores: this.getUrl('/Catalogo/FactorObtenerPorClasificacion'),
        municipios: this.getUrl('/Catalogo/MunicipiosObtener')
    };

    this.controls = {
        grids: {
            division: new controls.Grid('divFactoresMunicipio', { maxHeight: 400, pager: { show: true, pageSize: 50, currentPage: 1 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };

    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Name', css: 'col-xs-3' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-6' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];

    this.controls.grids.division.init(columns);

    this.controls.grids.division.addListener("onPage", function (evt) {
        self.controls.grids.division.currentPage = evt.currentPage;
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
                self.controls.grids.division.reload([]);
                self.controls.grids.division.reload(data);
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
                CurrentPage: self.controls.grids.division.currentPage,
                Rows: self.controls.grids.division.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.division.addListener("onButtonClick", function (evt) {
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
                    var getMunicipios = function (municipios) {
                        var array = [];
                        municipios.each(function (i, option) {
                            array.push({ Identificador: option.value });
                        });
                        return array;
                    };
                    var data = {
                        ObjectResult: {
                            IdFactor: $("#IdFactor").val(),
                            IdClasificacion: $("#IdClasificacion").val(),
                            IdEstado: $("#IdEstado").val(),
                            Municipios : getMunicipios($("#Municipios").find("option"))
                        }, Paging: {
                            CurrentPage: self.controls.grids.division.currentPage,
                            Rows: self.controls.grids.division.pageSize
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
                url = self.urls.division;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.division;
                data = { model: { Action: evt.event } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.cambiarestatus;
                var object = evt.dataRow;
                object.IsActive = !object.IsActive;
                data = {
                    ObjectResult: object, Paging: {
                        CurrentPage: self.controls.grids.division.currentPage,
                        Rows: self.controls.grids.division.pageSize
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

    $("#IdClasificacion").on("change",function (evt) {
        destino = document.getElementById("IdFactor");
        var url = self.urls.factores + "/" + this.value + "/";
        if (this.value != "") {
            $.get(url, execute);
        }        
    });
    $("#IdEstado").on("change", function (evt) {
        destino = document.getElementById("municipios");
        var url = self.urls.municipios + "/" + this.value + "/";
        if (this.value != "") {
            $.get(url, execute);
        }
    });

    $("#btnAddAll, #btnAddItems, #btnDeleteItems, #btnDeleteAll").on("click", function (evt) {
        var origenItems = $("#municipios option:selected");
        var destinoItems = $("#Municipios option:selected");
        var origen = $("#municipios");
        var destino = $("#Municipios");
        var compareOptions = function (a, b) {
            return a.text > b.text ? 1 : a.text < b.text ? -1 : 0;
        };
        var setOptions = function (elements,control) {
            for (var i = 0; i < elements.length; i++) {
                control.append(elements[i]);
            }
            var options = $.extend({},control.find("option").sort(compareOptions),0);
            control.empty();
            for (var i = 0; i < options.length; i++) {
                control.append(options[i]);
            }
        };
        switch (this.id)
        {
            case "btnAddAll":
                var options = origen.find("option");
                setOptions(options, destino);
                break;
            case "btnAddItems":
                setOptions(origenItems, destino);               
                break;
            case "btnDeleteItems":
                setOptions(destinoItems, origen);
                break;
            case "btnDeleteAll":
                var options = destino.find("option");
                setOptions(options, origen);
                break;
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
        $(opcion).data("option",data[i]);
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
    var page = self.controls.grids.division.currentPage;
    var rows = self.controls.grids.division.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.division.loadRows([]);
        if (data.Result) {
            self.controls.grids.division.currentPage = data.Paging.CurrentPage;
            self.controls.grids.division.pages = data.Paging.Pages;
            self.controls.grids.division.loadRows(data.List);
        }

    };
    var data = {
        ObjectResult: {
            IsActive: $("input[name=estatus]:checked").val(),
        }, Paging: {
            CurrentPage: self.controls.grids.division.currentPage,
            Rows: self.controls.grids.division.pageSize
        }
    };

    this.SendAjax('POST', self.urls.buscar, 'json', data, loadGrid);
};

Ui.prototype.SendAjax = function (method, url, dataType, data, $function) {
    $.ajax({
        type: method,
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