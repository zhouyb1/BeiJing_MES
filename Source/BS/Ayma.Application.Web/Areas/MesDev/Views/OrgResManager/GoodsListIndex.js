/* * 创建人：Yabo,Zhou
 * 描  述：组装物料列表
 */
var stock = request('stock');
var refreshGirdData;
//上级元素的刷新表格方法
var parentRefreshGirdData;
//上级元素的删除表格方法 
var parentRemoveGridData;
//上级元素的id
var parentFormId = request('formId');
var newArray = [];
//查询数据
var queryJson;
//关闭窗口
var closeWindow;



//处理前物料列表
var goodsOutList = [];
//处理后物料列表
var goodsSecList = [];

//仓库编码
var bootstrap = function ($, ayma) {
    "use strict";
    var page = {
        init: function () {
            var rows;
            rows = top.GetGoodsListHead();
            for (var i = 0; i < rows.length; i++) {
                goodsOutList.push({ O_GoodsCode: rows[i].O_GoodsCode, O_SecGoodsCode: rows[i].O_SecGoodsCode });
            }
            rows = top.GetGoodsListDetails();
            for (var i = 0; i < rows.length; i++) {
                goodsSecList.push({ O_SecGoodsCode: rows[i].O_SecGoodsCode });
            }


            page.initGird();
            page.bind();
         
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
            //        newArray[i]['O_GoodsCode'] = newArray[i]['G_GoodsCode'];
            //        newArray[i]['O_GoodsName'] = newArray[i]['G_GoodsName'];
            //        newArray[i]['O_Unit'] = newArray[i]['G_Unit'];
            //        newArray[i]["O_Qty"] = quantity;
            //        newArray[i]['O_Batch'] = newArray[i]["G_Batch"];
            //        newArray[i]["ID"] = newArray[i]["G_ID"];
            //        array.push(newArray[i]);
            //    }
            //    parentRefreshGirdData(array);

            //});
        },
        // 初始化列表
        initGird: function () {
            $('#girdtable').jfGrid({
                url: top.$.rootUrl + '/MesDev/OrgResManager/GetGoodsList',
                headData: [
                   {
                       label: "转换的前物料",
                       name: "B",
                       width: 160,
                       align: "center",
                       children: [
                           { label: "物料编码", name: "o_goodscode", width: 90, align: "center", },
                           { label: "物料名称", name: "o_goodsname", width: 120, align: "center" },
                           { label: "单价", name: "o_price", width: 60, align: "center" },
                           { label: "单位", name: "o_unit", width: 60, align: "center" },
                           { label: "数量", name: "o_qty", width: 90, align: "center" },
                       ]
                   },
                    {
                        label: "转换后的物料",
                        name: "B",
                        width: 160,
                        align: "center",
                        children: [
                           { label: "物料编码", name: "o_secgoodscode", width: 90, align: "center", },
                           { label: "物料名称", name: "o_secgoodsname", width: 120, align: "center" },
                           { label: "单位", name: "o_secunit", width: 60, align: "center" },
                        ]
                    }
                ],
                isMultiselect: true,         // 是否允许多选
                isShowNum: true,
                isPage: true,
                sord: 'ASC',
                onSelectRow: function (rowobj, rowdata, rowid) {
                    var isChecked = $("[rownum='" + rowid + "']").find("input[role='checkbox']");

                    var row = {};
                    row.O_GoodsCode = rowdata.o_goodscode;
                    row.O_GoodsName = rowdata.o_goodsname;
                    row.O_Unit = rowdata.o_unit;
                    row.O_Price = rowdata.o_price;
                    row.StockQty = rowdata.o_qty;

                    row.O_SecGoodsCode = rowdata.o_secgoodscode;
                    row.O_SecGoodsName = rowdata.o_secgoodsname;
                    row.O_SecUnit = rowdata.o_secunit;

                    if (!isChecked.is(":checked")) {
                        //移除
                        var ismove = false;
                        for (var i = 0; i < goodsOutList.length; i++) {
                            if (goodsOutList[i].O_GoodsCode == row.O_GoodsCode && goodsOutList[i].O_SecGoodsCode == row.O_SecGoodsCode) {
                                ismove = true;
                                goodsOutList.splice(i, 1);
                                break;
                            }
                        }
                        if (ismove) {
                            top.FormRemoveGirdDataHead(row);
                        }
                  

                        ismove = true;
                        for (var i = 0; i < goodsOutList.length; i++) {
                            if (goodsOutList[i].O_SecGoodsCode == row.O_SecGoodsCode) {
                                ismove = false;
                                break;
                            }
                        }
                        if (ismove) {
                            for (var i = 0; i < goodsSecList.length; i++) {
                                if (goodsSecList[i].O_SecGoodsCode == row.O_SecGoodsCode ) {
                                    goodsSecList.splice(i, 1);
                                    top.FormRemoveGirdDataDetails(row);
                                    break;
                                }
                            }
                        }

                    } else {
                        //新增

                        //判断是否已经添加过
                        var isadd = true;
                        for (var i = 0; i < goodsOutList.length; i++) {
                            if (goodsOutList[i].O_GoodsCode == row.O_GoodsCode && goodsOutList[i].O_SecGoodsCode == row.O_SecGoodsCode) {
                                isadd = false;
                                break;;
                            }
                        }
                        if (isadd) {
                            top.FormRefreshGirdDataHead(row);
                            goodsOutList.push({ O_GoodsCode: row.O_GoodsCode,O_SecGoodsCode:row.O_SecGoodsCode});
                        }


                        isadd = true;
                        for (var i = 0; i < goodsSecList.length; i++) {
                            if (goodsSecList[i].O_SecGoodsCode == row.O_SecGoodsCode ) {
                                isadd = false;
                                break;
                            }
                        }
                        if (isadd) {
                            top.FormRefreshGirdDataDetails(row);
                            goodsSecList.push({ O_SecGoodsCode: row.O_SecGoodsCode });
                        }
                    }
                },

                onRenderComplete: function (rows) {
                    newArray = rows;
                    var rowslist = top.NewGirdData();
                    if (JSON.stringify(rowslist) !== '[]') {
                        for (var i = 0; i < rows.length; i++) {
                            for (var j = 0, len = rowslist.length; j < len; j++) {
                                if (rows[i]['o_goodscode'] == rowslist[j]['O_GoodsCode'] && rows[i]["o_secgoodscode"]==rowslist[j]["O_SecGoodsCode"]) {
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
            param = $("#txt_Keyword").val();
            queryJson.stock = stock;
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
