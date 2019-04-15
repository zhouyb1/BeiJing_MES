var keyValue = request('keyValue');
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
            $('#Mes_OutWorkShopDetail').jfGrid({
                headData: [
                    { label: "物料编码", name: "O_GoodsCode", width: 130, align: "left" },
                    { label: "物料名称", name: "O_GoodsName", width: 130, align: "left" },
                    { label: "单位", name: "O_Unit", width: 60, align: "left" },
                    { label: "数量", name: "O_Qty", width: 60, align: "left" },
                    { label: "价格", name: "O_Price", width: 60, align: "left" },
                    { label: "批次", name: "O_Batch", width: 60, align: "left" }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                height: 300,
                inputCount: 2
            });
           
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/OutWorkShopManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_OutWorkShopDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_OutWorkShopDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };

    top.NewGirdData = function () {
        return $('#Mes_OutWorkShopDetail').jfGridGet('rowdatas');
    }
    page.init();
}