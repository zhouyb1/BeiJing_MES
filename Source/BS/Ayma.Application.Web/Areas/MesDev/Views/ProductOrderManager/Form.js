/* * 创建人：超级管理员
 * 日  期：2019-03-07 11:05
 * 描  述：生产订单管理
 */
var acceptClick;
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
            //$('#am_form_tabs').FormTab();
            //$('#am_form_tabs ul li').eq(0).trigger('click');
            $('#P_Status').DataItemSelect({ code: 'ProductOrderStatus' });

            $('#Mes_ProductOrderDetail').jfGrid({
                headData: [
                     {
                         label: 'ID', name: 'ID', width: 160, align: 'left', editType: 'label', hidden: true
                     },
                    {
                        label: '物料编码', name: 'P_GoodsCode', width: 140, align: 'left', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'P_GoodsName', width: 140, align: 'left', editType: 'label'
                    },
                    {
                        label: '单位', name: 'P_Unit', width: 60, align: 'left', editType: 'label'
                    },
                     {
                         label: '数量', name: 'P_Qty', width: 60, align: 'left', editType: 'input',
                         editOp: {
                             callback: function (rownum, row) {
                                 if (/\D/.test(row.P_Qty.toString().replace('.', ''))) { //验证只能为数字
                                     row.M_Qty = 0;
                                 }

                             }
                         }
                     },
                    {
                        label: '订单时间', name: 'P_OrderDate', width: 100, align: 'left', editType: 'label'
                    }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                //inputCount: 2
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/ProductOrderManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_ProductOrderDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
        postData.strEntity = JSON.stringify($('[data-table="Mes_ProductOrderHead"]').GetFormData());
        postData.strmes_ProductOrderDetaillist = JSON.stringify($('#Mes_ProductOrderDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/ProductOrderManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
