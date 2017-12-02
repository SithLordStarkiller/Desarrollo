Ui.prototype.grids = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.listaControles = [];
    this.listaAcciones = [];

    this.messages = {
        updatequestion: ""
    };
    this.controls.grids = $.extend(this.controls.grids, {
        GridControles: new controls.Grid('divControles', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 1 } }),
        GridAcciones: new controls.Grid('divAcciones', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 1 } })
    }, true);


    var columnsControles = [
        { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Tipo de control', field: 'TipoControl', css: '', visible: false },
        { text: 'Nombre de control', field: 'Nombre', css: '', visible: false },
        { text: 'Descripcion', field: 'Descripcion', css: 'col-xs-3' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Remove' } } }];

    //var columnsAcciones = [
    //    { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
    //    { text: 'Tipo de accion', field: 'IdTipoServicio', css: 'col-xs-4', visible: false },
    //    { text: 'Descripcion', field: 'Actividad', css: 'col-xs-3' },
    //    { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Remove' } } }];

    this.controls.grids.GridControles.init(columnsControles);

    //this.controls.grids.GridAcciones.init(columnsAcciones);

    this.controls.grids.GridControles.addListener("onPage", function (evt) {
        self.controls.grids.GridControles.currentPage = evt.currentPage;
        self.load();
    });

    //this.controls.grids.GridAcciones.addListener("onPage", function (evt) {
    //    self.controls.grids.GridAcciones.currentPage = evt.currentPage;
    //    self.load();
    //});

    var cargarListaControles = function (data) {
        if (data.Result === 1) {
            var loadGrid = function (data) {
                self.controls.grids.GridControles.reload([]);
                self.controls.grids.GridControles.reload(data);
            };

            if (data.Message != null & data.Message !== "")
                self.showMessage(data.Message);

            loadGrid(data.List);
        } else {
            self.showMessage(data.Message);
        }
    };

    //var cargarListaAcciones = function (data) {
    //    if (data.Result === 1) {
    //        var loadGrid = function (data) {
    //            self.controls.grids.GridAcciones.reload([]);
    //            self.controls.grids.GridAcciones.reload(data);
    //        };

    //        if (data.Message != null & data.Message !== "")
    //            self.showMessage(data.Message);

    //        loadGrid(data.List);
    //    } else {
    //        self.showMessage(data.Message);
    //    }
    //};

    //this.controls.grids.GridAcciones.addListener("onButtonClick", function (evt) {
    //    var data = [];
    //    switch (evt.event) {
    //        case 'New':
    //            alert("new");
    //            //url = self.urls.Modulo;
    //            //data = { model: { Action: evt.event } };
    //            //self.SendAjax('POST', url, 'html', data, getPartial);
    //            break;
    //        case 'Remove':
    //            data = { Query: $.extend(evt.dataRow, { Action: evt.event }) };

    //            self.SendAjax('GET', "../Configuracion/QuitarValidador", 'json', data, cargarListaAcciones);
    //            break;
    //    }
    //});

    this.controls.grids.GridControles.addListener("onButtonClick", function (evt) {
        var data = [];
        switch (evt.event) {
            case 'Remove':

                var item = evt.key - 1;

                self.listaControles.splice(item, 1);

                self.controls.grids.GridControles.reload(self.listaControles);

                self.showMessage("Se elimino el control");
                break;
        }
    });

    this.loadGridControles();

    if (ModuloModel) {
        self.listaControles = ModuloModel.listaControles ? ModuloModel.listaControles : [];
    }

    $("#btnAgregarControl").click(function () {
        var idTipoControl = $("#ddlTipoControlControl").val();
        var tipoControl = $("#ddlTipoControlControl").find(":selected").text();
        var nombre = $("#txbNombreControl").val();
        var descripcion = $("#txbDescControl").val();

        if (idTipoControl === "0" || nombre === "" || descripcion === "") {
            self.showMessage("Se debe ingresar un nombre, una descripcion y un tipo control para agregar el control");
            return;
        }

        var servicio = { IdTipoControl: idTipoControl, TipoControl: tipoControl, Nombre: nombre, Descripcion: descripcion }

        self.listaControles.push(servicio);
        self.controls.grids.GridControles.reload(self.listaControles);

        $("#ddlTipoControlControl").val("0");
        $("#txbNombreControl").val("");
        $("#txbDescControl").val("");

    });

};

//Ui.prototype.loadGridAcciones = function (evt) {
//    var self = this;
//    var loadGrid = function (data) {
//        self.controls.grids.GridAcciones.loadRows([]);
//        if (data.Result) {
//            self.controls.grids.GridAcciones.loadRows(data.List);
//        }

//    };
//    var data = {  };

//    this.SendAjax('POST', self.urls.ObtenerAcciones, 'json', data, loadGrid);
//};

Ui.prototype.loadGridControles = function (evt) {
    var self = this;
    var loadGrid = function (data) {
        self.controls.grids.GridControles.loadRows([]);
        if (data.Result) {
            self.controls.grids.GridControles.loadRows(data.List);
        }

    };
    var data = {};

    this.SendAjax('POST', self.urls.ObtenerControles, 'json', data, loadGrid);
};