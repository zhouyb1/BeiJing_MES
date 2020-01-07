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
var newArray = [];
//仓库的编码
var O_StockCode = request('O_StockCode');
//供应商编码
var supplyCode = request('supplyCode');
//查询数据
var queryJson;
//关闭窗口
var closeWindow;
//批次时间
var batch = new Date();
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
            $('#txt_Keyword').bind('keydown', function (event) {
                if (event.keyCode == "13") {
                    $('#am_btn_querySearch').click();
                }
            });
            $('#am_btn_querySearch').on('click', function () {
                page.search();
            });
            //输入关键字搜索
            //$("#txt_Keyword").on('keyup', function () {
            //    page.search();
            //});
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
                    newArray[i]['O_GoodsCode'] = newArray[i]['G_Code'];
                    newArray[i]['O_GoodsName'] = newArray[i]['G_Name'];
                    newArray[i]['O_Unit'] = newArray[i]['G_Unit'];
                    newArray[i]['O_Price'] = newArray[i]['G_Price'];
                    newArray[i]["O_Qty"] = quantity;
                    newArray[i]['O_Batch'] = ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, "");
                    newArray[i]["ID"] = newArray[i]['ID'];
                    array.push(newArray[i]);
                }
                parentRefreshGirdData(array);

            });
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/OtherWarehouseReceipt/GetGoodsList',
                headData: [
                    { label: "主键", name: "ID", width: 130, align: "left", hidden: true },
                    { label: "物料编码", name: "G_Code", width: 130, align: "left" },
                    { label: "物料名称", name: "G_Name", width: 130, align: "left" },
                    { label: "保质时间", name: "G_Period", width: 70, align: "left" },
                    {
                        label: "价格", name: "G_Price", width: 70, align: "left"
                    },
                    { label: "单位", name: "G_Unit", width: 70, align: "left" },
                    { label: "包装单位", name: "G_Unit2", width: 70, align: "left" },
                    { label: "包装规格", name: "G_UnitQty", width: 70, align: "left" }

                ],
                mainId: 'ID',
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sidx: 'G_Code',
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
                        row['O_GoodsCode'] = row['G_Code'];
                        row['O_Unit2'] = row['G_Unit2'];
                        row['O_UnitQty'] = row['G_UnitQty'];
                        row['O_GoodsName'] = row['G_Name'];
                        row['O_Unit'] = row['G_Unit'];
                        row['O_Price'] = row['G_Price'];
                        row["O_Qty"] = quantity;
                        row['O_Batch'] = ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, "");
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
                                if (rows[i]['G_Code'] == rowslist[j]['O_GoodsCode']) {
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
            queryJson = param || {};
            queryJson.O_StockCode = O_StockCode;
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
