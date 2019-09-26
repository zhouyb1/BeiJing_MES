/* * 创建人：超级管理员
 * 日  期：2019-01-08 17:17
 * 描  述：库存查询
 */
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
            //// 查看详情
            //$('#am_edit').on('click', function () {
            //    var keyValue = $('#girdtable').jfGridValue('ID');
            //    if (ayma.checkrow(keyValue)) {
            //        ayma.layerForm({
            //            id: 'form',
            //            title: '库存查询',
            //            url: top.$.rootUrl + '/MesDev/InventorySeach/Form?keyValue=' + keyValue,
            //            width: 700,
            //            height: 500,
            //            maxmin: true,
            //            btn: null,
            //            callBack: function (id) {
            //                //return top[id].acceptClick(refreshGirdData);
            //            }
            //        });
            //    }
            //});

            $('#girdtable').on('dblclick', function () {
                var I_GoodsName = escape($('#girdtable').jfGridValue('I_GoodsName'));//商品名称 转码
                var I_StockName = escape($('#girdtable').jfGridValue('I_StockName'));
                var I_Unit = escape($('#girdtable').jfGridValue('I_Unit'));
                if (ayma.checkrow(I_GoodsName)) {
                    ayma.layerForm({
                        id: 'OrderMaterListForm',
                        title: '库存明细',
                        url: top.$.rootUrl + '/MesDev/InventorySeach/InvertoryList?I_GoodsName=' + I_GoodsName + '&I_StockName=' + I_StockName + '&I_Unit=' + I_Unit,
                        width: 800,
                        height: 600,
                        maxmin: true,
                        callback: function (id, index) {
                            return top[id].closeWindow();
                        }
                    });
                }
            });
            //明细
            $('#am_edit').on('click', function () {
                var I_GoodsName = escape($('#girdtable').jfGridValue('I_GoodsName'));//商品名称 转码
                var I_StockName = escape($('#girdtable').jfGridValue('I_StockName'));
                var I_Unit = escape($('#girdtable').jfGridValue('I_Unit'));
                if (ayma.checkrow(I_GoodsName)) {
                ayma.layerForm({
                    id: 'OrderMaterListForm',
                    title: '库存明细',
                    url: top.$.rootUrl + '/MesDev/InventorySeach/InvertoryList?I_GoodsName=' + I_GoodsName + '&I_StockName=' + I_StockName + '&I_Unit=' + I_Unit,
                    width: 800,
                    height: 600,
                    maxmin: true,
                    callback: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
                }
            });
            ////明细
            //$('#am_detail').on('click', function () {
            //    if ($("#C_StockName").selectGet() == "") {
            //        ayma.alert.error("请选择原料仓库！");
            //        return false;
            //    }
            //    ayma.layerForm({
            //        id: 'OrderMaterListForm',
            //        title: '添加订单物料',
            //        url: top.$.rootUrl + '/MesDev/PickingMater/OrderMaterList?formId=' + parentFormId + '&stockCode=' + $("#C_StockCode").val() + '&C_TeamCode=' + $("#C_TeamCode").selectGet(),
            //        width: 100,
            //        height: 1000,
            //        maxmin: true,
            //        callback: function (id, index) {
            //            return top[id].closeWindow();
            //        }
            //    });
            //});
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').AuthorizeJfGrid({
                url: top.$.rootUrl + '/MesDev/InventorySeach/GetPageList',
                headData: [
                    { label: "仓库编码", name: "I_StockCode", width: 160, align: "left"},
                    { label: "仓库名称", name: "I_StockName", width: 160, align: "left"},
                    { label: "商品编码", name: "I_GoodsCode", width: 160, align: "left"},
                    { label: "商品名称", name: "I_GoodsName", width: 160, align: "left"},
                    { label: "单位", name: "I_Unit", width: 160, align: "left"},
                    {label: "数量", name: "I_Qty", width: 160, align: "left"},
                    {label: "下限预警量", name: "G_Lower", width: 160, align: "left" },
                    {label: "上限预警量", name: "G_Super", width: 160, align: "left" },
                    {label: "预警状态", name: "G_State", width: 160, align: "left" },  
                    { label: "批次", name: "I_Batch", width: 160, align: "left"},
                    { label: "备注", name: "I_Remark", width: 160, align: "left"},
                ],
                mainId:'ID',
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
    page.init();
}
