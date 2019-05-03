
var contacto = null;
var dataTableReporteEmpresa = null;
var selectedRow = 0;
var mensajeRegistro = '¿Desea registrar los datos ingresados?';
var mensajeActualizar = '¿Desea actualizar los datos ingresados?';
var tituloActualizar = 'Actualizar ReporteEmpresa';
var tituloRegistro = 'Registrar  ReporteEmpresa';
var typeActualizar = 'Actualizar';
var typeRegistro = 'Registrar';

var ReporteEmpresa = function () {

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
    var dataTableReporteEmpresaView = function (url) {
        //if (!$.fn.DataTable.isDataTable('#ReporteEmpresaDataTable')) {
        dataTableReporteEmpresa = $("#ReporteEmpresaDataTable").dataTable({
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
                { "data": "idEmpresa" },
                { "data": "razonSocial" },
                { "data": "ruc" },
                { "data": "direccionFiscal" },
                { "data": "tipoEmpresa" },
                { "data": "padreSubcontratista" },
                { "data": "categoria" },
                { "data": "estado" }
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
                    title: 'Reporte de empresas',
                    action: newExportAction,

                }

            ],
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

    var Eventos = function (ReporteEmpresa_CTL) {


        $(ReporteEmpresa_CTL).on("change", "select#TipoEmpresaSearch", function () {
            dataTableReporteEmpresa.fnReloadAjax();
        });
        $(ReporteEmpresa_CTL).on("change", "select#IdCategoriaSearch", function () {
            dataTableReporteEmpresa.fnReloadAjax();
        });
        $("#FechaFinSearch").on("change", function () {
            dataTableReporteEmpresa.fnReloadAjax();
        });
    }
    return {
        init: function () {
            var _this = this;
            _this.ReporteEmpresa_CTL = $("#ReporteEmpresa");
            Eventos(_this.ReporteEmpresa_CTL);
            //Configuracion DataTable
            Utils.ConfigurarDataTable();
            checkSessionUser.checkSession(function () {
                dataTableReporteEmpresaView(urlReporteEmpresaListar);
            });
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
                },
                minDate: 'now'
            });
            webApp.ListaCategoria("EMPRESA");
        },
        buscar: function (e) {

            let tecla = (document.all) ? e.keyCode : e.which;
            if (tecla === 13) {
                checkSessionUser.checkSession(function () {
                    dataTableReporteEmpresa.fnReloadAjax();
                });
            }
        }

    };
}(jQuery);