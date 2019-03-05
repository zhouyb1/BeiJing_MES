/* * 创建人：超级管理员
 * 日  期：2019-03-05 11:20
 * 描  述：采购单制作及查询
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
            $('#Mes_PurchaseDetail').jfGrid({
                headData: [
                   
                    {
                        label: '采购单号', name: 'P_PurchaseNo', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '生产订单号', name: 'P_OrderNo', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '物料编码', name: 'P_GoodsCode', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '物料名称', name: 'P_GoodsName', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '单位', name: 'P_Unit', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '数量', name: 'P_Qty', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '价格', name: 'P_Price', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '批次', name: 'P_Batch', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '备注', name: 'P_Remark', width: 160, align: 'left',editType: 'input'
                    },
                ],
                isAutoHeight: true,
                isEidt: true,
                footerrow: true,
                minheight: 400
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/PurchaseHead/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_PurchaseDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_PurchaseHead"]').GetFormData());
        postData.strmes_PurchaseDetailList = JSON.stringify($('#Mes_PurchaseDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/PurchaseHead/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
