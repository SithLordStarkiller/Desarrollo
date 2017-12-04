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
            //GridIntegrantesArea: new controls.Grid('divIntegrantesRol', { maxHeight: 400, pager: { show: true, pageSize: 5, currentPage: 1, pages: 1 } }),
            GridIntegrantes: new controls.Grid('divIntegrantes', { maxHeight: 400, pager: { show: true, pageSize: 5/*, currentPage: 1, pages: 1 */} }),
            GridReceptor: new controls.Grid('divReceptor', { maxHeight: 400, pager: { show: true, pageSize: 5/*, currentPage: 1, pages: 1*/ } })
        },
        buttons: {
            BuscarIntegrante: document.getElementById("btnBuscarIntegrante"),
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

    var columnsIntegrantes = [
        //{ text: 'Area', field: 'Area', css: 'col-xs-3', visible: true },
        { text: 'No. Empleado', key:true, css: 'col-xs-1' },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
        { text: 'A.Paterno', field: 'ApPaterno', css: 'col-xs-2' },
        { text: 'A.Materno', field: 'ApMaterno', css: 'col-xs-2' },
        { text: 'Correo', field: 'Correo', css: 'col-xs-2' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Plus' } } }
    ];

    var columnsReceptor = [
        { text: 'No. Receptor', key: true, css: 'col-xs-1', visible: true },
        { text: 'Tipo de receptor', field: 'TipoReceptor', css: 'col-xs-2' },
        { text: 'Descripcion', field: 'Descripcion', css: 'col-xs-3' },
        { text: 'Con copia', field: 'EsCopia', css: 'col-xs-3' },
        { text: 'Correo', field: 'Correo', css: 'col-xs-3' },
        { text: '', field: 'IdReceptor', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Remove' } } }
    ];

    this.controls.grids.GridIntegrantes.init(columnsIntegrantes);
    this.controls.grids.GridReceptor.init(columnsReceptor);

    $(this.controls.buttons.AgregarRol).click(function () {
        var rol = ddlRol.value;
        if (rol === "0" || rol === "") {
            self.showMessage("Se debe seleccionar un rol valido");
        }
        else {
            var data = {
                Query: {
                    IdTipoReceptor: 1,
                    TipoReceptor: "Rol",
                    IdRol: rol,
                    Descripcion: $("#ddlRol option:selected").text(),
                    Correo: "N/A",
                    EsCopia: $('#ckbCorreoCcR').is(":checked")
                }
            };

            var loadGridReceptor = function (data) {
                self.controls.grids.GridReceptor.loadRows([]);
                if (data.Result) {
                    self.controls.grids.GridReceptor.loadRows(data.List);
                    self.showMessage(data.Message);
                } else {
                    self.controls.grids.GridReceptor.loadRows(data.List);
                    self.showMessage(data.Message);
                }

            };
            self.SendAjaxGrid('POST', self.urls.AgregarReceptores, 'json', data, loadGridReceptor);
        }
    });

    $(this.controls.buttons.BuscarIntegrante).click(function () {

        var loadGridIntegrantes = function (data) {
            self.controls.grids.GridIntegrantes.loadRows([]);
            if (data.Result) {
                self.controls.grids.GridIntegrantes.loadRows(data.List);
            }

        };
        var dataIntegrantes = {
            Query: {
                IdTiporeceptor: 2,
                Nombre: $("#tbxBuscarIntegrante").val()
            }
        };


        $.ajax({
            type: 'POST',
            url: self.urls.ObtenerIntegrantes,
            dataType: 'json',
            data: JSON.stringify(dataIntegrantes),
            beforeSend: function () { },
            contentType: 'application/json; charset=utf-8',
            success: loadGridIntegrantes,
            error: function (xmlHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }, fail: function (xmlHttpRequest, textStatus) {
                alert(textStatus);
            }
        });

    });

    $(this.controls.buttons.AgregarCliente).click(function () {
        var listClient = [];

        var esCopia = $("#ckbCorreoCcC").is(":checked");

        var ckbRepLeg = $("#ckbRepLeg").is(":checked");
        var ckbConJur = $("#ckbConJur").is(":checked");
        var ckbConOpe = $("#ckbConOpe").is(":checked");
        var ckbConFin = $("#ckbConFin").is(":checked");
        var ckbRh = $("#ckbRh").is(":checked");

        if (ckbRepLeg)
            listClient.push(1);
        if (ckbConJur)
            listClient.push(2);
        if (ckbConOpe)
            listClient.push(3);
        if (ckbConFin)
            listClient.push(4);
        if (ckbRh)
            listClient.push(5);

        if (listClient.length > 0) {

            var data = {
                Query: {
                    IdTipoReceptor: 3,
                    TipoReceptor: "Cliente",
                    EsCopia: esCopia
                }
                ,Message: JSON.stringify(listClient)
            };

            var loadGridReceptor = function(data) {
                self.controls.grids.GridReceptor.loadRows([]);

                if (data.Result) {
                    self.controls.grids.GridReceptor.loadRows(data.List);
                    self.showMessage(data.Message);
                } else {
                    self.controls.grids.GridReceptor.loadRows(data.List);
                    self.showMessage(data.Message);
                }

            };
            self.SendAjaxGrid('POST', self.urls.AgregarReceptores, 'json', data, loadGridReceptor);
        } else {
            self.showMessage("Se debe seleccionar almenos un cliente");
        }
    });

    $(this.controls.buttons.AgregarAlternativo).click(function() {
        var email = self.controls.textBox.tbxCorreo.val();

        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        var res = re.test(email);
        if (!res) {
            self.showMessage("El correo ingresado no es valido");
        } else {
            var data = {
                Query: {
                    IdTipoReceptor: 4,
                    TipoReceptor: "Alternativo",
                    Correo: self.controls.textBox.tbxCorreo.val(),
                    EsCopia: $('#ckbCorreoCcA').is(":checked")
                }
            };

            var loadGridReceptor = function(data) {
                self.controls.grids.GridReceptor.loadRows([]);
                if (data.Result) {
                    self.controls.grids.GridReceptor.loadRows(data.List);
                    self.showMessage(data.Message);
                } else {
                    self.showMessage(data.Message);
                }

            };
            self.SendAjaxGrid('POST', self.urls.AgregarReceptores, 'json', data, loadGridReceptor);
        }
    });

    this.controls.grids.GridIntegrantes.addListener("onButtonClick", function (evt) {

        var loadGridRoles = function (data) {
            self.controls.grids.GridReceptor.loadRows([]);
            if (data.Result) {
                self.controls.grids.GridReceptor.loadRows(data.List);
                self.showMessage(data.Message);
            } else {
                self.controls.grids.GridReceptor.loadRows(data.List);
                self.showMessage(data.Message);
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
                        IdTipoReceptor: 2,
                        IdPersona: evt.dataRow.Identificador,
                        Correo: evt.dataRow.Correo,
                        Descripcion: evt.dataRow.Nombre + ' ' + evt.dataRow.ApPaterno + ' ' + evt.dataRow.ApMaterno,
                        EsCopia: $("#ckbCorreoCcI").is(":checked"),
                        TipoReceptor: "Integrante"
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
                    , Paging: {
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
    ////GridIntegrantesRol

    var self = this;
    //var page = self.controls.grids.GridIntegrantesArea.currentPage;
    //var rows = self.controls.grids.GridIntegrantesArea.pageSize;
    //var loadGridRoles = function (data) {
    //    self.controls.grids.GridIntegrantesArea.loadRows([]);
    //    if (data.Result) {
    //        //self.controls.grids.GridIntegrantesArea.currentPage = page;
    //        //self.controls.grids.GridIntegrantesArea.pages = rows;
    //        //self.controls.grids.GridIntegrantesArea.loadRows(data.List);
    //    }

    //};
    //var data = {
    //    Query: {
    //        IdTiporeceptor: 1,
    //        IdArea: 0,
    //        IdRol: 0
    //    }
    //};

    //this.SendAjaxGrid('POST', self.urls.ObtenerIntegrantes, 'json', data, loadGridRoles);

    //GridIntegrante
    var loadGridIntegrantes = function (data) {
        self.controls.grids.GridIntegrantes.loadRows([]);
        if (data.Result) {
            self.controls.grids.GridIntegrantes.loadRows(data.List);
        }

    };
    var dataIntegrantes = {
        Query: {
            IdTiporeceptor: 2,
            Nombre: ''
        }
    };

    this.SendAjaxGrid('POST', self.urls.ObtenerIntegrantes, 'json', dataIntegrantes, loadGridIntegrantes);
};

Ui.prototype.SendAjaxGrid = function (method, url, dataType, data, loadGrid) {
    $.ajax({
        type: 'POST',
        url: url,
        dataType: dataType,
        data: JSON.stringify(data),
        beforeSend: function () { },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data) {
                loadGrid(data);
            }
        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }, fail: function (xmlHttpRequest, textStatus) {
            alert(textStatus);
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
            IdTipoServicio: $("#IdTipoServicio").val(),
            IdFase: $("#IdFase").val(),
            IdActividad: $("#IdActividad").val(),
            CuerpoCorreo: $("#CuerpoCorreo").val(),
            EsCorreo: $("#EsCorreo").val(),//.is("checked"),
            EsSistema: $("#EsSistema").val(),//.is("checked"),
            EmitirAlerta: $('input[name=EmitirAlerta]:checked').val(),
            AlertaEsCorreo: $("#AlertaEsCorreo").val(),//.is("checked"),
            AlertaEsSistema: $("#AlertaEsSistema").val(),//.is("checked"),
            TiempoAlerta: $("#TiempoAlerta").val(),
            Frecuencia: $("#Frecuencia").val(),
            CuerpoAlerta: $("#CuerpoAlerta").val()
        }
        var data = {
            Query: notificacion
        };

        $.ajax({
            type: 'POST',
            url: '/Configuracion/GuardarNotificacion',
            dataType: 'json',
            data: JSON.stringify(data),
            beforeSend: function () { },
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data.Result) {
                    //self.showMessage(data.Message);
                    window.location.replace("/Configuracion/NotifacionesAlertas");
                } else {
                    //self.showMessage(data.Message);
                }
            }
        });
    });
});