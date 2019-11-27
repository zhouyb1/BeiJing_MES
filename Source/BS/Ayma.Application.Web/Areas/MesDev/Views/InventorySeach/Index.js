/* * 创建人：超级管理员
 * 日  期：2019-01-08 17:17
 * 描  述：库存查询
 */
var refreshSubGirdData;
var $subgridTable;//子列表
var refreshGirdData;
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
        },
        bind: function () {
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 220, 500);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            //仓库
            $('#I_StockName').select({
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockList',
                // 访问数据接口参数
                param: {}
            });
            //$('#girdtable').on('dblclick', function () {
            //    var I_GoodsName = escape($('#girdtable').jfGridValue('I_GoodsName'));//商品名称 转码
            //    var I_StockName = escape($('#girdtable').jfGridValue('I_StockName'));
            //    var I_Unit = escape($('#girdtable').jfGridValue('I_Unit'));
            //    if (ayma.checkrow(I_GoodsName)) {
            //        ayma.layerForm({
            //            id: 'OrderMaterListForm',
            //            title: '库存明细',
            //            url: top.$.rootUrl + '/MesDev/InventorySeach/InvertoryList?I_GoodsName=' + I_GoodsName + '&I_StockName=' + I_StockName + '&I_Unit=' + I_Unit,
            //            width: 800,
            //            height: 600,
            //            maxmin: true,
            //            callback: function (id, index) {
            //                return top[id].closeWindow();
            //            }
            //        });
            //    }
            //});
            //明细
            //$('#am_edit').on('click', function () {
            //    var I_GoodsName = escape($('#girdtable').jfGridValue('I_GoodsName'));//商品名称 转码
            //    var I_StockName = escape($('#girdtable').jfGridValue('I_StockName'));
            //    var I_Unit = escape($('#girdtable').jfGridValue('I_Unit'));
            //    if (ayma.checkrow(I_GoodsName)) {
            //    ayma.layerForm({
            //        id: 'OrderMaterListForm',
            //        title: '库存明细',
            //        url: top.$.rootUrl + '/MesDev/InventorySeach/InvertoryList?I_GoodsName=' + I_GoodsName + '&I_StockName=' + I_StockName + '&I_Unit=' + I_Unit,
            //        width: 800,
            //        height: 600,
            //        maxmin: true,
            //        callback: function (id, index) {
            //            return top[id].closeWindow();
            //        }
            //    });
            //    }
            //});
            //});
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/InventorySeach/GetPageList',
                headData: [
                    {label: "仓库编码", name: "I_StockCode", width: 114, align: "left"},
                    { label: "仓库名称", name: "I_StockName", width: 114, align: "left" },
                    {
                        label: "仓库类型", name: "S_Kind", width: 115, align: "left",
                        formatterAsync: function (callback, value, row) {
                            ayma.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'StockType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: "商品编码", name: "I_GoodsCode", width: 114, align: "left" },
                    { label: "商品名称", name: "I_GoodsName", width: 114, align: "left" },
                    { label: "单位", name: "I_Unit", width: 114, align: "left" },
                    { label: "数量", name: "I_Qty", width: 114, align: "left" },
                    { label: "物料价格", name: "Price", width: 114, align: "left" },
                    { label: "总金额", name: "AllMoney", width: 114, align: "left" },
                    { label: "下限预警量", name: "G_Lower", width: 114, align: "left" },
                    { label: "上限预警量", name: "G_Super", width: 114, align: "left" },
                    { label: "预警状态", name: "G_State", width: 114, align: "left" }
                ],
                mainId: 'ID',
                sidx: "I_StockCode",
                sord: 'ASC',
                reloadSelected: true,
                isPage: true,
                onRenderComplete: function (rows) {
                    for (var i = 0; i < rows.length; i++) {
                        if (rows[i].G_State == "正常" || rows[i].G_State == "无") {
                            continue;
                        }
                        else {
                            $("[rownum='rownum_girdtable_" + i + "']").css('background-color', '#d9534f');//预警状态不正常的整行标红
                        }
                    }
                },
                isSubGrid: true,
                subGridRowExpanded: function (subgridId, row) {
                    var I_GoodsName = row.I_GoodsName;
                    var I_StockName = row.I_StockName;
                    var I_Unit = row.I_Unit;
                var subgridTableId = subgridId + "_t";
                $("#" + subgridId).html("<div class=\"am-layout-body\" id=\"" + subgridTableId + "\"></div>");
                $subgridTable = $("#" + subgridTableId);
                $subgridTable.jfGrid({
                    url: top.$.rootUrl + '/MesDev/InventorySeach/GetInventoryList?I_GoodsName=' + I_GoodsName + '&I_StockName=' + I_StockName + '&I_Unit=' + I_Unit,
                    headData: [
                      { label: "仓库编码", name: "I_StockCode", width: 100, align: "left" },
                      { label: "仓库名称", name: "I_StockName", width: 100, align: "left" },
                      { label: "商品编码", name: "I_GoodsCode", width: 100, align: "left" },
                      { label: "商品名称", name: "I_GoodsName", width: 100, align: "left" },
                      { label: "单位", name: "I_Unit", width: 100, align: "left" },
                      { label: "数量", name: "I_Qty", width: 100, align: "left" },
                      { label: "物料价格", name: "Price", width: 100, align: "left" },
                      { label: "金额", name: "OneMoney", width: 160, align: "left" },
                      { label: "批次", name: "I_Batch", width: 100, align: "left" },
                      { label: "备注", name: "I_Remark", width: 100, align: "left" },
                    ],
                    mainId: 'ID',
                    isPage: true,
                    sidx: "I_Batch",
                    sord: 'ASC',
                    reloadSelected: false,
                }).jfGridSet("reload");
            }
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(param) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    //子列表刷新
    refreshSubGirdData = function () {
        $subgridTable.jfGridSet("reload");
    };
    page.init();
}
