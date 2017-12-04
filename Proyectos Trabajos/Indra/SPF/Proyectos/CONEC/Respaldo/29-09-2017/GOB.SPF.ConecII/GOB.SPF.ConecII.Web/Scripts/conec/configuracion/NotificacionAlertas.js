Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        buscar: this.getUrl('/Configuracion/ObtenerNotificaciones'),
        nuevo: this.getUrl('../Configuracion/CreacionDeNotifacionesAlertas'),
        editar: this.getUrl('/Configuracion/EditarNotificacion')
    };
    this.controls = {
        grids: {
            Notificaciones: new controls.Grid('divNotificaciones', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [
        { text: 'No.', iskey: true/*field: 'IdNotificacion'*/, css: 'col-xs-1', visible: true },
        { text: 'Tipo Servicio', field: 'TipoServicio', css: 'col-xs-2' },
        { text: 'Fase', field: 'Fase', css: 'col-xs-2' },
        { text: 'Actividad', field: 'Actividad', css: 'col-xs-2' },
        //{ text: 'Notificacion', field: 'CuerpoCorreo', css: 'col-xs-2' },
        { text: 'Alertas', field: 'EmitirAlerta', css: 'col-xs-3' },
        { text: 'Frecuencia', field: 'Frecuencia', css: 'col-xs-1' },
        { text: '', field: 'IdNotificacion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } }
    ];
    this.controls.grids.Notificaciones.init(columns);
    this.controls.grids.Notificaciones.addListener("onPage", function (evt) {
        self.controls.grids.Notificaciones.currentPage = evt.currentPage;
        self.load();
    });

    this.functions = {
        hideGrid: function (data) {
            $(".flip div.front").slideToggle(),
            $(".flip div.back").html(data).fadeIn();
        },
        hideForm: function (data) {
            $(".flip div.back div").remove().slideToggle(),
            $(".flip div.front").fadeIn();
        }

    };

    var success = function (data) {
        if (data.Result === 1) {
            var loadGrid = function (data) {
                self.controls.grids.Notificaciones.reload([]);
                self.controls.grids.Notificaciones.reload(data);
            };
            loadGrid(data.List);
            self.functions.hideForm();
        }
    };

    //$(this.controls.buttons.buscar).click(function () {
    //    var data = {
    //        ObjectResult: {
    //            IsActive: $("input[name=estatus]:checked").val()
    //        }, Paging: {
    //            CurrentPage: self.controls.grids.Notificaciones.currentPage,
    //            Rows: self.controls.grids.Notificaciones.pageSize
    //        }
    //    };
    //    self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    //});

    this.controls.grids.Notificaciones.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};

        switch (evt.event) {
            case 'Edit':
                url = self.urls.editar;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) }
                window.location.href = url + '?idNotificacion=' + data.model.IdNotificacion;
                break;
            case 'View':
                break;
            case 'New':
                url = self.urls.nuevo;
                window.location.replace(url);
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

    var pages = self.controls.grids.Notificaciones.pages;
    var pageSize = self.controls.grids.Notificaciones.pageSize;
    var currentPage = self.controls.grids.Notificaciones.currentPage;

    var loadGrid = function (data) {
        self.controls.grids.Notificaciones.loadRows([]);
        if (data.Result) {
            self.controls.grids.Notificaciones.CurrentPage = currentPage;
            self.controls.grids.Notificaciones.Pages = pages;
            self.controls.grids.Notificaciones.pageSize = pageSize;
            self.controls.grids.Notificaciones.loadRows(data.List);
        }

    };
    var data = { Paging: { pages: pages, Rows: pageSize, CurrentPage: currentPage } };

    this.SendAjax('POST', self.urls.buscar, 'json', data, loadGrid);
};

//Ui.prototype.SendAjax = function (method, url, dataType, data, $function) {
//    $.ajax({
//        type: 'POST',
//        url: url,
//        dataType: dataType,
//        data: $.toJSON(data),
//        beforeSend: function () { },
//        contentType: 'application/json; charset=utf-8',
//        success: $function
//    });
//};

function init() {
    var ui = new Ui();
    ui.init();
};

init();