﻿/* * 创建人：超级管理员
 * 日  期：2019-03-14 11:20
 * 描  述：报废单据管理
 */
var refreshGirdData;//表格商品添加
var RemoveGridData;//移除表格
var stockCode;
var parentFormId = request('formId');
var acceptClick;
var keyValue = request('keyValue');
var status = request('status');
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
            if (status == "2") {
                $('#S_StockName').attr('readonly', 'readonly'); 
                $('#S_Remark').attr('readonly', 'readonly'); 
                $("#am_add").attr('disabled',true);
            } if (status == 3) {
                $('#am_add').css('display', 'none');
                $('#S_StockName').attr('readonly', 'readonly');
                $('#S_Remark').attr('readonly', 'readonly');
            }
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
                url: top.$.rootUrl + '/MesDev/Tools/GetStockListByParam',
                // 访问数据接口参数
                param: { strWhere: "S_Kind = 1" }
            };
            //绑定仓库
            $('#S_StockName').select(dfop).on('change', function () {
                if (status=="1") {
                    $('#Mes_ScrapDetail').jfGridSet('refreshdata', { rowdatas: [] });
                }
                var code = $(this).selectGet();
                $.ajax({
                    type: "get",
                    url: top.$.rootUrl + '/MesDev/Tools/ByCodeGetStockEntity',
                    data: { code: code },
                    success: function (data) {
                        var entity = JSON.parse(data).data;
                        stockCode = entity == null ? "" : entity.S_Code;
                        $('#S_StockCode').val(stockCode);
                    }
                });
            });
            //绑定仓库

            //添加报废物料
            $('#am_add').on('click', function () {
                var stock = $('#S_StockName').selectGet();
                if (stock == "") {
                    ayma.alert.error("请选择仓库");
                    return false;
                }
                ayma.layerForm({
                    id: 'MaterListForm',
                    title: '添加物料',
                    url: top.$.rootUrl + '/MesDev/ScrapManager/MaterListIndex?formId=' + parentFormId + '&stockCode=' + stockCode,
                    width: 700,
                    height: 500,
                    maxmin: true,
                    btn: ['关闭'],
                    callback: function(id, index) {
                        return top[id].closeWindow();
                    }
                });
            });
            $('#Mes_ScrapDetail').jfGrid({
                headData: [
                    { label: "物料编码", name: "S_GoodsCode", width: 130, align: "left", },
                    { label: "物料名称", name: "S_GoodsName", width: 130, align: "left" },
                    { label: "单价", name: "S_Price", width: 130, align: "left", },
                    { label: "单位", name: "S_Unit", width: 60, align: "left",hidden:true },
                    {
                        label: "数量", name: "S_Qty", width: 60, align: "left", statistics: true, editType: 'input',
                        editOp: {
                            callback: function (rownum, row) {
                                if (row.S_Qty != undefined && !!row.S_Qty) {
                                    if (! /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/.test(row.S_Qty.toString().replace('.', ''))) {
                                        ayma.alert.error("数量必须是非负数.");
                                        row.S_Qty = 0;
                                    }
                                }
                                if (row.S_Qty > row.G_Qty) {
                                        ayma.alert.error("数量不能大于库存数量");
                                        row.R_Qty = 0;
                                }
                            }
                        }
                    },
                         {
                             label: "金额", name: "金额", width: 60, align: "left", formatter: function (value, row, dfop) {
                                 if (row.S_Qty == "" || row.S_Qty == null || row.S_Qty == undefined) {
                                     return row.金额 = 0;
                                 }
                                 else {
                                     return row.金额 = row.S_Price * row.S_Qty;
                                 }
                             }, statistics: true
                         },

                    { label: "库存", name: "G_Qty", width: 60, align: "left", hidden: keyValue == "" ? false : true },

                    { label: "批次", name: "S_Batch", width: 80, align: "left" }
                ],
                isAutoHeight: false,
                footerrow: true,
                minheight: 400,
                isEidt: status == 1 || status == "" ? true : false,
                isMultiselect: status == 1 || status == "" ? true : false,
                height: 300,
                inputCount: 1,
                isStatistics:true
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.SetForm(top.$.rootUrl + '/MesDev/ScrapManager/GetFormData?keyValue=' + keyValue+'&state=1', function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#Mes_ScrapDetail').jfGridSet('refreshdata', { rowdatas: data[id] });
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
            $('#Mes_ScrapDetail').jfGridSet('refreshdata', { rowdatas: data });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').Validform()) {
            return false;
        }
        var data = $('#Mes_ScrapDetail').jfGridGet('rowdatas');
        if (data.length==0||data[0].S_GoodsCode == null) {
            ayma.alert.error('请添加物料');
            return false;
        }
        for (var i = 0; i < data.length; i++) {
            data[i].S_Price = null;
        }
        var postData = {
            strEntity: JSON.stringify($('body').GetFormData()),
            detailList: JSON.stringify(data)
        };
        $.SaveForm(top.$.rootUrl + '/MesDev/ScrapManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    
    top.NewGirdData = function () {
        return $('#Mes_ScrapDetail').jfGridGet('rowdatas');
    }
    //表格商品添加
    refreshGirdData = function (data, row) {
        var rows = $('#Mes_ScrapDetail').jfGridGet('rowdatas');
        if (data.length == 0) { //单选
            if (!tmp.get(row)) {
                tmp.set(row, 1);
                var rowFlag = true;
                //加个循环判断数组重复
                for (var k = 0; k < rows.length; k++) {
                    if (rows[k].G_GoodsCode == row.S_GoodsCode && rows[k].G_Batch == row.S_Batch) {
                        rowFlag = false;
                    }
                }
                if (rowFlag) {
                    rows.push(row);
                }
            }
        } else { //多选                  
            for (var i = 0; i < data.length; i++) {
                if (!tmp.get(data[i])) {
                    tmp.set(data[i], 1);
                    var isExist = true;
                    for (var j = 0; j < rows.length; j++) {
                        if (data[i].G_GoodsCode == rows[j].S_GoodsCode && data[i].G_Batch == rows[j].S_Batch) {
                            isExist = false;
                        }
                    }
                    if (isExist) {
                        rows.push(data[i]);
                    }
                }
            }
        }
        //数组过滤
        var filterarray = $.grep(rows, function (item) {
            return item["S_GoodsCode"] != undefined;
        });
        page.search(filterarray);
    };
    //表格商品删除
    RemoveGridData = function (row) {
        var rows = $('#Mes_ScrapDetail').jfGridGet('rowdatas');
        for (var i = 0; i < rows.length; i++) {
            if (rows[i]["S_GoodsCode"] == row["G_GoodsCode"]&&rows[i]["S_Batch"] == row["G_Batch"]) {
                rows.splice(i, 1);
                tmp.delete(row);
                page.search(rows);
            }
        }
    };
    page.init();
}
