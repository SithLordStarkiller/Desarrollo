Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        grupos: this.getUrl('/Catalogo/GrupoConsulta'),
        grupo: this.getUrl('/Catalogo/Grupo'),
        guardar: this.getUrl('/Catalogo/GrupoGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/GrupoCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/GrupoConsultaCriterio'),
        grupoPorDivision: this.getUrl('/Catalogo/GrupoConsultaPorIdDivision/')
    };
    this.messages = {
        updatequestion: "¿Está seguro que desea modificar el grupo?.",
        activequestion: "¿Está seguro de que desea @ el grupo seleccionado?.",
    };
    this.controls = {
        grids: {
        grupo: new controls.Grid('divGrupo', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 0  }, showPlusButton: true })
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

            if (data.Message != null & data.Message != "")
                self.showMessage(data.Message);

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
                IdDivision: $("#Division option:selected").val(),
                Identificador: $("#Grupo option:selected").val()
            }
        };        

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
                var identificador = $("#Identificador").val();
                var ejecutarGuardado = function () {
                    var data = {
                        ObjectResult: {
                            IdDivision: $("#Divisiones option:selected").val(),
                            Name: $("#Name").val(),
                            Descripcion: $("#Descripcion").val(),
                            Identificador: $("#Identificador").val()
                        },
                        Paging: {
                            CurrentPage: self.controls.grids.grupo.currentPage,
                            Rows: self.controls.grids.grupo.pageSize
                        }
                    };
                    self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                };
                $.validator.unobtrusive.parse("#grupoForm");

                if ($("#grupoForm").valid()) {
                    if (parseInt(identificador) > 0)
                        self.confirmacion(self.messages.updatequestion, { title: "Modificación", aceptar: ejecutarGuardado });
                    else
                        ejecutarGuardado();
                }
            });
            $("#Divisiones").select2();
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
                var actualizarRegistro = function () {
                    var data = {
                        Query: {
                            IsActive: $("input[name=estatus]:checked").val(),
                        },
                        ObjectResult: object,
                        Paging: {
                            CurrentPage: self.controls.grids.grupo.currentPage,
                            Rows: self.controls.grids.grupo.pageSize
                        }
                    };
                    self.SendAjax('POST', url, 'json', data, success);
                };

                var anuncio = self.messages.activequestion.replace("@", (object.IsActive == true ? "activar" : "desactivar"));
                self.confirmacion(anuncio, { title: "Cambiar estatus", aceptar: actualizarRegistro })
                break;
        }
    });

    this.load();
    $("#Division").select2().change(function () {
        var destino = document.getElementById("Grupo");
        if (this.value !== "") {            
            $.get(self.urls.grupoPorDivision + this.value, function (data) {
                switch (data.Result) {
                    case 1:
                        var list = data.List.map(function (item, index) {
                            return { Identificador: item.Identificador, Nombre: item.Name };
                        });

                        self.loadCombo(list, destino);
                        break;
                    case 0:
                        break;
                }
            });
        } else {
            self.loadCombo([], destino);
        }
    });;
    $("#Grupo").select2();   
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
            self.controls.grids.grupo.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.grupos, 'json', data, loadGrid);
};

function init() {
    var ui = new Ui();
    ui.init();
};

init();