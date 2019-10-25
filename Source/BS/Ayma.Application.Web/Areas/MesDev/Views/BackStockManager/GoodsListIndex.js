/* * 创建人：Yabo,Zhou
 * 描  述：退库物料列表
 */
//仓库编码
var stockCode = request('stockCode');
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
            //全选
            //$("#jfgrid_all_cb_girdtable").on('click', function () {
            //    var array = [];
            //    //获取一键数量
            //    var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
            //    for (var i = 0; i < newArray.length; i++) {
            //        //copy需要更改的地方
            //        newArray[i]['B_GoodsCode'] = newArray[i]['G_GoodsCode'];
            //        newArray[i]['B_GoodsName'] = newArray[i]['G_GoodsName'];
            //        newArray[i]['B_Unit'] = newArray[i]['G_Unit'];
            //        newArray[i]["B_Qty"] = quantity;
            //        newArray[i]['B_Batch'] = newArray[i]["G_Batch"];
            //        newArray[i]['B_Price'] = newArray[i]["G_Price"];
            //        newArray[i]["ID"] = newArray[i]["G_ID"];
            //        newArray[i]["G_Qty"] = newArray[i]["G_Qty"];//库存
            //        array.push(newArray[i]);
            //    }
            //    parentRefreshGirdData(array);

            //});
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/BackStockManager/GetGoodsList?stockCode=' + stockCode,
                headData: [
                    { label: "物料编码", name: "G_GoodsCode", width: 130, align: "left", },
                    { label: "物料名称", name: "G_GoodsName", width: 130, align: "left" },
                    { label: "单价", name: "G_Price", width: 130, align: "left" },
                    { label: "单位", name: "G_Unit", width: 60, align: "left" },
                    { label: "数量", name: "G_Qty", width: 60, align: "left" },
                    { label: "批次", name: "G_Batch", width: 80, align: "left" }
                ],
                mainId: 'G_ID',
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sidx: 'G_GoodsCode,G_Batch',
                sord: 'ASC',
                onSelectRow: function (rowdata, row, rowid) {
                    //if ($("input[role='checkbox']:checked").eq(0).attr("id")) {
                    //    return;
                    //}
                    var allCheck = $("#jfgrid_all_cb_girdtable");
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    if (isChecked.is(":checked")) {
                        if (row['G_Qty'] <=0) {
                            isChecked.attr('checked', false);  //移除 checked 状态
                            ayma.alert.error('库存为负数');
                        }
                        else {
                            if (!allCheck.is(":checked")) {
                                //提示用户选择最早批次
                                var list = $('#girdtable').jfGridGet('rowdatas');
                                var data = [];
                                data = list.filter(function (item) {
                                    if (item.G_Qty > 0) {
                                        return item.G_GoodsCode == row['G_GoodsCode'];
                                    }
                                    
                                })
                                var min = data[0].G_Batch;
                                var len = data.length;
                                for (var i = 1; i < len; i++) {
                                    if (data[i].G_Batch < min) {
                                        min = data[i].G_Batch;
                                    }
                                }
                                if (row['G_Batch'] > min) {
                                    ayma.alert.error('请优先使用最早批次为' + min + '的【' + row['G_GoodsName'] + '】');
                                }
                            }
                        }
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方
                        row['B_GoodsCode'] = row['G_GoodsCode'];
                        row['B_GoodsName'] = row['G_GoodsName'];
                        row['B_Unit'] = row['G_Unit'];
                        row["B_Qty"] = quantity;
                        row['B_Batch'] = row['G_Batch'];
                        row["ID"] = row["G_ID"];
                        row["G_Qty"] = row["G_Qty"];
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
                                if (rows[i]['G_GoodsCode'] == rowslist[j]['B_GoodsCode'] && rows[i]["G_Batch"] == rowslist[j]["B_Batch"]) {
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
            queryJson = param;
            param = $("#txt_Keyword").val();
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
