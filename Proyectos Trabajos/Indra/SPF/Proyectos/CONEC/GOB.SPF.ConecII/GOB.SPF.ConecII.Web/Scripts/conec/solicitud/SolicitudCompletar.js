/*
 * Funciones diversas para completar solicitud
 * 
 * 
 * Programó: Alfredo Barrios Cruz
 * Fecha: 20-Oct-2017
 *   
 */
var commonFunctions = {
    pad:function(numero, size) {
        var sSs = numero + "";
        while (sSs.length < size) sSs = "0" + sSs;
        return sSs;
    },
    ConvertToDate: function (param) {
        var ddtt = eval(("new " + param).replace(/\//g, ""));
        var dd = ddtt.getDay();
        var mm = ddtt.getMonth() + 1;
        var yy = ddtt.getYear();

        return commonFunctions.pad(dd, 2) + "/" + commonFunctions.pad(mm, 2) + "/" + commonFunctions.pad(yy, 4);
    },
    makarena: function () { }
};

var modelo = {};
Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    //this.urls = {
    //    autocomplete: {
    //        razonSocial: this.getUrl(''),
    //        nombreCorto: this.getUrl(''),
    //        rfc: this.getUrl('')
    //    },
    //    listado: {
    //        busqueda: this.getUrl(''),
    //        complementar: this.getUrl('')
    //    },
    //    solicitud: {
    //        obtener: this.getUrl('')
    //    }
    //};

    this.urls = {
        buscar: this.getUrl('/Solicitud/ComplementarSolicutudListaGrid'),
        ComplementoSolicitud: this.getUrl('/Solicitud/ComplementoSolicitud'),
        autocompleterazonsocial: this.getUrl('/Solicitud/ClientesObtenerPorRazonSocial'),
        autocompletenombrecorto: this.getUrl('/Solicitud/ClientesObtenerPorNombreCorto'),
        autocompleteRFC: this.getUrl('/Solicitud/ClientesObtenerPorRFC'),
        GetSolicitud: this.getUrl('/Solicitud/GetSolicitud'),
        gomigomi: this.getUrl('/Catalogo/gomigomi')
    };

    this.messages = {
        updatequestion: "¿Está seguro que desea actualizar la cuota del factor?.",
        activequestion: "Al realizar esta acción afectará a la generación de las cotizaciones y la generación de recibos. “¿Está seguro de que desea @ el factor seleccionado?.",
    };

    var columns = [
        { text: 'No.', iskey: true, isHierarchy: true, css: 'col-xs-1', visible: true },
        { text: 'No. Solicitud', field: 'Identificador', css: 'col-xs-2' },
        { text: 'Nombre Corto', field: 'Cliente.NombreCorto', css: 'col-xs-7' },
        { text: 'RFC', field: 'Cliente.RFC', css: 'col-xs-2' }
    ];
    var columnsIns = [
       { text: 'No. Servicio', iskey: true, ishisHierarchy: false, css: 'col-xs-1', visible: true },
        { text: 'Tipo Servicio', field: 'TipoServicio.Nombre', css: 'col-xs-10' },
        //{ text: '', field: 'Ver', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'Editar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } }
    ];
    this.controls = {
        grids: {
            solicitudes: new controls.Grid('divRegistros', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 1, pages: 1 }, showPlusButton: false, detail: { field: "Servicios", columns: columnsIns } })
        },
        buttons: {
            buscar: document.getElementById("btnBuscar")
        }
    };



    this.controls.grids.solicitudes.init(columns);

    this.controls.grids.solicitudes.addListener("onPage", function (evt) {
        self.controls.grids.solicitudes.currentPage = evt.currentPage;
        self.load();
    });

    this.functions = {
        ShowForm: function (data) {
            try {
                $("#SearchView").fadeOut();
                $("#partial1").slideToggle();
                $("#partial1").html(data);
                $("#partial1").fadeIn();


                var data = { id: $(".IdSolicitud").val() };
                var CurrentSolicitud = function (soledad) {
                    var step = "0";
                    try {
                        modelo = soledad;
                    } catch (e) {
                        alert("Error en CurrentSolicitud step " + step + ": " + e.message);
                    }

                };


                self.SendAjax('POST', self.urls.GetSolicitud, 'json', data, CurrentSolicitud);

            } catch (e) {
                alert("Error ShowForm: " + e.message);
            }
        },
        HideForm: function (data) {
            try {
                $("#partial1").fadeOut();
                $("#SearchView").fadeIn();
            } catch (e) {
                alert("Error HideForm: " + e.message);

            }
        }
    };

    $(this.controls.buttons.buscar).click(function () {
        try {
            self.GridBuscarReloaded();
        } catch (e) {
            alert("Error buscar.click: " + e.message);
        }

    });

    this.controls.grids.solicitudes.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        var getPartial = function (data) {
            try {
                $("#SearchView").fadeOut('slow', function () {
                    self.functions.ShowForm(data);
                    $("HTML, BODY").animate({
                        scrollTop: 0
                    }, 1000);
                    $("#hRegresar").click(function () {
                        self.functions.HideForm();
                    });
                    $("#btnBack").click(function () {
                        self.functions.HideForm();
                    });
                    $("#tabContainer").tabs();

                    $.fn.select2.defaults.set("theme", "classic");
                    $("#idInstalacion").select2({
                        theme: "classic"
                    });

                    $("#idInstalacion").change(function () {
                        var htmlOriginalContent = "<div class='panel-body'>";
                        htmlOriginalContent += "<div class='form-horizontal'>";
                        htmlOriginalContent += "<div class='form-group'>";
                        htmlOriginalContent += "<div class='row'>";
                        htmlOriginalContent += "<div class='col-md-1'>";
                        htmlOriginalContent += "&nbsp;";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div id='NombreInstalacion' class='col-md-10'>{NombreInstalacion}</div>";
                        htmlOriginalContent += "<div class='col-md-1'>";
                        htmlOriginalContent += "&nbsp;";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='row'>";
                        htmlOriginalContent += "<div class='col-md-1'>";
                        htmlOriginalContent += "&nbsp;";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div id='divDireccionInstalacion' class='col-md-10'>{DireccionInstalacion}</div>";
                        htmlOriginalContent += "<div class='col-md-1'>";
                        htmlOriginalContent += "&nbsp;";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='row'>";
                        htmlOriginalContent += "<div class='col-md-1'>";
                        htmlOriginalContent += "&nbsp;";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='col-md-5'>";
                        htmlOriginalContent += "<div class='input-group date' id='datetimepicker1'>";
                        htmlOriginalContent += "<input type='text' id='FechaInicioInstalacion' class='form-control Fecha' value='{FechaInicioInstalacion}' />";
                        htmlOriginalContent += "<span class='input-group-addon'>";
                        htmlOriginalContent += "<span class='glyphicon glyphicon-calendar'></span>";
                        htmlOriginalContent += "</span>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='col-md-5'>";
                        htmlOriginalContent += "<div class='input-group date' id='datetimepicker1'>";
                        htmlOriginalContent += "<input type='text' id='FechaFinInstalacion' class='form-control Fecha' value='{FechaFinInstalacion}' />";
                        htmlOriginalContent += "<span class='input-group-addon'>";
                        htmlOriginalContent += "<span class='glyphicon glyphicon-calendar'></span>";
                        htmlOriginalContent += "</span>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='col-md-1'>";
                        htmlOriginalContent += "&nbsp;";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='row'>";
                        htmlOriginalContent += "<div class='col-md-4'>";
                        htmlOriginalContent += "<label for='divDivisionInstalacion'>Divisi&oacute;n</label>";
                        htmlOriginalContent += "<div id='divDivisionInstalacion'>{DivisionInstalacion}</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='col-md-4'>";
                        htmlOriginalContent += "<label for='divGrupoInstalacion'>Grupo</label>";
                        htmlOriginalContent += "<div id='divGrupoInstalacion'>{GrupoInstalacion}</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='col-md-4'>";
                        htmlOriginalContent += "<label for='divFraccionInstalacion'>Fracci&oacute;n</label>";
                        htmlOriginalContent += "<div id='divFraccionInstalacion'>{FraccionInstalacion}</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='row'>";
                        htmlOriginalContent += "<div class='col-md-12'>";
                        htmlOriginalContent += "<label for='divDescripcionInstalacion'>Descripci&oacute;n</label>";
                        htmlOriginalContent += "<div id='divDescripcionInstalacion'>{DescripcionInstalacion}</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='row'>";
                        htmlOriginalContent += "<div class='col-md-4'>";
                        htmlOriginalContent += "<label for='divActividadInstalacion'>Actividad</label>";
                        htmlOriginalContent += "<div id='divActividadInstalacion'>{ActividadInstalacion}</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "";
                        htmlOriginalContent += "<div class='col-md-4'>";
                        htmlOriginalContent += "<label for='divDistanciaInstalacion'>Distancia</label>";
                        htmlOriginalContent += "<div id='divDistanciaInstalacion'>{DistanciaInstalacion}</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "<div class='col-md-4'>";
                        htmlOriginalContent += "<label for='divCriminalidadInstalacion'>Criminalidad</label>";
                        htmlOriginalContent += "<div id='divCriminalidadInstalacion'>{CriminalidadInstalacion}</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "</div>";
                        htmlOriginalContent += "</div>";
                        var htmlContent = "";
                        try {
                            $("#Instalaciones").html("");

                            var sltdVals = $(this).val() + "";
                            sltdVals = sltdVals.split(',');


                            for (var iI=0; iI < sltdVals.length; iI++) {
                                for (var iI2=0; iI2 < modelo.Servicio.ServicioInstalaciones.length; iI2++) {
                                    if (modelo.Servicio.ServicioInstalaciones[iI2].Identificador == sltdVals[iI]) {

                                        htmlContent = htmlOriginalContent;
                                        htmlContent = htmlContent.replace("{NombreInstalacion}", modelo.Servicio.ServicioInstalaciones[iI2].Nombre);
                                        htmlContent = htmlContent.replace("{DireccionInstalacion}", modelo.Servicio.ServicioInstalaciones[iI2].DireccionCompleta);
                                        htmlContent = htmlContent.replace("{FechaInicioInstalacion}", commonFunctions.pad(modelo.Servicio.ServicioInstalaciones[iI2].FechaInicio, 2));
                                        htmlContent = htmlContent.replace("{FechaFinInstalacion}", commonFunctions.pad(modelo.Servicio.ServicioInstalaciones[iI2].FechaFin, 2));
                                    }
                                }
                            }
                            $("#Instalaciones").html(htmlContent);
                        } catch (e) {
                            alert("Error in idInstalacion change: " + e.message);
                        }

                    });


                });

            } catch (e) {
                alert("Error getPartial: " + e.message);
            }
        };
        try {
            switch (evt.event) {
                case 'Edit':
                    url = self.urls.ComplementoSolicitud;
                    data = { model: { Action: evt.event } };
                    self.model = data;
                    self.SendAjax('POST', url, 'html', data, getPartial);
                    break;
                case 'View':
                    alert('button View');
                    break;

            };
        } catch (e) {
            alert("Error en boton de grid " + evt.event + " : " + e.message);
        }
    });


    self.load();
};

/**
* Método que carga la lista de clientes en el grid
*
* @method load
* @param {evt} evt Evento de combo
*/
Ui.prototype.load = function (evt) {
    try {
        this.autocomplete('#razonSocial', this.urls.autocompleterazonsocial, function () { return void (0); });
        this.autocomplete('#nombreCorto', this.urls.autocompletenombrecorto, function () { return void (0); });
        this.autocomplete('#rfc', this.urls.autocompleteRFC, function () { return void (0); });

        this.GridBuscarReloaded();
    } catch (e) {
        alert("Error en load " + evt.event + " : " + e.message);
    }

};

Ui.prototype.GridBuscarReloaded = function () {
    var self = this;
    var page = 1; //self.controls.grids.solicitudes.currentPage;
    var rows = 1; //self.controls.grids.solicitudes.pageSize;
    var loadGrid = function (data) {
        //self.controls.grids.solicitudes.reload([]);
        if (data.Result) {
            self.controls.grids.solicitudes.currentPage = data.Paging.CurrentPage;
            self.controls.grids.solicitudes.pages = data.Paging.Pages;
            self.controls.grids.solicitudes.reload(data.List);
        }
    };

    var data = {
        page: page,
        rows: rows,
        Query: {
            Identificador: $("#IdSolicitud").val(),
            RazonSocial: $("#razonSocial").val(),
            NombreCorto: $("#nombreCorto").val(),
            RFC: $("#RFC").val(),
            Estatus: $("#Estatus").val()
        }
    };

    this.SendAjax('POST', self.urls.buscar, 'json', data, loadGrid);
};

Ui.prototype.loadControls = function () {
    var self = this;
    var destino = "";
    var execute = function (data) {
        if (data.Result === 1) {
            self.loadCombo(data.List, destino);
        }
    };
};

Ui.prototype.autocomplete = function (selector, url, $function) {
    if ($(selector).length)
        $(selector).autocomplete({
            source: url,
            minLength: 3,
            scroll: true,
            select: $function
        });
};


function init() {
    var ui = new Ui();
    ui.init();
};
init();
