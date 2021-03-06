﻿var keyValue = request('keyValue');
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
            $('#I_WorkShop').select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetLineStockList',
                // 访问数据接口参数
                param: {}
            });

            $('#I_StockCode').select({
                type: 'default',
                value: 'S_Code',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetLineStockList',
                // 访问数据接口参数
                param: {}
            });

            $('#Mes_InWorkShopDetail').jfGrid({
                headData: [
                    { label: "物料编码", name: "I_GoodsCode", width: 130, align: "left" },
                    { label: "物料名称", name: "I_GoodsName", width: 130, align: "left" },
                    { label: "单位", name: "I_Unit", width: 60, align: "left" },
                    { label: "数量", name: "I_Qty", width: 60, align: "left" },
                    { label: "价格", name: "I_Price", width: 60, align: "left" },
                    { label: "金额", name: "I_Amount", width: 60, align: "left", statistics: true },
                    { label: "批次", name: "I_Batch", width: 80, align: "left" }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                height: 300,
                isStatistics:true
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/InWorkShopManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_InWorkShopDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_InWorkShopDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };

    top.NewGirdData = function () {
        return $('#Mes_InWorkShopDetail').jfGridGet('rowdatas');
    }
    page.init();
}