﻿/* * 创建人：超级管理员
 * 日  期：2019-03-15 16:11
 * 描  述：线边仓退料到仓库
 */
var acceptClick;
var refreshGirdData;//表格商品添加
var parentFormId = request('formId');
var RemoveGridData;//移除表格
var tmp = new Map();
var stockCode;
var keyValue = request('keyValue');
var bootstrap = function($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function() {
            $('.am-form-wrap').mCustomScrollbar({ theme: "minimal-dark" });
            page.bind();
            page.initData();
        },
        bind: function() {
            //绑定仓库
            $('#B_StockName').select({
                text: "s_name",
                value: "s_name",
                type: 'default',
                maxHeight: 200,
                allowSearch: true,
                url: top.$.rootUrl + '/AM_SystemModule/DataSource/GetDataTable',
                param: { code: "StockList", strWhere: "S_Kind =1 " },
            }).on('change', function() {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function(data) {
                        var entity = JSON.parse(data).data;
                        stockCode = entity.S_Code;
                        $('#B_StockCode').val(stockCode);
                    }
                });
            });

            //绑定线边仓
            $('#B_StockToName').select({
                text: "s_name",
                value: "s_name",
                type: 'default',
                maxHeight: 200,
                allowSearch: true,
                url: top.$.rootUrl + '/AM_SystemModule/DataSource/GetDataTable',
                param: { code: "StockList", strWhere: "S_Kind =4 " },
            }).on('change', function() {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function(data) {
                        var entity = JSON.parse(data).data;
                        $('#B_StockToCode').val(entity.S_Code);
                    }
                });
            });

            //添加报废物料
            $('#am_add').on('click', function() {
                var stock = $('#B_StockName').selectGet();
                if (stock == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'MaterListForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/BackStockManager/GoodsListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 700,
                    height: 500,
                    maxmin: true,
                    callback: function(id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_BackStockDetail').jfGrid({
                headData: [
                    { label: "物料编码", name: "B_GoodsCode", width: 130, align: "left", },
                    { label: "物料名称", name: "B_GoodsName", width: 130, align: "left" },
                    { label: "单价", name: "B_Price", width: 130, align: "left" },
                    { label: "单位", name: "B_Unit", width: 60, align: "left" },
                    { label: "返回数量", name: "B_Qty", width: 60, align: "left", editType: 'input' },
                    //{ label: "现有数量", name: "B_OldQty", width: 60, align: "left", editType: 'input' },
                    { label: "批次", name: "B_Batch", width: 60, align: "left" }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                inputCount: 2
            });
        },
        initData: function() {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/BackStockManager/GetFormData?keyValue=' + keyValue, function(data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_BackStockDetail').jfGridSet('refreshdata', { rowdatas: data[id] });

                        } else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            }
        },
        search: function (data) {
            data = data || {};
            $('#Mes_BackStockDetail').jfGridSet('refreshdata', { rowdatas: data });
        }

    };
    //保存数据
    acceptClick = function(callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_BackStockHead"]').GetFormData());
        postData.strmes_BackStockDetailList = JSON.stringify($('#Mes_BackStockDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/BackStockManager/SaveForm?keyValue=' + keyValue, postData, function(res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    top.NewGirdData = function() {
        return $('#Mes_BackStockDetail').jfGridGet('rowdatas');
    }
    //表格商品添加
    refreshGirdData = function(data, row) {
        var rows = $('#Mes_BackStockDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                rows.push(row);
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp.get(data[i])) {
                    tmp.set(data[i], 1);
                    rows.push(data[i]);
                }
            }
        }
        //数组过滤
        var filterarray = $.grep(rows, function(item) {
            return item["B_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function(row) {
        var rows = $('#Mes_BackStockDetail').jfGridGet('rowdatas');
        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["B_GoodsCode"] == row["G_GoodsCode"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    page.init();
};
