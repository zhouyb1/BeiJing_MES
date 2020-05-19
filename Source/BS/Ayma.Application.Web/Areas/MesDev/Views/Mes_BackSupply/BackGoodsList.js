/* * 创建人：Yabo,Zhou
 * 描  述：商品列表
 */

var refreshGirdData;
//上级元素的刷新表格方法
var parentRefreshGirdData;
//上级元素的删除表格方法;
var parentRemoveGridData;
//上级元素的id
var parentFormId = request('formId');
var newArray = [];
//查询数据
var queryJson;
//关闭窗口
var closeWindow;
//仓库编码
var stockCode = request('stockCode');
//供应商编码
var supplyCode = request('supplyCode');
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
                        if (newQty > newArray[i]["i_qty"]) {
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
            //        newArray[i]['B_GoodsCode'] = newArray[i]['i_goodscode'];
            //        newArray[i]['B_GoodsName'] = newArray[i]['i_goodsname'];
            //        newArray[i]['B_Unit'] = newArray[i]['i_unit'];
            //        newArray[i]['B_Batch'] = newArray[i]['i_batch'];
            //        newArray[i]['Qty'] = newArray[i]['i_qty'];
            //        newArray[i]["B_Qty"] = quantity;
            //        //newArray[i]['ID'] = newArray[i]['id'];
            //        array.push(newArray[i]);
            //    }
            //    parentRefreshGirdData(array);

            //});
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/Mes_BackSupply/GetBackGoodsList',
                headData: [
                    { label: "物料编码", name: "i_goodscode", width: 80, align: "left", },
                    { label: "物料名称", name: "i_goodsname", width: 130, align: "left" },
                    { label: "单位", name: "i_unit", width: 80, align: "left" },
                    { label: "单价(不含税)", name: "i_price", width: 90, align: "left" },
                    { label: "库存", name: "i_qty", width: 90, align: "left" },
                    { label: "批次", name: "i_batch", width: 80, align: "left" }
                ],
                mainId: 'ID',
                isMultiselect: true,         // 是否允许多选
                reloadSelected: true,
                isShowNum: true,
                isPage: true,
                sidx: 'i_goodscode,i_batch',
                sord: 'ASC',
                onSelectRow: function (rowdata, row, rowid) {
                    //if ($("input[role='checkbox']:checked").eq(0).attr("id")) {
                    //    return;
                    //}
                    var allCheck = $("#jfgrid_all_cb_girdtable");
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    if (isChecked.is(":checked")) {
                        if (row['i_qty'] <= 0) {
                            isChecked.attr('checked', false);  //移除 checked 状态
                            ayma.alert.error('库存为负数');
                        }
                        else { 
                        if (!allCheck.is(":checked")) {
                            //提示用户选择最早批次
                            var list = $('#girdtable').jfGridGet('rowdatas');
                            var data = [];
                            data = list.filter(function (item) {
                                if (item.i_qty > 0) {
                                    return item.i_goodscode == row['i_goodscode'];
                                }
                            })
                            var min = data[0].i_batch;
                            var len = data.length;
                            for (var i = 1; i < len; i++) {
                                if (data[i].i_batch < min) {
                                    min = data[i].i_batch;
                                }
                            }
                            for (var i = 0; i < list.length; i++) {
                                if (list[i].i_batch == min && list[i].i_goodscode == data[0].i_goodscode)
                                {
                                    var minrowid = i;
                                }
                            }
                            var minisChecked = $("[rownum='rownum_girdtable_" + minrowid + "']").find("input[role='checkbox']");
                            if (!minisChecked.is(":checked")) {
                                if (row['i_batch'] > min) {
                                    ayma.alert.error('请优先使用最早批次为' + min + '的【' + row['i_goodsname'] + '】');
                                }
                            }
                        }
                    }
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方
                        row['B_GoodsCode'] = row['i_goodscode'];
                        row['B_GoodsName'] = row['i_goodsname'];
                        row['B_Unit'] = row['i_unit'];
                        row['B_Batch'] = row['i_batch'];
                        row['Qty'] = row['i_qty'];
                        row["B_Qty"] = quantity;
                        row["B_Price"] = row["i_price"];
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
                        var rowlistlenght = rowslist[0]["B_GoodsCode"] == undefined ? 0 : rowslist.length;
                        for (var i = 0; i < rows.length; i++) {
                            for (var j = 0; j < rowlistlenght; j++) {
                                if (rows[i]['i_goodscode'] == rowslist[j]['B_GoodsCode'] && rows[i]['i_batch'] == rowslist[j]['B_Batch']) {
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
            queryJson = param|| {}
            param = $("#txt_Keyword").val();
            queryJson.stockCode = stockCode;
            queryJson.supplyCode = supplyCode;
            $('#girdtable').jfGridSet('reload', { param: { keyword: param, queryJson: JSON.stringify(queryJson) } });
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
