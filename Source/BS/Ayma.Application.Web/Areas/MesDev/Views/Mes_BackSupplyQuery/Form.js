﻿/* * 创建人：超级管理员
 * 日  期：2019-03-18 10:37
 * 描  述：退供应商单查询
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
            $('#Mes_BackSupplyDetail').jfGrid({
                headData: [
                    {
                        label: '主键', name: 'ID', width: 160, align: 'left', editType: 'label', hidden: true
                    },
                    {
                        label: '退供应商单号', name: 'B_BackSupplyNo', width: 160, align: 'left', editType: 'label', hidden: true
                    },
                    {
                        label: '物料编码', name: 'B_GoodsCode', width: 80, align: 'left', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'B_GoodsName', width: 160, align: 'left', editType: 'label'
                    },
                    {
                        label: '单位', name: 'B_Unit', width: 40, align: 'left', editType: 'label'
                    },
                    {
                        label: '不含税单价', name: 'B_Price', width: 80, align: 'left', editType: 'label'
                    },
                     {
                         label: '含税单价', name: 'B_TaxPrice', width: 80, align: 'left', editType: 'label'
                     }, {
                         label: "不含税金额", name: "不含税金额", width: 100, align: "left", formatter: function (value, row, dfop) {
                             if (row.B_Qty == "" || row.B_Qty == null || row.B_Qty == undefined) {
                                 return row.不含税金额 = 0;
                             }
                             else {
                                 return row.不含税金额 = row.B_Price * row.B_Qty;
                             }
                         }, statistics: true
                     }, {
                         label: "含税金额", name: "含税金额", width: 100, align: "left", formatter: function (value, row, dfop) {
                             if (row.B_Qty == "" || row.B_Qty == null || row.B_Qty == undefined) {
                                 return row.含税金额 = 0;
                             }
                             else {
                                 return row.含税金额 = row.B_TaxPrice * row.B_Qty;
                             }
                         }, statistics: true
                     },
                    {
                        label: '数量', name: 'B_Qty', width: 80, align: 'left', statistics: true, editType: 'label'
                    },
                    {
                        label: '批次', name: 'B_Batch', width: 160, align: 'left', editType: 'label'
                    },
                    {
                        label: '备注', name: 'B_Remark', width: 160, align: 'left', editType: 'label'
                    },
                ],
                isAutoHeight: true,
                footerrow: true,
                minheight: 400,
                isStatistics: true
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/Mes_BackSupplyQuery/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_BackSupplyDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
        postData.strEntity = JSON.stringify($('[data-table="Mes_BackSupplyHead"]').GetFormData());
        postData.strmes_BackSupplyDetailList = JSON.stringify($('#Mes_BackSupplyDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/Mes_BackSupplyQuery/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
