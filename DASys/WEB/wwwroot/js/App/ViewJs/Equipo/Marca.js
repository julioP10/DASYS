
var contacto = null;
var dataTableMarca = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Marca';
var tituloRegistro = 'Registrar  Marca';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Marca = function () {

    var dataTableMarcaView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#MarcaDataTable')) {
        dataTableMarca = $("#MarcaDataTable").dataTable({
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
                        return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idMarca + '" value="' + obj.idMarca + '" ><label class="custom-control-label" for="' + obj.idMarca + '"></label></div >';
                    }
                },
                { "data": "idMarca" },
                {
                    "data": function (obj) {
                        return '<a href="javascript:void(0)" class="btnEditarMarca"  data-id="' + obj.idMarca + '">' + obj.nombre + '</a>';
                    }
                },
                {
                    "data": function (obj) {
                        var html = '';
                        if (obj.entidad !== '') {
                            var arr = obj.entidad.split(',');
                            for (var i = 0; i < arr.length; i++) {
                                html += '<span class="badge badge-pill badge-success"> <i class="nc-icon nc-tag-content"></i> ' + arr[i] + '</span>';
                            }
                        }
                        return html;
                    }
                },
                { "data": "estado" },
                {
                    "data": function (obj) {
                        //return "" Agregando los botone
                        var _htmlOpcion = "";
                        _htmlOpcion += '<div><a href="javascript:void(0)" class="btnConsultarMarca" data-id="' + obj.idMarca + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>';
                        if (__Perfil === "SUPERUSUARIO") {
                           
                            _htmlOpcion += '<a href="javascript:void(0)" class="btnEditarMarca"  data-id="' + obj.idMarca + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>';
                            _htmlOpcion += '<a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idMarca + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a>';
                        } else {
                            $("#btnMarcaRegistro").remove();
                        }
                        _htmlOpcion += "</div>";
                        return _htmlOpcion;

                    }
                }
            ],
            "aoColumnDefs": [
                { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                { "className": "dt-body-center", "aTargets": [1], "width": "1%" },
                { "className": "dt-body-center", "aTargets": [2], "width": "20%" },
                { "className": "dt-body-center", "aTargets": [3], "width": "40%" },
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

    var Eventos = function (Marca_CTL) {
        //Marca
        //Registrar Marca
        $(Marca_CTL).on("click", "button#btn-RegistrarMarca", function () {
            var selected = "";
            $("input[type=checkbox]:checked").each(function () {
                if ($(this).attr("data-cat") == "cat")
                    selected += ($(this).val()) + ",";
            });

            if (selected.length > 0) {
                //var ids = selected.slice(0, -1)
                $("#IdEntidad").val(selected);
                $("#IdEmpresa").val($("#__IdEmpresa").val()); 
                var form = $("#formMarca");
                if (webApp.validateForm(form)) {

                    var query = $(form).serialize();
                    var MarcaMensaje = $("#IdMarca").val();
                    var msj = ((MarcaMensaje == '' || MarcaMensaje == null) ? mensajeRegistro : mensajeActualizar);
                    var titlemsj = ((MarcaMensaje == '' || MarcaMensaje == null) ? tituloRegistro : tituloActualizar);
                    var typemsj = ((MarcaMensaje == '' || MarcaMensaje == null) ? typeRegistro : typeActualizar);
                    var mensaje = {
                        title: titlemsj,
                        text: msj,
                        confirmButtonText: typemsj,
                        closeOnConfirm: false
                    };
                    webApp.sweetajax(mensaje, { url: urlMantenimientoMarca, parametros: query }
                        , function (jsonResponse) {
                            if (jsonResponse.type == "success") {
                                webApp.sweetresponseOk(mensaje.title, jsonResponse, urlMarcaIndex);
                            } else {
                                webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                            }
                        }
                    );
                }
            } else {
                webApp.sweetmensaje("Registro Marca", "Debe seleccionar al menos una entidad", "warning");
            }
        });
        $(Marca_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdMarca: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Marca",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarMarca, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Marca", jsonResponse.mensaje, jsonResponse.type);
                        dataTableMarca.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Marca", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Marca_CTL).on("change", "input#IdEstado", function () {

            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdMarca: $("#IdMarca").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarMarca, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Marca", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });
        //Regresar al index
        $(Marca_CTL).on("click", "button#btn-RegresarMarca", function () {
            webApp.getDataVistaViewPartial(urlMarcaIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Marca_CTL).on("click", "a#btnMarcaRegistro", function () {

            webApp.getDataVistaViewPartial(urlRegistrarMarca, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Marca_CTL).on("click", "a.btnConsultarMarca", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdMarca: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarMarca, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Marca_CTL).on("click", "a.btnEditarMarca", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdMarca: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarMarca, param, function () {
                webApp.CampoLlenoInput();
            });
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Marca_CTL = $("#Marca");
            Eventos(_this.Marca_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion', 'NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableMarcaView(urlMarcaPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio', 'FechaFin');
            if (__Perfil !== "SUPERUSUARIO") {
                $("#btnMarcaRegistro").remove();
            }
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableMarca.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);