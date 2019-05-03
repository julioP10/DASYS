
var contacto = null;
var dataTableUsuario = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Usuario';
var tituloRegistro = 'Registrar  Usuario';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Usuario = function () {

    var dataTableUsuarioView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#UsuarioDataTable')) {
        dataTableUsuario = $("#UsuarioDataTable").dataTable({
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
                { "data": "idUsuario" },
                {
                    "data": function (obj) {
                        return '<a href="javascript:void(0)" class="btnEditarUsuario"  data-id="' + obj.idUsuario + '">' + obj.usuarios + '</a>';
                    }
                },
                { "data": "correo" },
                { "data": "perfil" },
                { "data": "estado" },
                {
                    "data": function (obj) {
                        //return "" Agregando los botone
                        return '<div><a href="javascript:void(0)" class="btnConsultarUsuario" data-id="' + obj.idUsuario + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarUsuario"  data-id="' + obj.idUsuario + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idUsuario + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a>\
                            <a href="javascript:void(0)" class="btnPermiso" data-perfil="'+ obj.idPerfil + '" data-id="' + obj.idUsuario + '" data-toggle="tooltip" title="Permisos"><i class="fa fa-user"></i></a></div > ';


                    }
                }
            ],
            "aoColumnDefs": [
                { "className": "dt-body-center", "aTargets": [0], "width": "5%" },
                { "className": "dt-body-center", "aTargets": [1], "width": "20%" },
                { "className": "dt-body-center", "aTargets": [2], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [4], "width": "10%" },
                { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [5], "width": "1%" }
            ],
            "order": [[0, "desc"]],
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
        var url = "/Comun/ListaEmpresaUsuario";
        var param = {
            codigo: id
        }
        webApp.JsonParam(url, param, function (data) {
            $('#IdEmpresa').empty();
            $('#IdEmpresa').append($('<option>', {
                value: '0',
                text: 'Seleccione Empresa'
            }))
            $.each(data.data, function (i, item) {
                $('#IdEmpresa').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }))
            });

            if ($('#_IdEmpresa').val() !== "") {
                $('#IdEmpresa').val($('#_IdEmpresa').val());
            }
        });
    }
    var Eventos = function (Usuario_CTL) {
        //Usuario
        //Registrar Usuario
        $(Usuario_CTL).on("click", "button#btn-RegistrarUsuario", function () {

            var _perfil = $("option:selected", $("#IdPerfil")).text();
            var _tipoEmpresa = $("option:selected", $("#_tipoEmpresa")).text();
            var _empresaPertenece = $("option:selected", $("#IdEmpresa")).text();
            $("#Perfil").val(_perfil);
            $("#TipoEmpresa").val(_tipoEmpresa);
            $("#EmpresaPertence").val(_empresaPertenece);
            var form = $("#formUsuario");
            if (webApp.validateForm(form)) {

                var query = $(form).serialize();
                var UsuarioMensaje = $("#IdUsuario").val();
                var msj = ((UsuarioMensaje == '' || UsuarioMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((UsuarioMensaje == '' || UsuarioMensaje == null) ? tituloRegistro : tituloActualizar);
                var typemsj = ((UsuarioMensaje == '' || UsuarioMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoUsuario, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlUsuarioIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });
        $("#espacio").on("click", "button#btnModificarUsuario", function () {

            var _perfil = $("option:selected", $("#IdPerfil")).text();
            var _tipoEmpresa = $("option:selected", $("#_tipoEmpresa")).text();
            var _empresaPertenece = $("option:selected", $("#IdEmpresa")).text();
            $("#Perfil").val(_perfil);
            $("#TipoEmpresa").val(_tipoEmpresa);
            $("#EmpresaPertence").val(_empresaPertenece);
            var form = $("#formUsuario");
            if (webApp.validateForm(form)) {

                var query = $(form).serialize();
                var UsuarioMensaje = $("#IdUsuario").val();
                var msj = ((UsuarioMensaje == '' || UsuarioMensaje == null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((UsuarioMensaje == '' || UsuarioMensaje == null) ? tituloRegistro : tituloActualizar);
                var typemsj = ((UsuarioMensaje == '' || UsuarioMensaje == null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoUsuario, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type == "success") {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }
        });

        $(Usuario_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdUsuario: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Usuario",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarUsuario, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Usuario", jsonResponse.mensaje, jsonResponse.type);
                        dataTableUsuario.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Usuario", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });

        //Regresar al index
        $(Usuario_CTL).on("click", "button#btn-RegresarUsuario", function () {
            webApp.getDataVistaViewPartial(urlUsuarioIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Usuario_CTL).on("change", "select#_tipoEmpresa", function () {

            var _value = $(this).val();
            if (_value != "0") {
                ListaEmpresa(_value);
            }
        });

        $(Usuario_CTL).on("click", "a.btnPermiso", function () {

            let urlpermiso = "/Seguridad/Usuario/Permiso";
            let _usuario = $(this).attr("data-id");
            let _perfil = $(this).attr("data-perfil");
            var param = {
                IdUsuario: _usuario,
                IdPerfil: _perfil
            }
            webApp.getDataVistaParam(urlpermiso, param, function (status) {
                $("#espacio #idval").val(_usuario);
                $("#espacio #idvalpr").val(_perfil);
            });
        });


        $(Usuario_CTL).on("click", "a#btnUsuarioRegistro", function () {

            webApp.getDataVistaViewPartial(urlRegistrarUsuario, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Usuario_CTL).on("click", "a.btnConsultarUsuario", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdUsuario: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarUsuario, param, function () {
                webApp.CampoLlenoInput();
                ListaEmpresa($("#_tipoEmpresa").val());
            });
        });
        $(Usuario_CTL).on("click", "a.btnEditarUsuario", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdUsuario: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarUsuario, param, function () {
                webApp.CampoLlenoInput();

                ListaEmpresa($("#_tipoEmpresa").val());
            });
        });
        $("#espacio").on("change", "input.chkPermiso", function () {
            var message = "";
            var confirm = "";
            var _check = 0;
            if ($(this).is(':checked')) {
                _check = 1;
            } else {
            }
            var query = {
                IdUsuario: $("#espacio #idval").val(),
                IdPerfil: $("#espacio #idvalpr").val(),
                IdOpcion: $(this).val(),
                Check: _check
            };

            var urlPermisoUsuario = "/Seguridad/Usuario/PermisoPerfil";
            webApp.JsonParam(urlPermisoUsuario, query, function (data) {
                webApp.sweetmensaje("Permiso", data.mensaje, data.type);
            });
        });
    }
    return {
        init: function () {
            var _this = this;
            _this.Usuario_CTL = $("#Usuario");
            Eventos(_this.Usuario_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion', 'NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableUsuarioView(urlUsuarioPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio', 'FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableUsuario.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);