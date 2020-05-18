/* * 创建人：超级管理员
 * 日  期：2019-03-13 10:06
 * 描  述：领料单查询
 */
var js_method_stock;
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
$('.am-form-wrap').mCustomScrollbar({theme: "minimal-dark"}); 
            page.bind();
            page.initData();
        },
        bind: function () {

            var dfop = {
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockListByParam',
                // 访问数据接口参数
                param: { strWhere: "S_Kind =4" }
            }
            //绑定目标仓
            $("#C_StockToName").select(dfop).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#C_StockToCode").val(entity == null ? "" : entity.S_Code);
                    }
                });
            });
            $('#Mes_CollarDetail').jfGrid({
                headData: [
                    { label: "物料编码", name: "C_GoodsCode", width: 80, align: "left" },
                    { label: "物料名称", name: "C_GoodsName", width: 100, align: "left" },
                    { label: "单价", name: "C_Price", width: 70, align: "left" },
                    { label: "单位", name: "C_Unit", width: 40, align: "left" },
                    { label: "领料数量", name: "C_Qty", width: 60, align: "left" },
                    { label: "金额", name: "C_Amount", width: 80, align: "left", statistics: true },
                    { label: "批次", name: "C_Batch", width: 80, align: "left" },
                    { label: "包装数量", name: "C_Qty2", width: 60, align: "left" },
                    { label: "包装规格", name: "C_UnitQty", width: 60, align: "left", },
                    { label: "包装单位", name: "C_Unit2", width: 60, align: "left" },
                    { label: "计划数量", name: "C_PlanQty", width: 100, align: "left" },
                    { label: "建议数量", name: "C_SuggestQty", width: 100, align: "left" },
                    { label: "原仓库编码", name: "C_StockCode", width: 90, align: "left" },
                            {
                                label: "原仓库名称",
                                name: "C_StockName",
                                width: 90,
                                align: "left",
                                formatter: function (value, row, dfop) {
                                    if (row != null && row.C_StockName != undefined) {
                                        if (row != null && row.C_StockName != undefined) {
                                            return "<a href =# style=text-decoration:underline title='点击查询库存' onclick=js_method_stock('" + row.C_StockCode + "','9b04a0f2-28c0-4a58-973d-47bd51944a1c')>" + row.C_StockName + "</ a>";
                                        } else {
                                            return row.C_StockName;
                                        }
                                        if (row.C_StockName == "蔬菜库") {
                                            return row.C_Qty = row.StockQty;
                                        }
                                    }
                                }
                            },
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                height: 300,
                isStatistics: true
        });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/PickingMaterQuery/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_CollarDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').SetFormData(data[id]);
                        }
                    }
                });
            }
        },
    };
    js_method_stock = function (code, moduleId) {
        var module = top.ayma.clientdata.get(['modulesMap', moduleId]);
        module.F_UrlAddress = '/MesDev/InventorySeach/Index?stock=' + encodeURIComponent(code);
        top.ayma.frameTab.openNew(module);
        var index = window.parent.layer.getFrameIndex(window.name);
        window.parent.layer.close(index);//关闭layer
    }
    page.init();
}
