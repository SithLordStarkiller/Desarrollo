Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.urls = {
        cuotas: this.getUrl('/Catalogo/CuotasConsulta'),
        cuota: this.getUrl('/Catalogo/Cuota'),
        guardar: this.getUrl('/Catalogo/CuotaGuardar'),
        cambiarestatus: this.getUrl('/Catalogo/CuotaCambiarEstatus'),
        buscar: this.getUrl('/Catalogo/CuotaConsultaCriterio')
    };
    this.messages = {
        updatequestion: "¿Está seguro que desea modificar los datos de la cuota?.",
        validquestion: "¿Está seguro de la cuota a validar?.",
        activequestion: "Al realizar esta acción se afectará a las cotizaciones, los recibos y las hojas de ayuda. ¿Está seguro de que desea @ la cuota seleccionada?.",
    };
    this.controls = {
        grids: {
            cuota: new controls.Grid('divCuota', { maxHeight: 400, pager: { show: true, pageSize: 20, currentPage: 1, pages: 1 }, showPlusButton: true })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Servicio', field: 'TipoServicio', css: 'col-xs-2' },
        { text: 'Referencia', field: 'Referencia', css: 'col-xs-2' },
        { text: 'Dependencia', field: 'Dependencia', css: 'col-xs-2' },
        { text: 'Dep. Cadena', field: 'DescripcionDependencia', css: 'col-xs-2' },
        { text: 'Concepto', field: 'Concepto', css: 'col-xs-3' },
        { text: 'Cuota Base', field: 'CuotaBase', css: 'col-xs-1' },
        { text: 'F. Autorización', field: 'FechaAutorizacion', css: 'col-xs-1', style: 'display:none' },
        { text: 'F.E. Vigor', field: 'FechaEntradaVigor', css: 'col-xs-1', style: 'display:none' },
        //{ text: 'Fecha Termino', field: 'FechaTermino', css: 'col-xs-1', style: 'display:none' },
        //{ text: 'Fecha Publicación', field: 'FechaPublicacionDof', css: 'col-xs-1', style: 'display:none' },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Identificador', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }];
    this.controls.grids.cuota.init(columns);
    this.controls.grids.cuota.addListener("onPage", function (evt) {
        self.controls.grids.cuota.currentPage = evt.currentPage;
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
                self.controls.grids.cuota.reload([]);
                self.controls.grids.cuota.reload(data);
            };
            if (data.Message!= null & data.Message!= "")
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
                IdTipoServicio: $("#Servicios option:selected").val(),
                Concepto: $("#Conceptos option:selected").val(),
                Ano: $("#FechaVigor option:selected").val(),
                EsProducto: $("#Productos option:selected").val()
            }, Paging: {
                CurrentPage: self.controls.grids.cuota.currentPage,
                Rows: self.controls.grids.cuota.pageSize
            }
        };
        
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);
        $("#Servicios").val(null);
        $("#Servicios").select2().val(null);
        $("#Conceptos").val(null);
        $("#Conceptos").select2().val(null);
        $("#FechaVigor").val(null);
        $("#FechaVigor").select2().val(null);
        $("#Productos").val(null);
        $("#Productos").select2().val(null);

    });

    this.controls.grids.cuota.addListener("onButtonClick", function (evt) {
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
                                IdTipoServicio: $("#IdTipoServicio").val(),
                                TipoServicio: $("#IdTipoServicio").text(),
                                IdReferencia: $("#IdReferencia").val(),
                                Referencia: $("#IdReferencia").text(),
                                IdDependencia: $("#IdDependencia").val(),
                                Dependencia: $("#IdDependencia").text(),
                                IdJerarquia: $("#Jerarquia").val(),
                                Jerarquia: $("#Jerarquia").text(),
                                IdGrupoTarifario: $("#GrupoTarifario").val(),
                                GrupoTarifario: $("#GrupoTarifario").text(),
                                IdMedidaCobro: $("#IdMedidaCobro").val(),
                                MedidaCobro: $("#IdMedidaCobro").text(),
                                Name: $("#Name").val(),
                                Concepto: $("#Concepto").val(),
                                CuotaBase: $("#CuotaBase").val(),
                                Iva: $("#Iva").val(),
                                FechaAutorizacion: $("#FechaAutorizacion").val(),
                                FechaEntradaVigor: $("#FechaEntradaVigor").val(),
                                FechaTermino: $("#FechaTermino").val(),
                                FechaPublicaDof: $("#FechaPublicaDof").val(),
                                Identificador: $("#Identificador").val()
                            },
                            Paging: {
                                CurrentPage: self.controls.grids.cuota.currentPage,
                                Rows: self.controls.grids.cuota.pageSize
                            }
                        };
                        self.SendAjax('POST', self.urls.guardar, 'json', data, success);
                    };
                    $.validator.unobtrusive.parse("#cuotaForm");

                if ($("#cuotaForm").valid())
                    {
                    if (data.ObjectResult != null) {
                        if (data.ObjectResult.Identificador > 0)
                            self.confirmacion(self.messages.updatequestion, { title: "Guardar...", aceptar: ejecutarGuardado })
                    }
                    else ejecutarGuardado();
                    }
                });

            self.loadControls();
            if (data.Result == 1) {
                self.loadCombo(data.List, destino)
            }
        };


        switch (evt.event) {
            case 'Edit':
            case 'View':

                evt.dataRow.FechaAutorizacion = self.replaceFormaFecha(evt.dataRow.FechaAutorizacion);
                evt.dataRow.FechaEntradaVigor = self.replaceFormaFecha(evt.dataRow.FechaEntradaVigor);
                evt.dataRow.FechaTermino = self.replaceFormaFecha(evt.dataRow.FechaTermino);
                evt.dataRow.FechaPublicaDof = self.replaceFormaFecha(evt.dataRow.FechaPublicaDof);

                url = self.urls.cuota;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.cuota;
                data = { model: { Action: evt.event } };
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
            case 'Inactive':
                url = self.urls.cambiarestatus;
                var object = evt.dataRow;
                object.IsActive = !object.IsActive;
                var actualizarRegistro = function () {
                    data = {
                        Query: {
                            IsActive: $("input[name=estatus]:checked").val(),
                        },
                        ObjectResult: object, Paging: {
                            CurrentPage: self.controls.grids.cuota.currentPage,
                            Rows: self.controls.grids.cuota.pageSize
                        }
                    };
                    self.SendAjax('POST', url, 'json', data, success);
                }
                var anuncio = self.messages.activequestion.replace("@", (object.IsActive == true ? "activar" : "desactivar"));
                self.confirmacion(anuncio, { title: "Cambiar estatus", aceptar: actualizarRegistro })

                break;
        }
    });
    this.load();
    //JZR Cambio en autocomplete de servicios 
    $("#Servicios").select2().on("change", function () {
        var newcuotabase = '<option value="">Seleccione</option>';
        var datatos = {
            ObjectResult: {
                IsActive: $("input[name=estatus]:checked").val(),
                IdTipoServicio: $("#Servicios option:selected").val(),
            }, Paging: {
                CurrentPage: self.controls.grids.cuota.currentPage,
                Rows: self.controls.grids.cuota.pageSize
            }
        };
        // en caso de no seleccionar nada muestra esto 
        if ($("#Servicios").val() == '') {
            $.ajax({
                type: 'POST',
                url: "/Catalogo/CuotaConsultaCriterio",
                dataType: "JSON",
                data: JSON.stringify(datatos),
                beforeSend: function () { },
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    console.log(data);
                    $("#Conceptos").html('');
                    for (var resutado in data.List) {
                        console.log(data.List[resutado].Identificador)
                        newcuotabase += '<option value="' + data.List[resutado].Identificador + '">' + data.List[resutado].Concepto + '</option>';
                    }
                    $("#Conceptos").append(newcuotabase);
                    $("#Conceptos").select2();
                }
            });
        }
        else
        {
            // en caso de seleccionar un tipo de servicio carga las tipo cuotas que tengan ese servicio 
            $.ajax({
                type: 'POST',
                url: "/Catalogo/CuotaConsultaCriterio",
                dataType: "JSON",
                data: JSON.stringify(datatos),
                beforeSend: function () { },
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    console.log(data);
                    $("#Conceptos").html('');
                    for (var resutado in data.List)
                    {
                        console.log(data.List[resutado].Identificador)
                        newcuotabase += '<option value="' + data.List[resutado].Identificador + '">' + data.List[resutado].Concepto + '</option>';
                    }
                    $("#Conceptos").append(newcuotabase);
                    $("#Conceptos").select2();
                }
            });
        }
    });
    $("#Conceptos").select2();
    $("#FechaVigor").select2();
    $("#Productos").select2();
};


/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    var self = this;
    var page = self.controls.grids.cuota.currentPage;
    var rows = self.controls.grids.cuota.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.cuota.loadRows([]);
        if (data.Result ) {
            self.controls.grids.cuota.currentPage = data.Paging.CurrentPage;
            self.controls.grids.cuota.pages = data.Paging.Pages;
            self.controls.grids.cuota.loadRows(data.List);
        }

    };
    var data = { page: page, rows: rows };

    this.SendAjax('POST', self.urls.cuotas, 'json', data, loadGrid);
};


//JZR Controles para cargar Date piker
Ui.prototype.loadControls = function () {
    var self = this;
    //self.FuncionesFechaPiker();
    //JZR
    //Condiciones para fechas minimas o maximas si es que lo requiere el caso de uso
    FechaCondicionMin = "2017-12-12";
    FechaCondicionMax = "2017-12-12";
    //JZR
    //Obtencion de fecha de campos datetime
    var FechaAutorizacion = $("#FechaAutorizacion").val();
    var FechaEntradaVigor = $("#FechaEntradaVigor").val();
    var FechaTermino = $("#FechaTermino").val();
    var FechaPublicaDof = $("#FechaPublicaDof").val();

    //Formato de fechas para los campos date llama una funcion "replaceFechaHora" 
    //Parametro de entrada : fecha del campo date
    //Retorno la fecha cn formato deacuerdo al caso de uso

    $("#FechaAutorizacion").val(self.replaceFechaHora(FechaAutorizacion));
    $("#FechaEntradaVigor").val(self.replaceFechaHora(FechaEntradaVigor));
    $("#FechaTermino").val(self.replaceFechaHora(FechaTermino));
    $("#FechaPublicaDof").val(self.replaceFechaHora(FechaPublicaDof));

    //Creacionde date piker 
    self.DatepikerNormal();
    self.DatepikerMinDateActual();
    self.DatepikerMinDateActualsimple();
    self.DatepikerMinDateCondicion(FechaCondicionMin);
    self.DatepikerMaxDateActual();
    self.DatepikerMaxDateCondicion(FechaCondicionMax);

    //En caso de ser nuevo pone por defaul la fecha del dia.
    var concepto = $("#Concepto").val();
    if ($("#Concepto").val() == "") {
        $('.datepickerFechaActua').datepicker('setDate', new Date());
        $('.datepickerMinFechaActua').datepicker('setDate', new Date());
        $('.datepickerMaxFechaActua').datepicker('setDate', new Date());
        //$("#FechaTermino").val("");
        //$("#FechaPublicaDof").val("");
    }

    concepto = 0;

    //Reglas de negocio
    //IVa

    var EsProducto = $("#EsProducto").val();
    if (EsProducto != "True") {
        $("#Iva").attr('readonly', true);
    }
    else
    {
        $("#Iva").attr('readonly', false);
    }



    $("#IdReferencia").on("change", function () {

        var datatos = {
            ObjectResult: {
                Identificador: $("#IdReferencia").val(),
                IsActive: "True"
            }, Paging: {
                CurrentPage: self.controls.grids.cuota.currentPage,
                Rows: self.controls.grids.cuota.pageSize
            }
        };

        $.ajax({
            type: 'POST',
            url: "/Catalogo/ReferenciasConsultaCriterio",
            dataType: "JSON",
            data: JSON.stringify(datatos),
            beforeSend: function () { },
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                console.log(data.List[0].EsProducto);
                if (data.List[0].EsProducto == true) {
                    $("#Iva").attr('readonly', false);
                }
                else {
                    $("#Iva").attr('readonly', true);
                    $("#Iva").val(0);
                }
            }
        });
        datatos = null;
    });
};

Ui.prototype.DatepikerNormal = function () {

    $('.datepickerFechaActua').datepicker({
        dateFormat: 'yy/mm/dd'
    });
    $(".datepickerFechaActua").attr('readonly', true);

}

Ui.prototype.DatepikerMinDateActual = function () {

    $('.datepickerMinFechaActua').datepicker({
        dateFormat: 'yy/mm/dd',
        minDate: new Date()
    });
    $(".datepickerMinFechaActua").attr('readonly', true);

}

Ui.prototype.DatepikerMaxDateActual = function () {

    $('.datepickerMaxFechaActua').datepicker({
        dateFormat: 'yy/mm/dd',
        maxDate: new Date()
    });
    $(".datepickerMaxFechaActua").attr('readonly', true);
}

Ui.prototype.DatepikerMinDateCondicion = function (FechaCondicionMin) {

    $('.datepickerMinFechaCondicion').datepicker({
        dateFormat: 'yy/mm/dd',
        minDate: FechaCondicionMin
    });
    $(".datepickerMinFechaCondicion").attr('readonly', true);

}

Ui.prototype.DatepikerMaxDateCondicion = function (FechaCondicionMax) {

    $('.datepickerMaxFechaCondicion').datepicker({
        dateFormat: 'yy/mm/dd',
        minDate: FechaCondicionMin
    });
    $(".datepickerMaxFechaCondicion").attr('readonly', true);

}

Ui.prototype.replaceFechaHora = function (fecha) {
    if (fecha != null)
    {
        resplacefecha = fecha.replace(" 12:00:00 a.m.", "");
        resplacefecha = fecha.replace(" 0:00:00", "");
        var res = resplacefecha.split("/");
        var fechaDate = res[2] + "/" + res[1] + "/" + res[0];
        //var nuevafecha = new Date(fechaDate)
        //var resfechaformat = nuevafecha.getFullYear() + "/" + ("0" + (nuevafecha.getMonth() + 1)).slice(-2) + "/" + ("0" + nuevafecha.getDate()).slice(-2);
    }
    return fechaDate;
}

Ui.prototype.replaceFormaFecha = function (cellValue) {
    var self = this;
    if (cellValue instanceof Date)
    {
        resfechaformat = cellValue.getFullYear() + "/" + ("0" + (cellValue.getMonth() + 1)).slice(-2) + "/" + ("0" + cellValue.getDate()).slice(-2);
        return cellValue;
    }

    cellValue = cellValue.replace(/</g, '&lt;').replace(/>/g, '&gt;')
    if (cellValue.indexOf('/Date(') !== -1) {
        var date = cellValue.replace(/\/Date\(|\)\//g, '');
        var newDate = new Date(parseInt(date));
        return newDate;
    }
}

Ui.prototype.DatepikerMinDateActualsimple = function () {
    var self = this;
    $('.datepickerMinFechaActuaSimple').datepicker({
        dateFormat: 'yy/mm/dd',
        minDate: new Date()
    });
    $(".datepickerMinFechaActuaSimple").val('');
}

function init() {
    var ui = new Ui();
    ui.init();
    
};

init();