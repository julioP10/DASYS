
var contacto = null;
var dataTableModulo = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Modulo';
var tituloRegistro = 'Registrar  Modulo';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Modulo = function () {

    var dataTableModuloView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#ModuloDataTable')) {
            dataTableModulo = $("#ModuloDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idModulo + '" value="' + obj.idModulo + '" ><label class="custom-control-label" for="' + obj.idModulo + '"></label></div >';
                        }
                    },
                    { "data": "idModulo" },
                
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarModulo"  data-id="' + obj.idModulo + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "descripcion" },
                    { "data": "posicion" },
                    { "data": "icono" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            return '<div><a href="javascript:void(0)" class="btnConsultarModulo" data-id="' + obj.idModulo + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarModulo"  data-id="' + obj.idModulo + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idModulo + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


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

    var Eventos = function (Modulo_CTL) {
        //Modulo
        //Registrar Modulo
        $(Modulo_CTL).on("click", "button#btn-RegistrarModulo", function () {
            
            var form = $("#formModulo");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var ModuloMensaje = $("#IdModulo").val();
                var msj = ((ModuloMensaje == '' || ModuloMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((ModuloMensaje == '' || ModuloMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((ModuloMensaje == '' || ModuloMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoModulo, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlModuloIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Modulo_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdModulo: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Modulo",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarModulo, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Modulo", jsonResponse.mensaje, jsonResponse.type);
                        dataTableModulo.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Modulo", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });

        //Regresar al index
        $(Modulo_CTL).on("click", "button#btn-RegresarModulo", function () {
            webApp.getDataVistaViewPartial(urlModuloIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Modulo_CTL).on("click", "a#btnModuloRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarModulo, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Modulo_CTL).on("click", "a.btnConsultarModulo", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdModulo: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarModulo, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Modulo_CTL).on("click", "a.btnEditarModulo", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdModulo: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarModulo, param, function () {
                webApp.CampoLlenoInput();
            });
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Modulo_CTL = $("#Modulo");
            Eventos(_this.Modulo_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableModuloView(urlModuloPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio','FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableModulo.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);