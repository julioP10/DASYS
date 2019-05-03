cache = new Array();
if (typeof (translateString) == "undefined") translateString = {};
translateString.PLEASEWAIT = "Espere un momento ...";

function getData(params, fsuccess, ferror) {
    params.asyn = (params.asyn == undefined || params.asyn == null) ? true : params.asyn;
    var url;
    var defaults = {
        url: params.url || null,
        data: params.data || { ajax: 'ajax' },
        dataType: params.dataType || 'json',
        type: params.type || 'post',
        asyn: params.asyn,
        beforeSend: function (xhr, set) {
            if (params.cache != "true") return true;
            url = murmurhash3_32_gc(set.url, 0);
            if (cache[url] != undefined) {
                defaults.success(cache[url]);
                return false;
            } else {
                return true;
            }
        },
        dataFilter: function (data, type) {
            if (type == 'json') {
                var str = data;
                return str;
            }
            return data;
        },
        success: function (response) {
            if (typeof (response) == 'string' && params.dataType == 'json') {
            }

            if (params.noWaitDialog == undefined || !params.noWaitDialog) {
                //$("div#dlgWait").data("kendoWindow").close();
            }

            if (params.dataType == 'html') {
                if (typeof (fsuccess) == 'function') {
                    if (params.cache == "true") cache[url] = response;
                    eval('fsuccess(response)');
                }
            } else {
                eval('fsuccess(response)');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            debug("ERROR GEDATA AJAX", jqXHR, textStatus, errorThrown);
            var r = jqXHR.responseText;
            var o = eval('(' + r + ')');
            eval('fsuccess(o.response)');
        }
    };
    $.ajax(defaults);
}