(function ($) {
    "use strict";

    //Ready cuando carga termino de cargar #espacio (Excepcion el /home)
    $(document).ready(function () {

        webApp.cargarCalendarios();
        webApp.bloquearFechaAntes();
        lu_toggleClassCampoLleno();
        lu_toggleClassCampoLlenoFocus();
        lu_toggleClassCampoLlenoBlur();
        $(".b-filtros .form-group:first-child > :first-child:not(.lu-cal)").focus();

        //Renderiza cuando termino de cargar la tabla (Callback after table is drawn)
        //$('#ContactoDataTable').on('draw.dt', function () {
        //    $('#ContactoDataTable tbody tr td').css({
        //        "background": "yellow"
        //    });
        //});
    });

    $(".custom-modal").on('shown.bs.modal', function () {
        webApp.cargarCalendarios();
        webApp.bloquearFechaAntes();
        lu_toggleClassCampoLleno();
        lu_toggleClassCampoLlenoFocus();
        lu_toggleClassCampoLlenoBlur();
    });

    //$("#myModalSeg").on('shown.bs.modal', function () {
    //    webApp.cargarCalendarios();    
    //    lu_toggleClassCampoLleno();
    //    lu_toggleClassCampoLlenoFocus();
    //    lu_toggleClassCampoLlenoBlur();
    //});

    //$("#registrar-contacto").on('shown.bs.modal', function () {
    //    webApp.cargarCalendarios();
    //    webApp.bloquearFechaAntes();
    //    lu_toggleClassCampoLleno();
    //    lu_toggleClassCampoLlenoFocus();
    //    lu_toggleClassCampoLlenoBlur();
    //});

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