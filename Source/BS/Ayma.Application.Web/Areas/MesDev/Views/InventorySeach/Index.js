/* * 创建人：超级管理员
 * 日  期：2019-01-08 17:17
 * 描  述：库存查询
 */
var refreshSubGirdData;
var $subgridTable;//子列表
var refreshGirdData;
var stock = decodeURIComponent(request('stock'));
var goodsCode = decodeURIComponent(request('goodsCode'));
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
            }, 250, 480);
            // 刷新
            $('#am_refresh').on('click', function () {
                location.href = location.pathname;
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
                url: top.$.rootUrl + '/MesDev/InventorySeach/GetPageList?stock=' + stock + '&goodsCode=' + goodsCode,
                headData: [
                    {label: "仓库编码", name: "I_StockCode", width: 60, align: "left"},
                    { label: "仓库名称", name: "I_StockName", width: 120, align: "left" },
                    {
                        label: "仓库类型", name: "S_Kind", width: 60, align: "left",
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
                    { label: "商品编码", name: "I_GoodsCode", width: 80, align: "left" },
                    { label: "商品名称", name: "I_GoodsName", width: 120, align: "left" },
                    { label: "单位", name: "I_Unit", width: 60, align: "left" },
                    {
                        label: "数量", name: "I_Qty", width: 60, align: "left", formatter: function (value, row, dfop) {
                            if (row.I_Qty != undefined && !!row.I_Qty) {
                                return row.I_Qty.toFixed(6) / 1;
                            }
                            if (row.I_Qty == "") {
                                return row.I_Qty = 0;
                            }
                        }
                    },
                    {
                        label: "物料价格", name: "Price", width: 85 , align: "left", formatter: function (value, row, dfop) {
                            if (row.Price != undefined && !!row.Price) {
                                return row.Price.toFixed(6) / 1;
                            }
                            if (row.Price == "") {
                                return row.Price = 0;
                            }
                        }
                    },
                    {
                        label: "总金额", name: "AllMoney", width: 100, align: "left", formatter: function (value, row, dfop) {
                            if (row.AllMoney != undefined && !!row.AllMoney) {
                                return row.AllMoney.toFixed(6) / 1;
                            }
                            if (row.AllMoney == "") {
                                return row.AllMoney = 0;
                            }
                        }
                    },
                    { label: "下限预警量", name: "G_Lower", width: 70, align: "left" },
                    { label: "上限预警量", name: "G_Super", width: 70, align: "left" },
                    { label: "预警状态", name: "G_State", width: 100, align: "left" }
                ],
                mainId: 'ID',
                sidx: "I_StockCode",
                sord: 'ASC',
                reloadSelected: false,
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
                      {
                          label: "数量", name: "I_Qty", width: 100, align: "left", formatter: function (value, row, dfop) {
                              if (row.I_Qty != undefined && !!row.I_Qty) {
                                  return row.I_Qty.toFixed(6) / 1;
                              }
                              if (row.I_Qty == "") {
                                  return row.I_Qty = 0;
                              }
                          }
                      },
                      {
                          label: "物料价格", name: "Price", width: 100, align: "left", formatter: function (value, row, dfop) {
                              if (row.Price != undefined && !!row.Price) {
                                  return row.Price.toFixed(6) / 1;
                              }
                              if (row.Price == "") {
                                  return row.Price = 0;
                              }
                          }
                      },
                      {
                          label: "金额", name: "OneMoney", width: 160, align: "left", formatter: function (value, row, dfop) {
                              if (row.OneMoney != undefined && !!row.OneMoney) {
                                  return row.OneMoney.toFixed(6) / 1;
                              }
                              if (row.OneMoney == "") {
                                  return row.OneMoney = 0;
                              }
                          }
                      },
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
