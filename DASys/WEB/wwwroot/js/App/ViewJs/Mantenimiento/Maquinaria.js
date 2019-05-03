
var contacto = null;
var dataTableMaquinaria = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Maquinaria';
var tituloRegistro = 'Registrar  Maquinaria';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Maquinaria = function () {
    var iniciarFecha = function () {
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
    }
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
    var dataTableMaquinariaView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#MaquinariaDataTable')) {
        dataTableMaquinaria = $("#MaquinariaDataTable").dataTable({
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
                        return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idMaquinaria + '" value="' + obj.idMaquinaria + '" ><label class="custom-control-label" for="' + obj.idMaquinaria + '"></label></div >';
                    }
                },
                { "data": "idMaquinaria" },
                {
                    "data": function (obj) {
                        return '<a href="javascript:void(0)" class="btnEditarMaquinaria"  data-id="' + obj.idMaquinaria + '">' + obj.codigo + '</a>';
                    }
                },
                { "data": "descripcion" },
                { "data": "documento" },
                { "data": "inicioContrato" },
                { "data": "finContrato" },
                { "data": "empresa" },
                { "data": "tipo" },
                { "data": "marca" },
                { "data": "modelo" },
                { "data": "proveedor" },
                { "data": "categoria" },
                { "data": "observacion" },
                { "data": "estado" },
                {
                    "data": function (obj) {
                        //return "" Agregando los botone
                        return '<div><a href="javascript:void(0)" class="btnConsultarMaquinaria" data-id="' + obj.idMaquinaria + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarMaquinaria"  data-id="' + obj.idMaquinaria + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idMaquinaria + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                    }
                }
            ],
            "aoColumnDefs": [
                { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                { "className": "dt-body-center", "aTargets": [1], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [2], "width": "10%" },
                { "aTargets": [3], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [4], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [5], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [6], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [7], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [8], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [9], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [10], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [11], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [12], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [13], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [14], "width": "10%" },
                { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [15], "width": "1%" }
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

    var Eventos = function (Maquinaria_CTL) {
        //Maquinaria
        //Registrar Maquinaria
        $(Maquinaria_CTL).on("click", "button#btn-RegistrarMaquinaria", function () {

            var form = $("#formMaquinaria");
            if (webApp.validateForm(form)) {

                var query = $(form).serialize();
                var MaquinariaMensaje = $("#IdMaquinaria").val();
                var msj = ((MaquinariaMensaje === '' || MaquinariaMensaje === null) ? mensajeRegistro : mensajeActualizar);
                var titlemsj = ((MaquinariaMensaje === '' || MaquinariaMensaje === null) ? tituloRegistro : tituloActualizar);
                var typemsj = ((MaquinariaMensaje === '' || MaquinariaMensaje === null) ? typeRegistro : typeActualizar);
                var mensaje = {
                    title: titlemsj,
                    text: msj,
                    confirmButtonText: typemsj,
                    closeOnConfirm: false
                };
                webApp.sweetajax(mensaje, { url: urlMantenimientoMaquinaria, parametros: query }
                    , function (jsonResponse) {
                        if (jsonResponse.type === "success") {
                            webApp.sweetresponseOk(mensaje.title, jsonResponse, urlMaquinariaIndex);
                        } else {
                            webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                        }
                    }
                );
            }

        });
        $(Maquinaria_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdMaquinaria: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Maquinaria",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarMaquinaria, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type === "success") {
                        webApp.sweetmensaje("Maquinaria", jsonResponse.mensaje, jsonResponse.type);
                        dataTableMaquinaria.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Maquinaria", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Maquinaria_CTL).on("change", "input#IdEstado", function () {
            var _accion = $("#Accion").val();
            if (_accion !== undefined) {
                if ($(this).is(":checked") === false) {
                    var query = {
                        IdMaquinaria: $("#IdMaquinaria").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarMaquinaria, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje !== "ok") {
                            webApp.sweetmensaje("Maquinaria", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });

        //Regresar al index
        $(Maquinaria_CTL).on("click", "button#btn-RegresarMaquinaria", function () {
            webApp.getDataVistaViewPartial(urlMaquinariaIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Maquinaria_CTL).on("click", "a#btnMaquinariaRegistro", function () {

            webApp.getDataVistaViewPartial(urlRegistrarMaquinaria, function () {
                webApp.CampoLlenoInput();
                iniciarFecha();
                $("#InicioContrato").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                $("#FinContrato").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
            });
        });
        $(Maquinaria_CTL).on("click", "a.btnConsultarMaquinaria", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdMaquinaria: id
            };
            webApp.getDataVistaViewPartialParam(urlConsultarMaquinaria, param, function () {
                webApp.CampoLlenoInput();
                ListaModelo($("#espacio #IdMarca").val());
            });
        });
        $(Maquinaria_CTL).on("click", "a.btnEditarMaquinaria", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdMaquinaria: id
            };
            webApp.getDataVistaViewPartialParam(urlActualizarMaquinaria, param, function () {
                webApp.CampoLlenoInput();
                iniciarFecha();
                $("#InicioContrato").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                $("#FinContrato").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
                ListaModelo($("#espacio #IdMarca").val());
            });
        });

        $(Maquinaria_CTL).on("change", "#IdMarca", function () {
            var id = $(this).val();
            ListaModelo(id);
        });

        $("#espacio").on("change", "input#InicioContrato", function () {
            var valor = $(this).val();
            webApp.validarFechaDesdeHasta("FinContrato", valor);
            $("#FinContrato").prop("disabled", false);
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Maquinaria_CTL = $("#Maquinaria");
            Eventos(_this.Maquinaria_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion', 'Codigo', 'Observacion']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableMaquinariaView(urlMaquinariaPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('InicioContrato', 'FinContrato');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableMaquinaria.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);