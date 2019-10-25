﻿/* * 创建人：Yabo,Zhou
 * 描  述：商品列表
 */

var refreshGirdData;
//上级元素的刷新表格方法
var parentRefreshGirdData;
//上级元素的删除表格方法;
var parentRemoveGridData;
//班组的编号
var C_TeamCode = request('C_TeamCode');
//上级元素的id
var parentFormId = request('formId');
var newArray = [];
//查询数据
var queryJson;
//关闭窗口
var closeWindow;
var stockCode = request('stockCode');//物料仓库

var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            //获取父级iframe中的刷新商品列表方法
            parentRefreshGirdData = $(top[parentFormId]).context.firstChild.contentWindow.refreshGirdData;
            parentRemoveGridData = $(top[parentFormId]).context.firstChild.contentWindow.RemoveGridData;
        },
        bind: function () {
           
            //输入关键字搜索
            $("#txt_Keyword").on('keydown', function (event) {
                if (event.keyCode == "13") {
                    $('#am_btn_querySearch').click();
                }
            });
            //查询
            $('#am_btn_querySearch').on('click', function () {
                page.search();
            });
            //数量验证
            $("#quantity").on('keyup', function () {
                var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                if (quantity < 0) {
                    ayma.alert.error("请别输入负数！");
                    $("#quantity").val("");
                    return;
                }
                if (quantity == "0") {
                    return;
                } else {
                    var newQty = parseInt(quantity);
                    ayma.loading(true);
                    for (var i = 0; i < newArray.length; i++) {
                        if (newQty > newArray[i]["C_Qty"]) {
                            ayma.alert.error("其中有输入数量大于库存数量的库存，不能一键设置");
                            $("#quantity").val("");
                            ayma.loading(false);
                            return;
                        }
                    }
                    ayma.loading(false);
                }
            });
            ////全选
            //$("#jfgrid_all_cb_girdtable").on('click', function () {
            //    var array = [];
            //    //获取一键数量
            //    var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
            //    for (var i = 0; i < newArray.length; i++) {
            //        //copy需要更改的地方
            //        newArray[i]["C_OrderDate"] = newArray[i]['I_OrderDate'];
            //        newArray[i]['C_GoodsCode'] = newArray[i]['I_GoodsCode'];
            //        newArray[i]['C_GoodsName'] = newArray[i]['I_GoodsName'];
            //        newArray[i]['C_SupplyCode'] = newArray[i]['I_SupplyCode'];
            //        newArray[i]['C_SupplyName'] = newArray[i]['I_SupplyName'];
            //        newArray[i]['C_Unit'] = newArray[i]['I_Unit'];
            //        newArray[i]["C_Qty"] = quantity;
            //        newArray[i]['C_Batch'] = newArray[i]['I_Batch'];
            //        newArray[i]["ID"] = newArray[i]['ID'];
            //        newArray[i]["C_Price"] = newArray[i]['I_Price'];
            //        newArray[i]["StockQty"] = newArray[i]["I_Qty"];
            //        array.push(newArray[i]);
            //    }
            //    parentRefreshGirdData(array);

            //});
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/PickingMater/GetMaterList?stockCode=' + stockCode,
                headData: [
                    { label: "物料编码", name: "I_GoodsCode", width: 130, align: "left", },
                    { label: "物料名称", name: "I_GoodsName", width: 130, align: "left" },
                    { label: "供应商编码", name: "I_SupplyCode", width: 130, align: "left" },
                    { label: "供应商名称", name: "I_SupplyName", width: 130, align: "left" },
                    { label: "价格", name: "I_Price", width: 60, align: "left" },
                    { label: "单位", name: "I_Unit", width: 60, align: "left" },
                    { label: "数量", name: "I_Qty", width: 60, align: "left" },
                    { label: "批次", name: "I_Batch", width: 80, align: "left" },
                   
                ],
                mainId: 'ID',
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sidx: 'I_GoodsCode,I_Batch',
                sord: 'ASC',
                onSelectRow: function (rowdata, row, rowid) {
                    //if ($("input[role='checkbox']:checked").eq(0).attr("id")) {
                    //    return;
                    //} 
                    var allCheck = $("#jfgrid_all_cb_girdtable");
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    if (isChecked.is(":checked")) {         
                        if (row['I_Qty'] < 0) {
                            isChecked.attr('checked', false)  //移除 checked 状态 
                            ayma.alert.error('库存为负数');
                        }
                        else { 
                            if (!allCheck.is(":checked")) {
                                //提示用户选择最早批次                          
                                var list = $('#girdtable').jfGridGet('rowdatas');
                                var data = [];
                                data = list.filter(function (item) {
                                    if (item.I_Qty > 0) {
                                        return item.I_GoodsCode == row['I_GoodsCode'];
                                    }
                                })
                                var min = data[0].I_Batch;
                                var len = data.length;
                                for (var i = 1; i < len; i++) {
                                    if (data[i].I_Batch < min) {
                                        min = data[i].I_Batch;
                                    }
                                }
                                if (row['I_Batch'] > min) {
                                    ayma.alert.error('请优先使用最早批次为' + min + '的【' + row['I_GoodsName'] + '】');
                                }
                            }
                        }
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方
                        row["C_OrderDate"] = row['I_OrderDate'];
                        row['C_GoodsCode'] = row['I_GoodsCode'];
                        row['C_GoodsName'] = row['I_GoodsName'];
                        row['C_SupplyCode'] = row['I_SupplyCode'];
                        row['C_SupplyName'] = row['I_SupplyName'];
                        row['C_Unit'] = row['I_Unit'];
                        row['C_Batch'] = row["I_Batch"];
                        row["C_Qty"] = quantity;
                        row["ID"] = row['ID'];
                        row["C_TeamCode"] = row["I_TeamCode"];
                        row["C_TeamName"] = row["I_TeamName"];
                        row["StockQty"] = row["I_Qty"];
                        row["C_Price"] = row["I_Price"];
                        //2019年7月18日14:16:17 行数据唯一标识
                        //row["row_sign"] = row["I_GoodsCode"] + row["I_Batch"];
                        parentRefreshGirdData([], row);
                    }
                    if (!isChecked.is(":checked")) {
                        parentRemoveGridData(row);
                    }
                },
                onRenderComplete: function (rows) {
                    newArray = rows;
                    var rowslist = top.NewGirdData();
                    if (JSON.stringify(rowslist) !== '[]') {
                        var rowlistlenght = rowslist[0]["ID"] == undefined ? 0 : rowslist.length;
                        for (var i = 0; i < rows.length; i++) {
                            for (var j = 0; j < rowlistlenght; j++) {
                                if (rows[i]['I_GoodsCode'] == rowslist[j]['C_GoodsCode'] && rows[i]["I_Batch"]==rowslist[j]["C_Batch"]) {
                                    $("[rownum='rownum_girdtable_" + i + "']").eq(2).children().attr("checked", "checked");
                                    break;
                                }
                            }
                        }
                    }

                }
            });
            page.search();
        },
        search: function (param) {
            //queryJson = param;
            
            queryJson = param || {};
            queryJson.stockCode = stockCode;
            param = $("#txt_Keyword").val();
            $('#girdtable').jfGridSet('reload', { param: { keyword: param,  queryJson: JSON.stringify(queryJson) } });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    closeWindow = function () {
        top.layer.close(parent.layer.getFrameIndex(window.name));
    }
    page.init();
}
