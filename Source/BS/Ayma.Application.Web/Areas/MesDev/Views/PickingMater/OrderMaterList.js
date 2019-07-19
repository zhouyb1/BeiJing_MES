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
            //全选
            $("#jfgrid_all_cb_girdtable").on('click', function () {
                var array = [];
                //获取一键数量
                var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                for (var i = 0; i < newArray.length; i++) {
                    //copy需要更改的地方
                    newArray[i]["C_OrderDate"] = newArray[i]['I_OrderDate'];
                    newArray[i]['C_GoodsCode'] = newArray[i]['I_GoodsCode'];
                    newArray[i]['C_GoodsName'] = newArray[i]['I_GoodsName'];
                    newArray[i]['C_Unit'] = newArray[i]['I_Unit'];
                    newArray[i]["C_Qty"] = quantity;
                    newArray[i]['C_Batch'] = newArray[i]['I_Batch'];
                    newArray[i]["ID"] = newArray[i]['ID'];
                    newArray[i]["C_Price"] = newArray[i]['I_Price'];;
                    //2019年7月18日14:18:35 行数据唯一标识 防止复选框数据重复添加
                    //newArray[i]["row_sign"] = newArray[i]["I_GoodsCode"] + newArray[i]["I_Batch"];
                    array.push(newArray[i]);
                }
                parentRefreshGirdData(array);

            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/PickingMater/GetMaterList?stockCode='+stockCode,
                headData: [
                    //{ label: "生产订单号", name: "P_OrderNo", width: 130, align: "left",  },
                    //{ label: "订单时间", name: "P_OrderDate", width: 80, align: "left" },
                    { label: "物料编码", name: "I_GoodsCode", width: 130, align: "left", },
                    { label: "物料名称", name: "I_GoodsName", width: 130, align: "left" },
                    { label: "价格", name: "I_Price", width: 60, align: "left" },
                    { label: "单位", name: "I_Unit", width: 60, align: "left" },
                    { label: "数量", name: "I_Qty", width: 60, align: "left" },
                    { label: "批次", name: "I_Batch", width: 80, align: "left" }
                ],
                mainId: 'ID',
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sidx: 'I_GoodsCode',
                sord: 'ASC',
                onSelectRow: function (rowdata, row, rowid) {
                    //if ($("input[role='checkbox']:checked").eq(0).attr("id")) {
                    //    return;
                    //}
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    if (isChecked.is(":checked")) {
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方
                        row["C_OrderDate"] = row['I_OrderDate'];
                        row['C_GoodsCode'] = row['I_GoodsCode'];
                        row['C_GoodsName'] = row['I_GoodsName'];
                        row['C_Unit'] = row['I_Unit'];
                        row['C_Batch'] = row["I_Batch"];
                        row["C_Qty"] = quantity;
                        row["ID"] = row['ID'];
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
