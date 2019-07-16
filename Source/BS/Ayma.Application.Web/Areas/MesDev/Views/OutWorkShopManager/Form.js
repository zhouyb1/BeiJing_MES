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
            //绑定仓库
            var dfop = {
                type: 'default',
                value: 'S_Code',
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
            $('#O_StockName').select(dfop).on('change', function() {
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function(data) {
                        var entity = JSON.parse(data).data;
                        stockCode = entity == null ? "" : entity.S_Code;
                        $("#O_StockCode").val(stockCode);
                    }
                });
            });
            $('#O_WorkShop').select({
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
                    url: top.$.rootUrl + '/MesDev/OutWorkShopManager/GetOrderNoBy?keyValue=' + keyValue,
                    type: "GET",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        orderNo = data.info;
                    }
                });
            }
            $('#O_OrderNo').select({
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

            $('#Mes_OutWorkShopDetail').jfGrid({
                headData: [
                    { label: "物料编码", name: "O_GoodsCode", width: 130, align: "left" },
                    { label: "物料名称", name: "O_GoodsName", width: 130, align: "left" },
                    { label: "单位", name: "O_Unit", width: 60, align: "left" },
                    { label:"单价", name:"O_Price", width:60, align:"left"},
                    {
                        label: "数量", name: "O_Qty", width: 60, align: "left", editType: 'numinput' ,
                        editOp: {
                            callback: function (rownum, row) {
                                if (row.O_Qty != undefined && !!row.O_Qty) {
                                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(row.O_Qty.toString().replace('.', ''))) {
                                        ayma.alert.error("数量必须是非负数.");
                                        row.O_Qty = 0;
                                    }
                                }
                                if (row.O_Qty > row.I_Qty) {
                                    ayma.alert.error("出库数量不能大于库存数量");
                                    row.O_Qty = 0;
                                }
                            }
                        }
                    },
                     {
                         label: '价格', name: 'O_Price', width: 60, align: 'left', editType: 'input',
                         editOp: {
                             callback: function (rownum, row) {
                                 if (/\D/.test(row.O_Price.toString().replace('.', ''))) { //验证只能为数字
                                     row.O_Price = 0;
                                 }

                             }
                         }
                     },
                    { label: '库存', name: 'I_Qty', width: 100, align: 'left', hidden: keyValue == "" ? false : true },
                    { label: "批次", name: "O_Batch", width: 60, align: "left" }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 360,
                isEidt: true,
                isMultiselect: true,
                height: 300,
                inputCount: 2
            });
            //添加物料
            $('#am_add').on('click', function () {
                var stock = $('#O_StockName').selectGet();
                if (stock == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'MaterListForm',
                    title: '添加订单物料',
                    url: top.$.rootUrl + '/MesDev/OutWorkShopManager/MaterListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 700,
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
                $.SetForm(top.$.rootUrl + '/MesDev/OutWorkShopManager/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_OutWorkShopDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_OutWorkShopDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_OutWorkShopDetail').jfGridGet('rowdatas');
        if (data.length==0) {
            ayma.alert.error('请添加物料');
            return false;
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="Mes_OutWorkShopHead"]').GetFormData());
        postData.strmes_OutWorkShopDetailList = JSON.stringify(data);
        $.SaveForm(top.$.rootUrl + '/MesDev/OutWorkShopManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    top.NewGirdData = function () {
        return $('#Mes_OutWorkShopDetail').jfGridGet('rowdatas');
    }
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_OutWorkShopDetail').jfGridGet('rowdatas');
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
            return item["O_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_OutWorkShopDetail').jfGridGet('rowdatas');
        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["O_GoodsCode"] == row["I_GoodsCode"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
  
    page.init();
}
