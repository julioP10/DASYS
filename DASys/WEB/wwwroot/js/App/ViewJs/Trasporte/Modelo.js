
var contacto = null;
var dataTableModelo = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Modelo';
var tituloRegistro = 'Registrar  Modelo';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';


var Modelo = function () {
    var ListaMarca = function (id) {
        
        var url = "/Comun/ListaMarca";
        var param = {
            codigo: $("#__IdEmpresa").val(),
            empresa: "Trasporte"
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

    var dataTableModeloView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#ModeloDataTable')) {
            dataTableModelo = $("#ModeloDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idModelo + '" value="' + obj.idModelo + '" ><label class="custom-control-label" for="' + obj.idModelo + '"></label></div >';
                        }
                    },
                    { "data": "idModelo" },
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarModelo"  data-id="' + obj.idModelo + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "sdk" },
                    { "data": "marca" },
                    { "data": "configuracion" },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            var _htmlOpcion = "";
                            _htmlOpcion += '<div><a href="javascript:void(0)" class="btnConsultarModelo" data-id="' + obj.idModelo + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>';
                            if (__Perfil === "SUPERUSUARIO" || __Perfil === "Administrador") {
                               
                                _htmlOpcion += '<a href="javascript:void(0)" class="btnEditarModelo"  data-id="' + obj.idModelo + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>';
                                _htmlOpcion += '<a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idModelo + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a>';
                            } else {
                                $("#btnModeloRegistro").remove();
                            }
                            _htmlOpcion += "</div>";
                            return _htmlOpcion;
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
                    { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [7], "width": "1%" }
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

    var Eventos = function (Modelo_CTL) {
        //Modelo
        //Registrar Modelo
        $(Modelo_CTL).on("click", "button#btn-RegistrarModelo", function () {

            $("#IdEmpresa").val($("#__IdEmpresa").val()); 
            var form = $("#formModelo");
            if (webApp.validateForm(form)) {
                
                var query = $(form).serialize();
                var ModeloMensaje = $("#IdModelo").val();
                var msj = ((ModeloMensaje == '' || ModeloMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((ModeloMensaje == '' || ModeloMensaje == null) ? tituloRegistro: tituloActualizar);
                var typemsj = ((ModeloMensaje == '' || ModeloMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoModelo, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlModeloIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Modelo_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdModelo: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Modelo",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarModelo, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Modelo", jsonResponse.mensaje, jsonResponse.type);
                        dataTableModelo.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Modelo", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });

        $(Modelo_CTL).on("change", "input#IdEstado", function () {
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdModelo: $("#IdModelo").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarModelo, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Modelo", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });

        //Regresar al index
        $(Modelo_CTL).on("click", "button#btn-RegresarModelo", function () {
            webApp.getDataVistaViewPartial(urlModeloIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Modelo_CTL).on("click", "a#btnModeloRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarModelo, function () {
                webApp.CampoLlenoInput();
                ListaMarca("");
                ListaConfiguracion("");
            });
        });
        $(Modelo_CTL).on("click", "a.btnConsultarModelo", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdModelo: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarModelo, param, function () {
                webApp.CampoLlenoInput();
                if ($("#IdModelo").val() !== "") {
                    var empresa = $("#espacio #IdEmpresa").val();
                    $("select#__IdEmpresa").val(empresa);
                } 
            });
        });
        $(Modelo_CTL).on("click", "a.btnEditarModelo", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdModelo: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarModelo, param, function () {
                if ($("#espacio #IdModelo").val() !== "") {
                    var empresa = $("#espacio #IdEmpresa").val();
                    $("select#__IdEmpresa").val(empresa);
                } 
                webApp.CampoLlenoInput();
                ListaMarca("");
                ListaConfiguracion("");
            });
        });

        $("select#__IdEmpresa").on("change", function () {
            ListaMarca("");
            ListaConfiguracion("");
        });
        

    }
    return {
        init: function () {
            var _this = this;
            _this.Modelo_CTL = $("#Modelo");
            Eventos(_this.Modelo_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableModeloView(urlModeloPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio', 'FechaFin');
            if (!(__Perfil == "SUPERUSUARIO" || __Perfil == "Administrador")) {
                $("#btnMarcaRegistro").remove();
            }
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableModelo.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);