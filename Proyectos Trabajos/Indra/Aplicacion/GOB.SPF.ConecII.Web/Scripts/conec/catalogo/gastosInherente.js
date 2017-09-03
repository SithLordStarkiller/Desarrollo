Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        gastosInherentes: this.getUrl('/Catalogo/GastosInherentesConsulta'),
        gastosInherente: this.getUrl('/Catalogo/GastosInherente'),
        guardar: this.getUrl('/Catalogo/GastosInherenteGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/GastosInherenteCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/GastosInherentesConsultaCriterio')
    };
    this.controls = {
        grids: {
            gastosInherente: new controls.Grid('divGastosInherente', { maxHeight: 400, pager: { show: true, pageSize: 50, currentPage: 1 }, showPlusButton: true })
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
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View'} } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.gastosInherente.init(columns);
    this.controls.grids.gastosInherente.addListener("onPage", function (evt) {
        self.controls.grids.gastosInherente.currentPage = evt.currentPage;
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
                self.controls.grids.gastosInherente.reload([]);
                self.controls.grids.gastosInherente.reload(data);
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
            }, Paging: {
                CurrentPage: self.controls.grids.gastosInherente.currentPage,
                Rows: self.controls.grids.gastosInherente.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.gastosInherente.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};        
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {               
                $.validator.unobtrusive.parse("#gastosInherenteForm");
                if ($("#gastosInherenteForm").valid()) {
                    var data = {
                        ObjectResult: {
                            Name: $("#Name").val(),
                            Descripcion: $("#Descripcion").val(),
                            Identificador: $("#Identificador").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.gastosInherente.currentPage,
                            Rows: self.controls.grids.gastosInherente.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
            });

        };


        switch (evt.event) {            
            case 'Edit':
            case 'View':
                url = self.urls.gastosInherente;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.gastosInherente;
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
                        CurrentPage: self.controls.grids.gastosInherente.currentPage,
                        Rows: self.controls.grids.gastosInherente.pageSize
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
    var page = self.controls.grids.gastosInherente.currentPage;
    var rows = self.controls.grids.gastosInherente.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.gastosInherente.loadRows([]);
        if (data.Result) {
            self.controls.grids.gastosInherente.currentPage = data.Paging.CurrentPage;
            self.controls.grids.gastosInherente.pages = data.Paging.Pages;
            self.controls.grids.gastosInherente.loadRows(data.List);
        }
        
    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.gastosInherentes, 'json', data, loadGrid);
};

Ui.prototype.SendAjax = function (method,url,dataType,data,$function) {
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