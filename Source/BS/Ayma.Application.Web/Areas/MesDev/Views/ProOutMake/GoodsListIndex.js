/* * 创建人：超级管理员
 * 日  期：2018-08-16 20:02
 * 描  述：商品列表
 */
var refreshGirdData;
//上级元素的刷新表格方法
var parentRefreshGirdData;
//上级元素的删除表格方法;
var parentRemoveGridData;
//上级元素的id
var parentFormId = request('formId');
//仓库编码
var stockCode = request('stockCode');
var newArray = [];
//查询数据
var queryJson;
//关闭窗口
var closeWindow;
//批次时间
var batch = request('batch');
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
            // 刷新
            $('#am_refresh').on('click', function () {
                location.reload();
            });
            $('#multiple_condition_query').MultipleQuery(function (queryJson) {
                page.search(queryJson);
            }, 200, 350);
           
            //输入关键字搜索
            $('#txt_Keyword').bind('keydown', function (event) {
                if (event.keyCode == "13") {
                    $('#am_btn_querySearch').click();
                }
            });
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
                    newArray[i]['P_GoodsCode'] = newArray[i]['I_GoodsCode'];
                    newArray[i]['P_GoodsName'] = newArray[i]['I_GoodsName'];
                    newArray[i]['P_Unit'] = newArray[i]['I_Unit'];
                    newArray[i]['I_Qty'] = newArray[i]['I_Qty'];
                    newArray[i]['P_Price'] = newArray[i]['I_Price'];
                    newArray[i]["P_Qty"] = quantity;
                    newArray[i]['P_Batch'] = newArray[i]["I_Batch"];
                    newArray[i]["ID"] = newArray[i]['ID'];
                    array.push(newArray[i]);
                }
                parentRefreshGirdData(array);

            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/ProOutMake/GetMaterList?stockCode=' + stockCode,
                headData: [
                    { label: "物料编码", name: "I_GoodsCode", width: 130, align: "left", },
                    { label: "物料名称", name: "I_GoodsName", width: 130, align: "left" },
                    { label: "单位", name: "I_Unit", width: 60, align: "left" },
                    { label: "数量", name: "I_Qty", width: 60, align: "left" },
                    { label: "单价", name: "I_Price", width: 60, align: "left" },
                    { label: "批次", name: "I_Batch", width: 120, align: "left" }
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
                        if (!allCheck.is(":checked")) {
                            //提示用户选择最早批次
                            var list = $('#girdtable').jfGridGet('rowdatas');
                            var data = [];
                            data = list.filter(function (item) {
                                return item.I_GoodsCode == row['I_GoodsCode']
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
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方
                        row['P_GoodsCode'] = row['I_GoodsCode'];
                        row['P_GoodsName'] = row['I_GoodsName'];
                        row['P_Unit'] = row['I_Unit'];
                        row['I_Qty'] = row['I_Qty'];
                        row['P_Price'] = row['I_Price'];
                        row["P_Qty"] = quantity;
                        row['P_Batch'] = row['I_Batch'];
                        row["ID"] = row['ID'];
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
                               
                                if (rows[i]['I_GoodsCode'] == rowslist[j]['P_GoodsCode'] && rows[i]['I_Batch'] == rowslist[j]['P_Batch']) {
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
