
var contacto = null;
var dataTableDepartamento = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Departamento';
var tituloRegistro = 'Registrar  Departamento';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Departamento = function () {

    var dataTableDepartamentoView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#DepartamentoDataTable')) {
            dataTableDepartamento = $("#DepartamentoDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idDepartamento + '" value="' + obj.idDepartamento + '" ><label class="custom-control-label" for="' + obj.idDepartamento + '"></label></div >';
                        }
                    },
                    { "data": "idDepartamento" },
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarDepartamento"  data-id="' + obj.idDepartamento + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            return '<div><a href="javascript:void(0)" class="btnConsultarDepartamento" data-id="' + obj.idDepartamento + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarDepartamento"  data-id="' + obj.idDepartamento + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idDepartamento + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                    { "className": "dt-body-center", "aTargets": [1], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [3], "width": "20%" },
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

    var Eventos = function (Departamento_CTL) {
        //Departamento
        //Registrar Departamento
        $(Departamento_CTL).on("click", "button#btn-RegistrarDepartamento", function () {
            
            var form = $("#formDepartamento");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var DepartamentoMensaje = $("#IdDepartamento").val();
                var msj = ((DepartamentoMensaje == '' || DepartamentoMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((DepartamentoMensaje == '' || DepartamentoMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((DepartamentoMensaje == '' || DepartamentoMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoDepartamento, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlDepartamentoIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Departamento_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdDepartamento: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Departamento",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarDepartamento, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Departamento", jsonResponse.mensaje, jsonResponse.type);
                        dataTableDepartamento.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Departamento", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Departamento_CTL).on("change", "input#IdEstado", function () {
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdDepartamento: $("#IdDepartamento").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarDepartamento, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Departamento", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });

        //Regresar al index
        $(Departamento_CTL).on("click", "button#btn-RegresarDepartamento", function () {
            webApp.getDataVistaViewPartial(urlDepartamentoIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Departamento_CTL).on("click", "a#btnDepartamentoRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarDepartamento, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Departamento_CTL).on("click", "a.btnConsultarDepartamento", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdDepartamento: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarDepartamento, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Departamento_CTL).on("click", "a.btnEditarDepartamento", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdDepartamento: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarDepartamento, param, function () {
                webApp.CampoLlenoInput();
            });
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Departamento_CTL = $("#Departamento");
            Eventos(_this.Departamento_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableDepartamentoView(urlDepartamentoPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio', 'FechaFin');
            $('#datetimepicker').datetimepicker();
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableDepartamento.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);