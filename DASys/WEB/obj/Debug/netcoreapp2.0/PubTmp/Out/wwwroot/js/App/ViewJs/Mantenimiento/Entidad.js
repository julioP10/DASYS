
var contacto = null;
var dataTableEntidad = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Entidad';
var tituloRegistro = 'Registrar  Entidad';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';
var _permiso = 0;

var Entidad = function () {
    var EvaluarPermiso = function () {
         _permiso = $("#Permiso").val();
        if (_permiso==0) {
            $("#_registrarEntidad").remove();
        }
    }
    var dataTableEntidadView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#EntidadDataTable')) {
            dataTableEntidad = $("#EntidadDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idEntidad + '" value="' + obj.idEntidad + '" ><label class="custom-control-label" for="' + obj.idEntidad + '"></label></div >';
                        }
                    },
                    { "data": "idEntidad" },
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarEntidad"  data-id="' + obj.idEntidad + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            if (_permiso==1) {
                                return '<div><a href="javascript:void(0)" class="btnConsultarEntidad" data-id="' + obj.idEntidad + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarEntidad"  data-id="' + obj.idEntidad + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                                <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idEntidad + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';
                            } else {
                                return '<div></div>';
                            }


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                    { "className": "dt-body-center", "aTargets": [1], "width": "5%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                    { "bSortable": false, "bVisible": (_permiso==1)? true:false,  "className": "datatable-td-opciones", "aTargets": [4], "width": "1%" }
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

    var Eventos = function (Entidad_CTL) {
        //Entidad
        //Registrar Entidad
        $(Entidad_CTL).on("click", "button#btn-RegistrarEntidad", function () {
            
            var form = $("#formEntidad");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var EntidadMensaje = $("#IdEntidad").val();
                var msj = ((EntidadMensaje == '' || EntidadMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((EntidadMensaje == '' || EntidadMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((EntidadMensaje == '' || EntidadMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoEntidad, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlEntidadIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Entidad_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdEntidad: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Entidad",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarEntidad, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Entidad", jsonResponse.mensaje, jsonResponse.type);
                        dataTableEntidad.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Entidad", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });

        //Regresar al index
        $(Entidad_CTL).on("click", "button#btn-RegresarEntidad", function () {
            webApp.getDataVistaViewPartial(urlEntidadIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Entidad_CTL).on("click", "a#btnEntidadRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarEntidad, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Entidad_CTL).on("click", "a.btnConsultarEntidad", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdEntidad: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarEntidad, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Entidad_CTL).on("click", "a.btnEditarEntidad", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdEntidad: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarEntidad, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Entidad_CTL).on("change", "input#IdEstado", function () {
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdEntidad: $("#IdEntidad").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarEntidad, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Categoria", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Entidad_CTL = $("#Entidad");
            EvaluarPermiso();
            Eventos(_this.Entidad_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableEntidadView(urlEntidadPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio','FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableEntidad.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);