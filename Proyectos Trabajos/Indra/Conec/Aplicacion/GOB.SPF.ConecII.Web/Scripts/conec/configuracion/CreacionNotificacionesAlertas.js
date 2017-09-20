Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.urls = {
        ObtenerIntegrantes: this.getUrl('/Configuracion/ConsultaIntegrantes'),
        AgregarReceptores: this.getUrl('/Configuracion/AgregarReceptor'),
        QuitarReceptores: this.getUrl('/Configuracion/QuitarReceptor'),
        CrearNotificacion: this.getUrl('/Configuracion/CrearNotificacion')
    };

    this.controls = {
        grids: {
            GridIntegrantesArea: new controls.Grid('divIntegrantesRol', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 } }),
            GridIntegrantes: new controls.Grid('divIntegrantes', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 } }),
            GridReceptor: new controls.Grid('divReceptor', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 } })
        },
        buttons: {
            AgregarIntegrante: document.getElementById("btnAgregarIntegrante"),
            AgregarRol: document.getElementById("btnAgregarRol"),
            AgregarAlternativo: document.getElementById("btnAgregarAlternativo"),
            AgregarCliente: document.getElementById("btnAgregarCliente"),
            CrearNotificacion: document.getElementById("btnCrearNotificacion")
        },
        dropdownlist: {
            ddlAreas: $("#ddlAreas"),
            ddlRol: $("#ddlRol"),
            ddlTipoServicio: $("#ddlTipoServicio"),
            ddlFase: $("#ddlFase"),
            ddlActividad: $("#ddlActividad")
        },
        textBox: {
            tbxCorreo: $("#tbxCorreo"),
            tbxTiempoAlerta: $("#tbxTiempoAlerta"),
            tbxTiempoAlertaFrecuencia: $("#tbxTiempoAlertaFrecuencia")
        },
        textAreas: {
            txaCuerpoNotificacion: $("#txaCuerpoNotificacion"),
            txaCuerpoAlerta: ("#txaCuerpoAlerta")
        },
        checkBox: {
            ckbNotificacionCorreo: $("#ckbNotificacionCorreo"),
            ckbNotificacionSistema: $("#ckbNotificacionSistema"),
            ckbCorreoAlerta: $("#ckbCorreoAlerta"),
            ckbSistemaAlerta: $("#ckbSistemaAlerta")
        }
    };

    var columnsIntegrantesArea =
        [
        { text: 'No.', key: true, css: 'col-xs-2', visible: true },
        { text: 'Integrante', field: 'NombreCompleto', css: 'col-xs-5' },
        { text: 'Correo', field: 'CorreoTrabajo', css: 'col-xs-6' }];

    var columnsIntegrantes =
        [
        { text: 'Area', field: 'Area', css: 'col-xs-3', visible: true },
        //{ text: 'No. Empleado', field: 'Identificador', css: 'col-xs-1' },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
        { text: 'Apellido P.', field: 'ApPaterno', css: 'col-xs-2' },
        { text: 'Apellido M.', field: 'ApMaterno', css: 'col-xs-2' },
        { text: 'Correo', field: 'CorreoTrabajo', css: 'col-xs-2' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Plus' } } }
        ];

    var columnsReceptor =
    [
        { text: 'No. Receptor', key: true, css: 'col-xs-1', visible: true },
        { text: 'Tipo de receptor', field: 'TipoReceptor', css: 'col-xs-3' },
        { text: 'Descripcion', field: 'Jerarquia', css: 'col-xs-3' },
        { text: 'Correo', field: 'CorreoTrabajo', css: 'col-xs-3' },
        { text: '', field: 'IdReceptor', css: 'col-xs-2', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Remove' } } }
    ];

    this.controls.grids.GridIntegrantesArea.init(columnsIntegrantesArea);
    this.controls.grids.GridIntegrantes.init(columnsIntegrantes);
    this.controls.grids.GridReceptor.init(columnsReceptor);

    this.controls.grids.GridIntegrantesArea.addListener("onPage", function (evt) {
        self.controls.grids.GridIntegrantesArea.currentPage = evt.currentPage;
        self.controls.grids.GridIntegrantes.currentPage = evt.currentPage;
        self.controls.grids.GridReceptor.currentPage = evt.currentPage;
        self.load();
    });

    this.functions = {
        hideGrid: function (data) {
            $(".flip div.front").slideToggle();
            $(".flip div.back").html(data).fadeIn();
        },
        hideForm: function (data) {
            $(".flip div.back div").remove().slideToggle();
            $(".flip div.front").fadeIn();
        }

    };

    $(this.controls.buttons.AgregarRol).click(function () {
        var rol = ddlRol.value;
        var data = {
            Query: {
                IdTipoReceptor: 1,
                IdRol: rol
            }, Paging: {
                CurrentPage: self.controls.grids.GridReceptor.currentPage,
                Rows: self.controls.grids.GridReceptor.pageSize
            }
        };

        var loadGridReceptor = function (data) {
            self.controls.grids.GridReceptor.loadRows([]);
            if (data.Result) {
                //self.controls.grids.GridReceptor.currentPage = page;
                //self.controls.grids.GridReceptor.pages = rows;
                self.controls.grids.GridReceptor.loadRows(data.List);
            }

        };
        self.SendAjaxGrid('POST', self.urls.AgregarReceptores, 'json', data, loadGridReceptor);

    });

    $(this.controls.buttons.AgregarAlternativo).click(function () {
        var data = {
            Query: {
                IdTipoReceptor: 4,
                Correo: self.controls.textBox.tbxCorreo.val()
            }, Paging: {
                CurrentPage: self.controls.grids.GridReceptor.currentPage,
                Rows: self.controls.grids.GridReceptor.pageSize
            }
        };

        var loadGridReceptor = function (data) {
            self.controls.grids.GridReceptor.loadRows([]);
            if (data.Result) {
                //self.controls.grids.GridReceptor.currentPage = page;
                //self.controls.grids.GridReceptor.pages = rows;
                self.controls.grids.GridReceptor.loadRows(data.List);
            }

        };
        self.SendAjaxGrid('POST', self.urls.AgregarReceptores, 'json', data, loadGridReceptor);

    });

    

    //$(ddlAreas).change(function (e) {
    //    var data = {
    //        ObjectResult: {
    //            Area: $(this).val()
    //        }, Paging: {
    //            CurrentPage: self.controls.grids.GridIntegrantesArea.currentPage,
    //            Rows: self.controls.grids.GridIntegrantesArea.pageSize
    //        }
    //    };
    //    self.SendAjax('POST', self.urls.ObtenerIntegrantesArea, 'json', data, success);
    //});

    this.controls.grids.GridIntegrantes.addListener("onButtonClick", function (evt) {

        var loadGridRoles = function (data) {
            self.controls.grids.GridReceptor.loadRows([]);
            if (data.Result) {
                //self.controls.grids.GridReceptor.currentPage = page;
                //self.controls.grids.GridReceptor.pages = rows;
                self.controls.grids.GridReceptor.loadRows(data.List);
            }
        };

        switch (evt.event) {
            case 'Edit':
                break;
            case 'View':
                break;
            case 'New':
                break;
            case 'Plus':
                var url = self.urls.AgregarReceptores;
                var data = {
                    Query: {
                        IdTiporeceptor: 2,
                        IdReceptor: evt.key
                    }, Paging: {
                    }
                }
                self.SendAjaxGrid('POST', url, 'json', data, loadGridRoles);
                break;
            case 'Active':
            case 'Inactive':
                break;
        }
    });

    this.controls.grids.GridReceptor.addListener("onButtonClick", function (evt) {

        var loadGridRoles = function (data) {
            self.controls.grids.GridReceptor.loadRows([]);
            if (data.Result) {
                //self.controls.grids.GridReceptor.currentPage = page;
                //self.controls.grids.GridReceptor.pages = rows;
                self.controls.grids.GridReceptor.loadRows(data.List);
            }
        };

        switch (evt.event) {
            case 'Edit':
                break;
            case 'View':
                break;
            case 'New':
                break;
            case 'Plus':
                break;
            case 'Remove':
                var url = self.urls.QuitarReceptores;
                var data = {
                    Query: evt.dataRow
                    ,Paging: {
                    }
                }
                self.SendAjaxGrid('POST', url, 'json', data, loadGridRoles);
                break;
            case 'Active':
            case 'Inactive':
                break;
        }
    });

    this.load();

};

Ui.prototype.load = function (evt) {
    //GridIntegrantesRol
    var self = this;
    var page = self.controls.grids.GridIntegrantesArea.currentPage;
    var rows = self.controls.grids.GridIntegrantesArea.pageSize;
    var loadGridRoles = function (data) {
        self.controls.grids.GridIntegrantesArea.loadRows([]);
        if (data.Result) {
            self.controls.grids.GridIntegrantesArea.currentPage = page;
            self.controls.grids.GridIntegrantesArea.pages = rows;
            self.controls.grids.GridIntegrantesArea.loadRows(data.List);
        }

    };
    var data = {
        Query: {
            IdTiporeceptor: 1,
            IdArea: 0,
            IdRol: 0
        }, Paging: {
            CurrentPage: self.controls.grids.GridIntegrantesArea.currentPage,
            Rows: self.controls.grids.GridIntegrantesArea.pageSize
        }
    };

    this.SendAjaxGrid('POST', self.urls.ObtenerIntegrantes, 'json', data, loadGridRoles);

    //GridIntegrante

    var pageIntegrantes = self.controls.grids.GridIntegrantes.currentPage;
    var rowsIntegrantes = self.controls.grids.GridIntegrantes.pageSize;
    var loadGridIntegrantes = function (data) {
        self.controls.grids.GridIntegrantes.loadRows([]);
        if (data.Result) {
            self.controls.grids.GridIntegrantes.currentPage = pageIntegrantes;
            self.controls.grids.GridIntegrantes.pages = rowsIntegrantes;
            self.controls.grids.GridIntegrantes.loadRows(data.List);
        }

    };
    var dataIntegrantes = {
        Query: {
            IdTiporeceptor: 2,
            Nombre: ''
        }, Paging: {
            CurrentPage: self.controls.grids.GridIntegrantes.currentPage,
            Rows: self.controls.grids.GridIntegrantes.pageSize
        }
    };

    this.SendAjaxGrid('POST', self.urls.ObtenerIntegrantes, 'json', dataIntegrantes, loadGridIntegrantes);
};

Ui.prototype.SendAjaxGrid = function (method, url, dataType, data, loadGrid) {
    $.ajax({
        type: 'POST',
        url: url,
        dataType: dataType,
        data: $.toJSON(data),
        beforeSend: function () { },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data) {
                loadGrid(data);
            }
        }
    });
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();

$(document).ready(function () {

    $("#btnCrearNotificacion").click(function () {
        var notificacion = {
            IdTipoServicio: ddlTipoServicio.value,
            IdFase: ddlFase.value,
            IdActividad: ddlActividad.value,
            CuerpoCorreo: txaCuerpoNotificacion.value,
            NotificacionCorreo: $("#ckbNotificacionCorreo").val(),
            NotificacionSistema: $("#ckbNotificacionSistema").val(),
            EmitirAlerta: $('input[name=rdbEmitirAlerta]:checked').val(),
            AlertaCorreo: $("#ckbCorreoAlerta").val(),
            AlertaSistema: $("#ckbSistemaAlerta").val(),
            TiempoAlerta: $("#tbxTiempoAlerta").val(),
            FrecuenciaAlerta: $("#tbxTiempoAlertaFrecuencia").val(),
            CuerpoAlerta: txaCuerpoAlerta.value
        }
        var data = {
            Query: notificacion
        };

        $.ajax({
            type: 'POST',
            url: '/Configuracion/GuardarNotificacion',
            dataType: 'json',
            data: $.toJSON(data),
            beforeSend: function () { },
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data) {
                }
            }
        });
    });
});