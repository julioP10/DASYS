
var contacto = null;
var dataTableConfiguracion = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Configuracion';
var tituloRegistro = 'Registrar  Configuracion';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Configuracion = function () {

    var dataTableConfiguracionView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#ConfiguracionDataTable')) {
            dataTableConfiguracion = $("#ConfiguracionDataTable").dataTable({
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
                            NombreSearch: $("#NombreSearch").val()
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
                    {
                        "data": function (obj) {
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idConfiguracion + '" value="' + obj.idConfiguracion + '" ><label class="custom-control-label" for="' + obj.idConfiguracion + '"></label></div >';
                        }
                    },
                    { "data": "idConfiguracion" },
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarConfiguracion"  data-id="' + obj.idConfiguracion + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "tiempoColor" },
                    { "data": "tiempoEntreMarcaciones" },
                    { "data": "tiempoRELAY" },
                    { "data": "tipo" },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            return '<div><a href="javascript:void(0)" class="btnConsultarConfiguracion" data-id="' + obj.idConfiguracion + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarConfiguracion"  data-id="' + obj.idConfiguracion + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idConfiguracion + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                    { "className": "dt-body-center", "aTargets": [1], "width": "5%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "20%" },
                    { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [4], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [5], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [6], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [7], "width": "10%" },
                    { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [8], "width": "1%" }
                ],
                "order": [[1, "desc"]],
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

    var Eventos = function (Configuracion_CTL) {
        //Configuracion
        //Registrar Configuracion
        $(Configuracion_CTL).on("click", "button#btn-RegistrarConfiguracion", function () {
            
            var form = $("#formConfiguracion");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var ConfiguracionMensaje = $("#IdConfiguracion").val();
                var msj = ((ConfiguracionMensaje == '' || ConfiguracionMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((ConfiguracionMensaje == '' || ConfiguracionMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((ConfiguracionMensaje == '' || ConfiguracionMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoConfiguracion, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlConfiguracionIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Configuracion_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdConfiguracion: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Configuracion",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarConfiguracion, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Configuracion", jsonResponse.mensaje, jsonResponse.type);
                        dataTableConfiguracion.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Configuracion", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Configuracion_CTL).on("change", "input#IdEstado", function () {
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdConfiguracion: $("#IdConfiguracion").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarConfiguracion, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Configuracion", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });
        //Regresar al index
        $(Configuracion_CTL).on("click", "button#btn-RegresarConfiguracion", function () {
            
            webApp.getDataVistaViewPartial(urlConfiguracionIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Configuracion_CTL).on("click", "a#btnConfiguracionRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarConfiguracion, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Configuracion_CTL).on("click", "a.btnConsultarConfiguracion", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdConfiguracion: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarConfiguracion, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Configuracion_CTL).on("click", "a.btnEditarConfiguracion", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdConfiguracion: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarConfiguracion, param, function () {
                webApp.CampoLlenoInput();
            });
        });


    }
    return {
        init: function () {
            var _this = this;
            _this.Configuracion_CTL = $("#Configuracion");
            Eventos(_this.Configuracion_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableConfiguracionView(urlConfiguracionPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio', 'FechaFin');
            $('#datetimepicker').datetimepicker();
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableConfiguracion.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);