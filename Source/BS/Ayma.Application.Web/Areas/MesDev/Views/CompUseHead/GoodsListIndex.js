/* * 创建人：超级管理员
 * 日  期：2018-08-16 20:02
 * 描  述：商品列表
 */
var refreshGirdData;
//上级元素的刷新表格方法
//var parentRefreshGirdData;
//上级元素的删除表格方法;
//var parentRemoveGridData;
//上级元素的id
var parentFormId = request('formId');
//仓库编码
var stockCode = request('stockCode');
var newArray = [];
//查询数据
var queryJson;
//关闭窗口
var closeWindow;
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
            page.bind();
            //获取父级iframe中的刷新商品列表方法
            //parentRefreshGirdData = $(top[parentFormId]).context.firstChild.contentWindow.refreshGirdData;
            //parentRemoveGridData = $(top[parentFormId]).context.firstChild.contentWindow.RemoveGridData;
        },
        bind: function () {
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 200, 350);
            //搜索
            $("#btn_Search").on('click', function () {
                page.search();
            });
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
            //商品类型
            $("#GoodsType").DataItemSelect({ code: "GoodsType" });
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
            $("#jfgrid_all_cb_girdtable").on('click', function () {
                var array = [];
                //获取一键数量
                var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                for (var i = 0; i < newArray.length; i++) {
                    //copy需要更改的地方
                    newArray[i]['C_GoodsCode'] = newArray[i]['g_code'];
                    newArray[i]['C_GoodsName'] = newArray[i]['g_name'];
                    newArray[i]['C_Unit'] = newArray[i]['g_unit'];
                    newArray[i]['C_Price'] = newArray[i]['g_price'];
                    newArray[i]["C_Qty"] = quantity;
                    newArray[i]['C_Batch'] = newArray[i]['batch'];
                    //newArray[i]['Qty'] = newArray[i]['qty'];
                    newArray[i]["ID"] = newArray[i]['id'];
                    array.push(newArray[i]);
                }
                top.refreshGirdData(array);

            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/CompUseHead/GetGoodsList?stockCode=' + stockCode,
                headData: [
                    { label: "物料编码", name: "id", width: 130, align: "left", hidden: true },
                    { label: "物料编码", name: "g_code", width: 130, align: "left" },
                    { label: "物料名称", name: "g_name", width: 130, align: "left" },                 
                    { label: "数量", name: "qty", width: 60, align: "left" },
                    { label: "价格", name: "g_price", width: 60, align: "left" },
                    { label: "单位", name: "g_unit", width: 60, align: "left" },
                    { label: "批次", name: "batch", width: 80, align: "left" }
                ],
                mainId: 'id',
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sidx: 'g_code,batch',
                sord: 'ASC',
                onSelectRow: function (rowdata, row, rowid) {
                    //if ($("input[role='checkbox']:checked").eq(0).attr("id")) {
                    //    return;
                    //}
                    var allCheck = $("#jfgrid_all_cb_girdtable");
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    if (isChecked.is(":checked")) {

                        if (!allCheck.is(":checked")) {
                            //提示用户选择最早批次
                            var list = $('#girdtable').jfGridGet('rowdatas');
                            var data = [];
                            data = list.filter(function (item) {
                                return item.g_code == row['g_code']
                            })
                            var min = data[0].batch;
                            var len = data.length;
                            for (var i = 1; i < len; i++) {
                                if (data[i].batch < min) {
                                    min = data[i].batch;
                                }
                            }
                            if (row['batch'] > min) {
                                ayma.alert.error('请优先使用最早批次为' + min + '的【' + row['g_name'] + '】');
                            }
                        }
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方
                        row['C_GoodsCode'] = row['g_code'];
                        row['C_GoodsName'] = row['g_name'];
                        row['C_Unit'] = row['g_unit'];
                        row['C_Price'] = row['g_price'];
                        row["C_Qty"] = quantity;
                        row['C_Batch'] = row['batch'];
                        //row['Qty'] = row['qty'];
                        row["ID"] = row['id'];
                        top.refreshGirdData([], row);
                    }
                    if (!isChecked.is(":checked")) {
                        top.RemoveGridData(row);
                    }
                },
                onRenderComplete: function (rows) {
                    newArray = rows;
                    var rowslist = top.NewGirdData();
                    if (JSON.stringify(rowslist) !== '[]') {
                        var rowlistlenght = rowslist[0]["C_GoodsCode"] == undefined ? 0 : rowslist.length;
                        for (var i = 0; i < rows.length; i++) {
                            for (var j = 0; j < rowlistlenght; j++) {
                                if (rows[i]['g_code'] == rowslist[j]['C_GoodsCode'] && rows[i]['batch'] == rowslist[j]['C_Batch']) {
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
            $('#girdtable').jfGridSet('reload', { param: { keyword: param, stockCode: stockCode, queryJson: JSON.stringify(queryJson) } });
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
