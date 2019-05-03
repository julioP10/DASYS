
var contacto = null;
var dataTableEmpresa = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Empresa';
var tituloRegistro = 'Registrar  Empresa';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';
var correcto = 0;
var Empresa = function () {

    var ListaCategoria = function (id) {
        var url = "/Comun/ListaCategoria";
        var param = {
            codigo: id,
            empresa: $("option:selected", $("#IdPadre")).attr("padre")
        };
        webApp.JsonParam(url, param, function (data) {
            $('#IdCategoria').empty();
            $('#IdCategoria').append($('<option>', {
                value: '0',
                text: 'Seleccione Categoria'
            }));
            $.each(data.data, function (i, item) {
                $('#IdCategoria').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }));
            });
            if ($("#_IdCategoria").val() !== undefined) {
                if ($("#_IdCategoria").val() != "0")
                    $('#IdCategoria').val($("#_IdCategoria").val());
            }
        });
    };
    var dataTableEmpresaView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#EmpresaDataTable')) {
        dataTableEmpresa = $("#EmpresaDataTable").dataTable({
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
                        NombreSearch: $("#NombreSearch").val(),
                        EsPrincipalSearch: $("#EsPrincipalSearch").val(),
                        EsContratistaSearch: $("#EsContratistaSearch").val(),
                        EsSubContratistaSearch: $("#EsSubContratistaSearch").val()
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
                        return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idEmpresa + '" value="' + obj.idEmpresa + '" ><label class="custom-control-label" for="' + obj.idEmpresa + '"></label></div >';
                    }
                },
                { "data": "idEmpresa" },
                {
                    "data": function (obj) {
                        return '<a href="javascript:void(0)" class="btnEditarEmpresa"  data-id="' + obj.idEmpresa + '">' + obj.razonSocial + '</a>';
                    }
                },
                { "data": "ruc" },
                { "data": "direccionFiscal" },
                { "data": "tipoEmpresa" },
                { "data": "padreSubcontratista" },
                { "data": "categoria" },
                { "data": "estado" },
                {
                    "data": function (obj) {
                        var _html = '';
                        if (obj.esPrincipal === 1) {
                            _html = '<a href="javascript:void(0)" class="btnConfiguracion"  data-id="' + obj.idEmpresa + '" data-toggle="tooltip" title="Configrar parametros de correo"><i class="fa fa-cog"></i></a>'
                        }
                        //return "" Agregando los botone
                        return '<div><a href="javascript:void(0)" class="btnConsultarEmpresa" data-id="' + obj.idEmpresa + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarEmpresa"  data-id="' + obj.idEmpresa + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                               <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idEmpresa + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a>\
                               '+ _html + '</div>';


                    }
                }
            ],
            "aoColumnDefs": [
                { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                { "className": "dt-body-center", "aTargets": [1], "width": "1%" },
                { "className": "dt-body-center", "aTargets": [2], "width": "20%" },
                { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [4], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [5], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [6], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [7], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [8], "width": "10%" },
                { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [9], "width": "1%" }
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
    var ListaEmpresa = function (id) {
        var url = "/Comun/ListaEmpresa";
        var param = {
            codigo: id
        }
        webApp.JsonParam(url, param, function (data) {
            $('#_empresaPrincipal').empty();
            $('#_empresaPrincipal').append($('<option>', {
                value: '0',
                text: 'Seleccione Empresa'
            }))
            $.each(data.data, function (i, item) {
                $('#_empresaPrincipal').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }))
            });

            if ($('#_IdPadre').val() !== "") {
                $('#_empresaPrincipal').val($('#_IdPadre').val());
            }
        });
    }
    var Eventos = function (Empresa_CTL) {
        //Empresa
        //Registrar Empresa
        $(Empresa_CTL).on("click", "button#btn-RegistrarEmpresa", function () {
            var form = $("#formEmpresa");
            if (correcto > 0) {
                if (webApp.validateForm(form)) {

                    var query = $(form).serialize();
                    var EmpresaMensaje = $("#IdEmpresa").val();
                    var msj = ((EmpresaMensaje === '' || EmpresaMensaje == null) ? mensajeRegistro : mensajeActualizar);
                    var titlemsj = ((EmpresaMensaje === '' || EmpresaMensaje == null) ? tituloRegistro : tituloActualizar);
                    var typemsj = ((EmpresaMensaje === '' || EmpresaMensaje == null) ? typeRegistro : typeActualizar);
                    var mensaje = {
                        title: titlemsj,
                        text: msj,
                        confirmButtonText: typemsj,
                        closeOnConfirm: false
                    };
                    webApp.sweetajax(mensaje, { url: urlMantenimientoEmpresa, parametros: query }
                        , function (jsonResponse) {
                            if (jsonResponse.type == "success") {
                                webApp.sweetresponseOk(mensaje.title, jsonResponse, urlEmpresaIndex);
                            } else {
                                webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                            }
                        }
                    );
                }
            } else {
                var _result = "RUC: " + $("#RUC").val() + "\nNo existe.";
                swal("Empresa", _result, "warning");
            }
        });
        $(Empresa_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdEmpresa: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Empresa",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarEmpresa, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Empresa", jsonResponse.mensaje, jsonResponse.type);
                        dataTableEmpresa.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Empresa", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Empresa_CTL).on("change", "input#IdEstado", function () {
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdEmpresa: $("#IdEmpresa").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarEmpresa, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Empresa", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });

        //Regresar al index
        $(Empresa_CTL).on("click", "button#btn-RegresarEmpresa", function () {
            webApp.getDataVistaViewPartial(urlEmpresaIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Empresa_CTL).on("click", "a#btnEmpresaRegistro", function () {
            var param = {
            }
            webApp.getDataVistaViewPartialParam(urlRegistrarEmpresa, param, function () {
                window.urlEmpresaIndex = "/Mantenimiento/Empresa/Index";
                webApp.CampoLlenoInput();
            });
        });
        $(Empresa_CTL).on("click", "a.btnConsultarEmpresa", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdEmpresa: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarEmpresa, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Empresa_CTL).on("click", "a.btnEditarEmpresa", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdEmpresa: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarEmpresa, param, function () {
                webApp.CampoLlenoInput();
                correcto = true;
            });
        });
        $(Empresa_CTL).on("click", "a.btnConfiguracion", function () {
            let url = "/Mantenimiento/Empresa/RegistrarParametros";
            let id = $(this).attr("data-id");
            var param = {
                IdEmpresa: id
            }
            webApp.getDataVistaParam(url, param, function (status) {
                $("#espacio #IdEmpresa").val(id);
                var _id = $("#espacio #IdParametros").val();
                if (_id !== "") {
                    $("#espacio #Host").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                    $("#espacio #Port").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                    $("#espacio #Correo").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                    $("#espacio #Password").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');

                }
            });
        });

        $("#espacio").on("click", "button#btn-Guardar-parametro", function () {
            var url = "/Mantenimiento/Empresa/MantenimientoParametros";
            var form = $("#espacio #frmParametrosCorreo");
            if (webApp.validateForm(form)) {
                var query = $(form).serialize();
                var mensaje = {
                    title: "Parametro para envio de correo",
                    text: "¿Estas seguro de Registrar?",
                    confirmButtonText: "Registrar",
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: url, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetmensaje("Parametro para envio de correo", jsonResponse.mensaje, jsonResponse.type);
                            $('#myModal').modal("hide");
                        } else {
                            webApp.sweetmensaje("Parametro para envio de correo", jsonResponse.mensaje, jsonResponse.type);
                        }

                    }
                );
            }

        });
        $(Empresa_CTL).on("change", "select#TipoEmpresa", function () {
            var _value = $(this).val();
            var _perfilLogeado = $("#_PerfilEmpresa").val();
            $("#IdPadre").prop("disabled", false);
            if (_value === 'PRINCIPAL') {
                $("#IdPadre").prop("required", false);
                $("#IdCategoria").prop("required", false);
                $("#IdPadre").prop("disabled", true);
                $("select[name = '__IdPadre']").prop("disabled", true);
                $("#IdPadre").parents().eq(0).removeClass("has-danger");
                $("#espacio #_empresaPrincipal").siblings("label").nextUntil().remove();
                $("#EsPrincipal").val(1);
                $("#EsContratista").val(0);
                $("#EsSubContratista").val(0);

            }
            else if (_value === "CONTRATISTA") {

                if (_perfilLogeado != "SUPERUSUARIO") {
                    $("#IdPadre").val(0);
                    $("#IdPadre").prop("required", false);
                    $("#IdCategoria").prop("required", true);
                    $("#IdPadre").prop("disabled", true);
                    $("select[name = '__IdPadre']").prop("disabled", true);
                    $("select[name = '__IdPadre']").val($("#_IdPadre").val());
                    $("#IdPadre").parents().eq(0).removeClass("has-default");
                    $("#EsContratista").val(1);
                    $("#EsSubContratista").val(0);
                    $("#EsPrincipal").val(0);


                }
                else {
                    $("#IdPadre").prop("required", true); 
                    $("#IdCategoria").prop("required", true);
                    $("#IdPadre").prop("disabled", false);
                    $("select[name = '__IdPadre']").prop("disabled", false);
                    $("select[name = '__IdPadre']").val(0);
                    $("#IdPadre").parents().eq(0).removeClass("has-default");
                    $("#EsContratista").val(1);
                    $("#EsSubContratista").val(0);
                    $("#EsPrincipal").val(0);
                    var Values = $(this).val();
                    var _empresaSelceccionada = null;
                    if ($("#IdEmpresa").val() !== "") {
                        _empresaSelceccionada = $("#IdEmpresa").val();
                    }
                    var param = {
                        codigo: Values,
                        empresaSelceccionada: _empresaSelceccionada
                    };
                    webApp.JsonParam("/Comun/ListaEmpresa", param, function (data) {
                        $("#IdPadre").empty();
                        $('#IdPadre').append($('<option>', {
                            value: "0",
                            text: "Seleccione Empresa"
                        }));
                        $.each(data.data, function (i, e) {
                            $('#IdPadre').append($('<option>', {
                                value: e.value,
                                text: e.nombre,
                                padre: e.valor1
                            }));
                        });
                        $("#IdPadre").val(0);
                    });
                }
            }
            else {
                $("#IdPadre").prop("required", true);
                $("#IdCategoria").prop("required", true);
                $("#IdPadre").prop("disabled", false);
                $("select[name = '__IdPadre']").prop("disabled", false);
                $("select[name = '__IdPadre']").val(0);
                $("select[name = '__IdPadre']").prop("required", true);
                $("#IdPadre").parents().eq(0).removeClass("has-default");
                $("#EsSubContratista").val(1);
                $("#EsContratista").val(0);
                $("#EsPrincipal").val(0);
                if (_perfilLogeado === "SUPERUSUARIO" || _perfilLogeado === "ADMINISTRADOR") {
                    var Values = $(this).val();
                    var _empresaSelceccionada = null;
                    if ($("#IdEmpresa").val()!=="") {
                        _empresaSelceccionada = $("#IdEmpresa").val();
                    }
                    var param = {
                        codigo: Values,
                        empresaSelceccionada: _empresaSelceccionada
                    };
                    webApp.JsonParam("/Comun/ListaEmpresa", param, function (data) {
                        $("#IdPadre").empty();
                        $('#IdPadre').append($('<option>', {
                            value: "0",
                            text: "Seleccione Empresa"
                        }));
                        $.each(data.data, function (i, e) {
                            $('#IdPadre').append($('<option>', {
                                value: e.value,
                                text: e.nombre,
                                padre:e.valor1
                            }));
                        });
                        $("#IdPadre").val(0);
                    });
                }
            }

        });

        $(Empresa_CTL).on("change", "select#IdPadre", function () {
            ListaCategoria("EMPRESA");
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Empresa_CTL = $("#Empresa");
            Eventos(_this.Empresa_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion', 'NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Empresa DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableEmpresaView(urlEmpresaPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio', 'FechaFin');
            $('#datetimepicker').datetimepicker();
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableEmpresa.fnReloadAjax();
                });
            }
        },
        ruc: function (e) {
            if ($("#IdEmpresa").val()=="") {
                var _value = $(e).val();
                if (_value.length >= 11) {
                    $.post("https://cors-anywhere.herokuapp.com/wmtechnology.org/Consultar-RUC/?modo=1&btnBuscar=Buscar&nruc=" + _value, function (respuesta) {
                        var doc = document.implementation.createHTMLDocument()
                            .documentElement,
                            res = "",
                            txt, campos,
                            ok = false;

                        doc.innerHTML = respuesta;
                        campos = doc.querySelectorAll(".list-group-item");
                        if (campos.length) {
                            for (txt of campos)
                                res += txt.innerText + "\n";
                            res = res.replace(/^\s+\n*|(:) *\n| +$/gm, "$1");
                            ok = /^Estado: *ACTIVO *$/m.test(res);
                            $("#content-empresa").css("display", "");
                            $("#resultado").html(campos);
                            $("#espacio .list-group-item-heading").css("margin-top", "0px");
                            $("#espacio .list-group-item-heading").css("margin-bottom", "7px");
                            $("#espacio .list-group-item-heading").css("font-size", "1em");

                            var arreglo = res.split(":");
                            if (arreglo.length > 0) {
                                var arregloRuc = arreglo[1].split("-");
                                var nuevaCadena = arregloRuc[1].replace("Estado", "");
                                $("#RazonSocial").val(nuevaCadena)
                                $("#DireccionFiscal").val(arreglo[8])
                                $("#espacio #RazonSocial").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                                $("#espacio #DireccionFiscal").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                            }
                            correcto = 1;
                        } else {
                            res = "RUC: " + _value + "\nNo existe.";
                            swal("Empresa", res, "warning");
                            $("#content-empresa").css("display", "none");
                            $("#espacio #RazonSocial").val("")
                            $("#espacio #DireccionFiscal").val("")
                            correcto = 0;
                        }
                    });
                }
            }

        }

    };
}(jQuery);