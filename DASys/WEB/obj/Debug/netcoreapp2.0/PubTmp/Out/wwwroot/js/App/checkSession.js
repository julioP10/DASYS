var checkSessionUser = function () {
    var checkSessionInit = function () {
        $.ajax({
            type: "POST",
            url: VerifySession,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (response) {
                if (response.type === 'error') {
                    redireccionarLogin(response.Title, response.Message);
                }
            },
            error: function (error) {
                webApp.sweetmensaje('Error', error, 'error');
            }
        });
    }

    return {
        init: function () {
            checkSessionInit();
        },

        checkSession: function (callback) {

            $.ajax({
                type: "POST",
                url: VerifySession,
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (response) {
                    if (response.type === 'error') {
                        redireccionarLogin(response.Title, response.Message);
                    } else {
                        if (callback != null && typeof (callback) == "function")
                            callback();
                    }
                },
                error: function (response) {
                    webApp.sweetmensaje('Error', response, 'error');
                }
            });        
        }
    }
}(jQuery);

function redireccionarLogin(titulo, mensaje) {
    webApp.sweetmensaje('Sesion', 'Sesion Terminada', 'error');
    setTimeout(function () {
        window.location.href = "/Login";
    }, 1000);
}