
var contacto = null;
var dataTableOpcion = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Opcion';
var tituloRegistro = 'Registrar  Opcion';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Opcion = function () {

    var dataTableOpcionView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#OpcionDataTable')) {
            dataTableOpcion = $("#OpcionDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idOpcion + '" value="' + obj.idOpcion + '" ><label class="custom-control-label" for="' + obj.idOpcion + '"></label></div >';
                        }
                    },
                    { "data": "idOpcion" },
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarOpcion"  data-id="' + obj.idOpcion + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "descripcion" },
                    { "data": "area" },
                    { "data": "controlador" },
                    { "data": "accion" },
                    { "data": "posicion" },
                    { "data": "icono" },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            return '<div><a href="javascript:void(0)" class="btnConsultarOpcion" data-id="' + obj.idOpcion + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarOpcion"  data-id="' + obj.idOpcion + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idOpcion + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                    { "className": "dt-body-center", "aTargets": [1], "width": "5%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [4], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [5], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [6], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [7], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [8], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [9], "width": "10%" },
                    { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [10], "width": "1%" }
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

    var Eventos = function (Opcion_CTL) {
        //Opcion
        //Registrar Opcion
        $(Opcion_CTL).on("click", "button#btn-RegistrarOpcion", function () {
            
            var form = $("#formOpcion");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var OpcionMensaje = $("#IdOpcion").val();
                var msj = ((OpcionMensaje == '' || OpcionMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((OpcionMensaje == '' || OpcionMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((OpcionMensaje == '' || OpcionMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoOpcion, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlOpcionIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Opcion_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdOpcion: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Opcion",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarOpcion, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Opcion", jsonResponse.mensaje, jsonResponse.type);
                        dataTableOpcion.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Opcion", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });

        //Regresar al index
        $(Opcion_CTL).on("click", "button#btn-RegresarOpcion", function () {
            webApp.getDataVistaViewPartial(urlOpcionIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Opcion_CTL).on("click", "a#btnOpcionRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarOpcion, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Opcion_CTL).on("click", "a.btnConsultarOpcion", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdOpcion: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarOpcion, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Opcion_CTL).on("click", "a.btnEditarOpcion", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdOpcion: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarOpcion, param, function () {
                webApp.CampoLlenoInput();
            });
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Opcion_CTL = $("#Opcion");
            Eventos(_this.Opcion_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableOpcionView(urlOpcionPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio','FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableOpcion.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);