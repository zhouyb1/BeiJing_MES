/* * 创建人：超级管理员
 * 日  期：2019-03-14 11:20
 * 描  述：报废单查询
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
            $('#S_StockName').select(dfop);
            $('#Mes_ScrapDetail').jfGrid({
                headData: [
                    { label: "物料编码", name: "S_GoodsCode", width: 130, align: "left", },
                    { label: "物料名称", name: "S_GoodsName", width: 130, align: "left" },
                    { label: "单价", name: "S_Price", width: 60, align: "left" },
                    { label: "单位", name: "S_Unit", width: 60, align: "left" },
                    { label: "数量", name: "S_Qty", width: 60, align: "left", },
                    { label: "金额", name: "S_Amount", width: 60, align: "left",statistics:true },
                    { label: "批次", name: "S_Batch", width: 80, align: "left" }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                height: 300,
                
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/ScrapManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_ScrapDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_ScrapDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    

    top.NewGirdData = function () {
        return $('#Mes_ScrapDetail').jfGridGet('rowdatas');
    }
    page.init();
}
