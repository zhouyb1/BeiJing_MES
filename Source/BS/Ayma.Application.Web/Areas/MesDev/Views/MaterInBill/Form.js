/* * 创建人：超级管理员
 * 日  期：2019-01-08 14:58
 * 描  述：入库单制作
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
            $('#Mes_MaterInDetail').jfGrid({
                headData: [
                    {
                        label: '物料编码', name: 'M_GoodsCode', width: 140, align: 'left',editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'M_GoodsName', width: 140, align: 'left', editType: 'label'
                    },
                    {
                        label: '单位', name: 'M_Unit', width: 60, align: 'left', editType: 'label'
                    },
                    {
                        label: '数量', name: 'M_Qty', width: 60, align: 'left', editType: 'label'
                    },
                    {
                        label: '批次', name: 'M_Batch', width: 100, align: 'left', editType: 'label'
                    },
                    {
                        label: '备注', name: 'M_Remark', width: 160, align: 'left', editType: 'label'
                    },
                ],
                isAutoHeight: true,
                isEidt: false,
                footerrow: true,
                minheight: 400
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/MaterInBill/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_MaterInDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
        postData.strEntity = JSON.stringify($('[data-table="Mes_MaterInHead"]').GetFormData());
        postData.strmes_MaterInDetailList = JSON.stringify($('#Mes_MaterInDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/MaterInBill/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
