/* * 创建人：Yabo,Zhou
 * 描  述：商品列表
 */

var refreshGirdData;
//上级元素的刷新表格方法
//var parentRefreshGirdData;
//上级元素的删除表格方法;
//var parentRemoveGridData;
//上级元素的id
var parentFormId = request('formId');
var stock = request('stock');
var newArray = [];
//查询数据
var queryJson;
//关闭窗口
var closeWindow;
//批次时间 默认当天
var batch = new Date();
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
           
            //输入关键字搜索
            //$("#txt_Keyword").on('keyup', function () {
            //    page.search();
            //});
            $('#txt_Keyword').bind('keydown', function (event) {
                if (event.keyCode == "13") {
                    $('#am_btn_querySearch').click();
                }
            });
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
                }
            });
            //全选
            //$("#jfgrid_all_cb_girdtable").on('click', function () {
            //    var array = [];
            //    //获取一键数量
            //    var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
            //    for (var i = 0; i < newArray.length; i++) {
            //        //copy需要更改的地方
            //        newArray[i]['I_GoodsCode'] = newArray[i].i_goodscode;
            //        newArray[i]['I_GoodsName'] = newArray[i].i_goodsname;
            //        newArray[i]['I_Unit'] = newArray[i].i_unit;
            //        newArray[i]['I_Price'] = newArray[i].i_price;
            //        newArray[i]['I_Batch'] = ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, "");
            //        newArray[i]["I_Qty"] = quantity;
            //        newArray[i]["ID"] = newArray[i].id;
            //        array.push(newArray[i]);
            //    }
            //    top.refreshGirdData(array);

            //});
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/InWorkShopManager/GetGoodsList',
                headData: [
                    { label: "物料名称", name: "o_secgoodsname", width: 125, align: "center" },
                    { label: "物料编码", name: "o_secgoodscode", width: 90, align: "center" },
                    { label: "批次", name: "o_secbatch", width: 100, align: "center" },
                    { label: "单位", name: "o_secunit", width: 60, align: "center" },
                    { label: "库存", name: "o_secqty", width: 90, align: "center" },
                    { label: "单价", name: "o_secprice", width: 60, align: "center" },
                    { label: "制作时间", name: "o_procutiondate", width: 150, align: "center" }
                ],
                mainId: 'id',
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sord: 'ASC',
                sort:'O_ProcutionDate',
                onSelectRow: function (rowdata, row, rowid) {
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");
                    if (isChecked.is(":checked")) {
                        //获取一键数量
                        var quantity = ($("#quantity").val()) == "" ? "0" : $("#quantity").val();
                        //copy需要更改的地方 ayma.formatDate(batch, "yyyy-MM-dd").toString().replace(/-/g, "");
                      
                        row['I_GoodsCode'] = row.o_secgoodscode;
                        row['I_GoodsName'] = row.o_secgoodsname;
                        row['I_Unit'] = row.o_secunit;
                        row['I_Price'] = row.o_secprice;
                        row['I_Batch'] = row.o_secbatch;
                        row["I_Qty"] = row.o_secqty;
                        row["stockQty"] = row.o_secqty;
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
                        var rowlistlenght = rowslist[0]["I_GoodsCode"] == undefined ? 0 : rowslist.length;
                        for (var i = 0; i < rows.length; i++) {
                            for (var j = 0; j < rowlistlenght; j++) {
                                if (rows[i]['o_secgoodscode'] == rowslist[j]['I_GoodsCode']) {
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
            queryJson = queryJson || {};
            queryJson.keyword = $("#txt_Keyword").val();
            queryJson.stock = stock;
            $('#girdtable').jfGridSet('reload', { param: { queryJson: JSON.stringify(queryJson) } });
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
