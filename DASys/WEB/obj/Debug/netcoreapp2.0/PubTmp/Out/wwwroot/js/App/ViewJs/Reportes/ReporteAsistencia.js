
var contacto = null;
var dataTableReporteAsistencia = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar ReporteAsistencia';
var tituloRegistro = 'Registrar  ReporteAsistencia';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var ReporteAsistencia = function () {
    var oldExportAction = function (self, e, dt, button, config) {
        if (button[0].className.indexOf('buttons-excel') >= 0) {
            if ($.fn.dataTable.ext.buttons.excelHtml5.available(dt, config)) {
                $.fn.dataTable.ext.buttons.excelHtml5.action.call(self, e, dt, button, config);
            }
            else {
                $.fn.dataTable.ext.buttons.excelFlash.action.call(self, e, dt, button, config);
            }
        } else if (button[0].className.indexOf('buttons-print') >= 0) {
            $.fn.dataTable.ext.buttons.print.action(e, dt, button, config);
        }
    };

    var newExportAction = function (e, dt, button, config) {
        var self = this;
        var oldStart = dt.settings()[0]._iDisplayStart;

        dt.one('preXhr', function (e, s, data) {
            // Just this once, load all data from the server...
            data.start = 0;
            data.length = 2147483647;

            dt.one('preDraw', function (e, settings) {
                // Call the original action function 
                oldExportAction(self, e, dt, button, config);

                dt.one('preXhr', function (e, s, data) {
                    // DataTables thinks the first item displayed is index 0, but we're not drawing that.
                    // Set the property to what it was before exporting.
                    settings._iDisplayStart = oldStart;
                    data.start = oldStart;
                });

                // Reload the grid with the original page. Otherwise, API functions like table.cell(this) don't work properly.
                setTimeout(dt.ajax.reload, 0);

                // Prevent rendering of the full data to the DOM
                return false;
            });
        });

        // Requery the server with the new one-time export settings
        dt.ajax.reload();
    };
    var dataTableReporteAsistenciaView = function (url) {
 
        //if (!$.fn.DataTable.isDataTable('#ReporteAsistenciaDataTable')) {
        dataTableReporteAsistencia = $("#ReporteAsistenciaDataTable").dataTable({
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
                        EntidadSearch: $("#EntidadSearch").val(),
                        TipoEmpresaSearch: $("#TipoEmpresaSearch").val(),
                        IdCategoriaSearch: $("#IdCategoriaSearch").val(),
                        IdDepartamentoSearch: $("#IdDepartamentoSearch").val(),
                        IdUbicacionSearch: $("#IdUbicacionSearch").val(),
                        IdTerminalSearch: $("#IdTerminalSearch").val(),
                        FechaInicioSearch: $("#FechaInicioSearch").val(),
                        FechaFinSearch: $("#FechaFinSearch").val(),
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
            "pageLength": 20,
            "bAutoWidth": false,
            "columns": [
                { "data": "marcaciones" },
                { "data": "id" },
                { "data": "nombre" },
                { "data": "nombre" },
                { "data": "genero" },
                { "data": "empresa" },
                { "data": "departamento" },
                { "data": "ubicacion" },
                { "data": "condicion" },
                { "data": "terminal" },
                { "data": "fecha" },
                { "data": "entrada" },
                { "data": "salida" },
                { "data": "acumulado" }
            ],
            "aoColumnDefs": [
                { "className": "dt-body-center", "aTargets": [0], "width": "5%" },
                { "className": "dt-body-center", "aTargets": [1], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [2], "width": "10%" },
                { "className": "dt-body-center", "aTargets": [3], "width": "10%" },
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

            ],
            "stripeClasses": [],
            dom: "<'row'<'col-sm-6'l><'col-sm-4 'f><'col-sm-2  text-right pull-right'B>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            buttons: [

                {
                    extend: 'excel',
                    text: '<div class="b-actions">' +
                        '<div class= "row" >' +
                        '<a class= "col b-action lu-link-add" id = "btnExportar">' +
                        '<img height="50" width="150" src="/img/excelx.png"> <br>' +
                        '<span>Exportar</span>' +
                        ' </a>' +
                        '</div>' +
                        '</div>',
                    title: 'Reporte de Asistencia',
                    action: newExportAction,

                }

            ],
            "lengthMenu": [[20, 50, 100, 10000], [20, 50, 100, "All"]],
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

    var Eventos = function (ReporteAsistencia_CTL) {


        $(ReporteAsistencia_CTL).on("change", "select#TipoEmpresaSearch", function () {
            dataTableReporteAsistencia.fnReloadAjax();
        });
        $(ReporteAsistencia_CTL).on("change", "select#IdDepartamentoSearch", function () {
            var id = $(this).val();
            webApp.ListaUbicacion(id);
            dataTableReporteAsistencia.fnReloadAjax();
        });
        $(ReporteAsistencia_CTL).on("change", "select#IdUbicacionSearch", function () {
            dataTableReporteAsistencia.fnReloadAjax();
        });
        $(ReporteAsistencia_CTL).on("change", "select#EntidadSearch", function () {
            var id = $(this).val();
            webApp.ListaCategoria(id);
            dataTableReporteAsistencia.fnReloadAjax();
        });
        $(ReporteAsistencia_CTL).on("change", "select#IdCategoriaSearch", function () {
            dataTableReporteAsistencia.fnReloadAjax();
        });
        $(ReporteAsistencia_CTL).on("change", "select#IdTerminalSearch", function () {
            dataTableReporteAsistencia.fnReloadAjax();
        });

        $("#FechaFinSearch").on("change", function () {
            dataTableReporteAsistencia.fnReloadAjax();
        });
    }
    return {
        init: function () {
            var _this = this;
            _this.ReporteAsistencia_CTL = $("#ReporteAsistencia");
            Eventos(_this.ReporteAsistencia_CTL);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableReporteAsistenciaView(urlReporteAsistenciaListar);
            });
            webApp.ListaCategoria($("#EntidadSearch").val());
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableReporteAsistencia.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);