/* * 创建人：超级管理员
 * 日  期：2019-03-13 10:06
 * 描  述：领料单查询
 */
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
            $('#Mes_CollarDetail').jfGrid({
                headData: [
                    { label: "物料编码", name: "C_GoodsCode", width: 130, align: "left" },
                    { label: "物料名称", name: "C_GoodsName", width: 130, align: "left" },
                    { label: "单位", name: "C_Unit", width: 60, align: "left" },
                    { label: "数量", name: "C_Qty", width: 60, align: "left" },
                    { label: "批次", name: "C_Batch", width:90, align: "left" }
                ],
                isAutoHeight: false,
                //minheight: 400,
                height: 250,
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
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        Window.close();
        //if (!$('body').Validform()) {
        //    return false;
        //}
        //var postData = {};
        //postData.strEntity = JSON.stringify($('[data-table="Mes_CollarHead"]').GetFormData());
        //postData.strmes_CollarDetailEntity = JSON.stringify($('[data-table="Mes_CollarDetail"]').GetFormData());
        //$.SaveForm(top.$.rootUrl + '/MesDev/PickingMaterQuery/SaveForm?keyValue=' + keyValue, postData, function (res) {
        //    // 保存成功后才回调
        //    if (!!callBack) {
        //        callBack();
        //    }
        //});
    };
    page.init();
}
