/* * 创建人：超级管理员
 * 日  期：2019-03-13 11:57
 * 描  述：出库单制作
 */
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var stockCode;
var parentFormId = request('formId');
var acceptClick;
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
        bind: function () {
            //绑定线边仓仓库
            var dfop = {
                type: 'default',
                value: 'S_Name',
                text: 'S_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetLineStockList',
                // 访问数据接口参数
                param: {}
            };
            //绑定仓库
            $('#I_StockName').select(dfop).on('change', function() {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function(data) {
                        var entity = JSON.parse(data).data;
                        stockCode = entity.S_Code;
                        $("#I_StockCode").val(stockCode);
                    }
                });
            });
            $('#I_WorkShop').select({
                type: 'default',
                value: 'W_Code',
                text: 'W_Name',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetWorkShopList',
                // 访问数据接口参数
                param: {}
            });
            var orderNo = "";
            if (!!keyValue) {//根据主键获取生产订单号
                $.ajax({
                    url: top.$.rootUrl + '/MesDev/InWorkShopManager/GetOrderNoBy?keyValue=' + keyValue,
                    type: "GET",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        orderNo = data.info;
                    }
                });
            }
            $('#I_OrderNo').select({
                type: 'default',
                value: 'P_OrderNo',
                text: 'P_OrderNo',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/MesDev/Tools/GetProductOrderList',
                // 访问数据接口参数
                param: { orderNo: orderNo }
            });

            $('#Mes_InWorkShopDetail').jfGrid({
                headData: [
                    { label: "物料编码", name: "I_GoodsCode", width: 130, align: "left" },
                    { label: "物料名称", name: "I_GoodsName", width: 130, align: "left" },
                    { label: "单位", name: "I_Unit", width: 60, align: "left" },
                    { label: "单价", name: "I_Price", width: 60, align: "left" },
                    {
                        label: "数量", name: "I_Qty", width: 60, align: "left", editType: 'numinput',
                        editOp: {
                            callback: function (rownum, row) {
                                if (row.I_Qty != undefined && !!row.I_Qty) {
                                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(row.I_Qty.toString().replace('.', ''))) {
                                        ayma.alert.error("数量必须是非负数.");
                                        row.I_Qty = 0;
                                    }
                                }
                                
                            }
                        }
                    },
                    {
                        label: "批次", name: "I_Batch", width: 80, align: "left", editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (/\D/.test(row.I_Batch.toString().replace('.', ''))) { //验证只能为数字
                                    row.I_Batch = 0;
                                }
                                if (row.I_Batch != undefined && !!row.I_Batch) {
                                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(row.I_Batch.toString().replace('.', ''))) {
                                        ayma.alert.error("批次必须是非负数.");
                                        row.I_Batch = 0;
                                    }
                                }
                            }
                        }
                    }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 330,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                inputCount: 2
            });
            //添加物料
            $('#am_add').on('click', function () {
                ayma.layerForm({
                    id: 'MaterListForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/InWorkShopManager/MaterListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 750,
                    height: 500,
                    maxmin: true,
                    callback: function (id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            ////订单校验
            //$('#O_OrderNo').on('blur', function () {
            //    var orderNo = $.trim($(this).val()); //去除空格
            //    $.ajax({
            //        type: "get",
            //        url: top.$.rootUrl + '/MesDev/Tools/IsOrderNo',
            //        data: { tables: "Mes_ProductOrderHead", field: "P_OrderNo", orderNo: orderNo },
            //        success: function(data) {
            //            var isOk = JSON.parse(data).data;
            //            if (!isOk) {
                         
            //                ayma.alert.error("订单不存在");
            //            } else {
            //                //$("#O_OrderNo").removeClass("am-field-error");
            //                //$("#isCode").remove();
            //            }
            //        }
            //    });
            //});
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/InWorkShopManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_InWorkShopDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_InWorkShopDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_InWorkShopDetail').jfGridGet('rowdatas');
        if (data.length==0) {
            ayma.alert.error('请添加物料');
            return false;
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_InWorkShopHead"]').GetFormData());
        postData.strmes_InWorkShopDetailList = JSON.stringify(data);
        $.SaveForm(top.$.rootUrl + '/MesDev/InWorkShopManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    top.NewGirdData = function () {
        return $('#Mes_InWorkShopDetail').jfGridGet('rowdatas');
    }
    //表格商品添加
    top.refreshGirdData = function (data, row) {
        var rows = $('#Mes_InWorkShopDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                var flagRow = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].I_GoodsCode == row.i_goodscode) {
                        flagRow = false;
                    }
                }
                if (flagRow) {
                    rows.push(row);
                }
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp.get(data[i])) {
                    tmp.set(data[i], 1);
                    var flag = true;
                    //加个循环判断数组重复
                    for (var j = 0; j < rows.length; j++) {
                        if (rows[j].I_GoodsCode == data[i].i_goodscode) {
                            flag = false;
                        }
                    }
                    if (flag) {
                        rows.push(data[i]);
                    }
                }
            }
        }
        //数组过滤
        var filterarray = $.grep(rows, function (item) {
            return item["I_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    top.RemoveGridData = function (row) {
        var rows = $('#Mes_InWorkShopDetail').jfGridGet('rowdatas');
        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["I_GoodsCode"] == row["i_goodscode"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
  
    page.init();
}
