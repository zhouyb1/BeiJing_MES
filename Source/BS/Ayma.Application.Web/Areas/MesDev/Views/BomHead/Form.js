﻿/* * 创建人：超级管理员
 * 日  期：2019-03-06 17:41
 * 描  述：配方表
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
            $('#B_Avail').DataItemSelect({ code: 'YesOrNo' });
            $('#Mes_BomRecord').jfGrid({
                headData: [
                    {
                        label: 'ID', name: 'ID', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '配方编码', name: 'B_FormulaCode', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '物料编码', name: 'B_GoodsCode', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '物料名称', name: 'B_GoodsName', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '单位', name: 'B_Unit', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '数量', name: 'B_Qty', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '下级物料编码', name: 'B_SecGoodsCode', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '下级物料名称', name: 'B_SecGoodsName', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '下级物料数量', name: 'B_SecQty', width: 160, align: 'left',editType: 'input'
                    },
                    {
                        label: '下级物料单位', name: 'B_SecUnit', width: 160, align: 'left',editType: 'input'
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
                $.SetForm(top.$.rootUrl + '/MesDev/BomHead/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_BomRecord').jfGridSet('refreshdata', { rowdatas: data[id] });
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
        postData.strEntity = JSON.stringify($('[data-table="Mes_BomHead"]').GetFormData());
        postData.strmes_BomRecordList = JSON.stringify($('#Mes_BomRecord').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/BomHead/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
