/* * 创建人：超级管理员
 * 日  期：2019-03-14 11:20
 * 描  述：退库单查询
 */

var keyValue = request('keyValue');
var tmp = new Map();
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            $('.am-form-wrap').mCustomScrollbar({ theme: "minimal-dark" });
            page.bind();
            page.initData();
        },
        bind: function () {
            //绑定仓库
            var dfop = {
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
            };
            //绑定仓库
            $('#B_StockName').select(dfop);
            $('#Mes_BackStockDetail').jfGrid({
                headData: [
                     { label: "物料编码", name: "B_GoodsCode", width: 130, align: "center", },
                    { label: "物料名称", name: "B_GoodsName", width: 130, align: "center" },
                    { label: "单价", name: "B_Price", width: 130, align: "center" },
                    { label: "单位", name: "B_Unit", width: 60, align: "center" },
                    { label: "返回数量", name: "B_Qty", width: 60, align: "center", },
                    { label: "批次", name: "B_Batch", width: 60, align: "center" }
                ],
                isAutoHeight: false,
                //minheight: 400,
                height: 400,
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/BackStockManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_BackStockDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
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


    top.NewGirdData = function () {
        return $('#Mes_BackStockDetail').jfGridGet('rowdatas');
    }
    page.init();
}
