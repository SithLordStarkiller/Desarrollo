/// <reference path="registros.js" />
Ui.prototype.init = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.Cliente = {
        /// Información del cliente
        Solicitantes: [],
        Contactos: [],
        Documentos: [],
        /// Información del cliente

        /// Información de la solicitud a guardar
        Solicitud: {
            Documento: {},
            Servicios: []
        }
    };

    this.Servicio = {
        Documentos: [],
        Instalaciones: []
    };

    this.Solicitud = {
        Solicitudes: []
    }

    this.Numero = 0;
    this.urls = {
        clientes: this.getUrl('/Solicitud/ClientesConsulta'),
        guardar: this.getUrl('/Solicitud/ClienteGuardar'),
        registro: this.getUrl('/Solicitud/Registro'),

        buscar: this.getUrl('/Solicitud/ClienteConsultaPorCriterio'),

        instalaciones: this.getUrl('/Solicitud/SolicitudInstalaciones'),
        autocomplete: {
            razonSocial: this.getUrl('/Solicitud/ClientesObtenerPorRazonSocial'),
            nombreCorto: this.getUrl('/Solicitud/ClientesObtenerPorNombreCorto'),
            rfc: this.getUrl('/Solicitud/ClientesObtenerPorRFC')
        },

        solicitantes: this.getUrl('/Solicitud/Solicitantes'),
        solicitante: this.getUrl('/Solicitud/Solicitante'),
        contactos: this.getUrl('/Solicitud/Contactos'),
        contacto: this.getUrl('/Solicitud/Contacto'),

        documento: this.getUrl('/Files/UploadFile'),

        servicios: this.getUrl('/Solicitud/ObtenerServicio'),
        servicio: this.getUrl('/Solicitud/ServicioCaptura'),

        modificacion: this.getUrl('/Solicitud/ModificarSolicitud'),
        modificaciones: this.getUrl('/Solicitud/ModificarSolicitudes'),

        documentoServicios: this.getUrl('/Solicitud/ClienteDocumentos'),
        documentoServicio: this.getUrl('/Files/UploadFile'),

        saveSolicitud: this.getUrl('/Solicitud/GuardarSolicitud')
    };
    this.mensajes = {
        fileError: 'El archivo no puede ser leido.',
        fileErrorType: 'El archivo no es del tipo requerido, solo puede cargar archivos con la extensión pdf.',
        fileErrorSchema: 'El archivo no tiene el formato correcto.',
        saveMessage: 'Registro de la solicitud se ha guardado correctamente.'
    };
    this.controls = {
        grids: {
            cliente: new controls.Grid('divRegistros', { maxHeight: 400, pager: { show: true, pageSize: 10, currentPage: 1, pages: 1 }, showPlusButton: false })
        },
        buttons: {
            create: $("#btnCreate"),
            regresar: document.getElementById("btnBack"),
            buscar: document.getElementById("btnBuscar")
        }
    };
    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Régimen Fiscal', field: 'RegimenFiscal', css: 'col-xs-2' },
        { text: 'Sector', field: 'Sector', css: 'col-xs-2' },
        { text: 'Razón Social (Dependencia o Empresa Privada)', field: 'RazonSocial', css: 'col-xs-4' },
        { text: 'Nombre Corto', field: 'NombreCorto', css: 'col-xs-3' },
        { text: 'RFC', field: 'RFC', css: 'col-xs-2' },
        { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        { text: '', field: 'Agregar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Add' } } }];

    this.controls.grids.cliente.init(columns);
    this.controls.grids.cliente.addListener("onPage", function (evt) {
        self.controls.grids.cliente.currentPage = evt.currentPage;
        self.loadData();
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
                self.controls.grids.cliente.reload([]);
                self.controls.grids.cliente.reload(data);
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
                RazonSocial: $("input[name=razonSocial]").val(),
                NombreCorto: $("input[name=nombreCorto]").val(),
                RFC: $("input[name=rfc]").val(),
                IdRegimenFiscal: $("#regimenFiscal").val(),
                IdSector: $("#sector").val()
            }, Paging: {
                CurrentPage: self.controls.grids.cliente.currentPage,
                Rows: self.controls.grids.cliente.pageSize
            }
        };
        self.SendAjax('POST', self.urls.buscar, 'json', data, success);

    });

    this.controls.grids.cliente.addListener("onButtonClick", function (evt) {
        var url = "";
        var data = {};
        var model = {};
        var validacion = "";
        var tipoResultado = null;

        var getPartial = function (data) {
            self.functions.hideGrid(data);
            /*
            Inicializamos el objeto de cliente con sus solicitantes y contactos,
            para que estos sean cargados en la vista a partir del objeto modelo
            */
            if (clienteModel) {
                self.Cliente.Solicitantes = clienteModel.Solicitantes ? clienteModel.Solicitantes : [];
                self.Cliente.Contactos = clienteModel.Contactos ? clienteModel.Contactos : [];
                self.Cliente.Instalaciones = clienteModel.Instalaciones ? clienteModel.Instalaciones : [];
            }
            self.contactos();
            self.solicitantes();

            /// Reiniciamos la variable de solicitud a sus valores iniciales
            self.Cliente.Solicitud = {
                Documento: [],
                Servicios: []
            };

            $("#TipoSolicitud1").click(function () {
                $("#TipoSolicitud1").prop("checked", true);
                $("#TipoSolicitud2").prop("checked", false);
                $("#TipoSolicitud3").prop("checked", false);
            });
            $("#TipoSolicitud2").click(function () {
                $("#TipoSolicitud1").prop("checked", false);
                $("#TipoSolicitud2").prop("checked", true);
                $("#TipoSolicitud3").prop("checked", false);

                var partial = function (data) {
                    $("#modificarSolicitudes").html(data).fadeIn('fast',
                        function () {
                            self.modificaciones();
                            $('#classModal').modal('show');
                        });
                }
                self.SendAjax('POST', self.urls.modificacion, 'html', {}, partial);
            });
            $("#TipoSolicitud3").click(function () {
                $("#TipoSolicitud1").prop("checked", false);
                $("#TipoSolicitud2").prop("checked", false);
                $("#TipoSolicitud3").prop("checked", true);
            });

            $('#btnBack').on('click', function () {
                self.functions.hideForm(data);
                $("HTML, BODY").animate({
                    scrollTop: 0
                }, 1000);
                $("#Header").show();
            });

            $("#ServicesType").on("click", function (evt) {
                var id = $("#IdTipoServicio").val();
                if (id == null || id == undefined || id == "") {
                    self.showMessage("Debes seleccionar el tipo de servicio.");
                } else {
                    /// Reiniciamos la variable ya que es un nuevo servicio
                    self.Servicio = {
                        Documentos: [],
                        Instalaciones: []
                    };

                    var partial = function (data) {
                        $("#HideView").fadeOut('slow',
                            function () {
                                $("#partial1").html(data).fadeIn('fast',
                                    function () {
                                        self.documentoServicios();
                                        //self.documentos();

                                        if ($('#instalacionesCliente').length) {
                                            self.instalaciones();
                                        }
                                    });
                                $("HTML, BODY").animate({
                                    scrollTop: 0
                                },
                                    1000);
                                $("#ActiveComite").click(function () {
                                    var IsActive = $("#ActiveComite").prop('checked');
                                    if (IsActive == false) {
                                        $("#Comite").prop("disabled", false);
                                    } else {
                                        $("#Comite").prop("disabled", true);
                                        $("#Comite").val("");
                                    }
                                });

                                self.removeValidations("#formCaptura");

                                $("#FechaInicio").datepicker({
                                    dateFormat: 'yy/mm/dd',

                                });
                                $("#FechaFinal").datepicker({
                                    dateFormat: 'yy/mm/dd'
                                });
                                $("#FechaExamen").datepicker({
                                    dateFormat: 'yy/mm/dd'
                                });

                                $("#FechaInicio").change(function () {
                                    $("#FechaInicio").removeClass(" input-validation-error");
                                    $("span[for=FechaInicio]").removeClass().remove();
                                });
                                $("#FechaFinal").change(function () {
                                    $("#FechaFinal").removeClass(" input-validation-error");
                                    $("span[for=FechaFinal]").removeClass().remove();
                                });
                                $("#FechaExamen").change(function () {
                                    $("#FechaExamen").removeClass(" input-validation-error");
                                    $("span[for=FechaExamen]").removeClass().remove();
                                });

                                $("#CancelService").on('click',
                                    function () {
                                        $("#Header").hide();
                                        $('#partial1').fadeOut('slow',
                                            function () {
                                                $("#IdTipoServicio").val(null);
                                                $("#HideView").fadeIn('fast');
                                                $("HTML, BODY").animate({
                                                    scrollTop: 0
                                                },
                                                    1000);
                                            });
                                    });

                                var success = function (data) {
                                    if (data.Result === 1) {
                                        self.showMessage("Solicitud guardado exitosamente.");
                                    } else {
                                        self.showMessage(
                                            "Hubo un error al guardar, favor de reportar al administrador.");
                                    }
                                };
                                $("#SaveService").on("click",
                                    function () {
                                        var docSer = self.Servicio.Documentos;
                                        var inst = self.Servicio.Instalaciones;

                                        $.validator.unobtrusive.parse("#formCaptura");
                                        if (!$("#formCaptura").valid()) { $("HTML, BODY").animate({ scrollTop: 0 }, 1000); return; }
                                        /// La instalación mínima debe tener uno al menos
                                        if ($("#instalacionesCliente").length > 0 && (inst.length == 0 || inst.length == undefined)) {
                                            $("HTML, BODY").animate({ scrollTop: 0 }, 1000);
                                            self.showMessage("Debes seleccionar al menos una instalación.");
                                            return;
                                        }
                                        /// El servicio mínimo debe tener un documento agregado
                                        if (docSer.length == 0 || docSer.length == undefined) {
                                            $("HTML, BODY").animate({ scrollTop: 0 }, 1000);
                                            self.showMessage("Debe seleccionar al menos un documento.");
                                            return;
                                        }

                                        var saveSolicitud = function () {
                                            /// creamos un nuevo servicio
                                            var servicio = {
                                                FechaInicial: $("#FechaInicio").length > 0
                                                    ? $("#FechaInicio").val()
                                                    : ($("#FechaExamen").length > 0
                                                        ? $("#FechaExamen").val()
                                                        : null),
                                                FechaFinal: $("#FechaFinal").length > 0
                                                    ? $("#FechaFinal").val()
                                                    : null,
                                                Documentos: self.Servicio.Documentos,
                                                NumeroPersonas: $("#NumeroPersonas").length > 0
                                                    ? $("#NumeroPersonas").val
                                                    : undefined,
                                                Observaciones: $("#Observaciones").length > 0
                                                    ? $("#Observaciones").val()
                                                    : undefined,
                                                TieneComite: $("#ActiveComite").length > 0
                                                    ? $("#ActiveComite").prop("checked")
                                                    : undefined,
                                                ObservacionesComite: $("#Comite").length > 0
                                                    ? $("#Comite").val()
                                                    : undefined,
                                                BienCustodia: $("#TipoBienCustodiar").length > 0
                                                    ? $("#TipoBienCustodiar").val()
                                                    : undefined,
                                                FechaComite: Date.now(),
                                                Cuota: {
                                                    Identificador: $("#IdCuota").length > 0
                                                        ? $("#IdCuota").val()
                                                        : undefined
                                                },
                                                TipoServicio: {
                                                    Identificador: $("#IdTipoServicio").length > 0
                                                        ? $("#IdTipoServicio").val()
                                                        : undefined,
                                                    Nombre: $("#IdTipoServicio option:selected").text()
                                                },
                                                HorasCurso: $("#HorasCurso").length > 0
                                                    ? $("#HorasCurso").val()
                                                    : undefined,
                                                TipoInstalacionesCapacitacion: {
                                                    Identificador: $("#IdTipoInstalacion").length > 0
                                                        ? $("#IdTipoInstalacion").val()
                                                        : undefined
                                                },
                                                Instalaciones: self.Servicio.Instalaciones
                                            };
                                            $("#Header").hide();
                                            $('#partial1').fadeOut('slow',
                                                function () {
                                                    $("#HideView").fadeIn('fast');
                                                    $("HTML, BODY").animate({ scrollTop: 0 }, 1000);
                                                    setTimeout(function () {
                                                        self.showMessage('El Servicio de tipo "' + $("#IdTipoServicio option:selected").text()
                                                            + '" fue agregado correctamente.');
                                                        $("#IdTipoServicio").val(null);
                                                    }, 1000);

                                                    /// Finaliza la animación de regresar y carga el grid de Servicios en la pantalla de solicitud
                                                    self.controls.grids.servicios.reload(self.Cliente.Solicitud
                                                        .Servicios);
                                                });
                                            /// Insertamos el servicio nuevo en la solicitud
                                            self.Cliente.Solicitud.Servicios.push(servicio);

                                        };

                                        //$("#Document").removeClass(" input-validation-error");
                                        //$("#fileUrls").removeClass(" input-validation-error");
                                        //$("span[data-valmsg-for=Document]").removeClass().remove();
                                        //$("span[data-valmsg-for=fileUrls]").removeClass().remove();
                                        //$("#Document").removeAttr("data-val-required");
                                        //$("#fileUrls").removeAttr("data-val-required");
                                        saveSolicitud();


                                        function ok() {
                                            var id = $("#IdTipoServicio").val(); // validar el numero de tipo servicio
                                            //var bienCustodia = "";
                                            // obtener fecha actual formato yy/mm/dd
                                            var d = new Date();
                                            var month = d.getMonth() + 1;
                                            var day = d.getDate();
                                            var getDate = d.getFullYear() +
                                                '/' +
                                                (month < 10 ? '0' : '') +
                                                month +
                                                '/' +
                                                (day < 10 ? '0' : '') +
                                                day;
                                            /// para solicitudes
                                            var idClient = $("#Identificador").val();
                                            var tipoSolicitud = $("input[type=radio]:checked").val();
                                            var documento = $("#documento").val();
                                            var folio = "";
                                            var fechaReg = getDate;
                                            var minuta = 1;
                                            var cancelado = false;
                                            // para servicios
                                            var idSoli = 0; // falta obtener el id
                                            var idTipoServ = $("#IdTipoServicio").val();
                                            var idCuota = $("#Curso").val();
                                            var tipoIns = $("#TipoInstalacion").val();
                                            var bienCust = bienCustodia;
                                            var fechaComite = getDate;
                                            var idInstalacion = 1;
                                            // para servicio documentos
                                            var IdServ = 0;
                                            var IdTipoDoc = 0;
                                            var archivo = $("#Document").val();
                                            var nombreArch = $("#Url").val();
                                            var DocSop = 1;
                                            var fechaRegis = getDate;
                                            var Observ = "";
                                            var esSoporte = 1;
                                            var Active = true;

                                            var dataSolicitud = {
                                                ObjectResult: {
                                                    FechaRegistro: fechaRegis,
                                                    Cancelado: cancelado,
                                                    TipoSolicitud: {
                                                        Identificador: tipoSolicitud
                                                    },
                                                    Cliente: {
                                                        Identificador: idClient,
                                                        UniqueId: clienteModel.UniqueId
                                                    },
                                                    Documento: self.Cliente.Documentos[0],
                                                    TipoSolicitud: {
                                                        Identificador: $("input[type=radio]:checked").val()
                                                    },
                                                    Servicios: [
                                                        {
                                                            FechaInicial: $("#FechaInicio").val(),
                                                            FechaFinal: $("#FechaFinal").val(),
                                                            FechaFin: $("#FechaFinal").val(),
                                                            Documentos: self.Cliente.DocumentoServicios,
                                                            NumeroPersonas: $("#NumeroPersonas").val(),
                                                            Observaciones: $("#Observaciones").val(),
                                                            TieneComite: $("#ActiveComite").prop("checked"),
                                                            ObservacionesComite: $("#Comite").val(),
                                                            BienCustodia: bienCust,
                                                            FechaComite: fechaReg,
                                                            Cuota: {
                                                                Identificador: $("#IdCurso").val()
                                                            },
                                                            TipoServicio: {
                                                                Identificador: $("#IdTipoServicio").val()
                                                            },
                                                            HorasCurso: $("#HorasCurso").val(),
                                                            TipoInstalacionesCapacitacion: {
                                                                Identificador: $("#IdTipoInstalacion").val()
                                                            } //,
                                                            //Instalaciones: {

                                                            //}
                                                        }
                                                    ]
                                                },
                                                Paging: {
                                                    CurrentPage: self.controls.grids.cliente.currentPage,
                                                    Rows: self.controls.grids.cliente.pageSize
                                                }
                                            };

                                            self.SendAjax('POST',
                                                self.urls.saveSolicitud,
                                                'json',
                                                dataSolicitud,
                                                success);
                                        };

                                    });

                            });
                    };
                    var cuotas = {
                        idTipoServicio: id
                    }
                    self.SendAjax('POST', self.urls.servicio, 'html', cuotas, partial);
                }
            });

            $("#btnSave").on("click", function (evt) {
                /// TODO: Extraer la informacion de la solicitud y enviarla a guardar a la base de datos

                var getPartial = function (data) {
                    if (data.Result === 1) {
                        self.functions.hideForm(); $("HTML, BODY").animate({ scrollTop: 0 }, 1000);
                        setTimeout(function() { self.showMessage("La solicitud se ha guardado con éxito"); }, 1000);
                    } else {
                        self.showMessage(data.Message);
                    }
                };
                var docSol = self.Cliente.Solicitud.Documento;
                var serv = self.Cliente.Solicitud.Servicios;

                /// La solicitud debe tener al menos un servicio agregado
                if (serv.length === 0 || serv.length == undefined) {
                    self.showMessage("Debe seleccionar al menos un servicio.");
                    return;
                }

                /// La solicitud debe tener al menos un documento agregado
                if (docSol.Numero === 0 || docSol.Numero === undefined) {
                    self.showMessage("Debe seleccionar al menos un documento.");
                    return;
                }

                var obj = {
                    ObjectResult: {
                        TipoSolicitud:
                        {
                            Identificador: $("input[name=tipoSolicitud]").val()
                        },
                        Cliente: {
                            Identificador: clienteModel.Identificador,
                            UniqueId: clienteModel.UniqueId
                        },
                        Servicios: self.Cliente.Solicitud.Servicios,
                        Documento: self.Cliente.Solicitud.Documento
                    }
                };
                self.SendAjax('POST', self.urls.saveSolicitud, 'json', obj, getPartial);

            });

            var columnsGrids = {
                documentos: [
                    { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
                    { text: 'Nombre', field: 'Nombre', css: 'col-xs-5' }
                ],
                servicios: [
                    { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
                    { text: 'Nombre', field: 'TipoServicio.Nombre', css: 'col-xs-10' },
                    { text: '', field: 'Quitar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Delete' } } }]
            };

            self.controls.grids = $.extend(self.controls.grids,
                {
                    documentos: new controls.Grid('clienteDocumentos', { maxHeight: 400, pager: { show: false } }),
                    servicios: new controls.Grid('clienteServicios', { maxHeight: 400, pager: { show: true, pageSize: 5, currentPage: 1, pages: 2 }, showPlusButton: false })
                },
                true);
            self.controls.grids.documentos.init(columnsGrids.documentos);
            self.controls.grids.servicios.init(columnsGrids.servicios);

            self.documentos();
            self.servicios();
        };

        switch (evt.event) {
            case 'Edit':
            case 'Add':
                $("#Header").hide('fast');
            case 'View':
                url = self.urls.registro;
                data = { model: $.extend(evt.dataRow, { Action: evt.event }) };
                model = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                $("#Header").hide('fast');
                break;
            case 'New':
                url = self.urls.registro;
                data = { model: { Action: evt.event } };
                this.model = data;
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
                    ObjectResult: object,
                    Paging: {
                        CurrentPage: self.controls.grids.cliente.currentPage,
                        Rows: self.controls.grids.cliente.pageSize
                    }
                };
                self.SendAjax('POST', url, 'json', data, success);
                break;
            case 'Delete':
                var quitarServicio = $.grep(self.Cliente.Solicitud.Servicios, function (element, index) {
                    return !(element.row === evt.row);
                });
                self.Cliente.Solicitud.Servicios = quitarServicio;
                break;
            case 'Location':
                /* Aquí enviamos la pantalla */
                break;
        }
    });

    self.loadData();
    self.autocomplete('#razonSocial', this.urls.autocomplete.razonSocial);
    self.autocomplete('#nombreCorto', this.urls.autocomplete.nombreCorto);
    self.autocomplete('#rfc', this.urls.autocomplete.rfc);
};

Ui.prototype.documentos = function () {
    var self = this;

    self.controls.grids.documentos.reload([]);

    if (self.Cliente)
        if (self.Cliente.Solicitud && self.Cliente.Solicitud.Documentos)
            self.controls.grids.documentos.reload(self.Cliente.Solicitud.Documentos);

    var jqXHRData = undefined;
    'use strict';

    $('#documento').fileupload({
        formData: { Directory: clienteModel.UniqueId },
        maxFileSize: 5000000000,
        url: self.urls.documento,
        dataType: 'json',
        add: function (e, data) {
            jqXHRData = data;
            /// Validar la extensión pdf y PDF
            let m = self.fileExtension(data.files[0].name, /(\.|\/)(pdf|PDF)$/i);
            if (m && jqXHRData) {
                jqXHRData.submit();
            }
            else {
                self.showMessage('Error al cargar el archivo: ' + self.mensajes.fileErrorType);
            }
            //return false;
        },
        done: function (event, data) {
            if (data.result.isUploaded) {
                data.result.documento.Numero = self.Cliente.Documentos.length + 1;

                self.Cliente.Solicitud.Documento = data.result.documento;
                if (self.Cliente.Documentos.length > 0)
                    self.Cliente.Documentos[0] = data.result.documento;
                else
                    self.Cliente.Documentos.push(data.result.documento);

                self.controls.grids.documentos.loadRows([]);
                self.controls.grids.documentos.loadRows(self.Cliente.Documentos);
            }
        },
        fail: function (event, data) {
            if (data.files[0].error) {
                alert(data.files[0].error);
            }
        }
    });

    $("input[type=file][name=documentoSolicitud]").on("change", function() {
        var input = $(this),
            numFiles = input.get(0).files ? input.get(0).files.length : 1,
            label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
        $("#fileUrl").val(label);
        input.trigger('fileselect', [numFiles, label]);
    });

    $("#clienteDocumentos table tfoot").remove();
};

Ui.prototype.servicios = function () {
    var self = this;
    self.controls.grids.servicios.loadRows([]);

    if (self.Cliente)
        if (self.Cliente.Servicios)
            self.controls.grids.servicios.loadRows(self.Cliente.Servicios);


    this.controls.grids.servicios.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();

        switch (evt.event) {
            case 'Active':
            case 'Inactive':
                var messageOptions = {
                    title: !evt.dataRow.IsActive ? "Activar" : "Desactivar",
                    aceptar: function () {
                        var findElement = function (item) {
                            return item.Numero == evt.dataRow.Numero;
                        };
                        var servicioIndex = self.Cliente.Servicios.findIndex(findElement);
                        self.Cliente.Servicios[servicioIndex].IsActive = !self.Cliente.Servicios[servicioIndex].IsActive;
                        self.controls.grids.servicios.loadRows([]);
                        self.controls.grids.servicios.loadRows(self.Cliente.Servicios);
                    }
                };
                self.confirmacion(evt.dataRow.IsActive ? self.mensajes.desactivaMensaje : self.mensajes.activaMensaje, messageOptions);
                break;
            case 'Delete':
                var servicioMessageOptions = {
                    title: "Quitar servicio",
                    aceptar: function () {
                        var quitarServicio = $.grep(self.Cliente.Solicitud.Servicios, function (element, index) {
                            return (index !== (evt.row - 1));
                        });
                        self.Cliente.Solicitud.Servicios = quitarServicio;

                        self.controls.grids.servicios.loadRows([]);
                        self.controls.grids.servicios.loadRows(self.Cliente.Solicitud.Servicios);
                    }
                };
                self.confirmacion("¿Se encuentra seguro de quitar el servicio?", servicioMessageOptions);
                break;
        }
    });
};

Ui.prototype.documentoServicios = function () {
    var self = this;
    var controls = gob.Controls;

    this.controls.grids = $.extend(this.controls.grids, {
        documentoServicios: new controls.Grid('servicioDocumentos', { maxHeight: 400, pager: { show: true, pageSize: 4, currentPage: 1, pages: 2 }, showPlusButton: false })
    }, true);

    var columns = [
        { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' }
    ];

    self.controls.grids.documentoServicios.init(columns);
    self.controls.grids.documentoServicios.loadRows([]);
    if (self.Servicio.Documentos.length > 0)
        self.controls.grids.documentoServicios.reload(self.Servicio.Documentos);

    var jqXHRData = undefined;
    var getPartial = function (data) {
        if (data.length) {
            this.controls.grids.documentoServicios.reload(data);
        }

        'use strict';

        $('#Document').fileupload({
            formData: { Directory: clienteModel.UniqueId },
            maxFileSize: 5000000000,
            url: self.urls.documentoServicio,
            dataType: 'json',
            add: function (e, data) {
                jqXHRData = data;
                /// Validar la extensión pdf y PDF
                let m = self.fileExtension(data.files[0].name, /(\.|\/)(pdf|PDF)$/i);
                if (m && jqXHRData) {
                    //$("#Document").removeClass(" input-validation-error");
                    //$("#fileUrls").removeClass(" input-validation-error");
                    //$("span[data-valmsg-for=Document]").removeClass().remove();
                    //$("span[data-valmsg-for=fileUrls]").removeClass().remove();
                    //$("#Document").removeAttr("data-val-required");
                    //$("#fileUrls").removeAttr("data-val-required");
                    jqXHRData.submit();
                }
                else {
                    self.showMessage('Error al cargar el archivo: ' + self.mensajes.fileErrorType);
                }
                //return false;
            },
            done: function (event, data) {
                if (data.result.isUploaded) {
                    data.result.documento.Numero = self.Servicio.Documentos.length + 1;
                    self.Servicio.Documentos.push(data.result.documento);

                    self.controls.grids.documentoServicios.reload([]);
                    self.controls.grids.documentoServicios.reload(self.Servicio.Documentos);
                }
            },
            fail: function (event, data) {
                if (data.files[0].error) {
                    alert(data.files[0].error);
                }
            }
        });

        $("input[type=file][name=documentoServicio]").on("change", function () {
            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
            $("#fileUrls").val(label);
            input.trigger('fileselect', [numFiles, label]);
        });
    };

    var data = { ClienteId: clienteModel.Identificador };
    self.SendAjax('POST', self.urls.documentoServicios, 'json', data, getPartial);
};

Ui.prototype.modificaciones = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    self.controls.grids = $.extend(this.controls.grids, {
        modificaciones: new controls.Grid('modificarSolicitudesRegistros', { maxHeight: 400, pager: { show: true, pageSize: 4, currentPage: 1, pages: 2 }, showPlusButton: false }),
    }, true);

    var columns = [
        { text: 'No.', iskey: true, css: 'col-xs-0', visible: true },
        { text: 'Folio', field: 'Folio', css: 'col-xs-1' },
        //{ text: 'Tipo Servicio', field: 'TipoServicio.Nombre', css: 'col-xs-2' },
        { text: 'Razón Social', field: 'RazonSocial', css: 'col-xs-3' },
        { text: 'Nombre Corto', field: 'NombreCorto', css: 'col-xs-1' },
        { text: 'RFC', field: 'RFC', css: 'col-xs-1' },
        { text: 'Fecha', field: 'FechaRegistro', css: 'col-xs-1' },
        { text: 'Tipo Solicitud', field: 'TipoSolicitud.Nombre', css: 'col-xs-2' },
        { text: 'Estatus', field: 'Estatus', css: 'col-xs-1' },
        { text: '', field: 'Editar', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Cancelar', css: 'col-xs-0', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Delete' } } }
    ];

    //$("#modificarSolicitudesRegistros table tfoot").remove();

    $("#Close").on("click", function (evt) {
        $("#TipoSolicitud1").prop("checked", true);
        $("#TipoSolicitud2").prop("checked", false);
    });

    self.controls.grids.modificaciones.init(columns);
    self.controls.grids.modificaciones.loadRows([]);
    if (self.Solicitud.Solicitudes.length > 0)
        self.controls.grids.modificaciones.reload(self.Solicitud.Solicitudes);

    var getPartialModificaciones = function (data) {
        if (data.List.length) {
            self.controls.grids.modificaciones.reload(data.List);
        }
    };

    var data = { Identificador: clienteModel.Identificador };
    self.SendAjax('POST', self.urls.modificaciones, 'json', data, getPartialModificaciones);
};

Ui.prototype.instalaciones = function () {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;

    self.controls.grids = $.extend(self.controls.grids, {
        instalaciones: new controls.Grid('instalacionesCliente', { maxHeight: 400, pager: { show: true, pageSize: 5, currentPage: 1, pages: 2 }, showPlusButton: false })
    }, true);

    var columns = [
        { text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
        { text: 'Zona', field: 'NombreZona', css: 'col-xs-2' },
        { text: 'Estación', field: 'NombreEstacion', css: 'col-xs-2' },
        { text: 'Estado', field: 'NombreEstado', css: 'col-xs-2' },
        { text: 'Municipio', field: 'NombreMunicipio', css: 'col-xs-2' },
        {
            text: 'Vigente',
            field: 'Zona.Vigente',
            css: 'col-xs-1',
            cellType: {
                type: cons.cellType.CHECKBOX,
                cssClass: {
                    active: 'form-control'
                }
            },
            controlSettings: {
                block: true
            }
        },
        {
            text: 'Seleccione',
            field: 'Seleccionado',
            css: 'col-xs-1',
            cellType: {
                type: cons.cellType.CHECKBOX,
                cssClass: {
                    active: 'form-control'
                }
            },
            controlSettings: {
                blockByFieldValue: {
                    block: true,
                    fieldName: 'Zona.Vigente',
                    value: false
                }
            }
        }
    ];

    this.controls.grids.instalaciones.init(columns);
    this.controls.grids.instalaciones.reload([]);

    if (self.Cliente && self.Cliente.Instalaciones)
        self.controls.grids.instalaciones.reload(self.Cliente.Instalaciones);

    var getPartial = function (dataInstalacion) {
        if (dataInstalacion.length) {
            self.controls.grids.instalaciones.reload([]);
            self.controls.grids.instalaciones.reload(dataInstalacion);
        }
    };

    self.controls.grids.instalaciones.addListener("onCheckClick", function (evt) {
        var url = "";
        var data = {};
        var model = {};
        var checked = evt.checked;
        var idInstalcion = evt.dataRow.Identificador;

        // TODO: Buscamos sí se encuentra en el arreglo de instalaciones
        if (checked)
            self.Servicio.Instalaciones.push({
                row: evt.row,
                Identificador: idInstalcion
            });
        else {
            // TODO: busca el id en el arreglo y si lo encuentra lo expulsa del arreglo
            var instalacion = $.grep(self.Servicio.Instalaciones, function (element, index) {
                return !(element.row === evt.row);
            });
            self.Servicio.Instalaciones = instalacion;
        }
    });

    var solicitud = {
        solicitud: {
            Cliente: { Identificador: clienteModel.Identificador },
            Servicio: { Identificador: 0 }
        }
    };
    self.SendAjax('POST', self.urls.instalaciones, 'json', solicitud, getPartial);
};

Ui.prototype.fileExtension = function (fileName, extensions) {
    var regex = extensions;
    var str = fileName;
    return regex.exec(str);
};



//Funciones de los datos del cliente//
Ui.prototype.contactos = function () {
    var self = this;
    this.contactosArray = [];
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.controls.grids = $.extend(this.controls.grids, {
        contactos: new controls.Grid('clienteContactos', { maxHeight: 400, pager: { show: false } }),
    }, true);

    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
        { text: 'Apellido Paterno', field: 'ApellidoPaterno', css: 'col-xs-2' },
        { text: 'Apellido Materno', field: 'ApellidoMaterno', css: 'col-xs-2' },
        { text: 'Cargo', field: 'Cargo', css: 'col-xs-3' },
        { text: 'Tipo', field: 'TipoContacto', css: 'col-xs-4' },
        //{ text: '', field: 'Editar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        /*{ text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }*/];

    /* Si el registro unicamente es para visualizar entonces no debe mostrar los campos de editar y activar y desactivar solo el de ver detalle */
    if (clienteModel.Action === 2) {
        columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
            { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
            { text: 'Apellido Paterno', field: 'ApellidoPaterno', css: 'col-xs-2' },
            { text: 'Apellido Materno', field: 'ApellidoMaterno', css: 'col-xs-2' },
            { text: 'Cargo', field: 'Cargo', css: 'col-xs-3' },
            { text: 'Tipo', field: 'TipoContacto', css: 'col-xs-4' },
            { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } }];
    }

    this.controls.grids.contactos.init(columns);
    this.controls.grids.contactos.loadRows([]);

    if (self.Cliente)
        if (self.Cliente.Contactos)
            this.controls.grids.contactos.loadRows(self.Cliente.Contactos);

    /*Autocomplete Change*/

    $("#clienteContactos table tfoot").remove();
    this.controls.grids.contactos.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        var dataRow = undefined;
        var getPartial = function (data) {
            var divContacto = document.getElementById("partialContacto");
            $(divContacto).html('');
            $(divContacto).append(data);
            $(divContacto).toggle("slow");//.addClass("display-form").removeClass("hide-form");
            $(self.controls.grids.contactos.grid).toggle("slow");//.addClass("hide-form");
            self.removeValidations(divContacto);
            self.loadTelefonosCorreos(dataRow);

            $('#btnBackContacto').click(function () {
                $(divContacto).toggle("slow");//.addClass("hide-form").removeClass("display-form");
                $(self.controls.grids.contactos.grid).toggle("slow");//.removeClass("hide-form").addClass("display-form");
            });

            $('#btnSaveContacto').click(function () {
                $.validator.unobtrusive.parse("#formContacto");
                if (!$("#formContacto").valid()) return;
                var data = {
                    Identificador: $("#Identificador", "#formSolicitante").val(),
                    Nombre: $("#Nombre", "#formContacto").val(),
                    ApellidoPaterno: $("#ApellidoPaterno", "#formContacto").val(),
                    ApellidoMaterno: $("#ApellidoMaterno", "#formContacto").val(),
                    Cargo: $("#Cargo", "#formContacto").val(),
                    IdTipoPersona: 2,
                    IdTipoContacto: $("#IdTipoContacto", "#formContacto").val(),
                    TipoContacto: $("#IdTipoContacto option:selected", "#formContacto").text(),
                    IsActive: $("#IsActive", "#formContacto").val() == 'True',
                    Numero: self.Cliente.Contactos.length === 0 ? 1 : self.Cliente.Contactos.length + 1,
                    Telefonos: self.Contacto.Telefonos,
                    Correos: self.Contacto.Correos
                };

                var contactos = $.grep(self.Cliente.Contactos, function (element, index) {
                    return (evt.dataRow !== undefined) ? (element.Numero !== evt.dataRow.Numero) : true;
                });

                if (contactos === undefined)
                    self.Cliente.Contactos.push(data);
                else {
                    self.Cliente.Contactos = contactos;
                    if (evt.dataRow !== undefined)
                        data.Numero = evt.dataRow.Numero;
                    self.Cliente.Contactos.push(data);
                }
                var order = function (a, b) {
                    if (a.Nombre < b.Nombre)
                        return -1
                    if (a.Nombre > b.Nombre)
                        return 1;
                    return 0;
                };
                self.Cliente.Contactos.sort(order);
                self.controls.grids.contactos.reload(self.Cliente.Contactos);
                $(divContacto).toggle("slow");
                $(self.controls.grids.contactos.grid).toggle("slow");

                self.Contacto = { Telefonos: [], Correos: [] };
            });
        };

        switch (evt.event) {
            case 'Edit':
            case 'Add':
            case 'View':
                url = self.urls.contacto;
                data = { contacto: $.extend(evt.dataRow, { Action: evt.event }) };
                model = data;
                dataRow = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.contacto;
                data = { model: { Action: evt.event } };
                this.model = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
                //var solictante = evt.dataRow;
                //var item = self.cliente.solicitantes.find(function (item) {
                //    return item.Nombre === solicitante.Nombre && item.Paterno === solictante.Paterno && item.Materno === solicitante.Materno;
                //});
                //self.cliente.solictantes.pop(item);


                break;
        }
    });
};

Ui.prototype.loadTelefonosCorreos = function (data) {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.Contacto = data === undefined ? { Telefonos: [], Correos: [] } : data.contacto === undefined ? { Telefonos: [], Correos: [] } : data.contacto;

    this.controls.grids = $.extend(this.controls.grids, {
        telefonos: new controls.Grid('clienteContactosTelefonos', { maxHeight: 400, pager: { show: false } }),
        correos: new controls.Grid('clienteContactosCorreos', { maxHeight: 400, pager: { show: false } })
    }, true);

    var columnsTelefonos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Tipo', field: 'TipoTelefono', css: 'col-xs-2' },
        { text: 'Numero', field: 'Numero', css: 'col-xs-2' },
        { text: 'Extension', field: 'Extension', css: 'col-xs-4' },
        /*{ text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }*/];

    var columnsCorreos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Correo', field: 'CorreoElectronico', css: 'col-xs-2' },
        /*{ text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }*/];

    /* Si el registro unicamente es para visualizar entonces no debe mostrar los campos de editar y activar y desactivar solo el de ver detalle */
    if (clienteModel.Action == "View") {
        columnsTelefonos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
            { text: 'Tipo', field: 'TipoTelefono', css: 'col-xs-2' },
            { text: 'Numero', field: 'Numero', css: 'col-xs-2' },
            { text: 'Extension', field: 'Extension', css: 'col-xs-4' }];

        columnsCorreos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
            { text: 'Correo', field: 'CorreoElectronico', css: 'col-xs-2' }];
    }

    this.controls.grids.telefonos.init(columnsTelefonos);
    this.controls.grids.telefonos.loadRows(self.Contacto.Telefonos === undefined ? [] : self.Contacto.Telefonos);

    this.controls.grids.correos.init(columnsCorreos);
    this.controls.grids.correos.loadRows(self.Contacto.Correos === undefined ? [] : self.Contacto.Correos);

    this.controls.grids.telefonos.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        switch (evt.event) {

            case 'Active':
            case 'Inactive':
                //var array = self.array.contacto.telefonos.filter(function (item) {                    
                //    if (item.Numero !== evt.dataRow.Numero)
                //        return item;
                //});
                var array = Contacto.Telefonos.filter(function (item) {
                    if (item.Numero !== evt.dataRow.Numero)
                        return item;
                });
                self.controls.grids.telefonos.reload(array);
                //self.array.contacto.telefonos = array;
                Contacto.Telefonos = array;
                break;
        }
    });
    this.controls.grids.correos.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        switch (evt.event) {

            case 'Active':
            case 'Inactive':
                //var array = self.array.contacto.correos.filter(function (item) {
                //    if (item.Numero !== evt.dataRow.Numero)
                //        return item;
                //});
                var array = Contacto.Correos.filter(function (item) {
                    if (item.Numero !== evt.dataRow.Numero)
                        return item;
                });
                self.controls.grids.correos.reload(array);
                Contacto.Correos = array;
                break;
        }
    });
    $("#partialContacto #btnTelefono").click(function () {
        $.validator.unobtrusive.parse($("#formTelefono", "#partialContacto"));
        if (!$("#formTelefono", "#partialContacto").valid()) return;

        var data = {
            IdTipoTelefono: $("#IdTipoTelefono", "#partialContacto").val(),
            TipoTelefono: $("#IdTipoTelefono option:selected", "#partialContacto").text(),
            Numero: $("#Numero", "#partialContacto").val(),
            Extension: $("#Extension", "#partialContacto").val(),
            IsActive: true
        };
        self.Contacto.Telefonos.push(data);
        self.controls.grids.telefonos.reload(self.Contacto.Telefonos);
        $("#IdTipoTelefono option:selected", "#partialContacto").removeAttr("selected");
        $("#IdTipoTelefono:first-child", "#partialContacto").attr("selected", "selected");
        $("#Numero", "#partialContacto").val("");
        $("#Extension", "#partialContacto").val("");
    });

    $("#partialContacto #btnCorreo").click(function () {
        $.validator.unobtrusive.parse($("#formCorreo", "#partialContacto"));
        if (!$("#formCorreo", "#partialContacto").valid()) return;

        var data = {
            CorreoElectronico: $("#CorreoElectronico", "#partialContacto").val()
        };
        self.Contacto.Correos.push(data);
        self.controls.grids.correos.reload(self.Contacto.Correos);
        $("#CorreoElectronico", "#partialContacto").val("");
    });

    $("#clienteContactosTelefonos table tfoot").remove();
    $("#clienteContactosCorreos table tfoot").remove();
};

Ui.prototype.solicitantes = function () {
    var self = this;
    this.contactosArray = [];
    var controls = gob.Controls;
    var cons = gob.Constants;

    this.controls.grids = $.extend(this.controls.grids, {
        solicitantes: new controls.Grid('clienteSolicitantes', { maxHeight: 400, pager: { show: false } }),
    }, true);

    var columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
        { text: 'Apellido Paterno', field: 'ApellidoPaterno', css: 'col-xs-2' },
        { text: 'Apellido Materno', field: 'ApellidoMaterno', css: 'col-xs-2' },
        { text: 'Cargo', field: 'Cargo', css: 'col-xs-3' },
        //{ text: '', field: 'Editar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } },
        { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } },
        /*{ text: '', field: 'IsActive', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { active: 'Active', inactive: 'Inactive' } } }*/];

    /* Si el registro unicamente es para visualizar entonces no debe mostrar los campos de editar y activar y desactivar solo el de ver detalle */
    if (clienteModel.Action === 2) {
        columns = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
            { text: 'Nombre', field: 'Nombre', css: 'col-xs-2' },
            { text: 'Apellido Paterno', field: 'ApellidoPaterno', css: 'col-xs-2' },
            { text: 'Apellido Materno', field: 'ApellidoMaterno', css: 'col-xs-2' },
            { text: 'Cargo', field: 'Cargo', css: 'col-xs-3' },
            { text: '', field: 'Ver', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'View' } } }];
    }

    this.controls.grids.solicitantes.init(columns);
    this.controls.grids.solicitantes.loadRows([]);

    if (self.Cliente)
        if (self.Cliente.Solicitantes)
            this.controls.grids.solicitantes.loadRows(self.Cliente.Solicitantes);

    this.controls.grids.solicitantes.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        var dataRow = undefined;
        var getPartial = function (data) {
            var divSolicitante = document.getElementById("partialSolicitante");
            $(divSolicitante).html('');
            $(divSolicitante).append(data);
            $(divSolicitante).toggle("slow");
            $(self.controls.grids.solicitantes.grid).toggle("slow");
            self.removeValidations(divSolicitante);
            self.loadTelefonosCorreosSolicitante(dataRow);

            $('#btnBackSolicitante').click(function () {
                $(divSolicitante).toggle("slow");
                $(self.controls.grids.solicitantes.grid).toggle("slow");
            });

            $('#btnSaveSolicitante').click(function () {
                $.validator.unobtrusive.parse("#formSolicitante");
                if (!$("#formSolicitante").valid()) return;
                var data = {
                    Identificador: $("#Identificador", "#formSolicitante").val(),
                    Nombre: $("#Nombre", "#formSolicitante").val(),
                    ApellidoPaterno: $("#ApellidoPaterno", "#formSolicitante").val(),
                    ApellidoMaterno: $("#ApellidoMaterno", "#formSolicitante").val(),
                    Cargo: $("#Cargo", "#formSolicitante").val(),
                    IdTipoPersona: 1,
                    IsActive: $("#IsActive", "#formSolicitante").val() == 'True',
                    Numero: self.Cliente.Solicitantes.length === 0 ? 1 : self.Cliente.Solicitantes.length + 1,
                    Telefonos: self.Solicitante.Telefonos,
                    Correos: self.Solicitante.Correos
                };

                var solicitantes = $.grep(self.Cliente.Solicitantes, function (element, index) {
                    return (evt.dataRow !== undefined) ? (element.Numero !== evt.dataRow.Numero) : true;
                });

                if (solicitantes === undefined)
                    self.Cliente.Solicitantes.push(data);
                else {
                    self.Cliente.Solicitantes = solicitantes;
                    if (evt.dataRow !== undefined)
                        data.Numero = evt.dataRow.Numero;
                    self.Cliente.Solicitantes.push(data);
                }
                var order = function (a, b) {
                    if (a.Nombre < b.Nombre)
                        return -1
                    if (a.Nombre > b.Nombre)
                        return 1;
                    return 0;
                };
                self.Cliente.Solicitantes.sort(order);
                self.controls.grids.solicitantes.reload(self.Cliente.Solicitantes);
                $(divSolicitante).toggle("slow");
                $(self.controls.grids.solicitantes.grid).toggle("slow");

                self.Solicitante = { Telefonos: [], Correos: [] };
            });
        };

        switch (evt.event) {
            case 'Edit':
            case 'Add':
            case 'View':
                url = self.urls.solicitante;
                data = { solicitante: $.extend(evt.dataRow, { Action: evt.event }) };
                model = data;
                dataRow = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'New':
                url = self.urls.solicitante;
                data = { model: { Action: evt.event } };
                this.model = data;
                self.SendAjax('POST', url, 'html', data, getPartial);
                break;
            case 'Active':
                break;
        }
    });

    $("#clienteSolicitantes table tfoot").remove();
};

Ui.prototype.loadTelefonosCorreosSolicitante = function (data) {
    var self = this;
    var controls = gob.Controls;
    var cons = gob.Constants;
    this.Solicitante = data === undefined ? { Telefonos: [], Correos: [] } : data.solicitante === undefined ? { Telefonos: [], Correos: [] } : data.solicitante;

    this.controls.grids = $.extend(this.controls.grids, {
        telefonosSolicitante: new controls.Grid('clienteSolicitantesTelefonos', { maxHeight: 400, pager: { show: false } }),
        correosSolicitante: new controls.Grid('clienteSolicitantesCorreos', { maxHeight: 400, pager: { show: false } })
    }, true);

    var columnsTelefonosSolicitantes = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Tipo', field: 'TipoTelefono', css: 'col-xs-2' },
        { text: 'Numero', field: 'Numero', css: 'col-xs-2' },
        { text: 'Extension', field: 'Extension', css: 'col-xs-4' },
        /*{ text: '', field: 'Editar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } }*/];

    var columnsCorreosSolicitantes = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
        { text: 'Correo', field: 'CorreoElectronico', css: 'col-xs-2' },
        /*{ text: '', field: 'Editar', css: 'col-xs-1', cellType: { type: cons.cellType.BUTTON, cssClass: { normal: 'Edit' } } }*/];

    /* Si el registro unicamente es para visualizar entonces no debe mostrar los campos de editar y activar y desactivar solo el de ver detalle */
    if (clienteModel.Action == "View") {
        columnsTelefonos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
            { text: 'Tipo', field: 'TipoTelefono', css: 'col-xs-2' },
            { text: 'Numero', field: 'Numero', css: 'col-xs-2' },
            { text: 'Extension', field: 'Extension', css: 'col-xs-4' }];

        columnsCorreos = [{ text: 'No.', iskey: true, css: 'col-xs-1', visible: true },
            { text: 'Correo', field: 'CorreoElectronico', css: 'col-xs-2' }];
    }

    this.controls.grids.telefonosSolicitante.init(columnsTelefonosSolicitantes);
    this.controls.grids.telefonosSolicitante.loadRows(self.Solicitante.Telefonos === undefined ? [] : self.Solicitante.Telefonos);

    this.controls.grids.correosSolicitante.init(columnsCorreosSolicitantes);
    this.controls.grids.correosSolicitante.loadRows(self.Solicitante.Correos === undefined ? [] : self.Solicitante.Correos);

    this.controls.grids.telefonosSolicitante.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        switch (evt.event) {

            case 'Add':
                var array = Solicitante.Telefonos.filter(function (item) {
                    if (item.Numero !== evt.dataRow.Numero)
                        return item;
                });
                self.controls.grids.telefonosSolicitante.reload(array);
                Solicitante.Telefonos = array;
                break;
            case 'Edit':
                var array = Solicitante.Telefonos.filter(function (item) {
                    if (item.Numero !== evt.dataRow.Numero)
                        return item;
                });
                self.controls.grids.telefonosSolicitante.reload(array);
                Solicitante.Telefonos = array;
                break;
        }
    });

    this.controls.grids.correosSolicitante.addListener("onButtonClick", function (evt) {
        evt.e.preventDefault();
        switch (evt.event) {

            case 'Add':
                var array = Solicitante.Telefonos.filter(function (item) {
                    if (item.Numero !== evt.dataRow.Numero)
                        return item;
                });
                self.controls.grids.telefonosSolicitante.reload(array);
                Solicitante.Telefonos = array;
                break;
            case 'Edit':
                var array = Solicitante.Correos.filter(function (item) {
                    if (item.Numero !== evt.dataRow.Numero)
                        return item;
                });
                self.controls.grids.correosSolicitante.reload(array);
                Solicitante.Correos = array;
                break;
        }
    });

    $("#partialSolicitante #btnTelefono").click(function () {
        $.validator.unobtrusive.parse($("#formTelefono", "#partialSolicitante"));
        if (!$("#formTelefono", "#partialSolicitante").valid()) return;

        var data = {
            IdTipoTelefono: $("#IdTipoTelefono", "#partialSolicitante").val(),
            TipoTelefono: $("#IdTipoTelefono option:selected", "#partialSolicitante").text(),
            Numero: $("#Numero", "#partialSolicitante").val(),
            Extension: $("#Extension", "#partialSolicitante").val(),
            IsActive: true
        };
        self.Solicitante.Telefonos.push(data);
        self.controls.grids.telefonosSolicitante.reload(self.Solicitante.Telefonos);

        $("#IdTipoTelefono option:selected", "#partialSolicitante").removeAttr("selected");
        $("#IdTipoTelefono:first-child", "#partialSolicitante").attr("selected", "selected");
        $("#Numero", "#partialSolicitante").val("");
        $("#Extension", "#partialSolicitante").val("");
    });

    $("#partialSolicitante #btnCorreo").click(function () {
        $.validator.unobtrusive.parse($("#formCorreo", "#partialSolicitante"));
        if (!$("#formCorreo", "#partialSolicitante").valid()) return;

        var data = {
            CorreoElectronico: $("#CorreoElectronico", "#partialSolicitante").val()
        };
        self.Solicitante.Correos.push(data);
        self.controls.grids.correosSolicitante.reload(self.Solicitante.Correos);
        $("#CorreoElectronico", "#partialSolicitante").val("");
    });

    $("#clienteSolicitantesTelefonos table tfoot").remove();
    $("#clienteSolicitantesCorreos table tfoot").remove();
};
//Funciones de los datos del cliente//


//Funciones de apoyo//
Ui.prototype.loadData = function (evt) {
    var self = this;
    var page = self.controls.grids.cliente.currentPage;
    var rows = self.controls.grids.cliente.pageSize;
    var loadGrid = function (data) {
        self.controls.grids.cliente.reload([]);
        if (data.Result) {
            self.controls.grids.cliente.currentPage = data.Paging.CurrentPage;
            self.controls.grids.cliente.pages = data.Paging.Pages;
            self.controls.grids.cliente.reload(data.List);
        }

    };

    var data = {
        page: page, rows: rows,
        Query: {
            IsActive: $("input[name=estatus]:checked").val(),
        },
    };

    this.SendAjax('POST', self.urls.clientes, 'json', data, loadGrid);
};

Ui.prototype.loadControls = function () {
    var self = this;
    var destino = "";
    var execute = function (data) {
        if (data.Result === 1) {
            self.loadCombo(data.List, destino)
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
//Funciones de apoyo//




//Handler evento click en CheckBox del grid//
gob.Controls.Grid.prototype.checkCell = function (check, event) {
    var row = check.parentNode.parentNode;
    var dataRow = {};
    var rowChild = check.parentNode.parentNode.getAttribute("parent");
    if (rowChild !== null && rowChild !== undefined) {
        var id = row.getAttribute("parent");
        dataRow = { parent: $("tr.parent-tr[id=" + id + "]").data("dataRow"), child: $(row).data('dataRow') };
    } else {
        dataRow = $(row).data('dataRow');
    }

    var column = $.extend(true, {}, this.columnDefault, this.columns[check.parentNode.cellIndex]);

    this.fire({ type: 'onCheckClick', checked: check.checked, row: row.rowIndex, column: check.parentNode.cellIndex, dataRow: dataRow, key: row.id, event: this.showPlusButton && row.rowIndex === 0 ? "New" : check.getAttribute("data-cmd"), e: event });

    if (event) {
        event.stopPropagation();
    }
};
//Handler evento click en CheckBox del grid//




// Iniciamos el flujo de los script en la pantalla
function init() {
    var ui = new Ui();
    ui.init();
};

init();