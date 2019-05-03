(function ($) {
    "use strict";

    //Ready cuando carga termino de cargar #espacio (Excepcion el /home)
    $(document).ready(function () {
        
        webApp.cargarCalendarios();
        webApp.bloquearFechaAntes();

        var formu = $("form .form-group:first-child");
        var formuFirst = formu.children().first();

        if (formuFirst.is("input")) {
            var input = $("form .form-group:first-child input:first-child:not(.lu-cal)").attr("id");
            $('#' + input).focus()
                .putCursorAtEnd()
                .on("focus", function () { // could be on any event
                    $('#' + input).putCursorAtEnd();
                });
        }
        if (formuFirst.is("select")) {
            var select = $("form .form-group:first-child select:first-child").attr("id");
            $('#' + select).focus();
        }
        if ($("section").hasClass("view-reporte-charlas")) {
            $("section.view-reporte-charlas #NombreSeach").focus();
        }

        //FIX CENTRADO DE TOOLTIPS
        $(document).on('mouseenter', '[data-toggle=tooltip]', function () {
            $(this).tooltip('show');
        });
        $(document).on('mouseleave', '[data-toggle=tooltip]', function () {
            $(this).tooltip('hide');
        });

        //Renderiza cuando termino de cargar la tabla (Callback after table is drawn)
        //var anchosTabla = [];
        //$('table.dataTable').on('draw.dt', function () {
        //});

        //DATE PICKER MES

        //DATE PICKER MES EN LISTADO DE METAS
        var getDate = function (input) {
            return new Date(input.date.valueOf());
        }
    });

    $(".custom-modal").on('shown.bs.modal', function () {
        webApp.cargarCalendarios();
        webApp.bloquearFechaAntes();
    });

    //Datatables en tabs
    $('a.lu-tab-datatable').on('shown.bs.tab', function (e) {
        let target_tab = $(e.target).attr("href");
        let target_dt = $(target_tab).find(".dataTables_scrollBody > table").attr("id");
        $("#" + target_dt).DataTable().draw();
    })
    $("#FechaInicioSearch").on("change", function () {
        var valor = $(this).val();
        webApp.validarFechaDesdeHasta("FechaFinSearch", valor);
        $("#FechaFinSearch").prop("disabled", false);
    });
})(jQuery);

//PUT CURSOR TO THE END
jQuery.fn.putCursorAtEnd = function () {
    return this.each(function () {
        // Cache references
        var $el = $(this),
            el = this;

        // Only focus if input isn't already
        //if (!$el.is(":focus")) {
        //    $el.focus();
        //}

        // If this function exists... (IE 9+)
        if (el.setSelectionRange) {
            // Double the length because Opera is inconsistent about whether a carriage return is one character or two.
            var len = $el.val().length * 2;

            // Timeout seems to be required for Blink
            setTimeout(function () {
                el.setSelectionRange(len, len);
            }, 1);
        } else {
            // As a fallback, replace the contents with itself
            // Doesn't work in Chrome, but Chrome supports setSelectionRange
            $el.val($el.val());
        }

        // Scroll to the bottom, in case we're in a tall textarea
        // (Necessary for Firefox and Chrome)
        this.scrollTop = 999999;
    });
};