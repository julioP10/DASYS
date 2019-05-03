
var Utils = function () {
    //AJAX PARA MOSTRAR VISTAS( PAGINAS HIJA)
    //Actualización del contenido del container
    var LoadPartialView = function () {
        var cookieIdx = "PANEL_IDX";
        var cookieActualSubItem = "PANEL_APLICACION";

        $(".enlace-ajax").click(function (e) {
            e.preventDefault();
            $('#espacio').html('');
            var ruta = $(this).attr("data-url-ajax");  //'/Ventas/Contacto/Index';

            GuardarRutaDelMenu(ruta);

            $.ajax({
                type: "GET",
                url: ruta,
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (data) {
                    $('#espacio').html(data);
                  
                    var ulrs = ruta.split("/");
                    var urlValidar = ulrs[1];
                   
                }
                , error: function (jqXHR, textStatus, errorThrown) {
                    console.log("Error : " + jqXHR.status);
                    //window.location.href = "/StatusCode/" + jqXHR.status;
                }
            });
        });
    };

    var GuardarRutaDelMenu = function (ruta) {
        localStorage.rutaDelMenuSeleccionado = ruta;
        var texto = ruta,
            separador = "/",
            textoseparado = texto.split(separador);
        $("#redireccionMenu").text(textoseparado[1] + '>' + textoseparado[2]+'>');
        $("#redireccionMenu").attr("data-url-ajax", ruta);
    };

    var ConfigurarDataTable = function () {
        $.extend($.fn.dataTable.defaults, {
            language: { url: urlDatatableLanguage },
            responsive: true,
            "lengthMenu": [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]],
            "bProcessing": true,
            "dom": 'fltip'
        });
    };

    // ver Notificaciones
        $("#verNotificacion").click(function (e) {
            e.preventDefault();
            $('#espacio').html('');
            var ruta ='/Ventas/Counter/Agenda';
            $.ajax({
                type: "GET",
                url: ruta,
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (data) {
                    localStorage.EventoTipo = 'agendaDay';
                    $('#espacio').html(data);
                }
                , error: function (jqXHR, textStatus, errorThrown) {
                    console.log("Error : " + jqXHR.status);
                }
            });
        });

    //Logout
    $("#Logout").on("click", function () {
        var urlLogout = '/Login/LogOut';

        var mensaje = {
            title: "Cerrar Sesion",
            text: "¿Está seguro de salir del sistema ?",
            confirmButtonText: "Salir",
            closeOnConfirm: true
        };
        var cookies = document.cookie.split(";");
        for (var i = 0; i < cookies.length; i++) {
            var equals = cookies[i].indexOf("=");
            var name = equals > -1 ? cookies[i].substr(0, equals) : cookies[i];
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
        }
        webApp.sweetajax(mensaje, { url: urlLogout },
            function (data) {
                location.reload();
            });
    });
    $("#btn-enviarCorreo").on("click", function () {
        $("#ModalEntidades").modal("show");
    });
    $("#btnEnviarCorreoDigitalizacion").on("click", function () {
        var entidad = $("#_Entidades").val();
        var query = {
            TipoEntidad: entidad
        };
        var mensaje = {
            title: "Envio de correo",
            text: "¿Estas seguro de enviar correo de los cambios realizados?",
            confirmButtonText: "Si acepto",
            closeOnConfirm: false
        };
        var _url = "/Digitalizacion/Digitalizacion/EnviarCorreo";
        webApp.sweetajax(mensaje, { url: _url, parametros: query }
            , function (jsonResponse) {
                var urlDigitalizacionColaboradorIndex = _urlIndex;
                if (jsonResponse.type === "success") {
                    webApp.sweetresponseOk(mensaje.title, jsonResponse, urlDigitalizacionColaboradorIndex);
                } else {
                    webApp.sweetmensaje(mensaje.title, jsonResponse.mensaje, jsonResponse.type);
                }
            }
        );
    });
    $("#_usuarioPerfil").on("click", function () {
        var _url = "/Seguridad/Usuario/Perfil";
        webApp.getDataVistaViewPartial(_url, function () {
            ListaEmpresa($("#_tipoEmpresa").val());  
            webApp.CampoLlenoInput();
        });
    });
    var ListaEmpresa = function (id) {
        var url = "/Comun/ListaEmpresa";
        var param = {
            codigo: id
        }
        webApp.JsonParam(url, param, function (data) {
            $('#espacio #IdEmpresa').empty();
            $('#espacio #IdEmpresa').append($('<option>', {
                value: '0',
                text: 'Seleccione Empresa'
            }))
            $.each(data.data, function (i, item) {
                $('#espacio #IdEmpresa').append($('<option>', {
                    value: item.value,
                    text: item.nombre
                }))
            });

            if ($('#espacio #_IdEmpresa').val() !== "") {
                $('#espacio #IdEmpresa').val($('#espacio #_IdEmpresa').val());
            }
        });
    }
    $(".menu-opcion").on("click", function () {
        $(".menu-opcion").removeClass("active");
        $(".menu-modulo").removeClass("active");
        $(this).addClass("active");
        $(this).parents(".menu-modulo").eq(0).addClass("active");
    });
    return {
        init: function () {
            LoadPartialView();
        },
        ConfigurarDataTable: ConfigurarDataTable
    }
}(jQuery);