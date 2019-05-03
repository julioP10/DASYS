
var contacto = null;
var dataTableDigitalizacionColaborador = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar DigitalizacionColaborador';
var tituloRegistro = 'Registrar  DigitalizacionColaborador';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var DigitalizacionColaborador = function () {

    var dataTableDigitalizacionColaboradorView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#DigitalizacionColaboradorDataTable')) {
            dataTableDigitalizacionColaborador = $("#DigitalizacionColaboradorDataTable").dataTable({
                "bFilter": false,
                "bProcessing": true,
                "serverSide": true,
                //"scrollY": "400px",
                //"sScrollX": "100%",
                "ajax": {
                    "url": url,
                    "type": "POST",
                    "data": function (request) {
                        //
                        request.filter = {
                            NombreSearch: $("#NombreSearch").val(),
                            IdColaboradorSearch: _colaborador
                        }
                    },
                    dataFilter: function (data) {
                        if (data.substring(0, 9) === "<!DOCTYPE") {
                            redireccionarLogin("Sesión Terminada", "Se terminó la sesión");
                        } else {
                            return data;
                        }
                    }
                },
                "pageLength": 10,
                "bAutoWidth": false,
                "columns": [
                    { "data": "idColaborador" },
                    { "data": "alias" },
                    { "data": "categoria" },
                    { "data": "documento" },
                    {
                        "data": function (obj) {
                            return (obj.adjuntado > 0) ? '<span class="badge badge-pill badge-success">Adjuntado</span>' : '<span class="badge badge-pill badge-danger">Sin adjuntar</span>'
                        }
                    },
                    {
                        "data": function (obj) {
                            
                            return (obj.obligatorio==='SI') ? '<span class="badge badge-pill badge-success">SI</span>' : '<span class="badge badge-pill badge-info">NO</span>'
                        }
                    },
                    {
                        "data": function (obj) {
                            return (obj.fechaVencimiento === 'SI') ? '<span class="badge badge-pill badge-success">SI</span>' : '<span class="badge badge-pill badge-info">NO</span>'
                        }
                    },
                    { "data": "estado" },
                    { "data": "estadoAdjunto" },
                    { "data": "observacion" },
                    { "data": "observacionAdjunto" },
                    {
                        "data": function (obj) {
                            if (_estadoDigitalizacion == "REVISION" || _estadoDigitalizacion == "APROBADO") {
                                return (obj.idDocumentoAdjunto !== null) ? '<div><a href="javascript:void(0)" class="btn-ver-documentos" data-adjuntoestado="' + obj.estadoAdjunto + '"   data-obligatorio="' + obj.obligatorio + '"  data-estado="' + obj.adjuntado + '" data-digitalizacion="' + obj.idDigitalizacion + '" data-id="' + obj.idPersona + '" data-toggle="tooltip" title="Ver Documentos"><i class="fa fa-eye" aria-hidden="true"></i></a> ' : '<div></div> ';

                            } else {
                                return (obj.idDocumentoAdjunto !== null) ? '<div><a href="javascript:void(0)" class="btn-ver-documentos" data-adjuntoestado="' + obj.estadoAdjunto + '"   data-obligatorio="' + obj.obligatorio + '"  data-estado="' + obj.adjuntado + '" data-digitalizacion="' + obj.idDigitalizacion + '" data-id="' + obj.idPersona + '" data-toggle="tooltip" title="Ver Documentos"><i class="fa fa-eye" aria-hidden="true"></i></a> ' : '<div><a href="javascript:void(0)" class="btnDigitalizacionColaborador" data-documento="' + obj.idDocumento + '"  data-obligatorio="' + obj.obligatorio + '" data-vencimiento="' + obj.fechaVencimiento + '" data-digitalizacion="' + obj.idDigitalizacion + '" data-id="' + obj.idPersona + '" data-estado="' + obj.adjuntado + '" data-adjuntoestado="' + obj.estadoAdjunto + '"  data-toggle="tooltip" title="Adjuntar"><i class="fa fa-file-pdf-o" aria-hidden="true"></i></a> ';

                            }
                            //return "" Agregando los botone  <span class="badge badge-pill badge-success">Another One</span>

                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false, "aTargets": [0] },
                    { "className": "dt-body-center", "aTargets": [1], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                    { "bVisible": _adjuntado,"className": "dt-body-center", "aTargets": [4], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [5], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [6], "width": "10%" },
                    { "bVisible": _observacion1, "className": "dt-body-center", "aTargets": [7], "width": "10%" },
                    { "bVisible": _observacion2, "className": "dt-body-center", "aTargets": [8], "width": "10%" },
                    { "bVisible": _estado1,"className": "dt-body-center", "aTargets": [9], "width": "10%" },
                    { "bVisible": _estado2,"className": "dt-body-center", "aTargets": [10], "width": "10%" },
                    { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [11], "width": "1%" }
                ],
                "lengthMenu": [[20, 50, 100, 10000], [20, 50, 100, "All"]],
                "order": [[0, "desc"]],
                "initComplete": function (settings, json) {
                    $('[data-toggle="tooltip"]').tooltip({
                        placement: 'top'
                    });
                },
                "fnDrawCallback": function (oSettings) {
                    $(".selected_" + selectedRow).parents().eq(1).addClass("selected");
                    $('[data-toggle="tooltip"]').tooltip({
                        placement: 'top'
                    });
                }
            });
        //}
    }

    var Eventos = function (DigitalizacionColaborador_CTL) {
        //DigitalizacionColaborador
        $(DigitalizacionColaborador_CTL).on("click", "a.btnDigitalizacionColaborador", function () {
            var obligatorio = $(this).attr("data-obligatorio");
            var vencimiento = $(this).attr("data-vencimiento");
            var documento = $(this).attr("data-documento");
            var digitalizacion = $(this).attr("data-digitalizacion");
            var _digitalizacion = (digitalizacion === "null") ? "" : digitalizacion;
            var colaborador = $(this).attr("data-id");

            let urlDropZone = "/Digitalizacion/Digitalizacion/DropZone";
            let _estadoDigitalizacion = $("#_EstadoDigitalizacion").val();
            let id = $(this).attr("data-digitalizacion");
            var param = {
            }
            webApp.getDataVistaParam(urlDropZone, param, function (status) {
                (vencimiento === "SI") ? $("#espacio #_FechaVencimiento").css("display", "") : $("#espacio #_FechaVencimiento").css("display", "none");
                $("#espacio #IdDocumento").val(documento);
                $("#espacio #IdPersona").val(colaborador);
                $("#espacio #documento").val(documento);
                $("#espacio #IdDigitalizacion").val(_digitalizacion);
                $("#espacio #IdEstado").val(_estadoDigitalizacion);

                $("#espacio #FechaVencimiento").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                $('#espacio .datepicker').datetimepicker({
                    defaultDate: 'now',
                    format: 'DD/MM/YYYY',
                    locale: 'es',
                    icons: {
                        time: "fa fa-clock-o",
                        date: "fa fa-calendar",
                        up: "fa fa-chevron-up",
                        down: "fa fa-chevron-down",
                        previous: 'fa fa-chevron-left',
                        next: 'fa fa-chevron-right',
                        today: 'fa fa-screenshot',
                        clear: 'fa fa-trash',
                        close: 'fa fa-remove'
                    },
                    minDate: 'now'
                });

                var _dropzone = $("#my-awesome-dropzone");
                //inicializar dropzone
                Dropzone.autoDiscover = false;
                myDropzone = new Dropzone("#my-awesome-dropzone", {
                    url: "/Digitalizacion/Digitalizacion/CargarArchivo",
                    maxFilesize: 5,
                    parallelUploads: 10000,
                    addRemoveLinks: true,
                    acceptedFiles: ".pdf",
                    autoProcessQueue: false,
                    addRemoveLinks: true,
                    acceptedFiles: ".pdf",
                    parallelUploads: 10,
                    init: function () {
                        myDropzone = this;
                        this.on("maxfilesexceeded", function (file) {
                            $("#files").val(file.name);
                            this.removeAllFiles();
                            this.addFile(file);
                        });

                        this.on("addedfile", function (file) {
                            if (file.size > "9058401") {
                                swal({
                                    title: "Documentos Adjuntos",
                                    text: "El documento " + file.name + ", tiene un tamaño de " + file.size + " Kb, como maximo esta permido solo 358400 Kb.",
                                    type: "warning"
                                });
                                file.previewElement.parentNode.removeChild(file.previewElement);
                                file.previewElement = null;
                                return false;
                            }

                            if (file.type === 'application/pdf') {
                                this.emit("thumbnail", file, "/img/documento.png");
                            }
                            var tmpDate = new Date();
                            //var now = tmpDate.getTime();
                            var fecha = moment().format("DDMMYYYYmmssmmm");
                            $('#my-awesome-dropzone').find('.dz-preview').find('.dz-details').find('.dz-filename').text(fecha);
                        });

                        this.on("success", function () {
                            myDropzone.options.autoProcessQueue = true;
                            dataTableDigitalizacionColaborador.fnReloadAjax();
                            $("#myModal").modal("hide");
                            myDropzone.removeAllFiles();
                        });
                    }
                });

            });
        });
        $("#espacio").on("click", "button#btn-cancelar-envio", function () {
            myDropzone.removeAllFiles()
        });

        $("#espacio").on("click", "button#btn-enviar-archivo", function () {


            var form = $("#frmDigitalizacionColaborador");
            if (webApp.validateForm(form)) {

                var query = $(form).serialize();
                var mensaje = {
                    title: "Digitalizacion",
                    text: "Estas seguro de enviar los archivos adjuntos?",
                    confirmButtonText: "Si enviar",
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlRegistrarDigitalizacionColaborador, parametros: query }
                    , function (jsonResponse) {

                        if (jsonResponse.isValid) {

                            var _IdDigitalizacion = jsonResponse.data;
                            var _IdDocumento = $("#IdDocumento").val();
                            var _IdPersona = $("#IdPersona").val();
                            var _url = "/Digitalizacion/Digitalizacion/CargarArchivo?IdDocumento=" + _IdDocumento + "&IdDigitalizacion=" + _IdDigitalizacion + "&IdPersona=" + _IdPersona;
                            myDropzone.options.url = _url;
                            myDropzone.processQueue();
                            webApp.sweetmensaje("Digitalizacion", jsonResponse.mensaje, jsonResponse.type);
                            dataTableDigitalizacionColaborador.fnReloadAjax();
                            $("#IdDocumento").val("");
                            $("#IdPersona").val("");
                        }
                        else {
                            webApp.sweetmensaje("Digitalizacion", jsonResponse.mensaje, jsonResponse.type);
                            dataTableDigitalizacionColaborador.fnReloadAjax();
                        }
                        //myDropzone.removeAllFiles();
                    }
                );
            }
        });
        //ver documnetos adjuntos
        $(DigitalizacionColaborador_CTL).on('click', 'a.btn-ver-documentos', function () {
            let urlVerDocumentos = "/Digitalizacion/Digitalizacion/DocumentosAdjuntos";
            let _estadoDigitalizacion = $("#_EstadoDigitalizacion").val();
            let id = $(this).attr("data-digitalizacion");
            var param = {
                IdDigitalizacion: id,
                Tipo: _estadoDigitalizacion
            }
            webApp.getDataVistaParam(urlVerDocumentos, param, function (status) {

            });
        });
        $("#espacio").on("click", "a#btn-cambiar-estado", function () {
            var cTmp = 0;
            var persona = "";
            var digitalizacion = "";
            $('#DigitalizacionColaboradorDataTable tr a').each(function (i, e) {
                var pre = $(e).attr("data-obligatorio");
                var pos = $(e).attr("data-estado");
                persona = $(e).attr("data-id");
                digitalizacion = $(e).attr("data-digitalizacion");
                if (pre === "SI") {
                    if (pos === "0") {
                        $(e).parents("tr").eq(0).css("background-color", "#ff57229c");
                        cTmp++;
                    }

                }
                if (pre === "NO") {
                    if (pos === "0") {
                        $(e).parents("tr").eq(0).css("background-color", "#8bc34a7a");
                    }

                }
            });

            if (cTmp > 0) {
                var _total = (cTmp == 1) ? "registro" : "registros";
                webApp.sweetmensaje("Digitalizacion", "Falta agregar documentos obligatorios de " + cTmp + " " + _total, "warning");
            } else {
                var query = {
                    IdPersona: persona,
                    IdDigitalizacion: digitalizacion,
                    IdEstado: 'REVISION'
                };
                var mensaje = {
                    title: "Digitalizacion",
                    text: "¿Estas seguro de enviar a Revision?",
                    confirmButtonText: "Si acepto",
                    closeOnConfirm: false
                };

                webApp.sweetajax(mensaje, { url: urlRegistrarDigitalizacionColaborador, parametros: query }
                    , function (jsonResponse) {

                        var urlDigitalizacionColaboradorIndex = _urlIndex;
                        if (jsonResponse.type === "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlDigitalizacionColaboradorIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $("#espacio").on("click", "a#btn-cambiar-aprobar", function () {
            var cTmp = 0;
            var revisar = 0;
            var persona = "";
            var digitalizacion = "";
            $('#DigitalizacionColaboradorDataTable tr a').each(function (i, e) {
                var pos = $(e).attr("data-adjuntoestado");
                var pre = $(e).attr("data-obligatorio");
                persona = $(e).attr("data-id");
                digitalizacion = $(e).attr("data-digitalizacion");
                if (pos === "APROBADO") {
                    $(e).parents("tr").eq(0).css("background-color", "#8bc34a7a");
                }

                else if (pos === "DESAPROBADO") {
                    $(e).parents("tr").eq(0).css("background-color", "#ff57229c");
                    cTmp++;
                }
                else if (pre === "SI") {
                    if (pos === "DESAPORBADO") {
                        $(e).parents("tr").eq(0).css("background-color", "#ff57229c");
                        cTmp++;
                    } else {
                        $(e).parents("tr").eq(0).css("background-color", "#ff57229c");
                        cTmp++;
                        revisar++;
                    }

                }

            });

            if (cTmp > 0) {
                var _total = (cTmp == 1) ? "documento desaprobado" : "documentos desaprobados";
                if (revisar > 0) {
                    webApp.sweetmensaje("Revision", "Debe revisar " + revisar + " documentos obligatorios", "warning");
                } else {
                    webApp.sweetmensaje("Revision", "Hay " + cTmp + " " + _total, "warning");
                }


            } else {
                var query = {
                    IdPersona: persona,
                    IdDigitalizacion: digitalizacion,
                    IdEstado: 'APROBADO'
                };
                var mensaje = {
                    title: "Revision",
                    text: "¿Estas seguro de enviar como aprobado?",
                    confirmButtonText: "Si acepto",
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlRegistrarDigitalizacionColaborador, parametros: query }
                    , function (jsonResponse) {
                        var urlDigitalizacionColaboradorIndex = _urlIndex;
                        if (jsonResponse.type === "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlDigitalizacionColaboradorIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $("#espacio").on("click", "a#btn-cambiar-desaprobar", function () {
            var cTmp = 0;
            var persona = "";
            var digitalizacion = "";
            var _digitalizacionDesaprobado = "";
            $('#DigitalizacionColaboradorDataTable tr a').each(function (i, e) {
                var pos = $(e).attr("data-adjuntoestado");
                persona = $(e).attr("data-id");
                digitalizacion = $(e).attr("data-digitalizacion");
                if (pos === "APROBADO") {
                    cTmp++;
                }

                if (pos === "DESAPROBADO") {
                    cTmp++;
                    _digitalizacionDesaprobado += digitalizacion + ",";
                }

            });

            if (cTmp < 0) {
                webApp.sweetmensaje("Revision", "Debe revisar al menos un Documneto", "warning");
            } else {
                var query = {
                    IdPersona: persona,
                    IdDigitalizacion: digitalizacion,
                    IdDigitalizacionDesaprobado: _digitalizacionDesaprobado,
                    IdEstado: 'REGISTRO'
                };
                var mensaje = {
                    title: "Revision",
                    text: "¿Estas seguro de desaprobar?",
                    confirmButtonText: "Si acepto",
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlRegistrarDigitalizacionColaborador, parametros: query }
                    , function (jsonResponse) {
                        var urlDigitalizacionColaboradorIndex = _urlIndex;
                        if (jsonResponse.type === "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlDigitalizacionColaboradorIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $("#espacio").on('click', 'a.btn-desaprobar-documento', function () {
            var _digitalizacion = $(this).attr("data-id");
            $("#btn-aceptar-desaprobacion").attr("data-id", _digitalizacion);
            $("#ModalDesaprobar").modal("show");
        });
        $("#espacio").on("click", "a.btn-revisar-documento", function () {
            $("#_Observacion").parents("div").eq(0).removeClass("has-danger");
            var _digitalizacion = $(this).attr("data-id");
            var _opcion = $(this).attr("data-opcion").toUpperCase();
            if (_opcion === "DESAPROBAR") {
                if ($("#_Observacion").val() === "") {
                    $("#_Observacion").parents("div").eq(0).addClass("has-danger");
                    return;
                }
            }
            var query = {
                IdDigitalizacion: _digitalizacion,
                Opcion: _opcion,
                Observacion: $("#_Observacion").val()
            };
            var mensaje = {
                title: "Revision",
                text: "¿Estas seguro de " + _opcion + "?",
                confirmButtonText: "Si acepto",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlRegistrarDigitalizacionColaborador, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        dataTableDigitalizacionColaborador.fnReloadAjax();
                        $("#ModalDesaprobar").modal("hide");
                        $("#myModal").modal("hide");
                    } else {
                        webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                    }
                }
            );

        });

        $("#espacio").on("click", "a.btn-eliminar-documento", function () {
            var _digitalizacion = $(this).attr("data-id");
            var query = {
                IdDocumentoAdjunto: _digitalizacion,
                Documento: $(this).attr("data-documento")
            };
            var mensaje = {
                title: "Documento",
                text: "¿Estas seguro de eliminar?",
                confirmButtonText: "Si acepto",
                closeOnConfirm: false
            };
            var _url = "/Digitalizacion/Digitalizacion/EliminarArchivo";
            webApp.sweetajax(mensaje, { url: _url, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        dataTableDigitalizacionColaborador.fnReloadAjax();
                        $("#ModalDesaprobar").modal("hide");
                        $("#myModal").modal("hide");
                    } else {
                        webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                    }
                }
            );

        });
    };
    return {
        init: function () {
            var _this = this;
            _this.DigitalizacionColaborador_CTL = $("#DigitalizacionColaborador");
            Eventos(_this.DigitalizacionColaborador_CTL);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableDigitalizacionColaboradorView(urlDigitalizacionListarColaborador);
            });
            $('.datepicker').datetimepicker({
                defaultDate: 'now',
                format: 'DD/MM/YYYY',
                locale: 'es',
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-chevron-up",
                    down: "fa fa-chevron-down",
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-screenshot',
                    clear: 'fa fa-trash',
                    close: 'fa fa-remove'
                },
                minDate: 'now'
            });
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableDigitalizacionColaborador.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);