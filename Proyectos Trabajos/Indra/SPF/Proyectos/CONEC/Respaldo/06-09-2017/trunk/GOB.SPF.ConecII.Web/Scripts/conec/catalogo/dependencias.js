Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        dependencias: this.getUrl('/Catalogo/DependenciaConsulta'),
        dependencia: this.getUrl('/Catalogo/Dependencia'),
        guardar: this.getUrl('/Catalogo/DependenciaGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/DependenciaCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/DependenciaConsultaCriterio')
    };
    this.controls = {
        grids: {
            dependencia: new controls.Grid('divDependencias', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 }, showPlusButton: true })
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
    this.controls.grids.dependencia.init(columns);
    this.controls.grids.dependencia.addListener("onPage", function (evt) {
        self.controls.grids.dependencia.currentPage = evt.currentPage;
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
                self.controls.grids.dependencia.reload([]);
                self.controls.grids.dependencia.reload(data);
            };
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
                Identificador: $("#DependenciaId").val()
            }, Paging: {
                CurrentPage: self.controls.grids.dependencia.currentPage,
                Rows: self.controls.grids.dependencia.pageSize
            }
        };

        $("#DependenciaId").val(0);

        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.dependencia.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#dependenciasForm");
                if ($("#dependenciasForm").valid()) {
                    var data = {
                        ObjectResult: {
                            Name: $("#Name").val(),
                            Descripcion: $("#Descripcion").val(),
                            Identificador: $("#Identificador").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.dependencia.currentPage,
                            Rows: self.controls.grids.dependencia.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
            });

        };


        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.dependencia;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.dependencia;
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
                        CurrentPage: self.controls.grids.dependencia.currentPage,
                        Rows: self.controls.grids.dependencia.pageSize
                    }
                };
                self.SendAjax('POST', url, 'json', data, success);
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
    var page = self.controls.grids.dependencia.currentPage;
    var rows = self.controls.grids.dependencia.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.dependencia.loadRows([]);
        if (data.Result) {
            self.controls.grids.dependencia.currentPage = data.Paging.CurrentPage;
            self.controls.grids.dependencia.pages = data.Paging.Pages;
            self.controls.grids.dependencia.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.dependencias, 'json', data, loadGrid);
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

$(document).ready(function () {

    $(function () {
        $("#AutComD").autocomplete({
            source: "/Catalogo/ObtenerDependencia",
            minLength: 1,
            scroll: true,
            select: function (event, ui) {
                console.log("Selected: " + ui.item.value + " aka " + ui.item.id);
                $("#IdDependencia").val(ui.item.id).attr("selected", "selected").change();
                var id = ui.item.id;
                $("#DependenciaId").val(id);
                console.log("el id es: " + id);
            }
        });
    });

});

init();