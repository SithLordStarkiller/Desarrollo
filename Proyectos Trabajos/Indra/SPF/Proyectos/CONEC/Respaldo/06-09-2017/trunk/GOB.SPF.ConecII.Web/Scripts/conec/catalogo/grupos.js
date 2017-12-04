Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        grupos: this.getUrl('/Catalogo/GrupoConsulta'),
        grupo: this.getUrl('/Catalogo/Grupo'),
        guardar: this.getUrl('/Catalogo/GrupoGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/GrupoCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/GrupoConsultaCriterio')
    };
    this.controls = {
        grids: {
            grupo: new controls.Grid('divGrupo', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'División', field: 'Division', css: 'col-xs-3' },
        { text: 'Nombre', field: 'Name', css: 'col-xs-2' },
        { text: 'Descripción', field: 'Descripcion', css: 'col-xs-4' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.grupo.init(columns);
    this.controls.grids.grupo.addListener("onPage", function (evt) {
        self.controls.grids.grupo.currentPage = evt.currentPage;
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
                self.controls.grids.grupo.reload([]);
                self.controls.grids.grupo.reload(data);
            };
            loadGrid(data.List);
            self.functions.hideForm();
        }
        else {
            self.showMessage(data.Message);

        }
    };

    $(this.controls.buttons.buscar).click(function () {
        debugger;
        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val(),
                IdDivision: $("#DivisionId").val(),
                Identificador: $("#GrupoId").val()
            }, Paging: {
                CurrentPage: self.controls.grids.grupo.currentPage,
                Rows: self.controls.grids.grupo.pageSize
            }
        };

        var x1 = $("#Division").val();
        var x2 = $("#Grupo").val();

        if (x1 == "" && x2 != "") {
            $("#DivisionId").val(0);
        }
        if (x1 != "" && x2 == "") {
            $("#GrupoId").val(0);
        }
        if (x1 == "" && x2 == "") {
            $("#DivisionId").val(0);
            $("#GrupoId").val(0);
        }
        else
        {
            $("#GrupoId").val();        
            $("#DivisionId").val();
        }

        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.grupo.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#grupoForm");
                if ($("#grupoForm").valid()) {
                    var data = {
                        ObjectResult: {
                            IdDivision: $("#Divisiones").val(),
                            Division: $("#Divisiones option:selected").text(),
                            Name: $("#Name").val(),
                            Descripcion: $("#Descripcion").val(),
                            Identificador: $("#Identificador").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.grupo.currentPage,
                            Rows: self.controls.grids.grupo.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
            });

        };

        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.grupo;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.grupo;
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
                        CurrentPage: self.controls.grids.grupo.currentPage,
                        Rows: self.controls.grids.grupo.pageSize
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
    var page = self.controls.grids.grupo.currentPage;
    var rows = self.controls.grids.grupo.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.grupo.loadRows([]);
        if (data.Result) {
            self.controls.grids.grupo.currentPage = data.Paging.CurrentPage;
            self.controls.grids.grupo.pages = data.Paging.Pages;
            self.controls.grids.grupo.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.grupos, 'json', data, loadGrid);
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