
/*
 *  Document   : webApp.js
 *  Description: Custom scripts and plugin initializations
 */

var webApp = function () {
    var _popupEspera = "popupEspera";
    var _popupMensaje = "popupMensaje";
    var _popupConfirmacion = "popupConfirmacion";
    var _popupReConfirmacion = "popupReConfirmacion";
    var _popupEliminacionMensaje = "popupEliminar";
    var _mensajeEspera = "";
    var _tituloPopupMensaje = "";
    var _tituloPopupComfirmacion = "";
    var _tituloEliminacionPopupMensaje = "";
    var _mensajePopupConfirmacion = "";
    var _mensajePopupEliminacionConfirmacion = "";
    var _btnCancelar = "";
    var _btnAceptar = "";
    var _formatoFecha = "";

    var createMessageDialog = function () {
        var dialogMessage = "<div id='" + _popupMensaje + "' tabindex='-1' role='dialog' aria-hidden='true' class='modal fade' data-backdrop='static' style='z-index:100000;'>";
        dialogMessage += "<div class='modal-dialog'>";
        dialogMessage += "<div class='modal-content'>";
        dialogMessage += "<div class='modal-header'>";
        dialogMessage += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
        dialogMessage += "<h4 class='modal-title'> <i class='ace-icon fa fa-exclamation-triangle yellow'></i> " + _tituloPopupMensaje + "</h4>";
        dialogMessage += "</div>";
        dialogMessage += "<div class='modal-body paddingTop15'></div>";
        dialogMessage += "<div class='modal-footer' style='margin-top: 0px; margin-bottom: 0px;'>";
        dialogMessage += "<button class='btn btn-primary btn-aceptar btn-sm' data-dismiss='modal'><i class='fa fa-thumbs-o-up'></i> " + _btnAceptar + "</button>";
        dialogMessage += "</div>";
        dialogMessage += "</div>";
        dialogMessage += "</div>";

        $("body").append(dialogMessage);
    };

    var createConfirmDialog = function () {
        var dialogConfirm = "<div id='" + _popupConfirmacion + "' tabindex='-1' role='dialog' aria-hidden='true' class='modal fade' data-backdrop='static' style='z-index:100000;'>";
        dialogConfirm += "<div class='modal-dialog'>";
        dialogConfirm += "<div class='modal-content'>";
        dialogConfirm += "<div class='modal-header'><h4 class='modal-title'> <i class='ace-icon fa fa-check'></i> " + _tituloPopupComfirmacion + "</h4></div>";
        dialogConfirm += "<div class='modal-body paddingTop15'><p></p></div>";
        dialogConfirm += "<div class='modal-footer' style='margin-top: 0px; margin-bottom: 0px;'>";
        dialogConfirm += "<button class='btn btn-danger btn-sm' data-dismiss='modal'><i class='fa fa-remove'></i> " + _btnCancelar + "</button> ";
        dialogConfirm += "<button class='btn btn-primary btn-sm' data-dismiss='modal'><i class='fa fa-thumbs-o-up'></i> " + _btnAceptar + "</button>";
        dialogConfirm += "</div>";
        dialogConfirm += "</div>";
        dialogConfirm += "</div>";

        $("body").append(dialogConfirm);
    };

    var createReConfirmDialog = function () {
        var dialogConfirm = "<div id='" + _popupReConfirmacion + "' tabindex='-1' role='dialog' aria-hidden='true' class='modal fade' data-backdrop='static' style='z-index:100000;'>";
        dialogConfirm += "<div class='modal-dialog'>";
        dialogConfirm += "<div class='modal-content'>";
        dialogConfirm += "<div class='modal-header'><h4 class='modal-title'><i class='ace-icon fa fa-check'></i> " + _tituloPopupComfirmacion + "</h4></div>";
        dialogConfirm += "<div class='modal-body paddingTop15'><p></p></div>";
        dialogConfirm += "<div class='modal-footer' style='margin-top: 0px; margin-bottom: 0px;'>";
        dialogConfirm += "<button class='btn btn-danger btn-sm' data-dismiss='modal'><i class='fa fa-remove'></i> " + _btnCancelar + "</button> ";
        dialogConfirm += "<button class='btn btn-primary btn-sm' data-dismiss='modal'><i class='fa fa-thumbs-o-up'></i> " + _btnAceptar + "</button>";
        dialogConfirm += "</div>";
        dialogConfirm += "</div>";
        dialogConfirm += "</div>";

        $("body").append(dialogConfirm);
    };

    var createMessageDeleteDialog = function () {
        var dialogMessage = "<div id='" + _popupEliminacionMensaje + "' class='modal fade' tabindex='-1' role='dialog' aria-labelledby='lblTituloConfirmarEliminar' aria-hidden='true' class='modal fade' data-backdrop='static' style='z-index:100000;'>";
        dialogMessage += "<div class='modal-dialog'>";
        dialogMessage += "<div class='modal-content'>";
        dialogMessage += "<div class='modal-header'>";
        dialogMessage += '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>';
        dialogMessage += "<h4 id='lblTituloConfirmarEliminar' class='modal-title'> <i class='ace-icon fa fa-exclamation-triangle red'></i> " + _tituloEliminacionPopupMensaje + "</h4>";
        dialogMessage += "</div>";
        dialogMessage += "<div class='modal-body paddingTop15'><p></p></div>";
        dialogMessage += "<div class='modal-footer' style='margin-top: 0px; margin-bottom: 0px;'>";
        dialogMessage += "<button class='btn btn-danger btn-sm' data-dismiss='modal'><i class='fa fa-remove'></i> " + _btnCancelar + "</button> ";
        dialogMessage += "<button class='btn btn-primary btn-sm' data-dismiss='modal'><i class='fa fa-thumbs-o-up'></i> " + _btnAceptar + "</button>";
        dialogMessage += "</div>";
        dialogMessage += "</div>";
        dialogMessage += "</div>";

        $("body").append(dialogMessage);
    };

    var getDataForm = function (form) {
        var that = $(form);
        var url = that.attr('action');
        var type = that.attr('method');
        var data = {};

        var namex = "";
        that.find('[name]').each(function (index, value) {
            var that = $(this);
            var name = that.attr('name');
            var value = that.val();

            if (that.attr('type') === 'radio') {
                if (that.is(':checked')) {
                    data[name] = value;
                }
            } else if (that.attr('type') === 'checkbox') {
                if (that.is(':checked') && namex != name) {
                    data[name] = value;
                    namex = name;
                } else if (namex == name) {
                    namex = "";
                }
            }
            else if (namex == name && that.attr('type') === 'hidden') {
                namex = "";
            }
            else {
                data[name] = value;
            }
        });

        var obj = {
            url: url,
            type: type,
            data: data
        };

        return obj;
    };

    var formValidBootstrap = function () {
        //$('form').validateBootstrap(true);
    };

    var createLoading = function () {
        var urlImgLoading = location.protocol + "//" + location.host + "/Content/images/loading.gif";
        $("body").append('<div id="' + _popupEspera + '" tabindex="-1" role="dialog" aria-hidden="true" class="modal fade" data-backdrop="static" style="z-index:100000; "><div class="modal-dialog"><div class="modal-content"><div class="modal-body"><h4 class="text-center" > ' + _mensajeEspera + '</h4></div></div></div></div>');
    };

    var createGrid = function (opciones) {
        var grid = jQuery(opciones.grid_selector);
        var estadoSubGrid = false;

        if (opciones.caption == null)
            opciones.caption = "";

        if (opciones.sortname == null)
            opciones.sortname = 'Id';

        if (opciones.sortorder == null)
            opciones.sortorder = 'desc';

        if (opciones.subGrid != null)
            estadoSubGrid = true;

        if (opciones.rules == null)
            opciones.rules = false;

        if (opciones.viewrecords == null)
            opciones.viewrecords = true;

        if (opciones.rownumbers == null)
            opciones.rownumbers = true;

        if (opciones.rowNum == null)
            opciones.rowNum = 15;

        if (opciones.rowList == null)
            opciones.rowList = [opciones.rowNum, 20, 50, 100, 150];

        if (opciones.altRows == null)
            opciones.altRows = true;

        if (opciones.height == null)
            opciones.height = 'auto';

        if (opciones.width == null)
            opciones.width = 'auto';

        if (opciones.noregistro == null)
            opciones.noregistro = false;

        if (opciones.rules == null)
            opciones.rules = false;

        if (opciones.search == null)
            opciones.search = false;

        if (opciones.footerrow == null)
            opciones.footerrow = false;

        if (opciones.multiselect == null)
            opciones.multiselect = false;

        if (opciones.agregarBotones == null)
            opciones.agregarBotones = false;

        if (opciones.autowidth == null)
            opciones.autowidth = false;

        if (opciones.GridLocal == null) {
            opciones.GridLocal = false;

            if (opciones.CellEdit == null)
                opciones.CellEdit = false;
        }

        if (opciones.AlertTitle == null)
            opciones.AlertTitle = "Alerta";

        if (opciones.SinRegistro == null)
            opciones.SinRegistro = "Sin registros";

        if (opciones.NuevoCaption == null)
            opciones.NuevoCaption = "";

        if (opciones.NuevoTitle == null)
            opciones.NuevoTitle = "Nuevo";

        if (opciones.EditarCaption == null)
            opciones.EditarCaption = "";

        if (opciones.EditarTitle == null)
            opciones.EditarTitle = "Editar";

        if (opciones.EliminarCaption == null)
            opciones.EliminarCaption = "";

        if (opciones.EliminarTitle == null)
            opciones.EliminarTitle = "Eliminar";

        if (opciones.Lenguaje == null)
            opciones.Lenguaje = "es-PE";

        if (opciones.dialogDelete == null)
            opciones.dialogDelete = 'dialog-delete';

        if (opciones.dialogAlert == null)
            opciones.dialogAlert = 'dialog-alert';

        if (opciones.seleccioneRegistro == null)
            opciones.seleccioneRegistro = 'Por favor seleccione un registro';

        if (opciones.CustomButtons == null)
            opciones.CustomButtons = {};

        if (opciones.canEventSameRow == null)
            opciones.canEventSameRow = true;

        if (opciones.pgbuttons == null)
            opciones.pgbuttons = true;

        if (opciones.pginput == null)
            opciones.pginput == true;

        if (opciones.refresh == null)
            opciones.refresh == false;

        if (opciones.treeGrid == null)
            opciones.treeGrid = false;

        if (opciones.grouping == null)
            opciones.grouping = false;

        if (opciones.async == null)
            opciones.async = true;

        if (opciones.groupingViewOptions == null)
            opciones.groupingViewOptions = new Object();

        var rowKey;
        var lastRowKey;

        if (!opciones.GridLocal) {
            var settingsGrid = {
                prmNames: {
                    search: 'isSearch',
                    nd: null,
                    rows: 'rows',
                    page: 'page',
                    sort: 'sortField',
                    order: 'sortOrder',
                    filters: 'filters'
                },

                postData: { searchString: '', searchField: '', searchOper: '', filters: '' },
                jsonReader: {
                    root: 'rows',
                    page: 'page',
                    total: 'total',
                    records: 'records',
                    cell: 'cell',
                    id: 'id', //index of the column with the PK in it
                    userdata: 'userdata',
                    repeatitems: true
                },
                pgbuttons: opciones.pgbuttons,
                pginput: opciones.pginput,
                rowNum: opciones.rowNum,
                rowList: opciones.rowList,
                pager: opciones.pager_selector,
                sortname: opciones.sortname,
                viewrecords: opciones.viewrecords,
                shrinkToFit: opciones.shrinkToFit != null ? opciones.shrinkToFit : true,
                multiselect: opciones.multiselect,
                rownumbers: opciones.rownumbers,
                sortorder: opciones.sortorder,
                height: opciones.height,
                footerrow: opciones.footerrow,
                width: opciones.width,
                autowidth: opciones.autowidth,
                colNames: opciones.colNames,
                colModel: opciones.colModel,
                caption: opciones.caption,
                subGrid: estadoSubGrid,
                grouping: opciones.grouping,
                groupingView: {
                    groupField: opciones.groupingViewOptions.groupField == null ? [] : opciones.groupingViewOptions.groupField,
                    groupColumnShow: opciones.groupingViewOptions.groupColumnShow == null ? [] : opciones.groupingViewOptions.groupColumnShow,
                    groupText: opciones.groupingViewOptions.groupText == null ? [] : opciones.groupingViewOptions.groupText,
                    groupCollapse: opciones.groupingViewOptions.groupCollapse == null ? false : opciones.groupingViewOptions.groupCollapse,
                    groupOrder: opciones.groupingViewOptions.groupOrder == null ? [] : opciones.groupingViewOptions.groupOrder,
                    groupSummary: opciones.groupingViewOptions.groupSummary == null ? [] : opciones.groupingViewOptions.groupSummary,
                    hideFirstGroupCol: opciones.groupingViewOptions.hideFirstGroupCol == null ? true : opciones.groupingViewOptions.hideFirstGroupCol,
                    groupSummaryPos: opciones.groupingViewOptions.groupSummaryPos == null ? [] : opciones.groupingViewOptions.groupSummaryPos,
                },
                editurl: opciones.EditingOptions ? opciones.EditingOptions.editUrl : '',
                ajaxSelectOptions: { type: "POST", contentType: 'application/json; charset=utf-8', dataType: 'json', },
                subGridRowColapsed: function (subgrid_id, row_id) {
                    var subgrid_table_id, pager_id;
                    subgrid_table_id = subgrid_id + "_t";
                    pager_id = "p_" + subgrid_table_id;
                    jQuery("#" + subgrid_table_id).remove();
                    jQuery("#" + pager_id).remove();
                },
                subGridRowExpanded: function (subgrid_id, row_id) {
                    var subGrid = opciones.subGrid;

                    var subgrid_table_id, pager_id;
                    subgrid_table_id = subgrid_id + "_t";
                    pager_id = "p_" + subgrid_table_id;

                    $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");

                    var parameters = { Id: row_id };
                    $.ajax({
                        type: "POST",
                        url: subGrid.Url,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(parameters),
                        success: function (rsp) {
                            var data = (typeof rsp.d) == 'string' ? eval('(' + rsp.d + ')') : rsp.d;
                            $("#" + subgrid_table_id).jqGrid({
                                datatype: "local",
                                colNames: subGrid.ColNames,
                                colModel: subGrid.ColModels,
                                rowNum: 10,
                                rowList: [10, 20, 50, 100],
                                sortorder: "desc",
                                viewrecords: true,
                                rownumbers: true,
                                pager: "#" + pager_id,
                                loadonce: true,
                                sortable: true,
                                height: subGrid.Height,
                                width: subGrid.Width
                            });

                            for (var i = 0; i <= data.length; i++)
                                jQuery("#" + subgrid_table_id).jqGrid('addRowData', i + 1, data[i]);

                            $("#" + subgrid_table_id).trigger("reloadGrid");
                        },
                        failure: function (msg) {
                            $('#mensajeFalla').show().fadeOut(8000);
                        }
                    });
                },

                ondblClickRow: function (rowid) {
                    if (opciones.search) {
                        var ret = grid.getRowData(rowid);
                        SelectRow(ret);
                    }

                    if (opciones.EditingOptions != null && opciones.EditingOptions.canEditRowInline) {
                        if (opciones.BeforeEditHandler != null && typeof (opciones.BeforeEditHandler) == "function")
                            opciones.BeforeEditHandler(grid, rowKey);

                        var editparameters = {
                            "keys": true,
                            "oneditfunc": null,
                            "successfunc": opciones.SaveRowInline ? opciones.SaveRowInline : null,
                            "url": opciones.EditingOptions ? opciones.EditingOptions.editUrl : null,
                            "extraparam": {},
                            "aftersavefunc": null,
                            "errorfunc": null,
                            "afterrestorefunc": opciones.AfterSaveRowInline ? opciones.AfterSaveRowInline : null,
                            "restoreAfterError": true,
                            "mtype": "POST"
                        }

                        grid.jqGrid("editRow", rowKey, editparameters);

                        lastRowKey = rowKey;
                    }

                    if (opciones.DblClickHandler != null && typeof (opciones.DblClickHandler) == 'function') {
                        opciones.DblClickHandler(rowid);
                    }
                },
                onSelectRow: function (id) {
                    rowKey = grid.getGridParam('selrow');

                    if (rowKey != null) {
                        $("#" + opciones.identificador).val(rowKey);
                    }

                    if (opciones.EditingOptions != null && opciones.EditingOptions.canEditRowInline) {
                        if (lastRowKey !== rowKey) {
                            if (lastRowKey != "undefined") {
                                if ($("tr#" + lastRowKey).attr("editable") === "1") {
                                    grid.jqGrid('restoreRow', lastRowKey);
                                }
                            }
                        }
                    }

                    if (opciones.OnSelectRow != null && typeof (opciones.OnSelectRow) == 'function') {
                        if (opciones.canEventSameRow || (lastRowKey !== rowKey))
                            if (opciones.multiselect == false)
                                opciones.OnSelectRow(id);
                            else
                                opciones.OnSelectRow(id, opciones.nameArraySelected);
                    }
                    lastRowKey = rowKey;
                },
                onSelectAll: opciones.OnSelectAll,
                gridComplete: function () {
                    if (grid.getGridParam('records') == 0) {
                        if (opciones.noregistro == true) {
                            webApp.showMessageDialog(opciones.SinRegistro);
                        }
                    }

                    rowKey = null;
                    if (opciones.agregarBotones == true) {
                        AgregarBotones();
                    }

                    if (opciones.GridCompleteHandler != null && typeof (opciones.GridCompleteHandler) == "function")
                        opciones.GridCompleteHandler(grid);
                },
                loadComplete: function () {
                    if (opciones.LoadCompleteHandler != null && typeof (opciones.LoadCompleteHandler) == "function")
                        opciones.LoadCompleteHandler(this);
                },
                datatype: function (postdata) {
                    var migrilla = new Object();
                    migrilla.page = postdata.page;
                    migrilla.rows = postdata.rows;
                    migrilla.sidx = postdata.sortField;
                    migrilla.sord = postdata.sortOrder;
                    migrilla._search = postdata.isSearch;
                    migrilla.filters = postdata.filters;

                    if (opciones.rules != false) {
                        if (opciones.GetRulesMethod == null)
                            opciones.GetRulesMethod = GetRules;
                        var parametroExtra = null;
                        var customRules = opciones.GetRulesMethod();

                        if (migrilla.filters != "") {
                            parametroExtra = JSON.parse(migrilla.filters);

                            if (customRules != null) {
                                parametroExtra.groups = new Array();

                                if (opciones.AdvancedSearch == true) {
                                    parametroExtra.groups.push(customRules);
                                }
                                else {
                                    var nuevoSubGrupo = { groupOp: 'AND', rules: {} };
                                    nuevoSubGrupo.rules = customRules;

                                    parametroExtra.groups.push(nuevoSubGrupo);
                                }
                            }
                        }
                        else {
                            if (opciones.AdvancedSearch == true && customRules != null)
                                parametroExtra = customRules;
                            else if (customRules != null && customRules.length > 0) {
                                parametroExtra = { groupOp: 'AND', rules: {} };
                                parametroExtra.rules = customRules;
                            }
                        }

                        migrilla.filters = parametroExtra == null ? "" : parametroExtra;
                    }

                    if (migrilla._search == true) {
                        migrilla.searchField = postdata.searchField;
                        migrilla.searchOper = postdata.searchOper;
                        migrilla.searchString = postdata.searchString;
                    }

                    $.ajax({
                        url: opciones.urlListar,
                        type: 'post',
                        data: { grid: migrilla },
                        success: function (data, st) {
                            if (st == 'success') {
                                if (opciones.pivotGridOptions == null) {
                                    var jq = grid[0];
                                    jq.addJSONData(data);
                                } else {
                                    if (opciones.pivotGridOptions.colTotals)
                                        settingsGrid.footerrow = true;

                                    grid.jqGrid('jqPivot', data, {
                                        //grid.jqGrid('jqPivot', opciones.pivotGridOptions.urlData+"2?grid=" + gridPivot, {
                                        xDimension: opciones.pivotGridOptions.xDimensionColumns,
                                        yDimension: opciones.pivotGridOptions.yDimensionColumns,
                                        aggregates: opciones.pivotGridOptions.aggregateColumns,
                                        groupSummaryPos: opciones.pivotGridOptions.groupSummaryPos == null ? 'header' : opciones.pivotGridOptions.groupSummaryPos,
                                        colTotals: opciones.pivotGridOptions.colTotals == null ? false : opciones.pivotGridOptions.colTotals,
                                        frozenStaticCols: opciones.pivotGridOptions.frozenStaticCols == null ? false : opciones.pivotGridOptions.frozenStaticCols,
                                        groupSummary: opciones.pivotGridOptions.groupSummary == null ? true : opciones.pivotGridOptions.groupSummary,
                                        rowTotals: opciones.pivotGridOptions.rowTotals == null ? false : opciones.pivotGridOptions.rowTotals,
                                        rowTotalsText: opciones.pivotGridOptions.rowTotalsText == null ? "Total" : opciones.pivotGridOptions.rowTotalsText
                                    }, settingsGrid, opciones.pivotGridOptions.ajaxOptions);
                                }
                            }
                        },
                        error: function (a, b, c) {
                            alert('Error with AJAX callback:' + a.responseText);
                        }
                    });
                }
            };

            grid.jqGrid(settingsGrid);
        }
        else if (opciones.GridLocal) {
            var settingsGrid = {
                colNames: opciones.colNames,
                colModel: opciones.colModel,
                sortorder: opciones.sortorder,
                pgbuttons: opciones.pgbuttons,
                pginput: opciones.pginput,
                rowNum: opciones.rowNum,
                rowList: opciones.rowList,
                rownumbers: opciones.rownumbers,
                cellEdit: opciones.CellEdit,
                cellsubmit: "clientArray",
                pager: opciones.pager_selector,
                sortname: opciones.sortname,
                viewrecords: opciones.viewrecords,
                multiselect: opciones.multiselect,
                sortorder: opciones.sortorder,
                footerrow: opciones.footerrow,
                height: height,
                width: width,
                gridview: true,
                autowidth: false,
                caption: opciones.caption,
                subGrid: estadoSubGrid,
                editurl: opciones.editurl,
                grouping: opciones.grouping,
                groupingView: {
                    groupField: opciones.groupingViewOptions.groupField == null ? [] : opciones.groupingViewOptions.groupField,
                    groupColumnShow: opciones.groupingViewOptions.groupColumnShow == null ? [] : opciones.groupingViewOptions.groupColumnShow,
                    groupText: opciones.groupingViewOptions.groupText == null ? [] : opciones.groupingViewOptions.groupText,
                    groupCollapse: opciones.groupingViewOptions.groupCollapse == null ? false : opciones.groupingViewOptions.groupCollapse,
                    groupOrder: opciones.groupingViewOptions.groupOrder == null ? [] : opciones.groupingViewOptions.groupOrder,
                    groupSummary: opciones.groupingViewOptions.groupSummary == null ? [] : opciones.groupingViewOptions.groupSummary,
                    hideFirstGroupCol: opciones.groupingViewOptions.hideFirstGroupCol == null ? true : opciones.groupingViewOptions.hideFirstGroupCol,
                    groupSummaryPos: opciones.groupingViewOptions.groupSummaryPos == null ? [] : opciones.groupingViewOptions.groupSummaryPos,
                },
                treeGrid: opciones.treeGrid,
                gridComplete: function () {
                    if (opciones.GridCompleteHandler != null && typeof (opciones.GridCompleteHandler) == "function")
                        opciones.GridCompleteHandler();
                },
                loadComplete: function () {
                    if (opciones.LoadCompleteHandler != null && typeof (opciones.LoadCompleteHandler) == "function")
                        opciones.LoadCompleteHandler(this);
                },
                afterSaveCell: function (rowid, cellname, value, iRow, iCol) {
                    if (opciones.AfterSaveCellHandler != null)
                        opciones.AfterSaveCellHandler(rowid, cellname, value, iRow, iCol);
                },
                onSelectRow: function () {
                    rowKey = grid.getGridParam('selrow');

                    if (rowKey != null) {
                        $("#" + opciones.identificador).val(rowKey);
                    }
                    if (opciones.selectRowFunc != null) {
                        if (typeof (opciones.selectRowFunc) == 'function') {
                            opciones.selectRowFunc(rowKey)
                        }
                    }
                },
                ondblClickRow: function (rowid) {
                    if (opciones.search) {
                        var ret = grid.getRowData(rowid);
                        SelectRow(ret);
                    }

                    if (opciones.DblClickHandler != null && typeof (opciones.DblClickHandler) == 'function') {
                        opciones.DblClickHandler(rowid);
                    }
                },
                beforeEditCell: function (rowid, cellname, value, iRow, iCol) {
                    if (opciones.BeforeEditCellHandler != null)
                        opciones.BeforeEditCellHandler(rowid, cellname, value, iRow, iCol);
                },
                afterEditCell: function (rowid, cellname, value, iRow, iCol) {
                    if (opciones.AfterEditCellHandler != null)
                        opciones.AfterEditCellHandler(rowid, cellname, value, iRow, iCol);
                },
                rowattr: function (rowId) {
                    if (opciones.RowAttrHandler != null)
                        opciones.RowAttrHandler(rowId);
                }
            };

            if (opciones.pivotGridOptions == null) {
                settingsGrid.datatype = "local";

                grid.jqGrid(settingsGrid);
            } else {
                if (opciones.pivotGridOptions.colTotals)
                    settingsGrid.footerrow = true;

                grid.jqGrid('jqPivot', opciones.pivotGridOptions.urlData, {
                    xDimension: opciones.pivotGridOptions.xDimensionColumns,
                    yDimension: opciones.pivotGridOptions.yDimensionColumns,
                    aggregates: opciones.pivotGridOptions.aggregateColumns,
                    groupSummaryPos: opciones.pivotGridOptions.groupSummaryPos == null ? 'header' : opciones.pivotGridOptions.groupSummaryPos,
                    colTotals: opciones.pivotGridOptions.colTotals == null ? false : opciones.pivotGridOptions.colTotals,
                    frozenStaticCols: opciones.pivotGridOptions.frozenStaticCols == null ? false : opciones.pivotGridOptions.frozenStaticCols,
                    groupSummary: opciones.pivotGridOptions.groupSummary == null ? true : opciones.pivotGridOptions.groupSummary,
                    rowTotals: opciones.pivotGridOptions.rowTotals == null ? false : opciones.pivotGridOptions.rowTotals,
                    rowTotalsText: opciones.pivotGridOptions.rowTotalsText == null ? "Total" : opciones.pivotGridOptions.rowTotalsText
                }, settingsGrid, opciones.pivotGridOptions.ajaxOptions);
            }
        }

        grid.jqGrid('navGrid', opciones.pager_selector, {
            edit: false,
            add: false,
            del: false,
            search: false,
            refresh: false,
        },
            {}, // use default settings for edit
            {}, // use default settings for add
            {}, // delete instead that del:false we need this
            {
                multipleSearch: true,
                beforeShowSearch: function () {
                    return true;
                }
            });

        if (opciones.nuevo) {
            $(opciones.grid_selector).navButtonAdd(opciones.pager_selector, {
                caption: opciones.NuevoCaption,
                title: opciones.NuevoTitle,
                buttonicon: 'ace-icon fa fa-plus-circle purple',
                position: 'first',
                onClickButton: function () {
                    if (opciones.NuevoCommand != null && typeof (opciones.NuevoCommand) == "function")
                        opciones.NuevoCommand();
                    else
                        Nuevo();
                }
            });
        }

        if (opciones.editar) {
            $(opciones.grid_selector).navButtonAdd(opciones.pager_selector, {
                caption: opciones.EditarCaption,
                title: opciones.EditarTitle,
                buttonicon: 'ace-icon fa fa-pencil blue',
                position: 'second',
                onClickButton: function () {
                    if (rowKey != null) {
                        if (opciones.EditarCommand != null && typeof (opciones.EditarCommand) == "function")
                            opciones.EditarCommand(rowKey);
                        else
                            Editar(rowKey);
                    } else {
                        webApp.showMessageDialog(opciones.seleccioneRegistro);
                    }
                }
            });
        }

        if (opciones.eliminar) {
            $(opciones.grid_selector).navButtonAdd(opciones.pager_selector, {
                caption: opciones.EliminarCaption,
                title: opciones.EliminarTitle,
                buttonicon: 'ace-icon fa fa-trash-o red',
                position: 'thrid',
                onClickButton: function () {
                    if (rowKey != null) {
                        if (opciones.EliminarCommand != null && typeof (opciones.EliminarCommand) == "function")
                            opciones.EliminarCommand(rowKey);
                        else
                            Eliminar(rowKey);

                        //$("#" + opciones.dialogDelete).dialog("open");
                    } else {
                        webApp.showMessageDialog(opciones.seleccioneRegistro);
                    }
                }
            });
        }

        if (opciones.CustomButtons) {
            $.each(opciones.CustomButtons, function (index, botonNuevo) {
                $(opciones.grid_selector).navButtonAdd(opciones.pager_selector, {
                    caption: botonNuevo.Caption,
                    title: botonNuevo.Title,
                    buttonicon: botonNuevo.ButtonIcon ? botonNuevo.ButtonIcon : 'ace-icon fa fa-search-plus grey',
                    position: botonNuevo.Position ? botonNuevo.Position : 'fourth',
                    onClickButton: function () {
                        if (botonNuevo.ClickFunction != null && typeof (botonNuevo.ClickFunction) == "function")
                            botonNuevo.ClickFunction();
                    }
                });
            });
        }
    };

    var exportarExcel = function (urlExportar) {
        webApp.showLoading();

        var iframe_ = document.createElement("iframe");
        iframe_.style.display = "none";
        iframe_.setAttribute("src", urlExportar);
        iframe_.frameBorder = 0;

        if (navigator.userAgent.indexOf("MSIE") > -1 && !window.opera) {
            // Si es Internet Explorer
            iframe_.onreadystatechange = function () {
                switch (this.readyState) {
                    case "loading":
                        webApp.showLoading();
                        break;
                    case "complete":
                    case "interactive":
                    case "uninitialized":
                        webApp.hideLoading();
                        getCookie("DescargaCompleta");
                        break;
                    default:
                        webApp.hideLoading();
                        delCookie("DescargaCompleta");
                        break;
                }
            };
        } else {
            // Si es Firefox o Chrome
            document.body.appendChild(iframe_);

            var _timer = setInterval(function () {
                var success = getCookie("DescargaCompleta");
                if (success === "1") {
                    clearInterval(_timer);
                    webApp.hideLoading();
                    delCookie("DescargaCompleta");
                }
            }, 1000);
            return;
        }
        document.body.appendChild(iframe_);
    };

    var disableAllFormElements = function (formId) {
        $('#' + formId).find('input, textarea, button, select').attr('disabled', 'disabled');
    };

    var mayuscula = function (e, elemento) {
        elemento.value = elemento.value.toUpperCase();
    };

    var inicializarFileUpload = function (id) {
        var itemTemplate = '<div id="' + id + '_SWFUpload_0_0" class="uploadify-queue-item">\
            <span class="ace-file-name" ><i class=" ace-icon fa fa-upload"></i> Sin Archivo ...</span>\
            <div class="uploadify-progress">\
                <div class="uploadify-progress-bar"><!--Progress Bar--></div>\
            </div>\
        </div>';
        $('#' + id + '-queue').html('');
        $('#' + id + '-queue').append(itemTemplate);
    }

    var sumaFecha = function (d, fecha) {
        var Fecha = new Date();
        var sFecha = fecha || (Fecha.getDate() + "-" + (Fecha.getMonth() + 1) + "-" + Fecha.getFullYear());
        var sep = sFecha.indexOf('/') != -1 ? '/' : '-';
        var aFecha = sFecha.split(sep);
        var fecha = aFecha[2] + '/' + aFecha[1] + '/' + aFecha[0];
        fecha = new Date(fecha);
        fecha.setDate(fecha.getDate() + parseInt(d));
        var anno = fecha.getFullYear();
        var mes = fecha.getMonth() + 1;
        var dia = fecha.getDate();
        mes = (mes < 10) ? ("0" + mes) : mes;
        dia = (dia < 10) ? ("0" + dia) : dia;
        var fechaFinal = dia + sep + mes + sep + anno;
        return (fechaFinal);
    };

    var number_format = function (number, decimals, dec_point, thousands_sep) {
        // *     example 1: number_format(1234.56);
        // *     returns 1: '1,235'
        // *     example 2: number_format(1234.56, 2, ',', ' ');
        // *     returns 2: '1 234,56'
        // *     example 3: number_format(1234.5678, 2, '.', '');
        // *     returns 3: '1234.57'
        // *     example 4: number_format(67, 2, ',', '.');
        // *     returns 4: '67,00'
        // *     example 5: number_format(1000);
        // *     returns 5: '1,000'
        // *     example 6: number_format(67.311, 2);
        // *     returns 6: '67.31'
        // *     example 7: number_format(1000.55, 1);
        // *     returns 7: '1,000.6'
        // *     example 8: number_format(67000, 5, ',', '.');
        // *     returns 8: '67.000,00000'
        // *     example 9: number_format(0.9, 0);
        // *     returns 9: '1'
        // *    example 10: number_format('1.20', 2);
        // *    returns 10: '1.20'
        // *    example 11: number_format('1.20', 4);
        // *    returns 11: '1.2000'
        // *    example 12: number_format('1.2000', 3);
        // *    returns 12: '1.200'
        var n = !isFinite(+number) ? 0 : +number,
            prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
            sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
            dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
            toFixedFix = function (n, prec) {
                // Fix for IE parseFloat(0.55).toFixed(0) = 0;
                var k = Math.pow(10, prec);
                return Math.round(n * k) / k;
            },
            s = (prec ? toFixedFix(n, prec) : Math.round(n)).toString().split('.');
        if (s[0].length > 3) {
            s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
        }
        if ((s[1] || '').length < prec) {
            s[1] = s[1] || '';
            s[1] += new Array(prec - s[1].length + 1).join('0');
        }
        return s.join(dec);
    };

    var validarDecimal = function (evt) {
        var keyPressed = (evt.which) ? evt.which : event.keyCode
        return !((keyPressed != 13) && (keyPressed != 46) && (keyPressed < 48 || keyPressed > 57));
    };

    var validarNumero = function (evt) {
        var keyPressed = (evt.which) ? evt.which : event.keyCode
        return !((keyPressed != 13) && (keyPressed < 48 || keyPressed > 57));
    };

    var validarLetrasEspacio = function (identificadores) {
        $.each(identificadores, function (index, item) {
            $('#' + item).validCampoFranz(' abcdefghijklmnñopqrstuvwxyzáéíóú_ [\t]');
        });
    };
    var validarCorreos = function (identificadores) {
        $.each(identificadores, function (index, item) {
            $('#' + item).validCampoFranz('abcdefghijklmnñopqrstuvwxyz@_1234567890. [\t]');
        });
    };
    var validarAlfanumerico = function (identificadores) {
        $.each(identificadores, function (index, item) {
            $('#' + item).validCampoFranz(' abcdefghijklmnñopqrstuvwxyzáéíóú1234567890_° [\t]');
        });
    };

    var validarAlfanumericoGuion = function (identificadores) {
        $.each(identificadores, function (index, item) {
            $('#' + item).validCampoFranz(' abcdefghijklmnñopqrstuvwxyzáéíóú1234567890_- [\t]');
        });
    };

    var validarNumerico = function (identificadores) {
        $.each(identificadores, function (index, item) {
            $('#' + item).validCampoFranz('1234567890 [\t]');
        });
    };
    var validarNumericoTelefono = function (identificadores) {
        $.each(identificadores, function (index, item) {
            $('#' + item).validCampoFranz(' 1234567890()+- [\t]');
        });
    };
    var validarCantidadAmarillaMax = function (identificadores, cantidad) {
        return (identificadores > cantidad) ? true : false;
    };

    var validarBisiesto = function (anio) {
        return ((anio % 4 == 0 && anio % 100 != 0) || anio % 400 == 0) ? true : false;
    };
    var validarEspacioBlancoIzquierda = function (indentificadores) {
        $.each(indentificadores, function (index, item) {
            $('#' + item).focusout(function () {
                let text_ = this.value.replace(/^\s*|\s*$/g, '');
                $('#' + item).val(text_);
            });
        });
    };

    var validarFormatoFecha = function (campo) {
        let RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
        if ((campo.match(RegExPattern)) && (campo != ''))
            return true;
        else
            return false;
    };

    var validarFechaExiste = function (fecha) {
        let fechaf1 = fecha.split("-");
        let fechaf2 = fecha.split("/");
        let fechaSplit = null;
        if (fechaf1 != null)
            fechaSplit = fechaf1;
        else
            fechaSplit = fechaf2;

        let day = fechaSplit[0];
        let month = fechaSplit[1];
        let year = fechaSplit[2];
        let date = new Date(year, month, '0');
        if ((day - 0) > (date.getDate() - 0))
            return false
        else
            return true
    };

    var validarFechaMayorActual = function (fecha) {
        let fechaf1 = fecha.split("-");
        let fechaf2 = fecha.split("/");
        let fechaSplit = null;
        let x = new Date();
        if (fechaf1 != null)
            fechaSplit = fechaf1;
        else
            fechaSplit = fechaf2;

        x.setFullYear(fechaSplit[2], fechaSplit[1], fechaSplit[0]);
        var today = new Date();
        if (x <= tod)
            return false;
        else
            return true;
    };

    var ValidarFormatoFechaDDMMYYYY = function (identificador) {
        $("#" + identificador).validCampoFranz('/^([0][1-9]|[12][0-9]|3[01])(\/|-)([0][1-9]|[1][0-2])\2(\{4})$/');
    };

    var campo_llenoAutoComplete = function () {
        $("#espacio").find(".easy-autocomplete").addClass("campo-lleno");
    }


    return {
        init: function (parametros) {
            if (parametros) {
                _mensajeEspera = parametros.mensajeEspera;
                _tituloPopupMensaje = parametros.tituloPopupMensaje;
                _tituloPopupComfirmacion = parametros.tituloPopupComfirmacion;
                _tituloEliminacionPopupMensaje = parametros.tituloEliminacionPopupMensaje;
                _mensajePopupConfirmacion = parametros.mensajePopupConfirmacion;
                _mensajePopupEliminacionConfirmacion = parametros.mensajePopupEliminacionConfirmacion
                _btnCancelar = parametros.btnCancelar;
                _btnAceptar = parametros.btnAceptar;
                _formatoFecha = parametros.formatoFecha;
            }
            formValidBootstrap();
            createLoading();
            createMessageDialog();
            createConfirmDialog();
            createReConfirmDialog();
            createMessageDeleteDialog();
            campo_llenoAutoComplete();
        },
        getDataForm: getDataForm,
        showLoading: function () {
            $('#' + _popupEspera).modal("show");
        },
        hideLoading: function () {
            $('#' + _popupEspera).modal("hide");
        },
        showMessageDialog: function (message, fnAceptar) {
            $('#' + _popupMensaje + ' .modal-body').html(message);
            $('#' + _popupMensaje).modal('show');

            if ($.isFunction(fnAceptar)) {
                $('#' + _popupMensaje + ' .btn-aceptar').off('click');
                $('#' + _popupMensaje + ' .close').off('click');
                $('#' + _popupMensaje + ' .btn-aceptar').on('click', fnAceptar);
                $('#' + _popupMensaje + ' .close').on('click', fnAceptar);
            }
        },
        showConfirmDialog: function (fnSuccess, message, fnCancel) {
            var popup = $('#' + _popupConfirmacion);
            popup.modal('show');
            var btnSuccess = $(popup).find('.btn-primary');
            var btnCancel = $(popup).find('.btn-danger');

            btnSuccess.off('click');
            if ($.isFunction(fnSuccess)) {
                btnSuccess.on('click', function () { fnSuccess(); })
            }

            btnCancel.off('click');
            if ($.isFunction(fnCancel)) {
                btnCancel.on('click', function () { fnCancel(); })
            }

            if (message != null && message != '') {
                $('#' + _popupConfirmacion + ' .modal-body p').html(message);
            } else {
                $('#' + _popupConfirmacion + ' .modal-body p').html(_mensajePopupConfirmacion);
            }
        },
        showReConfirmDialog: function (fnSuccess, message, fnCancel) {
            var popup = $('#' + _popupReConfirmacion);
            var btnSuccess = $(popup).find('.btn-primary');
            var btnCancel = $(popup).find('.btn-danger');

            btnSuccess.off('click');
            if ($.isFunction(fnSuccess)) {
                btnSuccess.on('click', function () { fnSuccess(); })
            }

            btnCancel.off('click');
            if ($.isFunction(fnCancel)) {
                btnCancel.on('click', function () { fnCancel(); })
            }

            if (message != null && message != '') {
                $('#' + _popupReConfirmacion + ' .modal-body p').html(message);
            } else {
                $('#' + _popupReConfirmacion + ' .modal-body p').html(_mensajePopupConfirmacion);
            }

            popup.modal('show');
        },
        showReDeleteConfirmDialog: function (fnSuccess, message, fnCancel) {
            var popup = $('#' + _popupReConfirmacion);
            var btnSuccess = $(popup).find('.btn-primary');
            var btnCancel = $(popup).find('.btn-danger');

            btnSuccess.off('click');
            if ($.isFunction(fnSuccess)) {
                btnSuccess.on('click', function () { fnSuccess(); })
            }

            btnCancel.off('click');
            if ($.isFunction(fnCancel)) {
                btnCancel.on('click', function () { fnCancel(); })
            }

            if (message != null && message != '') {
                $('#' + _popupReConfirmacion + ' .modal-body p').html(message);
            } else {
                $('#' + _popupReConfirmacion + ' .modal-body p').html(_mensajePopupEliminacionConfirmacion);
            }

            popup.modal('show');
        },
        showConfirmResumeDialog: function (fnSuccess, message, fnCancel) {
            var popup = $('#' + _popupConfirmacion);
            popup.modal('show');
            var btnSuccess = $(popup).find('.btn-primary');
            var btnCancel = $(popup).find('.btn-danger');

            btnSuccess.off('click');
            if ($.isFunction(fnSuccess)) {
                btnSuccess.on('click', function () { fnSuccess(); })
            }

            btnCancel.off('click');
            if ($.isFunction(fnCancel)) {
                btnCancel.on('click', function () { fnCancel(); })
            }

            if (message != null && message != '') {
                $('#' + _popupConfirmacion + ' .modal-body p').html(message);
            } else {
                $('#' + _popupConfirmacion + ' .modal-body p').text(_mensajePopupConfirmacion);
            }
        },
        showDeleteConfirmDialog: function (fnSuccess, message, fnCancel) {
            var popup = $('#' + _popupEliminacionMensaje);
            var btnSuccess = $(popup).find('.btn-primary');
            var btnCancel = $(popup).find('.btn-danger');

            btnSuccess.off('click');
            if ($.isFunction(fnSuccess)) {
                btnSuccess.on('click', function () { fnSuccess(); })
            }

            btnCancel.off('click');
            if ($.isFunction(fnCancel)) {
                btnCancel.on('click', function () { fnCancel(); })
            }

            if (message != null && message != '') {
                $('#' + _popupEliminacionMensaje + ' .modal-body p').text(message);
            } else {
                $('#' + _popupEliminacionMensaje + ' .modal-body p').text(_mensajePopupEliminacionConfirmacion);
            }

            popup.modal('show');
        },
        formatResponse: function (respuesta, contenedor) {
            var estado = "";
            if (respuesta.Success) {
                if (!respuesta.Warning) {
                    estado = "alert-success";
                }
            } else {
                estado = "alert-danger";
            }
            $("#" + contenedor).append("<div class='alert " + estado + "'>" + respuesta.Message + "</div>");
        },
        clearResponse: function (contenedor) {
            $("#" + contenedor).html('');
        },
        clearForm: function (form) {
            $(".form-group").removeClass('has-error');
            $(".help-block").remove();
            $('#' + form).find('[name]').each(function (index, value) {
                var that = $(this);
                var name = that.attr('name');
                var value = that.val();

                if (that.attr('type') === 'radio') {
                    if (that.is(':checked')) {
                        that.attr('checked', false)
                    }
                } else if (that.attr('type') === 'checkbox') {
                    if (that.is(':checked')) {
                        that.attr('checked', false)
                    }
                } else {
                    that.val('');
                }
            });
        },

        //JFS
        clearFormError: function () {
            $(".form-group").removeClass('has-error');
            $(".help-block").remove();
        },

        clearFormDatos: function (form) {
            $(".form-group").removeClass('has-error');
            $(".help-block").remove();
            $('#' + form).find('[name]').each(function (index, value) {
                var that = $(this);
                var name = that.attr('name');
                var value = that.val();

                if (that.attr('type') === 'checkbox') {
                    if (that.is(':checked')) {
                        that.attr('checked', false)
                    }
                } else {
                    that.val('');
                }
            });
        },
        GetCorrelativo: function (item, correlativo, length) {
            var output = correlativo + '';
            while (output.length < length) {
                output = '0' + output;
            }
            return $('#' + item).val(output);
        },
        GetCorrelativoItem: function (correlativo, length) {
            var output = correlativo + '';
            while (output.length < length) {
                output = '0' + output;
            }
            return output;
        },
        Ajax: function (opciones, successCallback, failureCallback, errorCallback) {
            if (opciones.url == null)
                opciones.url = "";

            if (opciones.cache == null)
                opciones.cache = false;

            if (opciones.parametros == null)
                opciones.parametros = {};

            if (opciones.async == null)
                opciones.async = true;

            if (opciones.datatype == null)
                opciones.datatype = "json";

            if (opciones.contentType == null)
                opciones.contentType = "application/json; charset=utf-8";

            if (opciones.type == null)
                opciones.type = "POST";

            checkSessionUser.checkSession(function () {
                $.ajax({
                    type: opciones.type,
                    url: opciones.url,
                    cache: opciones.cache,
                    async: opciones.async,
                    data: opciones.parametros,
                    success: function (response) {
                        if (successCallback != null && typeof (successCallback) == "function")
                            successCallback(response);
                    },
                    failure: function (msg) {
                        if (failureCallback != null && typeof (failureCallback) == "function")
                            failureCallback(msg);
                    },
                    error: function (xhr, status, error) {
                        if (errorCallback != null && typeof (errorCallback) == "function")
                            errorCallback(xhr);
                    }
                });
            });
        },

        ExportarExcel: function (url) {
            exportarExcel(url);
        },
        InicializarValidacion: function (formName, rules, messages, ignore) {
            if (ignore == null) ignore = ".ignore";

            $('#' + formName).validate({
                errorElement: 'div',
                errorClass: 'help-block',
                focusInvalid: false,
                ignore: ignore,
                rules: rules,
                messages: messages,

                highlight: function (e) {
                    $(e).closest('.form-group').removeClass('has-info').addClass('has-error');
                },

                success: function (e) {
                    $(e).closest('.form-group').removeClass('has-error');//.addClass('has-info');
                    $(e).remove();
                },

                errorPlacement: function (error, element) {
                    if (element.is('input[type=checkbox]') || element.is('input[type=radio]')) {
                        var controls = element.closest('div[class*="col-"]');
                        if (controls.find(':checkbox,:radio').length > 1) controls.append(error);
                        else error.insertAfter(element.nextAll('.lbl:eq(0)').eq(0));
                    }
                    else if (element.is('.select2')) {
                        error.insertAfter(element.siblings('[class*="select2-container"]:eq(0)'));
                    }
                    else if (element.is('.chosen-select')) {
                        error.insertAfter(element.siblings('[class*="chosen-container"]:eq(0)'));
                    }
                    else error.insertAfter(element.parent());
                },

                submitHandler: function (form) {
                },
                invalidHandler: function (form) {
                }
            });
        },
        guid: function () {
            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                s4() + '-' + s4() + s4() + s4();
        },
        disableAllFormElements: disableAllFormElements,
        mayuscula: mayuscula,
        sumaFecha: sumaFecha,
        inicializarFileUpload: inicializarFileUpload,
        number_format: number_format,
        createGrid: createGrid,
        validarDecimal: validarDecimal,
        validarNumero: validarNumero,
        validarLetrasEspacio: validarLetrasEspacio,
        validarAlfanumerico: validarAlfanumerico,
        validarAlfanumericoGuion: validarAlfanumericoGuion,
        validarNumerico: validarNumerico,
        validarNumericoTelefono: validarNumericoTelefono,
        validarCorreos: validarCorreos,
        validarBisiesto: validarBisiesto,
        validarEspacioBlancoIzquierda: validarEspacioBlancoIzquierda,
        ValidarFormatoFechaDDMMYYYY: ValidarFormatoFechaDDMMYYYY,
        validarFormatoFecha: validarFormatoFecha,
        validarFechaExiste: validarFechaExiste,
        validarFechaMayorActual: validarFechaMayorActual,

        //juliio cesar
        getDataVista: function (url, callbak) {
            var status = false;
            checkSessionUser.checkSession(function () {
                $.ajax({
                    type: "POST",
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success: function (data) {
                        //webApp.cargarCalendarios();
                        var options = {
                            "backdrop": "static",
                            "keyboard": false
                        };
                        $('#myModal').modal(options);
                        $('#myModalContent').html(data);
                        $('#myModal').modal('show');
                        status = true;
                        return callbak(status);
                    },
                    error: function () {
                        webApp.sweetmensaje('Error', 'Carga no obtenida', 'error');
                    }
                });
            });

        },

        getDataVistaParam: function (url, data, callbak) {
            var status = false;
            checkSessionUser.checkSession(function () {
                $.ajax({
                    type: "POST",
                    url: url,
                    data: data,
                    success: function (data) {

                        var options = {
                            "backdrop": "static",
                            "keyboard": false
                        };
                        $('#myModal').modal(options);
                        $('#myModalContent').html(data);
                        $('#myModal').modal('show');
                        //webApp.cargarCalendarios();
                        status = true;
                        return callbak(status);
                    },
                    error: function () {
                        webApp.sweetmensaje('Error', 'Carga no obtenida', 'error');
                    }
                });
            });

        },
        getDataVistaViewPartial: function (url, callbak) {
            $('[data-toggle="tooltip"]').tooltip('dispose')
            var status = false;
            checkSessionUser.checkSession(function () {
                $.ajax({
                    type: "POST",
                    url: url,
                    // contentType: "application/json; charset=utf-8",
                    datatype: "html",
                    success: function (data) {
                        $('#espacio').html('');
                        $('#espacio').html(data);
                        //  webApp.cargarCalendarios();
                        status = true;
                        return callbak(status);
                    },
                    error: function () {
                        webApp.sweetmensaje('Error', 'Carga no obtenida', 'error');
                    }
                });
            });
        },

        getDataVistaViewPartialParam: function (url, param, callbak) {
            $('[data-toggle="tooltip"]').tooltip('dispose')
            var status = false;
            $('#espacio').empty();
            checkSessionUser.checkSession(function () {
                $.ajax({
                    type: "POST",
                    url: url,
                    //contentType: "application/json; charset=utf-8",
                    datatype: "html",
                    data: param,
                    success: function (data) {
                        $('#espacio').html('');
                        $('#espacio').html(data);
                        //webApp.cargarCalendarios();
                        status = true;
                        return callbak(status);
                    },
                    error: function (error) {
                        webApp.sweetmensaje('Error', "error en ", 'error');
                    }
                });
            });
        },

        getDataVistaViewPartialModal: function (url, callbak) {
            var status = false;
            checkSessionUser.checkSession(function () {
                $.ajax({
                    type: "POST",
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success: function (data) {

                        var options = {
                            "backdrop": "static",
                            "keyboard": false
                        };
                        $('#registrar-contacto').modal(options);
                        $('#model-AtencionContacto').html(data);
                        $('#registrar-contacto').modal('show');
                        webApp.cargarCalendarios();
                        status = true;
                        return callbak(status);

                    },
                    error: function () {
                        webApp.sweetmensaje('Error', 'Carga no obtenida', 'error');
                    }
                });
            });

        },
        getDataVistaViewPartialParamModal: function (url, param, callbak) {
            var status = false;
            checkSessionUser.checkSession(function () {
                $.ajax({
                    type: "POST",
                    url: url,
                    //contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    data: param,
                    success: function (data) {

                        var options = {
                            "backdrop": "static",
                            "keyboard": false
                        };
                        $('#registrar-contacto').modal(options);
                        $('#model-AtencionContacto').html(data);
                        $('#registrar-contacto').modal('show');
                        webApp.cargarCalendarios();
                        status = true;
                        return callbak(status);

                    },
                    error: function () {
                        webApp.sweetmensaje('Error', 'Carga no obtenida', 'error');
                    }
                });
            });
        },


        resetForm: function (form) {
            $(form)[0].reset();
            $("input[name=id]").val(0);
            $.each(form.find("textarea"), function (i, e) {
                var sm = $(e).attr("data-sn")
                if (sm === "yes") {
                    $(e).summernote('code', '');
                }
            });
            $("div.has-error").removeClass("has-error");
        },

        //Jorge Castro
        sweetmensaje: function (stitulo, sMensaje, type) {
            //success , warning ,error
            if (sMensaje == null || sMensaje.trim() == 0) {
                sMensaje = "Sin Mensaje";
            }
            if (stitulo == null || stitulo.trim() == 0) {
                stitulo = "Sin Título";
            }
            if (type == null || type.trim() == 0) {
                type = "warning";
            }
            swal({
                title: stitulo,
                text: sMensaje,
                timer: 5000,
                type: type
            })
        },
        sweetresponse: function (stitulo, response) {
            //success , warning ,error
            if (stitulo == null || stitulo.trim() == 0) {
                stitulo = "Sin Título";
            }
            if (response.mensaje == null || response.mensaje.trim() == 0) {
                response.mensaje = "Sin Mensaje";
            }
            if (response.type == null || response.type.trim() == 0) {
                response.type = "warning";
            }
            swal(stitulo, response.mensaje, response.type);
        },
        sweetresponseOk: function (stitulo, response, url) {
            //success , warning ,error
            if (stitulo == null || stitulo.trim() == 0) {
                stitulo = "Sin Título";
            }
            if (response.mensaje == null || response.mensaje.trim() == 0) {
                response.mensaje = "Sin Mensaje";
            }
            if (response.type == null || response.type.trim() == 0) {
                response.type = "warning";
            }

            checkSessionUser.checkSession(function () {
                swal({
                    title: stitulo,
                    text: response.mensaje,
                    showConfirmButton: false,
                    timer: 2000,
                    type: response.type
                });
                setTimeout(function () {
                    $.ajax({
                        type: "POST",
                        url: url,
                        contentType: "application/json; charset=utf-8",
                        datatype: "json",
                        success: function (data) {
                            $('#espacio').html(data);
                        },
                        error: function (error) {
                            webApp.sweetmensaje('Error', error, 'error');
                        }
                    });
                }, 2000);

            });

        },
        sweetresponseOkHTML: function (stitulo, response, url) {
            //success , warning ,error
            if (stitulo == null || stitulo.trim() == 0) {
                stitulo = "Sin Título";
            }
            if (response.mensaje == null || response.mensaje.trim() == 0) {
                response.mensaje = "Sin Mensaje";
            }
            if (response.type == null || response.type.trim() == 0) {
                response.type = "warning";
            }
            if (response.confirmButtonText == null || response.confirmButtonText == 0) {
                response.confirmButtonText = "OK";
            }
            checkSessionUser.checkSession(function () {
                swal({
                    html: true,
                    title: stitulo,
                    text: response.mensaje,
                    type: response.type,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: response.confirmButtonText
                }, function () {
                    if (url != null || url != '') {
                        window.location.href = url;
                    }

                });
            });

        },
        sweetajax: function (mensajes, opciones, successCallback, failureCallback, errorCallback) {
            //CTRL + M + H
            if (mensajes.title == null)
                mensajes.title = "Sin Título";

            if (mensajes.text == null)
                mensajes.text = "Sin Texto.";

            if (mensajes.cancelar == null)
                mensajes.cancelar = "Se canceló el proceso.";

            if (mensajes.aceptar == null)
                mensajes.aceptar = "Se aceptó el proceso.";

            if (mensajes.type == null)
                mensajes.type = "warning";

            if (mensajes.showCancelButton == null)
                mensajes.showCancelButton = true;

            if (mensajes.showCancelButton == null)
                mensajes.confirmButtonColor = "#DD6B55";

            if (mensajes.confirmButtonText == null)
                mensajes.confirmButtonText = "Aceptar";

            if (mensajes.cancelButtonText == null)
                mensajes.cancelButtonText = "Cancelar";

            if (mensajes.closeOnConfirm == null)
                mensajes.closeOnConfirm = true;

            if (mensajes.closeOnCancel == null)
                mensajes.closeOnCancel = false;


            if (opciones.url == null)
                opciones.url = "";

            if (opciones.cache == null)
                opciones.cache = false;

            if (opciones.parametros == null)
                opciones.parametros = {};

            if (opciones.async == null)
                opciones.async = true;

            if (opciones.datatype == null)
                opciones.datatype = "json";

            if (opciones.contentType == null)
                opciones.contentType = "application/json; charset=utf-8";

            if (opciones.type == null)
                opciones.type = "POST";

            checkSessionUser.checkSession(function () {
                swal(mensajes, function (isConfirm) {

                    if (isConfirm) {
                        $.ajax({
                            type: opciones.type,
                            url: opciones.url,
                            cache: opciones.cache,
                            async: opciones.async,
                            data: opciones.parametros,
                            success: function (response) {
                                if (successCallback != null && typeof (successCallback) == "function") {
                                    successCallback(response);
                                }
                                else {
                                    webApp.sweetresponse(mensajes.title, response)
                                }
                            },
                            failure: function (msg) {
                                if (failureCallback != null && typeof (failureCallback) == "function")
                                    failureCallback(msg);
                            },
                            error: function (xhr, status, error) {
                                if (errorCallback != null && typeof (errorCallback) == "function")
                                    errorCallback(xhr);
                            }
                        });
                    }
                    else {
                        swal({
                            title: 'Cancelado',
                            timer: 10
                        })
                    }
                });
            })


        },
        Json: function (url, successCallback) {
            checkSessionUser.checkSession(function () {
                $.getJSON(url, function (data) {
                    if (successCallback != null && typeof (successCallback) == "function")
                        successCallback(data);
                });
            });
        },
        JsonParam: function (url, param, callback) {
            checkSessionUser.checkSession(function () {
                $.post(url, param, function (data) {
                    if (callback != null && typeof (callback) == "function")
                        return callback(data);
                });
            });
        },
        getDataListado: function (url, div, callback) {
            var status = false;
            $.ajax({
                type: "POST",
                url: url,
                contentType: "application/json; charset=utf-8",
                datatype: "json",
                success: function (data) {
                    $('#' + div).html(data);
                    status = true;
                    if (callback != null && typeof (callback) == "function")
                        return callback(data);
                },
                error: function () {
                    webApp.sweetmensaje('Error', 'Carga no obtenida', 'error');
                }
            });
        },
        getDataListadoParam: function (url, div, param, callback) {
            var status = false;
            $.ajax({
                type: "POST",
                url: url,
                data: param,
                success: function (data) {
                    $('#' + div).html('');
                    $('#' + div).html(data);
                    status = true;
                    if (callback != null && typeof (callback) == "function")
                        return callback(data);
                },
                error: function () {
                    webApp.sweetmensaje('Error', 'Carga no obtenida', 'error');
                }
            });
        },
        getDataVistaSeguridad: function (url, callbak) {
            var status = false;
            $('#myModalContentSeg').html('');
            checkSessionUser.checkSession(function () {
                $.ajax({
                    type: "POST",
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success: function (data) {
                        webApp.cargarCalendarios();
                        var options = {
                            "backdrop": "static",
                            "keyboard": false
                        };
                        $('#myModalSeg').modal(options);
                        $('#myModalContentSeg').html(data);
                        $('#myModalSeg').modal('show');
                        status = true;
                        return callbak(status);
                    },
                    error: function () {
                        webApp.sweetmensaje('Error', 'Carga no obtenida', 'error');
                    }
                });
            });

        },
        getDataVistaSeguridadLG: function (url, callbak) {
            var status = false;
            $('#myModalContentSeglg').html('');
            checkSessionUser.checkSession(function () {
                $.ajax({
                    type: "POST",
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    success: function (data) {
                        webApp.cargarCalendarios();
                        var options = {
                            "backdrop": "static",
                            "keyboard": false
                        };
                        $('#myModalSeglg').modal(options);
                        $('#myModalContentSeglg').html(data);
                        $('#myModalSeglg').modal('show');
                        status = true;
                        return callbak(status);
                    },
                    error: function () {
                        webApp.sweetmensaje('Error', 'Carga no obtenida', 'error');
                    }
                });
            });

        },
        deleteData: function (url, id, nombre, tb, callbak) {
            checkSessionUser.checkSession(function () {
                swal({
                    title: 'Estas seguro?',
                    text: "vas a eliminar " + nombre,
                    type: 'question',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Sí, eliminar ahora!'
                }).then(function () {
                    getData({ url: url, data: { id: id } }, function (response) {
                        if (response.status === "SUCCESS") {
                            swal('Eliminado!', response.message, 'success');
                            $(tb).dataTable();
                            if (callbak !== undefined) {
                                return callbak(response);
                            }
                        }
                    });
                });
            });

        },
        //julio cesar pillaca
        validateForm: function (form) {
            var error = 0, r = true;
            $(form).find("input,textarea,select").each(function (i, elem) {
                var texto = $(this).siblings("label").eq(0).text();
                var idNuevo = $(elem).attr("id");
                var obligatorio = texto + " Obligatorio";
                var html = '<lable class="label text-danger">' + obligatorio + '</label>';

                if ($(elem).val() == '') {
                    if ($(elem).attr("required")) {
                        if ($(elem).hasClass("autocompli")) {
                            $(this).parents("div").eq(1).removeClass("has-default");
                            $(this).parents("div").eq(1).addClass("has-danger");
                            //var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            //if (tamaño.length < 1) {
                            //    $(this).parents("div").eq(0).append(html);
                            //}

                            error++;
                        } else {
                            $(this).parents("div").eq(0).removeClass("has-default");
                            $(this).parents("div").eq(0).addClass("has-danger");
                            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            if (tamaño.length < 1) {
                                $(this).parents("div").eq(0).append(html);
                            }
                            error++;
                        }
                    }

                } else if ($(elem).val() == 0) {
                    if ($(elem).attr("required")) {
                        $(this).parents("div").eq(0).removeClass("has-default");
                        $(this).parents("div").eq(0).addClass("has-danger");
                        var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                        var obligatorio = "Seleccione " + texto;
                        html = '<lable class="label text-danger">' + obligatorio + '</label>';
                        if (tamaño.length < 1) {
                            $(this).parents("div").eq(0).append(html);
                        }
                        error++;
                    }
                } else if ($(elem).val() != '') {
                    var type = $(this).attr("type");
                    var value = $(this).val();
                    var formato = "Formarto Incorrecto";
                    var html = '<lable class="label text-danger">' + formato + '</label>';
                    $(this).parents("div").eq(0).removeClass("has-danger");
                    $(this).parents("div").eq(0).addClass("has-default");

                    if ($(this).hasClass("autocompli")) {
                        $(this).parents("div").eq(1).removeClass("has-danger");
                        $(this).parents("div").eq(1).addClass("has-default");
                    }
                    if (type == "date") {
                        if ($(this).val() != "") {
                            if (/^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/.test(value)) {
                            } else {
                                $(this).parents("div").eq(0).removeClass("has-default");
                                $(this).parents("div").eq(0).addClass("has-danger");
                                var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                                if (tamaño.length < 1) {
                                    $(this).parents("div").eq(0).append(html);
                                }
                                error++
                            }
                        }
                    }

                    if ($(this).hasClass("fecha")) {
                        if ($(elem).val() != "") {
                            if (!/^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/.test(value)) {
                                $(elem).parents("div").eq(0).removeClass("has-default");
                                $(elem).parents("div").eq(0).addClass("has-danger");
                                var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                                if (tamaño.length < 1) {
                                    $(elem).parents("div").eq(0).append(htmlFormato);
                                }
                                error++
                            }
                        }
                    }
                    if ($(this).attr("min")) {
                        var valor = parseInt($(this).attr("min"));
                        if ($(this).val().length < valor) {
                            var texto = valor + " Caracteres mínimo a Ingresar";
                            var html = '<lable class="label text-danger">' + texto + '</label>';
                            $(this).parents("div").eq(0).removeClass("has-default");
                            $(this).parents("div").eq(0).addClass("has-danger");
                            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            if (tamaño.length < 1) {
                                $(this).parents("div").eq(0).append(html);
                            }
                            error++
                        }
                    }
                    if ($(this).hasClass("correo")) {
                        if ($(this).val() != "") {
                            if (/[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}/.test(value)) {
                            } else {
                                $(this).parents("div").eq(0).removeClass("has-default");
                                $(this).parents("div").eq(0).addClass("has-danger");
                                var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                                if (tamaño.length < 1) {
                                    $(this).parents("div").eq(0).append(html);
                                }
                                error++
                            }
                        }
                    }
                    if (type == "email") {
                        if ($(this).val() != "") {
                            if (/[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}/.test(value)) {
                            } else {
                                $(this).parents("div").eq(0).removeClass("has-default");
                                $(this).parents("div").eq(0).addClass("has-danger");
                                var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                                if (tamaño.length < 1) {
                                    $(this).parents("div").eq(0).append(html);
                                }
                                error++
                            }
                        }
                    }
                    if ($(this).hasClass("dni")) {
                        if ($(this).val().length == 8) {
                        } else {
                            $(this).parents("div").eq(0).removeClass("has-default");
                            $(this).parents("div").eq(0).addClass("has-danger");
                            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            if (tamaño.length < 1) {
                                $(this).parents("div").eq(0).append(html);
                            }
                            error++
                        }
                    }
                    if ($(this).hasClass("telefono")) {
                        if (/^\d{3}-\d{3}-\d{3}$/.test(value) || /^\d{9}$/.test(value) || /^\(\d{2}\)\d{7}$/.test(value) || /^\(\d{2}\)\s\d{7}$/.test(value) || /^\+\d{2,3}\s\d{9}$/.test(value)) {
                        } else {
                            $(this).parents("div").eq(0).removeClass("has-default");
                            $(this).parents("div").eq(0).addClass("has-danger");
                            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            if (tamaño.length < 1) {
                                $(this).parents("div").eq(0).append(html);
                            }
                            error++
                        }
                    }
                    if (type == "tel") {
                        if (/^\d{3}-\d{3}-\d{3}$/.test(value) || /^\d{9}$/.test(value) || /^\(\d{2}\)\d{7}$/.test(value) || /^\(\d{2}\)\s\d{7}$/.test(value) || /^\+\d{2,3}\s\d{9}$/.test(value)) {
                        } else {
                            $(this).parents("div").eq(0).removeClass("has-default");
                            $(this).parents("div").eq(0).addClass("has-danger");
                            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            if (tamaño.length < 1) {
                                $(this).parents("div").eq(0).append(html);
                            }
                            error++
                        }
                    }
                    if ($(this).hasClass("ip")) {
                        if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(value)) {
                        } else {
                            $(this).parents("div").eq(0).removeClass("has-default");
                            $(this).parents("div").eq(0).addClass("has-danger");
                            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            if (tamaño.length < 1) {
                                $(this).parents("div").eq(0).append(html);
                            }
                            error++
                        }
                    }
                }
            });
            if (error > 0) {
                r = false;
            }
            return r;
        },
        validationForm: function (form, fields, btn) {
            $("form#" + form)
                .bootstrapValidator({
                    message: 'This value is not valid',
                    feedbackIcons: {
                        valid: 'glyphicon glyphicon-ok',
                        invalid: 'glyphicon glyphicon-remove',
                        validating: 'glyphicon glyphicon-refresh'
                    },
                    fields: fields
                })
                .on('error.field.bv', function (e, data) {
                    if (data.field == 'postalCode') {
                        // The postal code is not valid

                        $('.btn-Registrar-Prospecto').prop('disabled', true);
                    }
                })
                .on('success.field.bv', function (e, data) {
                    if (data.field == 'postalCode') {
                        // The postal code is valid
                        $(btn).prop('disabled', false).removeClass('btn-danger btn-warning').addClass('btn-danger');
                    }
                });
        },
        validateInputsSelect: function (elem) {
            var error = 0;
            var r = true;
            var texto = $(elem).siblings("label").eq(0).text();
            var idNuevo = $(elem).attr("id");
            var obligatorio = texto + " Obligatorio";
            var html = '<lable class="label text-danger">' + obligatorio + '</label>';
            var formato = "Formarto Incorrecto";
            var htmlFormato = '<lable class="label text-danger">' + formato + '</label>';

            if ($(elem).val() == "" || $(elem).val() == "0") {
                if ($(elem).attr("required")) {
                    if ($(elem).val() == '') {
                        var value = $(elem).val();
                        
                        if ($(elem).attr("correo") == "correo") {
                            if (value != "") {
                                if (/[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}/.test(value)) {
                                } else {
                                    $(elem).parents("div").eq(0).removeClass("has-default");
                                    $(elem).parents("div").eq(0).addClass("has-danger");
                                    var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                                    if (tamaño.length < 1) {
                                        $(this).parents("div").eq(0).append(htmlFormato);
                                    }
                                    error++
                                    return;
                                }
                            } else {
                                $(elem).parents("div").eq(0).removeClass("has-danger");
                                $(elem).parents("div").eq(0).addClass("has-default");
                            }
                        }

                        else if ($(elem).attr("telefono") == "telefono") {
                            if (value != "") {
                                if (/^\d{3}-\d{3}-\d{3}$/.test(value) || /^\d{9}$/.test(value) || /^\(\d{2}\)\d{7}$/.test(value) || /^\(\d{2}\)\s\d{7}$/.test(value) || /^\+\d{2,3}\s\d{9}$/.test(value)) {
                                } else {
                                    $(elem).parents("div").eq(0).removeClass("has-default");
                                    $(elem).parents("div").eq(0).addClass("has-danger");
                                    var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                                    if (tamaño.length < 1) {
                                        $(elem).parents("div").eq(0).append(htmlFormato);
                                    }
                                    error++
                                    return;
                                }
                            } else {
                                $(elem).parents("div").eq(0).removeClass("has-danger");
                                $(elem).parents("div").eq(0).addClass("has-default");
                            }
                        }
                        
                        else if ($(elem).hasClass("fecha")) {
                                if ($(elem).val() == "") {
                                    if (!/^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/.test(value)) {
                                        $(elem).parents("div").eq(0).removeClass("has-default");
                                        $(elem).parents("div").eq(0).addClass("has-danger");
                                        var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                                        if (tamaño.length < 2) {
                                            $(elem).parents("div").eq(0).append(htmlFormato);
                                        }
                                        error++
                                    }
                               }
                            
                        }
                        else if ($("#espacio " + elem).hasClass("autocompli")) {
                            $(elem).parents("div").eq(1).removeClass("has-default");
                            $(elem).parents("div").eq(1).addClass("has-danger");
                            //var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            //if (tamaño.length < 1) {
                            //    $(elem).parents("div").eq(0).append(html);
                            //}
                            error++;
                        }

                        else {
                            $(elem).parents("div").eq(0).removeClass("has-default");
                            $(elem).parents("div").eq(0).addClass("has-danger");
                            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            if (tamaño.length < 1) {
                                $(elem).parents("div").eq(0).append(html);
                            }
                            error++;
                        }
                    }
                    else if ($(elem).val() == 0) {
                        $(elem).parents("div").eq(0).removeClass("has-default");
                        $(elem).parents("div").eq(0).addClass("has-danger");
                        var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                        if (tamaño.length < 1) {
                            $(elem).parents("div").eq(0).append(html);
                        }
                        error++;
                    }
                } else {
                    var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                    if (tamaño.length > 0) {
                        $("#espacio #" + idNuevo).siblings("label").nextUntil().remove();
                    }
                    $(elem).parents().eq(0).removeClass("has-danger");
                }
            }
            else {
                var type = $(elem).attr("type");
                var value = $(elem).val();
                $(elem).parents("div").eq(0).removeClass("has-danger");
                $(elem).parents("div").eq(0).addClass("has-default");
                if (type == "email") {
                    if (/[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}/.test(value)) {
                    } else {
                        $(elem).parents("div").eq(0).removeClass("has-default");
                        $(elem).parents("div").eq(0).addClass("has-danger");
                        var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                        if (tamaño.length < 1) {
                            $(elem).parents("div").eq(0).append(htmlFormato);
                        }
                        error++
                    }
                }
                if ($(elem).hasClass("datepicker")) {
                    if ($(elem).val() != "") {
                        if (!/^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/.test(value)) {
                            $(elem).parents("div").eq(0).removeClass("has-default");
                            $(elem).parents("div").eq(0).addClass("has-danger");
                            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            if (tamaño.length < 1) {
                                $(elem).parents("div").eq(0).append(htmlFormato);
                            }
                            error++
                        }
                    }
                }
                
                if ($(this).hasClass("fecha")) {
                    if ($(elem).val() != "") {
                        if (!/^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/.test(value)) {
                            $(elem).parents("div").eq(0).removeClass("has-default");
                            $(elem).parents("div").eq(0).addClass("has-danger");
                            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                            if (tamaño.length < 1) {
                                $(elem).parents("div").eq(0).append(htmlFormato);
                            }
                            error++
                        }
                    }
                }
                if ($(elem).hasClass("dni")) {
                    if ($(this).val().length == 8) {
                    } else {
                        $(elem).parents("div").eq(0).removeClass("has-default");
                        $(elem).parents("div").eq(0).addClass("has-danger");
                        var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                        if (tamaño.length < 1) {
                            $(elem).parents("div").eq(0).append(htmlFormato);
                        }
                        error++
                    }
                }
                if ($(elem).attr("min")) {
                    var valor = parseInt($(elem).attr("min"));
                    if ($(elem).val().length < valor) {
                        var texto = valor + " Caracteres mínimo a Ingresar";
                        var html = '<lable class="label text-danger">' + texto + '</label>';
                        $(elem).parents("div").eq(0).removeClass("has-default");
                        $(elem).parents("div").eq(0).addClass("has-danger");
                        var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                        if (tamaño.length < 1) {
                            $(elem).parents("div").eq(0).append(html);
                        }
                        error++
                    }
                }
                if (type == "tel") {
                    if (/^\d{3}-\d{3}-\d{3}$/.test(value) || /^\d{9}$/.test(value) || /^\(\d{2}\)\d{7}$/.test(value) || /^\(\d{2}\)\s\d{7}$/.test(value) || /^\+\d{2,3}\s\d{9}$/.test(value)) {
                    } else {
                        $(elem).parents("div").eq(0).removeClass("has-default");
                        $(elem).parents("div").eq(0).addClass("has-danger");
                        var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                        if (tamaño.length < 1) {
                            $(elem).parents("div").eq(0).append(htmlFormato);
                        }
                        error++
                    }
                }
            }

            if (error > 0) {
                r = false;
            }
            return r;
        },
        ValidadorPorElemento : function (tipo, campo) {
            var contador = 0;
            var elemCampo = $("#" + campo);
            var _tipo = $("#" + tipo).val();
            var texto = $("#" + campo).siblings("label").eq(0).text();
            var idNuevo = $("#" + campo).attr("id");
            var obligatorio = texto + " Obligatorio";
            var html = '<lable class="label text-danger">' + obligatorio + '</label>';
            var htmlF = '<lable class="label text-danger">Formato Incorrecto</label>';
            if ($(elemCampo).val() != "") {
                
                if ($(elemCampo).attr("telefono") =="telefono") {
                    if (/^\d{3}-\d{3}-\d{3}$/.test((elemCampo).val()) || /^\d{9}$/.test((elemCampo).val()) || /^\(\d{2}\)\d{7}$/.test((elemCampo).val()) || /^\(\d{2}\)\s\d{7}$/.test((elemCampo).val()) || /^\+\d{2,3}\s\d{9}$/.test((elemCampo).val())) {
                    } else {
                        $(elemCampo).parents("div").eq(0).removeClass("has-default");
                        $(elemCampo).parents("div").eq(0).addClass("has-danger");
                        var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                        if (tamaño.length < 1) {
                            $("#espacio #" + idNuevo).parents("div").eq(0).append(htmlF);
                        }
                        contador++
                    }
                }
                else if ($(elemCampo).hasClass("tarjeta")) { }
                else if ($(elemCampo).attr("correo") =="correo") {
                    if (/[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*@[a-zA-Z0-9_]+([.][a-zA-Z0-9_]+)*[.][a-zA-Z]{1,5}/.test($(elemCampo).val())) {
                    } else {
                        $(elemCampo).parents("div").eq(0).removeClass("has-default");
                        $(elemCampo).parents("div").eq(0).addClass("has-danger");
                        var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                        var tamaño2 = $("#espacio #" + idNuevo).siblings("label");
                        if (tamaño.length < 1) {
                            $("#espacio #" + idNuevo).parents("div").eq(0).append(htmlF);
                        }
                        contador++;
                    }
                }
            } else {
                $(elemCampo).parents("div").eq(0).removeClass("has-default");
                $(elemCampo).parents("div").eq(0).addClass("has-danger");
                var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                var tamaño2 = $("#espacio #" + idNuevo).siblings("label");
                if (tamaño.length < 1) {
                    $("#espacio #" + idNuevo).parents("div").eq(0).append(html);
                }
                contador++;
            }

            if (_tipo == "0") {
                $("#" + tipo).find("div").eq(0).addClass("has-danger");
                $("#" + tipo).parents("div").eq(0).removeClass("has-default");
                $("#" + tipo).parents("div").eq(0).addClass("has-danger");
                var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
                if (tamaño.length < 1) {
                    $("#" + tipo).parents("div").eq(0).append(html);
                }
                contador++;
            }
            if (contador == 0) {
                return true;
            } else {
                return false;
            }
        },
        numeroInt: function (evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
        },
        subirArchivos: function (mensajes, opciones, form, successCallback, failureCallback, errorCallback) {
            //CTRL + M + H
            if (mensajes.title == null)
                mensajes.title = "Sin Título";

            if (mensajes.text == null)
                mensajes.text = "Sin Texto.";

            if (mensajes.cancelar == null)
                mensajes.cancelar = "Se canceló el proceso.";

            if (mensajes.aceptar == null)
                mensajes.aceptar = "Se aceptó el proceso.";

            if (mensajes.type == null)
                mensajes.type = "warning";

            if (mensajes.showCancelButton == null)
                mensajes.showCancelButton = true;

            if (mensajes.showCancelButton == null)
                mensajes.confirmButtonColor = "#DD6B55";

            if (mensajes.confirmButtonText == null)
                mensajes.confirmButtonText = "Aceptar";

            if (mensajes.cancelButtonText == null)
                mensajes.cancelButtonText = "Cancelar";

            if (mensajes.closeOnConfirm == null)
                mensajes.closeOnConfirm = true;

            if (mensajes.closeOnCancel == null)
                mensajes.closeOnCancel = false;

            if (opciones.url == null)
                opciones.url = "";

            if (opciones.cache == null)
                opciones.cache = false;

            if (opciones.parametros == null)
                opciones.parametros = {};

            if (opciones.async == null)
                opciones.async = true;

            if (opciones.datatype == null)
                opciones.datatype = "json";

            if (opciones.contentType == null)
                opciones.contentType = "application/json; charset=utf-8";

            if (opciones.type == null)
                opciones.type = "POST";

            checkSessionUser.checkSession(function () {
                swal(mensajes, function (isConfirm) {
                    if (isConfirm) {
                        form.ajaxSubmit({
                            type: opciones.type,
                            url: opciones.url,
                            cache: opciones.cache,
                            async: opciones.async,
                            data: opciones.parametros,
                            success: function (response) {
                                if (successCallback != null && typeof (successCallback) == "function") {
                                    successCallback(response);
                                }
                                else {
                                    webApp.sweetresponse(mensajes.title, response)
                                }
                            },
                            failure: function (msg) {
                                if (failureCallback != null && typeof (failureCallback) == "function")
                                    failureCallback(msg);
                            },
                            error: function (xhr, status, error) {
                                if (errorCallback != null && typeof (errorCallback) == "function")
                                    errorCallback(xhr);
                            }
                        });
                    }
                    else {
                        webApp.sweetmensaje(mensajes.title, mensajes.cancelar, 'warning');
                    }
                });
            });

        },
        campoLlenoInput: function () {
            $("#espacio .lu-cal").addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
        },
        cargarCalendarios: function () {

            var hoy = new Date();
            var dd = hoy.getDate();
            var mm = hoy.getMonth() + 1; //hoy es 0!
            var yyyy = hoy.getFullYear() - 18;
            let maxDate_ = dd + '/' + mm + '/' + yyyy;

            $(".lu-cal-default").flatpickr({
                "locale": "es",
                dateFormat: "d/m/Y",
                onClose: function (selectedDates, dateStr, instance) {
                    webApp.campoLlenoInput();
                }
            });
            $(".lu-cal-time").flatpickr({
                "locale": "es",
                enableTime: true,
                dateFormat: "d/m/Y H:i",
                onClose: function (selectedDates, dateStr, instance) {
                    webApp.campoLlenoInput();
                }
            });
            $(".lu-cal-maxdate01012005").flatpickr({
                "locale": "es",
                dateFormat: "d/m/Y",
                maxDate: '01/01/2005',
                onClose: function (selectedDates, dateStr, instance) {
                    webApp.campoLlenoInput();
                }
            });
            $(".lu-cal-mayordeEdad").flatpickr({
                "locale": "es",
                dateFormat: "d/m/Y",
                maxDate: maxDate_,
                defaultDate: "01-01-" + yyyy,
                onClose: function (selectedDates, dateStr, instance) {

                }
            });
            $(".lu-cal-range").flatpickr({
                "locale": "es",
                mode: "range",
                //minDate: "today",
                dateFormat: "d/m/Y" + " A " + "d/m/Y",

            });
            $(".lu-cal-time-local").flatpickr({
                enableTime: true,
                noCalendar: true,
                dateFormat: "H:i",
            });
        },

        bloquearFechaAntes: function () {
            var hoy = new Date();
            var dd = hoy.getDate();
            var mm = hoy.getMonth() + 1; //hoy es 0!
            var yyyy = hoy.getFullYear()
            let minDate_ = dd + '/' + mm + '/' + yyyy;

            $(".lu-cal-timeHoy").flatpickr({
                "locale": "es",
                enableTime: true,
                dateFormat: "d/m/Y H:i",
                minDate: minDate_,
                onClose: function (selectedDates, dateStr, instance) {
                    webApp.campoLlenoInput();
                }
            });
            $(".lu-cal-mindatehoy").flatpickr({
                "locale": "es",
                dateFormat: "d/m/Y",
                minDate: minDate_,
                onClose: function (selectedDates, dateStr, instance) {
                    webApp.campoLlenoInput();
                }
            });
        },
        bloquearFechaAnterioresAlMes: function (elemento) {

            var hoy = new Date();
            var primerdia = new Date(hoy.getFullYear(), hoy.getMonth() + 1, 1);
            var ultimoDia = new Date(hoy.getFullYear(), hoy.getMonth() + 1, 0);

            let minDate_ = primerdia.getDate() + '/' + (primerdia.getMonth()) + '/' + primerdia.getFullYear();
            let maxDate_ = ultimoDia.getDate() + '/' + (ultimoDia.getMonth() + 1) + '/' + ultimoDia.getFullYear();

            $("#" + elemento).datetimepicker({
                locale: "es",
                format: "d/m/Y",
                minDate: minDate_,
                maxDate: maxDate_

            });
        },
        ListaUbicacion: function (id) {
            var url = "/Comun/ListaUbicacion";
            var param = {
                codigo: id
            }
            webApp.JsonParam(url, param, function (data) {
                $('#IdUbicacionSearch').empty();
                $('#IdUbicacionSearch').append($('<option>', {
                    value: '0',
                    text: 'Seleccione Ubicacion'
                }))
                $.each(data.data, function (i, item) {
                    $('#IdUbicacionSearch').append($('<option>', {
                        value: item.value,
                        text: item.nombre
                    }))
                });
            });
        },
         ListaModelo: function (id) {
            var url = "/Comun/ListaModelo";
            var param = {
                codigo: id,
                empresa: $("#_IdPadre").val()
            };
            webApp.JsonParam(url, param, function (data) {
                $('#IdModeloSearch').empty();
                $('#IdModeloSearch').append($('<option>', {
                    value: '0',
                    text: 'Seleccione Modelo'
                }));
                $.each(data.data, function (i, item) {
                    $('#IdModeloSearch').append($('<option>', {
                        value: item.value,
                        text: item.nombre
                    }));
                });
            });
        },
        ListaCategoria: function (id) {
            var url = "/Comun/ListaCategoriaSolo";
            var param = {
                codigo: id
            }
            webApp.JsonParam(url, param, function (data) {
                $('#IdCategoriaSearch').empty();
                $('#IdCategoriaSearch').append($('<option>', {
                    value: '0',
                    text: 'Seleccione Categoria'
                }))
                $.each(data.data, function (i, item) {
                    $('#IdCategoriaSearch').append($('<option>', {
                        value: item.value,
                        text: item.nombre
                    }))
                });
            });
        },
        ///end ----- julio cesar pillaca
        ValidarFechaNacimiento: function (fechaSeleccionada) {
            let fecha = new Date();
            if (new Date(fechaSeleccionada).getFullYear() >= fecha.getFullYear())
                return false;
            if (new Date(fechaSeleccionada).getMonth() >= fecha.getMonth())
                return false;
            if (new Date(fechaSeleccionada).getDate() >= fecha.getDate())
                return false;
            else
                return true;
        },
        validarFormTrim: function (formulario) {
            $('#' + formulario).find('input[type=text]').each(function (i, elem) {
                $(this).val($(this).val().trim());
            });
        },
        ValidartoUpperCase: function () {
            $('#espacio').find('input').each(function (i, elem) {
                if (elem.id != '') {
                    return $('#' + elem.id).val($('#' + elem.id).val().toUpperCase());
                }
            });

        },
        //validar que la fecha final sea mayor a la 
        FechaCorrecta: function (fechaInicial, fechaFinal) {
            ;
            //Split de las fechas recibidas para separarlas
            var x = fechaInicial.split("/");
            var z = fechaFinal.split("/");

            //Cambiamos el orden al formato americano, de esto dd/mm/yyyy a esto mm/dd/yyyy
            fechaInicial = x[1] + "-" + x[0] + "-" + x[2];
            fechaFinal = z[1] + "-" + z[0] + "-" + z[2];

            //Comparamos las fechas
            if (Date.parse(fechaInicial) >= Date.parse(fechaFinal)) {
                //CloseGrabar();
                webApp.sweetmensaje('Alerta!', "LA FECHA INICIAL: " + fechaInicial + " NO PUEDE SER MAYOR O IGUAL A FECHA FINAL: " + fechaFinal, 'warning')
                // return toastr.error("LA FECHA INICIAL: " + fechaInicial + " NO PUEDE SER MAYOR O IGUAL A FECHA FINAL: " + fechaFinal);
            }
            else {
                return 1;
            }

        },
        Calcular_edad: function (fecha) {
            if (fecha != "" || fecha != null) {
                /* esto es lo importante */
                var d = new Date();
                var a = d.getFullYear();

                var sun = fecha.substring(6);
                var anioper = parseInt(sun);
                edad = a - anioper;
                if (edad < 18) {
                    return -1;
                }
                else {
                    return 1;
                }
            } else {
                return false;
            }

        },
        activateSelect: function () {
            $('select').not('.selectbox').each(function () {
                var options = {
                    placeholder: function () {
                        var el = $(this);
                        el.data('placeholder');
                    }
                };

                if (!$(this).is('[data-search-enable]')) {
                    options['minimumResultsForSearch'] = Infinity;
                }

                $(this).select2(options);
            });
        },

        tipoAvento: function (idCategoria) {
            let result = '';
            if (idCategoria == 1) {
                result = 'mensaje';
            }
            else if (idCategoria == 2) {
                result = 'correo';
            }
            else if (idCategoria == 3) {
                result = 'llamada';
            }
            else if (idCategoria == 4) {
                result = 'visita';
            }
            return result;
        },
        CampoLlenoInput: function () {
            $('#espacio input.form-control').each(function () {
                $(this).val() ? $(this).addClass('campo-lleno').parent(":not(.form-group)").addClass('campo-lleno') : $(this).removeClass('campo-lleno').parent(":not(.form-group)").removeClass('campo-lleno');
            });
        },
        formatoFecha: function (fecha) {
            let date = fecha.toString().replace("/", "-").replace("/", "-").replace("p. m.", "");
            let splic_ = date.split('-');
            let anioH = splic_[2].split(" ");
            return anioH[0] + '-' + splic_[1] + '-' + splic_[0] + ' ' + anioH[1];
        },

        roundNumber: function (num, scale) {
            if (!("" + num).includes("e")) {
                return +(Math.round(num + "e+" + scale) + "e-" + scale);
            } else {
                var arr = ("" + num).split("e");
                var sig = ""
                if (+arr[1] + scale > 0) {
                    sig = "+";
                }
                var i = +arr[0] + "e" + sig + (+arr[1] + scale);
                var j = Math.round(i);
                var k = +(j + "e-" + scale);
                return k;
            }
        },
        stringToBoolean: function (string) {
            switch (string.toLowerCase().trim()) {
                case "true": case "yes": case "1": return true;
                case "false": case "no": case "0": case null: return false;
                default: return Boolean(string);
            }
        },
        normalize: function (s) {
            var r = s.toLowerCase();
            r = r.replace(new RegExp(/\s/g), "");
            r = r.replace(new RegExp(/[àáâãäå]/g), "a");
            r = r.replace(new RegExp(/[èéêë]/g), "e");
            r = r.replace(new RegExp(/[ìíîï]/g), "i");
            r = r.replace(new RegExp(/ñ/g), "n");
            r = r.replace(new RegExp(/[òóôõö]/g), "o");
            r = r.replace(new RegExp(/[ùúûü]/g), "u");

            return r;

        },
        createPropiedadObject: function (obj, nombre, valor) {
            Object.defineProperty(obj, '' + nombre + '', {
                value: valor,
                writable: true,
                configurable: true,
                enumerable: true
            });
        },
        lengthArray: function (array) {
            ;
            let result = [];
            for (var i = 0; i < array.length; i++) {
                if (array[i] != 0) {
                    result.push(array[i]);
                }
            }
            return result.length;
        },
        ValidarSiexiste: function (lista, valor) {
            var texto = lista,
                separador = ",",
                textoseparado = texto.split(separador);
            var contador = 0;
            if (textoseparado.length > 1) {
                jQuery.each(textoseparado, function (index, item) {
                    if (contador == 0) {

                        if (item == valor) {
                            contador++;
                        }
                    }
                });
            }
            if (contador > 0)
                return true;
            else
                return false;
        },
        EliminarElementoEspecificoArray: function (arr, item) {
            var i = arr.indexOf(item);

            if (i !== -1) {
                arr.splice(i, 1);
            }
            return arr;
        },
        validarFechaDesdeHasta: function (elemento, fecha) {
            var x = fecha.split("/");
            let formatFecha = x[2] + "/" + x[1] + "/" + x[0];
            var TuFecha = new Date(formatFecha);
            var dias = 0; // Número de días a agregar
            TuFecha.setDate(TuFecha.getDate() + dias);
            let fechaResultado = TuFecha.getDate() + '/' + (TuFecha.getMonth() + 1) + '/' + TuFecha.getFullYear();
            ;
            $("#" + elemento).flatpickr({
                "locale": "es",
                dateFormat: "d/m/Y",
                minDate: fechaResultado,
                onClose: function (selectedDates, dateStr, instance) {

                }
            });
        },
        estadoPagoAlumno: function (estado) {
            let result = '';
            if (estado == 'ACT') {
                result = '<td><button class="btn-columbia-pendiente">Pendiente</button></td>';
            }
            else if (estado == 'PAG') {
                result = '<td><button class="btn-columbia-pagado">Pagado</button></td>';
            }
            else if (estado == 'AMO') {
                result = '<td><button class="btn-columbia-amortizado">Amortizado</button></td>';
            }
            else if (estado == 'CON') {
                result = '<td><button class="btn-columbia-convalidado">Convalidado</button></td>';
            }

            return result;
        }
    }
}();

var ColumbiaSelect = function (elmselector) {
    let elm = $('#' + elmselector);
    let children = elm.find('option');
    let options = $(`<div class="columbia-select-option"></div>`), primero = '', option = '';
    let select = $(`<div class="columbia-select" data-elm="${elmselector}"></div>`);
    for (let i = 0; i < children.length; i++) {
        let child = $(children[i]);
        if (i == 0) primero = child;
        if (child.attr('data-color') !== undefined) {
            option = $(`<a data-value="${child.attr('value')}"><span class="color-select" style="background-color: ${child.attr('data-color')};"></span> ${child.html()}</a>`);
        }
        else {
            option = $(`<a data-value="${child.attr('value')}"> ${child.html()}</a>`);
        }
        option.bind('click', function (e) {
            e.stopPropagation();
            let selected = $(this).parents('.columbia-select').find('.columbia-select-selected');
            selected.attr('data-value', $(this).attr('data-value'));
            selected.html($(this).html() + '<i>&#9660;</i>');
            elm.val($(this).attr('data-value'));
            options.removeClass('active');
        });
        if (option.html() !== '') { options.append(option) } else { options.html(option); }
    }
    let selected = $(`<a class="columbia-select-selected" data-value="${primero.attr('value')}"></a>`);
    selected.html(`${primero.html()}<i>&#9660;</i>`);
    select.html(selected);
    select.append(options);
    select.insertAfter(elm);
    elm.hide();
    selected.bind('click', function (e) {
        e.stopPropagation();
        $(this).parent().find('.columbia-select-option').addClass('active');
    });
    $(document).on('click', function () {
        options.removeClass('active');
    });
};

function getCookie(name) {
    var pairs = document.cookie.split("; "),
        count = pairs.length,
        parts;
    while (count--) {
        parts = pairs[count].split("=");
        if (parts[0] === name)
            return parts[1];
    }
    return false;
}
$(document).on('keyup', "#espacio input,textarea", function () {
    /* Obtengo el valor contenido dentro del input */
    var texto = $(this).siblings("label").eq(0).text();
    var obligatorio = texto + " Obligatorio";
    var value = $(this).val().trim();
    var idNuevo = $(this).attr("id");
    var Autocomplete = $("#espacio #" + idNuevo).hasClass("autocompli");
    /* Elimino todos los espacios en blanco que tenga la cadena delante*/
    var espacioIzquierda;
    if (value == "") {
        espacioIzquierda = $.trim(value)
        $(this).val(espacioIzquierda);
    }
    if ($(this).val() != "") {
        if (Autocomplete) {
            $(this).parents().eq(1).removeClass("has-danger");
            var d = $("#espacio #" + idNuevo).parents("label");
        } else {
            $(this).parents().eq(0).removeClass("has-danger");
            $("#espacio #" + idNuevo).siblings("label").nextUntil().remove();
        }

    } else {
        if ($(this).prop('required')) {
            $(this).parents().eq(0).addClass("has-danger");
            var html = '<lable class="label text-danger">' + obligatorio + '</label>';
            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
            if (tamaño.length < 1) {
                if (Autocomplete) {
                    $(this).parents().eq(1).addClass("has-danger");
                } else {
                    $(this).parents("div").eq(0).append(html);
                }
            }
        } else {
            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
            if (tamaño.length > 0) {
                $("#espacio #" + idNuevo).siblings("label").nextUntil().remove();
            }
            $(this).parents().eq(0).removeClass("has-danger");
        }
    }
});

$(document).on('change', "#espacio input", function () {
    /* Obtengo el valor contenido dentro del input */
    var texto = $(this).siblings("label").eq(0).text();
    var obligatorio = texto + " Obligatorio";
    var value = $(this).val().trim();
    var idNuevo = $(this).attr("id");
    var Autocomplete = $("#espacio #" + idNuevo).hasClass("autocompli");
    /* Elimino todos los espacios en blanco que tenga la cadena delante*/
    var espacioIzquierda;
    if (value == "") {
        espacioIzquierda = $.trim(value)
        $(this).val(espacioIzquierda);
    }
    if ($(this).val() != "") {
        if (Autocomplete) {
            $(this).parents().eq(1).removeClass("has-danger");
            var d = $("#espacio #" + idNuevo).parents("label");
        } else {
            $(this).parents().eq(0).removeClass("has-danger");
            $("#espacio #" + idNuevo).siblings("label").nextUntil().remove();
        }

    } else {
        if ($(this).prop('required')) {
            $(this).parents().eq(0).addClass("has-danger");
            var html = '<lable class="label text-danger">' + obligatorio + '</label>';
            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
            if (tamaño.length < 1) {
                if (Autocomplete) {
                    $(this).parents().eq(1).addClass("has-danger");
                } else {
                    $(this).parents("div").eq(0).append(html);
                }
            }
        } else {
            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
            if (tamaño.length > 0) {
                $("#espacio #" + idNuevo).siblings("label").nextUntil().remove();
            }
            $(this).parents().eq(0).removeClass("has-danger");
        }
    }
});

$(document).on('focus', "#espacio input.form-control", function () {
    $(this).addClass('campo-lleno campo-focus').parent(":not(.form-group)").addClass('campo-lleno');
});
$(document).on('blur', "#espacio input.form-control", function () {
    $(this).removeClass('campo-focus').parent(":not(.form-group)").removeClass('campo-focus');
    $(this).val() ? $(this).addClass('campo-lleno').parent(":not(.form-group)").addClass('campo-lleno') : $(this).removeClass('campo-lleno').parent(":not(.form-group)").removeClass('campo-lleno');
});


$(document).on('change', "#espacio select", function () {
    /* Obtengo el valor contenido dentro del input */
    var texto = $(this).siblings("label").eq(0).text();
    var obligatorio = texto + " Obligatorio";
    var value = $(this).val().trim();
    var idNuevo = $(this).attr("id");
    /* Elimino todos los espacios en blanco que tenga la cadena delante*/
    var espacioIzquierda;

    if ($(this).val() != "0") {
        $(this).parents().eq(0).removeClass("has-danger");
        $(this).parents().eq(0).removeClass("has-danger");
        $("#espacio #" + idNuevo).siblings("label").nextUntil().remove();
    } else {
        if ($(this).prop('required')) {
            $(this).parents().eq(0).addClass("has-danger");
            var html = '<lable class="label text-danger">' + obligatorio + '</label>';
            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
            if (tamaño.length < 1) {
                $(this).parents("div").eq(0).append(html);
            }

        } else {
            var tamaño = $("#espacio #" + idNuevo).siblings("label").nextUntil();
            if (tamaño.length > 0) {
                $("#espacio #" + idNuevo).siblings("label").nextUntil().remove();
            }
            $(this).parents().eq(0).removeClass("has-danger");
        }
    }
});

$(document).on("click", "input#_checkGeneral", function () {
    $("input:checkbox").prop('checked', $(this).prop("checked"));
});
$(document).on("click", "input.custom-control-item", function () {
    $("input#_checkGeneral").prop('checked', false);
});

function delCookie(name) {
    var date = new Date();
    date.setDate(date.getDate() - 1);
    document.cookie = name + "=" + '=;expires=' + date + "; path=/";
}


function s4() {
    return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
}
