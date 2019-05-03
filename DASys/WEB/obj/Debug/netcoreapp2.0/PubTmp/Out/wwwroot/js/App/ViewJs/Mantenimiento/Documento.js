
var contacto = null;
var dataTableDocumento = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Documento';
var tituloRegistro = 'Registrar  Documento';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var Documento = function () {
    var listarCategoriaXDocumento = function (id) {
        var url = "/Comun/ListaCategoria";
        var empresa = $("#IdEmpresa").val();
        var param = {
            codigo: id,
            empresa: empresa
        }
        webApp.JsonParam(url, param, function (data) {
            var html = '';
            if (data.data.length>0) {
                $.each(data.data, function (i, item) {
                    html += '<div class="col-md-4 col-md-offset-2">';
                    html += ' <div class="anil_nepal">';
                    html += '    <label class="switch switch-left-right">';
                    if (item.valor1 === "1") {
                        html += '<input checked data-id="' + item.value + '" data-check="si" value"' + item.value + '" class="switch-input" type="checkbox">';
                    } else {
                        html += '<input data-id="' + item.value + '" data-check="si" value"' + item.value + '" class="switch-input" type="checkbox">';
                    }
                    html += '         <span class="switch-label" data-on="SI" data-off="NO"></span> <span class="switch-handle"></span>';
                    html += '    </label> ' + item.nombre + '';
                    html += '  </div>';
                    html += '</div>';
                });
                $('#checkCategorias').html(html);
            } else {
                var _html = '<div class="alert alert-danger alert-dismissible fade show"> <button type="button" aria-hidden="true" class="close" data-dismiss="alert" aria-label="Close"> <i class="nc-icon nc-simple-remove"></i></button> <span><b>No hay - </b> categorias para esta entidad </span></div>';
                $('#checkCategorias').html(_html);
            }
        });
    }
    var dataTableDocumentoView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#DocumentoDataTable')) {
            dataTableDocumento = $("#DocumentoDataTable").dataTable({
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idDocumento + '" value="' + obj.idDocumento + '" ><label class="custom-control-label" for="' + obj.idDocumento + '"></label></div >';
                        }
                    },
                    { "data": "idDocumento" },
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarDocumento"  data-id="' + obj.idDocumento + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "descripcion" },
                    {
                        "data": function (obj) {
                            var html = '';
                            if (obj.obligatorio) {
                                return  html += '<span class="badge badge-pill badge-primary"> SI</span>';
                            } else {
                                return html += '<span class="badge badge-pill badge-warning">NO </span>';
                            }
                        }
                    },
                    {
                        "data": function (obj) {
                            var html = '';
                            if (obj.fechaVencimiento) {
                                return html += '<span class="badge badge-pill badge-primary"> SI</span>';
                            } else {
                                return html += '<span class="badge badge-pill badge-warning"> NO </span>';
                            }
                        }
                    },
                    {
                        "data": function (obj) {
                            var html = '';
                            if (obj.categoria !== '') {
                                var arr = obj.categoria.split(',');
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
                            return '<div><a href="javascript:void(0)" class="btnConsultarDocumento" data-id="' + obj.idDocumento + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarDocumento"  data-id="' + obj.idDocumento + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idDocumento + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bVisible": false,"bSortable": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                    { "className": "dt-body-center", "aTargets": [1], "width": "5%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [4], "width": "10%" },
                    { "className": "dt-body-center", "aTargets": [5], "width": "10%" },
                    { "aTargets": [6], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [7], "width": "10%" },
                    { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [8], "width": "1%" }
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

    var Eventos = function (Documento_CTL) {
        //Documento
        //Registrar Documento
        $(Documento_CTL).on("click", "button#btn-RegistrarDocumento", function () {
            
           
            var selected = "";
            $("input[type=checkbox]:checked").each(function () {
                if ($(this).attr("data-check") === "si")
                    selected += ($(this).attr("data-id")) + ",";
            });

            if (selected.length > 0) {
                //var ids = selected.slice(0, -1)
                $("#IdCategoria").val(selected);
                var form = $("#formDocumento");
                if (webApp.validateForm(form)) {

                    var query = $(form).serialize();
                    var DocumentoMensaje = $("#IdDocumento").val();
                    var msj = ((DocumentoMensaje == '' || DocumentoMensaje == null) ? mensajeRegistro : mensajeActualizar);
                    var titlemsj = ((DocumentoMensaje == '' || DocumentoMensaje == null) ? tituloRegistro : tituloActualizar);
                    var typemsj = ((DocumentoMensaje == '' || DocumentoMensaje == null) ? typeRegistro : typeActualizar);
                    var mensaje = {
                        title: titlemsj,
                        text: msj,
                        confirmButtonText: typemsj,
                        closeOnConfirm: false
                    };
                    webApp.sweetajax(mensaje, { url: urlMantenimientoDocumento, parametros: query }
                        , function (jsonResponse) {
                            if (jsonResponse.type == "success") {
                                webApp.sweetresponseOk(mensaje.title, jsonResponse, urlDocumentoIndex);
                            } else {
                                webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                            }
                        }
                    );
                }
            }
            else {
                webApp.sweetmensaje("Documentos", "Debe seleccionar al menos una entidad", "warning");
            }
            
        });
        $(Documento_CTL).on("click", "a.btnEliminar", function () {
            var query = {
                IdDocumento: $(this).attr("data-id")
            };
            var mensaje = {
                title: "Documento",
                text: "¿Estas seguro de Eliminar?",
                confirmButtonText: "Eliminar",
                closeOnConfirm: false
            };
            webApp.sweetajax(mensaje, { url: urlEliminarDocumento, parametros: query }
                , function (jsonResponse) {
                    if (jsonResponse.type == "success") {
                        webApp.sweetmensaje("Documento", jsonResponse.mensaje, jsonResponse.type);
                        dataTableDocumento.fnReloadAjax();
                    } else {
                        webApp.sweetmensaje("Documento", jsonResponse.mensaje, jsonResponse.type);
                    }

                }
            );
        });
        $(Documento_CTL).on("change", "input#IdEstado", function () {
            var _accion = $("#Accion").val();
            if (_accion != undefined) {
                if ($(this).is(":checked") == false) {
                    var query = {
                        IdDocumento: $("#IdDocumento").val(),
                        Accion: _accion
                    };
                    webApp.JsonParam(urlEliminarDocumento, query, function (jsonResponseDto) {
                        if (jsonResponseDto.mensaje != "ok") {
                            webApp.sweetmensaje("Documento", jsonResponseDto.mensaje, "warning");
                            $("#IdEstado").prop("checked", true);
                        }
                    });
                }

            }
        });
        //Regresar al index
        $(Documento_CTL).on("click", "button#btn-RegresarDocumento", function () {
            webApp.getDataVistaViewPartial(urlDocumentoIndex, function () {
                webApp.CampoLlenoInput();
            });
        });

        $(Documento_CTL).on("click", "a#btnDocumentoRegistro", function () {
             
            webApp.getDataVistaViewPartial(urlRegistrarDocumento, function () {
                webApp.CampoLlenoInput();
            });
        });
        $(Documento_CTL).on("click", "a.btnConsultarDocumento", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdDocumento: id
            }
            webApp.getDataVistaViewPartialParam(urlConsultarDocumento, param, function () {
                listarCategoriaXDocumento(id);
                webApp.CampoLlenoInput();
            });
        });
        $(Documento_CTL).on("click", "a.btnEditarDocumento", function () {
            var id = $(this).attr("data-id");
            var param = {
                IdDocumento: id
            }
            webApp.getDataVistaViewPartialParam(urlActualizarDocumento, param, function () {
         
                listarCategoriaXDocumento(id);
                webApp.CampoLlenoInput();
            });
        });
        $(Documento_CTL).on("change", "#IdEntidad", function () {
            var id = $(this).val();
            listarCategoriaXDocumento(id);
        });
        
    }
    return {
        init: function () {
            var _this = this;
            _this.Documento_CTL = $("#Documento");
            Eventos(_this.Documento_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableDocumentoView(urlDocumentoPaginado);
            });
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio','FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableDocumento.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);