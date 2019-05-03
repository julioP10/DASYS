
var contacto = null;
var dataTableCategoria = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Categoria';
var tituloRegistro = 'Registrar  Categoria';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Categoria = function () {

    var dataTableCategoriaView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#CategoriaDataTable')) {
            dataTableCategoria = $("#CategoriaDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idCategoria + '" value="' + obj.idCategoria + '" ><label class="custom-control-label" for="' + obj.idCategoria + '"></label></div >';
                        }
                    },
                    { "data": "idCategoria" },
                   
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarCategoria"  data-id="' + obj.idCategoria + '">' + obj.nombre + '</a>';
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
                            return '<div><a href="javascript:void(0)" class="btnConsultarCategoria" data-id="' + obj.idCategoria + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarCategoria"  data-id="' + obj.idCategoria + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idCategoria + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                    { "className": "dt-body-center", "aTargets": [1], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "10%" },
                    { "aTargets": [3], "width": "30%" },
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

    var Eventos = function (Categoria_CTL) {
        //Categoria
        //Registrar Categoria
        $(Categoria_CTL).on("click", "button#btn-RegistrarCategoria", function () {
            
           
            var selected = "";
            $("input[type=checkbox]:checked").each(function () {
                if ($(this).attr("data-cat") == "cat")
                    selected += ($(this).val()) + ",";
            });

            if (selected.length > 0) {
                //var ids = selected.slice(0, -1)
                $("#IdEntidad").val(selected);
                var form = $("#formCategoria");
                if (webApp.validateForm(form)) {

                    var query = $(form).serialize();
                    var CategoriaMensaje = $("#IdCategoria").val();
                    var msj = ((CategoriaMensaje == '' || CategoriaMensaje == null) ? mensajeRegistro : mensajeActualizar);
                    var titlemsj = ((CategoriaMensaje == '' || CategoriaMensaje == null) ? tituloRegistro : tituloActualizar);
                    var typemsj = ((CategoriaMensaje == '' || CategoriaMensaje == null) ? typeRegistro : typeActualizar);
                    var mensaje = {
                        title: titlemsj,
                        text: msj,
                        confirmButtonText: typemsj,
                        closeOnConfirm: false
                    };
                    webApp.sweetajax(mensaje, { url: urlMantenimientoCategoria, parametros: query }
                        , function (jsonResponse) {
                            if (jsonResponse.type == "success") {
                                webApp.sweetresponseOk(mensaje.title, jsonResponse, urlCategoriaIndex);
                            } else {
                                webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                            }
                        }
                    );
                }
            }
            else {
                webApp.sweetmensaje("Categorias", "Debe seleccionar al menos una entidad", "warning");
            }
            
        });
        $(Categoria_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdCategoria: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Categoria",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarCategoria, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Categoria", jsonResponse.mensaje, jsonResponse.type);
                        dataTableCategoria.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Categoria", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Categoria_CTL).on("change", "input#IdEstado", function () {
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdCategoria: $("#IdCategoria").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarCategoria, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Categoria", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });

        //Regresar al index
        $(Categoria_CTL).on("click", "button#btn-RegresarCategoria", function () {
            webApp.getDataVistaViewPartial(urlCategoriaIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Categoria_CTL).on("click", "a#btnCategoriaRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarCategoria, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Categoria_CTL).on("click", "a.btnConsultarCategoria", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdCategoria: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarCategoria, param, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Categoria_CTL).on("click", "a.btnEditarCategoria", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdCategoria: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarCategoria, param, function () {
                webApp.CampoLlenoInput();
            });
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Categoria_CTL = $("#Categoria");
            Eventos(_this.Categoria_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableCategoriaView(urlCategoriaPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio','FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableCategoria.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);