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
            //搜索
            $("#btn_Search").on('click', function () {
                page.search();
            });
            //输入关键字搜索
            $("#txt_Keyword").on('keyup', function () {
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
                    newArray[i]['M_GoodsCode'] = newArray[i]['G_Code'];
                    newArray[i]['M_GoodsName'] = newArray[i]['G_Code'];
                    newArray[i]['M_Unit'] = newArray[i]['G_Unit'];
                    newArray[i]["M_Qty"] = quantity;
                    newArray[i]['M_Batch'] = batch;
                    newArray[i]["ID"] = newArray[i]['ID'];
                    array.push(newArray[i]);
                }
                parentRefreshGirdData(array);

            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/MaterInBill/GetProductList?stockCode=' + stockCode,
                headData: [
                    { label: "物料编码", name: "ID", width: 130, align: "left", hidden: true },
                    { label: "物料编码", name: "G_Code", width: 130, align: "left" },
                    { label: "物料名称", name: "G_Name", width: 130, align: "left" },
                     {
                         label: "商品类型", name: "G_Kind", width: 160, align: "left",
                         formatterAsync: function (callback, value, row) {

                             ayma.clientdata.getAsync('dataItem', {
                                 key: value,
                                 code: 'GoodsType',
                                 callback: function (_data) {
                                     console.log(_data)
                                     callback(_data.text);
                                 }
                             });
                         }
                     },
                    { label: "保质时间", name: "G_Period", width: 80, align: "left" },
                    { label: "价格", name: "G_Price", width: 60, align: "left" },
                    { label: "单位", name: "G_Unit", width: 60, align: "left" }
                ],
                mainId: 'F_Id',
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sidx: 'G_Code',
                sord: 'ASC',
                onSelectRow: function (rowdata, row, rowid) {
                    if ($("input[role='checkbox']:checked").eq(0).attr("id")) {
                        return;
                    }
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    if (isChecked.is(":checked")) {
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方
                        row['M_GoodsCode'] = row['G_Code'];
                        row['M_GoodsName'] = row['G_Name'];
                        row['M_Unit'] = row['G_Unit'];
                        row["M_Qty"] = quantity;
                        row['M_Batch'] = batch;
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
                                if (rows[i]['G_Code'] == rowslist[j]['M_GoodsCode']) {
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
