
var contacto = null;
var dataTableProveedor = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Proveedor';
var tituloRegistro = 'Registrar  Proveedor';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Proveedor = function () {

    var dataTableProveedorView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#ProveedorDataTable')) {
            dataTableProveedor = $("#ProveedorDataTable").dataTable({
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
                        };
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idProveedor + '" value="' + obj.idProveedor + '" ><label class="custom-control-label" for="' + obj.idProveedor + '"></label></div >';
                        }
                    },
                    { "data": "codigo" },
                   
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarProveedor"  data-id="' + obj.idProveedor + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "descripcion" },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            return '<div><a href="javascript:void(0)" class="btnConsultarProveedor" data-id="' + obj.idProveedor + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarProveedor"  data-id="' + obj.idProveedor + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idProveedor + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                    { "className": "dt-body-center", "aTargets": [1], "width": "5%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [4], "width": "10%" },
                    { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [5], "width": "1%" }
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

    var Eventos = function (Proveedor_CTL) {
        //Proveedor
        //Registrar Proveedor
        $(Proveedor_CTL).on("click", "button#btn-RegistrarProveedor", function () {
            
            var form = $("#formProveedor");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var ProveedorMensaje = $("#IdProveedor").val();
                var msj = ((ProveedorMensaje == '' || ProveedorMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((ProveedorMensaje == '' || ProveedorMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((ProveedorMensaje == '' || ProveedorMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoProveedor, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlProveedorIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Proveedor_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdProveedor: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Proveedor",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarProveedor, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Proveedor", jsonResponse.mensaje, jsonResponse.type);
                        dataTableProveedor.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Proveedor", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Proveedor_CTL).on("change", "input#IdEstado", function () {
            
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdProveedor: $("#IdProveedor").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarProveedor, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Proveedor", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });
        //Regresar al index
        $(Proveedor_CTL).on("click", "button#btn-RegresarProveedor", function () {
            webApp.getDataVistaViewPartial(urlProveedorIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Proveedor_CTL).on("click", "a#btnProveedorRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarProveedor, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Proveedor_CTL).on("click", "a.btnConsultarProveedor", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdProveedor: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarProveedor, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Proveedor_CTL).on("click", "a.btnEditarProveedor", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdProveedor: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarProveedor, param, function () {
                webApp.CampoLlenoInput();
            });
        });
      
      

    }
    return {
        init: function () {
            var _this = this;
            _this.Proveedor_CTL = $("#Proveedor");
            Eventos(_this.Proveedor_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableProveedorView(urlProveedorPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio','FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableProveedor.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);