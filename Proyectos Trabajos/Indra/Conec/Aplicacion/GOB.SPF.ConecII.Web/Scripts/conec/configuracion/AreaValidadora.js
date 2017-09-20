Ui.prototype.gridValidadores = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    
    this.messages = {
        updatequestion: ""
    };
    this.controls = {
        grids: {
            GridValidadores: new controls.Grid('divActividadesAreas', { maxHeight: 400, pager: { show: true, pageSize: 50, currentPage: 1 } })
        }
    };
    var columnsValidadores = [
        { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: '', field: 'IdTipoServicio', css: '', visible: false },
        { text: '', field: 'IdActividad', css: '', visible: false },
        { text: 'Actividad', field: 'Actividad', css: 'col-xs-3' },
        { text: '', field: 'IdCentroCostos', css: '', visible: false },
        { text: 'Centro de costo', field: 'CentroCostos', css: 'col-xs-3' },
        { text: 'Obligatoriedad', field: 'Obligatorio', css: 'col-xs-3' },
        { text: '', field: 'Descripcion', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Remove' } } }];

    this.controls.grids.GridValidadores.init(columnsValidadores);

    //this.controls.grids.GridValidadores.addListener("onPage", function (evt) {
    //    self.controls.grids.GridValidadores.currentPage = evt.currentPage;
    //    //self.load();
    //});

    var cargarListaValidador = function (data) {
        if (data.Result === 1) {
            var loadGrid = function (data) {
                self.controls.grids.GridValidadores.reload([]);
                self.controls.grids.GridValidadores.reload(data);
            };

            if (data.Message != null & data.Message !== "")
            self.showMessage(data.Message);

            loadGrid(data.List);
        } else {
            self.showMessage(data.Message);
        }
    };

    this.controls.grids.GridValidadores.addListener("onButtonClick", function (evt) {
        var data = [];
        switch (evt.event) {

            case 'Remove':
                data = { Query: $.extend(evt.dataRow, { Action: evt.event }) };
                
                self.SendAjax('GET', "../Configuracion/QuitarValidador", 'json', data, cargarListaValidador);
                break;
        }
    });

    $(".areaVelidadora-add").on("click", function (evt) {

        var id = this.getAttribute("data-id");
        var idTipoServicio = $("#IdTipoServicio").val();
        var tipoServicio = $('#TipoServicio').find(":selected").text();
        var idActividad = id;
        var actividad = $("#txtActividad_" + id).val();
        var idCentroCostos = $("#ddlIdCentroCosto_" + id).val();
        var centroCostos = $("#ddlIdCentroCosto_" + id).find(":selected").text();
        var obligatorio = $("input[name=rdbObligatorio_" + id + "]:checked").val();

        if (true) {
            var data = {
                Query: {
                    IdValidacionActividad: 0,
                    IdTipoServico: idTipoServicio,
                    TipoServicio: tipoServicio,
                    IdActividad: idActividad,
                    Actividad: actividad,
                    IdCentroCosto: idCentroCostos,
                    CentroCostos: centroCostos,
                    Obligatorio: obligatorio
                },
                Paging: {
                    CurrentPage: 1, //self.controls.grids.gridValidadores.currentPage,
                    Rows: 30 //self.controls.grids.gridValidadores.pageSize
                },
                Message: idTipoServicio
            };

            Ui.prototype.SendAjax('POST', '../Configuracion/AgregarValidador', 'json', data, cargarListaValidador);
        }
    });
};



Ui.prototype.loadAreaValidadora = function (evt) {
    var self = this;
    var page = self.controls.grids.GridValidadores.currentPage;
    var rows = self.controls.grids.GridValidadores.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.GridValidadores.loadRows([]);
        if (data.Result) {
            self.controls.grids.GridValidadores.currentPage = data.Paging.CurrentPage;
            self.controls.grids.GridValidadores.pages = data.Paging.Pages;
            self.controls.grids.GridValidadores.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.ObtenerValidadores, 'json', data, loadGrid);
};

/*
//Ui.prototype.AgregarValidador = function (id) {
//    alert(id);
//    var idTipoServicio = $("#TipoServicio").val();
//    var idActividad = id;
//    var idCentroCostos = $("#ddlIdCentroCosto_" + id).val();
//    var obligatorio = $("#input[name=rdbObligatorio_" + id + "]:checked").val();

//    if (true) {
//        var data = {
//            Query: {
//                IdValidacionActividad: 0,
//                IdActividad: idActividad,
//                Actividad: "",
//                IdCentroCosto: idCentroCostos,
//                CentroCosto: "",
//                Obligatorio: obligatorio
//            },
//            Paging: {
//                CurrentPage: 1, //self.controls.grids.gridValidadores.currentPage,
//                Rows: 30 //self.controls.grids.gridValidadores.pageSize
//            },
//            Message: idTipoServicio
//        };

//        $.ajax({
//            type: 'POST',
//            url: '../Configuracion/AgregarValidador',
//            dataType: 'json',
//            data: $.toJSON(data),

//            beforeSend: function () { },
//            contentType: 'application/json; charset=utf-8',
//            success: function (data) {
//                if (data.Result === 1) {
//                    var loadGrid = function (data) {
//                        Ui.prototype.gridValidadores.controls.grids.GridValidadores.reload([]);
//                        Ui.prototype.gridValidadores.controls.grids.GridValidadores.reload(data);
//                    };

//                    if (data.Message != null & data.Message != "")
//                        self.showMessage(data.Message);

//                    loadGrid(data.List);
//                    self.functions.hideForm();
//                } else {
//                    self.showMessage(data.Message);
//                }
//            }
//        });
//    }
//};
*/

