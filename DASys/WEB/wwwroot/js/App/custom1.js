function lu_sidebarToggle() {
    $(".sidebar-toggler2").on("click", function () {
        if (!$("body").hasClass("sidebar-md") && !$("body").hasClass("sidebar-sm")) {
            $("body").toggleClass("sidebar-hidden");
        }
    });
}
function lu_toggleClassCampoLleno() {
    $('input.form-control').each(function () {
        $(this).val() ? $(this).addClass('campo-lleno').parent(":not(.form-group)").addClass('campo-lleno') : $(this).removeClass('campo-lleno').parent(":not(.form-group)").removeClass('campo-lleno');
    });   
}
function lu_toggleClassCampoLlenoFocus() {
    $('input.form-control').focus(function () {
        $(this).addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
    });
}
function lu_toggleClassCampoLlenoBlur() {
    $('input.form-control').on("blur", function (e) {
        $(this).removeClass('campo-focus').parent(":not(.form-group)").removeClass('campo-focus');
        $(this).val() ? $(this).addClass('campo-lleno').parent(":not(.form-group)").addClass('campo-lleno') : $(this).removeClass('campo-lleno').parent(":not(.form-group)").removeClass('campo-lleno');
    });
}

function er_calcHeightMenu() {
    $('.sf-menu li').hover(function () {
        var $elemUl = $(this).find("ul.sidebar-section-subnav");
        var $elemHeight = $elemUl.height();
        $elemHeight = $elemHeight / 2 - 22;
        $elemUl.attr('style', 'top: -' + $elemHeight + 'px');
        //console.log($elemHeight);
    });
}

//function lu_toggleClassCampoLlenoKeyUp() {
//    $('input.form-control:not(:focus)').on("keyup", function (e) {
//        $(this).val() ? $(this).addClass('campo-lleno').parent(":not(.form-group)").addClass('campo-lleno') : $(this).removeClass('campo-lleno').parent(":not(.form-group)").removeClass('campo-lleno');
//    });
//}

(function ($) {
    "use strict";

    //Ready cuando termino de cargar el /home
    $(document).ready(function () {
        lu_sidebarToggle();
        lu_toggleClassCampoLleno();
        lu_toggleClassCampoLlenoFocus();
        lu_toggleClassCampoLlenoBlur();

        er_calcHeightMenu();

        //lu_toggleClassCampoLlenoKeyUp();
        $('[data-toggle="tooltip"]').tooltip({
            placement: 'top'
        });

        //$(".sidebar-user-a__avatar").on("click", function () {
        //    $("#menuPrincipal").toggleClass("ocultame");
        //    $("#menuSecundario").toggleClass("ocultame");
        //});

        $('#menuPrincipal').superfish({
            animation: { height: 'show' },
            delay: 1200
        });

    });
})(jQuery);