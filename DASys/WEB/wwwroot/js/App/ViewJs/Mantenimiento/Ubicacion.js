
var contacto = null;
var dataTableUbicacion = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Ubicacion';
var tituloRegistro = 'Registrar  Ubicacion';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Ubicacion = function () {

    var dataTableUbicacionView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#UbicacionDataTable')) {
            dataTableUbicacion = $("#UbicacionDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idUbicacion + '" value="' + obj.idUbicacion + '" ><label class="custom-control-label" for="' + obj.idUbicacion + '"></label></div >';
                        }
                    },
                    { "data": "idUbicacion" },
                   
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarUbicacion"  data-id="' + obj.idUbicacion + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "nivel" },
                    { "data": "departamento" },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            return '<div><a href="javascript:void(0)" class="btnConsultarUbicacion" data-id="' + obj.idUbicacion + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarUbicacion"  data-id="' + obj.idUbicacion + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idUbicacion + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false, "className": "datatable-td-check",  "aTargets": [0] },
                    { "className": "dt-body-center", "aTargets": [1], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "30%" },
                    { "bVisible": false,  "className": "dt-body-center", "aTargets": [3], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [4], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [5], "width": "10%" },
                    { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [6], "width": "1%" }
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

    var Eventos = function (Ubicacion_CTL) {
        //Ubicacion
        //Registrar Ubicacion
        $(Ubicacion_CTL).on("click", "button#btn-RegistrarUbicacion", function () {
            
            var form = $("#formUbicacion");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var UbicacionMensaje = $("#IdUbicacion").val();
                var msj = ((UbicacionMensaje == '' || UbicacionMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((UbicacionMensaje == '' || UbicacionMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((UbicacionMensaje == '' || UbicacionMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoUbicacion, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlUbicacionIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Ubicacion_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdUbicacion: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Ubicacion",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarUbicacion, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Ubicacion", jsonResponse.mensaje, jsonResponse.type);
                        dataTableUbicacion.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Ubicacion", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Ubicacion_CTL).on("change", "input#IdEstado", function () {
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdUbicacion: $("#IdUbicacion").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarUbicacion, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Ubicacion", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });
        //Regresar al index
        $(Ubicacion_CTL).on("click", "button#btn-RegresarUbicacion", function () {
            webApp.getDataVistaViewPartial(urlUbicacionIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Ubicacion_CTL).on("click", "a#btnUbicacionRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarUbicacion, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Ubicacion_CTL).on("click", "a.btnConsultarUbicacion", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdUbicacion: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarUbicacion, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Ubicacion_CTL).on("click", "a.btnEditarUbicacion", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdUbicacion: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarUbicacion, param, function () {
                webApp.CampoLlenoInput();
            });
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Ubicacion_CTL = $("#Ubicacion");
            Eventos(_this.Ubicacion_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableUbicacionView(urlUbicacionPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio','FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableUbicacion.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);