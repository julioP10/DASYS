
var contacto = null;
var dataTableColaborador = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Colaborador';
var tituloRegistro = 'Registrar  Colaborador';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Colaborador = function () {
    var ListaProvincia = function (id) {
        var url = "/Comun/ListaProvincia";
        var param = {
            codigo: id
        }
        webApp.JsonParam(url, param, function (data) {
            $('#IdProvincia').empty();
            $('#IdProvincia').append($('<option>', {
                value: '0',
                text: 'Seleccione Distrito'
            }))
            $.each(data.data, function (i, item) {
                $('#IdProvincia').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }))
            });

            if ($('#_IdProvincia').val() !== "") {
                $('#IdProvincia').val($('#_IdProvincia').val());
                ListaDistrito($("#IdProvincia").val());
            } else {
                $('#IdProvincia').val(0);
            }
        });
    }
    var ListaDistrito = function (id) {
        var url = "/Comun/ListaDistrito";
        var param = {
            codigo: id,
            codigo2: $("#IdUDepartamento").val()
        }
        webApp.JsonParam(url, param, function (data) {
            $('#IdDistrito').empty();
            $('#IdDistrito').append($('<option>', {
                value: '0',
                text: 'Seleccione Distrito'
            }))
            $.each(data.data, function (i, item) {
                $('#IdDistrito').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }))
            });

            if ($('#_IdDistrito').val() !== "") {
                $('#IdDistrito').val($('#_IdDistrito').val());
            } else {
                $('#IdDistrito').val(0);
            }
        });
    }
    var ListaUbicacion = function (id) {
        var url = "/Comun/ListaUbicacion";
        var param = {
            codigo: id
        }
        webApp.JsonParam(url, param, function (data) {
            $('#IdUbicacion').empty();
            $('#IdUbicacion').append($('<option>', {
                value: '0',
                text: 'Seleccione Ubicacion'
            }))
            $.each(data.data, function (i, item) {
                $('#IdUbicacion').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }))
            });

            if ($('#_IdUbicacion').val() !== "") {
                $('#IdUbicacion').val($('#_IdUbicacion').val());
            } else {
                $('#IdUbicacion').val(0);
            }
        });
    }
    var dataTableColaboradorView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#ColaboradorDataTable')) {
        dataTableColaborador = $("#ColaboradorDataTable").dataTable({
            responsive: true,
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
                        return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idColaborador + '" value="' + obj.idColaborador + '" ><label class="custom-control-label" for="' + obj.idColaborador + '"></label></div >';
                    }
                },
                { "data": "idColaborador" },
                {
                    "data": function (obj) {
                        return '<a href="javascript:void(0)" class="btnEditarColaborador"  data-id="' + obj.idColaborador + '">' + obj.nombre + '</a>';
                    }
                },
                { "data": "sexo" },
                { "data": "empresa" },
                { "data": "padreSubcontratista" },
                { "data": "departamento" },
                { "data": "ubicacion" },
                { "data": "estado" },
                {
                    "data": function (obj) {
                        //return "" Agregando los botone
                        return '<div><a href="javascript:void(0)" class="btnConsultarColaborador" data-id="' + obj.idColaborador + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarColaborador"  data-id="' + obj.idColaborador + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idColaborador + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                    }
                }
            ],
            "aoColumnDefs": [
                { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                { "className": "dt-body-center", "aTargets": [1], "width": "10%" },
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
    var ListarTelefono = function (id) {
        var param = {
            IdPersona: id
        }
        var url = "/Mantenimiento/Colaborador/ListarTelefono";
        webApp.JsonParam(url, param, function (data) {
            var _html = '';
            $.each(data.data, function (i, item) {
                _html += '<tr>';
                _html += '<td>' + item.nombre + '</td>';
                _html += '<td>' + item.tipo + '</td>';
                _html += '<td><span class="badge badge-success badge-pill  btnEditarTelefono" data-nombre="' + item.nombre + '"  data-idTipo="' + item.idTipo + '" data-id="' + item.idPersona + '" ><i class="fa fa-edit"></i></span> <span class="badge badge-danger badge-pill  btnEliminarTelefono" data-id="' + item.idPersona + '"><i class="fa fa-trash"></i></span></td>';
                _html += '</tr>';
            });
            $("#_tableTelefono").html(_html);
        });
    }
    var ListarCorreo = function (id) {
        var param = {
            IdPersona: id
        }
        var url = "/Mantenimiento/Colaborador/ListarCorreo";
        webApp.JsonParam(url, param, function (data) {
            var _html = '';
            $.each(data.data, function (i, item) {
                _html += '<tr>';
                _html += '<td>' + item.nombre + '</td>';
                _html += '<td>' + item.tipo + '</td>';
                _html += '<td><span class="badge badge-success badge-pill  btnEditarCorreo" data-nombre="' + item.nombre + '" data-idTipo="' + item.idTipo + '" data-id="' + item.idPersona + '"  ><i class="fa fa-edit"></i></span> <span class="badge badge-danger badge-pill  btnEliminarTelefono" data-id="' + item.idPersona + '><i class="fa fa-trash"></i></span> <span class="badge badge-danger badge-pill  btnEliminarCorreo" data-id="' + item.idPersona + '"><i class="fa fa-trash"></i></span></td>';
                _html += '</tr>';
            });
            $("#_tableCorreo").html(_html);
        });
    }
    var ListarTarjeta = function (id) {
        var param = {
            IdPersona: id
        }
        var url = "/Mantenimiento/Colaborador/ListarTarjeta";
        webApp.JsonParam(url, param, function (data) {
            var _html = '';
            $.each(data.data, function (i, item) {
                _html += '<tr>';
                _html += '<td>' + item.nombre + '</td>';
                _html += '<td>' + item.tipo + '</td>';
                _html += '<td><span class="badge badge-success badge-pill  btnEditarTarjeta" data-nombre="' + item.nombre + '" data-idTipo="' + item.idTipo + '" data-id="' + item.idPersona + '" ><i class="fa fa-edit"></i></span> <span class="badge badge-danger badge-pill  btnEliminarCorreo" data-id="' + item.idPersona + '><i class="fa fa-edit"></i></span> <span class="badge badge-danger badge-pill  btnEliminarTelefono" data-id="' + item.idPersona + '"><i class="fa fa-trash"></i></span></td>';
                _html += '</tr>';
            });
            $("#_tableTarjeta").html(_html);
        });
    }
    var Validate = function () {
        
        var image = document.getElementById("file").value;
        if (image != '') {
            var checkimg = image.toLowerCase();
            if (!checkimg.match(/(\.jpg|\.png|\.JPG|\.PNG|\.jpeg|\.JPEG|\.mp4|\.MP4|\.flv|\.FLV|\.mkv|\.MKV)$/)) { // validation of file extension using regular expression before file upload
                document.getElementById("image").focus();
                document.getElementById("errorName5").innerHTML = "Wrong file selected";
                return false;
            }
            var img = document.getElementById("file");
            alert(img.files[0].size);
            if (img.files[0].size < 1048576)  // validation according to file size
            {
                document.getElementById("errorName5").innerHTML = "Image size too short";
                return false;
            }
            return true;
        }
    }
    var Eventos = function (Colaborador_CTL) {
        //Colaborador
        //Registrar Colaborador
        $(Colaborador_CTL).on("click", "button#btn-RegistrarColaborador", function () {

            var selected = "";
            $("input[type=checkbox]:checked").each(function () {
                if ($(this).attr("area") === "area")
                    selected += ($(this).val()) + ",";
            });
            
            $("#IdArea").val(selected);
            var _file = $('#espacio input:file[name=file]').val();

            var form = $("#formColaborador");
            if (webApp.validateForm(form)) {

                var query = $(form).serialize();
                var ColaboradorMensaje = $("#IdColaborador").val();
                var msj = ((ColaboradorMensaje === '' || ColaboradorMensaje === null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((ColaboradorMensaje === '' || ColaboradorMensaje === null) ? tituloRegistro : tituloActualizar);
                var typemsj = ((ColaboradorMensaje === '' || ColaboradorMensaje === null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.subirArchivos(mensaje, { url: urlMantenimientoColaborador }, form
                    , function (jsonResponse) {
                        if (jsonResponse.type === "success") {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                            var resul = jsonResponse.data.split("|");
                            $("#IdColaborador").val(resul[0]);
                            $("#IdPersona").val(resul[1]);
                            $("#_sig").prop("disabled", false);
                            ListarCorreo(jsonResponse.data);
                            ListarTelefono(jsonResponse.data);
                            ListarTarjeta(jsonResponse.data);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $(Colaborador_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdColaborador: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Colaborador",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarColaborador, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Colaborador", jsonResponse.mensaje, jsonResponse.type);
                        dataTableColaborador.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Colaborador", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });

        $(Colaborador_CTL).on("change", "input#IdEstado", function () {
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdColaborador: $("#IdColaborador").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarColaborador, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Colaborador", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });
        //Regresar al index
        $(Colaborador_CTL).on("click", "button#btn-RegresarColaborador", function () {
            webApp.getDataVistaViewPartial(urlColaboradorIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Colaborador_CTL).on("click", "a#btnColaboradorRegistro", function () {

            webApp.getDataVistaViewPartial(urlRegistrarColaborador, function () {
                webApp.CampoLlenoInput();
                $("#FechaNacimiento").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
            });
        });
        $(Colaborador_CTL).on("click", "a.btnConsultarColaborador", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdColaborador: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarColaborador, param, function () {
                webApp.CampoLlenoInput();
                $("#FechaVencimiento").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                var _valor = $("#espacio #IdTipoLugar").val();
                if (_valor == "TU0002") {
                    $("#espacio #_Descripcion").css("display", "");
                }
            });
        });
        $(Colaborador_CTL).on("click", "a.btnEditarColaborador", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdColaborador: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarColaborador, param, function () {
                // "myAwesomeDropzone" is the camelized version of the HTML element's ID
                $("#FechaVencimiento").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                var _valor = $("#espacio #IdTipoLugar").val();
                if (_valor == "TU0002") {
                    $("#espacio #_Descripcion").css("display", "");
                }
                webApp.CampoLlenoInput();
            });
        });
        //Correo9
        $(Colaborador_CTL).on("click", "button#btnRegistrarCorreo", function () {

            var valid = webApp.ValidadorPorElemento("TipoCorreo", "_correo");
            if (valid == false) {
                return;
            }
            var query = {
                IdPersona: $("#IdPersona").val(),
                IdTipo: $("#TipoCorreo").val(),
                nombre: $("#_correo").val(),
            };
            var mensaje = {
                title: "Colaborador",
                text: "¿Estas seguro de registrar?",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            };
            var _url = "/Mantenimiento/Colaborador/MantenimientoCorreo";
            webApp.sweetajax(mensaje, { url: _url, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Correo Colaborador", jsonResponse.mensaje, jsonResponse.type);
                        ListarCorreo($("#IdPersona").val());
                    } else {
                        webApp.sweetmensaje("Correo Colaborador", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        //Correo9
        $(Colaborador_CTL).on("click", "button#btnActualizarCorreo", function () {
            var valid = webApp.ValidadorPorElemento("TipoCorreo", "_correo");
            if (valid == false) {
                return;
            }
            var _tipo = $("#TipoCorreo").val();
            if (_tipo == "0") {
                $("#TipoCorreo").find("div").eq(0).addClass("has-danger");
                return;
            }
            var query = {
                IdPersona: $("#IdPersona").val(),
                IdTipo: $("#TipoCorreo").val(),
                nombre: $("#_correo").val(),
            };
            var mensaje = {
                title: "Colaborador",
                text: "¿Estas seguro de actualizar?",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            };
            var _url = "/Mantenimiento/Colaborador/MantenimientoCorreo";
            webApp.sweetajax(mensaje, { url: _url, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Correo Colaborador", jsonResponse.mensaje, jsonResponse.type);
                        ListarCorreo($("#IdPersona").val());
                        $("#btnActualizarCorreo").prop("disabled", true);
                        $("#btnRegistrarCorreo").prop("disabled", false);
                    } else {
                        webApp.sweetmensaje("Correo Colaborador", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        //Correo
        $(Colaborador_CTL).on("click", "span.btnEliminarCorreo", function () {
            var query = {
                IdPersona: $("#IdPersona").val(),
                IdTipo: $("#TipoCorreo").val(),
                nombre: $("#_correo").val(),
            };
            var mensaje = {
                title: "Colaborador",
                text: "¿Estas seguro de registrar?",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            };
            var _url = "/Mantenimiento/Colaborador/EliminarCorreo";
            webApp.sweetajax(mensaje, { url: _url, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Correo Colaborador", jsonResponse.mensaje, jsonResponse.type);
                        ListarCorreo($("#IdPersona").val());
                    } else {
                        webApp.sweetmensaje("Correo Colaborador", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Colaborador_CTL).on("click", "span.btnEditarCorreo", function () {

            $("#espacio #_correo").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
            $("#TipoCorreo").val($(this).attr("data-idTipo"));
            $("#_correo").val($(this).attr("data-nombre"));
            $("#btnActualizarCorreo").prop("disabled", false);
            $("#btnRegistrarCorreo").prop("disabled", true);

        });

        //TELEFONO
        $(Colaborador_CTL).on("click", "button#btnRegistrarTelefono", function () {
            var valid = webApp.ValidadorPorElemento("TipoTelefono", "_telefono");
            if (valid == false) {
                return;
            }

            var query = {
                IdPersona: $("#IdPersona").val(),
                IdTipo: $("#TipoTelefono").val(),
                nombre: $("#_telefono").val(),
            };
            var mensaje = {
                title: "Colaborador",
                text: "¿Estas seguro de registrar?",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            };
            var _url = "/Mantenimiento/Colaborador/MantenimientoTelefono";
            webApp.sweetajax(mensaje, { url: _url, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Telefono Colaborador", jsonResponse.mensaje, jsonResponse.type);
                        ListarTelefono($("#IdPersona").val());
                        $("#btnActualizarTelefono").prop("disabled", true);
                        $("#btnRegistrarTelefono").prop("disabled", false);
                    } else {
                        webApp.sweetmensaje("Telefono Colaborador", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Colaborador_CTL).on("click", "button#btnActualizarTelefono", function () {
            var valid = webApp.ValidadorPorElemento("TipoTelefono", "_telefono");
            if (valid == false) {
                return;
            }

            var query = {
                IdPersona: $("#IdPersona").val(),
                IdTipo: $("#TipoTelefono").val(),
                nombre: $("#_telefono").val(),
            };
            var mensaje = {
                title: "Colaborador",
                text: "¿Estas seguro de actuzalizar?",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            };
            var _url = "/Mantenimiento/Colaborador/MantenimientoTelefono";
            webApp.sweetajax(mensaje, { url: _url, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Telefono Colaborador", jsonResponse.mensaje, jsonResponse.type);
                        ListarTelefono($("#IdPersona").val());
                        $("#btnActualizarTelefono").prop("disabled", true);
                        $("#btnRegistrarTelefono").prop("disabled", false);
                    } else {
                        webApp.sweetmensaje("Telefono Colaborador", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Colaborador_CTL).on("click", "span.btnEliminarTelefono", function () {
            var query = {
                IdPersona: $("#IdPersona").val(),
                IdTipo: $("#TipoTelefono").val(),
                nombre: $("#_telefono").val(),
            };
            var mensaje = {
                title: "Colaborador",
                text: "¿Estas seguro de registrar?",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            };
            var _url = "/Mantenimiento/Colaborador/EliminarTelefono";
            webApp.sweetajax(mensaje, { url: _url, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Telefono Colaborador", jsonResponse.mensaje, jsonResponse.type);
                        ListarTelefono($("#IdPersona").val());
                    } else {
                        webApp.sweetmensaje("Telefono Colaborador", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Colaborador_CTL).on("click", "span.btnEditarTelefono", function () {

            $("#espacio #_telefono").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
            $("#TipoTelefono").val($(this).attr("data-idTipo"));
            $("#_telefono").val($(this).attr("data-nombre"));
            $("#btnActualizarTelefono").prop("disabled", false);
            $("#btnRegistrarTelefono").prop("disabled", true);

        });

        //tarjeta
        $(Colaborador_CTL).on("click", "button#btnRegistrarTarjeta", function () {
            var valid = webApp.ValidadorPorElemento("TipoTarjeta", "_tarjeta");
            if (valid == false) {
                return;
            }
            var query = {
                IdPersona: $("#IdPersona").val(),
                IdTipo: $("#TipoTarjeta").val(),
                nombre: $("#_tarjeta").val(),
            };
            var mensaje = {
                title: "Colaborador",
                text: "¿Estas seguro de registrar?",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            };
            var _url = "/Mantenimiento/Colaborador/MantenimientoTarjeta";
            webApp.sweetajax(mensaje, { url: _url, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Tarjeta Colaborador", jsonResponse.mensaje, jsonResponse.type);
                        ListarTarjeta($("#IdPersona").val());
                    } else {
                        webApp.sweetmensaje("Tarjeta Colaborador", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Colaborador_CTL).on("click", "button#btnActualizarTarjeta", function () {
            var valid = webApp.ValidadorPorElemento("TipoTarjeta", "_tarjeta");
            if (valid == false) {
                return;
            }
            var query = {
                IdPersona: $("#IdPersona").val(),
                IdTipo: $("#TipoTarjeta").val(),
                nombre: $("#_tarjeta").val(),
            };
            var mensaje = {
                title: "Colaborador",
                text: "¿Estas seguro de actualizar?",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            };
            var _url = "/Mantenimiento/Colaborador/MantenimientoTarjeta";
            webApp.sweetajax(mensaje, { url: _url, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Tarjeta Colaborador", jsonResponse.mensaje, jsonResponse.type);
                        ListarTarjeta($("#IdPersona").val());
                        $("#btnActualizarTarjeta").prop("disabled", true);
                        $("#btnRegistrarTarjeta").prop("disabled", false);
                    } else {
                        webApp.sweetmensaje("Tarjeta Colaborador", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Colaborador_CTL).on("click", "span.btnEliminarTarjeta", function () {
            var query = {
                IdPersona: $("#IdPersona").val(),
                IdTipo: $("#TipoTarjeta").val(),
                nombre: $("#_tarjeta").val(),
            };
            var mensaje = {
                title: "Colaborador",
                text: "¿Estas seguro de registrar?",
                confirmButtonText: "Aceptar",
                closeOnConfirm: false
            };
            var _url = "/Mantenimiento/Colaborador/EliminarTarjeta";
            webApp.sweetajax(mensaje, { url: _url, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Tarjeta Colaborador", jsonResponse.mensaje, jsonResponse.type);
                        ListarTarjeta($("#IdPersona").val());
                    } else {
                        webApp.sweetmensaje("Tarjeta Colaborador", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Colaborador_CTL).on("click", "span.btnEditarTarjeta", function () {
            $("#espacio #_tarjeta").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
            $("#TipoTarjeta").val($(this).attr("data-id"));
            $("#_tarjeta").val($(this).attr("data-nombre"));
            $("#btnActualizarTarjeta").prop("disabled", false);
            $("#btnRegistrarTarjeta").prop("disabled", true);

        });

        //combo anidado de ubigeo
        $(Colaborador_CTL).on("change", "select#IdUDepartamento", function () {
            var id = $(this).val();
            ListaProvincia(id);

        });
        $(Colaborador_CTL).on("change", "select#IdProvincia", function () {
            var id = $(this).val();
            ListaDistrito(id);
        });
        $(Colaborador_CTL).on("change", "select#IdDepartamento", function () {
            var id = $(this).val();
            ListaUbicacion(id);
        });

        $(Colaborador_CTL).on("change", "select#IdTipoLugar", function () {

            var _val = $("#IdColaborador").val();
            if (_val == "") {
                $("#Descripcion").val("");
            }
            var _valor = $('option:selected', this).attr("data-valor");
            if (_valor === 'SI') {
                $("#_Descripcion").css("display", "");
                $("#Descripcion").prop("required", true);
            } else {
                $("#_Descripcion").css("display", "none");
                $("#Descripcion").prop("required", false);
            }
        });
    }
    return {
        init: function () {
            var _this = this;
            _this.Colaborador_CTL = $("#Colaborador");
            Eventos(_this.Colaborador_CTL);
            //Validar
            webApp.validarNumericoTelefono(['_telefono']);
            webApp.validarCorreos(['_correo']);
            webApp.validarNumerico(['_tarjeta']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableColaboradorView(urlColaboradorPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio', 'FechaFin');
            $('.datepicker').datetimepicker({
                defaultDate: 'now',
                format: 'DD/MM/YYYY',
                locale: 'es',
                icons: {
                    time: "fa fa-clock-o",
                    date: "fa fa-calendar",
                    up: "fa fa-chevron-up",
                    down: "fa fa-chevron-down",
                    previous: 'fa fa-chevron-left',
                    next: 'fa fa-chevron-right',
                    today: 'fa fa-screenshot',
                    clear: 'fa fa-trash',
                    close: 'fa fa-remove'
                }
            });
            if ($("#IdPersona").val() !== '') {
                if ($("#IdColaborador").val() !== "") {
                    ListaProvincia($("#IdUDepartamento").val());
                    ListaUbicacion($("#IdDepartamento").val());
                    ListarCorreo($("#IdPersona").val());
                    ListarTelefono($("#IdPersona").val());
                    ListarTarjeta($("#IdPersona").val());
                }
            }
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableColaborador.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);

