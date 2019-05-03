
var contacto = null;
var dataTableTerminal = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Terminal';
var tituloRegistro = 'Registrar  Terminal';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Terminal = function () {
    var ListaMarca = function () {
        var url = "/Comun/ListaMarca";
        var param = {
            codigo: $("#__IdEmpresa").val(),
            empresa: "EN0109"
        };
        webApp.JsonParam(url, param, function (data) {
            $('#IdMarca').empty();
            $('#IdMarca').append($('<option>', {
                value: '0',
                text: 'Seleccione Marca'
            }));
            $.each(data.data, function (i, item) {
                $('#IdMarca').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }));
            });
            if ($("#_IdMarca").val() !== undefined) {
                if ($("#_IdMarca").val() != "0")
                    $('#IdMarca').val($("#_IdMarca").val());
            }
        });
    };

    var ListaConfiguracion = function (id) {
        var url = "/Comun/ListaConfiguiracion";
        var param = {
            codigo: $("#__IdEmpresa").val(),
            empresa: ""
        };
        webApp.JsonParam(url, param, function (data) {
            $('#IdConfiguracion').empty();
            $('#IdConfiguracion').append($('<option>', {
                value: '0',
                text: 'Seleccione Configuracion'
            }));
            $.each(data.data, function (i, item) {
                $('#IdConfiguracion').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }));
            });
            if ($("#_IdConfiguracion").val() !== undefined) {
                if ($("#_IdConfiguracion").val() != "0")
                    $('#IdConfiguracion').val($("#_IdConfiguracion").val());
            }
        });
    };
    var ListaArea = function () {
        var url = "/Comun/ListaArea";
        var param = {
            codigo: "",
            empresa: $("#__IdEmpresa").val(),
        };
        webApp.JsonParam(url, param, function (data) {
            $('#IdArea').empty();
            $('#IdArea').append($('<option>', {
                value: '0',
                text: 'Seleccione Area'
            }));
            $.each(data.data, function (i, item) {
                $('#IdArea').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }));
            });
            if ($("#_IdArea").val() !== undefined) {
                if ($("#_IdArea").val() != "0")
                    $('#IdArea').val($("#_IdArea").val());
            }
        });
    };
    var ListaModelo = function (id) {
        var url = "/Comun/ListaModelo";
        var param = {
            codigo: id,
            empresa: $("#_IdPadre").val()
        };
        webApp.JsonParam(url, param, function (data) {
            $('#IdModelo').empty();
            $('#IdModelo').append($('<option>', {
                value: '0',
                text: 'Seleccione Modelo'
            }));
            $.each(data.data, function (i, item) {
                $('#IdModelo').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }));
            });
            if ($("#_IdModelo").val() !== undefined) {
                if ($("#_IdModelo").val() != "0")
                    $('#IdModelo').val($("#_IdModelo").val());
            }
        });
    };
    var dataTableTerminalView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#TerminalDataTable')) {
            dataTableTerminal = $("#TerminalDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idTerminal + '" value="' + obj.idModelo + '" ><label class="custom-control-label" for="' + obj.idTerminal + '"></label></div >';
                        }
                    },
                    { "data": "idTerminal" },
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarTerminal"  data-id="' + obj.idTerminal + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "ip" },
                    { "data": "puerto" },
                    { "data": "modelo" },
                    { "data": "area" },
                    { "data": "marca" },
                    { "data": "configuracion" },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            var _htmlOpcion = "";
                            _htmlOpcion += '<div><a href="javascript:void(0)" class="btnConsultarTerminal" data-id="' + obj.idTerminal + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>';
                            _htmlOpcion += '<a href="javascript:void(0)" class="btnEditarTerminal"  data-id="' + obj.idTerminal + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>';
                            _htmlOpcion += '<a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idTerminal + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a>';
                            _htmlOpcion += "</div>";
                            return _htmlOpcion;


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

    var Eventos = function (Terminal_CTL) {
        //Terminal
        //Registrar Terminal
        $(Terminal_CTL).on("click", "button#btn-RegistrarTerminal", function () {

            $("#IdEmpresa").val($("#__IdEmpresa").val()); 
            var form = $("#formTerminal");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var TerminalMensaje = $("#IdTerminal").val();
                var msj = ((TerminalMensaje == '' || TerminalMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((TerminalMensaje == '' || TerminalMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((TerminalMensaje == '' || TerminalMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoTerminal, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlTerminalIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Terminal_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdTerminal: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Terminal",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarTerminal, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Terminal", jsonResponse.mensaje, jsonResponse.type);
                        dataTableTerminal.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Terminal", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });


        $(Terminal_CTL).on("change", "input#IdEstado", function () {
  
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdTerminal: $("#IdTerminal").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarTerminal, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Terminal", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });
        //Regresar al index
        $(Terminal_CTL).on("click", "button#btn-RegresarTerminal", function () {
            webApp.getDataVistaViewPartial(urlTerminalIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Terminal_CTL).on("click", "a#btnTerminalRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarTerminal, function () {
                webApp.CampoLlenoInput();
                ListaMarca();
                ListaArea();
                ListaConfiguracion("");
            });
        });
        $(Terminal_CTL).on("click", "a.btnConsultarTerminal", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdTerminal: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarTerminal, param, function () {
                if ($("#espacio #IdTerminal").val() !== "") {
                    var empresa = $("#espacio #IdEmpresa").val();
                    $("select#__IdEmpresa").val(empresa);
                } 
                webApp.CampoLlenoInput();
                ListaMarca();
                ListaArea();
                ListaConfiguracion("");
                ListaModelo($("#espacio #_IdMarca").val());

            });
        });
        $(Terminal_CTL).on("click", "a.btnEditarTerminal", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdTerminal: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarTerminal, param, function () {
                webApp.CampoLlenoInput();
                if ($("#espacio #IdTerminal").val() !== "") {
                    var empresa = $("#espacio #IdEmpresa").val();
                    $("select#__IdEmpresa").val(empresa);
                } 
                ListaMarca();
                ListaArea();
                ListaConfiguracion("");
                ListaModelo($("#espacio #_IdMarca").val());
            });
        });

        $(Terminal_CTL).on("change", "#IdMarca", function () {
            var id = $(this).val();
            ListaModelo(id);
        });

        $("select#__IdEmpresa").on("change", function () {
            ListaMarca();
            ListaArea();
            ListaConfiguracion("");
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Terminal_CTL = $("#Terminal");
            Eventos(_this.Terminal_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Terminal DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableTerminalView(urlTerminalPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio', 'FechaFin');
            $('#IP').mask('0ZZ.0ZZ.0ZZ.0ZZ', {
                translation: {
                    'Z': {
                        pattern: /[0-9]/, optional: true
                    }
                }
            });
            $('#IP').mask('099.099.099.099');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableTerminal.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);