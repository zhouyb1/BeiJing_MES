﻿/* * 创建人：超级管理员
 * 日  期：2019-03-02 17:05
 * 描  述：成品出库单制作
 */
var acceptClick;
var refreshGirdData;
var RemoveGridData;
var tmp = new Map();
var parentFormId = request('formId');
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
            $('#P_Status').DataItemSelect({ code: 'ProOutStatus' });
            //仓库列表
            var dfop1 = {
                type: 'default',
                value: 'S_Code',
                text: 'S_Code',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockList',
                // 访问数据接口参数
                param: {}
            }
            $("#P_StockCode").select(dfop1).on('change', function () {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        $("#P_StockName").val(entity.S_Name);
                    }
                });
            });
            //添加商品
            $("#am_add").on("click", function () {
                var stockCode = $('#P_StockCode').selectGet();
                if (stockCode == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'GoodsListIndexForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/ProOutMake/GoodsListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 1000,
                    height: 800,
                    maxmin: true,
                    callBack: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_ProOutDetail').jfGrid({
                headData: [
                    //{
                    //    label: '出库单号', name: 'P_ProOutNo', width: 160, align: 'left',editType: 'input'
                    //},
                    //{
                    //    label: '生产订单号', name: 'P_OrderNo', width: 160, align: 'left',editType: 'input'
                    //},
                    {
                        label: '物料编码', name: 'P_GoodsCode', width: 160, align: 'left', editType: 'label'
                    },
                    {
                        label: '物料名称', name: 'P_GoodsName', width: 160, align: 'left', editType: 'label'
                    },
                    {
                        label: '单位', name: 'P_Unit', width: 160, align: 'left', editType: 'label'
                    },
                    {
                        label: '数量', name: 'P_Qty', width: 160, align: 'left', editType: 'label'
                    },
                     {
                         label: '价格', name: 'P_Price', width: 60, align: 'left', editType: 'label'
                     },
                    {
                        label: '批次', name: 'P_Batch', width: 160, align: 'left',editType: 'label'
                    },
                    {
                        label: '备注', name: 'P_Remark', width: 160, align: 'left', editType: 'label'
                    },
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 430,
                isEidt: true,
                isMultiselect: true,
                height: 400
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/ProOutMake/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_ProOutDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_ProOutDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_ProOutHead"]').GetFormData());
        postData.strmes_ProOutDetailList = JSON.stringify($('#Mes_ProOutDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/ProOutMake/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_ProOutDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                rows.push(row);
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp.get(data[i])) {
                    tmp.set(data[i], 1);
                    rows.push(data[i]);
                }
            }
        }
        //数组过滤
        var filterarray = $.grep(rows, function (item) {
            return item["P_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_ProOutDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["P_GoodsCode"] == row["G_Code"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_ProOutDetail').jfGridGet('rowdatas');
    }
    page.init();
}
