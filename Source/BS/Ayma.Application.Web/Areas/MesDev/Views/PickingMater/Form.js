/* * 创建人：超级管理员
 * 日  期：2019-03-11 19:22
 * 描  述：领料单
 */
var acceptClick;
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var parentFormId = request('formId');//父级窗口id
var keyValue = request('keyValue');
var tmp = new Map();
var bootstrap = function ($, ayma) {
    "use strict";
    var selectedRow = ayma.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
$('.am-form-wrap').mCustomScrollbar({theme: "minimal-dark"}); 
            page.bind();
            page.initData();
        },
        bind: function() {
            //绑定仓库
            var dfop = {
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetStockList',
                // 访问数据接口参数
                param: {}
            }
            $("#C_StockName").select(dfop).on('change', function() {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function(data) {
                        var entity = JSON.parse(data).data;
                        $("#C_StockCode").val(entity.S_Code);
                    }
                });
            });

            //绑定目标仓
            $("#C_StockToName").select(dfop).on('change', function() {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function(data) {
                        var entity = JSON.parse(data).data;
                        $("#C_StockToCode").val(entity.S_Code);
                    }
                });
            });
            //添加订单原物料
            $('#am_add').on('click', function() {
                ayma.layerForm({
                    id: 'OrderMaterListForm',
                    title: '添加订单物料',
                    url: top.$.rootUrl + '/MesDev/PickingMater/OrderMaterList?formId=' + parentFormId,
                    width: 700,
                    height: 500,
                    maxmin: true,
                    callback: function(id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_CollarDetail').jfGrid({
                headData: [
                    { label: "领料单", name: "C_CollarNo", width: 130, align: "left", hidden: true },
                    { label: "生产订单", name: "C_OrderNo", width: 130, align: "left", hidden: true },
                    { label: "物料编码", name: "C_GoodsCode", width: 130, align: "left"},
                    { label: "物料名称", name: "C_GoodsName", width: 130, align: "left" },
                    { label: "单位", name: "C_Unit", width: 60, align: "left" },
                    { label: "数量", name: "C_Qty", width: 60, align: "left", editType: 'input' }
                    //{ label: "批次", name: "C_Batch", width: 60, align: "left" }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                inputCount: 2
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/PickingMater/GetFormData?keyValue=' + keyValue, function (data) {
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
        },
        search: function (data) {
            data = data || {};
            $('#Mes_CollarDetail').jfGridSet('refreshdata', { rowdatas: data });
        }

    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_CollarDetail').jfGridGet('rowdatas');
        if (data[0].C_GoodsCode == undefined || data[0].C_GoodsCode == "") {
            ayma.alert.error('请添加物料');
            return false;
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_CollarHead"]').GetFormData());
        postData.strmes_CollarDetailEntity = JSON.stringify(data);
        $.SaveForm(top.$.rootUrl + '/MesDev/PickingMater/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    //表格商品添加
    refreshGirdData = function (data, row) {
        $('#P_OrderNo').val(row.C_OrderNo);
        $('#P_OrderDate').val(row.C_OrderDate);
        var rows = $('#Mes_CollarDetail').jfGridGet('rowdatas');
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
            return item["C_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_CollarDetail').jfGridGet('rowdatas');

        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["M_GoodsCode"] == row["P_GoodsCode"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    top.NewGirdData = function () {
        return $('#Mes_CollarDetail').jfGridGet('rowdatas');
    }
    page.init();
}
