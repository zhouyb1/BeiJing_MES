/* * 创建人：超级管理员
 * 日  期：2019-03-02 15:05
 * 描  述：生成订单制作
 */
var acceptClick;
var refreshGirdData;
var RemoveGridData;
var tmp = new Map();
var keyValue = request('keyValue');
var parentFormId = request('formId');
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
            $('#P_Status').DataItemSelect({ code: 'ProductOrderStatus' });
            //添加商品
            $("#am_add").on("click", function () {
                var stockCode = $('#R_StockCode').selectGet();
                if (stockCode == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'GoodsForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/ProductOrderMake/GoodsListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 1000,
                    height: 800,
                    maxmin: true,
                    callBack: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_ProductOrderDetail').jfGrid({
                headData: [
                   
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
                ],
                isAutoHeight: true,
                isEidt: true,
                footerrow: true,
                minheight: 450
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/ProductOrderMake/GetFormData?keyValue=' + keyValue, function (data) {
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
        },
        search: function (data) {
            data = data || {};
            $('#Mes_ProductOrderDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_ProductOrderHead"]').GetFormData());
        postData.strmes_ProductOrderDetailList = JSON.stringify($('#Mes_ProductOrderDetail').jfGridGet('rowdatas'));
        $.SaveForm(top.$.rootUrl + '/MesDev/ProductOrderMake/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_ProductOrderDetail').jfGridGet('rowdatas');
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
        var rows = $('#Mes_ProductOrderDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["P_GoodsCode"] == row["g_code"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_ProductOrderDetail').jfGridGet('rowdatas');
    }
    page.init();
}
