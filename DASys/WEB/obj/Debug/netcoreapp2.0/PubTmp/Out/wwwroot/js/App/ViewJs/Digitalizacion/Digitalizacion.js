var dataTableColaborador = null;
var dataTableEmpresa = null;
var contacto = null;
var dataTableDigitalizacion = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar Digitalizacion';
var tituloRegistro = 'Registrar  Digitalizacion';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';
var urlColaborador = "/Mantenimiento/Colaborador/Listar";
var urlEmpresa = "/Mantenimiento/Empresa/Listar";
var Digitalizacion = function () {

    var dataTableDigitalizacionView = function (url) {

        //if (!$.fn.DataTable.isDataTable('#DigitalizacionDataTable')) {
            dataTableDigitalizacion = $("#DigitalizacionDataTable").dataTable({
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
                            DigitalizacionSearch: _estadoDigitalizacion
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
                            return '<div class="custom-control custom-checkbox"> <input type="checkbox" check-masiva class="custom-control-input  custom-control-item" id="' + obj.idDigitalizacion + '" value="' + obj.idDigitalizacion + '" ><label class="custom-control-label" for="' + obj.idDigitalizacion + '"></label></div >';
                        }
                    },
                    { "data": "idDigitalizacion" },
                    {
                        "data": function (obj) {
                            return '<a href="javascript:void(0)" class="btnEditarDigitalizacion"  data-id="' + obj.idDigitalizacion + '">' + obj.nombre + '</a>';
                        }
                    },
                    { "data": "estado" },
                    {
                        "data": function (obj) {
                            //return "" Agregando los botone
                            return '<div><a href="javascript:void(0)" class="btnConsultarDigitalizacion" data-id="' + obj.idDigitalizacion + '" data-toggle="tooltip" title="Consultar"><i class="fa fa-search"></i></a>\
                                <a href="javascript:void(0)" class="btnEditarDigitalizacion"  data-id="' + obj.idDigitalizacion + '" data-toggle="tooltip" title="Editar"><i class="fa fa-edit"></i></a>\
                            <a href="javascript:void(0)" class="btnEliminar"  data-id="' + obj.idDigitalizacion + '" data-toggle="tooltip" title="Eliminar"><i class="fa fa-trash"></i></a></div>';


                        }
                    }
                ],
                "aoColumnDefs": [
                    { "bSortable": false, "className": "datatable-td-check", "aTargets": [0], "width": "1%" },
                    { "bSortable": false, "aTargets": [1], "width": "1%" },
                    { "className": "dt-body-center", "aTargets": [2], "width": "30%" },
                    { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                    { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [4], "width": "1%" }
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
    var dataTableColaboradorView = function (urlColaborador) {
        //if (!$.fn.DataTable.isDataTable('#ColaboradorDataTable')) {
        dataTableColaborador = $("#ColaboradorDataTable").dataTable({
            "bFilter": false,
            "bProcessing": true,
            "serverSide": true,
            //"scrollY": "400px",
            //"sScrollX": "100%",
            "ajax": {
                "url": urlColaborador,
                "type": "POST",
                "data": function (request) {
                    //
                    
                    request.filter = {
                        NombreSearch: $("#NombreSearch").val(),
                        DigitalizacionSearch: _estadoDigitalizacion
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
                { "data": "idColaborador" },
                {
                    "data": function (obj) {
                        return '<a href="javascript:void(0)" class="btnDigitalizarColaborador"  data-digitalizacion="' + obj.digitalizacion + '" data-id="' + obj.idColaborador + '">' + obj.nombre + '</a>';
                    }
                },
                { "data": "empresa" },
                { "data": "sexo" },
                { "data": "ubicacion" },
                { "data": "entidad" },
                { "data": "departamento" },
                {
                    "data": function (obj) {
                        //return "" Agregando los botone
                        return '<div><a href="javascript:void(0)" class="btnDigitalizarColaborador" data-digitalizacion="' + obj.digitalizacion + '" data-id="' + obj.idColaborador + '" data-toggle="tooltip" title="Digitalizar"><i class="fa fa-file-pdf-o"></i></a>';
                    }
                }
            ],
            "aoColumnDefs": [
                { "bVisible": true,"className": "dt-body-center", "aTargets": [0], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [1], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [2], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [4], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [5], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [6], "width": "10%" },
                { "bSortable": false, "className": "datatable-td-opciones", "aTargets": [7], "width": "1%" }
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
    var dataTableEmpresaView = function (urlEmpresa) {
        //if (!$.fn.DataTable.isDataTable('#EmpresaDataTable')) {
        dataTableEmpresa = $("#EmpresaDataTable").dataTable({
            "bFilter": false,
            "bProcessing": true,
            "serverSide": true,
            //"scrollY": "400px",
            //"sScrollX": "100%",
            "ajax": {
                "url": urlEmpresa,
                "type": "POST",
                "data": function (request) {
                    //
                    request.filter = {
                        NombreSearch: $("#NombreSearch").val(),
                        DigitalizacionSearch: _estadoDigitalizacion
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
                { "data": "estado" },
                {
                    "data": function (obj) {
                        var _html = '';
                        if (obj.esPrincipal === 1) {
                            _html = '<a href="javascript:void(0)" class="btnConfiguracion"  data-id="' + obj.idEmpresa + '" data-toggle="tooltip" title="Configrar parametros de correo"><i class="fa fa-cog"></i></a>'
                        }
                        //return "" Agregando los botone
                        return '<div><a href="javascript:void(0)" class="btnDigitalizarEmpresa" data-digitalizacion="' + obj.digitalizacion + '" data-id="' + obj.idEmpresa + '" data-toggle="tooltip" title="Digitalizar"><i class="fa fa-file-pdf-o"></i></a>';



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
    var Eventos = function (Digitalizacion_CTL) {
        //Digitalizacion
        //ir a colaborador
        $(Digitalizacion_CTL).on("click", "a.btnDigitalizarColaborador", function () {
            
            var _digitalizacion = $(this).attr("data-digitalizacion");
            var _colaboradorid = $(this).attr("data-id");
            window._digitalizacionOpcion = _digitalizacion;
            window._EstadoDigitalizacion = _digitalizacion;
            window._colaborador = _colaboradorid;
            webApp.getDataVistaViewPartial(urlDigitalizacionColaborador, function () {
                $("#espacio #_EstadoDigitalizacion").val(_digitalizacion);
            });
        });

        //ir a empresa
        $(Digitalizacion_CTL).on("click", "a.btnDigitalizarEmpresa", function () {
            
            var _digitalizacion = $(this).attr("data-digitalizacion");
            var _empresaid = $(this).attr("data-id");
            window._digitalizacionOpcion = _digitalizacion;
            window._empresa = _empresaid;
            webApp.getDataVistaViewPartial(urlDigitalizacionEmpresa, function () {
                $("#espacio #_EstadoDigitalizacion").val(_digitalizacion);
            });
        });

    }
    return {
        init: function () {
            var _this = this;
            _this.Digitalizacion_CTL = $("#Digitalizacion");
            Eventos(_this.Digitalizacion_CTL);
            //Validar
            webApp.validarAlfanumericoGuion(['Nombre', 'Descripcion','NombreUbigeo']);
            webApp.validarLetrasEspacio(['Lugar', 'Descripcion']);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableDigitalizacionView(urlDigitalizacionPaginado);
            });

            if (entidad === "Colaborador") {
                dataTableColaboradorView(urlColaborador);
            }
            if (entidad === "Empresa") {
                dataTableEmpresaView(urlEmpresa);
            }
            webApp.ValidarFormatoFechaDDMMYYYY('FechaInicio','FechaFin');
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableDigitalizacion.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);