var NumeroDeFilasDataTable = 0;
(function ($) {
    $.dataTable = function (e) {
        this.table = e;
        this.init();
        $(e).data('instance', this);
        return this;
    };

    var $sdt = $.dataTable;
    $sdt.fn = $sdt.prototype = {
        dataTable: '0.1.1'
    };

    $sdt.fn.extend = $sdt.extend = $.extend;
    $sdt.fn.extend({
        clear: function () {
            $(this.table).find("tfoot").empty();
            $(this.table).find("tbody tr:not(.model)").remove();
        },
        /**
        *   Se encarga de inicializar el control, y verificar que existen los componentes
        *   necesarios
        */
        init: function () {
            $(this.table).find("tfoot").empty();
            $(this.table).find("tbody tr:not(.model)").remove();
            $(this.table).find("tbody tr.model").addClass("ui-widget-content");
            this.loadData();
        },
        /**
        *
        */
        loadData: function () {
            var obj = this;
            var a = $(this.table).find("thead a.onload");
            var url = $(a).attr("href");
            if (url == undefined) throw 'Debe definir href="" en la etiqueta <a class="onload" root="" href="" /> del dataTable.';
            var root = $(a).attr("root");

            var params = $.extend({}, $(this.table).data("params"));
            var limit = $(this.table).attr("data-limit") || 10;
            var offset = $(this.table).attr("data-offset") || 0;
            params.o = offset;
            params.l = limit;
            obj.offset = offset;
            obj.limit = limit;

            var _params = $(this.table).data('params');
            if (_params == undefined || _params == null) {
                _params = $(a).attr("data-params");
                if ((_params != undefined) && (_params != "")) {
                    _params = _params.split(",");
                    for (i = 0; i < _params.length; i++) {
                        var key = _params[i].split(":");
                        var value = key[1].replace(/&comma;/g, ',');
                        key = key[0];
                        eval('params.' + key + ' ="' + value + '"');
                    }
                }
            } else {
                for (i = 0; i < _params.length; i++) {
                    var key = _params[i].key;
                    var value = _params[i].value;
                    eval('params.' + key + ' ="' + value + '"');
                }
            }

            $(this.table).find("thead th a.order[data-order][data-orderby]").each(function () {
                var key = "orderby";
                var value = $(this).attr("data-orderby");
                eval('params.' + key + ' ="' + value + '"');
                params.order = $(this).attr("data-order");
            });

            this.getData(url, params, root);
        },

        pagination: function (c, tfoot) {
            if (tfoot == undefined) return false;
            var obj = this;
            NumeroDeFilasDataTable = c; // asignando numero de filas que viene en total
            var page = Math.floor(obj.offset / obj.limit) + 1; // numero de página actual
            var npages = Math.floor(c / obj.limit) + ((c % obj.limit > 0) ? 1 : 0); // total de páginas

            $(tfoot).empty();
            var tr = document.createElement("tr");
            var td = document.createElement("td");
            var span, a;
            $(tfoot).append(tr);
            $(tr).append(td);
            $(td).addClass("pagination");
            $(td).css("text-align", "right");
            var columns = $(obj.table).find("th").length;

            var showNumberRows = $(obj.table).attr('showNumberRows');
            if (showNumberRows == "true") {
                var colsNumberRows = $(obj.table).attr('colsNumberRows');
                if (colsNumberRows == undefined) colsNumberRows = 2;
                else colsNumberRows = parseFloat(colsNumberRows);

                var tdx = document.createElement("td");
                $(tdx).css("text-align", "left");
                $(tdx).css('color', '#0073EA');
                $(tdx).html('Total Registros: <span>' + c + '</span>');

                $(tdx).attr("colspan", colsNumberRows);
                $(tr).prepend(tdx);
                columns = columns - colsNumberRows;
            }
            $(td).attr("colspan", columns);

            var showPagination = $(obj.table).attr('showPagination') || 'true';
            if (showPagination == 'true') {
                if (npages > 6) {
                    var div = document.createElement("div");
                    $(div).css('display', 'inline-block');
                    $(div).append("<span class='pag'>P&aacute;gina " + page + " de " + npages + "</span>");
                    if (page != 1) {
                        span = document.createElement("span");
                        a = document.createElement("a");
                        $(a).attr("class", "off page");
                        $(a).attr("page", 1);
                        $(a).bind('click', this.changePage);
                        $(a).html('Primero &laquo;');
                        $(span).html(a);
                        $(div).append(span);

                        span = document.createElement("span");
                        a = document.createElement("a");
                        $(a).bind('click', this.changePage);
                        $(a).attr("class", "on page");
                        $(a).text('...');
                        $(span).html(a);
                        $(div).append(span);

                        span = document.createElement("span");
                        a = document.createElement("a");
                        $(a).bind('click', this.changePage);
                        $(a).attr("class", "off page");
                        $(a).attr("page", (page - 1));
                        $(a).html('&laquo;');
                        $(span).html(a);
                        $(div).append(span);
                    }
                    /* ================== CONFIGURACIÓN DEL PAGINADO ======================= */
                    var retroceder = 2; // cantidad de páginas a retroceder desde la actual.
                    // el número debe ser menor o igual a total_num.
                    var total_num = 5; // total de números que se muestran en el paginado.
                    // el número debe ser mayor o igual a 5
                    /* ===================================================================== */

                    var total_real = page + total_num; // número referencial de paáginas a mostrar.
                    var x = page; // página de inicio del paginado.
                    if ((page - 2) >= 1) {
                        var aux = npages - page;
                        if (aux >= 2) {
                            x -= retroceder;
                            total_real -= retroceder;
                        } else {
                            var add = retroceder - aux;
                            x = x - retroceder - add;
                            total_real = total_real - retroceder - add;
                        }
                    }
                    for (var j = x; j < total_real; j++) {
                        span = document.createElement("span");
                        a = document.createElement("a");
                        $(a).bind('click', this.changePage);
                        if (j == page) $(a).attr('class', 'on page');
                        else $(a).attr('class', 'off page');
                        $(a).attr('page', j);
                        $(a).text(j);
                        $(span).html(a);
                        $(div).append(span);
                    }
                    if (page != npages) {
                        span = document.createElement("span");
                        a = document.createElement("a");
                        $(a).bind('click', this.changePage);
                        $(a).html('&raquo;');
                        $(a).attr('class', 'off page');
                        $(a).attr('page', (page + 1));
                        $(span).html(a);
                        $(div).append(span);

                        span = document.createElement("span");
                        a = document.createElement("a");
                        $(a).bind('click', this.changePage);
                        $(a).text('...');
                        $(a).attr('class', 'on page');
                        $(span).html(a);
                        $(div).append(span);

                        span = document.createElement("span");
                        a = document.createElement("a");
                        $(a).bind('click', this.changePage);
                        $(a).html('&Uacute;ltimo &raquo;');
                        $(a).attr('class', 'off page');
                        $(a).attr('page', npages);
                        $(span).html(a);
                        $(div).append(span);
                    }
                    $(td).append(div);

                    var div_extra = document.createElement("div");
                    $(div_extra).css('display', 'inline-block');
                    $(div_extra).html(
                        '<div>&nbsp;&nbsp;<b>p&aacute;g</b>&nbsp;<input class="form-control input-xs" type="text" name="page" value="' + page + '" maxlength="5" style="width:60px;"/></div>' +
                        '<div>&nbsp;<input type="button" class="btn" paginas="' + npages + '" name="goPage" value="Ir" /></div>'
                    );
                    $(div_extra).find('input[name=page]').bind('keypress', this.goKeyPage);
                    $(div_extra).find('input[name=goPage]').bind('click', this.goPage);
                    $(div_extra).find('div').css('display', 'inline-block');
                    $(td).append(div_extra);
                } else {
                    for (var i = 1; i <= npages; i++) {
                        a = document.createElement("a");
                        $(a).attr("href", "#");
                        $(a).attr("page", i);
                        $(a).text(i);
                        $(a).bind('click', this.changePage);
                        var clase = "page";
                        if (page == i) clase += " on";
                        else clase += " off";
                        $(a).addClass(clase);
                        $(td).append(a);
                    }
                }
            }
            return true;
        },

        goKeyPage: function (e) {
            if (e.which == 13) {
                var obj = $(this).parents("table.dataTable").eq(0);
                $(obj).find('input[name=goPage]').click();
            }
        },

        goPage: function (e) {
            var obj = $(this).parents("table.dataTable").eq(0);
            var paginas = parseInt($(this).attr('paginas'));
            var page = parseInt($(obj).find('input[name=page]').val());
            if (page > paginas || page == 0 || isNaN(page)) {
                msgBox("El n&uacute;mero de p&aacute;gina no existe.", "Error");
                return false;
            }
            var limit = parseInt($(obj).attr('data-limit')) || 10;
            var offset = (page - 1) * limit;
            $(obj).attr("data-offset", offset);
            $(obj).DataTable("loadData");
            console.log("dadadadadad data");
            return true;
        },

        changePage: function (e) {
            e.preventDefault();
            var obj = $(this).parents("table.dataTable").eq(0);
            var page = parseInt($(this).attr('page'));
            var limit = parseInt($(obj).attr('data-limit')) || 10;
            var offset = (page - 1) * limit;
            $(obj).attr("data-offset", offset);
            console.log(obj);
            $(obj).DataTable("loadData");
        },

        parseData: function (response, root) {
            var obj = this;
            $(obj.table).find("tbody tr:not(.model)").remove();
            var c = response.count;
            obj.pagination(c, $(obj.table).find("tfoot"));
            var r = eval("response." + root);
            if (r == undefined) throw 'Debe definir root="" en la etiqueta <a class="onload" root="" href="" /> del dataTable.';
            $.each(r, function () {
                var tr = obj.newTR();
                obj.loadTR(tr, this);
            });
        },

        getData: function (url, params, root) {
            var obj = this;
            obj.offset = params.o;
            $(obj.table).trigger('begin', {});

            var noWaitDialog = $(obj.table).attr('noWaitDialog');
            if (noWaitDialog == "true") noWaitDialog = true;
            else noWaitDialog = false;

            $(obj.table).trigger('begin', {});

            getData({ url: url, data: params, dataType: 'json', noWaitDialog: noWaitDialog }, function (response) {
                obj.parseData(response, root);

                var filas = $(obj.table).find("tbody tr:not(.model)").length;
                if (filas == 0 && response.count > 0) {
                    $(obj.table).find('td.pagination a:first').click();
                }
                var td_x, obj_x, count = 0, i = 0;
                $(obj.table).find("tbody tr:not(.model) td[group]").each(function () {
                    var td = this;
                    var tr = $(td).parents('tr:eq(0)');
                    var obj1 = $(tr).data('data');
                    var campo = $(td).attr("class");

                    if (obj_x != undefined) {
                        var valorx = eval('obj_x.' + campo);
                        var valor = eval('obj1.' + campo);
                        //alert("valorx="+valorx+",valor="+valor+",count="+count);
                        if (valor == valorx) {
                            $(td).remove();
                        } else {
                            $(td_x).attr("rowspan", count);
                            obj_x = obj1;
                            td_x = td;
                            count = 0;
                        }
                        if ((i + 1) == filas) {
                            $(td_x).attr("rowspan", (count + 1));
                        }
                    }

                    if (i == 0) {
                        obj_x = obj1;
                        td_x = td;
                    }

                    count++;
                    i++;
                });

                $(obj.table).trigger('finished', response);
                //$("html").unblock(); // ustiliza block ui .js
            });
        },

        newTR: function () {
            var tr = $(this.table).find("tbody tr.model").clone();
            $(tr).removeClass("model");
            $(this.table).find("tbody").append(tr);
            return tr;
        },

        setValue: function (td, data) {
            //if ($(td).hasClass("dt-date")) {
            //    data = intDateParse(td, data);
            //}
            //if (data == true) {
            //    $(td).html("<span style='color: green;font-size: 20px;font-weight: bold;'>✔</span>");
            //} else if (data == false) {
            //    $(td).text("");
            //} else {
            //    $(td).text(data);
            //}
            $(td).text(data);
        },

        loadTR: function (tr, data) {
            var obj = this;
            $.each(data, function (i) {
                if ($(tr).find("td." + i).length <= 1) {
                    var td = $(tr).find("td." + i);
                    if (data[i] == undefined) obj.setValue(td, "");
                    else {
                        if ((typeof (data[i]) == "object") && data[i].length == undefined) {
                            var attribute = $(td).attr("attribute");
                            obj.setValue(td, eval("data['" + i + "']" + "." + attribute));
                        } else {
                            obj.setValue(td, data[i]);
                        }
                    }
                } else {
                    $.each($(tr).find("td." + i), function (j) {
                        var td = this;
                        var attribute = $(td).attr("attribute");
                        obj.setValue(td, eval("data['" + i + "']" + "." + attribute));
                    });
                }
            });
            $(tr).data('data', data);
            $(this.table).trigger('rowloaded', { tr: tr, obj: data });
        },

        orderBy: function () {
        }
    });

    $.fn.dataTable = function (o) {
        if (typeof (o) == 'string') {
            var ins = $(this).data('instance');
            args = Array.prototype.slice.call(arguments, 1);
            if (typeof (eval("ins." + o)) == 'function') {
                eval("ins." + o + "()");
            }
        } else {
            $(this).each(function () {
                new $sdt(this, o);
            });
        }
    };

    /** Activando funcion borrar ***/

    $(document).on('click', 'table.dataTable tbody tr a.delete', function (e) {
        e.preventDefault();
        var tr = $(this).parents("tr").eq(0);
        var table = $(tr).parents("table.dataTable").eq(0);
        var url = $(this).attr("href");
        var data = {};
        data.ajax = 'ajax';
        var obj = $(tr).data("data");
        var pk = $(tr).attr("data-pk");
        eval('data.' + pk + '= obj.' + pk);
        $(table).trigger('delete', { url: url, data: data, obj: obj, tr: tr });
    });

    /********************************/
    /*** Activando funcion editar ***/

    $(document).on('click', 'table.dataTable tbody tr a.edit', function (e) {
        e.preventDefault();
        var tr = $(this).parents("tr").eq(0);
        var table = $(tr).parents("table.dataTable").eq(0);
        var url = $(this).attr("href");
        var data = {};
        data.ajax = 'ajax';
        var obj = $(tr).data("data");
        var pk = $(tr).attr("data-pk");
        eval('data.' + pk + '= obj.' + pk);
        $(table).trigger('edit', { url: url, data: data, obj: obj, tr: tr });
    });

    /**
    * Habilita la Funcion de ordenar.
    * Los TH deben ser enlaces de la clase order
    */

    $(document).on('click', 'table.dataTable thead th a.order', function (e) {
        e.preventDefault();
        var table = $(this).parents("table.dataTable").eq(0);
        var order = $(this).attr("data-order") || undefined;
        $(table).find("thead th a.order").removeAttr("data-order");
        if (order == undefined) order = 'asc';
        else if (order == 'asc') order = 'desc';
        else if (order == 'desc') order = undefined;
        if (order == undefined) $(this).removeAttr("data-order");
        else $(this).attr("data-order", order);
        $(table).DataTable("loadData");
    });
})(jQuery);