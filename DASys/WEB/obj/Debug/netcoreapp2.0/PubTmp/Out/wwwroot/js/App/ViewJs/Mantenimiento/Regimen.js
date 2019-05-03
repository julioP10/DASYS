
var contacto = null;
var dataTableRegimen = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Regimen';
var tituloRegistro = 'Registrar  Regimen';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Regimen = function () {

    var dataTableRegimenView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#RegimenDataTable')) {
            dataTableRegimen = $("#RegimenDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idRegimen + '" value="' + obj.idRegimen + '" ><label class="custom-control-label" for="' + obj.idRegimen + '"></label></div >';
                        }
                    },
                    { "data": "idRegimen" },
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarRegimen"  data-id="' + obj.idRegimen + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            return '<div><a href="javascript:void(0)" class="btnConsultarRegimen" data-id="' + obj.idRegimen + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarRegimen"  data-id="' + obj.idRegimen + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idRegimen + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                    { "className": "dt-body-center", "aTargets": [1], "width": "5%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                    { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [4], "width": "1%" }
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

    var Eventos = function (Regimen_CTL) {
        //Regimen
        //Registrar Regimen
        $(Regimen_CTL).on("click", "button#btn-RegistrarRegimen", function () {
            
            var form = $("#formRegimen");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var RegimenMensaje = $("#IdRegimen").val();
                var msj = ((RegimenMensaje == '' || RegimenMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((RegimenMensaje == '' || RegimenMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((RegimenMensaje == '' || RegimenMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoRegimen, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlRegimenIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Regimen_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdRegimen: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Regimen",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarRegimen, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Regimen", jsonResponse.mensaje, jsonResponse.type);
                        dataTableRegimen.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Regimen", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Regimen_CTL).on("change", "input#IdEstado", function () {
            debugger
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdRegimen: $("#IdRegimen").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarRegimen, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Regimen", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });
        //Regresar al index
        $(Regimen_CTL).on("click", "button#btn-RegresarRegimen", function () {
            webApp.getDataVistaViewPartial(urlRegimenIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Regimen_CTL).on("click", "a#btnRegimenRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarRegimen, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Regimen_CTL).on("click", "a.btnConsultarRegimen", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdRegimen: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarRegimen, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Regimen_CTL).on("click", "a.btnEditarRegimen", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdRegimen: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarRegimen, param, function () {
                webApp.CampoLlenoInput();
            });
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Regimen_CTL = $("#Regimen");
            Eventos(_this.Regimen_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableRegimenView(urlRegimenPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio', 'FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableRegimen.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);