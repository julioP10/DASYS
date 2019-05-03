
var contacto = null;
var dataTableCondicion = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Condicion';
var tituloRegistro = 'Registrar  Condicion';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Condicion = function () {

    var dataTableCondicionView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#CondicionDataTable')) {
            dataTableCondicion = $("#CondicionDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idCondicion + '" value="' + obj.idCondicion + '" ><label class="custom-control-label" for="' + obj.idCondicion + '"></label></div >';
                        }
                    },
                    { "data": "idCondicion" },
                   
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarCondicion"  data-id="' + obj.idCondicion + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "regimen" },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            return '<div><a href="javascript:void(0)" class="btnConsultarCondicion" data-id="' + obj.idCondicion + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarCondicion"  data-id="' + obj.idCondicion + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idCondicion + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


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

    var Eventos = function (Condicion_CTL) {
        //Condicion
        //Registrar Condicion
        $(Condicion_CTL).on("click", "button#btn-RegistrarCondicion", function () {
            
            var form = $("#formCondicion");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var CondicionMensaje = $("#IdCondicion").val();
                var msj = ((CondicionMensaje == '' || CondicionMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((CondicionMensaje == '' || CondicionMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((CondicionMensaje == '' || CondicionMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoCondicion, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlCondicionIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Condicion_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdCondicion: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Condicion",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarCondicion, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Condicion", jsonResponse.mensaje, jsonResponse.type);
                        dataTableCondicion.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Condicion", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Condicion_CTL).on("change", "input#IdEstado", function () {
            
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdCondicion: $("#IdCondicion").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarCondicion, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Condicion", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });
        //Regresar al index
        $(Condicion_CTL).on("click", "button#btn-RegresarCondicion", function () {
            webApp.getDataVistaViewPartial(urlCondicionIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Condicion_CTL).on("click", "a#btnCondicionRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarCondicion, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Condicion_CTL).on("click", "a.btnConsultarCondicion", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdCondicion: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarCondicion, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Condicion_CTL).on("click", "a.btnEditarCondicion", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdCondicion: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarCondicion, param, function () {
                webApp.CampoLlenoInput();
            });
        });
      
      

    }
    return {
        init: function () {
            var _this = this;
            _this.Condicion_CTL = $("#Condicion");
            Eventos(_this.Condicion_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableCondicionView(urlCondicionPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio','FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableCondicion.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);