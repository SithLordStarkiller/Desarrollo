Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        tiposDocumento: this.getUrl('/Catalogo/TipoDocumentoConsulta'),
        tipoDocumento: this.getUrl('/Catalogo/TipoDocumento'),
        guardar: this.getUrl('/Catalogo/TipoDocumentoGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/TipoDocumentoCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/TipoDocumentoConsultaCriterio'),
    };
    this.controls = {
        grids: {
            tipoDocumento: new controls.Grid('divTipoDocumento', { maxHeight: 400, pager: { show: true, pageSize: 50, currentPage: 1 }, showPlusButton: true })

        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar"),
        }
    };
    var colums = [{ text: 'No.', iskey: true, css: 'col-md-1', visible: true},
    { text: 'Nombre', field: 'Name', css: 'col-md-2'},
    { text: 'Descripción', field: 'Descripcion', css: 'col-md-4'},
    { text: 'Actividad', field: 'Actividad', css: 'col-md-2' },
    { text: 'Confidencial', field: 'Confidencial', css: 'col-md-1'},
    { text: '', field: 'Descripcion', css: 'col-md-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
    { text: '', field: 'Descripcion', css: 'col-md-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
    { text: '', field: 'IsActive', css: 'col-md-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.tipoDocumento.init(colums);
    this.controls.grids.tipoDocumento.addListener("onPage", function (evt) {
        self.controls.grids.tipoDocumento.currentPage = evt.currentPage;
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
                self.controls.grids.tipoDocumento.reload([]);
                self.controls.grids.tipoDocumento.reload(data);
            };
            loadGrid(data.List);
            self.functions.hideForm();
        } else {
            self.showMessage(data.Message);
        }
    };
    $(this.controls.buttons.buscar).click(function () {
        var data = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val(),
                Identificador: $("#IdTipoDocumento").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.tipoDocumento.currentPage,
                Rows: self.controls.grids.tipoDocumento.pageSize
            }
        };
        $("#TipoDocumentoId").val(0);
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);
    });

    this.controls.grids.tipoDocumento.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var getPartial = function (data) {
            self.functions.hideGrid(data);
            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
            });
            $('#btnSave').on('click', function () {
                $.validator.unobtrusive.parse("#tipoDocumentoForm");
                if ($("#tipoDocumentoForm").valid()) {
                    var data = {
                        ObjectResult: {
                            Identificador: $("#Identificador").val(),
                            Name: $("#Name").val(),
                            IdActividad: $("#IdActividad").val(),
                            Confidencial: $("#Confidencial").val(),
                            Descripcion: $("#Descripcion").val()
                        }, Paging: {
                            CurrentPage: self.controls.grids.tipoDocumento.currentPage,
                            Rows: self.controls.grids.tipoDocumento.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                }
            });
        };

        switch (evt.event) {
            case 'Edit':
            case 'View':
                url = self.urls.tipoDocumento;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.tipoDocumento;
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
                        CurrentPage: self.controls.grids.tipoDocumento.currentPage,
                        Rows: self.controls.grids.tipoDocumento.pageSize
                    }
                };
                self.SendAjax('POST', url, 'json', data, success);
                break;
        }
        //$.ajax({
        //    type: 'POST',
        //    url: url,
        //    dataType: 'html',
        //    data: data,
        //    beforeSend: function () { },
        //    contentType: 'application/json; charset=utf-8',
        //    success: function (data) {
        //        self.functions.hideGrid(data);
        //        $('#btnBack').on('click', function () {
        //            self.functions.hideForm(data);
        //        });
        //        $('#btnSave').on('click', function () {
        //            $.validator.unobtrusive.parse("#tipoDocumentoForm");
        //            if ($("#tipoDocumentoForm").valid()) {
        //                var data = {
        //                    Identificador: $("#Identificador").val(),
        //                    Name: $("#Name").val(),
        //                    Actividad: $("IdActividad").val(),
        //                    Confidencial: $("Confidencial").val(),
        //                    Descripcion: $("#Descripcion").val(),                        
                            
        //                };

        //                $.ajax({
        //                    type: 'POST',
        //                    url: self.urls.guardar,
        //                    dataType: 'json',
        //                    data: $.toJSON(data),
        //                    beforeSend: function () { },
        //                    contentType: 'application/json; charset=utf-8',
        //                    success: function (data) {
        //                        if (data.result === 1) {
        //                            var loadGrid = function (data) {
        //                                self.controls.grids.tipoDocumento.reload([]);
        //                                self.controls.grids.tipoDocumento.reload(data);
        //                            };
        //                            loadGrid(data.List);
        //                            self.functions.hideForm();
        //                        }
        //                    }
        //                });
        //            }
        //        });
        //    }
        //});
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
    var page = self.controls.grids.tipoDocumento.currentPage;
    var rows = self.controls.grids.tipoDocumento.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.tipoDocumento.loadRows([]);
        if (data.Result) {
            self.controls.grids.tipoDocumento.currentPage = data.Paging.CurrentPage;
            self.controls.grids.tipoDocumento.pages = data.Paging.Pages;
            self.controls.grids.tipoDocumento.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.tiposDocumento, 'json', data, loadGrid);
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
        $("AutComTD").autocomplete({
            source: "/Catalogo/ObtenerTipoDocumento",
            minLength: 1,
            scroll: true,
            select: function (event, ui) {
                console.log("Selected: " + ui.item.value + " aka " + ui.item.id);
                $("#IdTipoDocumento").val(ui.item.id).attr("selected", "selected").change();
                var id = ui.item.id;
                $("#TipoDocumentoId").val(id);
                console.log("el id es: " + id);
            }
        });
    });
});

init();