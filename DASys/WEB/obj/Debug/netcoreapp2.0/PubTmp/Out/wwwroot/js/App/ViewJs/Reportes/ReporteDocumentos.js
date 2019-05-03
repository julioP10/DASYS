
var contacto = null;
var dataTableReporteDocumentos = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar ReporteDocumentos';
var tituloRegistro = 'Registrar  ReporteDocumentos';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var ReporteDocumentos = function () {
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
    var dataTableReporteDocumentosView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#ReporteDocumentosDataTable')) {
        dataTableReporteDocumentos = $("#ReporteDocumentosDataTable").dataTable({
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
                { "data": "id" },
                { "data": "nombre" },
                { "data": "genero" },
                { "data": "empresa" },
                { "data": "departamento" },
                { "data": "ubicacion" },
                { "data": "categoria" },
                { "data": "documento" },
                {
                    "data": function (obj) {
                        return (obj.adjuntado =="SI") ? '<span class="badge badge-pill badge-success">Adjuntado</span>' : '<span class="badge badge-pill badge-danger">Sin adjuntar</span>'
                    }
                },
                {
                    "data": function (obj) {

                        return (obj.obligatorio === 'SI') ? '<span class="badge badge-pill badge-success">SI</span>' : '<span class="badge badge-pill badge-info">NO</span>'
                    }
                },
                {
                    "data": function (obj) {
                        return (obj.fechaVencimiento === 'SI') ? '<span class="badge badge-pill badge-success">SI</span>' : '<span class="badge badge-pill badge-info">NO</span>'
                    }
                },
                { "data": "estadoAdjunto" },
                { "data": "observacionAdjunto" },
                { "data": "fecha" },
                { "data": "diasRestante" }
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
                { "className": "dt-body-center", "aTargets": [8], "width": "5%" },
                { "className": "dt-body-center", "aTargets": [9], "width": "5%" },
                { "className": "dt-body-center", "aTargets": [10], "width": "5%" },
                { "className": "dt-body-center", "aTargets": [11], "width": "5%" },
                { "className": "dt-body-center", "aTargets": [12], "width": "5%" },
                { "className": "dt-body-center", "aTargets": [13], "width": "5%" },
                { "className": "dt-body-center", "aTargets": [14], "width": "5%" },
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
                    title: 'Reporte de documentos',
                    action: newExportAction,

                }

            ],

            "bStateSave": false,
            "lengthMenu": [[20, 50, 100, 10000], [20, 50, 100, "All"]],
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

    var Eventos = function (ReporteDocumentos_CTL) {


        $(ReporteDocumentos_CTL).on("change", "select#EntidadSearch", function () {
            dataTableReporteDocumentos.fnReloadAjax();
        });

        $(ReporteDocumentos_CTL).on("change", "select#IdDepartamentoSearch", function () {
            var id = $(this).val();
            webApp.ListaUbicacion(id);
            dataTableReporteDocumentos.fnReloadAjax();
        });
        $(ReporteDocumentos_CTL).on("change", "select#IdUbicacionSearch", function () {
            dataTableReporteDocumentos.fnReloadAjax();
        });
        $(ReporteDocumentos_CTL).on("change", "select#EntidadSearch", function () {
            var id = $(this).val();
            webApp.ListaCategoria(id);
            dataTableReporteDocumentos.fnReloadAjax();
        });
        $(ReporteDocumentos_CTL).on("change", "select#IdCategoriaSearch", function () {
            dataTableReporteDocumentos.fnReloadAjax();
        });
        $(ReporteDocumentos_CTL).on("change", "select#IdTerminalSearch", function () {
            dataTableReporteDocumentos.fnReloadAjax();
        });
      
        $("#FechaFinSearch").on("change", function () {
            dataTableReporteDocumentos.fnReloadAjax();
        });
    }
    return {
        init: function () {
            var _this = this;
            _this.ReporteDocumentos_CTL = $("#ReporteDocumentos");
            Eventos(_this.ReporteDocumentos_CTL);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableReporteDocumentosView(urlReporteDocumentosListar);
            });
            
            webApp.ListaCategoria($("#EntidadSearch").val());
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableReporteDocumentos.fnReloadAjax();
                });
            }
        },
         filtrar: function (e) {
             
             dataTableReporteDocumentos.fnReloadAjax();
        }

    };
}(jQuery);