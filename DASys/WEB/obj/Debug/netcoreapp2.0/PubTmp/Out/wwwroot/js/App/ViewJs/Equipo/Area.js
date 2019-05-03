
var contacto = null;
var dataTableArea = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Area';
var tituloRegistro = 'Registrar  Area';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Area = function () {

    var dataTableAreaView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#AreaDataTable')) {
            dataTableArea = $("#AreaDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idArea + '" value="' + obj.idArea + '" ><label class="custom-control-label" for="' + obj.idArea + '"></label></div >';
                        }
                    },
                    { "data": "idArea" },
                  
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarArea"  data-id="' + obj.idArea + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            return '<div><a href="javascript:void(0)" class="btnConsultarArea" data-id="' + obj.idArea + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarArea"  data-id="' + obj.idArea + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idArea + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                    { "className": "dt-body-center", "aTargets": [1], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                    { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [4], "width": "1%" }
                ],
                "order": [1, "desc"],
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

    var Eventos = function (Area_CTL) {
        //Area
        //Registrar Area
        $(Area_CTL).on("click", "button#btn-RegistrarArea", function () {
            
            var form = $("#formArea");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var AreaMensaje = $("#IdArea").val();
                var msj = ((AreaMensaje == '' || AreaMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((AreaMensaje == '' || AreaMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((AreaMensaje == '' || AreaMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoArea, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlAreaIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Area_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdArea: $(this).attr("data-id"),
                Opcion:0
            };
            var mensaje = {
                title: "Area",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarArea, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Area", jsonResponse.mensaje, jsonResponse.type);
                        dataTableArea.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Area", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Area_CTL).on("change", "input#IdEstado", function () {
            
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdArea: $("#IdArea").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarArea, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Area", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }
                  
            }
        });
        //Regresar al index
        $(Area_CTL).on("click", "button#btn-RegresarArea", function () {
            webApp.getDataVistaViewPartial(urlAreaIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Area_CTL).on("click", "a#btnAreaRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarArea, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Area_CTL).on("click", "a.btnConsultarArea", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdArea: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarArea, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Area_CTL).on("click", "a.btnEditarArea", function () {
            var id = $(this).attr("data-id");
            var _accion = $("#Accion").val();
            var param = {
                IdArea: id,
                Accion: _accion
            }
            webApp.getDataVistaViewPartialParam(urlActualizarArea, param, function () {
                webApp.CampoLlenoInput();
            });
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Area_CTL = $("#Area");
            Eventos(_this.Area_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableAreaView(urlAreaPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio','FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableArea.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);